﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    abstract public class NoteCreator
    {
        public abstract Note Create(string topic, string text, bool isImportant, DateTime deadline);
    }
}
