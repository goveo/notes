using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Notes.Models
{
    public class ButtonEnabler : IButtonEnabler
    {
        public void EnableButton(Button button, string topic, string text)
        {
            button.IsEnabled = true;
        }
    }
}
