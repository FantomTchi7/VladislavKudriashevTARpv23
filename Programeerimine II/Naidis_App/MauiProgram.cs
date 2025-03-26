using Microsoft.EntityFrameworkCore;

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

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "naidisapp.db");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Filename={dbPath}"));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }

            return app;
        }
    }
}