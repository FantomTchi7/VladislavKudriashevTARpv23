
namespace Naidis_App;

public partial class Valgusfoor : ContentPage
{
	public Valgusfoor()
	{
		InitializeComponent();
	}
    async void sisseClicked(object sender, EventArgs args)
    {
        await punane.BackgroundColor = "Red";
    }
    async void valjaClicked(object sender, EventArgs args)
    {
        await punane.BackgroundColor = "Black";
    }
}