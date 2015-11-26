using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tek3.tp1;

namespace Tp2
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Droid> Droids { get; set; }

        private DroidFactory usine = new DroidFactory(new List<Type>() {
                typeof(C3PO),
                typeof(R2D2),
        });
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Droids = new ObservableCollection<Droid>();
        }

        //private void droidFactory_DroidCreated()
        //{
        //    this.Droids.Add();
        //}

        private void StartUsine(object sender, RoutedEventArgs e)
        {
            usine.Start(1);
            UsineStatus.Text = "On";
        }

        private void StopUsine(object sender, RoutedEventArgs e)
        {
            usine.Stop();
            UsineStatus.Text = "Off";
        }
    }
}