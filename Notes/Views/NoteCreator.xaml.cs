using Notes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Notes.Models;

namespace Notes.Views
{

    public partial class NoteCreator : Window
    {
        public event Action<string> TopicAction;
        public event Action<string> TextAction;
        public event Action<bool> IsImportantAction;
        public event Action<DateTime> DeadlineAction;

        public static string TopicFieldText { get; set; }
        public static string TextFieldText { get; set; }

        public NoteCreator()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            DeadlinePicker.Text = DateTime.Today.ToShortDateString();
            DeadlinePicker.SelectedDate = DateTime.Today;
            

            Console.WriteLine("DeadlinePicker.Text : " + DeadlinePicker.Text);
            Console.WriteLine("DeadlinePicker.SelectedDate : " + DeadlinePicker.SelectedDate);
        }

        public void createNoteClicked(object sender, RoutedEventArgs e)
        {
            AbstractActionSender actionSender = new ActionSender();

            if (ImportantCheckBox.IsChecked == true)
            {
                Console.WriteLine("important note checked");
                actionSender.Implementor = new ImportantImplementation(TopicAction, TextAction, IsImportantAction);
            }
            else
            {
                Console.WriteLine("not important note");
                actionSender.Implementor = new NotImportantImplementation(TopicAction, TextAction);
            }
            actionSender.SendNoteInfo(TopicFieldText, TextFieldText);

            DateTime deadline = new DateTime();
            try
            {
                deadline = (DateTime)DeadlinePicker.SelectedDate;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (deadline < DateTime.Today)
            {
                Console.WriteLine("Deadline is in the past, and is not been setted");
                
            }
            else
            {
                DeadlineAction(deadline);
            }

            this.Close();
        }

        private void Important_Checked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Checked");
        }


        private void TextChanged(object sender, RoutedEventArgs e)
        {
            TopicFieldText = ((TextBox)sender).Text;
        }
        private void TitleChanged(object sender, RoutedEventArgs e)
        {
            TextFieldText = ((TextBox)sender).Text;
        }
    }
}
