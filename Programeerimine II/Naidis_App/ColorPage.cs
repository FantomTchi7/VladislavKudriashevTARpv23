namespace Naidis_App;

public class ColorPage : ContentPage
{
    AbsoluteLayout al = new AbsoluteLayout();
    Frame frame = new Frame
    {
        BackgroundColor = Color.FromArgb("#FFFFFF")
    };
    Label labelSliderR = new Label
    {
        Text = "R",
        HorizontalOptions = LayoutOptions.Start
    };
    Label labelSliderG = new Label
    {
        Text = "G",
        HorizontalOptions = LayoutOptions.Start
    };
    Label labelSliderB = new Label
    {
        Text = "B",
        HorizontalOptions = LayoutOptions.Start
    };
    Label labelSliderA = new Label
    {
        Text = "A",
        HorizontalOptions = LayoutOptions.Start
    };
    Label labelSliderValueR = new Label
    {
        Text = "255",
        HorizontalOptions = LayoutOptions.End
    };
    Label labelSliderValueG = new Label
    {
        Text = "255",
        HorizontalOptions = LayoutOptions.End
    };
    Label labelSliderValueB = new Label
    {
        Text = "255",
        HorizontalOptions = LayoutOptions.End
    };
    Label labelSliderValueA = new Label
    {
        Text = "255",
        HorizontalOptions = LayoutOptions.End
    };
    Slider sliderR = new Slider
    {
        Minimum = 0,
        Maximum = 255,
        HorizontalOptions = LayoutOptions.Fill
    };
    Slider sliderG = new Slider
    {
        Minimum = 0,
        Maximum = 255,
        HorizontalOptions = LayoutOptions.Fill
    };
    Slider sliderB = new Slider
    {
        Minimum = 0,
        Maximum = 255,
        HorizontalOptions = LayoutOptions.Fill
    };
    Slider sliderA = new Slider
    {
        Minimum = 0,
        Maximum = 255,
        HorizontalOptions = LayoutOptions.Fill
    };

    Grid gridR = new Grid();
    Grid gridG = new Grid();
    Grid gridB = new Grid();
    Grid gridA = new Grid();

    int pageHeight = (int)(DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density);
    int pageWidth = (int)(DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density);

    public ColorPage()
    {
        sliderR.ValueChanged += SliderR_ValueChanged;
        sliderG.ValueChanged += SliderG_ValueChanged;
        sliderB.ValueChanged += SliderB_ValueChanged;
        sliderA.ValueChanged += SliderA_ValueChanged;

        gridR.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        gridR.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        gridR.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        gridG.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        gridG.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        gridG.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        gridB.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        gridB.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        gridB.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        gridA.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        gridA.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        gridA.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        gridR.Children.Add(labelSliderR);
        gridR.Children.Add(sliderR);
        gridR.Children.Add(labelSliderValueR);

        gridG.Children.Add(labelSliderG);
        gridG.Children.Add(sliderG);
        gridG.Children.Add(labelSliderValueG);

        gridB.Children.Add(labelSliderB);
        gridB.Children.Add(sliderB);
        gridB.Children.Add(labelSliderValueB);

        gridA.Children.Add(labelSliderA);
        gridA.Children.Add(sliderA);
        gridA.Children.Add(labelSliderValueA);

        Grid.SetColumn(labelSliderR, 0);
        Grid.SetColumn(sliderR, 1);
        Grid.SetColumn(labelSliderValueR, 2);

        Grid.SetColumn(labelSliderG, 0);
        Grid.SetColumn(sliderG, 1);
        Grid.SetColumn(labelSliderValueG, 2);

        Grid.SetColumn(labelSliderB, 0);
        Grid.SetColumn(sliderB, 1);
        Grid.SetColumn(labelSliderValueB, 2);

        Grid.SetColumn(labelSliderA, 0);
        Grid.SetColumn(sliderA, 1);
        Grid.SetColumn(labelSliderValueA, 2);

        al.Children.Add(frame);
        al.Children.Add(gridR);
        al.Children.Add(gridG);
        al.Children.Add(gridB);
        al.Children.Add(gridA);

        Content = al;

        al.SetLayoutBounds(frame, new Rect(0, 0, pageWidth, pageWidth));
        al.SetLayoutBounds(gridR, new Rect(0, pageWidth, pageWidth, 25));
        al.SetLayoutBounds(gridG, new Rect(0, pageWidth + 25, pageWidth, 25));
        al.SetLayoutBounds(gridB, new Rect(0, pageWidth + 50, pageWidth, 25));
        al.SetLayoutBounds(gridA, new Rect(0, pageWidth + 75, pageWidth, 25));
    }
    private void SliderR_ValueChanged(object? sender, ValueChangedEventArgs e)
    {
        int R;
        int G;
        int B;
        int A;
        labelSliderValueR.Text = ((int)(e.NewValue)).ToString();
        frame.BackgroundColor = Color.FromRgb(255, 255, 255);
    }
    private void SliderG_ValueChanged(object? sender, ValueChangedEventArgs e)
    {
        int R;
        int G;
        int B;
        int A;
        labelSliderValueG.Text = ((int)(e.NewValue)).ToString();
        frame.BackgroundColor = Color.FromRgb(255, 255, 255);
    }
    private void SliderB_ValueChanged(object? sender, ValueChangedEventArgs e)
    {
        int R;
        int G;
        int B;
        int A;
        labelSliderValueB.Text = ((int)(e.NewValue)).ToString();
        frame.BackgroundColor = Color.FromRgb(255, 255, 255);
    }
    private void SliderA_ValueChanged(object? sender, ValueChangedEventArgs e)
    {
        int R;
        int G;
        int B;
        int A;
        labelSliderValueA.Text = ((int)(e.NewValue)).ToString();
        frame.BackgroundColor = Color.FromRgb(255, 255, 255);
    }
}