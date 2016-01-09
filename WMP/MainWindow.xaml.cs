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
        private int currentIndex;
        private int ListSize = 0;
        private double prevVol = 0.5;


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
            if (onAir)
            {
                Timer.Text = SetDuration(media.Position.TotalSeconds);
                sliProgress.Value = media.Position.TotalSeconds;
                if (Timer.Text == Timermax.Text && currentIndex + 1 != ListSize)
                {
                    currentIndex += 1;
                    string path = (string)Playlistviewfull.Items[currentIndex];
                    media.Source = new Uri(path);
                    onAir = true;
                    media.Play();
                }
                else if(Timer.Text == Timermax.Text && currentIndex + 1 == ListSize)
                {
                    media.Stop();
                }
            }

        }
        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            try
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = media.NaturalDuration.TimeSpan.TotalSeconds;
                Timermax.Text = SetDuration(media.NaturalDuration.TimeSpan.TotalSeconds);
            }
            catch (Exception) { }


        }

        private void Plalistview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string path = (string)Playlistviewfull.Items[Playlistview.SelectedIndex];
            media.Source = new Uri(path);
            onAir = true;
            media.Play();
        }

        private void Lecture_Click(object sender, RoutedEventArgs e)
        {
            DialogFenetre();
        }
    
        private string SetDuration(double duration)
        {
            int minutes = 0;
            int seconds = 0;
            string min;
            string sec;
            minutes = (int)(duration / 60);
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
            fenetre.Filter = "Fichiers Multimedia|*.wav;*.mp3;*.mp4;*.wmv;*.wma;*.png;*.bmp;*.tiff;*.jpeg;*.gif;*.jpg;*.ico| Images|*.png;*.bmp;*.tiff;*.jpeg;*.gif;*.jpg;*.ico | Videos |*.mp4;*.wmv;*.wma| Musiques | *.wav;*.mp3";
            if (fenetre.ShowDialog() == true)
            {
                Playlistviewfull.Items.Insert(0, fenetre.FileName);
                Playlistview.Items.Insert(0, fenetre.SafeFileName);
                this.Title = fenetre.SafeFileName;
                currentIndex = 0;
                ListSize += 1;
                onAir = true;
                media.Source = new Uri(fenetre.FileName);
                media.LoadedBehavior = MediaState.Manual;
                media.UnloadedBehavior = MediaState.Manual;
                try
                {
                    media.Play();
                } catch (Exception) {}
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
            if (onAir == true && Volslider.Value == 0)
            {
                MuteButton.Visibility = Visibility.Visible;
                NonMuteButton.Visibility = Visibility.Hidden;
            }
            else if (onAir == true)
            {
                MuteButton.Visibility = Visibility.Hidden;
                NonMuteButton.Visibility = Visibility.Visible;
            }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            onAir = true;
            media.LoadedBehavior = MediaState.Play;
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {

            media.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            media.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {


        }

        private void Mute_UnMute(object sender, RoutedEventArgs e)
        {
            if (MuteButton.Visibility == Visibility.Hidden)
            {
                MuteButton.Visibility = Visibility.Visible;
                NonMuteButton.Visibility = Visibility.Hidden;
                prevVol = Volslider.Value;
                Volslider.Value = 0.0;
            }
            else
            {
                MuteButton.Visibility = Visibility.Hidden;
                NonMuteButton.Visibility = Visibility.Visible;
                Volslider.Value = prevVol;
            }
        }
    }
} 
