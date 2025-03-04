using System.Diagnostics;
using Microsoft.Maui.Controls.Shapes;
namespace Naidis_App;
public class Lumememm : ContentPage
{
    int pageHeight, pageWidth, pallEsimeneSize, pallInitialEsimeneSize, silmSize, noopSize, pallTeineSize, pallKolmasSize, porgandSize, torukubarSize, oksSize;
    ScrollView sv = new ScrollView();
    VerticalStackLayout vsl = new VerticalStackLayout();
    static List<string> views = new List<string> { "Size", "Items" };
    Picker picker = new Picker { ItemsSource = views, SelectedIndex = 0 };
    AbsoluteLayout al = new AbsoluteLayout { BackgroundColor = Color.FromArgb("#000000") };
    Border lumememm = new Border { Padding = 0, Margin = 0 };
    Border lumememmPea = new Border { Padding = 0, Margin = 0 };
    Border lumememmKeha = new Border { Padding = 0, Margin = 0 };
    Border lumememmAlus = new Border { Padding = 0, Margin = 0 };
    Border lumememmLSilm = new Border { Padding = 0, Margin = 0 };
    Border lumememmRSilm = new Border { Padding = 0, Margin = 0 };
    Border lumememmUNoop = new Border { Padding = 0, Margin = 0 };
    Border lumememmMNoop = new Border { Padding = 0, Margin = 0 };
    Border lumememmBNoop = new Border { Padding = 0, Margin = 0 };
    Border lumememmPPorgand = new Border { Padding = 0, Margin = 0 };
    Border lumememmPTorukubar = new Border { Padding = 0, Margin = 0 };
    Border lumememmLOks = new Border { Padding = 0, Margin = 0 };
    Border lumememmROks = new Border { Padding = 0, Margin = 0 };
    Image lOks = new Image { Source="l_stick.png" };
    Image rOks = new Image { Source="r_stick.png" };
    Image pTorukubar = new Image { Source="top_hat.png" };
    Image pPorgand = new Image { Source="carrot.png" };
    Image kLumi = new Image { Source="snow.png" };
    Image aLumi = new Image { Source="snow.png" };
    Image pLumi = new Image { Source="snow.png" };
    Image lNoop = new Image { Source="button.png" };
    Image rNoop = new Image { Source="button.png" };
    Image uNoop = new Image { Source="button.png" };
    Image mNoop = new Image { Source="button.png" };
    Image bNoop = new Image { Source="button.png" };
    Grid grid = new Grid();
    List<(Label Label, Slider Slider, Label ValueLabel)> sliderControls = new();
    int firstValue = 100;
    int secondValue = 50;
    int thirdValue = 50;
    int fourthValue = 50;
    public Lumememm()
    {
        InitializeSizes();
        InitializeBorders();
        InitializeSliders();
        BuildUI();
    }
    void InitializeSizes()
    {
        pageHeight = (int)(DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density);
        pageWidth = (int)(DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density);
        lumememm.WidthRequest = pageWidth;
        lumememm.HeightRequest = pageWidth;
        pallEsimeneSize = pageWidth / 5;
        pallInitialEsimeneSize = pallEsimeneSize;
        silmSize = pallEsimeneSize / 5;
        noopSize = silmSize;
        porgandSize = silmSize * 2;
        oksSize = (int)(pallTeineSize / 1.5);
        torukubarSize = (int)(pallEsimeneSize / 1.5);
        pallTeineSize = (int)(pallEsimeneSize * 1.25);
        pallKolmasSize = (int)(pallTeineSize * 1.5);
    }
    void InitializeBorders()
    {
        lumememmPea = CreateBorder(pallEsimeneSize);
        lumememmKeha = CreateBorder(pallTeineSize);
        lumememmAlus = CreateBorder(pallKolmasSize);
        lumememmLSilm = CreateBorder(silmSize);
        lumememmRSilm = CreateBorder(silmSize);
        lumememmUNoop = CreateBorder(noopSize);
        lumememmMNoop = CreateBorder(noopSize);
        lumememmBNoop = CreateBorder(noopSize);
        lumememmPPorgand = CreateBorder(porgandSize);
        lumememmPTorukubar = CreateBorder(torukubarSize);
        lumememmLOks = CreateBorder(oksSize);
        lumememmROks = CreateBorder(oksSize);
        foreach (Border border in new[] { lumememmPea, lumememmPTorukubar, lumememmKeha, lumememmLOks, lumememmROks, lumememmPPorgand, lumememmAlus, lumememmLSilm, lumememmRSilm, lumememmBNoop, lumememmMNoop, lumememmUNoop }) al.Children.Add(border);
    }
    Border CreateBorder(int cornerRadius) => new Border
    {
        StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(cornerRadius) },
        StrokeThickness = 0
    };
    void InitializeSliders()
    {
        (Label Label, Slider Slider, Label ValueLabel)[] sliders = new[]
        {
            (Label: new Label { Text = "Hide", HorizontalOptions = LayoutOptions.Start }, Slider: new Slider(0, 100, 100), ValueLabel: new Label { HorizontalOptions = LayoutOptions.End, Text = "100" }),
            (Label: new Label { Text = "G", HorizontalOptions = LayoutOptions.Start }, Slider: new Slider(0, 100, 50), ValueLabel: new Label { HorizontalOptions = LayoutOptions.End, Text = "50" }),
            (Label: new Label { Text = "B", HorizontalOptions = LayoutOptions.Start }, Slider: new Slider(0, 100, 50), ValueLabel: new Label { HorizontalOptions = LayoutOptions.End, Text = "50" }),
            (Label: new Label { Text = "A", HorizontalOptions = LayoutOptions.Start }, Slider: new Slider(0, 100, 50), ValueLabel: new Label { HorizontalOptions = LayoutOptions.End, Text = "50" })
        };
        for (int i = 0; i < sliders.Length; i++)
        {
            (Label label, Slider slider, Label valueLabel) = sliders[i];
            slider.ValueChanged += Slider_ValueChanged;
            sliderControls.Add((label, slider, valueLabel));
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.Children.Add(label); Grid.SetRow(label, i); Grid.SetColumn(label, 0);
            grid.Children.Add(slider); Grid.SetRow(slider, i); Grid.SetColumn(slider, 1);
            grid.Children.Add(valueLabel); Grid.SetRow(valueLabel, i); Grid.SetColumn(valueLabel, 2);
        }
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
    }
    void BuildUI()
    {
        lumememmPea.Content = pLumi;
        lumememmKeha.Content = kLumi;
        lumememmLOks.Content = lOks;
        lumememmROks.Content = rOks;
        lumememmAlus.Content = aLumi;
        lumememmLSilm.Content = lNoop;
        lumememmRSilm.Content = rNoop;
        lumememmUNoop.Content = uNoop;
        lumememmMNoop.Content = mNoop;
        lumememmBNoop.Content = bNoop;
        lumememmPPorgand.Content = pPorgand;
        lumememmPTorukubar.Content = pTorukubar;
        lumememm.Content = al;
        vsl.Children.Add(picker);
        vsl.Children.Add(lumememm);
        vsl.Children.Add(grid);
        sv.Content = vsl;
        Content = sv;
        Refresh();
    }
    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        Slider slider = (Slider)sender;
        int index = sliderControls.FindIndex(c => c.Slider == slider);
        int newValue = (int)e.NewValue;
        if (index == -1) return;
        switch (index)
        {
            case 0:
                firstValue = newValue;
                pallEsimeneSize = firstValue * pallInitialEsimeneSize / 100;
                Refresh();
                break;
            case 1:
                secondValue = newValue;
                break;
            case 2:
                thirdValue = newValue;
                break;
            case 3:
                fourthValue = newValue;
                break;
        }
        sliderControls[index].ValueLabel.Text = newValue.ToString();
    }
    int CalcPallKeskus(int size) => pageWidth / 2 - size / 2;
    int Lerp(int start, int end, int percent, int percentOutOf = 100) => start + (end - start) * percent / percentOutOf;
    void Refresh()
    {
        int pallEsimeneKeskus = CalcPallKeskus(pallEsimeneSize);
        int pallPeaY = pallEsimeneKeskus - pallEsimeneSize;
        al.SetLayoutBounds(lumememmPea, new Rect(pallEsimeneKeskus, pallPeaY, pallEsimeneSize, pallEsimeneSize));
        int torukubarKeskus = CalcPallKeskus(torukubarSize);
        int yTorukubar = pallPeaY - (int)(torukubarSize * (float)0.8);
        al.SetLayoutBounds(lumememmPTorukubar, new Rect(torukubarKeskus, yTorukubar, torukubarSize, torukubarSize));
        int silmKeskus = CalcPallKeskus(silmSize);
        int xEndLSilm = pallEsimeneKeskus + silmSize;
        int xEndRSilm = pageWidth - pallEsimeneKeskus - silmSize * 2;
        int yEndSilm = pallEsimeneKeskus - pallEsimeneSize + silmSize;
        al.SetLayoutBounds(lumememmLSilm, new Rect(Lerp(silmKeskus, xEndLSilm, firstValue), Lerp(silmKeskus, yEndSilm, firstValue), silmSize, silmSize));
        al.SetLayoutBounds(lumememmRSilm, new Rect(Lerp(silmKeskus, xEndRSilm, firstValue), Lerp(silmKeskus, yEndSilm, firstValue), silmSize, silmSize));
        int porgandKeskus = CalcPallKeskus(porgandSize);
        int yEndPPorgand = pallEsimeneKeskus - pallEsimeneSize + porgandSize;
        al.SetLayoutBounds(lumememmPPorgand, new Rect(porgandKeskus, Lerp(porgandKeskus, yEndPPorgand, firstValue), porgandSize, porgandSize));
        int pallTeineKeskus = CalcPallKeskus(pallTeineSize);
        al.SetLayoutBounds(lumememmKeha, new Rect(pallTeineKeskus, pallTeineKeskus, pallTeineSize, pallTeineSize));
        int oksKeskus = CalcPallKeskus(oksSize);
        int xEndLOks = pageWidth - pallTeineKeskus - oksSize * 2 - silmSize;
        int xEndROks = pallTeineKeskus + oksSize + silmSize;
        int yEndOks = pallTeineKeskus - pallTeineSize + oksSize;
        al.SetLayoutBounds(lumememmLOks, new Rect(Lerp(oksKeskus, xEndLOks, firstValue), Lerp(oksKeskus, yEndOks, firstValue), oksSize, oksSize));
        al.SetLayoutBounds(lumememmROks, new Rect(Lerp(oksKeskus, xEndROks, firstValue), Lerp(oksKeskus, yEndOks, firstValue), oksSize, oksSize));
        int pallKolmasKeskus = CalcPallKeskus(pallKolmasSize);
        al.SetLayoutBounds(lumememmAlus, new Rect(pallKolmasKeskus, pallKolmasKeskus + pallEsimeneSize, pallKolmasSize, pallKolmasSize));
        uNoop.Opacity = (float)firstValue / 100;
        lumememmUNoop.Background = Color.FromRgba("#000000" + ((int)(255 - firstValue / (float)100.0 * 255)).ToString("X2"));
        int startUNoopKeskus = CalcPallKeskus(pallKolmasSize + 2);
        int endUNoopKeskus = CalcPallKeskus(noopSize);
        int lerpedUNoopSize = Lerp(pallKolmasSize + 2, noopSize, firstValue);
        al.SetLayoutBounds(lumememmUNoop, new Rect(Lerp(startUNoopKeskus, endUNoopKeskus, firstValue), Lerp(startUNoopKeskus, endUNoopKeskus, firstValue), lerpedUNoopSize, lerpedUNoopSize));
        lumememmUNoop.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(Lerp(pallKolmasSize, noopSize, firstValue)) };
        int noopKeskus = CalcPallKeskus(noopSize);
        al.SetLayoutBounds(lumememmMNoop, new Rect(noopKeskus, Lerp(noopKeskus, noopKeskus + noopSize * 2, firstValue), noopSize, noopSize));
        al.SetLayoutBounds(lumememmBNoop, new Rect(noopKeskus, Lerp(noopKeskus, noopKeskus + noopSize * 4, firstValue), noopSize, noopSize));
    }
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        InitializeSizes();
        lumememmPea.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(pallEsimeneSize) };
        lumememmLSilm.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(silmSize) };
        lumememmRSilm.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(silmSize) };
        lumememmKeha.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(pallTeineSize) };
        lumememmAlus.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(pallKolmasSize) };
        lumememmUNoop.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(noopSize) };
        lumememmMNoop.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(noopSize) };
        lumememmBNoop.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(noopSize) };
        Refresh();
    }
}