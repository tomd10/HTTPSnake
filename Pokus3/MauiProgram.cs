using AndroidX.Annotations;
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace Pokus3;
public static class MauiProgram
{
	
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}
}

public class GraphicsDrawable : IDrawable
{
	//Drawing grid and snake
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
		Graphics.DrawGrid(canvas);
		Graphics.DrawSnake(canvas);
    }
}
