using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Naidis_App;

public class Lumememm : ContentPage
{
    static int pageHeight = (int)(DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density);
    static int pageWidth = (int)(DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density);

    ScrollView sv = new ScrollView();
    VerticalStackLayout vsl = new VerticalStackLayout();
    static List<string> views = new List<string> { "Size", "Items" };
    Picker picker = new Picker
    {
        ItemsSource = views,
        SelectedIndex = 0
    };
    Frame lumememm = new Frame
    {
        WidthRequest = pageWidth,
        HeightRequest = pageWidth,
        Padding = 0,
        Margin = 0
    };
    Frame lumememmPea = new Frame
    {
        BackgroundColor = Color.FromArgb("#FFFFFF"),
        BorderColor = Color.FromArgb("#FFFFFF")
    };
    Frame lumememmKeha = new Frame
    {
        BackgroundColor = Color.FromArgb("#FFFFFF"),
        BorderColor = Color.FromArgb("#FFFFFF")
    };
    Frame lumememmAlus = new Frame
    {
        BackgroundColor = Color.FromArgb("#FFFFFF"),
        BorderColor = Color.FromArgb("#FFFFFF")
    };
    Frame lumememmLSilm = new Frame
    {
        BackgroundColor = Color.FromArgb("#000000")
    };
    Frame lumememmRSilm = new Frame
    {
        BackgroundColor = Color.FromArgb("#000000")
    };
    Frame lumememmUNoop = new Frame
    {
        BackgroundColor = Color.FromArgb("#000000")
    };
    Frame lumememmMNoop = new Frame
    {
        BackgroundColor = Color.FromArgb("#000000")
    };
    Frame lumememmBNoop = new Frame
    {
        BackgroundColor = Color.FromArgb("#000000")
    };

    static int pallEsimeneSize = (int)(pageWidth / 5);

    static int silmSize = (int)(pallEsimeneSize / 5);
    static int noopSize = (int)(pallEsimeneSize / 5);

    static int pallTeineSize = (int)(pallEsimeneSize * 1.25);
    static int pallKolmasSize = (int)(pallTeineSize * 1.5);

    Label labelSliderFirst = new Label
    {
        Text = "R",
        HorizontalOptions = LayoutOptions.Start
    };
    Label labelSliderSecond = new Label
    {
        Text = "G",
        HorizontalOptions = LayoutOptions.Start
    };
    Label labelSliderThird = new Label
    {
        Text = "B",
        HorizontalOptions = LayoutOptions.Start
    };
    Label labelSliderFourth = new Label
    {
        Text = "A",
        HorizontalOptions = LayoutOptions.Start
    };
    Label labelSliderValueFirst = new Label
    {
        Text = "50",
        HorizontalOptions = LayoutOptions.End
    };
    Label labelSliderValueSecond = new Label
    {
        Text = "50",
        HorizontalOptions = LayoutOptions.End
    };
    Label labelSliderValueThird = new Label
    {
        Text = "50",
        HorizontalOptions = LayoutOptions.End
    };
    Label labelSliderValueFourth = new Label
    {
        Text = "50",
        HorizontalOptions = LayoutOptions.End
    };
    Slider sliderFirst = new Slider
    {
        Minimum = 0,
        Maximum = 100,
        Value = 50,
        HorizontalOptions = LayoutOptions.Fill
    };
    Slider sliderSecond = new Slider
    {
        Minimum = 0,
        Maximum = 100,
        Value = 50,
        HorizontalOptions = LayoutOptions.Fill
    };
    Slider sliderThird = new Slider
    {
        Minimum = 0,
        Maximum = 100,
        Value = 50,
        HorizontalOptions = LayoutOptions.Fill
    };
    Slider sliderFourth = new Slider
    {
        Minimum = 0,
        Maximum = 100,
        Value = 50,
        HorizontalOptions = LayoutOptions.Fill
    };

    int First = 50;
    int Second = 50;
    int Third = 50;
    int Fourth = 50;

    Grid grid = new Grid();

    AbsoluteLayout al = new AbsoluteLayout();
    public Lumememm()
    {
        al.Children.Add(lumememmPea);
        al.Children.Add(lumememmLSilm);
        al.Children.Add(lumememmRSilm);
        al.Children.Add(lumememmKeha);
        al.Children.Add(lumememmAlus);
        al.Children.Add(lumememmUNoop);
        al.Children.Add(lumememmMNoop);
        al.Children.Add(lumememmBNoop);

        lumememmPea.CornerRadius = pallEsimeneSize;
        lumememmLSilm.CornerRadius = silmSize;
        lumememmRSilm.CornerRadius = silmSize;
        lumememmKeha.CornerRadius = pallTeineSize;
        lumememmAlus.CornerRadius = pallKolmasSize;

        Refresh(100);

        lumememm.Content = al;

        sliderFirst.ValueChanged += Slider_ValueChanged;
        sliderSecond.ValueChanged += Slider_ValueChanged;
        sliderThird.ValueChanged += Slider_ValueChanged;
        sliderFourth.ValueChanged += Slider_ValueChanged;

        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        for (int i = 0; i < 4; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }

        grid.Children.Add(labelSliderFirst);
        grid.Children.Add(sliderFirst);
        grid.Children.Add(labelSliderValueFirst);
        Grid.SetRow(labelSliderFirst, 0);
        Grid.SetColumn(labelSliderFirst, 0);
        Grid.SetRow(sliderFirst, 0);
        Grid.SetColumn(sliderFirst, 1);
        Grid.SetRow(labelSliderValueFirst, 0);
        Grid.SetColumn(labelSliderValueFirst, 2);

        grid.Children.Add(labelSliderSecond);
        grid.Children.Add(sliderSecond);
        grid.Children.Add(labelSliderValueSecond);
        Grid.SetRow(labelSliderSecond, 1);
        Grid.SetColumn(labelSliderSecond, 0);
        Grid.SetRow(sliderSecond, 1);
        Grid.SetColumn(sliderSecond, 1);
        Grid.SetRow(labelSliderValueSecond, 1);
        Grid.SetColumn(labelSliderValueSecond, 2);

        grid.Children.Add(labelSliderThird);
        grid.Children.Add(sliderThird);
        grid.Children.Add(labelSliderValueThird);
        Grid.SetRow(labelSliderThird, 2);
        Grid.SetColumn(labelSliderThird, 0);
        Grid.SetRow(sliderThird, 2);
        Grid.SetColumn(sliderThird, 1);
        Grid.SetRow(labelSliderValueThird, 2);
        Grid.SetColumn(labelSliderValueThird, 2);

        grid.Children.Add(labelSliderFourth);
        grid.Children.Add(sliderFourth);
        grid.Children.Add(labelSliderValueFourth);
        Grid.SetRow(labelSliderFourth, 3);
        Grid.SetColumn(labelSliderFourth, 0);
        Grid.SetRow(sliderFourth, 3);
        Grid.SetColumn(sliderFourth, 1);
        Grid.SetRow(labelSliderValueFourth, 3);
        Grid.SetColumn(labelSliderValueFourth, 2);

        vsl.Children.Add(picker);
        vsl.Children.Add(lumememm);
        vsl.Children.Add(grid);
        sv.Content = vsl;
        Content = sv;
    }
    private int CalcPallKeskus(int pallSize)
    {
        return pageWidth / 2 - pallSize / 2;
    }
    private void Slider_ValueChanged(object? sender, ValueChangedEventArgs e)
    {
        if (sender == sliderFirst)
        {
            First = (int)e.NewValue;
            pallEsimeneSize = (int)e.NewValue;
            Refresh((int)e.NewValue);
        }
        else if (sender == sliderSecond)
        {
            Second = (int)e.NewValue;
        }
        else if (sender == sliderThird)
        {
            Third = (int)e.NewValue;
        }
        else if (sender == sliderFourth)
        {
            Fourth = (int)e.NewValue;
        }

        labelSliderValueFirst.Text = First.ToString();
        labelSliderValueSecond.Text = Second.ToString();
        labelSliderValueThird.Text = Third.ToString();
        labelSliderValueFourth.Text = Fourth.ToString();
    }

    void Refresh(int Value)
    {
        al.SetLayoutBounds(lumememmPea, new Rect(CalcPallKeskus(pallEsimeneSize), CalcPallKeskus(pallEsimeneSize) - pallEsimeneSize, pallEsimeneSize, pallEsimeneSize));

        al.SetLayoutBounds(lumememmLSilm, new Rect(CalcPallKeskus(pallEsimeneSize) + silmSize, CalcPallKeskus(pallEsimeneSize) - pallEsimeneSize + silmSize, silmSize, silmSize));
        al.SetLayoutBounds(lumememmRSilm, new Rect(pageWidth - CalcPallKeskus(pallEsimeneSize) - silmSize * 2, CalcPallKeskus(pallEsimeneSize) - pallEsimeneSize + silmSize, silmSize, silmSize));

        al.SetLayoutBounds(lumememmKeha, new Rect(CalcPallKeskus(pallTeineSize), CalcPallKeskus(pallTeineSize), pallTeineSize, pallTeineSize));
        al.SetLayoutBounds(lumememmAlus, new Rect(CalcPallKeskus(pallKolmasSize), CalcPallKeskus(pallKolmasSize) + pallEsimeneSize, pallKolmasSize, pallKolmasSize));

        int OldCalc1 = CalcPallKeskus(silmSize) - silmSize * 2;
        int OldCalc2 = CalcPallKeskus(silmSize);
        int OldCalc3 = CalcPallKeskus(silmSize) + silmSize * 2;

        int NewCalc1 = CalcPallKeskus(silmSize);
        int NewCalc2 = CalcPallKeskus(silmSize) + silmSize * 2;
        int NewCalc3 = CalcPallKeskus(silmSize) + silmSize * 4;

        al.SetLayoutBounds(lumememmUNoop, new Rect(CalcPallKeskus(silmSize), Lerp(OldCalc1, NewCalc1, Value), silmSize, silmSize));
        al.SetLayoutBounds(lumememmMNoop, new Rect(CalcPallKeskus(silmSize), Lerp(OldCalc2, NewCalc2, Value), silmSize, silmSize));
        al.SetLayoutBounds(lumememmBNoop, new Rect(CalcPallKeskus(silmSize), Lerp(OldCalc3, NewCalc3, Value), silmSize, silmSize));
    }
    int Lerp(int oldValue, int newValue, int percentage)
    {
        return oldValue + (newValue - oldValue) * percentage / 100;
    }
}
