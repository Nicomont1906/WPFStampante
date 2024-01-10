using System;
using System.IO;
using System.Windows;

namespace montaspro.nicolo._4i.Stampante1
{
    public partial class MainWindow : Window
    {
        private Models.Stampante stampante;

        public MainWindow()
        {
            InitializeComponent();
            stampante = new Models.Stampante();

            try
            {
                StreamReader lettore = new StreamReader("C:\\Users\\studente.ITTSBELLUZZIDAV\\Desktop\\montaspro.nicolo.4i.Stampante\\Models\\StampantePersistente.csv");
                string riga = lettore.ReadLine();

                if (riga != null && riga.Split(';').Length >= 5)
                {
                    int.TryParse(riga.Split(';')[0], out int ciano);
                    int.TryParse(riga.Split(';')[1], out int magenta);
                    int.TryParse(riga.Split(';')[2], out int giallo);
                    int.TryParse(riga.Split(';')[3], out int nero);
                    int.TryParse(riga.Split(';')[4], out int fogli);

                    stampante.Ciano = ciano <= 0 ? 100 : ciano;
                    stampante.Magenta = magenta;
                    stampante.Giallo = giallo;
                    stampante.Nero = nero;
                    stampante.Fogli = fogli;

                    AggiornaUI();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante la lettura del file: " + ex.Message);
            }

            caricaCianoButton.Click += CaricaCianoButtonClick;
            caricaMagentaButton.Click += CaricaMagentaButtonClick;
            caricaGialloButton.Click += CaricaGialloButtonClick;
            caricaNeroButton.Click += CaricaNeroButtonClick;
            AggiungiCartaButton.Click += AggiungiCartaButtonClick;
            riempiSerbatoiButton.Click += RiempiSerbatoiButtonClick;
            stampaPaginaCasualeButton.Click += StampaPaginaCasualeButtonClick;
        }

        private void AggiornaUI()
        {
            contatoreCianoTextBlock.Text = $"{stampante.Ciano}%";
            contatoreMagentaTextBlock.Text = $"{stampante.Magenta}%";
            contatoreGialloTextBlock.Text = $"{stampante.Giallo}%";
            contatoreNeroTextBlock.Text = $"{stampante.Nero}%";
            contatoreFogliTextBlock.Text = $"{stampante.Fogli}/200";
        }

        private void StampaPaginaCasualeButtonClick(object sender, RoutedEventArgs e)
        {
            Models.Pagina pagina = new Models.Pagina();
            bool stampaRiuscita = stampante.Stampa(pagina);

            risultatoTextBlock.Text = stampaRiuscita ? "Stampa riuscita!" : "Stampa non riuscita: inchiostro insufficiente o carta esaurita.";

            if (stampaRiuscita)
            {
                AggiornaUI();
            }

            SalvaSuFile();
        }

        private void AggiungiCartaButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int numeroFogli = int.Parse(txtNumeroFogli.Text);

                if (stampante.Fogli + numeroFogli > 200)
                {
                    MessageBox.Show("Non puoi aggiungere fogli. Il limite di 200 fogli è stato raggiunto.", "Avviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    stampante.AggiungiCarta(numeroFogli);
                    AggiornaUI();
                    MessageBox.Show($"Hai aggiunto {numeroFogli} fogli. Totale fogli: {stampante.Fogli}.", "Informazione");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Inserisci un numero valido.", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            SalvaSuFile();
        }

        private void RiempiSerbatoiButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (Models.Stampante.ColoreStampante colore in Enum.GetValues(typeof(Models.Stampante.ColoreStampante)))
            {
                stampante.SostituisciColore(colore);
            }

            risultatoTextBlock.Text = "Tutti i serbatoi sono stati riempiti.";
            AggiornaUI();
            SalvaSuFile();
        }

        private void CaricaMagentaButtonClick(object sender, RoutedEventArgs e)
        {
            stampante.SostituisciColore(Models.Stampante.ColoreStampante.Magenta);
            risultatoTextBlock.Text = "Il serbatoio di inchiostro magenta è stato caricato.";
            contatoreMagentaTextBlock.Text = "100%";
            SalvaSuFile();
        }

        private void CaricaCianoButtonClick(object sender, RoutedEventArgs e)
        {
            stampante.SostituisciColore(Models.Stampante.ColoreStampante.Ciano);
            risultatoTextBlock.Text = "Il serbatoio di inchiostro ciano è stato caricato.";
            contatoreCianoTextBlock.Text = "100%";
            SalvaSuFile();
        }

        private void CaricaNeroButtonClick(object sender, RoutedEventArgs e)
        {
            stampante.SostituisciColore(Models.Stampante.ColoreStampante.Nero);
            risultatoTextBlock.Text = "Il serbatoio di inchiostro nero è stato caricato.";
            contatoreNeroTextBlock.Text = "100%";
            SalvaSuFile();
        }

        private void CaricaGialloButtonClick(object sender, RoutedEventArgs e)
        {
            stampante.SostituisciColore(Models.Stampante.ColoreStampante.Giallo);
            risultatoTextBlock.Text = "Il serbatoio di inchiostro giallo è stato caricato.";
            contatoreGialloTextBlock.Text = "100%";
            SalvaSuFile();
        }

        private void SalvaSuFile()
        {
            try
            {
                using (StreamWriter wr = new StreamWriter("C:\\Users\\studente.ITTSBELLUZZIDAV\\Desktop\\montaspro.nicolo.4i.Stampante\\Models\\StampantePersistente.csv", false))
                {
                    wr.WriteLine($"{stampante.Ciano};{stampante.Magenta};{stampante.Giallo};{stampante.Nero};{stampante.Fogli}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il salvataggio su file: " + ex.Message);
            }
        }
    }
}
