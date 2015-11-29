using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Tek3.tp1;
using System.Text;

namespace Tp2
{
    public partial class MainWindow : Window
    {
        private double TimerDelay;
        private int num = 0;
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
                num += 1;
                Writeinlog(e.Droid.Name, e.Droid.isinit(), num);
                this.Droids.Add(e.Droid);
            }));
        }

        private void StartUsine(object sender, RoutedEventArgs e)
        {
            usine.Start(this.TimerDelay);
            UsineStatus.Text = "On";
            string[] lines = { };
            File.WriteAllLines(@"../../log.txt", lines);
        }

        private void StopUsine(object sender, RoutedEventArgs e)
        {
            usine.Stop();
            UsineStatus.Text = "Off";
            num = 0;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimerDelay = slider1.Value;
        }

        public static async void Writeinlog(string name, bool init, int num)
        {
            string sline = "Le Droid n'est pas initialisé";
            string empty = string.Empty;
            if (init == true)
                sline = "Le Droid est initialisé";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Creation d'un Droid n°= " + num);
            sb.AppendLine("C'est un Droid de type " + name);
            sb.AppendLine(sline);
            using (StreamWriter outfile = new StreamWriter(@"../../log.txt", true))
            {
                await outfile.WriteAsync(sb.ToString());
            }
        }
    }
}