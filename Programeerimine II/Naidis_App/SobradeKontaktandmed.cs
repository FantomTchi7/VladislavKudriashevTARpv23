namespace Naidis_App;

public class SobradeKontaktandmed : ContentPage
{
    static int displayHeight = (int)(DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density);
    static int displayWidth = (int)(DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density);

    ScrollView sv = new ScrollView();
    Grid grid = new Grid
    {
        RowDefinitions =
        {
            new RowDefinition { Height = GridLength.Auto },
            new RowDefinition { Height = GridLength.Auto },
            new RowDefinition { Height = GridLength.Auto },
            new RowDefinition { Height = GridLength.Auto }
        },
        ColumnDefinitions =
        {
            new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star ) },
            new ColumnDefinition { Width = GridLength.Star },
            new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star ) }
        }
    };

    Label labelNimi = new Label { Text = "Nimi:" };
    Editor inputNimi = new Editor
    {
        Keyboard = Keyboard.Text
    };
    Label labelEmailVõiTelefon = new Label { Text = "Email või telefon:" };
    Editor inputEmailVõiTelefon = new Editor
    {
        Keyboard = Keyboard.Email
    };
    Dictionary<string, string> kontaktiRaamat = new Dictionary<string, string>();
    Picker kontaktiRaamatPicker = new Picker
    {

    };
    Picker pickerEmailVõiTelefon = new Picker
    {
        ItemsSource = new List<string> { "Email", "Telefon", "Kontaktiraamat" },
        SelectedItem = "Email"
    };
    Label labelKirjeldus = new Label { Text = "Kirjeldus:" };
    Editor inputKirjeldus = new Editor
    {
        Keyboard = Keyboard.Text
    };
    Button buttonSaada = new Button { Text = "Saada Email" };

    public SobradeKontaktandmed()
    {
        pickerEmailVõiTelefon.SelectedIndexChanged += EmailVõiTelefonPicker_SelectedIndexChanged;
        buttonSaada.Clicked += ButtonSaada_Clicked;

        grid.Children.Add(labelNimi);
        grid.Children.Add(inputNimi);

        grid.Children.Add(labelEmailVõiTelefon);
        grid.Children.Add(inputEmailVõiTelefon);
        grid.Children.Add(pickerEmailVõiTelefon);

        grid.Children.Add(labelKirjeldus);
        grid.Children.Add(inputKirjeldus);

        grid.Children.Add(buttonSaada);

        grid.SetColumn(labelNimi, 0);
        grid.SetColumn(inputNimi, 1);
        grid.SetColumnSpan(inputNimi, 2);

        grid.SetColumn(labelEmailVõiTelefon, 0);
        grid.SetColumn(inputEmailVõiTelefon, 1);
        grid.SetColumn(kontaktiRaamatPicker, 1);
        grid.SetColumn(pickerEmailVõiTelefon, 2);

        grid.SetColumn(labelKirjeldus, 0);
        grid.SetColumn(inputKirjeldus, 1);
        grid.SetColumnSpan(inputKirjeldus, 2);

        grid.SetColumn(buttonSaada, 0);
        grid.SetColumnSpan(buttonSaada, 3);

        grid.SetRow(labelNimi, 0);
        grid.SetRow(inputNimi, 0);

        grid.SetRow(labelEmailVõiTelefon, 1);
        grid.SetRow(inputEmailVõiTelefon, 1);
        grid.SetRow(kontaktiRaamatPicker, 1);
        grid.SetRow(pickerEmailVõiTelefon, 1);

        grid.SetRow(labelKirjeldus, 2);
        grid.SetRow(inputKirjeldus, 2);

        grid.SetRow(buttonSaada, 3);

        sv.Content = grid;
        Content = sv;
    }

    private void EmailVõiTelefonPicker_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (pickerEmailVõiTelefon.SelectedItem == "Email")
        {
            if (!grid.Children.Contains(inputEmailVõiTelefon)) { grid.Children.Add(inputEmailVõiTelefon); }
            if (grid.Children.Contains(kontaktiRaamatPicker)) { grid.Children.Remove(kontaktiRaamatPicker); }
            inputEmailVõiTelefon.Keyboard = Keyboard.Email;
            buttonSaada.Text = "Saada Email";
        }
        else if (pickerEmailVõiTelefon.SelectedItem == "Telefon")
        {
            if (!grid.Children.Contains(inputEmailVõiTelefon)) { grid.Children.Add(inputEmailVõiTelefon); }
            if (grid.Children.Contains(kontaktiRaamatPicker)) { grid.Children.Remove(kontaktiRaamatPicker); }
            inputEmailVõiTelefon.Keyboard = Keyboard.Telephone;
            buttonSaada.Text = "Saada SMS";
        }
        else
        {
            if (grid.Children.Contains(inputEmailVõiTelefon)) { grid.Children.Remove(inputEmailVõiTelefon); }
            if (!grid.Children.Contains(kontaktiRaamatPicker)) { grid.Children.Add(kontaktiRaamatPicker); }
            buttonSaada.Text = "Saada Email";
        }
    }

    private async void ButtonSaada_Clicked(object? sender, EventArgs e)
    {
        if (pickerEmailVõiTelefon.SelectedItem == "Email")
        {
            string message = "Tere tulemast! Saadan email.";
            EmailMessage email = new EmailMessage
            {
                Subject = inputEmailVõiTelefon.Text,
                Body = inputKirjeldus.Text,
                BodyFormat = EmailBodyFormat.PlainText,
                To = new List<string>(new[] { inputEmailVõiTelefon.Text })
            };
            if (Email.Default.IsComposeSupported)
            {
                await Email.Default.ComposeAsync(email);
            }
        }
        else
        {
            string phone = inputEmailVõiTelefon.Text;
            string message = "Tere tulemast! Saadan sõnumi.";
            SmsMessage sms = new SmsMessage(message, phone);
            if (phone != null && Sms.Default.IsComposeSupported)
            {
                await Sms.Default.ComposeAsync(sms);
            }
        }
    }
}
