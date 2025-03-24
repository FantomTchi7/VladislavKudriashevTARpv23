using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Naidis_App
{
    public class EuroopaRiigid : ContentPage
    {
        private readonly AppDbContext _dbContext;
        private ObservableCollection<Riik> _riigid;

        private readonly ScrollView _scrollView = new ScrollView();
        private readonly ListView _listView = new ListView();

        public EuroopaRiigid()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Filename={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "naidisapp.db")}")
                .Options;

            _dbContext = new AppDbContext(options);
            _dbContext.Database.EnsureCreated();

            _riigid = new ObservableCollection<Riik>(_dbContext.Riik.ToList());
            _listView.ItemsSource = _riigid;

            _listView.ItemTemplate = new DataTemplate(() =>
            {
                var grid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Star }
                    }
                };

                var flagImage = new Image
                {
                    Aspect = Aspect.AspectFit
                };
                flagImage.SetBinding(Image.SourceProperty, "Lipp");
                grid.Children.Add(flagImage);
                grid.SetRow(flagImage, 0);
                grid.SetColumn(flagImage, 0);

                var textStack = new StackLayout
                {
                    Orientation = StackOrientation.Vertical
                };

                var nameLabel = new Label { FontAttributes = FontAttributes.Bold };
                nameLabel.SetBinding(Label.TextProperty, "Nimi");
                textStack.Children.Add(nameLabel);

                var capitalLabel = new Label { FontSize = 12 };
                capitalLabel.SetBinding(Label.TextProperty, "Pealinn");
                textStack.Children.Add(capitalLabel);

                var populationLabel = new Label { FontSize = 12 };
                populationLabel.SetBinding(Label.TextProperty, "Rahvastik");
                textStack.Children.Add(populationLabel);

                grid.Children.Add(textStack);
                grid.SetRow(textStack, 0);
                grid.SetColumn(textStack, 1);

                return new ViewCell { View = grid };
            });

            _scrollView.Content = _listView;
            Content = _scrollView;
        }
    }
}