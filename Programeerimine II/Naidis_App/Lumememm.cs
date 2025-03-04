using Microsoft.Maui.Controls.Shapes;
namespace Naidis_App;
public class Lumememm : ContentPage
{
    int leheKõrgus, leheLaius, esimesePalliSuurus, algneEsimesePalliSuurus, silmaSuurus, nupuSuurus, teisePalliSuurus, kolmandaPalliSuurus, porgandiSuurus, torukübaraSuurus, oksaSuurus, peaKeskus, peaY, torukübaraKeskus, torukübaraY, silmaKeskus, vasakSilmX, silmY, porgandiKeskus, porgandiY, kehaKeskus, oksaKeskus, vasakOksX, oksY, alusKeskus, alpha, ülemineNuppAlgKeskus, ülemineNuppLõppKeskus, ülemineNuppSuurus, nupuKeskus;
    ScrollView kerimisVaade = new ScrollView();
    VerticalStackLayout vertikaalnePaigutus = new VerticalStackLayout();
    AbsoluteLayout absoluutnePaigutus = new AbsoluteLayout { BackgroundColor = Color.FromArgb("#000000") };
    Border lumememmPiir = new Border { Padding = 0, Margin = 0 };
    Border peaPiir = new Border { Padding = 0, Margin = 0 };
    Border kehaPiir = new Border { Padding = 0, Margin = 0 };
    Border alusPiir = new Border { Padding = 0, Margin = 0 };
    Border vasakSilmPiir = new Border { Padding = 0, Margin = 0 };
    Border paremSilmPiir = new Border { Padding = 0, Margin = 0 };
    Border ülemineNuppPiir = new Border { Padding = 0, Margin = 0 };
    Border keskmineNuppPiir = new Border { Padding = 0, Margin = 0 };
    Border alumineNuppPiir = new Border { Padding = 0, Margin = 0 };
    Border porgandiPiir = new Border { Padding = 0, Margin = 0 };
    Border torukübaraPiir = new Border { Padding = 0, Margin = 0 };
    Border vasakOksPiir = new Border { Padding = 0, Margin = 0 };
    Border paremOksPiir = new Border { Padding = 0, Margin = 0 };
    Image vasakOks = new Image { Source = "l_stick.png" };
    Image paremOks = new Image { Source = "r_stick.png" };
    Image torukübar = new Image { Source = "top_hat.png" };
    Image porgand = new Image { Source = "carrot.png" };
    Image kehaLumi = new Image { Source = "snow.png" };
    Image alusLumi = new Image { Source = "snow.png" };
    Image peaLumi = new Image { Source = "snow.png" };
    Image ülemineNuppPilt;
    Button näitaPeidaNupp = new Button { Text = "Peida" };
    int esimeneVäärtus = 0;
    bool animatsioonKäib = false;
    bool näidatud = false;
    public Lumememm()
    {
        InitsialiseeriSuurused();
        InitsialiseeriPiirid();
        EhitaKasutajaliides();
    }
    void InitsialiseeriSuurused()
    {
        leheKõrgus = (int)(DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density);
        leheLaius = (int)(DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density);
        lumememmPiir.WidthRequest = leheLaius;
        lumememmPiir.HeightRequest = leheLaius;
        esimesePalliSuurus = leheLaius / 5;
        algneEsimesePalliSuurus = esimesePalliSuurus;
        silmaSuurus = esimesePalliSuurus / 5;
        nupuSuurus = silmaSuurus;
        porgandiSuurus = silmaSuurus * 2;
        oksaSuurus = (int)(teisePalliSuurus / (float)1.5);
        torukübaraSuurus = (int)(esimesePalliSuurus / (float)1.5);
        teisePalliSuurus = (int)(esimesePalliSuurus * (float)1.25);
        kolmandaPalliSuurus = (int)(teisePalliSuurus * (float)1.5);
    }
    void InitsialiseeriPiirid()
    {
        peaPiir = LooPiir(esimesePalliSuurus);
        kehaPiir = LooPiir(teisePalliSuurus);
        alusPiir = LooPiir(kolmandaPalliSuurus);
        vasakSilmPiir = LooPiir(silmaSuurus);
        paremSilmPiir = LooPiir(silmaSuurus);
        ülemineNuppPiir = LooPiir(nupuSuurus);
        keskmineNuppPiir = LooPiir(nupuSuurus);
        alumineNuppPiir = LooPiir(nupuSuurus);
        porgandiPiir = LooPiir(porgandiSuurus);
        torukübaraPiir = LooPiir(torukübaraSuurus);
        vasakOksPiir = LooPiir(oksaSuurus);
        paremOksPiir = LooPiir(oksaSuurus);
        foreach (Border piir in new[] { peaPiir, vasakSilmPiir, paremSilmPiir, porgandiPiir, torukübaraPiir, kehaPiir, vasakOksPiir, paremOksPiir, alusPiir, alumineNuppPiir, keskmineNuppPiir, ülemineNuppPiir })
        {
            absoluutnePaigutus.Children.Add(piir);
        }
    }
    Border LooPiir(int nurgaRaadius) => new Border
    {
        StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(nurgaRaadius) },
        StrokeThickness = 0
    };
    void EhitaKasutajaliides()
    {
        peaPiir.Content = peaLumi;
        kehaPiir.Content = kehaLumi;
        vasakOksPiir.Content = vasakOks;
        paremOksPiir.Content = paremOks;
        alusPiir.Content = alusLumi;
        vasakSilmPiir.Content = new Image { Source = "button.png" };
        paremSilmPiir.Content = new Image { Source = "button.png" };
        ülemineNuppPiir.Content = new Image { Source = "button.png" };
        keskmineNuppPiir.Content = new Image { Source = "button.png" };
        alumineNuppPiir.Content = new Image { Source = "button.png" };
        porgandiPiir.Content = porgand;
        torukübaraPiir.Content = torukübar;
        lumememmPiir.Content = absoluutnePaigutus;
        näitaPeidaNupp.Clicked += NäitaPeidaNupp_Clicked;
        vertikaalnePaigutus.Children.Add(näitaPeidaNupp);
        vertikaalnePaigutus.Children.Add(lumememmPiir);
        kerimisVaade.Content = vertikaalnePaigutus;
        Content = kerimisVaade;
        Värskenda();
    }
    async Task AnimeeriLumememm(bool näita)
    {
        if (animatsioonKäib) return;
        animatsioonKäib = true;
        int algVäärtus = esimeneVäärtus;
        int lõppVäärtus = näita ? 100 : 0;
        await Task.Run(() =>
        {
            Dispatcher.Dispatch(() =>
            {
                Animation anim = new Animation(v =>
                {
                    esimeneVäärtus = (int)v;
                    Värskenda();
                }, algVäärtus, lõppVäärtus);
                anim.Commit(this, "lumememmAnimatsioon", length: 500, easing: Easing.SinInOut, finished: (v, c) =>
                {
                    esimeneVäärtus = lõppVäärtus;
                    Värskenda();
                    animatsioonKäib = false;
                });
            });
        });
    }
    async void NäitaPeidaNupp_Clicked(object sender, EventArgs e)
    {
        näidatud = !näidatud;
        näitaPeidaNupp.Text = näidatud ? "Peida" : "Näita";
        await AnimeeriLumememm(näidatud);
    }
    int ArvutaPalliKeskus(int suurus) => leheLaius / 2 - suurus / 2;
    int LineaarneInterpolatsioon(int algus, int lõpp, int protsent) => algus + (lõpp - algus) * protsent / 100;
    void Värskenda()
    {
        esimesePalliSuurus = (int)(algneEsimesePalliSuurus * esimeneVäärtus / (float)100);
        peaKeskus = ArvutaPalliKeskus(esimesePalliSuurus);
        peaY = peaKeskus - esimesePalliSuurus;
        absoluutnePaigutus.SetLayoutBounds(peaPiir, new Rect(peaKeskus, peaY, esimesePalliSuurus, esimesePalliSuurus));
        torukübaraKeskus = ArvutaPalliKeskus(torukübaraSuurus);
        torukübaraY = peaY - (int)(torukübaraSuurus * (float)0.8);
        absoluutnePaigutus.SetLayoutBounds(torukübaraPiir, new Rect(torukübaraKeskus, torukübaraY, torukübaraSuurus, torukübaraSuurus));
        silmaKeskus = ArvutaPalliKeskus(silmaSuurus);
        vasakSilmX = LineaarneInterpolatsioon(silmaKeskus, peaKeskus + silmaSuurus, esimeneVäärtus);
        silmY = LineaarneInterpolatsioon(silmaKeskus, peaY + silmaSuurus, esimeneVäärtus);
        absoluutnePaigutus.SetLayoutBounds(vasakSilmPiir, new Rect(vasakSilmX, silmY, silmaSuurus, silmaSuurus));
        absoluutnePaigutus.SetLayoutBounds(paremSilmPiir, new Rect(leheLaius - vasakSilmX - silmaSuurus, silmY, silmaSuurus, silmaSuurus));
        porgandiKeskus = ArvutaPalliKeskus(porgandiSuurus);
        porgandiY = LineaarneInterpolatsioon(porgandiKeskus, peaY + porgandiSuurus, esimeneVäärtus);
        absoluutnePaigutus.SetLayoutBounds(porgandiPiir, new Rect(porgandiKeskus, porgandiY, porgandiSuurus, porgandiSuurus));
        kehaKeskus = ArvutaPalliKeskus(teisePalliSuurus);
        absoluutnePaigutus.SetLayoutBounds(kehaPiir, new Rect(kehaKeskus, kehaKeskus, teisePalliSuurus, teisePalliSuurus));
        oksaKeskus = ArvutaPalliKeskus(oksaSuurus);
        vasakOksX = LineaarneInterpolatsioon(oksaKeskus, leheLaius - kehaKeskus - oksaSuurus * 2 - silmaSuurus, esimeneVäärtus);
        oksY = LineaarneInterpolatsioon(oksaKeskus, kehaKeskus - teisePalliSuurus + oksaSuurus, esimeneVäärtus);
        absoluutnePaigutus.SetLayoutBounds(vasakOksPiir, new Rect(vasakOksX, oksY, oksaSuurus, oksaSuurus));
        absoluutnePaigutus.SetLayoutBounds(paremOksPiir, new Rect(leheLaius - vasakOksX - oksaSuurus, oksY, oksaSuurus, oksaSuurus));
        alusKeskus = ArvutaPalliKeskus(kolmandaPalliSuurus);
        absoluutnePaigutus.SetLayoutBounds(alusPiir, new Rect(alusKeskus, alusKeskus + esimesePalliSuurus, kolmandaPalliSuurus, kolmandaPalliSuurus));
        ülemineNuppPilt = (Image)ülemineNuppPiir.Content;
        ülemineNuppPilt.Opacity = esimeneVäärtus / (float)100;
        alpha = 255 - (int)(esimeneVäärtus / (float)100 * 255);
        ülemineNuppPiir.BackgroundColor = Color.FromRgba(0, 0, 0, alpha / (float)255);
        ülemineNuppAlgKeskus = ArvutaPalliKeskus(kolmandaPalliSuurus + 2);
        ülemineNuppLõppKeskus = ArvutaPalliKeskus(nupuSuurus);
        ülemineNuppSuurus = LineaarneInterpolatsioon(kolmandaPalliSuurus + 2, nupuSuurus, esimeneVäärtus);
        absoluutnePaigutus.SetLayoutBounds(ülemineNuppPiir, new Rect(LineaarneInterpolatsioon(ülemineNuppAlgKeskus, ülemineNuppLõppKeskus, esimeneVäärtus), LineaarneInterpolatsioon(ülemineNuppAlgKeskus, ülemineNuppLõppKeskus, esimeneVäärtus), ülemineNuppSuurus, ülemineNuppSuurus));
        ülemineNuppPiir.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(LineaarneInterpolatsioon(kolmandaPalliSuurus, nupuSuurus, esimeneVäärtus)) };
        nupuKeskus = ArvutaPalliKeskus(nupuSuurus);
        absoluutnePaigutus.SetLayoutBounds(keskmineNuppPiir, new Rect(nupuKeskus, LineaarneInterpolatsioon(nupuKeskus, nupuKeskus + nupuSuurus * 2, esimeneVäärtus), nupuSuurus, nupuSuurus));
        absoluutnePaigutus.SetLayoutBounds(alumineNuppPiir, new Rect(nupuKeskus, LineaarneInterpolatsioon(nupuKeskus, nupuKeskus + nupuSuurus * 4, esimeneVäärtus), nupuSuurus, nupuSuurus));
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(50), () =>
        {
            näidatud = true;
            näitaPeidaNupp.Text = "Peida";
            AnimeeriLumememm(näidatud);
        });
    }
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        InitsialiseeriSuurused();
        Värskenda();
    }
}