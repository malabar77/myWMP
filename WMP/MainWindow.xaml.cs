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
using System.IO;
using System.Timers;
using System.Windows.Controls.Primitives;

namespace WMP
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool onAir = false;

        public MainWindow()
        {
            InitializeComponent();
            media.MediaOpened += new RoutedEventHandler(media_MediaOpened);
        }

        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
//            SetDuration(media.NaturalDuration.TimeSpan.TotalSeconds);
            SetDuration(media.Position.TotalSeconds);
            sliProgress.Minimum = 0;
            sliProgress.Maximum = media.NaturalDuration.TimeSpan.TotalSeconds;
            sliProgress.Value = media.Position.TotalSeconds;

        }

        private void Plalistview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string path = (string)Playlistviewfull.SelectedItem;
            media.Source = new Uri(path);
            media.Play();
        }

        private void Lecture_Click(object sender, RoutedEventArgs e)
        {
            DialogFenetre();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {

        }

        private void SetDuration(double duration)
        {
            int n = 1;
            int minutes = 0;
            int seconds = 0;
            string min;
            string sec;
            while (n * 60 < duration)
                n = n + 1;
            minutes = n - 1;
            seconds = (int)duration % 60;
            min = minutes.ToString();
            sec = seconds.ToString();
            if (minutes < 10)
                min = "0" + minutes.ToString();
            if (seconds < 10)
                sec = "0" + seconds.ToString();
            Timer.Text = min + ":" + sec;
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            //Timer.Text = mediaPlayer.CurrentPosition.ToString() ;
            onAir = false;
            media.LoadedBehavior = MediaState.Pause;

        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            onAir = false;
            media.LoadedBehavior = MediaState.Stop;
        }

        private void DialogFenetre()
        {
            OpenFileDialog fenetre = new OpenFileDialog();
            //fenetre.Filter = "Fichiers Multimedia|*.wav;*.mp3;*.mp4";
            if (fenetre.ShowDialog() == true)
            {
                Playlistview.Items.Add(fenetre.FileName);
                Playlistviewfull.Items.Add(fenetre.SafeFileName);
                this.Title = fenetre.SafeFileName;
                if (onAir == true)
                {
                    onAir = false;
                }
                media.Source = new Uri(fenetre.FileName);
                media.LoadedBehavior = MediaState.Manual;
                media.Play();
            }
        }

        private void Parcourir_Click(object sender, RoutedEventArgs e)
        {
            DialogFenetre();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Volume = (double)Volslider.Value;
	    }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            media.LoadedBehavior = MediaState.Play;
        }
        private bool userIsDraggingSlider = false;


        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            media.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Timer.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

    }
}
