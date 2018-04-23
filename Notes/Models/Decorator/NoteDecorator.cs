using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    abstract public class NoteDecorator : Note
    {
        protected Note Note;
        public void SetDecoratorOn(Note baseNote)
        {
            this.Note = baseNote;
        }
        public override void SetTopic(string topic)
        {
            Console.WriteLine("NoteDecorator.SetTopic()");
            if (Note != null)
            {
                Note.SetTopic(topic);
            }
        }
    }
}
