using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Notes
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) {
            if (PropertyChanged != null)
                PropertyChanged(this, args);
        }
    }

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

    class MainViewModel : ViewModelBase
    {
        private DelegateCommand exitCommand;

        public NoteModel NotesArr { get; set; }

        public string TextToCreate { get; set; }
        public string TopicToCreate { get; set; }
        public string TextToEdit { get; set; }
        public string TopicToEdit { get; set; }

        public MainViewModel()
        {
            this.NotesArr = NoteModel.Current;
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

        private void Exit(object parameter)
        {
            // Save data before an exit.
            MessageBoxResult result = MessageBox.Show("Do you want to save changes?", "Save confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NotesArr.SaveNote();
            }

            Application.Current.Shutdown();
        }

        public bool CanExecuteCommandTrue(object parameter)
        {
            return true;
        }

        private ICommand createNote;
        public ICommand CreateNote
        {
            get
            {
                Console.WriteLine("CreateNote ICommand");
                if (null == createNote)
                {
                    createNote = new DelegateCommand(CheckAndInvokeCommand, CanExecuteCommandTrue);

                }
                return createNote;
            }
        }

        public void CheckAndInvokeCommand(object parameter)
        {
            Console.WriteLine("CheckAndInvokeCommand");
            if (TextToCreate == null)
                throw new ArgumentException("Text is null");
            if (TopicToCreate == null)
                throw new ArgumentException("Topic is null");

            NotesArr.CreateNote(TopicToCreate, TextToCreate);
        }

        public void DeleteNote(object NoteObj)
        {
            NotesArr.Remove((Note)NoteObj);
        }

        public void EditNote(object NoteObj)
        {
            Note oldNote = (Note)NoteObj;
            int index = NotesArr.GetNoteIndex(oldNote);
            NotesArr.RemoveNote(oldNote);

            Note newNote = new Note(TopicToEdit, TextToEdit);
            newNote.setEdited();
            NotesArr.CreateNote(newNote, index);
        }
    }
}
