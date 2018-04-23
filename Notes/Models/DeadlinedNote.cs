using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    [Serializable]
    public class DeadlinedNote : Note
    {
        private DateTime deadline;
        public DateTime Deadline
        {
            get
            {
                return this.deadline;
            }
            set
            {
                if (typeof(DateTime) == value.GetType())
                {
                    this.deadline = value;
                }
            }
        }

        public DeadlinedNote()
        {
        }

        public DeadlinedNote(string topic, string text, bool isImportant, DateTime deadline) : base(topic, text, isImportant)
        {
            this.deadline = deadline;
        }

        public override string GetFormattedNoteTime()
        {
            bool isToday = deadline.Date - DateTime.Today == TimeSpan.FromDays(0);

            string result = "deadline ";
            if (isToday == true)
            {
                result =  result + this.deadline.ToShortTimeString();
            }
            else
            {
                result = result + this.deadline.ToShortDateString();
            }
            //else if (isYesterday == true)
            //{
            //    result = "yesterday " + this.time.ToShortTimeString();
            //}
            //else
            //{
            //    result = this.time.ToLongDateString();
            //}
            //if (this.State == NoteState.EDITED)
            //{
            //    result = "edited " + result;
            //}

            return result;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitDeadlinedNote(this);
        }
    }
}
