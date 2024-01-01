using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CodePerformance
{
    internal static class Result
    {
        private const ConsoleColor backgroundBorder = ConsoleColor.Black;
        private const ConsoleColor foregroundBorder = ConsoleColor.Gray;
        private const ConsoleColor backgroundTitle = ConsoleColor.Black;
        private const ConsoleColor foregroundTitle = ConsoleColor.White;
        private const ConsoleColor backgroundMainTitle = ConsoleColor.Black;
        private const ConsoleColor foregroundMainTitle = ConsoleColor.Yellow;
        private const ConsoleColor backgroundResult = ConsoleColor.Black;
        private const ConsoleColor foregroundResult = ConsoleColor.Green;

        private const int identation = 2;
        private const int spacing = 35;
        private const int resultMaxLength = 25;
        private const int end = spacing + resultMaxLength;
        private const int endAfterBorder = spacing + resultMaxLength + 2;
        
        private static List<object> values = new List<object>();

        public static int Precision { get; set; } = 3;

        public static void MainTitle(string name)
        {
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;

            Print_TitleIdentation();
            Console.BackgroundColor = backgroundMainTitle;
            Console.ForegroundColor = foregroundMainTitle;
            Console.Write("{0}", name);

            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
            Console.Write(" ");
            int borderStart = name.Length + identation + 1;
            Console.CursorLeft = borderStart;
            Console.Write(new string('=', end - borderStart));
            Console.WriteLine();
        }

        public static void Title(string name)
        {
            values.Clear();
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;

            Print_TitleIdentation();
            Console.BackgroundColor = backgroundTitle;
            Console.ForegroundColor = foregroundTitle;
            Console.Write("{0}", name);

            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
            Console.Write(" ");
            int borderStart = name.Length + identation + 1;
            Console.CursorLeft = borderStart;
            Console.Write(new string('=', end - borderStart));
            Print_EndBorder();
            Print_Bottom();
            Print_Loading();
            //Print_Separator();
        }

        public static void Print(string name = null, object value = null, string suffix = "", bool preparation = false)
        {
            values.Add(value);
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;

            if (string.IsNullOrEmpty(name) == false)
            {
                Print_ResultIdentation();
                Console.Write("{0}:", name);
            }

            if (preparation == false)
            {
                Console.BackgroundColor = backgroundResult;
                Console.ForegroundColor = foregroundResult;
            }
            else
            {
                Console.BackgroundColor = backgroundBorder;
                Console.ForegroundColor = foregroundBorder;
            }
            Console.CursorLeft = spacing;
            if (value != null)
                Console.Write("{0} {1}", TryRound(value, suffix, out string newSuffix), newSuffix);

            Print_EndBorder();
            Print_Bottom();
            Print_Loading();

            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
        }

        public static void Summary(bool hasPreparation, string suffix = "")
        {
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;

            if (hasPreparation) values.RemoveAt(0);
            Console.CursorTop += 3;
            for (int i = 0; i < values.Count + 5; i++)
            {
                Print_Empty();
                Console.CursorTop--;
            }
            Print_ResultIdentation();
            values.Sort();
            if (values.Count > 0)
            {
                Colorful.Console.Write($"Result: ");
                object min = TryRound(values.First(), suffix, out string minSuffix);
                Colorful.Console.Write($"{min} {minSuffix}", Color.LimeGreen);
                Colorful.Console.Write($" - ");
                object max = TryRound(values.Last(), suffix, out string maxSuffix);
                Colorful.Console.Write($"{max} {maxSuffix}", foregroundResult, Color.Tomato);
            }
            Print_EndBorder();
            End();

            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
        }

        public static void End()
        {
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.BackgroundColor = backgroundResult;
            Console.ForegroundColor = foregroundResult;

            Console.CursorTop -= 1;
            Print_Bottom();
            Console.WriteLine();
            Print_Empty();
            Console.WriteLine();

            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
        }

        private static void Print_Empty()
        {
            Console.CursorLeft = 0;
            Console.Write(new string(' ', endAfterBorder));
            Console.CursorLeft = 0;
        }

        private static void Print_Bottom()
        {
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.BackgroundColor = backgroundBorder;
            Console.ForegroundColor = foregroundBorder;

            Console.WriteLine();
            Console.Write(new string('=', endAfterBorder));

            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
        }

        private static void Print_Loading()
        {
            Console.CursorTop -= 1;
            Print_ResultIdentation();
            Colorful.Console.Write("Loading...", Color.White);
        }

        private static void Print_Separator()
        {
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.BackgroundColor = backgroundBorder;
            Console.ForegroundColor = foregroundBorder;

            Print_ResultIdentation();
            Console.Write(new string('-', end - identation));
            Print_EndBorder();

            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
        }

        private static void Print_ResultIdentation()
        {
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.BackgroundColor = backgroundBorder;
            Console.ForegroundColor = foregroundBorder;

            Print_Empty();
            Console.Write("- ");
            Console.CursorLeft = identation;

            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
        }

        private static void Print_TitleIdentation()
        {
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.BackgroundColor = backgroundBorder;
            Console.ForegroundColor = foregroundBorder;

            Console.CursorLeft = 0;
            Console.Write("= ");
            Console.CursorLeft = identation;

            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
        }

        private static void Print_EndBorder()
        {
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.BackgroundColor = backgroundBorder;
            Console.ForegroundColor = foregroundBorder;

            Console.CursorLeft = end;
            Console.Write(" =");
            Console.WriteLine();

            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
        }

        private static object TryRound(object value, string suffix, out string newSuffix)
        {
            Type t = value.GetType();
            double result = 0;
            if (t.Equals(typeof(float))
                || t.Equals(typeof(double))
                || t.Equals(typeof(decimal)))
            {
                if (t.Equals(typeof(float)))
                {
                    result = Convert.ToDouble((float)value);
                }
                else if (t.Equals(typeof(double)))
                {
                    result = Convert.ToDouble((double)value);
                }
                else if (t.Equals(typeof(decimal)))
                {
                    result = Convert.ToDouble((decimal)value);
                }
                newSuffix = PowerPrefix(ref result) + suffix;
                result = Math.Round(result, Precision);
                return result;
            }
            else if (double.TryParse(value.ToString(), out result))
            {
                newSuffix = PowerPrefix(ref result) + suffix;
                result = Math.Round(result, Precision);
                return result;
            }
            else
            {
                newSuffix = suffix;
                return value;
            }
        }

        public static string PowerPrefix(ref double value)
        {
            Dictionary<int, string> powerPrefix = new Dictionary<int, string>()
            {
                { 18, "E" },
                { 15, "P" },
                { 12, "T" },
                { 9, "G" },
                { 6, "M" },
                { 3, "k" },
                { 0, string.Empty },
                { -3, "m" },
                { -6, "u" },
                { -9, "n" },
                { -12, "p" },
                { -15, "f" },
                { -18, "a" },
            };

            int power3 = 0;
            while (value > 1000)
            {
                value /= 1000;
                power3 += 3;
            }
            while (value < 1 && value != 0)
            {
                value *= 1000;
                power3 -= 3;
            }

            return powerPrefix[power3];
        }
    }

}
