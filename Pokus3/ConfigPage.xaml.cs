using System.Net.Http;

namespace Pokus3;

public partial class ConfigPage : ContentPage
{
	public ConfigPage()
	{
		InitializeComponent();
	}


	//Fetching config
    public async void OnFetchClicked(object sender, EventArgs e)
	{
		string URLbase = entryURL.Text;
		string difficulty = pickerDifficulty.SelectedItem == null ? "normal" : pickerDifficulty.SelectedItem.ToString().ToLower();
		string URL = URLbase + "/" + difficulty + ".txt";

		string HTTPResponse = "";
		try
		{
            //HTTPResponse = await Networking.GetResponse("https://kraken.pedf.cuni.cz/~chabadat/easy.txt");
            HTTPResponse = await Networking.GetResponse(URL);
        }
		catch
		{
            DisplayAlert("Error!", "Networking error, or wrong URL.", "OK");
			return;
        }
        

        //string HTTPReply = await GetConfig(URL);
		Model.ConfigData cfg = Model.ParseConfig(HTTPResponse);
		if (cfg.speed == 0)
		{
			await DisplayAlert("Error!", "File format error.", "OK");
		}
		else if (cfg.speed == -1)
		{
			await DisplayAlert("Error!", "HTTP error.", "OK");
		}
		else
		{
			labelConfigured.Text = "Configured";
			await DisplayAlert("Success!", "Configuration loaded.", "OK");
			DataStore.cfgData = cfg;
			DataStore.configured = true;
			Game.Initialize();
		}
    }

	//Toggling switch 1
	public void OnControlToggle(object sender, EventArgs e)
	{
		if (switchControl.IsToggled == true)
		{
			DataStore.method = Model.ControlMethod.Swipe;
		}
		else DataStore.method = Model.ControlMethod.Button;
	}

    //Toggling switch 2
    public void OnEasyToggle(object sender, EventArgs e)
	{
		DataStore.cheat = switchEasy.IsToggled;
	}

	/*
	 * 
	 * 
	 */


}