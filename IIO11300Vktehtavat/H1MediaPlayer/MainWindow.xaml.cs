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
using Microsoft.Win32;

namespace H1MediaPlayer
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool IsPlaying = false;
        public MainWindow()
        {
            InitializeComponent();
            IniMyStuff();
        }

        private void IniMyStuff() //Kootaan tänne kaikki alustukset
        {
            txtFileName.Text = "d:\\H8897\\Coffeemaker.mp4";                  
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if ((txtFileName.Text.Length > 0) && System.IO.File.Exists(txtFileName.Text))
                {
                    media.Source = new Uri(txtFileName.Text);
                    media.Play();
                    IsPlaying = true;
                    //napplat käyttöön
                    SetMyButtons();              
                }
                else
                {
                    MessageBox.Show("tiedostoa" + txtFileName.Text + "ei löydy");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }

        private void btnpause_Click(object sender, RoutedEventArgs e)
        {
            if (IsPlaying)
            {
                media.Pause();
                IsPlaying = false;
                btnpause.Content = "Pause";

            }
            else
            {
                media.Play();
                IsPlaying = true;
                btnpause.Content = "Continue";
            }

        }

        private void btnstop_Click(object sender, RoutedEventArgs e)
        {
            media.Stop();
            IsPlaying = false;
            SetMyButtons();

    }

    private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            Close();//sulkee ikkunan ja sovelluksen
            Application.Current.Shutdown(); //sulkee  sovelluksen
        }
        private void SetMyButtons()
        {
            btnpause.IsEnabled =IsPlaying;
            btnstop.IsEnabled =IsPlaying;
            btnPlay.IsEnabled = !IsPlaying;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {

            //haetaan käyttäjän vakiodialogilla valitsema tiedosto textboxiin
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "d:\\H8897";
            ofd.Filter = "Video-tiedostot mp4|*.mp4|*All files|*.*";
           Nullable<bool> result = ofd.ShowDialog();
            if (result==true)
            {
                txtFileName.Text = ofd.FileName;
            }
        }
    }
}
