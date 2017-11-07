using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys_Telekom_Sym
{

    public interface IArbiter
    {
        void DodajPakietNowy(Pakiet pakiet);
        void DodajPakietPrzesłany(Pakiet pakiet);
        void UstawCzasSymulacji(Double _czas);
        void Statystyki();
    }

    class SzybkiArbiter : IArbiter
    {
        Double lPakietNN, lPakietNP, lPakietN, lPakietOpóźN, lPakietOpóźP, op, OpóźnN, OpóźnP, lPakietP, czas;

        public SzybkiArbiter()
        {
            lPakietNN = 0;
            lPakietNP = 0;
            lPakietN = 0;
            lPakietOpóźN = 0;
            lPakietOpóźP = 0;
            op = 0;
            OpóźnN = 0;
            OpóźnP = 0;
            lPakietP = 0;
            czas = 0;
        }

        public void DodajPakietNowy(Pakiet pakiet)
        {
            if (pakiet.priorytet)
            {
                lPakietNP++;
            }
            else
            {
                lPakietNN++;
            }
        }

        public void DodajPakietPrzesłany(Pakiet pakiet)
        {
            op = pakiet.tTransmisji - pakiet.tPowstania;

            if (pakiet.priorytet)
            {
                lPakietP++;
                OpóźnP += op;

                if (op > 0)
                {
                    lPakietOpóźP++;
                }
            }
            else
            {
                lPakietN++;
                OpóźnN += op;

                if (op > 0)
                {
                    lPakietOpóźN++;
                }
            }
        }

        public void UstawCzasSymulacji(Double _czas)
        {
            czas = _czas;
        }

        public void Statystyki()
        {
            Console.WriteLine("Utwożone pakiety bez priorytetu {0} Śr. opóźnienie normalne: {1} [ms]", lPakietNN, OpóźnN / lPakietN);

            if (lPakietNP != 0)
            {
                Console.WriteLine("Utwożone pakiety priorytetowe {0} Śr.opoźnienie priorytetowe: {1} [ms]", lPakietNP, OpóźnP / lPakietP);
            }

            Console.WriteLine("Przesłane pakiety: {0}, w tym opóźnione: {1}", lPakietP + lPakietN, lPakietOpóźN);
            Console.WriteLine("Blokowanie: Normalne: {0}, Priorytetowe: {1}, Wszystkie: {2}", lPakietOpóźN / lPakietN, lPakietOpóźP / lPakietP, (lPakietOpóźN + lPakietOpóźP) / (lPakietP + lPakietN));
            Console.WriteLine("Natężenie ruchu: {0}", 100 * (lPakietNN + lPakietNP) / czas);
        }
    }

    class DokladnyArbiter : IArbiter
    {
        List<Double> opNormaL = new List<Double>();
        List<Double> opPriorL = new List<Double>();

        Double lPakietNN, lPakietNP, lPakietN, lPakietOpóźN, lPakietOpóźP, op, OpóźnN, OpóźnP, lPakietP, czas;

        public DokladnyArbiter()
        {
            lPakietNN = 0;
            lPakietNP = 0;
            lPakietN = 0;
            lPakietOpóźN = 0;
            lPakietOpóźP = 0;
            op = 0;
            OpóźnN = 0;
            OpóźnP = 0;
            lPakietP = 0;
            czas = 0;
        }

        public void DodajPakietNowy(Pakiet pakiet)
        {
            if (pakiet.priorytet)
            {
                lPakietNP++;
            }
            else
            {
                lPakietNN++;
            }
        }

        public void DodajPakietPrzesłany(Pakiet pakiet)
        {
            op = pakiet.tTransmisji - pakiet.tPowstania;

            if (pakiet.priorytet)
            {
                opPriorL.Add(op);

                lPakietP++;
                OpóźnP += op;

                if (op > 0)
                {
                    lPakietOpóźP++;
                }
            }
            else
            {
                opNormaL.Add(op);

                lPakietN++;
                OpóźnN += op;

                if (op > 0)
                {
                    lPakietOpóźN++;
                }
            }

        }

        public void UstawCzasSymulacji(Double _czas)
        {
            czas = _czas;
        }

        public void Statystyki()
        {
            Console.WriteLine("Utwożone pakiety bez priorytetu {0} Śr. opóźnienie normalne: {1} [ms]", lPakietNN, OpóźnN / lPakietN);

            if (lPakietNP != 0)
            {
                Console.WriteLine("Utwożone pakiety priorytetowe {0} Śr.opoźnienie priorytetowe: {1} [ms]", lPakietNP, OpóźnP / lPakietP);
            }

            Console.WriteLine("Przesłane pakiety: {0}, w tym opóźnione: {1}", lPakietP + lPakietN, lPakietOpóźN);
            Console.WriteLine("Blokowanie: Normalne: {0}, Priorytetowe: {1}, Wszystkie: {2}", lPakietOpóźN / lPakietN, lPakietOpóźP / lPakietP, (lPakietOpóźN + lPakietOpóźP) / (lPakietP + lPakietN));
            Console.WriteLine("Natężenie ruchu: {0}", 100 * (lPakietNN + lPakietNP) / czas);

            Console.Write("Aby zapisać dane do pliku podaj jego nazwę: ");
            string nazwa = Console.ReadLine();

            if (nazwa != "")
            {

                opNormaL.Sort();
                opPriorL.Sort();

                int długość, ostatniP = 0, ostatniN = 0;

                if (opPriorL.Any())
                {
                    ostatniP = Convert.ToInt32(opPriorL.Last());
                }

                if (opNormaL.Any())
                {
                    ostatniN = Convert.ToInt32(opNormaL.Last());
                }

                if (ostatniN > ostatniP)
                {
                    długość = ostatniN + 1;
                }
                else
                {
                    długość = ostatniP + 1;
                }

                int[,] listaOpN = new int[długość, 2];

                foreach (var opz in opNormaL)
                {

                    listaOpN[Convert.ToInt32(opz), 0] += 1;
                }

                foreach (var opz in opPriorL)
                {

                    listaOpN[Convert.ToInt32(opz), 1] += 1;
                }

                Console.OutputEncoding = System.Text.Encoding.UTF8;

                    Console.WriteLine("Zapisywanie danych do pliku");
                    System.IO.StreamWriter plik = new System.IO.StreamWriter(@"" + nazwa + ".txt");

                    plik.WriteLine("Utw. normalne\tUtw. priorytet\tPrzesłane pakiety norm\tPrz. p. prio.\tOpoznione\tBlokowanie\tB. N\tB. P\tSr. op. norm\tSr. Op. prio\tNat. ruchu");

                    plik.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}", lPakietNN, lPakietNP, lPakietN, lPakietP, lPakietOpóźN, (lPakietOpóźN + lPakietOpóźP) / (lPakietN + lPakietP), lPakietOpóźN / lPakietN, lPakietOpóźP / lPakietP, OpóźnN / lPakietN, OpóźnP / lPakietP, 100 * (lPakietNN + lPakietNP) / czas);

                    plik.WriteLine("Opoznienie\tL. pakietów norm\tL. pakietów prio");

                for (int i = 0; i < długość; i++)
                {
                    plik.WriteLine("{0}\t{1}\t{2}", i, listaOpN[i, 0], listaOpN[i, 1]);
                }

                    plik.Close();

                    Console.WriteLine("Zapisano dane do pliku");
                

            }
            return;
        }


    }

}