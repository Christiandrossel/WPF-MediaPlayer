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

namespace WPFPlayer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WPFWrapper wpfWrapper;


        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            
            dispatcherTimer.Tick += new EventHandler(onTimerTick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            wpfWrapper = new WPFWrapper(this, wpfPlayer);
        }

        private void Open_FileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                wpfWrapper.Open(ofd.FileName);
                this.Title = ofd.SafeFileName;
                sliderTime.Maximum = wpfWrapper.GetDuration();
                sliderVolume.Value = 5;
                sliderBalance.Value = 5;
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            wpfWrapper.Play();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            wpfWrapper.Pause();
        }

        private void FullscreenButton_Click(object sender, RoutedEventArgs e)
        {
            
            wpfWrapper.SetFullscreen(true);
            wpfPlayer.Margin = new Thickness(0, 0, 0, 0);
            myWindow.Background = new SolidColorBrush(Colors.Black);
            setControlElementsVisible(true);  

        }

        void setControlElementsVisible(bool visible)
        {
            if (visible && wpfPlayer.Source != null)
            {
                Open_FileButton.Visibility = Visibility.Hidden;
                Open_FileButton.Visibility = Visibility.Collapsed;

                PlayButton.Visibility = Visibility.Hidden;
                PlayButton.Visibility = Visibility.Collapsed;

                PauseButton.Visibility = Visibility.Hidden;
                PauseButton.Visibility = Visibility.Collapsed;

                FullscreenButton.Visibility = Visibility.Hidden;
                FullscreenButton.Visibility = Visibility.Collapsed;

                sliderBalance.Visibility = Visibility.Hidden;
                sliderBalance.Visibility = Visibility.Collapsed;
                
                sliderTime.Visibility = Visibility.Hidden;
                sliderTime.Visibility = Visibility.Collapsed;

                sliderVolume.Visibility = Visibility.Hidden;
                sliderVolume.Visibility = Visibility.Collapsed;

                lblStatus.Visibility = Visibility.Hidden;
                lblStatus.Visibility = Visibility.Collapsed;
            }
            else
            {
                Open_FileButton.Visibility = Visibility.Visible;
                PlayButton.Visibility = Visibility.Visible;
                PauseButton.Visibility = Visibility.Visible;
                FullscreenButton.Visibility = Visibility.Visible;
                sliderBalance.Visibility = Visibility.Visible;
                sliderTime.Visibility = Visibility.Visible;
                sliderVolume.Visibility = Visibility.Visible;
                lblStatus.Visibility = Visibility.Visible;
                myWindow.Background = new SolidColorBrush(Colors.White);
            }
        }

        void onTimerTick(object sender, EventArgs e)
        {
            sliderTime.Maximum = wpfWrapper.GetDuration();
            if (Mouse.LeftButton != MouseButtonState.Pressed)
            {
                // only update, if user is currently not dragging the slider
                double newValue = wpfWrapper.GetPosition();
                if (newValue != -1)
                {
                    sliderTime.Value = newValue;
                    lblStatus.Content = String.Format("{0:00}:{1:00} / {2:00}:{3:00}",
                    (int)newValue / 60, (int)newValue % 60,
                    (int)wpfWrapper.GetDuration() / 60,
                    (int)wpfWrapper.GetDuration() % 60);
                }
                else
                    lblStatus.Content = "No file selected...";
            }
        }



        private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            wpfWrapper.SetVolume(sliderVolume.Value*10);
            Console.WriteLine("SliderVolume: " + sliderVolume.Value*10);
        }

        private void sliderBalance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            wpfWrapper.SetBalance(sliderBalance.Value*10);
            Console.WriteLine("SliderBalance: " + sliderBalance.Value*10);
        }

        private void sliderTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            wpfWrapper.SetPosition(Convert.ToInt32(sliderTime.Value));
        }

        private void MouseLeftButton_Clicked(object sender, MouseButtonEventArgs e)
        {
            wpfWrapper.SetFullscreen(false);
            setControlElementsVisible(false);
        }
    }
}
