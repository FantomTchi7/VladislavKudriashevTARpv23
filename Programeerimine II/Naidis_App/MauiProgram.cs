using Microsoft.Extensions.Logging;

namespace Naidis_App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("BigBlueTermPlusNerdFontMono-Regular.ttf", "BBTP-Mono-Regular");
                    fonts.AddFont("BigBlueTermPlusNerdFontPropo-Regular.ttf", "BBTP-Propo-Regular");
                    fonts.AddFont("BigBlueTermPlusNerdFont-Regular.ttf", "BBTP-Regular");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
