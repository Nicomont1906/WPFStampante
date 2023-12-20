# WPFStampante
***
Classe "Stampante" 
- 4 serbatoi di colore: CMYB e un cassetto di fogli (tutte property int)
- un metodo bool Stampa( Pagina p ) (che torna false, se l'inchiostro non è sufficiente per stampare)
- un metodo int StatoInchiostro( Colore c ) che torna la quantità di inchiostro presente nei 4 serbatoi.
- un metodo int StatoCarta() che mi ritorna la quantità di fogli nel cassetto
- un metodo void SostituisciColore( Colore c ) che rimpiazza un serbatoio di inchiostro e lo riporta a 100
- un metodo void AggiungiCarta( int qta ) che aggiunge fogli fino ad un max di 200

Classe "Pagina" 
- 4 attributi CMYB che, se usata per stampare, consuma i serbatoi della stampante.
- un costruttore che accetta colori specifici al massimo di valore 3
- un costruttore che crea una Pagina con colori random

Enum Colore
***

All'interno del progetto andiamo a creare una Cartella Models nel quale andremo ad inserire le due classi.

Classe Stampante:
 ```
public Stampante()
        {
            C = M = Y = B = 100;
            Fogli = 200;
        }
```
Costruttore che inzializza colori e fogli 

```
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
```
Metodo che ci permette di riportare i colori a percentuale 100%

```
public enum Colore
        {
            C,
            M,
            Y,
            B
        }
```
Enum dei colori


Classe Pagina:
```
    public Pagina()
        {
            Random random = new Random();

            C = random.Next(4);
            M = random.Next(4);
            Y = random.Next(4);
            B = random.Next(4);
        }
```
Costruttore che ci permette di creare una pagina random 

MainWindowXaml.cs 

```
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
```
metodo che ci permette di stampare una pagina casuale  e ci viene mostrato a schermo se la stampa è riuscita oppure no.


```
private void RiempiSerbatoiButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (Stampante.Colore colore in Enum.GetValues(typeof(Stampante.Colore)))
            {
                stampante.SostituisciColore(colore);
            }

            risultatoTextBlock.Text = "Tutti i serbatoi sono stati riempiti.";
            AggiornaUI();
        }
```
metodo che ci permette di ricaricare tutti i serbatoi immediatamente.

```
 private void CaricaMagentaButtonClick(object sender, RoutedEventArgs e)
        {
            stampante.SostituisciColore(Stampante.Colore.M);
            risultatoTextBlock.Text = "Il serbatoio di inchiostro magenta è stato caricato.";
            contatoreMagentaTextBlock.Text = "100%";
        }
```
Metodo che carica solamente il serbatoi del Ciano. Facciamo così per tutti gli altri serbatoi. 

Infine andiamo a creare una Stampante Persistente che mantiene i valori continuamente salvati che vengono presi da un file csv:
```
public MainWindow()
        {
            InitializeComponent();

            stampante = new Stampante();

            try
            {
                StreamReader reader = new StreamReader("C:\\Users\\studente.ITTSBELLUZZIDAV\\Desktop\\WPFStampante-main\\montaspro.nicolo.4i.Stampante\\Models\\StampantePersistente.csv");
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
```
