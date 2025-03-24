
namespace Naidis_App;

public partial class StartPage : ContentPage
{
    public List<KeyValuePair<ContentPage, string>> Lehed =
    [
        new KeyValuePair<ContentPage, string>(new Valgusfoor(), "Tee lahti Valgusfoor"),
        new KeyValuePair<ContentPage, string>(new DateTimePage(), "Tee lahti DateTimePage"),
        new KeyValuePair<ContentPage, string>(new StepperSliderPage(), "Tee lahti StepperSliderPage"),
        new KeyValuePair<ContentPage, string>(new ColorPage(), "Tee lahti ColorPage"),
        new KeyValuePair<ContentPage, string>(new Lumememm(), "Tee lahti Lumememm"),
        new KeyValuePair<ContentPage, string>(new TripsTrapsTrull(), "Tee lahti Trips Traps Trull"),
        new KeyValuePair<ContentPage, string>(new SobradeKontaktandmed(), "Tee lahti Sõbrade Kontakti Andmed"),
        new KeyValuePair<ContentPage, string>(new EuroopaRiigid(), "Tee lahti Euroopa Riigid")
    ];
    private readonly ScrollView sv = new();
    private readonly VerticalStackLayout vsl = new()
    {
        BackgroundColor = Color.FromArgb("#000000")
    };

    public StartPage()
    {
        Title = "Avaleht";

        for (int i = 0; i < Lehed.Count; i++)
        {
            Button nupp = new()
            {
                Text = Lehed[i].Value,
                BackgroundColor = Color.FromArgb("#000000"),
                TextColor = Color.FromArgb("#FFFFFF"),
                BorderWidth = 10,
                ZIndex = i
            };
            vsl.Add(nupp);
            nupp.Clicked += Lehte_avamine;
        }
        sv.Content = vsl;
        Content = sv;
    }

    private async void Lehte_avamine(object? sender, EventArgs e)
    {
        Button? btn = sender as Button;
        await Navigation.PushAsync(Lehed[btn.ZIndex].Key);
    }
}