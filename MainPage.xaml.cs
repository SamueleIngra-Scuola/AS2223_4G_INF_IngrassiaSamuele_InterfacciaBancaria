namespace InterfacciaBanca;

public partial class MainPage : ContentPage
{
    const int N_MAX_ACCHOLDERS = 100;
    Holder[] holders;
    int n_holders;

    public MainPage()
    {
        InitializeComponent();
        holders = new Holder[N_MAX_ACCHOLDERS];
        n_holders = 0;
    }

    public class Holder
    {
        public string AccName { get; }
        public string CC { get; }
        public float Balance { get; }

        public Holder() { }

        public Holder(string accName, string CC)
        {
            this.AccName = AccName;
            this.CC = CC;
            Balance = 0;
        }

        public Holder(string line)
        {
            string[] token = line.Split(',');

            AccName = token[0];
            CC = token[1];
            Balance = 0;
        }

        //GetInfo Methods
        public string GetHolderInfo()
        {
            return $"{AccName} | {CC} | {Balance}";
        }
    }

    private async void LoadCSVFile()
    {
        try
        {
            StreamReader file = new StreamReader(txtPath.Text);

            string line = file.ReadLine();

            n_holders = 0;

            while (line != null)
            {
                holders[n_holders] = new Holder(line);

                line = file.ReadLine();

                n_holders++;
            }

            file.Close();
        }
        catch (Exception e)
        {
            await DisplayAlert("Errore durante il caricamento del file", e.ToString(), "Ho capito");
        }
    }

    private void Print_Clicked(object sender, EventArgs e)
    {
        int n = 0;

        LoadCSVFile();

        string[] holdersList = new string[n_holders];

        for (int i = 0; i < n_holders; i++)
        {
            holdersList[n] = holders[i].GetHolderInfo();
            n++;
        }

        Array.Resize<string>(ref holdersList, n);

        lstHolders.ItemsSource = holdersList;
        lstHolders.Header = $"Lista Correntisti ({n} Correntisti)";
    }

    private async void Browse_Clicked(object sender, EventArgs e)
    {
        var result = await FilePicker.Default.PickAsync();
        if (result != null)
            if (result.FileName.EndsWith("csv", StringComparison.OrdinalIgnoreCase))   
                txtPath.Text = result.FullPath.ToString();
            else
                await DisplayAlert("Errore", "Per favore, inserisci un file CSV", "Ho capito");
        else
            await DisplayAlert("Errore", "Non è stato inserito alcun file!", "Ho capito");
    }

    private async void CreateAccount_Clicked(object sender, EventArgs e)
    {
        if (txtPath.Text != null)
        {
            if (txtHolder.Text != null && txtCC.Text != null)
            {
                string accountDetails = $"{txtHolder.Text},{txtCC.Text},{0}" + Environment.NewLine;
                File.AppendAllText(txtPath.Text, accountDetails);
                await DisplayAlert("Successo", "Hai creato un nuovo conto!", "Ho capito");
                txtHolder.Text = "";
                txtCC.Text = "";
            }
            else
                await DisplayAlert("Errore", "Inserisci tutte le informazioni del correntista", "Ho capito");
        }
        else
            await DisplayAlert("Errore", "Non puoi salvare il conto da nessuna parte, carica un file CSV", "Ho capito");
    }

    private async void Deposit_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Errore", "Funzione Sperimentale!", "Ho capito");
    }
    private async void Withdraw_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Errore", "Funzione Sperimentale!", "Ho capito");
    }
}

