using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys_Telekom_Sym
{
    public class Pakiet
    {
        public int nrKlienta;
        public bool priorytet;
        public static double licznik = 1;
        public double nrPakietu;
        public double tPowstania;
        public double tTransmisji;
        public int rozmiarPakietu = 100;

        public Pakiet(int _nrKlienta, double _tPowstania, bool _priorytet)
        {
            nrKlienta = _nrKlienta;
            tPowstania = _tPowstania;
            priorytet = _priorytet;
            nrPakietu = licznik;

            licznik++;
        }
    }
}
