using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEEE754
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "IEEE 754 float Convertion JellyBeanci (c)";
            bool state = true;
            do
            {
                try
                {
                    ConsoleColor color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("IEEE 754 Float Convertion.");
                    Console.ForegroundColor = color;
                    Console.Write("Enter the Number-> ");
                    float number = GetUserInput();
                    Console.WriteLine(FloatToBinary(number));
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Real number: {0}\n", number);
                    Console.ForegroundColor = color;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Illegal Input!\n");
                    state = false;
                }
            }
            while (state);
            Console.Write("Press any button to Exit");
            Console.ReadKey();
        }
        static float GetUserInput()
        {
            float value = -1;
            try
            {
                string rawInput = Console.ReadLine();
                if (rawInput.Contains("."))
                {
                    // if has '.' we need to convert ','
                    var sep = rawInput.Split('.');
                    // split from '.'
                    if (sep.Length == 2)
                    {
                        // must has 2 cause (foo) . (bar) more than 1 point doesn't make any sense.
                        // Whole.Fraction
                        float whole = Int32.Parse(sep[0]);

                        // number / 10^number's length 
                        float fraction = (float)(Int32.Parse(sep[1]) / Math.Pow(10, sep[1].Length));
                        if(whole < 0)
                        {
                            //if negative must subtract
                            value = whole - fraction;
                        }
                        else
                        {
                            //if positive must add
                            value = whole + fraction;
                        }
                    }
                    else
                    {
                        // throw new IllegalArgumentException
                        throw new ArgumentException("Illegal Argument");
                    }
                }
                else
                {
                    return float.Parse(rawInput);
                }
            }
            catch
            {
                throw new ArgumentException("Illegal Argument");
            }
            return value;
        }
        static string FloatToBinary(float f)
        {
            StringBuilder sb = new StringBuilder();
            Byte[] ba = BitConverter.GetBytes(f);
            foreach (Byte b in ba)
            {
                for (int i = 0; i < 8; i++)
                {
                    sb.Insert(0, ((b >> i) & 1) == 1 ? "1" : "0");
                }
            }
            string s = sb.ToString();
            string r = s.Substring(0, 1) + " " + s.Substring(1, 8) + " " + s.Substring(9); //sign exponent mantissa
            return r;
        }
    }
}