using System;
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
        ///--------------------------------
        /// Description:
        /// 1-PROa C# Project Coockie Clicker WPL1
        ///--------------------------------

        ///<summary>
        ///Opstarten van de aplicatie: Timers aanmaken en laten starten.
        ///</summary>
        public MainWindow()
        {
            InitializeComponent();
            InitializeQuests();
            InitializeStatistieken();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick; 
            timer.Start();
            DispatcherTimer passiefInkomen = new DispatcherTimer();
            passiefInkomen.Interval = TimeSpan.FromSeconds(1);
            passiefInkomen.Tick += passiefInkomen_Tick;
            passiefInkomen.Start();
            DispatcherTimer goldenCookie = new DispatcherTimer();
            goldenCookie.Interval = TimeSpan.FromMinutes(1);
            goldenCookie.Tick += goldenCookie_Tick;
            goldenCookie.Start();
        }

        int elapsedSeconds = 0; //Aantal seconden
        long cookies = 0; //Aantal cookies
        int AantalCursors = 0;//Aantal cursors
        int AantalGrandma = 0;//Aantal grandma
        int AantalFarm = 0;//Aantal farm
        int AantalMine = 0;//Aantal mine
        int AantalFactory = 0;//Aantal factory
        int AantalBank = 0;//Aantal bank
        int AantalTemple = 0;//Aantal temple
        int AantalKeerGekochtCursor = 0;//Aantal gekochte cursors
        int AantalKeerGekochtGrandma = 0;//Aantal gekochte grandma's
        int AantalKeerGekochtFarm = 0;//Aantal gekochte farm's
        int AantalKeerGekochtMine = 0;//Aantal gekochte mine's
        int AantalKeerGekochtFactory = 0;//Aantal gekochte factory's
        int AantalKeerGekochtBank = 0;//Aantal gekochte banken
        int AantalKeerGekochtTemple = 0;//Aantal gekochte temple's
        int coockiesPerSeconde = 0;//Aantal cookies per seconden
        Random random = new Random();//Random generator
        int bonusMultiplier = 1;//Bonusmultiplier
        long bonusKosten = 100;//Aantal bonus kosten
        int basisInvestmentKosten = 1000;//Basis investerings kosten
        long  huidigeCookies = 0;//huidige cookies
        List<string> quests;//List met de quests in
        List<string> completedquests;//List met de completed quests in
        List<string> statistiek;//List met statistieken van de game
        int totaalAantalCookies = 0;//totaal aantal koekjes van heel het spel
        int clicksManueel =0;//aantal keer dat er manueel is geklikt
        int clicksGoudenCookies = 0;//Aantal keer dat er op het gouden koekje is geklikt
        int aantalQuestsGehaald = 0;//Aantal quests dat er gehaald zijn
        /// <summary>
        /// Optellen van seconden in de timer en het laten weergeven in een label.
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            elapsedSeconds++;
            LblTimer.Content = $"Aantal seconden in game: {elapsedSeconds}";
        }
        /// <summary>
        /// Het bereken van het passief inkomen en de cookies per seconden.
        /// </summary>
        private void passiefInkomen_Tick(object sender, EventArgs e)
        {
            double cursorPassiefInkomenPerInvestering = 0.1;
            int totaalCursor = AantalCursors;
            double grandmaPassiefInkomenPerInvestering = 1;
            int totaalGrandma = AantalGrandma;
            double farmPassiefInkomenPerInvestring = 8;
            int totaalFarm = AantalFarm;
            double minePassiefInkomenPerInvestering = 47;
            int totaalMine = AantalMine;
            double factoryPassiefInkomenPerInvestering = 260;
            int totaalFactory = AantalFactory;
            double bankPassiefInkomenPerInvestering = 1400;
            int totaalBank = AantalBank;
            double templePassiefInkomenPerInvestering = 7800;
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
            UpdateQuest();
        }
        /// <summary>
        /// De spawn kans van de golden cookie.
        /// </summary>
        private void goldenCookie_Tick(object sender, EventArgs e)
        {
            double randomValue = random.NextDouble();
            if (randomValue < 0.3)
            {
                ShowGoldenCookie();
            }
        }
        /// <summary>
        /// Als er op de button gedrukt wordt komt er 1 koekje bij.
        /// </summary>
        private void BtnCoockie_Click(object sender, RoutedEventArgs e)
        {
            cookies++;
            totaalAantalCookies++;
            clicksManueel++;
            UpdateCookieCount();
        }
        /// <summary>
        /// Het weergeven van de cookiecounter in een label.
        /// </summary>
        private void UpdateCookieCount()
        {
            LblCookieCount.Content = GrooteNummers(cookies) + "Coockies";
        }
        /// <summary>
        /// Een upgrade class om de image weer te geven in een listbox.
        /// </summary>
        private class Upgrade
        {
            public string Name { get; set; }
            public string ImagePath { get; set; }
        }
        /// <summary>
        /// Berekent de basis prijs van de cursor en berekent de nieuwe prijs eens er een cursor is gekocht en geeft een image in de listbox.
        /// </summary>
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
                Upgrade  cursorUpgrade = new Upgrade
                {
                    Name="Cursor",
                    ImagePath="Images/Cursor.jpg"
                };
                LstCursor.Items.Add(cursorUpgrade);
                UpdateCursorCount();
                UpdateCookieCount();
                UpdateQuest();
            }
            else
            {
                MessageBox.Show("Je hebt niet genoeg cookies om dit te kopen!", "Waarschuwing");
            }
            
        }
        /// <summary>
        /// Deze functie laat in een label zien hoeveel aantal cursors er al zijn gekocht.
        /// </summary>
        private void UpdateCursorCount()
        {
            LblAantalGekochteUpgradesCursor.Content = $"Aantal cursors: {AantalCursors}";
        }
        /// <summary>
        /// Berekent de basis prijs van de grandma en berekent de nieuwe prijs eens er een grandma is gekocht en geeft een image in de listbox.
        /// </summary>
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
                Upgrade grandmaUpgrade = new Upgrade
                {
                    Name = "Grandma",
                    ImagePath = "Images/Grandma.png"
                };
                LstGrandma.Items.Add(grandmaUpgrade);
                UpdateGranmaCount();
                UpdateCookieCount();
                UpdateQuest();
            }
            else
            {
                MessageBox.Show("Je hebt niet genoeg cookies om dit te kopen!");
            }
        }
        /// <summary>
        /// Deze functie laat in een label zien hoeveel aantal grandma's er al zijn gekocht.
        /// </summary>
        private void UpdateGranmaCount()
        {
            LblAantalGekochteUpgradesGrandma.Content = $"Aantal granma's {AantalGrandma}";
        }
        /// <summary>
        /// Berekent de basis prijs van de farm en berekent de nieuwe prijs eens er een farm is gekocht en geeft een image in de listbox.
        /// </summary>
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
                Upgrade farmUpgrade = new Upgrade
                {
                    Name = "Farm",
                    ImagePath = "Images/Farm.jpg"
                };
                LstFarm.Items.Add(farmUpgrade);
                UpdateFarmCount();
                UpdateCookieCount(); 
                UpdateQuest();

            }
            else
            {
                MessageBox.Show("Je hebt niet genoeg cookies om dit te kopen!");
            }
        }
        /// <summary>
        /// Deze functie laat in een label zien hoeveel aantal farm's er al zijn gekocht.
        /// </summary>
        private void UpdateFarmCount()
        {
            LblAantalGekochteUpgradesFarm.Content=$"Aantal Farm's {AantalFarm}";
        }
        /// <summary>
        /// Berekent de basis prijs van de mine en berekent de nieuwe prijs eens er een mine is gekocht en geeft een image in de listbox.
        /// </summary>
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
                Upgrade mineUpgrade = new Upgrade
                {
                    Name = "Mine",
                    ImagePath = "Images/Mine.jpg"
                };
                LstMine.Items.Add(mineUpgrade);
                UpdateMineCount();
                UpdateCookieCount();
                UpdateQuest();
            }
            else
            {
                MessageBox.Show("Je hebt niet genoeg cookies om dit te kopen!");
            }
        }
        /// <summary>
        /// Deze functie laat in een label zien hoeveel aantal mine's er al zijn gekocht.
        /// </summary>
        private void UpdateMineCount()
        {
            LblAantalGekochteUpgradesMine.Content=$"Aantal Mine's {AantalMine}";
        }
        /// <summary>
        /// Berekent de basis prijs van de factory en berekent de nieuwe prijs eens er een factory is gekocht en geeft een image in de listbox.
        /// </summary>
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
                Upgrade factoryUpgrade = new Upgrade
                {
                    Name = "Factory",
                    ImagePath = "Images/Factory.png"
                };
                LstFactory.Items.Add(factoryUpgrade);
                UpdateFactoryCount();
                UpdateCookieCount();
                UpdateQuest();
            }
            else
            {
                MessageBox.Show("Je hebt niet genoeg cookies om dit te kopen!");
            }
        }
        /// <summary>
        /// Deze functie laat in een label zien hoeveel aantal factory's er al zijn gekocht.
        /// </summary>
        private void UpdateFactoryCount()
        {
            LblAantalGekochteUpgradesFactory.Content = $"Aantal factory's {AantalFactory}";
        }
        /// <summary>
        /// Berekent de basis prijs van de bank en berekent de nieuwe prijs eens er een bank is gekocht en geeft een image in de listbox.
        /// </summary>
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
                Upgrade bankUpgrade = new Upgrade
                {
                    Name = "Bank",
                    ImagePath = "Images/Bank.jpg"
                };
                LstBank.Items.Add(bankUpgrade);
                UpdateBankCount();
                UpdateCookieCount();
                UpdateQuest();
            }
            else
            {
                MessageBox.Show("Je hebt niet genoeg cookies om dit te kopen!");
            }
        }
        /// <summary>
        /// Deze functie laat in een label zien hoeveel aantal banken er al zijn gekocht.
        /// </summary>
        private void UpdateBankCount()
        {
            LblAantalGekochteUpgradesBank.Content = $"Aantal banken: {AantalBank}";
        }
        /// <summary>
        /// Berekent de basis prijs van de temple en berekent de nieuwe prijs eens er een temple is gekocht en geeft een image in de listbox.
        /// </summary>
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
                Upgrade templeUpgrade = new Upgrade
                {
                    Name = "Temple",
                    ImagePath = "Images/Temple.jpg"
                };
                LstTemple.Items.Add(templeUpgrade);
                UpdateTempleCount();
                UpdateCookieCount();
                UpdateQuest();
            }
            else
            {
                MessageBox.Show("Je hebt niet genoeg cookies om dit te kopen!");
            }
        }
        /// <summary>
        /// Deze functie laat in een label zien hoeveel aantal temple's er al zijn gekocht.
        /// </summary>
        private void UpdateTempleCount()
        {
            LblAantalGekochteUpgradesTemple.Content = $"Aantal temple's {AantalTemple}";
        }
        /// <summary>
        /// Deze functie laat in een label zien het gemiddelde aantal coockies per seconden.
        /// </summary>
        /// <param name="cps">Dit staat voor de index cookies per seconden.</param>
        private void GemiddeldeCoockies(double cps)
        {
            LblGemCoockies.Content = $"{cps:F1} Coockies per seconde";
        }
        /// <summary>
        /// Deze functie kijkt hoeveel cookies ik heb en of het verkleint kan worden met een woord.
        /// </summary>
        /// <param name="nummmer">staat voor het nummer van aantal cookies.</param>
        /// <returns>Het nummer met de juisste achtervoeging als het een groot getal is.</returns>
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
       /// <summary>
       /// Deze functie laat je van naam veranden in een popup scherm.
       /// </summary>
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
        /// <summary>
        /// Deze functie laat de image van de golden cookie zien in een vakje.
        /// </summary>
        private void ShowGoldenCookie()
        {
            double rondomValue = random.NextDouble();
            ImgGoldenCookie.Source = new BitmapImage(new Uri("Images/GoldenCookie.png", UriKind.Relative));
            BtnGoldenCookie.Click += BtnGoldenCookie_Click; 
            if(rondomValue > 0.3) 
            {
                BtnGoldenCookie.Visibility = Visibility.Visible;
                BtnGoldenCookie.Click += BtnGoldenCookie_Click;
            }
        }
        /// <summary>
        /// Deze functie is voor als het gouden koekje verschijnt en er op geklikt wordt dat er dan bonussen gegeven worden aan de speler.
        /// </summary>
        private void BtnGoldenCookie_Click(object sender, RoutedEventArgs e)
        {
            clicksGoudenCookies++;
            int cookieProductiePerMinuut = 10;
            int cookieBijvoegen = 15 * cookieProductiePerMinuut;
            AddCoookiesAanSpeler(cookieBijvoegen);
            BtnGoldenCookie.Visibility = Visibility.Hidden;
            BtnGoldenCookie.Click -= BtnGoldenCookie_Click;
        }
       /// <summary>
       /// Deze functie laat de bonus van de golden koekje zien in de label waar je koekjes in komen.
       /// </summary>
       /// <param name="cookieBijvoegen">Zijn de koekjes die worden bijgevoegd.</param>
        private void AddCoookiesAanSpeler(int cookieBijvoegen)
        {
            if(LblCookieCount !=null && LblCookieCount.Content is string content)
            {
                if(int.TryParse(content, out int huidigecookies))
                {
                    int nieuwecookies = huidigecookies + cookieBijvoegen;
                    LblCookieCount.Content = nieuwecookies.ToString();
                }
            }
        }
        /// <summary>
        /// Deze functie geeft de  store als er op de button wordt geklikt.
        /// </summary>
        private void BtnBonusStore_Click(object sender, RoutedEventArgs e)
        {
            KrijgBonus();
            UpdateBonusKnop(null);
        }
        /// <summary>
        /// Deze functie geeft De knop om de bonus store te openen zichtbaar en geeft de bonussen weer.
        /// </summary>
        /// <param name="investeringsKnop">Is de index voor de investeringen.</param>
        private void UpdateBonusKnop(Button investeringsKnop)
        {
            BtnBonusStore.Visibility = CanAffordBonus() ? Visibility.Visible : Visibility.Hidden;
            BtnBonusStore.IsEnabled = CanAffordBonus();
            BtnBonusStore.Content = $"Verdubbel Inkomsten (Cost: {bonusKosten}, x{bonusMultiplier}";
            if(investeringsKnop != null)
            {
                BtnBonusStore.Margin = investeringsKnop.Margin;
            }
        }
        /// <summary>
        /// Deze functie geeft de huidige koekjes weer en roept de variable krijghuidigecookies.
        /// </summary>
        /// <returns>De huidige koekjes als ze meer of gelijk aan de bonuskosten zijn.</returns>
        private bool CanAffordBonus()
        {
            long huidigeCookies = KrijgHuidigeCookies();
            return huidigeCookies >= bonusKosten;
        }
        /// <summary>
        /// Is een return functie voor de huidige koekjes.
        /// </summary>
        /// <returns>De huidige koekjes.</returns>
        private long KrijgHuidigeCookies()
        {
            return huidigeCookies;
        }
        /// <summary>
        /// Deze functie geeft weer de bonus die je krijgt na het kopen van een bonus.
        /// </summary>
        private void KrijgBonus()
        {
            long huidigeCookies = KrijgHuidigeCookies();
            huidigeCookies = Math.Max(0, huidigeCookies-bonusKosten);
            bonusMultiplier *= 2;
            bonusKosten = BerekeningBonusKosten(bonusMultiplier, basisInvestmentKosten);
        }
        /// <summary>
        /// Deze functie is de berekening van de Bonuskosten als ze meer dan 1 keer worden gekocht.
        /// </summary>
        /// <param name="NummerVanApps"> Is de index voor het aantal upgrades</param>
        /// <param name="basisInvestmentKosten">Is de index van de basis investering kosten.</param>
        /// <returns></returns>
        private long BerekeningBonusKosten(int NummerVanApps, long basisInvestmentKosten)
        {
            long multiplier = 0;
            switch(NummerVanApps)
            {
                case 0:
                    multiplier = 100;
                    break;
                case 1:
                    multiplier = 500;
                    break;
                case 2:
                    multiplier = 5000;
                    break;
                case 3:
                    multiplier = 50000;
                    break;
                default:
                    multiplier= (long)Math.Pow(10, NummerVanApps -3) * 500000;
                    break;
            }
            long bonusKosten = basisInvestmentKosten * multiplier;
            return bonusKosten;
        }
        /// <summary>
        /// Deze functie kijkt naar de quest en als je een compleet hebt laat die een messagebox zien.
        /// </summary>
        private void CheckQuests()
        {
            foreach(string quest in quests)
            {
                if(!completedquests.Contains(quest) && HeeftSpelerQuestBehaald(quest))
                {
                    completedquests.Add(quest);
                    aantalQuestsGehaald++;
                    MessageBox.Show($"Gefeliciteerd! Je hebt de quest behaald:\n{quest}", "Quest Behaald");
                }
            }
        }
        /// <summary>
        /// Deze functie Kijkt of een speler de quest heeft gehaald of niet.
        /// </summary>
        /// <param name="quest">Deze index kijkt naar quest die er zijn.</param>
        /// <returns></returns>
        private bool HeeftSpelerQuestBehaald(string quest)
        {

            if(quest == "Behaal 1 cookie"&& cookies >=1)
            {
                return true;
            }
            else if (quest == "Behaal 50 cookies" && cookies >= 50)
            {
                return true;
            }
            else if (quest== "Behaal 100 cookies" && cookies >= 100)
            {
                return true;
            }
            else if (quest == "Behaal 1000 cookies" && cookies >= 1000)
            {
                return true;
            }
            else if (quest == "Behaal 5000 cookies" && cookies >= 5000)
            {
                return true;
            }
            else if (quest == "Behaal 10000 cookies" && cookies >= 10000)
            {
                return true;
            }
            else if (quest == "Behaal 50000 cookies" && cookies >= 50000)
            {
                return true;
            }
            else if (quest == "Behaal 100000 cookies" && cookies >= 100000)
            {
                return true;
            }
            else if (quest == "Behaal 500000 cookies" && cookies >= 500000)
            {
                return true;
            }
            else if (quest == "Behaal 1000000 cookies" && cookies >= 1000000)
            {
                return true;
            }
            else if (quest == "Behaal 5000000 cookies" && cookies >= 5000000)
            {
                return true;
            }
            else if (quest == "Behaal 1000000000 cookies" && cookies >= 1000000000)
            {
                return true;
            }
            else if (quest == "Behaal 5000000000 cookies" && cookies >= 5000000000)
            {
                return true;
            }
            else if(quest =="Koop 5 cursors"&& AantalCursors>=5)
            {
                return true;
            }
            else if (quest == "Koop 5 grandma's" && AantalGrandma >= 5)
            {
                return true;
            }
            else if (quest == "Koop 5 farm's" && AantalFarm >= 5)
            {
                return true;
            }
            else if (quest == "Koop 5 Mine's" && AantalMine >= 5)
            {
                return true;
            }
            else if (quest == "Koop 5 factory's" && AantalFactory >= 5)
            {
                return true;
            }
            else if (quest == "Koop 5 bank's" && AantalBank >= 5)
            {
                return true;
            }
            else if (quest == "Koop 5 temple's" && AantalTemple >= 5)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Deze functie checht de update de quest list als er een is voltooid.
        /// </summary>
        private void UpdateQuest()
        {
            CheckQuests();
        }
        /// <summary>
        /// Bij deze functie als ze op de button duwen dan kunnen ze de behaalde quest zien.
        /// </summary>
        private void BtnQuests_Click(object sender, RoutedEventArgs e)
        {
            ToonVoltooideQuests();
        }
        /// <summary>
        /// Deze functie laat de voltooide quests zien in een listbox in een popupwindow
        /// </summary>
        private void ToonVoltooideQuests()
        {
            Window voltooideQuests = new Window()
            {
                Title = "Behaalde Quests",
                Width = 500,
                Height = 500,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            ListBox LstVoltooideQuests = new ListBox();
            foreach(string quest in completedquests)
            {
                LstVoltooideQuests.Items.Add(quest);
            }
            voltooideQuests.Content = LstVoltooideQuests;
            voltooideQuests.ShowDialog();
        }
        /// <summary>
        /// Deze functie laat alle quests zien.
        /// </summary>
        private void InitializeQuests()
        {
            quests = new List<string>
            {
                "Behaal 1 cookie",
                "Koop 5 cursors",
                "Koop 5 grandma's",
                "Koop 5 farm's",
                "Koop 5 Mine's",
                "Koop 5 factory's",
                "Koop 5 bank's",
                "Koop 5 temple's",
                "Behaal 50 cookies",
                "Behaal 100 cookies",
                "Behaal 1000 cookies",
                "Behaal 5000 cookies",
                "Behaal 10000 cookies",
                "Behaal 50000 cookies",
                "Behaal 100000 cookies",
                "Behaal 500000 cookies",
                "Behaal 1000000 cookies",
                "Behaal 5000000 cookies",
                "Behaal 1000000000 cookies",
                "Behaal 5000000000 cookies"
            };
            completedquests = new List<string>();
        }
        /// <summary>
        /// In deze functie wordt er als er op de button gedrukt wordt een update van de statistieken en een popupvenster getoont van de statistieken.
        /// </summary>
        private void BtnStatistieken_Click(object sender, RoutedEventArgs e)
        {
            UpdateStatiestieken();
            ToonStatistieken();
        }
        /// <summary>
        /// In deze functie wordt de popupvenster gemaakt waar de statistieken in komen.
        /// </summary>
        private void ToonStatistieken()
        {
            Window statistieken = new Window()
            {
                Title = "Statistieken cookie clicker",
                Width=500, 
                Height=500,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            ListBox LstStatistiek = new ListBox();
            foreach(string statistiek in statistiek)
            {
                LstStatistiek.Items.Add(statistiek);
            }
            statistieken.Content = LstStatistiek;
            statistieken.ShowDialog();
        }
        /// <summary>
        /// In de deze functie staat een list met alle statistieken die in een popupvenster komt.
        /// </summary>
        private void InitializeStatistieken()
        {
            statistiek = new List<string>
            {
                $"Je hebt op dit moment {cookies} cookies",
                $"Je hebt tijdens dit spel al {totaalAantalCookies + coockiesPerSeconde} cookies geproduceerd",
                $"Je hebt al {LblTimer.Content}",
                $"Je hebt al {clicksManueel} keer manueel geklikt",
                $"Je hebt al {clicksGoudenCookies} keer op het gouden cookie geklikt",
                $"Je hebt al {aantalQuestsGehaald} quests behaald"
            };
        }
        /// <summary>
        /// In deze functie worden alle statistieken geupdate.
        /// </summary>
        private void UpdateStatiestieken()
        {
            InitializeStatistieken();
        }
    }
}
