using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    [Serializable]
    public class DefaultNote : Note
    {
        public DefaultNote()
        {
        }

        public DefaultNote(string topic, string text, bool isImportant) : base(topic, text, isImportant)
        {
        }

        public override string GetFormattedNoteTime()
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
                result = this.time.ToShortDateString();
            }
            if (this.State == NoteState.EDITED)
            {
                result = "edited " + result;
            }

            return result;
        }
    }
}
