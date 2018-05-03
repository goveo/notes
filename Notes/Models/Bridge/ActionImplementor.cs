using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    abstract public class ActionImplementor
    {
        public abstract void SendNoteInfo(string topic, string text);
    }
}
