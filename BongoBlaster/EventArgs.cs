using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BongoBlaster
{
    public class BongoButtonEventArgs : EventArgs
    {
        public BongoButton BongoButton;
    }

    public class ClapSensorValueEventArgs : EventArgs
    {
        public int ClapSensorValue;
    }
}
