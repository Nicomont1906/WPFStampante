using System;

namespace montaspro.nicolo._4i.Stampante1.Models
{
    public class Pagina
    {
        public int C { get; private set; }
        public int M { get; private set; }
        public int Y { get; private set; }
        public int B { get; private set; }

        public Pagina(int c, int m, int y, int b)
        {
            if (IsValidColor(c) && IsValidColor(m) && IsValidColor(y) && IsValidColor(b))
            {
                C = c;
                M = m;
                Y = y;
                B = b;
            }
        }

        public Pagina()
        {
            Random random = new Random();

            C = random.Next(4);
            M = random.Next(4);
            Y = random.Next(4);
            B = random.Next(4);
        }

        private bool IsValidColor(int value)
        {
            return value >= 0 && value <= 3;
        }
    }
}
