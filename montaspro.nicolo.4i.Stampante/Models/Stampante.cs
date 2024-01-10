using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace montaspro.nicolo._4i.Stampante1.Models
{
    public class Stampante
    {
        public int Ciano { get; set; }
        public int Magenta { get; set; }
        public int Giallo { get; set; }
        public int Nero { get; set; }
        public int Fogli { get; set; }

        public Stampante()
        {
            Ciano = Magenta = Giallo = Nero = 100;
            Fogli = 200;
        }

        public bool Stampa(Pagina pagina)
        {
            if (Fogli > 0 &&
                Ciano >= pagina.Ciano &&
                Magenta >= pagina.Magenta &&
                Giallo >= pagina.Giallo &&
                Nero >= pagina.Nero)
            {
                Ciano -= pagina.Ciano;
                Magenta -= pagina.Magenta;
                Giallo -= pagina.Giallo;
                Nero -= pagina.Nero;
                Fogli--;

                return true;
            }

            return false;
        }

        public void SostituisciColore(ColoreStampante colore)
        {
            switch (colore)
            {
                case ColoreStampante.Ciano:
                    Ciano = 100;
                    break;
                case ColoreStampante.Magenta:
                    Magenta = 100;
                    break;
                case ColoreStampante.Giallo:
                    Giallo = 100;
                    break;
                case ColoreStampante.Nero:
                    Nero = 100;
                    break;
            }
        }

        public void AggiungiCarta(int quantita)
        {
            if (quantita > 0)
            {
                if (Fogli + quantita <= 200)
                {
                    Fogli += quantita;
                }
                else
                {
                    Fogli = 200;
                }
            }
        }

        public enum ColoreStampante
        {
            Ciano,
            Magenta,
            Giallo,
            Nero
        }
    }
}
