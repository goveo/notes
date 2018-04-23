using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    [Serializable]
    public class CreatedNoteState : INoteState
    {
        public string GetFormattedNoteTime(Note note)
        {
            if (note.GetType() == typeof(DeadlinedNote))
            {
                DeadlinedNote newNote = (DeadlinedNote)note;
                return "deadline " + newNote.Deadline.ToShortDateString();
            }
            else
            {
                bool isYesterday = DateTime.Today - note.Time.Date == TimeSpan.FromDays(1);
                bool isToday = DateTime.Today - note.Time.Date == TimeSpan.FromDays(0);

                string result = "";
                if (isToday == true)
                {
                    result += note.Time.ToShortTimeString();
                }
                else if (isYesterday == true)
                {
                    result += "yesterday " + note.Time.ToShortTimeString();
                }
                else
                {
                    result += note.Time.ToShortDateString();
                }
                return result;
            }
        }
    }
}
