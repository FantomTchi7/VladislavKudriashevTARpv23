using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Naidis_App
{
    public class EuroopaRiigid : ContentPage
    {
        private readonly AppDbContext _dbContext;
        private ObservableCollection<Riik> _riigid;

        private readonly ScrollView _scrollView = new ScrollView();
        private readonly ListView _listView = new ListView();

        Grid grid;
        Image flagImage;
        StackLayout textStack;
        Label nameLabel;
        Label capitalLabel;
        Label populationLabel;

        public EuroopaRiigid()
        {
            DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Filename={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "naidisapp.db")}")
                .Options;

            _dbContext = new AppDbContext(options);
            _dbContext.Database.EnsureCreated();

            _riigid = new ObservableCollection<Riik>(_dbContext.Riik.ToList());
            _listView.ItemsSource = _riigid;

            _listView.ItemTemplate = new DataTemplate(() =>
            {
                grid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Star },
                        new ColumnDefinition { Width = GridLength.Auto }
                    }
                };

                flagImage = new Image
                {
                    Aspect = Aspect.AspectFit
                };
                flagImage.SetBinding(Image.SourceProperty, "Lipp");
                grid.Children.Add(flagImage);
                grid.SetColumn(flagImage, 1);

                textStack = new StackLayout
                {
                    Orientation = StackOrientation.Vertical
                };

                nameLabel = new Label { FontAttributes = FontAttributes.Bold };
                nameLabel.SetBinding(Label.TextProperty, "Nimi");
                textStack.Children.Add(nameLabel);

                capitalLabel = new Label { FontSize = 12 };
                capitalLabel.SetBinding(Label.TextProperty, new Binding("Pealinn", stringFormat: "Pealinn: {0}"));
                textStack.Children.Add(capitalLabel);

                populationLabel = new Label { FontSize = 12 };
                populationLabel.SetBinding(Label.TextProperty, new Binding("Rahvastik")
                {
                    StringFormat = "Rahvastik: {0:N0}"
                });
                textStack.Children.Add(populationLabel);

                grid.Children.Add(textStack);
                grid.SetColumn(textStack, 0);

                return new ViewCell { View = grid };
            });

            _scrollView.Content = _listView;
            Content = _scrollView;
        }
    }
}