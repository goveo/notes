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

        public void DeleteAllNotes()
        {

            DeleteHandler h1 = new DeleteImportantHandler();
            DeleteHandler h2 = new DeleteDefaultHandler();
            h1.SetSuccessor(h2);
            //h2.SetSuccessor(h1);

            Console.WriteLine(this.Count);
            foreach (Note note in new System.Collections.ArrayList(this))
            {
                h1.Delete(note);
            }
            Console.WriteLine(this.Count);
        }

        abstract class DeleteHandler
        {
            protected DeleteHandler successor;

            public void SetSuccessor(DeleteHandler successor)
            {
                this.successor = successor;
            }

            public abstract void Delete(Note note);
        }


        class DeleteImportantHandler : DeleteHandler
        {
            public override void Delete(Note note)
            {
                if (note.IsImportant)
                {
                    Console.WriteLine("NOTE WITH TOPIC '{0}' IS IMPORTANT, I CAN'T DELETE IT", note.Topic);
                }
                else if (successor != null)
                {
                    successor.Delete(note);
                }
            }
        }

        class DeleteDefaultHandler : DeleteHandler
        {
            public override void Delete(Note note)
            {
                if (!note.IsImportant)
                {
                    Current.Remove(note);
                }
                else if (successor != null)
                {
                    successor.Delete(note);
                }
            }
        }
    }
    
}
