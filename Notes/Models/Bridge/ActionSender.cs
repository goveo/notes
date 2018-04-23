using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class ActionSender : AbstractActionSender
    {
        public override void SendNoteInfo(string topic, string text)
        {
            implementor.SendNoteInfo(topic, text);
        }
    }
}
