using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    abstract public class DeleteHandler
    {
        protected DeleteHandler successor;

        public void SetSuccessor(DeleteHandler successor)
        {
            this.successor = successor;
        }

        public abstract void Delete(Note note, NotesModel Current);
    }
}
