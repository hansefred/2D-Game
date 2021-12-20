using GameEngine.Model.MapDefinitions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public int SizeX { get; set; }
        public int SizeY { get; set; }


        public ObservableCollection<MapType> MapTypes { get; set; } = new ObservableCollection<MapType>();


        public ObservableCollection<WPFMapObject> WPFMapObjects { get; set; } = new ObservableCollection<WPFMapObject>();


        public WPFMapObject LeftMouseSelected { get; set; } = null;
        public List<WPFMapObject> RighMouseSelected { get; set; } = new();
        public MainWindow()
        {
            DataContext = this;
            SizeX = 10;
            SizeY = 10;

            InitializeComponent();
            MapTypes.Add(new MapType() { Name = "Default", Color = Brushes.Transparent });
            MapTypes.Add(new MapType() { Name = "Wall", Color = Brushes.Black });
            MapTypes.Add(new SpawnerType() { Name = "Spawner", Color = Brushes.Red });





            for (int i = 0; i < SizeX; i++)
            {
                gd_MapGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int j = 0; j < SizeY; j++)
            {
                gd_MapGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {

                    var  element = new WPFMapObject() { Border = new Border { Background = Brushes.Transparent, Visibility = Visibility.Visible, MinHeight = 1, MinWidth = 1 },MapType = MapTypes.Where(o => o.Name == "Default").First().ShallowCopy(), X = j, Y = i };


                    element.Border.MouseEnter += Element_MouseEnter;
                    element.Border.MouseLeftButtonDown += Element_MouseLeftButtonDown;
                    Grid.SetRow(element.Border, i);
                    Grid.SetColumn(element.Border, j);
                    gd_MapGrid.Children.Add(element.Border);  
                    WPFMapObjects.Add(element);
                }
            }
           

        }

        private void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (LeftMouseSelected != null)
            {
                LeftMouseSelected.Border.BorderThickness = new Thickness();
            }


            var element = GetObject(sender);
            element.Border.BorderBrush = Brushes.Yellow;
            element.Border.BorderThickness = new Thickness(10);


            LeftMouseSelected = element;
            lb_SelectType.SelectedItem = MapTypes.Where((o) => o.Name == element.MapType.Name).FirstOrDefault();
            CC_Type.ContentTemplate = (DataTemplate)Application.Current.FindResource(element.MapType.Name);
            CC_Type.Content = element.MapType;

        }

        private void Element_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                var element = GetObject(sender);

                element.Border.BorderBrush = Brushes.Green;
                element.Border.BorderThickness = new Thickness(10);

                RighMouseSelected.Add(element);
            }
        }

        private void lb_SelectType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CC_Type.ContentTemplate = (DataTemplate)Application.Current.FindResource((lb_SelectType.SelectedItem as MapType).Name);
            CC_Type.Content = lb_SelectType.SelectedItem;
        }

        private void Button_One_Click(object sender, RoutedEventArgs e)
        {
            if (lb_SelectType.SelectedItem != null && LeftMouseSelected != null)
            {

                SetSpawnerType (LeftMouseSelected);

                        
            }
        }

        private void Button_Many_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in RighMouseSelected)
            {
                SetSpawnerType(item);
            }

        }

        private void Button_Many_Clear_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in RighMouseSelected)
            {
                item.Border.BorderThickness = new Thickness();
            }
            RighMouseSelected.Clear();
        }


        private WPFMapObject GetObject (object sender )
        {
            var ob = (sender as UIElement);

            var col = Grid.GetColumn(ob);
            var row = Grid.GetRow(ob);

            return  WPFMapObjects.Where(o => o.X == col && o.Y == row).First();
        }

        private void SetSpawnerType (WPFMapObject wPFMapObject)
        {
            if (lb_SelectType.SelectedItem is SpawnerType)
            {
                var spawner = lb_SelectType.SelectedItem as SpawnerType;
                wPFMapObject.MapType = spawner;
            }
            else  if (lb_SelectType.SelectedItem is WallType)
            {
                var wall = lb_SelectType.SelectedItem as WallType;
                wPFMapObject.MapType = wall;
            }
            else
            {
                var type = lb_SelectType.SelectedItem as MapType;
                wPFMapObject.MapType = type;
            }
        }


    }
}
