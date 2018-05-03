using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Notes
{
    public class ButtonChecker : IButtonEnabler
    {
        ButtonEnabler btnEnabler = new ButtonEnabler();

        public void EnableButton(Button button, string topic, string text)
        {
            if (!String.IsNullOrEmpty(topic) && !String.IsNullOrEmpty(text))
            {
                btnEnabler.EnableButton(button, topic, text);
                Console.WriteLine("Button enabled");
            }
            else
            {
                button.IsEnabled = false;
            }
        }
    }
}