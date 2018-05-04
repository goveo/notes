using Notes.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Notes.ViewModels
{
    public class DelegateCommand : ICommand
    {
        public delegate void ICommandOnExecute(object parameter);
        public delegate bool ICommandOnCanExecute(object parameter);

        private ICommandOnExecute _execute;
        private ICommandOnCanExecute _canExecute;

        public DelegateCommand(ICommandOnExecute onExecuteMethod, ICommandOnCanExecute onCanExecuteMethod)
        {
            _execute = onExecuteMethod;
            _canExecute = onCanExecuteMethod;
        }
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }

    class MainViewModel : BaseViewModel
    {
        private DelegateCommand exitCommand;
        private DelegateCommand deleteAllNotesCommand;

        public NotesModel NotesArr { get; set; }
        public string TextToCreate { get; set; }
        public string TopicToCreate { get; set; }
        public bool IsImportantToCreate { get; set; }
        public DateTime DeadlineToCreate { get; set; }
        public string TextToEdit { get; set; }
        public string TopicToEdit { get; set; }
        public MainViewModel()
        {
            this.NotesArr = NotesModel.Current;
        }
        public bool CanExecuteCommandTrue(object parameter)
        {
            return true;
        }
        public ICommand ExitCommand
        {
            get
            {
                if (null == exitCommand)
                {
                    exitCommand = new DelegateCommand(Exit, CanExecuteCommandTrue);
                }

                return exitCommand;
            }
        }
        public ICommand DeleteAllNotesCommand
        {
            get
            {
                Console.WriteLine("get ICommand DeleteAllNotesCommand");
                if (null == deleteAllNotesCommand)
                {
                    deleteAllNotesCommand = new DelegateCommand(DeleteAllNotes, CanExecuteCommandTrue);
                }


                return deleteAllNotesCommand;
            }
        }
        private void Exit(object parameter)
        {
            NotesArr.SaveNotes();
            Application.Current.Shutdown();
        }
        private void DeleteAllNotes(object parameter)
        {
            Console.WriteLine("DeleteAllNotes");
            MessageBoxResult result = MessageBox.Show("Delete all notes?", "Are your sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NotesArr.DeleteAllNotes();
            }

        }
        public void CreateNote(object parameter)
        {
            Console.WriteLine("CheckAndInvokeCommand");
            if (TextToCreate == null || TextToCreate == "")
            {
                return;
            }
            if (TopicToCreate == null || TopicToCreate == "")
            {
                return;
            }
            
            NotesArr.CreateNote(TopicToCreate, TextToCreate, IsImportantToCreate, DeadlineToCreate);
           
            NotesArr.SaveNotes();
        }

        public void DeleteNote(object NoteObj)
        {
            NotesArr.Remove((Note)NoteObj);
            NotesArr.SaveNotes();
        }

        public void EditNote(object NoteObj)
        {
            Note oldNote = (Note)NoteObj;
            bool isImportant = oldNote.IsImportant;
            DateTime deadline = new DateTime();
            try
            {
                DeadlinedNote note = (DeadlinedNote)NoteObj;
                deadline = note.Deadline;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            NotesArr.RemoveNote(oldNote);
            NotesArr.CreateNote(TopicToEdit, TextToEdit, isImportant, deadline);
            NotesArr[0].setEdited();
        }
    }
}
