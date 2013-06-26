using System;
using System.Collections.Generic;
using System.Windows.Data;  // for Converter
using System.Text.RegularExpressions;
using SharedClasses.SampleModels;   // for splitting strings (regex)

namespace DataBindingPracticeDos
{
    /// <summary>
    ///     Class which converts List<Game> to strings and vice-versa.
    /// </summary>
    [ValueConversion(typeof(object), typeof(string))]   // making it visible for GUI
    public class GameListToStringConverter : IValueConverter
    {
        // Conversion GameList to String
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string tmp = "";
            List<Game> gameList = (List<Game>)value;
            Console.WriteLine("Conversion GameList to String occured.");

            foreach (Game g in gameList)
            {
                tmp += g.ToString() + "\r\n";
            }

            return tmp;
        }

        // Conversion String to GameCatalogue (not yet required/in use)
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            GameCatalogue gc = new GameCatalogue(new Author("Unknown"), "-");
            string[] catInfo = Regex.Split((string)value, "\r\n");   // Stringinfo am Zeilenumbruch splitten --> enthaelt pro Zeile Info des Games (Title by Publisher)
            Console.WriteLine("Conversion String to GameCatalogue occured.");

            foreach (string s in catInfo)
            {
                string[] gameInfo = Regex.Split(s, " by ");
                if (gameInfo.Length >= 2 && gameInfo[0].Length > 0 && gameInfo[1].Length > 0)
                {
                    gc.Add(new Game(gameInfo[0], gameInfo[1]));
                    #region Info Ausgabe
                    Console.WriteLine("New game added to catalogue: {0} by {1}", gameInfo[0], gameInfo[1]);
                    #endregion Info Ausgabe
                }
            }
            return gc;
        }
    }
}
