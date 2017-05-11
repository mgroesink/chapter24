using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ComboBoxItem _selected ;
        private readonly List<Shape> _shapes = new List<Shape>();

        public MainWindow()
        {
            InitializeComponent();
            GetShapes();
            this._selected = (ComboBoxItem)this.ShapesComboBox.Items[0];
        }


        /// <summary>
        /// Gets the shapes from textfile.
        /// </summary>
        private void GetShapes()
        {
            if (File.Exists("shapes.txt"))
            {
                TextReader tr = new StreamReader("shapes.txt");
                string[] lines = System.IO.File.ReadAllLines("shapes.txt");

                // Display the file contents by using a foreach loop.
                foreach (string line in lines)
                {
                    StringReader stringReader = new StringReader(line);
                    XmlReader xmlReader = XmlReader.Create(stringReader);
                    Object s = (Object)XamlReader.Load(xmlReader);

                    if (s is Rectangle)
                    {
                        Square shape = new Square();
                        shape.Rect = (Rectangle)s;
                        this._shapes.Add(shape);
                        shape.DisplayOn(this.DrawCanvas);
                    }
                    else if (s is Circle)
                    {
                        Circle shape = new Circle();
                        shape.Ellipse = (Ellipse)s;
                        this._shapes.Add(shape);
                        shape.DisplayOn(this.DrawCanvas);
                    }
                    else if (s is Line)
                    {
                        Line shape = new Line();
                        shape.Line1 = (System.Windows.Shapes.Line)s;
                        this._shapes.Add(shape);
                        shape.DisplayOn(this.DrawCanvas);
                    }

                }
                tr.Close();
                tr = null;

            }
        }

        private void drawCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Shape s;
            switch (this._selected.Content.ToString().ToLower())
            {

                case "circle":
                    s = new Circle((int)e.GetPosition(this.DrawCanvas).X,
                        (int)e.GetPosition(this.DrawCanvas).Y, e.RightButton == MouseButtonState.Pressed);
                    break;
                case "line":
                    s = new Line((int)e.GetPosition(this.DrawCanvas).X,
                        (int)e.GetPosition(this.DrawCanvas).Y,
                        (int)e.GetPosition(this.DrawCanvas).X + 100,
                        (int)e.GetPosition(this.DrawCanvas).Y + 100, e.RightButton == MouseButtonState.Pressed);
                    break;
                default:
                    s = new Square((int)e.GetPosition(this.DrawCanvas).X,
                        (int)e.GetPosition(this.DrawCanvas).Y
                        , e.RightButton == MouseButtonState.Pressed);
                    break;


            }

            s.DisplayOn(this.DrawCanvas);
            this._shapes.Add(s);
        }



        /// <summary>
        /// Save all shapes to textfile.
        /// </summary>
        private void SaveShapes()
        {
            if (this.DrawCanvas.Children.Count > 0 && MessageBox.Show(
                    messageBoxText: "Do you want to save this " + this._shapes.Count.ToString()
                                    + " shapes?",
                    caption: "Save shapes?",
                    button: MessageBoxButton.YesNo, icon: MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                File.Delete("shapes.text");
                foreach (Shape s in this._shapes)
                {
                    s.Save("shapes.txt");
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonClear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            ClearCanvas();
        }

        /// <summary>
        /// Remove all shapes from the canvas.
        /// </summary>
        private void ClearCanvas()
        {
            this.DrawCanvas.Children.Clear();
            this._shapes.Clear();
            if (File.Exists("shapes.txt"))
            {
                ;
                if (MessageBox.Show(messageBoxText: "Do you also want to remove the saved shapes?"
                        , caption: "Delete saved shapes?"
                        , button: MessageBoxButton.YesNo
                        , icon: MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    File.Delete("shapes.txt");
                }
            }
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            this._selected = (ComboBoxItem)cb.SelectedItem;
            SetTooltipContent(cb);
        }

        /// <summary>
        /// Sets the content of the tooltip for the canvas.
        /// </summary>
        /// <param name="cb">The cb.</param>
        private void SetTooltipContent(ComboBox cb)
        {
            switch (cb.Text.ToLower())
            {
                case "circle":
                    this.CanvasToolTip.Content =
                        "Left click for open circle and right click for filled circle";
                    break;
                case "square":
                    this.CanvasToolTip.Content =
                        "Left click for open square and left click for filled square";
                    break;
                case "line":
                    this.CanvasToolTip.Content =
                        "Left click for black and left click for random color";
                    break;

            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveShapes();
        }
    }
}
