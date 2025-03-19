using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Naidis_App
{
    internal class EuroopaRiigid
    {
        private readonly AppDbContext _dbContext;
        private ObservableCollection<Riik> _riigid;

        ScrollView sv = new ScrollView();
        ListView lv = new ListView();

        public EuroopaRiigid()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Filename={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "naidisapp.db")}")
                .Options;

            _dbContext = new AppDbContext(options);
            _dbContext.Database.EnsureCreated();

            // Fetching the countries from the database
            _riigid = new ObservableCollection<Riik>(_dbContext.Riik.ToList());

            // Setting the ItemsSource of the ListView to the list of countries
            lv.ItemsSource = _riigid;

            // Define the ItemTemplate to specify how the data should be displayed in each row
            lv.ItemTemplate = new DataTemplate(() =>
            {
                var grid = new Grid();

                // Add Name of the Country
                var nameLabel = new Label();
                nameLabel.SetBinding(Label.TextProperty, "Nimi");
                grid.Children.Add(nameLabel);

                // Add Capital
                var capitalLabel = new Label();
                capitalLabel.SetBinding(Label.TextProperty, "Pealinn");
                grid.Children.Add(capitalLabel);

                // Add Population
                var populationLabel = new Label();
                populationLabel.SetBinding(Label.TextProperty, "Rahvastik");
                grid.Children.Add(populationLabel);

                // Add Flag
                var flagImage = new Image();
                flagImage.SetBinding(Image.SourceProperty, "Lipp");
                grid.Children.Add(flagImage);

                return new ViewCell { View = grid };
            });

            // Set up the ScrollView
            sv.Content = lv;
        }
    }
}