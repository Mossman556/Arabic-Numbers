using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Arabic_Numbers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static Dictionary<int, string> romanNumerals = new Dictionary<int, string>
{
    {1000, "M"},
    {900, "CM"},
    {500, "D"},
    {400, "CD"},
    {100, "C"},
    {90, "XC"},
    {50, "L"},
    {40, "XL"},
    {10, "X"},
    {9, "IX"},
    {5, "V"},
    {4, "IV"},
    {1, "I"}
};

        public static string arabic_to_roman(int arabicNumber)
        {
            if (arabicNumber < 1 || arabicNumber > 3999)
            {
                // Arabic numerals must be between 1 and 3999
                return "";
            }

            string romanNumber = "";

            foreach (KeyValuePair<int, string> kvp in romanNumerals)
            {
                while (arabicNumber >= kvp.Key)
                {
                    romanNumber += kvp.Value;
                    arabicNumber -= kvp.Key;
                }
            }

            return romanNumber;
        }

        public static int roman_to_arabic(string romanNumber)
        {
            romanNumber = romanNumber.ToUpper();

            int arabicNumber = 0;
            int i = 0;

            while (i < romanNumber.Length)
            {
                int currentNumber = romanNumerals.FirstOrDefault(x => romanNumber.Substring(i).StartsWith(x.Value)).Key;
                if (currentNumber == 0)
                {
                    // Invalid Roman numeral
                    return 0;
                }

                arabicNumber += currentNumber;
                i += romanNumerals[currentNumber].Length;
            }

            if (arabicNumber < 1 || arabicNumber > 3999)
            {
                // Arabic numerals must be between 1 and 3999
                return 0;
            }

            return arabicNumber;
        }

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btn_change_Click(object sender, RoutedEventArgs e)
        {
            string arabicText = arabic_number.Text.Trim();
            string romanText = roman_number.Text.Trim();

            if (!string.IsNullOrEmpty(arabicText) && !string.IsNullOrEmpty(romanText))
            {
                // Jos molemmat tekstikentät ovat täytettyjä, ilmoita virheestä
                // Käyttäjän tulisi antaa vain yksi luku kerrallaan
                var messageDialog = new MessageDialog("Syötä vain yksi luku kerrallaan.");
                _ = messageDialog.ShowAsync();
            }
            else if (!string.IsNullOrEmpty(arabicText))
            {
                // Muunna arabialainen luku roomalaiseksi numeroksi
                if (int.TryParse(arabicText, out int arabicNumber))
                {
                    string romanNumber = arabic_to_roman(arabicNumber);
                    roman_number.Text = romanNumber;
                }
                else
                {
                    // Ilmoita virheestä, jos käyttäjä antoi virheellisen arabialaisen luvun
                    var messageDialog = new MessageDialog("Syötä kelvollinen arabialainen luku.");
                    _ = messageDialog.ShowAsync();
                }
            }
            else if (!string.IsNullOrEmpty(romanText))
            {
                // Muunna roomalainen numero arabialaiseksi luvuksi
                int arabicNumber = roman_to_arabic(romanText);
                if (arabicNumber is int)
                {
                    arabic_number.Text = arabicNumber.ToString();
                }
                else
                {
                    // Ilmoita virheestä, jos käyttäjä antoi virheellisen roomalaisen numeron
                    var messageDialog = new MessageDialog("Syötä kelvollinen roomalainen numero.");
                    _ = messageDialog.ShowAsync();
                }
            }
            else
            {
                // Ilmoita virheestä, jos käyttäjä ei syöttänyt mitään
                var messageDialog = new MessageDialog("Syötä arabialainen luku tai roomalainen numero.");
                _ = messageDialog.ShowAsync();
            }
        }

        private void btn_empty_Click(object sender, RoutedEventArgs e)
        {
            arabic_number.Text = "";
            roman_number.Text = "";
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}



