using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class DeleteImportantHandler : DeleteHandler
    {
        public override void Delete(Note note, NotesModel Current)
        {
            if (note.IsImportant)
            {
                Console.WriteLine("NOTE WITH TOPIC '{0}' IS IMPORTANT, I CAN'T DELETE IT", note.Topic);
            }
            else if (successor != null)
            {
                successor.Delete(note, Current);
            }
        }
    }
}
