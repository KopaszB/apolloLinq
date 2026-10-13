using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace apolloLinq
{
    internal class Program
    {
        static List<ApolloKuldetes> kuldetesek = new List<ApolloKuldetes>();
        static void Main(string[] args)
        {
            adatBeolvas();
            //adatKiir();

            //1. feladat
            Console.WriteLine("1. feladat");
            kuldetesek.Select(x => x.Kuldetes).ToList().ForEach(w => Console.WriteLine(w));

            //2. feladat
            Console.WriteLine("2. feladat");
            Console.WriteLine($"Sikeres küldetések száma: {kuldetesek.Count(x=>x.Eredmeny=="Sikeres")}");

            //3. feladat
            Console.WriteLine("3. feladat");
            //kuldetesek
            //.GroupBy(x => x.InditasDatuma.Year)
            //.Select(x => new { ev = x.Key, kuldetesSzam = x.Count() })
            //.OrderByDescending(x => x.kuldetesSzam)
            //.Take(1)
            //.ToList()
            //.ForEach(w => Console.WriteLine($"Év: {w.ev,-8} Küldetések száma:{w.kuldetesSzam}"));
            var legtobb = kuldetesek
                .GroupBy(x => x.InditasDatuma.Year)
                .OrderByDescending(x=>x.Count())
                .First();
            Console.WriteLine($"Év: {legtobb.Key,-8} Küldetések száma:{legtobb.Count()}");

            //4. feladat
            Console.WriteLine("4. feladat");
            kuldetesek.Where(x => x.Eredmeny == "Sikertelen").ToList().ForEach(w => Console.WriteLine($"Küldetés neve: {w.Kuldetes,-20} küldetés éve: {w.InditasDatuma.Year}"));

            //5. feladat
            Console.WriteLine("5. feladat");
            kuldetesek.Where(x => x.FoCel.ToLower().Contains("holdraszállás")).ToList().ForEach(w => Console.WriteLine(w.Kuldetes));

            //6. feladat
            Console.WriteLine("6. feladat");
            kuldetesek.SelectMany(x => x.Legenyseg).Distinct().ToList().ForEach(w => Console.WriteLine(w));
            Console.WriteLine($"{kuldetesek.SelectMany(x => x.Legenyseg).Distinct().Count()} különböző űrhajós vett részt a programban");



            Console.ReadKey();
        }

        private static void adatKiir()
        {
            foreach (var item in kuldetesek)
            {
                Console.WriteLine($"{item.Legenyseg[0],-25}{item.Legenyseg[1],-25}{item.Legenyseg[2]}");
            }
        }

        private static void adatBeolvas()
        {
            try
            {
                using (StreamReader olvas = new StreamReader("apollo_program.txt"))
                {
                    olvas.ReadLine();
                    while (!olvas.EndOfStream)
                    {
                        string[] sor = olvas.ReadLine().Split(';');
                        var egyKuldetes = new ApolloKuldetes
                            (
                            sor[0],
                            DateTime.Parse(sor[1]),
                            sor[2],
                            sor[3],
                            sor[4]
                            );
                        kuldetesek.Add(egyKuldetes);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Hiba! {e.Message}");
            }
        }
    }
}
