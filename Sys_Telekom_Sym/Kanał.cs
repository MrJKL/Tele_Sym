using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys_Telekom_Sym
{
    public class Kanał
    {
        IArbiter arbiter;
        private int tTrwania = 0;
        Pakiet pakiet = null;

        public Kanał(IArbiter _arbiter)
        {
            arbiter = _arbiter;
            pakiet = null;
        }

        public bool Wolny()
        {
            return pakiet == null;
        }

        public void Weź(Pakiet _pakiet)
        {
            pakiet = _pakiet;
            tTrwania = pakiet.rozmiarPakietu;
        }

        public void Prześlij()
        {
            tTrwania--;
            if (tTrwania == 0)
            {
                arbiter.DodajPakietPrzesłany(pakiet);
                pakiet = null;
            }
        }
    }
}
