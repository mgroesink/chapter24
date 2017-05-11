using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApplication2
{
    interface IDisplayable
    {
        void DisplayOn(Canvas drawCanvas);
        void Save(string file);
        void RemoveFrom(Canvas drawCanvas);
    }
}
