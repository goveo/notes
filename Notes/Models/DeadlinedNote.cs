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

        //public override string GetFormattedNoteTime()
        //{
        //    bool isToday = deadline.Date - DateTime.Today == TimeSpan.FromDays(0);

        //    string result = "deadline ";
        //    if (isToday == true)
        //    {
        //        result =  result + this.deadline.ToShortTimeString();
        //    }
        //    else
        //    {
        //        result = result + this.deadline.ToShortDateString();
        //    }
        //    return result;
        //}

        public void SetDetailInfo(IVisitor visitor)
        {
            this.DetailInfo = visitor.VisitDeadlinedNote(this);
        }
    }
}
