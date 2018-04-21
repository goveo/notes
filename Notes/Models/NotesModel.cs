using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class NotesModel : ObservableCollection<Note>
    {
        private static object _threadLock = new Object();
        private static NotesModel current = null;

        public static NotesModel Current
        {
            get
            {
                Console.WriteLine("current");
                lock (_threadLock)
                    if (null == current)
                        current = new NotesModel();
                return current;
            }
        }

        private NotesModel()
        {
            Note[] NoteArr = DeserializeNotes();
            foreach (Note NoteObj in NoteArr)
            {
                Add(NoteObj);
            }
            Console.WriteLine(this.Items);
        }

        public int GetNoteIndex(Note note)
        {
            return IndexOf(note);
        }

        public void RemoveNote(Note note)
        {
            Remove(note);
        }

        public void CreateNote(Note note)
        {
            Insert(0, note);
        }

        public void CreateNote(string topic, string text, bool isImportant)
        {
            Note note = new Note(topic, text, isImportant);
            Insert(0, note);
            Console.WriteLine("number of notes: " + this.Count);
        }

        public void SaveNotes()
        {
            SerializeNotes(Current.ToArray());
        }

        // Serialization.
        public static void SerializeNotes(Note[] NoteArr)
        {
            FileStream fs = new FileStream("savednotes.dat", FileMode.Create);
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
                FileStream fs = new FileStream("savednotes.dat", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                NoteBuf = (Note[])bf.Deserialize(fs);

                fs.Close();
            }
            catch (Exception e)
            {
                NoteBuf = new Note[] 
                {
                    new Note()
                };
            }

            return NoteBuf;
        }
    }
}
