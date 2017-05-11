using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;

namespace WpfApplication2
{
    public class Line : Shape
    {

        #region Fields and Properties
        private readonly Random _rnd = new Random();

        public int X1 { get; set; }

        public int Y1 { get; set; }

        public int X2 { get; set; }

        public int Y2 { get; set; }

        public System.Windows.Shapes.Line Line1 { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        public Line()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="initX1">The initialize x1.</param>
        /// <param name="initY1">The initialize y1.</param>
        /// <param name="initX2">The initialize x2.</param>
        /// <param name="initY2">The initialize y2.</param>
        /// <param name="randomColor">if set to <c>true</c> [random color].</param>
        public Line(int initX1, int initY1, int initX2, int initY2, bool randomColor)
        {
            this.X1 = initX1;
            this.X2 = initX2;
            this.Y1 = initY1;
            this.Y2 = initY2;

            CreateLine(randomColor);
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Displays the on.
        /// </summary>
        /// <param name="drawCanvas">The draw canvas.</param>
        public override void DisplayOn(Canvas drawCanvas)
        {
            drawCanvas.Children.Add(this.Line1);
        }

        /// <summary>
        /// Removes from.
        /// </summary>
        /// <param name="drawCanvas">The draw canvas.</param>
        public override void RemoveFrom(Canvas drawCanvas)
        {
            drawCanvas.Children.Remove(this.Line1);
        }

        /// <summary>
        /// Creates the line.
        /// </summary>
        /// <param name="randomColor">if set to <c>true</c> [random color].</param>
        private void CreateLine(bool randomColor)
        {
            this.Line1 = new System.Windows.Shapes.Line
            {
                //Margin = new Thickness(this.x1, this.y1, 0, 0),
                X1 = this.X1,
                X2 = this.X2,
                Y1 = this.Y1,
                Y2 = this.Y2,
                Stroke = randomColor ? new SolidColorBrush(Color.FromRgb((byte)this._rnd.Next(256)
                    , (byte)this._rnd.Next(256), (byte)this._rnd.Next(256))) : new SolidColorBrush(Colors.Black),
                StrokeThickness = 2
            };


        }

        /// <summary>
        /// Saves the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        public override void Save(string file)
        {
            // Save the Button to a string.
            string savedShape = XamlWriter.Save(this.Line1);
            TextWriter tw = new StreamWriter(file, append: true);
            tw.WriteLine(savedShape);
            tw.Close();
            tw = null;
        } 
        #endregion

    }
}
