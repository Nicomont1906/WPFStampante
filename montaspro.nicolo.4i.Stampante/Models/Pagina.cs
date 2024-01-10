using System;

namespace montaspro.nicolo._4i.Stampante1.Models
{
    public class Pagina
    {
        public int Ciano { get; private set; }
        public int Magenta { get; private set; }
        public int Giallo { get; private set; }
        public int Nero { get; private set; }

        public Pagina(int ciano, int magenta, int giallo, int nero)
        {
            if (IsValidColor(ciano) && IsValidColor(magenta) && IsValidColor(giallo) && IsValidColor(nero))
            {
                Ciano = ciano;
                Magenta = magenta;
                Giallo = giallo;
                Nero = nero;
            }
        }

        public Pagina()
        {
            Random random = new Random();

            Ciano = random.Next(4);
            Magenta = random.Next(4);
            Giallo = random.Next(4);
            Nero = random.Next(4);
        }

        private bool IsValidColor(int value)
        {
            return value >= 0 && value <= 3;
        }
    }
}
