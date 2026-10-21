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

            //7. feladat
            Console.WriteLine("7. feladat");
            kuldetesek.Where(x => x.Legenyseg.Contains("Neil Armstrong")).ToList().ForEach(w => Console.WriteLine(w.Kuldetes));

            //8. feladat
            Console.WriteLine("8. feladat");
            kuldetesek.Where(x => x.FoCel.ToLower().Contains("hold")).ToList().ForEach(w => Console.WriteLine(w.Kuldetes));

            //9. feladat
            Console.WriteLine("9. feladat");
            kuldetesek.OrderBy(x=>x.InditasDatuma).ToList().ForEach(w => Console.WriteLine($"{w.Kuldetes,-15}{w.InditasDatuma}"));
            
            //10. feladat
            Console.WriteLine("10. feladat");
            kuldetesek
                .Where(x => x.FoCel.ToLower().Contains("holdkomp"))
                .SelectMany(x => x.Legenyseg)
                .Distinct()
                .ToList()
                .ForEach(w => Console.WriteLine(w));

            //11. feladat
            Console.WriteLine("11. feladat");
            var holdraszall = kuldetesek.Where(x => x.FoCel.ToLower().Contains("holdraszállás") && x.Eredmeny == "Sikeres").ToList().First();
            Console.WriteLine($"Első sikeres holdraszállás: {holdraszall.Kuldetes}");

            //12. feladat
            Console.WriteLine("12. feladat");
            kuldetesek
                .SelectMany(x => x.Legenyseg)
                .GroupBy(x => x)
                .Select(x => new { nev = x.Key, db = x.Count() })
                .ToList()
                .ForEach(w => Console.WriteLine($"{w.nev,-25}{w.db}"));

            //13. feladat
            Console.WriteLine("13. feladat");
            kuldetesek
                .Where(x => x.Legenyseg.Count == 3)
                .ToList()
                .ForEach(w => Console.WriteLine($"{w.Kuldetes,-15}{string.Join(", ",w.Legenyseg)}"));

            //14. feladat
            Console.WriteLine("14. feladat");
            var csoportok = kuldetesek.GroupBy(x => x.Eredmeny);
            foreach (var csoport in csoportok)
            {
                Console.WriteLine(csoport.Key);
                foreach (var kuldetes in csoport)
                {
                    Console.WriteLine($"\t{kuldetes.Kuldetes}");
                }
            }

            //kuldetesek
            //        .GroupBy(x => x.Eredmeny)
            //        .ToList()
            //        .ForEach(csoport =>
            //        {
            //            Console.WriteLine(csoport.Key);
            //            csoport.ToList().ForEach(kuldetes => Console.WriteLine($"\t{kuldetes.Kuldetes}"));
            //        });

            //kuldetesek
            //        .GroupBy(x => x.Eredmeny)
            //        .Select(csoport => $"{csoport.Key}\n" + string.Join("\n", csoport.Select(k => $"\t{k.Kuldetes}")))
            //        .ToList()
            //        .ForEach(Console.WriteLine);

            //15. feladat
            Console.WriteLine("15. feladat");
            kuldetesek
                .GroupBy(x => x.InditasDatuma.Year)
                .Select(x => new
                {
                    ev = x.Key,
                    sikeres = x.Count(y => y.Eredmeny == "Sikeres"),
                    sikertelen = x.Count(y => y.Eredmeny == "Sikertelen")
                })
                .ToList()
                .ForEach(w => Console.WriteLine($"{w.ev,-5} \n\tsikeres küldetések:{w.sikeres}\n\tsikertelen küldetések: {w.sikertelen}"));


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
