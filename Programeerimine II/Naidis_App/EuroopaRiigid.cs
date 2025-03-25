using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace Naidis_App
{
    public class EuroopaRiigid : ContentPage
    {
        private readonly AppDbContext _dbContext;
        private ObservableCollection<Riik> _riigid;

        ScrollView sv = new ScrollView();
        ListView lv = new ListView();

        public EuroopaRiigid()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Filename={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "naidisapp.db")}")
                .Options;

            _dbContext = new AppDbContext(options);
            _dbContext.Database.Migrate();

            _riigid = new ObservableCollection<Riik>(_dbContext.Riik.ToList());

            lv = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                ItemsSource = _riigid,
                HasUnevenRows = true
            };

            lv.ItemTemplate = new DataTemplate(() =>
            {
                Grid grid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Star },
                        new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star ) }
                    }
                };

                IModel model = _dbContext.Model;
                IEntityType entityType = model.GetEntityTypes().FirstOrDefault(et => et.ClrType.Name == "Riik");

                if (entityType != null)
                {
                    StackLayout textStack = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        VerticalOptions = LayoutOptions.Center
                    };

                    List<IProperty> properties = entityType.GetProperties()
                        .Where(p => p.Name != "Id")
                        .OrderBy(p => GetColumnPriority(p.Name))
                        .ToList();

                    foreach (IProperty property in properties)
                    {
                        string columnName = property.Name;
                        string columnType = property.ClrType.Name;

                        View cellView = CreateCellView(columnName, columnType);

                        if (cellView is Label && columnName != "Lipp")
                        {
                            textStack.Children.Add(cellView);
                        }
                        else if (columnName == "Lipp")
                        {
                            grid.Children.Add(cellView);
                            grid.SetColumn(cellView, 1);
                        }
                    }

                    grid.Children.Add(textStack);
                    grid.SetColumn(textStack, 0);
                }
                else
                {
                    Debug.WriteLine("Entity 'Riik' not found.");
                }

                return new ViewCell { View = grid };
            });

            sv.Content = lv;
            Content = sv;
        }

        private int GetColumnPriority(string columnName)
        {
            return columnName switch
            {
                "Nimi" => 0,
                _ => 1
            };
        }

        private View CreateCellView(string columnName, string columnType)
        {
            switch (columnName)
            {
                case "Lipp":
                    Image flagImage = new Image
                    {
                        Aspect = Aspect.AspectFit,
                        VerticalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.End
                    };
                    flagImage.SetBinding(Image.SourceProperty, "Lipp");
                    return flagImage;

                case "Nimi":
                    Label nameLabel = new Label
                    {
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 14,
                        LineBreakMode = LineBreakMode.WordWrap
                    };
                    nameLabel.SetBinding(Label.TextProperty, "Nimi");
                    return nameLabel;

                default:
                    return CreateGenericLabel(columnName, columnType);
            }
        }

        private Label CreateGenericLabel(string columnName, string columnType)
        {
            Label label = new Label
            {
                FontSize = 12,
                LineBreakMode = LineBreakMode.WordWrap
            };

            switch (columnType)
            {
                case "String":
                    label.SetBinding(Label.TextProperty, new Binding(columnName, stringFormat: $"{columnName}: {{0}}"));
                    break;
                case "Int32":
                    label.SetBinding(Label.TextProperty, new Binding(columnName, stringFormat: $"{columnName}: {{0:N0}}"));
                    break;
                default:
                    label.SetBinding(Label.TextProperty, new Binding(columnName, stringFormat: $"{columnName}: {{0}}"));
                    break;
            }

            return label;
        }
    }
}