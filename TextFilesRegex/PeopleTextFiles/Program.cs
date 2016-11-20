using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace PeopleTextFiles
{
    
    class Program
    {
        static string [] BulbSort (string [] array)
       {
        string temp = "";
        string [] arr= (string []) array.Clone();
        Regex regexp = new Regex(@"(\d+) ([a-zA-z_0-9а-яА-Я'ІЇЄіїє\-]+ [a-zA-z_0-9а-яА-Я'ІЇЄіїє\-]\.[a-zA-z_0-9а-яА-Я'ІЇЄіїє\-]\. )+(\d+ грн\.) (\d+) грн\.");
            bool ws=true;
           while(ws)
            {
                ws = false;
                for(int j=0;j<arr.Length-1;j++)
                    if (int.Parse(regexp.Match(arr[j]).Groups[4].Value) > int.Parse(regexp.Match(arr[j+1]).Groups[4].Value))
                    {
                        temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        ws = true;
                    }
            }
           return arr;
       }
        public static void ConsoleConfig(string title)
        {
            Console.Title = title;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.OutputEncoding = Encoding.GetEncoding(1251); // може буть несумісність кодувань
            Console.InputEncoding = Encoding.GetEncoding(1251);
        }

        static void Main(string[] args)
        {
            ConsoleConfig("dfsd");
            Regex regexp = new Regex(@"(\d+) (([a-zA-z_0-9а-яА-Я'ІЇЄіїє\-]+ [a-zA-z_0-9а-яА-Я'ІЇЄіїє\-]\.[a-zA-z_0-9а-яА-Я'ІЇЄіїє\-]\. )+)(\d+ грн\.) (\d+) грн\.");
            StreamReader sr = new StreamReader(@"1.txt",Encoding.GetEncoding(1251));
            
            MatchCollection mc = regexp.Matches(sr.ReadToEnd());
            if (mc.Count > 0)
            {
                int i = 0;
                string[] str = new string[mc.Count];
                foreach (Match m in mc)
                {
                    str[i] = m.Value;
                    i++;
                }
                str = BulbSort(str);
                int sum = 0;
                StreamWriter sw = new StreamWriter(@"Output1.txt");
                StreamWriter sw1 = new StreamWriter(@"Output2.txt");
                foreach (string s in str)
                {
                    Console.WriteLine(s);
                    sw.WriteLine(s);
                    sum += int.Parse(regexp.Match(s).Groups[5].Value);
                    sw1.WriteLine(regexp.Match(s).Groups[1].Value + ' '+regexp.Match(s).Groups[2].Value);
                }
                sw.WriteLine("summa :"+sum);
                Console.WriteLine("summa :" + sum);
                sw.Close();
                sw1.Close();
            }
            
         
            Console.ReadKey();
        }
    }
}