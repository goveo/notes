using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class DefaultNoteCreator : NoteCreator
    {
        public override Note Create(string topic, string text, bool isImportant, DateTime deadline)
        {
            Console.WriteLine("DefaultNoteCreator");
            return new DefaultNote(topic, text, isImportant);
        }
    }
}
