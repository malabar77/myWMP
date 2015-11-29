using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Tek3.tp1;

namespace Tp2
{
    public partial class MainWindow : Window
    {
        private double TimerDelay;
        public ObservableCollection<Droid> Droids { get; set; }

        private DroidFactory usine = new DroidFactory(new List<Type>() {
                typeof(C3PO),
                typeof(R2D2),
                typeof(BattleDroid),
                typeof(AstroMech),
        });
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            usine.DroidCreated += new EventHandler<DroidCreatedArg>(droidFactory_DroidCreated);

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Droids = new ObservableCollection<Droid>();
            this.LstDroids.ItemsSource = this.Droids;
            this.TimerDelay = 1.0;
        }

        private void droidFactory_DroidCreated(object sender, DroidCreatedArg e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Writeinlog(e.Droid.Name, e.Droid.isinit());
                this.Droids.Add(e.Droid);
            }));
        }

        private void StartUsine(object sender, RoutedEventArgs e)
        {
            usine.Start(this.TimerDelay);
            UsineStatus.Text = "On";
        }

        private void StopUsine(object sender, RoutedEventArgs e)
        {
            usine.Stop();
            UsineStatus.Text = "Off";
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimerDelay = slider1.Value;
        }
    
        public void Writeinlog(string name, bool init)
        {
            string sline = "Le Droid n'est pas initialisé";
            UsineStatus.Text = name;
            if (init == true)
                sline = "Le Droid est initialisé";
            string[] lines = { "Creation d'un Droid", "C'est un Droid de type " + name, sline };
            System.IO.File.WriteAllLines(@"../../log.txt", lines);
        }
    }
}