using System;
using System.Collections.Generic;
using System.Linq;

/*
 * TEMPLATE ESAME C# - NEGOZIO ONLINE
 *
 * Regola scelta per il template:
 * - i metodi di visualizzazione sono già implementati, così lo studente può concentrarsi
 *   sulle operazioni richieste dalla traccia.
 * - i metodi operazionali contengono TODO guidati: lo studente deve completarli senza
 *   modificare firma, nome, parametri o tipo di ritorno.
 *
 * Vincolo richiesto: tutto il codice è in un unico file .cs e senza namespace.
 */

public class Program
{
    public static void Main()
    {
        // Punto di ingresso della Console App.
        ApplicazioneNegozio applicazione = new ApplicazioneNegozio();
        // applicazione.Avvia();
        TestNegozioOnline.EseguiTuttiITest();
    }
}

public class ApplicazioneNegozio
{
    private readonly CatalogoProdotti catalogoProdotti;
    private readonly CarrelloUtente carrelloUtente;
    private readonly StoricoAcquisti storicoAcquisti;
    private readonly ServizioNegozio servizioNegozio;

    public ApplicazioneNegozio()
    {
        catalogoProdotti = new CatalogoProdotti();
        carrelloUtente = new CarrelloUtente();
        storicoAcquisti = new StoricoAcquisti();
        servizioNegozio = new ServizioNegozio(catalogoProdotti, carrelloUtente, storicoAcquisti);

        CaricaDatiIniziali();
    }

   public void Avvia()
   {
        Console.WriteLine("=== BENVENUTO NEL NEGOZIO ONLINE ===");
            
        bool esci = false;
        while (!esci)
        {
            Console.WriteLine("\n--- MENU PRINCIPALE ---");
            
            string scelta = ScegliRuolo();
            if (scelta == "utente")
            {
                // Console.WriteLine("\n--- Hai selezionato il ruolo di utente ---");
                GestisciMenuUtente();
            }
            else if (scelta == "amministratore")
            {
                // Console.WriteLine("\n--- Hai selezionato il ruolo di amministratore ---");
                GestisciMenuAmministratore();
            }
            else if (scelta == "esci")
            {
                esci = true;
                Console.WriteLine("\nGrazie per aver utilizzato il negozio online. Arrivederci!");
            }
            else
            {
                Console.WriteLine("Scelta non valida. Riprova.");
            }
        }
    }

    private void CaricaDatiIniziali()
    {
        // Metodo già implementato: fornisce prodotti di partenza per testare subito il sistema.
        catalogoProdotti.AggiungiProdotto(new Prodotto("P001", "Tastiera meccanica", 79.90m, 10));
        catalogoProdotti.AggiungiProdotto(new Prodotto("P002", "Mouse wireless", 24.50m, 25));
        catalogoProdotti.AggiungiProdotto(new Prodotto("P003", "Monitor 24 pollici", 149.99m, 7));
        catalogoProdotti.AggiungiProdotto(new Prodotto("P004", "Cavo USB-C", 9.99m, 40));
    }

    private string ScegliRuolo()
    {
        while (true)
        {
            Console.Write("\nScegli ruolo (utente/amministratore/esci): ");
            string? input = Console.ReadLine();
            
           
            if (!string.IsNullOrWhiteSpace(input))
            {
                string sceltaFormattata = input.Trim().ToLower();
                
                if (sceltaFormattata == "utente" || sceltaFormattata == "amministratore" || sceltaFormattata == "esci")
                {
                    return sceltaFormattata;
                }
            }
            
            Console.WriteLine("Opzione non valida. Inserisci esattamente 'utente', 'amministratore' o 'esci'.");
        }
    }

    private void GestisciMenuUtente()
    {
        // TODO: implementare il menu utente.
        // Operazioni richieste dalla traccia:
        // - visualizzare catalogo;
        // - aggiungere prodotto al carrello;
        // - visualizzare carrello;
        // - modificare quantità nel carrello;
        // - rimuovere prodotto dal carrello;
        // - svuotare carrello;
        // - confermare acquisto;
        // - visualizzare storico acquisti dell'utente.

        Console.WriteLine("=== MENU UTENTE ===");

        Utente? utente = null;
        while (utente == null)
        {
            Console.Write("Inserisci il tuo nome utente: ");
            string? username = Console.ReadLine();
            try
            {
                utente = new Utente(username);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Nome utente non valido. Riprova.");
            }
        }

        Console.WriteLine($"Benvenuto, {utente.Nome}!");

        bool tornaAlMenu = false;
        while (!tornaAlMenu){
            Console.WriteLine("\n--- Quale operazione vuoi eseguire? ---");
            Console.WriteLine("1. Visualizza catalogo");
            Console.WriteLine("2. Aggiungi prodotto al carrello");
            Console.WriteLine("3. Visualizza carrello");
            Console.WriteLine("4. Modifica quantità nel carrello");
            Console.WriteLine("5. Rimuovi prodotto dal carrello");
            Console.WriteLine("6. Svuota carrello");
            Console.WriteLine("7. Conferma acquisto");
            Console.WriteLine("8. Visualizza storico acquisti");
            Console.WriteLine("0. Torna al menu principale");
            Console.Write("Scelta: ");

            string? scelta = Console.ReadLine();
            switch (scelta)
            {
                case "1":
                    MostraCatalogo();
                    break;
                case "2":
                    Console.Write("Inserisci codice prodotto da aggiungere: ");
                    string? codAdd = Console.ReadLine();
                    int qtaAdd = LeggiInteroPositivo("Inserisci quantità: ");
                    if (!string.IsNullOrWhiteSpace(codAdd) && servizioNegozio.AggiungiProdottoAlCarrello(codAdd.Trim(), qtaAdd))
                    {
                        Console.WriteLine("Prodotto aggiunto al carrello!");
                    }
                    else
                    {
                        Console.WriteLine("Errore: prodotto non trovato o quantità non disponibile/non valida.");
                    }
                    break;
                case "3":
                    MostraCarrello();
                    break;
                case "4":
                    Console.Write("Inserisci codice prodotto da modificare nel carrello: ");
                    string? codMod = Console.ReadLine();
                    int qtaMod = LeggiInteroPositivo("Inserisci nuova quantità: ");
                    if (!string.IsNullOrWhiteSpace(codMod) && carrelloUtente.ModificaQuantitaNelCarrello(codMod.Trim(), qtaMod))
                    {
                        Console.WriteLine("Quantità aggiornata!");
                    }
                    else
                    {
                        Console.WriteLine("Errore: prodotto non presente nel carrello o disponibilità insufficiente.");
                    }
                    break;
                case "5":
                    Console.Write("Inserisci codice prodotto da rimuovere dal carrello: ");
                    string? codRem = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(codRem) && carrelloUtente.RimuoviDalCarrello(codRem.Trim()))
                    {
                        Console.WriteLine("Prodotto rimosso dal carrello.");
                    }
                    else
                    {
                        Console.WriteLine("Prodotto non trovato nel carrello.");
                    }
                    break;
                case "6":
                    carrelloUtente.SvuotaCarrello();
                    Console.WriteLine("Carrello svuotato.");
                    break;
                case "7":
                    try
                    {
                        Acquisto acq = servizioNegozio.ConfermaAcquisto(utente);
                        Console.WriteLine("\nAcquisto effettuato con successo!");
                        servizioNegozio.StampaAcquisto(acq);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Errore durante l'acquisto: " + ex.Message);
                    }
                    break;
                case "8":
                    MostraStoricoUtente();
                    break;
                case "0":
                    tornaAlMenu = true;
                    break;
                default:
                    Console.WriteLine("Scelta non valida. Riprova.");
                    break;
            }
        }
        // throw new NotImplementedException("Completare il metodo GestisciMenuUtente.");
    }

    private void GestisciMenuAmministratore()
    {
        Console.Clear();
        Console.WriteLine("=== AREA AMMINISTRATORE ===");

        bool tornaAlMenuPrincipale = false;
        while (!tornaAlMenuPrincipale)
        {
            Console.WriteLine("\n--- PANNELLO DI CONTROLLO ADMIN ---");
            Console.WriteLine("1. Visualizza catalogo completo");
            Console.WriteLine("2. Aggiungi nuovo prodotto al catalogo");
            Console.WriteLine("3. Elimina prodotto dal catalogo");
            Console.WriteLine("4. Modifica prezzo di un prodotto");
            Console.WriteLine("5. Modifica quantità disponibile (Incolla/Preleva magazzino)");
            Console.WriteLine("6. Visualizza tutti gli acquisti effettuati (Storico globale)");
            Console.WriteLine("7. Visualizza report vendite (Qta Iniziale/Venduta/Disponibile)");
            Console.WriteLine("0. Torna al menu principale");
            Console.Write("Scelta: ");

            string? scelta = Console.ReadLine()?.Trim();

            switch (scelta)
            {
                case "1":
                    MostraCatalogo();
                    break;
                case "2":
                    Console.Write("Inserisci codice prodotto: ");
                    string? codice = Console.ReadLine();
                    Console.Write("Inserisci nome prodotto: ");
                    string? nome = Console.ReadLine();
                    decimal prezzo = LeggiPrezzoPositivo("Inserisci prezzo (es. 19.99): ");
                    int quantita = LeggiInteroPositivo("Inserisci quantità disponibile: ");

                    if (string.IsNullOrWhiteSpace(codice) || string.IsNullOrWhiteSpace(nome))
                    {
                        Console.WriteLine("Codice o nome prodotto non valido.");
                        break;
                    }

                    try
                    {
                        catalogoProdotti.AggiungiProdotto(new Prodotto(codice.Trim(), nome.Trim(), prezzo, quantita));
                        Console.WriteLine("Prodotto aggiunto al catalogo.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Impossibile aggiungere il prodotto: " + ex.Message);
                    }
                    break;
                case "3":
                    Console.Write("Inserisci codice prodotto da eliminare dal catalogo: ");
                    string? codDel = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(codDel) && catalogoProdotti.EliminaProdotto(codDel.Trim()))
                    {
                        Console.WriteLine("Prodotto eliminato correttamente.");
                    }
                    else
                    {
                        Console.WriteLine("Impossibile trovare il prodotto specificato.");
                    }
                    break;
                case "4":
                    Console.Write("Inserisci codice prodotto: ");
                    string? codPrice = Console.ReadLine();
                    decimal nuovoPrezzo = LeggiPrezzoPositivo("Inserisci il nuovo prezzo: ");
                    if (!string.IsNullOrWhiteSpace(codPrice) && catalogoProdotti.ModificaPrezzoProdotto(codPrice.Trim(), nuovoPrezzo))
                    {
                        Console.WriteLine("Prezzo modificato con successo.");
                    }
                    else
                    {
                        Console.WriteLine("Errore: prodotto non trovato o prezzo non valido.");
                    }
                    break;
                case "5":
                    Console.Write("Inserisci codice prodotto: ");
                    string? codStock = Console.ReadLine();
                    Console.Write("Inserisci variazione di magazzino (es. 5 per aggiungere, -3 per prelevare): ");
                    if (int.TryParse(Console.ReadLine(), out int variazione))
                    {
                        if (!string.IsNullOrWhiteSpace(codStock) && catalogoProdotti.ModificaQuantitaProdotto(codStock.Trim(), variazione))
                        {
                            Console.WriteLine("Magazzino aggiornato con successo.");
                        }
                        else
                        {
                            Console.WriteLine("Errore: prodotto non trovato o l'operazione porterebbe il magazzino sotto zero.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Valore numerico non valido.");
                    }
                    break;
                case "6":
                    List<Acquisto> tuttiGliAcquisti = storicoAcquisti.OttieniTuttiGliAcquisti();
                    if (tuttiGliAcquisti.Count == 0)
                    {
                        Console.WriteLine("Nessun acquisto registrato.");
                    }
                    else
                    {
                        Console.WriteLine("=== TUTTI GLI ACQUISTI ===");
                        foreach (Acquisto a in tuttiGliAcquisti)
                        {
                            servizioNegozio.StampaAcquisto(a);
                        }
                    }
                    break;
                case "7":
                    servizioNegozio.StampaReportProdotti();
                    break;
                case "0":
                    tornaAlMenuPrincipale = true;
                    break;
                default:
                    Console.WriteLine("Scelta non valida. Riprova.");
                    break;
            }
        }
    }

    private void MostraCatalogo()
    {
        // Metodo già implementato: mostra a video tutti i prodotti del catalogo.
        List<Prodotto> prodotti = catalogoProdotti.OttieniTuttiIProdotti();

        Console.WriteLine();
        Console.WriteLine("=== CATALOGO PRODOTTI ===");

        if (prodotti.Count == 0)
        {
            Console.WriteLine("Il catalogo è vuoto.");
            return;
        }

        foreach (Prodotto prodotto in prodotti)
        {
            Console.WriteLine(
                prodotto.CodiceProdotto + " - " +
                prodotto.Nome + " - " +
                prodotto.Prezzo.ToString("0.00") + " euro - " +
                "Disponibili: " + prodotto.QuantitaDisponibile);
        }
    }

    private void MostraCarrello()
    {
        // Metodo già implementato: mostra contenuto del carrello e totale corrente.
        List<ElementoCarrello> elementi = carrelloUtente.OttieniElementi();

        Console.WriteLine();
        Console.WriteLine("=== CARRELLO ===");

        if (elementi.Count == 0)
        {
            Console.WriteLine("Il carrello è vuoto.");
            return;
        }

        foreach (ElementoCarrello elemento in elementi)
        {
            Console.WriteLine(
                elemento.ProdottoSelezionato.CodiceProdotto + " - " +
                elemento.ProdottoSelezionato.Nome + " - " +
                "Quantità: " + elemento.QuantitaScelta + " - " +
                "Prezzo unitario: " + elemento.PrezzoUnitario.ToString("0.00") + " euro - " +
                "Parziale: " + elemento.CalcolaTotaleParziale().ToString("0.00") + " euro");
        }

        Console.WriteLine("Totale carrello: " + carrelloUtente.CalcolaTotale().ToString("0.00") + " euro");
    }

    private void MostraStoricoUtente()
    {
        // Metodo già implementato: chiede un nome e mostra gli acquisti collegati.
        Console.Write("Inserisci nome utente: ");
        string? nomeUtente = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nomeUtente))
        {
            Console.WriteLine("Nome utente non valido.");
            return;
        }

        List<Acquisto> acquistiUtente = storicoAcquisti.OttieniAcquistiPerUtente(nomeUtente);

        Console.WriteLine();
        Console.WriteLine("=== STORICO ACQUISTI DI " + nomeUtente.Trim() + " ===");

        if (acquistiUtente.Count == 0)
        {
            Console.WriteLine("Nessun acquisto trovato per questo utente.");
            return;
        }

        foreach (Acquisto acquisto in acquistiUtente)
        {
            servizioNegozio.StampaAcquisto(acquisto);
        }
    }

    private int LeggiInteroPositivo(string messaggio)
    {
        // Legge da console un numero intero positivo > 0.
        while (true)
        {
            Console.Write(messaggio);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int valore) && valore > 0)
            {
                return valore;
            }

            Console.WriteLine("Inserisci un numero intero positivo valido.");
        }
    }

    private decimal LeggiPrezzoPositivo(string messaggio)
    {
        while (true)
        {
            Console.Write(messaggio);
            string? input = Console.ReadLine();

            if (decimal.TryParse(input, out decimal valore) && valore > 0m)
            {
                return valore;
            }

            Console.WriteLine("Inserisci un prezzo positivo valido (maggiore di 0).");
        }
    }
}

public interface IGestioneCatalogo
{
    void AggiungiProdotto(Prodotto prodotto);
    bool EliminaProdotto(string codiceProdotto);
    Prodotto? CercaProdottoPerCodice(string codiceProdotto);
    List<Prodotto> OttieniTuttiIProdotti();
    bool ModificaPrezzoProdotto(string codiceProdotto, decimal nuovoPrezzo);
    bool ModificaQuantitaProdotto(string codiceProdotto, int variazioneQuantita);
}

public interface IGestioneCarrello
{
    bool AggiungiAlCarrello(Prodotto prodotto, int quantita);
    bool ModificaQuantitaNelCarrello(string codiceProdotto, int nuovaQuantita);
    bool RimuoviDalCarrello(string codiceProdotto);
    void SvuotaCarrello();
    decimal CalcolaTotale();
    List<ElementoCarrello> OttieniElementi();
}

public interface IGestioneAcquisti
{
    void RegistraAcquisto(Acquisto acquisto);
    List<Acquisto> OttieniTuttiGliAcquisti();
    List<Acquisto> OttieniAcquistiPerUtente(string nomeUtente);
}

public class Utente
{
    public string Nome { get; private set; }

    public Utente(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("Il nome utente non può essere vuoto.");
        }

        Nome = nome.Trim();
    }
}

public class Prodotto
{
    public string CodiceProdotto { get; private set; }
    public string Nome { get; private set; }
    public decimal Prezzo { get; private set; }
    public int QuantitaDisponibile { get; private set; }
    public int QuantitaIniziale { get; private set; }

    public Prodotto(string codiceProdotto, string nome, decimal prezzo, int quantitaDisponibile)
    {
        CodiceProdotto = codiceProdotto;
        Nome = nome;
        Prezzo = prezzo;
        QuantitaDisponibile = quantitaDisponibile;
        QuantitaIniziale = quantitaDisponibile;
    }

    public void CambiaPrezzo(decimal nuovoPrezzo)
    {
        // Metodo già implementato: centralizza la validazione del prezzo.
        if (nuovoPrezzo <= 0)
        {
            throw new ArgumentException("Il prezzo deve essere maggiore di zero.");
        }

        Prezzo = nuovoPrezzo;
    }

    public void CambiaQuantita(int variazioneQuantita)
    {
        // Metodo già implementato: impedisce di portare il magazzino sotto zero.
        int nuovaQuantita = QuantitaDisponibile + variazioneQuantita;

        if (nuovaQuantita < 0)
        {
            throw new InvalidOperationException("La quantità disponibile non può diventare negativa.");
        }

        QuantitaDisponibile = nuovaQuantita;
    }

    public int CalcolaQuantitaVenduta()
    {
        // Metodo già implementato: serve per il report amministratore.
        return QuantitaIniziale - QuantitaDisponibile;
    }
}

public class ElementoCarrello
{
    public Prodotto ProdottoSelezionato { get; private set; }
    public int QuantitaScelta { get; private set; }
    public decimal PrezzoUnitario { get; private set; }

    public ElementoCarrello(Prodotto prodottoSelezionato, int quantitaScelta)
    {
        ProdottoSelezionato = prodottoSelezionato;
        QuantitaScelta = quantitaScelta;
        PrezzoUnitario = prodottoSelezionato.Prezzo;
    }

    public decimal CalcolaTotaleParziale()
    {
        // Metodo già implementato: evita di duplicare il calcolo del parziale.
        return PrezzoUnitario * QuantitaScelta;
    }

    public void CambiaQuantitaScelta(int nuovaQuantita)
    {
        // TODO: validare che la nuova quantità sia maggiore di zero.
        // Se è valida, aggiornare QuantitaScelta.
        // Se non è valida, lanciare ArgumentException con un messaggio comprensibile.
        if (nuovaQuantita <= 0)
        {
            throw new ArgumentException("La quantità scelta deve essere maggiore di zero.");
        }
        QuantitaScelta = nuovaQuantita;
        }
}

public class Acquisto
{
    public Utente Utente { get; private set; }
    public string NomeUtente
    {
        get { return Utente.Nome; }
    }

    public List<ElementoAcquistato> ProdottiAcquistati { get; private set; }
    public decimal TotaleOrdine { get; private set; }
    public DateTime DataAcquisto { get; private set; }

    public Acquisto(Utente utente, List<ElementoAcquistato> prodottiAcquistati)
    {
        Utente = utente;
        ProdottiAcquistati = prodottiAcquistati;
        DataAcquisto = DateTime.Now;
        TotaleOrdine = CalcolaTotaleOrdine();
    }

    private decimal CalcolaTotaleOrdine()
    {
        // Metodo già implementato: somma tutti i parziali dei prodotti acquistati.
        return ProdottiAcquistati.Sum(prodotto => prodotto.TotaleParziale);
    }
}

public class ElementoAcquistato
{
    public string CodiceProdotto { get; private set; }
    public string NomeProdotto { get; private set; }
    public int QuantitaAcquistata { get; private set; }
    public decimal PrezzoUnitario { get; private set; }
    public decimal TotaleParziale { get; private set; }

    public ElementoAcquistato(string codiceProdotto, string nomeProdotto, int quantitaAcquistata, decimal prezzoUnitario)
    {
        CodiceProdotto = codiceProdotto;
        NomeProdotto = nomeProdotto;
        QuantitaAcquistata = quantitaAcquistata;
        PrezzoUnitario = prezzoUnitario;
        TotaleParziale = prezzoUnitario * quantitaAcquistata;
    }
}

public class CatalogoProdotti : IGestioneCatalogo
{
    private readonly List<Prodotto> prodotti;

    public CatalogoProdotti()
    {
        prodotti = new List<Prodotto>();
    }

    public void AggiungiProdotto(Prodotto prodotto)
    {
        // Metodo già implementato: evita codici duplicati nel catalogo.
        bool codiceGiaPresente = prodotti.Any(p => p.CodiceProdotto == prodotto.CodiceProdotto);

        if (codiceGiaPresente)
        {
            throw new InvalidOperationException("Esiste già un prodotto con lo stesso codice.");
        }

        prodotti.Add(prodotto);
    }

    public bool EliminaProdotto(string codiceProdotto)
    {
        // TODO: cercare il prodotto tramite codice e rimuoverlo dalla lista.
        // Restituire true se il prodotto è stato eliminato, false se non esiste.
        Prodotto? prodotto = CercaProdottoPerCodice(codiceProdotto);
        if (prodotto == null) return false;
        
        return prodotti.Remove(prodotto);
    }

    public Prodotto? CercaProdottoPerCodice(string codiceProdotto)
    {
        // Metodo già implementato: ricerca case-insensitive per rendere più comodo l'input da console.
        return prodotti.FirstOrDefault(prodotto =>
            prodotto.CodiceProdotto.Equals(codiceProdotto, StringComparison.OrdinalIgnoreCase));
    }

    public List<Prodotto> OttieniTuttiIProdotti()
    {
        // Metodo già implementato: restituisce una copia per proteggere la lista interna.
        return new List<Prodotto>(prodotti);
    }

    public bool ModificaPrezzoProdotto(string codiceProdotto, decimal nuovoPrezzo)
    {
        // TODO: trovare il prodotto e chiamare prodotto.CambiaPrezzo(nuovoPrezzo).
        // Restituire false se il codice non esiste.
        Prodotto? prodotto = CercaProdottoPerCodice(codiceProdotto);
        if (prodotto == null) return false;

        try
        {
            prodotto.CambiaPrezzo(nuovoPrezzo);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    public bool ModificaQuantitaProdotto(string codiceProdotto, int variazioneQuantita)
    {
        // TODO: trovare il prodotto e chiamare prodotto.CambiaQuantita(variazioneQuantita).
        // La variazione può essere positiva o negativa, ma il magazzino non deve scendere sotto zero.
        Prodotto? prodotto = CercaProdottoPerCodice(codiceProdotto);
        if (prodotto == null) return false;

        try
        {
            prodotto.CambiaQuantita(variazioneQuantita);
            return true;
        }
        catch (Exception) 
        {
            return false;
        }
    }
}

public class CarrelloUtente : IGestioneCarrello
{
    private readonly List<ElementoCarrello> elementiCarrello;

    public CarrelloUtente()
    {
        elementiCarrello = new List<ElementoCarrello>();
    }

    public bool AggiungiAlCarrello(Prodotto prodotto, int quantita)
    {
        // TODO: completare l'aggiunta al carrello.
        // Regole:
        // - rifiutare quantità <= 0;
        // - rifiutare quantità maggiore della disponibilità di magazzino;
        // - se il prodotto è già presente, aumentare la quantità esistente;
        // - controllare che quantità esistente + quantità richiesta non superi il magazzino.
        if (quantita <= 0 || quantita > prodotto.QuantitaDisponibile) return false;

        ElementoCarrello? esistente = elementiCarrello.FirstOrDefault(e => 
            e.ProdottoSelezionato.CodiceProdotto.Equals(prodotto.CodiceProdotto, StringComparison.OrdinalIgnoreCase));

        if (esistente != null)
        {
            int totaleRichiesto = esistente.QuantitaScelta + quantita;
            if (totaleRichiesto > prodotto.QuantitaDisponibile) return false;
            
            esistente.CambiaQuantitaScelta(totaleRichiesto);
        }
        else
        {
            elementiCarrello.Add(new ElementoCarrello(prodotto, quantita));
        }
        return true;
    }

    public bool ModificaQuantitaNelCarrello(string codiceProdotto, int nuovaQuantita)
    {
        // TODO: trovare l'elemento del carrello e modificarne la quantità.
        // Regole:
        // - nuovaQuantita deve essere > 0;
        // - nuovaQuantita non deve superare la disponibilità del prodotto.
        if (nuovaQuantita <= 0) return false;

        ElementoCarrello? esistente = elementiCarrello.FirstOrDefault(e => 
            e.ProdottoSelezionato.CodiceProdotto.Equals(codiceProdotto, StringComparison.OrdinalIgnoreCase));

        if (esistente == null || nuovaQuantita > esistente.ProdottoSelezionato.QuantitaDisponibile) return false;

        esistente.CambiaQuantitaScelta(nuovaQuantita);
        return true;
    }

    public bool RimuoviDalCarrello(string codiceProdotto)
    {
        // TODO: rimuovere dal carrello l'elemento con il codice indicato.
        // Restituire true se rimosso, false se non trovato.
        ElementoCarrello? esistente = elementiCarrello.FirstOrDefault(e => 
        e.ProdottoSelezionato.CodiceProdotto.Equals(codiceProdotto, StringComparison.OrdinalIgnoreCase));

        if (esistente == null) return false;

        return elementiCarrello.Remove(esistente);
    }

    public void SvuotaCarrello()
    {
        // Metodo già implementato: cancella tutti gli elementi del carrello.
        elementiCarrello.Clear();
    }

    public decimal CalcolaTotale()
    {
        // Metodo già implementato: ricalcola sempre il totale dai parziali correnti.
        return elementiCarrello.Sum(elemento => elemento.CalcolaTotaleParziale());
    }

    public List<ElementoCarrello> OttieniElementi()
    {
        // Metodo già implementato: restituisce una copia per evitare modifiche esterne dirette.
        return new List<ElementoCarrello>(elementiCarrello);
    }
}

public class StoricoAcquisti : IGestioneAcquisti
{
    private readonly List<Acquisto> acquisti;

    public StoricoAcquisti()
    {
        acquisti = new List<Acquisto>();
    }

    public void RegistraAcquisto(Acquisto acquisto)
    {
        // Metodo già implementato: conserva l'acquisto in memoria durante l'esecuzione.
        acquisti.Add(acquisto);
    }

    public List<Acquisto> OttieniTuttiGliAcquisti()
    {
        // Metodo già implementato: restituisce una copia dello storico.
        return new List<Acquisto>(acquisti);
    }

    public List<Acquisto> OttieniAcquistiPerUtente(string nomeUtente)
    {
        // TODO: filtrare gli acquisti per nome utente.
        // Consiglio: usare StringComparison.OrdinalIgnoreCase per ignorare maiuscole/minuscole.
        return acquisti.Where(a => a.NomeUtente.Equals(nomeUtente, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}

public class ServizioNegozio
{
    private readonly CatalogoProdotti catalogoProdotti;
    private readonly CarrelloUtente carrelloUtente;
    private readonly StoricoAcquisti storicoAcquisti;

    public ServizioNegozio(CatalogoProdotti catalogoProdotti, CarrelloUtente carrelloUtente, StoricoAcquisti storicoAcquisti)
    {
        this.catalogoProdotti = catalogoProdotti;
        this.carrelloUtente = carrelloUtente;
        this.storicoAcquisti = storicoAcquisti;
    }

    public bool AggiungiProdottoAlCarrello(string codiceProdotto, int quantita)
    {
        // TODO: cercare il prodotto nel catalogo e delegare a carrelloUtente.AggiungiAlCarrello.
        // Restituire false se il prodotto non esiste o se la quantità non è valida.
        Prodotto? prodotto = catalogoProdotti.CercaProdottoPerCodice(codiceProdotto);
        if (prodotto == null) return false;

        return carrelloUtente.AggiungiAlCarrello(prodotto, quantita);
    }

    public Acquisto ConfermaAcquisto(Utente utente)
    {
        List<ElementoCarrello> elementi = carrelloUtente.OttieniElementi();
        if (elementi.Count == 0)
        {
            throw new InvalidOperationException("Impossibile confermare l'acquisto: il carrello è vuoto.");
        }

        // Validazione preventiva del magazzino prima di toccare i dati
        foreach (var elem in elementi)
        {
            if (elem.QuantitaScelta > elem.ProdottoSelezionato.QuantitaDisponibile)
            {
                throw new InvalidOperationException($"Prodotto {elem.ProdottoSelezionato.Nome} non sufficiente in magazzino.");
            }
        }

        List<ElementoAcquistato> acquistati = new List<ElementoAcquistato>();

        foreach (var elem in elementi)
        {
            // Scala dal magazzino passandogli il valore negativo
            elem.ProdottoSelezionato.CambiaQuantita(-elem.QuantitaScelta);

            acquistati.Add(new ElementoAcquistato(
                elem.ProdottoSelezionato.CodiceProdotto,
                elem.ProdottoSelezionato.Nome,
                elem.QuantitaScelta,
                elem.PrezzoUnitario
            ));
        }

        Acquisto nuovoAcquisto = new Acquisto(utente, acquistati);
        storicoAcquisti.RegistraAcquisto(nuovoAcquisto);
        carrelloUtente.SvuotaCarrello();

        return nuovoAcquisto;
    }

    public List<ReportProdotto> CreaReportProdotti()
    {
        // Metodo già implementato: prepara il report richiesto per l'amministratore.
        return catalogoProdotti.OttieniTuttiIProdotti()
            .Select(prodotto => new ReportProdotto(
                prodotto.CodiceProdotto,
                prodotto.Nome,
                prodotto.QuantitaIniziale,
                prodotto.CalcolaQuantitaVenduta(),
                prodotto.QuantitaDisponibile))
            .ToList();
    }

    public void StampaAcquisto(Acquisto acquisto)
    {
        // Metodo già implementato: mostra i dettagli di un acquisto completato.
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Utente: " + acquisto.NomeUtente);
        Console.WriteLine("Data: " + acquisto.DataAcquisto.ToString("dd/MM/yyyy HH:mm"));
        Console.WriteLine("Prodotti acquistati:");

        foreach (ElementoAcquistato elemento in acquisto.ProdottiAcquistati)
        {
            Console.WriteLine(
                "- " + elemento.CodiceProdotto + " - " +
                elemento.NomeProdotto + " - " +
                "Quantità: " + elemento.QuantitaAcquistata + " - " +
                "Prezzo unitario: " + elemento.PrezzoUnitario.ToString("0.00") + " euro - " +
                "Parziale: " + elemento.TotaleParziale.ToString("0.00") + " euro");
        }

        Console.WriteLine("Totale ordine: " + acquisto.TotaleOrdine.ToString("0.00") + " euro");
    }

    public void StampaReportProdotti()
    {
        // Metodo già implementato: mostra il report quantità richiesto all'amministratore.
        List<ReportProdotto> report = CreaReportProdotti();

        Console.WriteLine();
        Console.WriteLine("=== REPORT PRODOTTI ===");

        if (report.Count == 0)
        {
            Console.WriteLine("Nessun prodotto presente nel catalogo.");
            return;
        }

        foreach (ReportProdotto riga in report)
        {
            Console.WriteLine(
                riga.CodiceProdotto + " - " +
                riga.NomeProdotto + " - " +
                "Iniziale: " + riga.QuantitaIniziale + " - " +
                "Venduta: " + riga.QuantitaVenduta + " - " +
                "Disponibile: " + riga.QuantitaDisponibile);
        }
    }
}

public class ReportProdotto
{
    public string CodiceProdotto { get; private set; }
    public string NomeProdotto { get; private set; }
    public int QuantitaIniziale { get; private set; }
    public int QuantitaVenduta { get; private set; }
    public int QuantitaDisponibile { get; private set; }

    public ReportProdotto(string codiceProdotto, string nomeProdotto, int quantitaIniziale, int quantitaVenduta, int quantitaDisponibile)
    {
        CodiceProdotto = codiceProdotto;
        NomeProdotto = nomeProdotto;
        QuantitaIniziale = quantitaIniziale;
        QuantitaVenduta = quantitaVenduta;
        QuantitaDisponibile = quantitaDisponibile;
    }
}
