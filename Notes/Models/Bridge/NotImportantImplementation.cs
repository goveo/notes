using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class NotImportantImplementation : Implementor
    {
        private Action<string> topic;
        private Action<string> text;

        public NotImportantImplementation(Action<string> topic, Action<string> text)
        {
            this.topic = topic;
            this.text = text;
        }

        public override void SendNoteInfo(string topicText, string textText)
        {
            Console.WriteLine("SendNoteInfo Not Important");
            if (topic != null && text != null)
            {
                topic(topicText);
                text(textText);
            }
        }
    }
}
