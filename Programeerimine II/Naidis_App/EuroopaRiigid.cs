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
        Grid grid = new Grid();
        ListView lv = new ListView();
        Button buttonEdit = new Button { Text = "Muuda riik" };
        Button buttonAdd = new Button { Text = "Lisa riik" };

        public EuroopaRiigid()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Filename={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "naidisapp.db")}")
                .Options;

            _dbContext = new AppDbContext(options);
            _dbContext.Database.EnsureCreated();

            _riigid = new ObservableCollection<Riik>(_dbContext.Riik.ToList());

            lv = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                ItemsSource = _riigid,
                HasUnevenRows = true
            };

            lv.ItemTemplate = new DataTemplate(() =>
            {
                Grid gridItems = new Grid
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
                            gridItems.Children.Add(cellView);
                            gridItems.SetColumn(cellView, 1);
                        }
                    }

                    gridItems.Children.Add(textStack);
                    gridItems.SetColumn(textStack, 0);
                }
                else
                {
                    Debug.WriteLine("Entity 'Riik' not found.");
                }

                return new ViewCell { View = gridItems };
            });

            grid.ColumnDefinitions = new ColumnDefinitionCollection {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            };
            grid.RowDefinitions = new RowDefinitionCollection {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto }
            };

            grid.Children.Add(lv);
            grid.SetRow(lv, 0);
            grid.SetColumn(lv, 0);
            grid.SetColumnSpan(lv, 2);

            grid.Children.Add(buttonEdit);
            grid.SetRow(buttonEdit, 1);
            grid.SetColumn(buttonEdit, 0);

            grid.Children.Add(buttonAdd);
            grid.SetRow(buttonAdd, 1);
            grid.SetColumn(buttonAdd, 1);

            sv.Content = grid;
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