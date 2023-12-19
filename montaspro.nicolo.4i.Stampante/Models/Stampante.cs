using System;
using System.IO;
using System.Windows;

namespace montaspro.nicolo._4i.Stampante1.Models
{
    public class Stampante
    {
        public int C { get; set; }
        public int M { get; set; }
        public int Y { get; set; }
        public int B { get; set; }
        public int Fogli { get; set; }

        public Stampante()
        {
            C = M = Y = B = 100;
            Fogli = 200;
        }

        public bool Stampa(Pagina p)
        {
            if (Fogli > 0 &&
                C >= p.C &&
                M >= p.M &&
                Y >= p.Y &&
                B >= p.B)
            {
                C -= p.C;
                M -= p.M;
                Y -= p.Y;
                B -= p.B;
                Fogli--;

                return true;
            }

            return false;
        }

        public void SostituisciColore(Colore c)
        {
            switch (c)
            {
                case Colore.C:
                    C = 100;
                    break;
                case Colore.M:
                    M = 100;
                    break;
                case Colore.Y:
                    Y = 100;
                    break;
                case Colore.B:
                    B = 100;
                    break;
            }
        }

        public void AggiungiCarta(int qta)
        {
            if (qta > 0)
            {
                if (Fogli + qta <= 200)
                {
                    Fogli += qta;
                }
                else
                {
                    Fogli = 200;
                }
            }
        }

        public enum Colore
        {
            C,
            M,
            Y,
            B
        }
    }
}
