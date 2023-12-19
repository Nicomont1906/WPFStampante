using montaspro.nicolo._4i.Stampante1.Models;
using System;
using System.IO;
using System.Windows;

namespace montaspro.nicolo._4i.Stampante1
{
    public partial class MainWindow : Window
    {
        private Stampante stampante;

        public MainWindow()
        {
            InitializeComponent();

            stampante = new Stampante();

            try
            {
                StreamReader reader = new StreamReader("C:\\Users\\eneam\\Desktop\\montaspro.nicolo.4i.Stampante\\Models\\StampantePersistente.csv");
                string line = reader.ReadLine();

                if (line != null && line.Split(';').Length >= 5)
                {
                    int.TryParse(line.Split(';')[0], out int ciano);
                    int.TryParse(line.Split(';')[1], out int magenta);
                    int.TryParse(line.Split(';')[2], out int giallo);
                    int.TryParse(line.Split(';')[3], out int nero);
                    int.TryParse(line.Split(';')[4], out int fogli);

                    stampante.C = ciano <= 0 ? 100 : ciano;
                    stampante.M = magenta;
                    stampante.Y = giallo;
                    stampante.B = nero;
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
            contatoreCianoTextBlock.Text = $"{stampante.C}%";
            contatoreMagentaTextBlock.Text = $"{stampante.M}%";
            contatoreGialloTextBlock.Text = $"{stampante.Y}%";
            contatoreNeroTextBlock.Text = $"{stampante.B}%";
            contatoreFogliTextBlock.Text = $"{stampante.Fogli}/200";
        }

        private void StampaPaginaCasualeButtonClick(object sender, RoutedEventArgs e)
        {
            Pagina pagina = new Pagina();
            bool stampaRiuscita = stampante.Stampa(pagina);

            risultatoTextBlock.Text = stampaRiuscita ? "Stampa riuscita!" : "Stampa non riuscita: inchiostro insufficiente o carta esaurita.";

            if (stampaRiuscita)
            {
                AggiornaUI();
            }
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
        }

        private void RiempiSerbatoiButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (Stampante.Colore colore in Enum.GetValues(typeof(Stampante.Colore)))
            {
                stampante.SostituisciColore(colore);
            }

            risultatoTextBlock.Text = "Tutti i serbatoi sono stati riempiti.";
            AggiornaUI();
        }



        private void CaricaMagentaButtonClick(object sender, RoutedEventArgs e)
        {
            stampante.SostituisciColore(Stampante.Colore.M);
            risultatoTextBlock.Text = "Il serbatoio di inchiostro magenta è stato caricato.";
            contatoreMagentaTextBlock.Text = "100%";
        }
        private void CaricaCianoButtonClick(object sender, RoutedEventArgs e)
        {
            stampante.SostituisciColore(Stampante.Colore.C);
            risultatoTextBlock.Text = "Il serbatoio di inchiostro ciano è stato caricato.";
            contatoreCianoTextBlock.Text = "100%";
        }

      
        private void CaricaNeroButtonClick(object sender, RoutedEventArgs e)
        {
            stampante.SostituisciColore(Stampante.Colore.B);
            risultatoTextBlock.Text = "Il serbatoio di inchiostro nero è stato caricato.";
            contatoreNeroTextBlock.Text = "100%";
        }
        private void CaricaGialloButtonClick(object sender, RoutedEventArgs e)
        {
            stampante.SostituisciColore(Stampante.Colore.Y);
            risultatoTextBlock.Text = "Il serbatoio di inchiostro giallo è stato caricato.";
            contatoreGialloTextBlock.Text = "100%";
        }

    }
}
