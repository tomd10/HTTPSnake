
namespace Pokus3;

public partial class MainPage : ContentPage
{
	int count = 0;
	double dimx = DeviceDisplay.Current.MainDisplayInfo.Width;
	double dimy = DeviceDisplay.Current.MainDisplayInfo.Height;
	double dim;

	public MainPage()
	{
        //Responsivity
		InitializeComponent();
		canvas.HeightRequest = dimx / 3;
		canvas.WidthRequest = dimx / 3;
		DataStore.res = (float)dimx / 3;
		buttonStart.WidthRequest = dimx / 5;
        buttonStart.HeightRequest = dimx / 12;


    }

	public void OnStartClicked(object sender, EventArgs e)
	{
		if (DataStore.configured == false)
		{
			DisplayAlert("Error!", "No configuration loaded.", "OK");
		}
		else
		{
            //Game re-init
            if (DataStore.lost)
            {
                Game.Initialize();
            }

            DataStore.running = true;
			buttonStart.IsVisible = false;

            //Enabling of proper controls
            SwipeL.Swiped -= OnSwipedL;
            SwipeR.Swiped -= OnSwipedR;
            SwipeU.Swiped -= OnSwipedU;
            SwipeD.Swiped -= OnSwipedD;

            if (DataStore.method == Model.ControlMethod.Button)
			{
				buttonL.IsVisible = true;
                buttonR.IsVisible = true;
                buttonU.IsVisible = true;
                buttonD.IsVisible = true;
            }
			else
			{
                buttonL.IsVisible = false;
                buttonR.IsVisible = false;
                buttonU.IsVisible = false;
                buttonD.IsVisible = false;

                SwipeL.Swiped += OnSwipedL;
                SwipeR.Swiped += OnSwipedR;
                SwipeU.Swiped += OnSwipedU;
                SwipeD.Swiped += OnSwipedD;
            }

			canvas.Invalidate();

            //Starting the timer, if we are playing in normal mode
            if (DataStore.cheat == false)
            {
                DataStore.timer = new System.Timers.Timer(DataStore.cfgData.speed);
                DataStore.timer.Elapsed += OnTimerElapsed;
                DataStore.timer.Enabled = true;
                DataStore.timer.AutoReset = false;

            }
		}
	}


    public void OnTimerElapsed(object sender, EventArgs e)
    {
        bool result = Game.Shift(DataStore.pending);
        canvas.Invalidate();
        if (result == false) //Crash detection
        {
            DataStore.running = false;
            DataStore.lost = true;
            MainThread.BeginInvokeOnMainThread(() => { 
                buttonStart.IsVisible = true;
                DisplayAlert("Crash!", "Your final score was " + Game.GetLength() + ".", "OK");
            });
            
            
        }
        else
        {
            DataStore.timer.Start();
        }

    }

    //Handling of controls
	public void OnSwipedL(object sender, EventArgs e)
	{
		if (DataStore.running)
		{
            if (DataStore.cheat)
			{
                bool result = Game.Shift(Model.Direction.Left);
                if (result == false)
                {
                    buttonStart.IsVisible = true;
                    DisplayAlert("Crash!", "Your final score was " + Game.GetLength() + ".", "OK");
                    DataStore.running = false;
                    DataStore.lost = true;
                }
                canvas.Invalidate();
            }
			else
			{
				DataStore.pending = Model.Direction.Left;
			}
        }
	}

    public void OnSwipedR(object sender, EventArgs e)
    {
        if (DataStore.running)
        {
            if (DataStore.cheat)
            {
                bool result = Game.Shift(Model.Direction.Right);
                if (result == false)
                {
                    buttonStart.IsVisible = true;
                    DisplayAlert("Crash!", "Your final score was " + Game.GetLength() + ".", "OK");
                    DataStore.running = false;
                    DataStore.lost = true;
                }
                canvas.Invalidate();
            }
            else
            {
                DataStore.pending = Model.Direction.Right;
            }
        }
    }

    public void OnSwipedU(object sender, EventArgs e)
    {
        if (DataStore.running)
        {
            if (DataStore.cheat)
            {
                bool result = Game.Shift(Model.Direction.Up);
                if (result == false)
                {
                    buttonStart.IsVisible = true;
                    DisplayAlert("Crash!", "Your final score was " + Game.GetLength() + ".", "OK");
                    DataStore.running = false;
                    DataStore.lost = true;
                }
                canvas.Invalidate();
            }
            else
            {
                DataStore.pending = Model.Direction.Up;
            }
        }
    }

    public void OnSwipedD(object sender, EventArgs e)
    {
        if (DataStore.running)
        {
            if (DataStore.cheat)
            {
                bool result = Game.Shift(Model.Direction.Down);
                if (result == false)
                {
                    buttonStart.IsVisible = true;
                    DisplayAlert("Crash!", "Your final score was " + Game.GetLength() + ".", "OK");
                    DataStore.running = false;
                    DataStore.lost = true;
                }
                canvas.Invalidate();
            }
            else
            {
                DataStore.pending = Model.Direction.Down;
            }
        }
    }
}



