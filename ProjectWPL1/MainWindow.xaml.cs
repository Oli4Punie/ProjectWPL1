﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
            DispatcherTimer passiefInkomen = new DispatcherTimer();
            passiefInkomen.Interval = TimeSpan.FromMilliseconds(10);
            passiefInkomen.Tick += passiefInkomen_Tick;
            passiefInkomen.Start();
        }
        int elapsedSeconds = 0;
        long cookies = 0;
        int AantalCursors = 0;
        int AantalGrandma = 0;
        int AantalFarm = 0;
        int AantalMine = 0;
        int AantalFactory = 0;
        int AantalBank = 0;
        int AantalTemple = 0;
        int AantalKeerGekochtCursor = 0;
        int AantalKeerGekochtGrandma = 0;
        int AantalKeerGekochtFarm = 0;
        int AantalKeerGekochtMine = 0;
        int AantalKeerGekochtFactory = 0;
        int AantalKeerGekochtBank = 0;
        int AantalKeerGekochtTemple = 0;
        int coockiesPerSeconde = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            elapsedSeconds++;
            LblTimer.Content = $"Verstreken tijd: {elapsedSeconds}";
        }
        private void passiefInkomen_Tick(object sender, EventArgs e)
        {
            double cursorPassiefInkomenPerInvestering = 0.001;
            int totaalCursor = AantalCursors;
            double grandmaPassiefInkomenPerInvestering = 0.01;
            int totaalGrandma = AantalGrandma;
            double farmPassiefInkomenPerInvestring = 0.08;
            int totaalFarm = AantalFarm;
            double minePassiefInkomenPerInvestering = 0.47;
            int totaalMine = AantalMine;
            double factoryPassiefInkomenPerInvestering = 2.60;
            int totaalFactory = AantalFactory;
            double bankPassiefInkomenPerInvestering = 14;
            int totaalBank = AantalBank;
            double templePassiefInkomenPerInvestering = 78;
            int totaalTemple = AantalTemple;
            double totaalPassiefInkomenCursor = cursorPassiefInkomenPerInvestering * totaalCursor;
            double totaalPassiefInkomenGrandma = grandmaPassiefInkomenPerInvestering * totaalGrandma;
            double totaalPassiefInkomenFarm = farmPassiefInkomenPerInvestring * totaalFarm;
            double totaalPassiefInkomenMine = minePassiefInkomenPerInvestering * totaalMine;
            double totaalPassiefInkomenFactory = factoryPassiefInkomenPerInvestering * totaalFactory;
            double totaalPassiefInkomenBank = bankPassiefInkomenPerInvestering * totaalBank;
            double totaalPassiefInkomenTemple = templePassiefInkomenPerInvestering * totaalTemple;
            cookies += (int)(totaalPassiefInkomenCursor + totaalPassiefInkomenGrandma + totaalPassiefInkomenFarm + totaalPassiefInkomenMine + totaalPassiefInkomenFactory + totaalPassiefInkomenBank + totaalPassiefInkomenTemple);
            coockiesPerSeconde = (int)(totaalPassiefInkomenCursor + totaalPassiefInkomenGrandma + totaalPassiefInkomenFarm + totaalPassiefInkomenMine + totaalPassiefInkomenFactory + totaalPassiefInkomenBank + totaalPassiefInkomenTemple);
            UpdateCookieCount();
            GemiddeldeCoockies(coockiesPerSeconde);
        }
        private void BtnCoockie_Click(object sender, RoutedEventArgs e)
        {
            cookies+= 100000;
            UpdateCookieCount();
        }
        private void UpdateCookieCount()
        {
            LblCookieCount.Content = GrooteNummers(cookies) + "Coockies";
        }
        private void BtnCursor_Click(object sender, RoutedEventArgs e)
        {
            double basisPrijs = 15;
            double huidigeKosten = basisPrijs * Math.Pow(1.15, AantalKeerGekochtCursor);
            huidigeKosten = Math.Ceiling(huidigeKosten);
            if (cookies >= huidigeKosten)
            {
                cookies -= (int)huidigeKosten;
                AantalCursors++;
                AantalKeerGekochtCursor++;
                double nieuweKosten = basisPrijs * Math.Pow(1.15, AantalKeerGekochtCursor);
                nieuweKosten = Math.Ceiling(nieuweKosten);
                LblPrijsCursor.Content = $"Prijs: {nieuweKosten} cookies";
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
            double basisPrijs = 100;
            double huidigeKosten = basisPrijs * Math.Pow(1.15, AantalKeerGekochtGrandma);
            huidigeKosten = Math.Ceiling(huidigeKosten);
            if (cookies >= huidigeKosten)
            { 
                cookies -= (int)huidigeKosten;
                AantalGrandma++;
                AantalKeerGekochtGrandma++;
                double nieuweKosten = basisPrijs * Math.Pow(1.15, AantalKeerGekochtGrandma);
                nieuweKosten = Math.Ceiling(nieuweKosten);
                LblPrijsGrandma.Content = $"Prijs: {nieuweKosten} cookies";
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
            double basisPrijs = 1100;
            double huidigeKosten = basisPrijs * Math.Pow(1.15, AantalKeerGekochtFarm);
            huidigeKosten = Math.Ceiling(huidigeKosten);
            if (cookies >= huidigeKosten)
            {
                cookies -= (int)huidigeKosten;
                AantalFarm++;
                AantalKeerGekochtFarm++;
                double nieuweKosten = basisPrijs * Math.Pow(1.15, AantalKeerGekochtFarm);
                nieuweKosten = Math.Ceiling(nieuweKosten);
                LblPrijsFarm.Content = $"Prijs: {nieuweKosten} cookies";
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
            double basisPrijs = 12000;
            double huidigeKosten = basisPrijs * Math.Pow(1.15, AantalKeerGekochtMine);
            huidigeKosten = Math.Ceiling(huidigeKosten);
            if (cookies >= huidigeKosten)
            {
                cookies -= (int)huidigeKosten;
                AantalMine++;
                AantalKeerGekochtMine++;
                double nieuweKosten = basisPrijs * Math.Pow(1.15, AantalKeerGekochtMine);
                nieuweKosten = Math.Ceiling(nieuweKosten);
                LblPrijsMine.Content = $"Prijs: {nieuweKosten} cookies";
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
            double basisPrijs = 130000;
            double huidigeKosten = basisPrijs * Math.Pow(1.15, AantalKeerGekochtFactory);
            huidigeKosten = Math.Ceiling(huidigeKosten);
            if (cookies >= huidigeKosten)
            {
                cookies -= (int)huidigeKosten;
                AantalFactory++;
                AantalKeerGekochtFactory++;
                double nieuweKosten = basisPrijs * Math.Pow(1.15, AantalKeerGekochtFactory);
                nieuweKosten = Math.Ceiling(nieuweKosten);
                LblPrijsFactory.Content = $"Prijs: {nieuweKosten} cookies";
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
        private void BtnBank_Click(object sender, RoutedEventArgs e)
        {
            double basisPrijs = 1400000;
            double huidigeKosten = basisPrijs * Math.Pow(1.15, AantalKeerGekochtBank);
            huidigeKosten = Math.Ceiling(huidigeKosten);
            if (cookies >= huidigeKosten)
            {
                cookies -= (int)huidigeKosten;
                AantalBank++;
                AantalKeerGekochtBank++;
                double nieuweKosten = basisPrijs * Math.Pow(1.15, AantalKeerGekochtBank);
                nieuweKosten = Math.Ceiling(nieuweKosten);
                LblPrijsBank.Content = $"Prijs: {nieuweKosten} cookies";
                UpdateBankCount();
                UpdateCookieCount();
            }
            else
            {
                MessageBox.Show("Je hebt niet genoeg cookies om dit te kopen!");
            }
        }
        private void UpdateBankCount()
        {
            LblAantalGekochteUpgradesBank.Content = $"Aantal banken: {AantalBank}";
        }
        private void BtnTemple_Click(object sender, RoutedEventArgs e)
        {
            double basisPrijs = 20000000;
            double huidigeKosten = basisPrijs * Math.Pow(1.15, AantalKeerGekochtTemple);
            huidigeKosten = Math.Ceiling(huidigeKosten);
            if (cookies >= huidigeKosten)
            {
                cookies -= (int)huidigeKosten;
                AantalTemple++;
                AantalKeerGekochtTemple++;
                double nieuweKosten = basisPrijs * Math.Pow(1.15, AantalKeerGekochtTemple);
                nieuweKosten = Math.Ceiling(nieuweKosten);
                LblPrijsTemple.Content = $"Prijs: {nieuweKosten} cookies";
                UpdateTempleCount();
                UpdateCookieCount();
            }
            else
            {
                MessageBox.Show("Je hebt niet genoeg cookies om dit te kopen!");
            }
        }
        private void UpdateTempleCount()
        {
            LblAantalGekochteUpgradesTemple.Content = $"Aantal temple's {AantalTemple}";
        }
        private void GemiddeldeCoockies(double cps)
        {
            LblGemCoockies.Content = $"{cps:F1} Coockies per seconde";
        }
        private string GrooteNummers(long nummmer)
        { 
            const double Miljard = 1_000_000_000;
            const double Billion = 1_000_000_000_000;
            const double Biljart = 1_000_000_000_000_000;
            const double Trilion = 1_000_000_000_000_000_000;
            if (nummmer >= Trilion)
            {
                return $"{nummmer / Trilion:F3} Triljoen";
            }
            else if (nummmer >= Biljart)
            {
                return $"{nummmer / Biljart:F3} Biljart";
            }
            else if(nummmer >= Billion)
            {
                return $"{nummmer / Billion:F3} Biljoen";
            }
            else if (nummmer >= Miljard)
            {
                return $"{nummmer / Miljard} Miljard";
            }
            else if (nummmer >= 1_000_000)
            {
                return $"{nummmer / 1_000_000:F3} Miljoen";
            }
            else
            {
                return $"{nummmer:N0}";
            }
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
