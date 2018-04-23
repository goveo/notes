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
            Console.WriteLine("=====================VisitDefaultNote");
            return "DefaultNote info";
        }

        public string VisitDeadlinedNote(DeadlinedNote note)
        {
            Console.WriteLine("=====================VisitDeadlinedNote");
            return "VisitDeadlinedNote info";
        }
    }
}
