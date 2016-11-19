using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace TextFilesRegex
{
    class Program
    {
        public static void ConsoleConfig(string title)
        {
            Console.Title = title;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.OutputEncoding = Encoding.GetEncoding(1251); // може буть несумісність кодувань
            Console.InputEncoding = Encoding.GetEncoding(1251); 
        }
       public static void WorkWithTextFiles (string fileName )
        {
         
           FileInfo Text = new FileInfo(fileName); 

            if (Text.Exists)
            {
                string txt = Text.OpenText().ReadToEnd();
                Regex abz = new Regex(@"<p>.*?<\/p>");
                Regex pic = new Regex(@"<img.*?/>");
                Regex href = new Regex(@"((http)|(https)):\/\/[a-zA-Z0-9-,]+(\.[a-zA-Z0-9-,/&?=_%$#*^!]+)+");
                Regex email = new Regex(@"[a-zA-Z0-9-,.]+@[a-zA-Z0-9-,]+(\.[a-zA-Z0-9-,/&?=_]+)+");
                MatchCollection z = abz.Matches(txt);
                MatchCollection c = pic.Matches(txt);
                Console.WriteLine("кількість абзаців (тегів <p>...</p>): {0} \nкількість зображень (тегів <img ... />): {1}", z.Count, c.Count);
                MatchCollection h = href.Matches(txt);
                MatchCollection em = email.Matches(txt);
                Console.WriteLine("силочки");
                if (h.Count > 0)
                    foreach (Match hr in h)
                        Console.WriteLine(hr.Value);
                Console.WriteLine("електронні адреси");
                if (em.Count > 0)
                    foreach (Match e in em)
                        Console.WriteLine(e.Value);

            }
            else Console.WriteLine("Файл : " + fileName + " не існує");
        }
        static void Main(string[] args)
        {
            ConsoleConfig("Робота з текстовими файлами 1"); 
            Console.WriteLine("шлях до HTML-файла на диску");
            WorkWithTextFiles(Console.ReadLine()); 

             
            Console.ReadKey();
        }

    }
}
