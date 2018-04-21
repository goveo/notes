using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    [Serializable]
    public class Note
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
            this.Text = "Hey, i'm your first note. Double click on me to change me.";
            this.Topic = "Sample topic.";
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

        public void setEdited()
        {
            if (State == NoteState.CREATED)
            {
                State = NoteState.EDITED;
            }
        }

        private string GetFormattedNoteTime()
        {
            bool isYesterday = DateTime.Today - time.Date == TimeSpan.FromDays(1);
            bool isToday = DateTime.Today - time.Date == TimeSpan.FromDays(0);

            string result = "";
            if (isToday == true)
            {
                result = this.time.ToShortTimeString();
            }
            else if (isYesterday == true)
            {
                result = "yesterday " + this.time.ToShortTimeString();
            }
            else
            {
                result = this.time.ToLongDateString();
            }
            if (this.State == NoteState.EDITED)
            {
                result = "edited " + result;
            }

            return result;
        }

    }
}
