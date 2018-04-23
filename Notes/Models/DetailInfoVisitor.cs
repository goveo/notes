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
            return "created: " + note.Time.ToString();
        }

        public string VisitDeadlinedNote(DeadlinedNote note)
        {
            return "created: " + note.Time.ToShortDateString() + " deadline: " + note.Deadline.ToShortDateString();
        }
    }
}
