using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apolloLinq
{
    internal class ApolloKuldetes
    {
        public ApolloKuldetes(string kuldetes, DateTime inditasDatuma, string foCel, string eredmeny, string legenyseg)
        {
            Kuldetes = kuldetes;
            InditasDatuma = inditasDatuma;
            FoCel = foCel;
            Eredmeny = eredmeny;
            Legenyseg = legenyseg.Split(',').Select(x=>x.Trim()).ToList();
        }

        /*Küldetés;Indítás dátuma;Fő cél;Eredmény;Legénység
Apollo 1;1967.február.21.;Első emberes repülés, földi teszt;Sikertelen;Virgil 'Gus' Grissom, Edward White, Roger B. Chaffee*/

        public string Kuldetes { get; set; }
        public DateTime InditasDatuma { get; set; }
        public string FoCel { get; set; }
        public string Eredmeny { get; set; }
        public List<string> Legenyseg { get; set; }
    }
}
