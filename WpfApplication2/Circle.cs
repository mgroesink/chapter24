using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;

namespace WpfApplication2
{
    public class Circle : Shape
    {

        #region Fields and Properties
        private Ellipse _ellipse;
        private Color _fillColor;
        private Random _rnd = new Random();

        /// <summary>
        /// Gets or sets the ellipse.
        /// </summary>
        /// <value>
        /// The ellipse.
        /// </value>
        public Ellipse Ellipse
        {
            get
            {
                return this._ellipse;
            }

            set
            {
                this._ellipse = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the fill.
        /// </summary>
        /// <value>
        /// The color of the fill.
        /// </value>
        public Color FillColor
        {
            get
            {
                return this._fillColor;
            }

            set
            {
                this._fillColor = value;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="initX">The initialize x.</param>
        /// <param name="initY">The initialize y.</param>
        /// <param name="filled">if set to <c>true</c> [filled].</param>
        public Circle(int initX, int initY, bool filled)
        {
            this.X = initX;
            this.Y = initY;
            if (filled)
            {
                this.FillColor = Color.FromRgb((byte)this._rnd.Next(256)
                    , (byte)this._rnd.Next(256)
                    , (byte)this._rnd.Next(256));
            }
            CreateEllipse(filled);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        public Circle()
        {
        }

        #endregion

        #region Methods
        /// <summary>
        /// Creates the visual ellipse.
        /// </summary>
        /// <param name="filled">if set to <c>true</c> [filled].</param>
        private void CreateEllipse(bool filled)
        {
            this.Ellipse = new Ellipse();
            this.Ellipse.Stroke = this.Brush;
            this.Ellipse.Height = this.Size;
            this.Ellipse.Width = this.Size;
            this.Ellipse.Margin = new Thickness(X, Y, 0, 0);
            if (filled)
            {
                this.Ellipse.Fill = new SolidColorBrush(this.FillColor);
            }
            this.Ellipse.MouseEnter += Ellipse_MouseEnter;
            this.Ellipse.MouseLeave += Ellipse_MouseLeave;
        }

        /// <summary>
        /// Handles the MouseLeave event of the Ellipse control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void Ellipse_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var circle = sender as Ellipse;
            if (circle != null) circle.ToolTip = string.Empty;
        }

        /// <summary>
        /// Handles the MouseEnter event of the Ellipse control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void Ellipse_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var circle = sender as Ellipse;
            if (circle.Fill != null) circle.ToolTip = circle.Fill;
        }

        /// <summary>
        /// Displays the on.
        /// </summary>
        /// <param name="drawArea">The draw area.</param>
        public override void DisplayOn(Canvas drawArea)
        {
            drawArea.Children.Add(this.Ellipse);
        }

        /// <summary>
        /// Saves the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        public override void Save(string file)
        {
            // Save the Button to a string.
            string savedShape = XamlWriter.Save(this._ellipse);
            TextWriter tw = new StreamWriter(file, append: true);
            tw.WriteLine(savedShape);
            tw.Close();
            tw = null;
        }

        /// <summary>
        /// Removes the circle from the canvas.
        /// </summary>
        /// <param name="drawCanvas">The draw canvas.</param>
        public override void RemoveFrom(Canvas drawCanvas)
        {
            drawCanvas.Children.Remove(this._ellipse);
        }

        #endregion

    }
}