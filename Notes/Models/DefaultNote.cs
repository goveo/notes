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
        public void SetDetailInfo(IVisitor visitor)
        {
            this.DetailInfo = visitor.VisitDefaultNote(this);
        }
    }
}
