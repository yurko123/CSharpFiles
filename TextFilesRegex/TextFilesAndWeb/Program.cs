using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace TextFilesAndWeb
{  
    class Program
    {
        public static string res = "";
       

        public static void ConsoleConfig(string title)
        {
            Console.Title = title;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.OutputEncoding = Encoding.GetEncoding(1251); // може буть несумісність кодувань
            Console.InputEncoding = Encoding.GetEncoding(1251);
        }
    
        public static void GetSE(string adrress)
        {

            try
            {
                WebRequest myRequest = WebRequest.Create(adrress);
                WebResponse myResponse = myRequest.GetResponse();
                //Console.Write(myResponse.ContentType);
               
                
                    Stream html = myResponse.GetResponseStream();
                    StreamReader readhtml = new StreamReader(html, Encoding.GetEncoding(1251));
                   //StreamWriter linkswriter = new StreamWriter(@"Links.txt");// закомічано бо дужеее багато силок
                    ///StreamWriter mailswriter = new StreamWriter(@"Mails.txt");
                    string datahtml = readhtml.ReadToEnd();
                    myResponse.Close();
                    //Console.WriteLine(datahtml);

                    Regex href = new Regex(@"((http)|(https)):\/\/[a-zA-Z0-9-,]+(\.[a-zA-Z0-9-,/&?=_%$#*^!]+)+");
                    Regex email = new Regex(@"[a-zA-Z0-9-,.]+@[a-zA-Z0-9-,]+(\.[a-zA-Z0-9-,/&?=_]+)+");
                    Match hnew = href.Match(datahtml);
                    Match mail = email.Match(datahtml);

                    while (hnew.Success)
                    {
                       Console.WriteLine(hnew.Value);
                        //linkswriter.WriteLine(hnew.Value);
                       while (mail.Success)
                       {
                          //mailswriter.WriteLine(hnew.Value);
                           Console.WriteLine(mail.Value);
                           mail = mail.NextMatch();
                       }
                        if (!res.Contains(hnew.Value)) { res +="\n"+hnew.Value; GetSE(hnew.Value); }
                        hnew=hnew.NextMatch();

                    }

                  // mailswriter.Close();
                   // linkswriter.Close();
            }
            catch (Exception e)
            {
                Console.Write(e);
                
            }
           

        }
        static void Main(string[] args)
        {
            ConsoleConfig("Отримання адрес");
         GetSE(@"http://www.zu.edu.ua");
         //Console.Write(res);
         
            Console.ReadKey();
        }
    }
}
