using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Domein;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var response = await Api.Fetch($"https://vtm.be/tv-gids/api/v2/broadcasts/{date}");

            var lijstProgrammas = Api.ParseData(response);
            var lijstOmTeTonen = new List<Programma>();
            int aantalrRow = 0;

            foreach (var item in lijstProgrammas)
            {
                if (!Programma.IsAfgelopen(item) || Programma.IsBezig(item))
                {
                    if (item.Titel != "Geen uitzending")
                    {
                        if (item.Image.Trim() != "")
                        {
                            Debug.WriteLine(item.Image);
                            lijstOmTeTonen.Add(item);
                            if (aantalrRow == 4)
                            {
                                aantalrRow = 0;
                                Grid.RowDefinitions.Add(new RowDefinition());
                            }
                            aantalrRow++;
                        }
                    }
                }
            }

            int aantalProgrammascol = Grid.ColumnDefinitions.Count;
            int aantalProgrammascrow = Grid.RowDefinitions.Count;
            int img = 0;
            int col = 0;
            int row = 0;
            for (var i = 0; i < aantalProgrammascol; i++)
            {
                for (int a = 0; a < aantalProgrammascrow; a++)
                {
                    StackPanel sp = new();
                    sp.Margin = new Thickness(4);
                    var Pr = lijstOmTeTonen[img];

                    var image = new Image
                    {
                        Source = new BitmapImage(new Uri(Pr.Image)),
                        Cursor = Cursors.Hand
                    };


                    image.MouseDown += (sender, e) =>
                    {
                        ShowProgramma(sender, e, Pr);
                    };
                    sp.Children.Add(image);

                    sp.Children.Add(new Label
                    {
                        Content = Pr.Titel,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        FontSize = 14
                    });

                    Grid.SetColumn(sp, col);
                    Grid.SetRow(sp, row);
                    Grid.Children.Add(sp);
                    col++;
                    img++;
                    if (col == 4)
                    {
                        row++;
                        col = 0;
                    }
                }
            }
        }

        private void ShowProgramma(object sender, RoutedEventArgs e, Programma Pr)
        {
            var Dwindow = new Details();

            var Img = new Image
            {
                Source = new BitmapImage(new Uri(Pr.Image))
            };

            Dwindow.programTitle.Content = Pr.Titel;
            Dwindow.image.Source = Img.Source;
            Dwindow.MaxWidth = Dwindow.image.Source.Width - 100;
            Dwindow.MinWidth = 450;
            var date = DateTime.Now.ToLongDateString();
            Dwindow.time.Content = $"{date[(date.IndexOf(" ") + 1)..]}, {Pr.From:HH:mm} - {Pr.To:HH:mm}";

            if (Pr.Duration != "")
            {
                Dwindow.duratie.Content = $"Duurt : {int.Parse(Pr.Duration) / 60} Minuten.";
            }
            else
            {
                StackPanel parent = (StackPanel)VisualTreeHelper.GetParent(Dwindow.beschrijving);
                parent.Children.Remove(Dwindow.beschrijving);
            }

            if (Pr.Beschrijving != "")
            {
                Dwindow.beschrijving.Text = $"Omschrijving : {Pr.Beschrijving}";
            }
            else
            {
                StackPanel parent = (StackPanel)VisualTreeHelper.GetParent(Dwindow.beschrijving);
                parent.Children.Remove(Dwindow.beschrijving);
            }

            Dwindow.Show();
        }
    }
}
