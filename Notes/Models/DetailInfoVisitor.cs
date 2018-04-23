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
            //string result = "<table><tr><td>Свойство<td><td>Значение</td></tr>";
            //result += "<tr><td>Name<td><td>" + acc.Name + "</td></tr>";
            //result += "<tr><td>Number<td><td>" + acc.Number + "</td></tr></table>";
            //Console.WriteLine(result);
            return "";
        }

        public string VisitDeadlinedNote(DeadlinedNote note)
        {
            //string result = "<table><tr><td>Свойство<td><td>Значение</td></tr>";
            //result += "<tr><td>Name<td><td>" + acc.Name + "</td></tr>";
            //result += "<tr><td>RegNumber<td><td>" + acc.RegNumber + "</td></tr>";
            //result += "<tr><td>Number<td><td>" + acc.Number + "</td></tr></table>";
            //Console.WriteLine(result);
            return "";
        }
    }
}
