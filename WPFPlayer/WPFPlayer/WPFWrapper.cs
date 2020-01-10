using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPlayer;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;



/// <summary>
/// TODOS       FULSCREEN funktioniert noch nicht richtig--> Button bleiben im fullscreen an selber stelle
///                 -->Lösung vllt mit ein GRID https://www.wpf-tutorial.com/de/29/panels/the-grid-rows-columns/
///                     https://www.youtube.com/watch?v=qiEHTgqo4yE
///                     
///             Video Stockt beim abspielen seit dispatcher.Timer in Konstrukter der MainWindow.xaml.cs
/// </summary>



class WPFWrapper
{
    System.Windows.Controls.MediaElement player;
    MainWindow window;

    public WPFWrapper(MainWindow windowHandle, MediaElement player)
    {
        this.player = player;
        window = windowHandle;
    }

    /// Opens a Media File.
    /// <param name="path">The path to the Media file as a URI.</param>
    /// <param name="autoRun">A boolean to select the automatic start of the Video, once loaded.</param>
    public void Open(Uri path, bool autoRun)
    {
        player.Source = path;
        if (autoRun)
            Play();
    }
    /// Opens a Media File and starts playback.
    /// <param name="path">The path to the Media file as a URI.</param>
    public void Open(Uri path)
    {
        Open(path, true);
    }

    /// <summary>
    /// Opens a Media File.
    /// </summary>
    /// <param name="path">The path to the Media file as a String.</param>
    /// <param name="autoRun">A boolean to select the automatic start of the Video.</param>
    public void Open(string path, bool autoRun)
    {
        Open(new Uri(path, UriKind.RelativeOrAbsolute), autoRun);
    }
    /// <summary>
    /// Opens a Media File and starts playback.
    /// </summary>
    /// <param name="path">The path to the Media file as a String.</param>
    public void Open(string path)
    {
        Open(new Uri(path, UriKind.RelativeOrAbsolute), true);
    }

    /// Starts the Audio/Video.
    public void Play()
    {
        if (player.Source != null) player.Play();
    }
    /// Pauses the Audio/Video.
    public void Pause()
    {
        if (player.Source != null) player.Pause();
    }
    /// Stops the Audio/Video.
    public void Stop()
    {
        if (player.Source != null) player.Stop();
    }

    /// Sets the Fullscreen mode. (Video only)
    /// <param name="fullscreen">Boolean Value for the Fullscreen.</param>
    public void SetFullscreen(bool fullscreen)
    {
        if (player.Source != null)
        {
            if (fullscreen)
            {
                window.WindowStyle = WindowStyle.None;
                window.WindowState = WindowState.Maximized;
                player.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                player.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            }
            else
            {
                window.WindowStyle = WindowStyle.SingleBorderWindow;
                window.WindowState = WindowState.Normal;
                player.Width = 792;
                player.Height = 359;
            }
        }
    }

    /// Returns the current Position in seconds.
    public double GetPosition()
    {
        if (player.Source != null &&
        player.NaturalDuration.HasTimeSpan)
            return player.Position.TotalSeconds;
        return -1;
    }
    /// Sets the current Position in seconds.
    public void SetPosition(int position)
    {
        if (player.Source != null)
            player.Position = new TimeSpan(0, 0, position);
    }

    /// Returns the current Volume.
    /// 0 is silent, 100 is full Volume.
    public double GetVolume()
    {
        return player.Volume * 100;
    }
    /// Sets the Volume of the Audio.
    /// 0 is silent, 100 is full Volume.
    public void SetVolume(double volume)
    {
        player.Volume = volume / 100;
    }
    /// Returns the current Balance of the Audio.
    /// -100 is left only, +100 is right only.
    public double GetBalance()
    {
        return player.Balance * 100;
    }
    /// Sets the Balance of the Audio.
    /// -100 is left only, +100 is right only.
    public void SetBalance(double balance)
    {
        player.Balance = balance / 100;
    }

    public double GetDuration()
    { //return this.Duration;
        if (player.NaturalDuration.HasTimeSpan)
            return player.NaturalDuration.TimeSpan.TotalSeconds;
        else
            return 0;
    }
}

