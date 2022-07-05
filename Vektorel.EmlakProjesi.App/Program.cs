using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vektorel.EmlakProjesi.App
{
    class Program
    {
        static void Main(string[] args)
        {
            string dosyaYolu = @"D:\emlak.txt";
            Ev[] evler = EvleriGetir(p: dosyaYolu);


            Console.WriteLine("Emlak programına hoş geldiniz.");
            while (true)
            {
                Console.WriteLine("1-Ev ekle\n2-Evleri görüntüle\n3-Ev bul\n4-Ev sil\n5-Çıkış");
                char c = Console.ReadKey(true).KeyChar;
                if (c == '1')
                {
                    Ev ev = new Ev();
                    Console.Write("Semt:");
                    ev.Semt = Console.ReadLine();
                    Console.Write("Oda Sayısı:");
                    ev.OdaSayisi = Console.ReadLine();
                    Console.Write("Yapım yılı:");
                    ev.YapimYili = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Fiyat:");
                    ev.Fiyat = Convert.ToDouble(Console.ReadLine());
                    Ev sonEv = evler[evler.Length - 1];
                    ev.Id = sonEv.Id+1;

                    Array.Resize(ref evler, evler.Length + 1);
                    evler[evler.Length - 1] = ev;
                    File.AppendAllText(dosyaYolu, ev.BilgileriGetir());
                }
                else if (c == '2')
                {
                    foreach (var ev in evler)
                    {
                        Console.WriteLine(ev.BilgiGetir());
                    }
                }
                else if (c == '3')
                {
                    Console.WriteLine("1-Fiyata göre");
                    c = Console.ReadKey(true).KeyChar;
                    if (c=='1')
                    {
                        Console.Write("Max:");
                        double max = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Min:");
                        double min = Convert.ToDouble(Console.ReadLine());
                        foreach (var ev in evler)
                        {
                            if (ev.Fiyat>min&&ev.Fiyat<max)
                            {
                                Console.WriteLine(ev.BilgiGetir());
                            }
                        }
                    }
                }
            }
        }
        static Ev[] EvleriGetir(string p)
        {
            Ev[] result = new Ev[0];
            if (File.Exists(p))
            {
                string[] satirlar = File.ReadAllLines(p);
                foreach (string satir in satirlar)
                {
                    Ev ev = new Ev();
                    string[] props = satir.Split('#');
                    string[] keyValue = props[0].Split(':');
                    ev.Id = Convert.ToInt32(keyValue[1]);
                    ev.Semt = props[1].Split(':')[1];
                    ev.OdaSayisi = props[2].Split(':')[1];
                    ev.YapimYili = Convert.ToInt32(props[3].Split(':')[1]);
                    ev.Fiyat = Convert.ToDouble(props[4].Split(':')[1]);
                    Array.Resize(ref result, result.Length + 1);
                    result[result.Length - 1] = ev;
                }
            }
            return result;
        }
        
    }

    class Ev
    {
        public int Id { get; set; }

        public string Semt { get; set; }

        public string OdaSayisi { get; set; }

        public int YapimYili { get; set; }

        public double Fiyat { get; set; }

        public string BilgileriGetir()
        {
            return $"Id:{Id}#Semt:{Semt}#Oda Sayısı:{OdaSayisi}#Yapım yılı:{YapimYili}#Fiyat:{Fiyat}{Environment.NewLine}";
        }
        public string BilgiGetir()
        {
            return $"Id:{Id} Semt:{Semt} Oda Sayısı:{OdaSayisi} Yapım yılı:{YapimYili} Fiyat:{Fiyat}";
        }
    }
}
