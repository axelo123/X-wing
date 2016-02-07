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
using System.Windows.Media.Animation;
using AjoutCarte;
using CarteXWing;
using X_wing; 

namespace Login
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class Wrapper : Window
    {
        private AjouterLaCarte ajout_la_carte;
        private TabPanelXWing tab;



        public Wrapper()
        {
            X_wing.Core.App.ConnecterBD();
            InitializeComponent();
            Random r = new Random();
            int numberMattePainting = r.Next(1,3);

            ajout_la_carte = new AjouterLaCarte();
            tab = new TabPanelXWing();

            string chemin = string.Format("Ressources/MattePainting{0}", numberMattePainting);
            bgLoginImage.Source=(ImageSource)new ImageSourceConverter().ConvertFromString(chemin+".jpg");
            videoBackground.Source = new Uri(chemin+".mp4", UriKind.Relative);
            mainMenuMP3.Source=new Uri("Ressources/MainMenu" + numberMattePainting+ ".mp3", UriKind.Relative);

            
            completeGrid.Visibility = Visibility.Collapsed;


            EventHandler myEvent = new EventHandler(GoToMainMenu);
            login.OnConnection += myEvent;
            login.setEvent(myEvent);
            menu.Set_func_menu(Menu_CVP);
            menu.Set_func_disconnect(Disconnect);
            menu.Set_func_carte(Menu_Carte);
        }

        private void Disconnect(object sender, EventArgs e)
        {
            Random r = new Random();
            int numberMattePainting = r.Next(1, 3);

            string chemin = string.Format("Ressources/MattePainting{0}", numberMattePainting);
            bgLoginImage.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(chemin + ".jpg");
            videoBackground.Source = new Uri(chemin + ".mp4", UriKind.Relative);
            mainMenuMP3.Source = new Uri("Ressources/MainMenu" + numberMattePainting + ".mp3", UriKind.Relative);
        }


        private void GoToMainMenu(object sender, EventArgs e)
        {

            bgLoginImage.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation(bgLoginImage.Opacity, 0, TimeSpan.FromSeconds(1)));
            login.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation(login.Opacity, 0, TimeSpan.FromSeconds(1)));
            login.Visibility = Visibility.Collapsed;
            completeGrid.Visibility = Visibility.Visible;

            loginMP3.BeginAnimation(MediaElement.VolumeProperty, new DoubleAnimation(loginMP3.Volume, 0, TimeSpan.FromSeconds(1)));
            mainMenuMP3.BeginAnimation(MediaElement.VolumeProperty, new DoubleAnimation(mainMenuMP3.Volume, 1, TimeSpan.FromSeconds(1)));
            mainMenuMP3.Play();
            videoBackground.Visibility = Visibility.Visible;

        }

        private void videoBackground_MediaEnded(object sender, RoutedEventArgs e)
        {
            //play video again
            videoBackground.Position = TimeSpan.FromSeconds(0);
        }

        public void Menu_CVP(object sender, RoutedEventArgs e)
        {
            contentGrid.Children.Clear();
            contentGrid.Children.Add(ajout_la_carte);
        }
        public void Menu_Carte(object sender,RoutedEventArgs e)
        {
            contentGrid.Children.Clear();
            contentGrid.Children.Add(tab);
        }
    }
}
