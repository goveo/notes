using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    [Serializable]
    public class Note
    {
        public string text;
        public string topic;
        public DateTime time;
        public string timeToShow;

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
            this.TimeToShow = Time.ToString();
        }

        public Note(string topic, string text, DateTime time)
            : this()
        {
            this.Topic = topic;
            this.Text = text;
            this.Time = time;
            this.TimeToShow = time.ToString();
        }

        private string GetFormattedNoteTime()
        {
            return this.time.ToShortTimeString();
        }
    }


    public class NoteModel : ObservableCollection<Note>
    {
        private static object _threadLock = new Object();
        private static NoteModel current = null;

        public static NoteModel Current
        {
            get
            {
                Console.WriteLine("current");
                lock (_threadLock)
                    if (null == current)
                        current = new NoteModel();
                return current;
            }
        }

        private NoteModel()
        {
            Note[] NoteArr = DeserializeNotes();
            foreach (Note NoteObj in NoteArr)
            {
                Add(NoteObj);
            }
            Console.WriteLine(this.Items);
        }

        public int GetNoteIndex(Note Note)
        {
            return IndexOf(Note);
        }

        public void RemoveNote(Note Note)
        {
            Remove(Note);
        }

        public void CreateNote(Note Note, int index)
        {
            Insert(index, Note);
        }

        public void CreateNote(string topic, string text, DateTime time)
        {
            Note NoteObj = new Note(topic, text, time);
            Add(NoteObj);
            Console.WriteLine("number of Notes: " + this.Count);
        }

        public void SaveNote()
        {
            SerializeNotes(Current.ToArray());
        }

        // Serialization.
        public static void SerializeNotes(Note[] NoteArr)
        {
            FileStream fs = new FileStream("Notes.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, NoteArr);
            fs.Close();
        }

        // Deserialization.
        public static Note[] DeserializeNotes()
        {
            Note[] NoteBuf;
            try
            {
                FileStream fs = new FileStream("Notes.dat", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                NoteBuf = (Note[])bf.Deserialize(fs);

                fs.Close();
            }
            catch (Exception e)
            {
                NoteBuf = new Note[] { new Note(), };
            }

            return NoteBuf;
        }
    }
}
