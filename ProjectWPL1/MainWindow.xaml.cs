using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.Windows.Threading;
using Microsoft.VisualBasic;

namespace ProjectWPL1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick; 
            timer.Start();
        }
        int elapsedSeconds = 0;
        int cookies = 0;
        bool isMouseDown = false;
        double multiplier = 1;
        int AantalCursors = 0;
        int AantalGrandma = 0;
        int AantalFarm = 0;
        int AantalMine = 0;
        int AantalFactory = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            elapsedSeconds++;
            LblTimer.Content = $"Verstreken tijd: {elapsedSeconds}";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void BtnCoockie_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnCoockie_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
        private void ImgCoockie_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMouseDown)
            {
                ImgCoockie.Width = 250;
                ImgCoockie.Height = 250;
            }
            isMouseDown = false;
        }
        private void ImgCoockie_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ImgCoockie.Width = 180;
            ImgCoockie.Height = 180;
            isMouseDown = true;
        }
        private void BtnCoockie_Click(object sender, RoutedEventArgs e)
        {
            cookies++;
            UpdateCookieCount();
        }
        private void UpdateCookieCount()
        {
            LblCookieCount.Content = cookies + "Coockies";
        }
        private void BtnCursor_Click(object sender, RoutedEventArgs e)
        {
            if (cookies >= 15)
            {
                cookies -= 15;
                AantalCursors++;
                UpdateCursorCount();
                UpdateCookieCount();
            }
            else
            {
                MessageBox.Show("Je hebt niet genoeg cookies om dit te kopen!", "Waarschuwing");
            }
        }
        private void UpdateCursorCount()
        {
            LblAantalGekochteUpgradesCursor.Content = $"Aantal cursors: {AantalCursors}";
        }
        
        private void BtnGrandma_Click(object sender, RoutedEventArgs e)
        {
            if (cookies >= 100)
            {
                cookies -= 100;
                AantalGrandma++;
                UpdateGranmaCount();
                UpdateCookieCount();
            }
            else
            {
                MessageBox.Show("Je hebt niet genoeg cookies om dit te kopen!");
            }
        }
        private void UpdateGranmaCount()
        {
            LblAantalGekochteUpgradesGrandma.Content = $"Aantal granma's {AantalGrandma}";
        }

        private void BtnFarm_Click(object sender, RoutedEventArgs e)
        {
            if (cookies >= 1100)
            {
                cookies -= 1100;
                AantalFarm++;
                UpdateFarmCount();
                UpdateCookieCount();
            }
            else
            {
                MessageBox.Show("Je hebt niet genoeg cookies om dit te kopen!");
            }
        }
        private void UpdateFarmCount()
        {
            LblAantalGekochteUpgradesFarm.Content=$"Aantal Farm's {AantalFarm}";
        }
        private void BtnMine_Click(object sender, RoutedEventArgs e)
        {
            if (cookies >= 12000)
            {
                cookies -= 12000;
                AantalMine++;
                UpdateMineCount();
                UpdateCookieCount();
            }
            else
            {
                MessageBox.Show("Je hebt niet genoeg cookies om dit te kopen!");
            }
        }
        private void UpdateMineCount()
        {
            LblAantalGekochteUpgradesMine.Content=$"Aantal Mine's {AantalMine}";
        }
        private void BtnFactory_Click(object sender, RoutedEventArgs e)
        {
            if (cookies >= 130000)
            {
                cookies -= 130000;
                AantalFactory++;
                UpdateFactoryCount();
                UpdateCookieCount();
            }
            else
            {
                MessageBox.Show("Je hebt niet genoeg cookies om dit te kopen!");
            }
        }
        private void UpdateFactoryCount()
        {
            LblAantalGekochteUpgradesFactory.Content = $"Aantal factory's {AantalFactory}";
        }
        private void GemiddeldeCoockies()
        {

            //LblGemCoockies.Content = $"Gemiddelde coockies: {}"; 
        }

        private void LblNaamMiner_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Vul de naam van de miner in.");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("Het veld mag geen spaties bevatten of leeg zijn!");
            string minerNaam = Interaction.InputBox(stringBuilder.ToString(), "Verander de naam", LblNaamMiner.Content.ToString());
            if (minerNaam.Length < 1 || minerNaam.Contains(" "))
            {
                MessageBox.Show("Verandering van de naam is mislukt", "De naam mag geen spaties bevatten of leeg zijn");
            }
            LblNaamMiner.Content = minerNaam;
        }
    }
}
