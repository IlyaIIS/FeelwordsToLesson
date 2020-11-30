using System;
using System.Collections.Generic;
using System.Text;

namespace Fillwords
{
    static class Settings
    {
        static public int xSize = 10;
        static public int ySize = 10;
        static public int cellSize = 1;
        static public int fieldColor;
        static public int underCursorColor;
        static public int pickedWordColro;
        static public int guessedWordColro;
        static public bool isRandomGuessedWordColro = true;

        static public SettingsIndexer property = new SettingsIndexer();
        static public ColorsSet Colors = new ColorsSet();
    }

    class SettingsIndexer
    {
        public object this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Settings.xSize;
                    case 1: return Settings.ySize;
                    case 2: return Settings.cellSize;
                    case 3: return Settings.fieldColor;
                    case 4: return Settings.underCursorColor;
                    case 5: return Settings.pickedWordColro;
                    case 6: return Settings.guessedWordColro;
                    case 7: return Settings.isRandomGuessedWordColro;
                    default: return 0;
                }
            }
            set
            {
                switch (index)
                {
                    case 0: Settings.xSize                    = (int)value; break;
                    case 1: Settings.ySize                    = (int)value; break;
                    case 2: Settings.cellSize                 = (int)value; break;
                    case 3: Settings.fieldColor               = (int)value; break;
                    case 4: Settings.underCursorColor         = (int)value; break;
                    case 5: Settings.pickedWordColro          = (int)value; break;
                    case 6: Settings.guessedWordColro         = (int)value; break;
                    case 7: Settings.isRandomGuessedWordColro = (bool)value; break;
                }
            }
        }
    }

    class ColorsSet
    {
        public dynamic[,] colorsList { get; private set; } =
        {
            { ConsoleColor.Black    , ConsoleColor.White },
            { ConsoleColor.DarkBlue , ConsoleColor.White },
            { ConsoleColor.DarkGreen, ConsoleColor.White },
            { ConsoleColor.DarkCyan , ConsoleColor.White },
            { ConsoleColor.DarkRed  , ConsoleColor.White },
            { ConsoleColor.DarkMagenta, ConsoleColor.White },
            { ConsoleColor.DarkYellow , ConsoleColor.Black },
            { ConsoleColor.Blue     , ConsoleColor.Black },
            { ConsoleColor.Green    , ConsoleColor.Black },
            { ConsoleColor.Cyan     , ConsoleColor.Black },
            { ConsoleColor.Red      , ConsoleColor.White },
            { ConsoleColor.Magenta  , ConsoleColor.White },
            { ConsoleColor.Yellow   , ConsoleColor.Black }
        };

        public dynamic[] GetRandomColor()
        {
            Random rnd = new Random();
            int randomNum = rnd.Next(12) + 1;

            return new dynamic[]{ colorsList[randomNum, 0],
                                  colorsList[randomNum, 1] };
        }
    }
}
