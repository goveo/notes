﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    [Serializable]
    abstract public class Note
    {
        public string text;
        public string topic;
        public DateTime time;
        public string timeToShow;

        public NoteState State { get; set; }
        public bool IsImportant { get; set; }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.text = value;
                }
            }
        }

        public string Topic
        {
            get
            {
                return this.topic;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.topic = value;
                }
            }
        }

        public DateTime Time
        {
            get
            {
                return this.time;
            }
            set
            {
                if (typeof(DateTime) == value.GetType())
                {
                    this.time = value;
                }
            }
        }

        public string TimeToShow
        {
            get
            {
                return this.GetFormattedNoteTime();
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.timeToShow = value;
                }
            }
        }

        public Note()
        {
            this.Topic = "Sample topic";
            this.Text = "Sample text";
            this.Time = DateTime.Now;
            this.State = NoteState.CREATED;
            this.IsImportant = false;
        }

        public Note(string topic, string text, bool isImportant)
        {
            Console.WriteLine("Note CONSTRUCTOR ========================================= isImportant : " + isImportant);
            Console.WriteLine("Note CONSTRUCTOR ========================================= topic : " + topic);
            Console.WriteLine("Note CONSTRUCTOR ========================================= text : " + text);
            this.Topic = topic;
            this.Text = text;
            this.Time = DateTime.Now;
            this.State = NoteState.CREATED;
            this.IsImportant = isImportant;
        }

        //state
        public void setEdited()
        {
            if (State == NoteState.CREATED)
            {
                State = NoteState.EDITED;
            }
        }

        public abstract string GetFormattedNoteTime();

    }

    //abstract public class Decorator : Note
    //{
    //    protected Note Note;
    //    public void SetNote(Note baseNote)
    //    {
    //        this.Note = baseNote;
    //    }
    //}
    //public class DeadlineDecorator : Decorator
    //{
    //    public void SetDeadline()
    //    {
    //        Console.Write("Setted deadline");
    //    }
    //}
}