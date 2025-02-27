using Microsoft.Maui.Layouts;
using System.Runtime.CompilerServices;

namespace Naidis_App;

public class Lumememm : ContentPage
{
    static int pageHeight = (int)(DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density);
    static int pageWidth = (int)(DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density);

    ScrollView sv = new ScrollView();
    VerticalStackLayout vsl = new VerticalStackLayout();
    Frame lumememm = new Frame
    {
        WidthRequest = pageWidth,
        HeightRequest = pageWidth,
        Padding = 0,
        Margin = 0
    };
    Frame lumememmPea = new Frame
    {
        BackgroundColor = Color.FromArgb("#FFFFFF")
    };
    Frame lumememmKeha = new Frame
    {
        BackgroundColor = Color.FromArgb("#FFFFFF")
    };
    Frame lumememmAlus = new Frame
    {
        BackgroundColor = Color.FromArgb("#FFFFFF")
    };
    Frame lumememmLSilm = new Frame
    {
        BackgroundColor = Color.FromArgb("#FFFF00")
    };
    Frame lumememmRSilm = new Frame
    {
        BackgroundColor = Color.FromArgb("#FFFF00")
    };

    AbsoluteLayout al = new AbsoluteLayout();
    public Lumememm()
	{
        al.Children.Add(lumememmPea);
        al.Children.Add(lumememmLSilm);
        al.Children.Add(lumememmRSilm);
        al.Children.Add(lumememmKeha);
        al.Children.Add(lumememmAlus);

        int pallEsimeneSize = (int)(pageWidth / 5);

        int silmSize = (int)(pallEsimeneSize / 5);

        int pallTeineSize = (int)(pallEsimeneSize * 1.25);
        int pallKolmasSize = (int)(pallTeineSize * 1.5);

        lumememmPea.CornerRadius = pallEsimeneSize;
        lumememmLSilm.CornerRadius = silmSize;
        lumememmRSilm.CornerRadius = silmSize;
        lumememmKeha.CornerRadius = pallTeineSize;
        lumememmAlus.CornerRadius = pallKolmasSize;
        
        al.SetLayoutBounds(lumememmPea, new Rect(CalcPallKeskus(pallEsimeneSize), CalcPallKeskus(pallEsimeneSize) - pallEsimeneSize, pallEsimeneSize, pallEsimeneSize));

        al.SetLayoutBounds(lumememmLSilm, new Rect(CalcPallKeskus(pallEsimeneSize) + pallEsimeneSize / 5 + pallEsimeneSize / 2, CalcPallKeskus(pallEsimeneSize) - pallEsimeneSize, silmSize, silmSize));
        al.SetLayoutBounds(lumememmRSilm, new Rect(CalcPallKeskus(pallEsimeneSize) - pallEsimeneSize / 5 + pallEsimeneSize / 2, CalcPallKeskus(pallEsimeneSize) - pallEsimeneSize, silmSize, silmSize));

        al.SetLayoutBounds(lumememmKeha, new Rect(CalcPallKeskus(pallTeineSize), CalcPallKeskus(pallTeineSize), pallTeineSize, pallTeineSize));
        al.SetLayoutBounds(lumememmAlus, new Rect(CalcPallKeskus(pallKolmasSize), CalcPallKeskus(pallKolmasSize) + pallEsimeneSize, pallKolmasSize, pallKolmasSize));

        lumememm.Content = al;
        vsl.Children.Add(lumememm);
        sv.Content = vsl;
        Content = sv;
    }
    private int CalcPallKeskus(int pallSize)
    {
        return pageWidth / 2 - pallSize / 2;
    }
}