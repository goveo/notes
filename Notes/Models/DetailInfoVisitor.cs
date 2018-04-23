using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class DetailInfoVisitor : IVisitor
    {
        public string VisitDefaultNote(DefaultNote note)
        {
            string result = "created: " + note.Time.ToShortDateString();
            return result;
        }

        public string VisitDeadlinedNote(DeadlinedNote note)
        {
            string result = "created: " + note.Time.ToShortDateString() + "\n deadline: " + note.Deadline.ToShortDateString();
            return result;
        }
    }
}
