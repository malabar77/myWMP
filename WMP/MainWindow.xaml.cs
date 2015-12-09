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
        //private MP.MediaPlayer mediaPlayer;
        private string mp3 = "Birdy.mp3";
        private string wav = "LARCY.wav";
        private System.Timers.Timer timer;
        private double dur;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Lecture_Click(object sender, RoutedEventArgs e)
        {
            //mediaPlayer = new MP.MediaPlayer();
            //mediaPlayer.Open(mp3);
            ////player = new SoundPlayer();
            //player.SoundLocation = "..\\..\\LARCY.wav";
            //player.Load();
            //player.PlayLooping();
            onAir = true;
            SetDuration(GetDuration(mp3));


            ////Thread.Sleep(5000);
            //player.Stop();
            //music = new Audio("..\\..\\Birdy.mp3");
            //music.Play();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Timer.Text = dur.ToString();
        }

        private int GetDuration(string path)
        {
            byte[] TotalBytes = File.ReadAllBytes("..\\..\\LARCY.wav");
            bitrate = (BitConverter.ToInt32(new[] { TotalBytes[28], TotalBytes[29], TotalBytes[30], TotalBytes[31] }, 0) * 8);
            duration = (TotalBytes.Length - 8) * 8 / bitrate;
            return duration;
        }

        private void SetDuration(int duration)
        {
            int n = 1;
            int minutes = 0;
            int seconds = 0;
            string min;
            string sec;
            while (n * 60 < duration)
                n = n + 1;
            minutes = n - 1;
            seconds = duration % 60;
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
            //mediaPlayer.Pause();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            onAir = false;
            //mediaPlayer.Stop();
        }

        private void Parcourir_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fenetre = new OpenFileDialog();
            fenetre.Filter = "Fichiers Multimedia|*.wav;*.mp3;*.mp4";
            fenetre.Multiselect = false;
            if (fenetre.ShowDialog() == true)
            {
                Playlistview.Text = fenetre.SafeFileName;
                this.Title = fenetre.SafeFileName;
                if (onAir == true)
                {
                    onAir = false;
                    //mediaPlayer.Stop();
                }
                media.Source = new Uri(fenetre.FileName);
                SetDuration(GetDuration(fenetre.FileName));

                //mediaPlayer = new MP.MediaPlayer();
                //mediaPlayer.Open(fenetre.FileName);
                //onAir = true;
                //this.timer = new System.Timers.Timer();
                //this.timer.Interval = TimeSpan.FromSeconds(1).TotalMilliseconds;
                //this.timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                //this.timer.Start();
                //mediaPlayer.Play();
                //player = new SoundPlayer();
                //player.SoundLocation = fenetre.FileName;
                //player.LoadAsync();
                //player.PlayLooping();
            }
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Volume = (double)Volslider.Value;
	    }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            media.LoadedBehavior = MediaState.Play;
        }

    }
}
