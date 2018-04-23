using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    [Serializable]
    abstract public class Note
    {
        protected string text;
        protected string topic;
        protected DateTime time;
        protected string timeToShow;
        protected string detailInfo;

        public INoteState State { get; set; }
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
                    //this.topic = value;
                    SetTopic(value);
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

        public string DetailInfo
        {
            get
            {
                return detailInfo;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.detailInfo = value;
                }
            }
        }

        public Note()
        {
            this.Topic = "Sample topic";
            this.Text = "Sample text";
            this.Time = DateTime.Now;
            this.State = new CreatedNoteState();
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
            this.State = new CreatedNoteState();
            this.IsImportant = isImportant;
        }

        //state
        public void setEdited()
        {
            State = new EditedNoteState();
        }

        public virtual string GetFormattedNoteTime()
        {
            return State.GetFormattedNoteTime(this);
        }

        public virtual void SetTopic(string topic)
        {
            Console.WriteLine("SetTopic : " + topic);
            this.topic = topic;
        }

    }
}
