using System;
using System.Collections.Generic;
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

namespace MasterMindWPL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<Color, string> _availableColors = new Dictionary<Color, string>();
        List<string> _code = new List<string>();
        bool _isDebugMode = false;
        int _attempts;
        int _score;
        public MainWindow()
        {
            _score = 100;
            _attempts = 1;
            InitializeComponent();
            AddColorsToDictionary();
            FillComboBoxes();
            GenerateRandomCode();
            TxtPogingen.Text = $"Poging: {_attempts} / 10\nScore: {_score}";
            foreach (string color in _code)
            {
                this.Title = this.Title + " " + color;
                TxtCode.Text = TxtCode.Text + $" {color}";
            }
        }

        public void AddColorsToDictionary()
        {
            _availableColors.Clear();
            _availableColors.Add(Colors.Red, "Red");
            _availableColors.Add(Colors.Blue, "Blue");
            _availableColors.Add(Colors.Green, "Green");
            _availableColors.Add(Colors.Yellow, "Yellow");
            _availableColors.Add(Colors.Orange, "Orange");
            _availableColors.Add(Colors.White, "White");
        }

        public void GenerateRandomCode()
        {
            Random rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                int j = rand.Next(0,5);
                _code.Add(_availableColors.ElementAt(j).Value);
            }
        }

        public void FillComboBoxes()
        {
            foreach (KeyValuePair<Color, string> color in _availableColors)
            {
                cboColors1.Items.Add(color.Value);
                cboColors2.Items.Add(color.Value);
                cboColors3.Items.Add(color.Value);
                cboColors4.Items.Add(color.Value);
            }
        }

        private void cboColors1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Color color = _availableColors.FirstOrDefault(x => x.Value == cboColors1.SelectedItem).Key;
            ellipseColor1.Fill = new SolidColorBrush(color);
        }

        private void cboColors2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Color color = _availableColors.FirstOrDefault(x => x.Value == cboColors2.SelectedItem).Key;
            ellipseColor2.Fill = new SolidColorBrush(color);
        }

        private void cboColors3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Color color = _availableColors.FirstOrDefault(x => x.Value == cboColors3.SelectedItem).Key;
            ellipseColor3.Fill = new SolidColorBrush(color);
        }

        private void cboColors4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Color color = _availableColors.FirstOrDefault(x => x.Value == cboColors4.SelectedItem).Key;
            ellipseColor4.Fill = new SolidColorBrush(color);
        }

        private void btnCheckCode_Click(object sender, RoutedEventArgs e)
        {
            if (_code[0] == cboColors1.SelectedItem)
            {
                ellipseColor1.Stroke = new SolidColorBrush(Colors.DarkRed);
            }
            else if (_code.Contains(cboColors1.SelectedItem))
            {
                ellipseColor1.Stroke = new SolidColorBrush(Colors.Wheat);
                _score--;
            }
            else
            {
                ellipseColor1.Stroke = null;
                _score -= 2;
            }

            if (_code[1] == cboColors2.SelectedItem)
            {
                ellipseColor2.Stroke = new SolidColorBrush(Colors.DarkRed);
            }
            else if (_code.Contains(cboColors2.SelectedItem))
            {
                ellipseColor2.Stroke = new SolidColorBrush(Colors.Wheat);
                _score--;
            }
            else
            {
                ellipseColor2.Stroke = null;
                _score -= 2;
            }

            if (_code[2] == cboColors3.SelectedItem)
            {
                ellipseColor3.Stroke = new SolidColorBrush(Colors.DarkRed);
            }
            else if (_code.Contains(cboColors3.SelectedItem))
            {
                ellipseColor3.Stroke = new SolidColorBrush(Colors.Wheat);
                _score--;
            }
            else
            {
                ellipseColor3.Stroke = null;
                _score -= 2;
            }

            if (_code[3] == cboColors4.SelectedItem)
            {
                ellipseColor4.Stroke = new SolidColorBrush(Colors.DarkRed);
            }
            else if (_code.Contains(cboColors4.SelectedItem))
            {
                ellipseColor4.Stroke = new SolidColorBrush(Colors.Wheat);
                _score--;
            }
            else
            {
                ellipseColor4.Stroke = null;
                _score -= 2;
            }
            CheckAttempt();
            AddToHistory(ellipseColor1, ellipseColor2, ellipseColor3, ellipseColor4);
        }

        public void ToggleDebug()
        {
            _isDebugMode = !_isDebugMode;
            TxtCode.Visibility = _isDebugMode ? Visibility.Visible : Visibility.Collapsed;
        }

        private void TxtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.F12)
            {
                ToggleDebug();
            }
        }

        public void CheckAttempt()
        {
            if (_attempts < 10)
            {
                _attempts++;
            }
            else
            {
                MessageBox.Show("Je hebt het maximaal aantal pogingen bereikt.");
            }
            TxtPogingen.Text = $"Poging: {_attempts} / 10\nScore: {_score}";
        }

        public void AddToHistory(Ellipse ellipse1, Ellipse ellipse2, Ellipse ellipse3, Ellipse ellipse4)
        {
            StackPanel ellipsePanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(5)
            };

            ellipsePanel.Children.Add(CreateEllipse(ellipse1.Fill, ellipse1.Stroke));
            ellipsePanel.Children.Add(CreateEllipse(ellipse2.Fill, ellipse2.Stroke));
            ellipsePanel.Children.Add(CreateEllipse(ellipse3.Fill, ellipse3.Stroke));
            ellipsePanel.Children.Add(CreateEllipse(ellipse4.Fill, ellipse4.Stroke));

            ListBoxHistoriek.Items.Add(ellipsePanel);
        }

        public Ellipse CreateEllipse(Brush fillColor, Brush strokeColor)
        {
            return new Ellipse
            {
                Width = 40,
                Height = 40,
                Fill = fillColor,
                Stroke = strokeColor,
                StrokeThickness = 4
            };
        }
    }
}
