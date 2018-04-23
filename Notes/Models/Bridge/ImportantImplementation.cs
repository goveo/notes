using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class ImportantImplementation : Implementor
    {
        private Action<string> topic;
        private Action<string> text;
        private Action<bool> isImportant;

        public ImportantImplementation(Action<string> topic, Action<string> text, Action<bool> isImportant)
        {
            this.topic = topic;
            this.text = text;
            this.isImportant = isImportant;
        }

        public override void SendNoteInfo(string topicText, string textText)
        {
            Console.WriteLine("SendNoteInfo Important");
            if (topic != null && text != null && isImportant != null)
            {
                topic(topicText);
                text(textText);
                isImportant(true);
            }
        }
    }
}
