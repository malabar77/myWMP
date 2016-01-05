using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;

namespace WMP
{

    public partial class MainWindow : Window
    {
        private bool onAir = false;
        private bool userIsDraggingSlider = false;
        private string timemaxmedia = "00:00";


        public MainWindow()
        {
            InitializeComponent();
            media.MediaOpened += new RoutedEventHandler(media_MediaOpened);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Timer.Text = SetDuration(media.Position.TotalSeconds);
            sliProgress.Value = media.Position.TotalSeconds;
        }

        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            sliProgress.Minimum = 0;
            sliProgress.Maximum = media.NaturalDuration.TimeSpan.TotalSeconds;
            Timermax.Text = SetDuration(media.NaturalDuration.TimeSpan.TotalSeconds);


        }

        private void Plalistview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string path = (string)Playlistviewfull.Items[Playlistview.SelectedIndex];
            media.Source = new Uri(path);
            media.Play();
        }

        private void Lecture_Click(object sender, RoutedEventArgs e)
        {
            DialogFenetre();
        }
    
        private string SetDuration(double duration)
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
            return (min + ":" + sec);
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
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
            fenetre.Filter = "Fichiers Multimedia|*.wav;*.mp3;*.mp4;*.wmv";
            if (fenetre.ShowDialog() == true)
            {
                Playlistviewfull.Items.Add(fenetre.FileName);
                Playlistview.Items.Add(fenetre.SafeFileName);
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
            try {
                media.Volume = (double)Volslider.Value;
            } catch (NullReferenceException) {}
            }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            media.LoadedBehavior = MediaState.Play;
        }

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
            //Timer.Text = media.Position.TotalSeconds.ToString();
        }

    }
}
