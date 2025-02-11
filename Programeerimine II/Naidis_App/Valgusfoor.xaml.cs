
namespace Naidis_App;

public partial class Valgusfoor : ContentPage
{
    int i = 0;
    public Valgusfoor()
    {
        InitializeComponent();
    }
    void sisseClicked(object sender, EventArgs args)
    {
        if (i >= 3)
        {
            i = 1;
        }
        else
        {
            i++;
        }
        if (i == 1)
        {
            punane.BackgroundColor = Color.FromArgb("#FF0000");
            kollane.BackgroundColor = Color.FromArgb("#000000");
            roheline.BackgroundColor = Color.FromArgb("#000000");
            kollaneLabel.Text = "Kollane";
            rohelineLabel.Text = "Roheline";
        }
        else if (i == 2)
        {
            punane.BackgroundColor = Color.FromArgb("#000000");
            kollane.BackgroundColor = Color.FromArgb("#FFFF00");
            roheline.BackgroundColor = Color.FromArgb("#000000");
            punaneLabel.Text = "Punane";
            rohelineLabel.Text = "Roheline";
        }
        else if (i == 3)
        {
            punane.BackgroundColor = Color.FromArgb("#000000");
            kollane.BackgroundColor = Color.FromArgb("#000000");
            roheline.BackgroundColor = Color.FromArgb("#00FF00");
            punaneLabel.Text = "Punane";
            kollaneLabel.Text = "Kollane";
        }
    }
    async void autoClicked(object sender, EventArgs args)
    {
        i = 4;
        while (i == 4)
        {
            if (i == 4) { punane.BackgroundColor = Color.FromArgb("#FF0000"); } else { break; }
            if (i == 4) { kollane.BackgroundColor = Color.FromArgb("#000000"); } else { break; }
            if (i == 4) { roheline.BackgroundColor = Color.FromArgb("#000000"); } else { break; }
            if (i == 4) { await Task.Delay(500); } else { break; }
            if (i == 4) { punane.BackgroundColor = Color.FromArgb("#000000"); } else { break; }
            if (i == 4) { kollane.BackgroundColor = Color.FromArgb("#FFFF00"); } else { break; }
            if (i == 4) { roheline.BackgroundColor = Color.FromArgb("#000000"); } else { break; }
            if (i == 4) { await Task.Delay(500); } else { break; }
            if (i == 4) { punane.BackgroundColor = Color.FromArgb("#000000"); } else { break; }
            if (i == 4) { kollane.BackgroundColor = Color.FromArgb("#000000"); } else { break; }
            if (i == 4) { roheline.BackgroundColor = Color.FromArgb("#00FF00"); } else { break; }
            if (i == 4) { await Task.Delay(500); } else { break; }
        }
    }
    void valjaClicked(object sender, EventArgs args)
    {
        i = 0;
        punane.BackgroundColor = Color.FromArgb("#000000");
        kollane.BackgroundColor = Color.FromArgb("#000000");
        roheline.BackgroundColor = Color.FromArgb("#000000");
    }
    private void punaneTapped(object sender, TappedEventArgs e)
    {
        if (i == 1)
        {
            punaneLabel.Text = "Stop";
        }
        else
        {
            punaneLabel.Text = "Punane";
        }
    }
    private void kollaneTapped(object sender, TappedEventArgs e)
    {
        if (i == 2)
        {
            kollaneLabel.Text = "Oota";
        }
        else
        {
            kollaneLabel.Text = "Kollane";
        }
    }
    private void rohelineTapped(object sender, TappedEventArgs e)
    {
        if (i == 3)
        {
            rohelineLabel.Text = "Mine";
        }
        else
        {
            rohelineLabel.Text = "Roheline";
        }
    }
}