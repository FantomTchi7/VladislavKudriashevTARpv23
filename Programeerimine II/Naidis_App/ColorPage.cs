namespace Naidis_App;

public class ColorPage : ContentPage
{
    AbsoluteLayout al = new AbsoluteLayout();
    static List<string> views = new List<string> { "RGBA", "CMYK" };
    Picker picker = new Picker
    {
        ItemsSource = views,
        SelectedIndex = 0
    };
    Label valueLabel = new Label
    {
        Text = "HEX Code: #FFFFFFFF",
        HorizontalOptions = LayoutOptions.Start
    };
    string valueValue = "#FFFFFFFF";
    TapGestureRecognizer valueLabelGR = new TapGestureRecognizer();
    Frame frame = new Frame
    {
        BackgroundColor = Color.FromArgb("#FFFFFF")
    };
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
        Text = "255",
        HorizontalOptions = LayoutOptions.End
    };
    Label labelSliderValueSecond = new Label
    {
        Text = "255",
        HorizontalOptions = LayoutOptions.End
    };
    Label labelSliderValueThird = new Label
    {
        Text = "255",
        HorizontalOptions = LayoutOptions.End
    };
    Label labelSliderValueFourth = new Label
    {
        Text = "255",
        HorizontalOptions = LayoutOptions.End
    };
    Slider sliderFirst = new Slider
    {
        Minimum = 0,
        Maximum = 255,
        Value = 255,
        HorizontalOptions = LayoutOptions.Fill
    };
    Slider sliderSecond = new Slider
    {
        Minimum = 0,
        Maximum = 255,
        Value = 255,
        HorizontalOptions = LayoutOptions.Fill
    };
    Slider sliderThird = new Slider
    {
        Minimum = 0,
        Maximum = 255,
        Value = 255,
        HorizontalOptions = LayoutOptions.Fill
    };
    Slider sliderFourth = new Slider
    {
        Minimum = 0,
        Maximum = 255,
        Value = 255,
        HorizontalOptions = LayoutOptions.Fill
    };

    Grid gridFirst = new Grid();
    Grid gridSecond = new Grid();
    Grid gridThird = new Grid();
    Grid gridFourth = new Grid();

    int First = 255;
    int Second = 255;
    int Third = 255;
    int Fourth = 255;

    int pageHeight = (int)(DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density);
    int pageWidth = (int)(DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density);
    public ColorPage()
    {
        picker.SelectedIndexChanged += Picker_SelectedIndexChanged;

        valueLabelGR.Tapped += ValueLabel_Tapped;
        valueLabel.GestureRecognizers.Add(valueLabelGR);
        sliderFirst.ValueChanged += Slider_ValueChanged;
        sliderSecond.ValueChanged += Slider_ValueChanged;
        sliderThird.ValueChanged += Slider_ValueChanged;
        sliderFourth.ValueChanged += Slider_ValueChanged;

        gridFirst.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        gridFirst.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        gridFirst.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        gridSecond.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        gridSecond.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        gridSecond.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        gridThird.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        gridThird.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        gridThird.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        gridFourth.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        gridFourth.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        gridFourth.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        gridFirst.Children.Add(labelSliderFirst);
        gridFirst.Children.Add(sliderFirst);
        gridFirst.Children.Add(labelSliderValueFirst);

        gridSecond.Children.Add(labelSliderSecond);
        gridSecond.Children.Add(sliderSecond);
        gridSecond.Children.Add(labelSliderValueSecond);

        gridThird.Children.Add(labelSliderThird);
        gridThird.Children.Add(sliderThird);
        gridThird.Children.Add(labelSliderValueThird);

        gridFourth.Children.Add(labelSliderFourth);
        gridFourth.Children.Add(sliderFourth);
        gridFourth.Children.Add(labelSliderValueFourth);

        gridFirst.SetColumn(labelSliderFirst, 0);
        gridFirst.SetColumn(sliderFirst, 1);
        gridFirst.SetColumn(labelSliderValueFirst, 2);

        gridSecond.SetColumn(labelSliderSecond, 0);
        gridSecond.SetColumn(sliderSecond, 1);
        gridSecond.SetColumn(labelSliderValueSecond, 2);

        gridThird.SetColumn(labelSliderThird, 0);
        gridThird.SetColumn(sliderThird, 1);
        gridThird.SetColumn(labelSliderValueThird, 2);

        gridFourth.SetColumn(labelSliderFourth, 0);
        gridFourth.SetColumn(sliderFourth, 1);
        gridFourth.SetColumn(labelSliderValueFourth, 2);

        al.Children.Add(picker);
        al.Children.Add(valueLabel);
        al.Children.Add(frame);
        al.Children.Add(gridFirst);
        al.Children.Add(gridSecond);
        al.Children.Add(gridThird);
        al.Children.Add(gridFourth);

        Content = al;

        al.SetLayoutBounds(picker, new Rect(0, 0, pageWidth, 50));
        al.SetLayoutBounds(valueLabel, new Rect(0, 50, pageWidth, 25));
        al.SetLayoutBounds(frame, new Rect(0, 75, pageWidth, pageWidth));
        al.SetLayoutBounds(gridFirst, new Rect(0, pageWidth + 75, pageWidth, 25));
        al.SetLayoutBounds(gridSecond, new Rect(0, pageWidth + 100, pageWidth, 25));
        al.SetLayoutBounds(gridThird, new Rect(0, pageWidth + 125, pageWidth, 25));
        al.SetLayoutBounds(gridFourth, new Rect(0, pageWidth + 150, pageWidth, 25));
    }
    private void Picker_SelectedIndexChanged(object? sender, EventArgs e)
    {
        int selectedIndex = picker.SelectedIndex;
        if (selectedIndex == 0)
        {
            labelSliderFirst.Text = "R";
            labelSliderSecond.Text = "G";
            labelSliderThird.Text = "B";
            labelSliderFourth.Text = "A";

            sliderFirst.Maximum = 255;
            sliderSecond.Maximum = 255;
            sliderThird.Maximum = 255;
            sliderFourth.Maximum = 255;

            sliderFirst.Value = 255;
            sliderSecond.Value = 255;
            sliderThird.Value = 255;
            sliderFourth.Value = 255;
        }
        else if (selectedIndex == 1)
        {
            labelSliderFirst.Text = "C";
            labelSliderSecond.Text = "M";
            labelSliderThird.Text = "Y";
            labelSliderFourth.Text = "K";

            sliderFirst.Maximum = 100;
            sliderSecond.Maximum = 100;
            sliderThird.Maximum = 100;
            sliderFourth.Maximum = 100;

            sliderFirst.Value = 0;
            sliderSecond.Value = 0;
            sliderThird.Value = 0;
            sliderFourth.Value = 0;
        }
    }
    async private void ValueLabel_Tapped(object? sender, TappedEventArgs e)
    {
        await Clipboard.Default.SetTextAsync(valueValue);
    }
    private void Slider_ValueChanged(object? sender, ValueChangedEventArgs e)
    {
        if (sender == sliderFirst) First = (int)e.NewValue;
        else if (sender == sliderSecond) Second = (int)e.NewValue;
        else if (sender == sliderThird) Third = (int)e.NewValue;
        else if (sender == sliderFourth) Fourth = (int)e.NewValue;

        labelSliderValueFirst.Text = First.ToString();
        labelSliderValueSecond.Text = Second.ToString();
        labelSliderValueThird.Text = Third.ToString();
        labelSliderValueFourth.Text = Fourth.ToString();

        if (picker.SelectedIndex == 0)
        {
            frame.BackgroundColor = Color.FromRgba(First, Second, Third, Fourth);
            valueLabel.Text = $"HEX Code: #{First:X2}{Second:X2}{Third:X2}{Fourth:X2}";
            valueValue = $"#{First:X2}{Second:X2}{Third:X2}{Fourth:X2}";
        }
        else if (picker.SelectedIndex == 1)
        {
            int R = (int)(255 * (1 - First / 100.0) * (1 - Fourth / 100.0));
            int G = (int)(255 * (1 - Second / 100.0) * (1 - Fourth / 100.0));
            int B = (int)(255 * (1 - Third / 100.0) * (1 - Fourth / 100.0));

            frame.BackgroundColor = Color.FromRgba(R, G, B, 255);
            valueLabel.Text = $"CMYK Value: {First}%, {Second}%, {Third}%, {Fourth}%";
            valueValue = $"{First}%, {Second}%, {Third}%, {Fourth}%";
        }
    }
}