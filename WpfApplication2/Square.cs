using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;

namespace WpfApplication2
{
    public class Square : Shape
    {

        #region Fiels and Properties
        private readonly Random _rnd = new Random();

        public Rectangle Rect { get; set; }

        public Color FillColor { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Square"/> class.
        /// </summary>
        /// <param name="initX">The initialize x.</param>
        /// <param name="initY">The initialize y.</param>
        /// <param name="filled">if set to <c>true</c> [filled].</param>
        public Square(int initX, int initY, bool filled)
        {
            this.X = initX;
            this.Y = initY;
            if (filled)
            {
                this.FillColor = Color.FromRgb((byte)this._rnd.Next(256)
                    , (byte)this._rnd.Next(256)
                    , (byte)this._rnd.Next(256));
            }
            CreateRectangle(filled);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Square"/> class.
        /// </summary>
        public Square()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the visual rectangle.
        /// </summary>
        /// <param name="filled">if set to <c>true</c> [filled].</param>
        private void CreateRectangle(bool filled)
        {
            this.Rect = new Rectangle();
            this.Rect.Stroke = this.Brush;
            this.Rect.Width = this.Size;
            this.Rect.Height = this.Size;
            this.Rect.Fill = new SolidColorBrush(this.FillColor);
            this.Rect.Margin = new Thickness(this.X, this.Y, 0, 0);
            this.Rect.MouseEnter += Rect_MouseEnter;
            this.Rect.MouseLeave += Rect_MouseLeave;
        }

        /// <summary>
        /// Handles the MouseLeave event of the Rect control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void Rect_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var rectangle = sender as Rectangle;
            if (rectangle != null) rectangle.ToolTip = string.Empty;
        }

        /// <summary>
        /// Handles the MouseEnter event of the Rect control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void Rect_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var rectangle = (Rectangle)sender;
            rectangle.ToolTip = rectangle.Fill.ToString();
        }

        /// <summary>
        /// Displays the square on the canvas.
        /// </summary>
        /// <param name="drawArea">The draw area.</param>
        public override void DisplayOn(Canvas drawArea)
        {
            drawArea.Children.Add(this.Rect);
        }

        /// <summary>
        /// Saves the square to the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        public override void Save(string file)
        {
            // Save the Button to a string.
            string savedShape = XamlWriter.Save(this.Rect);
            TextWriter tw = new StreamWriter(file, append: true);
            tw.WriteLine(savedShape);
            tw.Close();
            tw = null;
        }

        /// <summary>
        /// Removes the square from the canvas.
        /// </summary>
        /// <param name="drawCanvas">The draw canvas.</param>
        public override void RemoveFrom(Canvas drawCanvas)
        {
            drawCanvas.Children.Remove(this.Rect);
        } 
        #endregion

    }
}