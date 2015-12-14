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
using MP = MediaPlayer;
using System.Timers;

namespace WMP
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool onAir = false;
        private int bitrate;
        private int duration;

        public MainWindow()
        {
            InitializeComponent();
            media.MediaOpened += new RoutedEventHandler(media_MediaOpened);
        }

        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            SetDuration(media.NaturalDuration.TimeSpan.TotalSeconds);
        }

        private void Plalistview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string path = (string)Playlistview.SelectedValue;
            media.Source = new Uri(path);
            media.Play();
        }

        private void Lecture_Click(object sender, RoutedEventArgs e)
        {
            DialogFenetre();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Timer.Text = dur.ToString();
        }

        private int GetDuration(string path)
        {
            byte[] TotalBytes = File.ReadAllBytes("..\\..\\Birdy.mp3");
            bitrate = (BitConverter.ToInt32(new[] { TotalBytes[28], TotalBytes[29], TotalBytes[30], TotalBytes[31] }, 0) * 8);
            duration = (TotalBytes.Length - 8) * 8 / bitrate;
            return duration;
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
            fenetre.Filter = "Fichiers Multimedia|*.wav;*.mp3;*.mp4";
            if (fenetre.ShowDialog() == true)
            {
                Playlistview.Items.Add(fenetre.FileName);
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
            if media
            media.LoadedBehavior = MediaState.Play;
        }

    }
}
