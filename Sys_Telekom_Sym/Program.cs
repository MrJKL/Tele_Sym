using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys_Telekom_Sym
{
    class Program
    {
        static void Main(string[] args)
        {
            int lKlientow;
            int lKlientowPr;
            int lKanałów;
            double tSymulacji;
            double lTur;

            Console.Write("Podaj liczbę kanałów: ");
            lKanałów = Convert.ToInt32(Console.ReadLine());

            Console.Write("Podaj liczbę klientów bez priorytetu: ");
            lKlientow = Convert.ToInt32(Console.ReadLine());

            Console.Write("Podaj liczbę klientów z priorytetem: ");
            lKlientowPr = Convert.ToInt32(Console.ReadLine());

            Console.Write("Podaj czas symulacji w minutach: ");
            tSymulacji = Convert.ToDouble(Console.ReadLine());
            lTur = tSymulacji * 60000;

            Symulacja(lKlientow, lKlientowPr, lTur, lKanałów);


            Console.ReadKey();
        }

        // Właciwa symulacja

        static void Symulacja(int lKlientow, int lKlientowPr, double lTur, int lKanałów)
        {
            //IArbiter arbiter = new SzybkiArbiter();
            IArbiter arbiter = new DokladnyArbiter();

            double tura = 1;
            int calkowitaLK = lKlientow + lKlientowPr;
            Klient[] klienci = new Klient[calkowitaLK];
            Wezel wezelKom = new Wezel(arbiter, lKanałów);

            for (int i = 0; i < calkowitaLK; i++)
            {
                if (i < lKlientow)
                {
                    klienci[i] = new Klient(false);
                }
                else
                {
                    klienci[i] = new Klient(true);
                }
            }

            while (tura <= lTur)
            {
                // Tworzenie pakietów
                for (int i = 0; i < calkowitaLK; i++)
                {
                    wezelKom.Przekaz(klienci[i].genPakiet(tura));
                }

                // Transmisja pakietów
                wezelKom.Przeslij(tura);



                if (tura % 10000 == 0)
                {
                    Console.CursorLeft = 0;
                    Console.Write("Ukończono: {0}%", Convert.ToInt32((tura / lTur) * 100));
                }

                tura++;
            }

            Console.WriteLine("");

            arbiter.UstawCzasSymulacji(tura);
            arbiter.Statystyki();

            return;
        }

    }
}
