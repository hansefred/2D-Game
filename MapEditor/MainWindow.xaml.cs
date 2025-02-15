﻿using GameEngine.Model;
using GameEngine.Model.MapDefinitions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
            MapTypes.Add(new WallType() { Name = "Wall", Color = Brushes.Black });
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

                SetMapType(LeftMouseSelected);

                        
            }
        }

        private void Button_Many_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in RighMouseSelected)
            {
                SetMapType(item);
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

        private void SetMapType (WPFMapObject wPFMapObject)
        {

            if (wPFMapObject.MapType is SpawnerType)
            {
                var output = lb_SelectType.SelectedItem as SpawnerType;
                wPFMapObject.MapType = output.ShallowCopy();
            }
            else if (wPFMapObject.MapType is WallType)
            {
                var output = lb_SelectType.SelectedItem as WallType;
                wPFMapObject.MapType = output.ShallowCopy();
            }
            else
            {
                var output = lb_SelectType.SelectedItem as MapType;
                wPFMapObject.MapType = output.ShallowCopy();
            }


        
        }

        private void Button_SaveMap_Click(object sender, RoutedEventArgs e)
        {           
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON file (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog.ShowDialog();

            var filepath = saveFileDialog.FileName;
            var DirPath = System.IO.Path.GetDirectoryName(filepath);
            if (!Directory.Exists(DirPath))
            {
                Directory.CreateDirectory(DirPath);
            }

            var Definition = new MapDefinition () { Name = filepath.Substring(filepath.LastIndexOf("\\")+1,filepath.LastIndexOf(".")- filepath.LastIndexOf("\\")-1), MapObjects = ExportToMapObjects (), MapSizeX = SizeX, MapSizeY = SizeY };

            Definition.MapSizeX = 1;
            Definition.MapSizeY = 1;

            Map.SaveMap (DirPath, Definition);

            
            


        }



        private List<Map_Object> ExportToMapObjects ()
        {
            var output = new List<Map_Object>();

            foreach (var element in WPFMapObjects)
            {
                output.Add(element.ToMap_Object());
            }

            return output;
        }
    }
}
