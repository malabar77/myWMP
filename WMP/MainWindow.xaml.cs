using System;
using System.Collections.Generic;
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
using Microsoft.DirectX.AudioVideoPlayback;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Media;
using System.Threading;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

namespace WMP
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Audio music;
        private SoundPlayer player;

        public MainWindow()
        {
            InitializeComponent();
	    }

        private void Lecture_Click(object sender, RoutedEventArgs e)
        {
            //System.Media.SoundPlayer player = new SoundPlayer();
            //player.SoundLocation = "..\\..\\LARCY.wav";
            //player.LoadAsync();
            //player.PlayLooping();
            //Thread.Sleep(5000);
            //player.Stop();
            music = new Audio("..\\..\\Birdy.mp3");
            music.Play();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Parcourir_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

	}
    }
}
