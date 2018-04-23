using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    public interface IVisitor
    {
        string VisitDeadlinedNote(DeadlinedNote note);
        string VisitDefaultNote(DefaultNote note);
    }
}
