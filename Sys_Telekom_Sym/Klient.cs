using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys_Telekom_Sym
{

    public class Klient
    {
        public static int licznik = 1;
        public int nrKlienta;
        public bool priorytet;
        private static Random rng = new Random();

        public Klient(bool _priorytet)
        {
            priorytet = _priorytet;
            nrKlienta = licznik;
            licznik++;
        }

        public Pakiet genPakiet(double tSymulacji)
        {
            if (rng.Next(1, 1000) == 500)
            {
                return new Pakiet(nrKlienta, tSymulacji, priorytet);
            }
            else
            {
                return null;
            }
        }
    }

}
