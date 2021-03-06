﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class AbstractActionSender
    {
        protected ActionImplementor implementor;
        public ActionImplementor Implementor
        {
            set { implementor = value; }
        }
        public virtual void SendNoteInfo(string topic, string text)
        {
            implementor.SendNoteInfo(topic, text);
        }
    }
}
