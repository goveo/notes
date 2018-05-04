using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class DeleteDefaultHandler : DeleteHandler
    {
        public override void Delete(Note note, NotesModel Current)
        {
            if (!note.IsImportant)
            {
                Current.Remove(note);
            }
            else if (successor != null)
            {
                successor.Delete(note, Current);
            }
        }
    }
}
