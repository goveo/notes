using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class UppercaseDecorator : NoteDecorator
    {
        public override void SetTopic(string topic)
        {
            Console.WriteLine("UppercaseDecorator.SetTopic()");
            Console.WriteLine("topic.ToUpper() : " + topic.ToUpper());
            base.SetTopic(topic.ToUpper());
        }
    }
}
