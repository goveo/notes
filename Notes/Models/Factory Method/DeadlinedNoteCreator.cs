using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class DeadlinedNoteCreator : NoteCreator
    {
        public override Note Create(string topic, string text, bool isImportant, DateTime deadline)
        {
            Console.WriteLine("DeadlinedNoteCreator");
            return new DeadlinedNote(topic, text, isImportant, deadline);
        }
    }
}
