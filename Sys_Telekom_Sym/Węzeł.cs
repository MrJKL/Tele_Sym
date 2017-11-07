using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys_Telekom_Sym
{

    public class Wezel
    {
        IArbiter arbiter;

        Kanał[] kanały;
        int lKanałów = 1;

        Queue<Pakiet> qNormal = new Queue<Pakiet>();
        Queue<Pakiet> qPriorytet = new Queue<Pakiet>();

        public Wezel(IArbiter _arbiter, int _lKanałów)
        {
            arbiter = _arbiter;
            lKanałów = _lKanałów;
            kanały = new Kanał[lKanałów];

            for (int k = 0; k < lKanałów; k++)
                kanały[k] = new Kanał(_arbiter);
        }

        public void Przekaz(Pakiet _nowyPakiet)
        {
            if (_nowyPakiet == null) return;

            arbiter.DodajPakietNowy(_nowyPakiet);

            if (_nowyPakiet.priorytet)
            {
                qPriorytet.Enqueue(_nowyPakiet);
            }
            else
            {
                qNormal.Enqueue(_nowyPakiet);
            }
        }

        public void Przeslij(double _tura)
        {
            Pakiet tempPakiet = null;

            for (int i = 0; i < lKanałów; i++)
            {
                if (kanały[i].Wolny())
                {
                    if (qPriorytet.Any())
                    {
                        tempPakiet = qPriorytet.Dequeue();
                        tempPakiet.tTransmisji = _tura;
                        kanały[i].Weź(tempPakiet);
                    }
                    else
                    {
                        if (qNormal.Any())
                        {
                            tempPakiet = qNormal.Dequeue();
                            tempPakiet.tTransmisji = _tura;
                            kanały[i].Weź(tempPakiet);
                        }

                    }
                }
                else
                {
                    kanały[i].Prześlij();
                }
            }
        }
    }

}
