using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace WpfApplication2
{
    public abstract class Shape : IDisplayable
    {

        #region Fields and Properties
        protected int X { get; set; }

        protected int Y { get; set; }

        protected int Size { get; set; } = 75;

        protected SolidColorBrush Brush { get; set; }
            = new SolidColorBrush(Colors.Black); 
        #endregion

        #region Abstract methods
        public abstract void DisplayOn(Canvas drawCanvas);
        public abstract void RemoveFrom(Canvas drawCanvas);
        public abstract void Save(string file);
        #endregion

    }
}
