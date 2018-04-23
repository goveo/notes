using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Notes.Models
{
    interface IButtonEnabler
    {
        void EnableButton(Button button);
    }
}
