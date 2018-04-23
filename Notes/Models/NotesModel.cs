using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Timers;

namespace Notes.Models
{
    public class NotesModel : ObservableCollection<Note>
    {
        private static object _threadLock = new Object();
        private static NotesModel current = null;
        private NoteCreator deadlinedNoteCreator = new DeadlinedNoteCreator();
        private NoteCreator defaultNoteCreator = new DefaultNoteCreator();

        public delegate void SavedDelegate(Note note);

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

        public void CreateNote(string topic, string text, bool isImportant, DateTime deadline)
        {
            Console.WriteLine("created note with deadline param");
            if (deadline == null || deadline < DateTime.Now)
            {
                DefaultNote note = (DefaultNote)defaultNoteCreator.Create(topic, text, isImportant, deadline);
                Current.Insert(0, note);
                note.Accept(new DetailInfoVisitor());
            } 
            else
            {
                DeadlinedNote note = (DeadlinedNote)deadlinedNoteCreator.Create(topic, text, isImportant, deadline);
                Current.Insert(0, note);
                note.Accept(new DetailInfoVisitor());
            }
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
                    new DefaultNote()
                };
            }

            return NoteBuf;
        }

        // Chain of responsibility
        public void DeleteAllNotes()
        {

            DeleteHandler h1 = new DeleteImportantHandler();
            DeleteHandler h2 = new DeleteDefaultHandler();
            h1.SetSuccessor(h2);

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
        // Chain of responsibility end

        // Factory Method
        abstract class NoteCreator
        {
            public abstract Note Create(string topic, string text, bool isImportant, DateTime deadline);
        }

        class DeadlinedNoteCreator : NoteCreator
        {
            public override Note Create(string topic, string text, bool isImportant, DateTime deadline)
            {
                Console.WriteLine("DeadlinedNoteCreator");
                return new DeadlinedNote(topic, text, isImportant, deadline);
            }
        }

        class DefaultNoteCreator : NoteCreator
        {
            public override Note Create(string topic, string text, bool isImportant, DateTime deadline)
            {
                Console.WriteLine("DefaultNoteCreator");
                return new DefaultNote(topic, text, isImportant);
            }
        }

        // Factory Method end

    }

}
