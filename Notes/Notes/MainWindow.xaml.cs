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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notes
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();

            //TextField.TextChanged += TextBoxOnChange;
        }
        
        private void TextBoxOnChange(object sender, EventArgs eventArgs)
        {
            System.Diagnostics.Debug.WriteLine("TextBoxOnChanged");
            System.Diagnostics.Debug.WriteLine("TextField : " + TextField.Text);

            //textBox.Text = TextField.Text;
            
            //checkCreateFields();
        }
        public void MW_createNote(object sender, RoutedEventArgs e)
        {
            NoteCreator noteCreator = new NoteCreator();
            string topic = "";
            string text = "";
            noteCreator.Topic += value => topic = value;
            noteCreator.Text += value => text = value;
            noteCreator.ShowDialog();

            ((MainViewModel)DataContext).TopicToCreate = topic;
            ((MainViewModel)DataContext).TextToCreate = text;

            System.Diagnostics.Debug.WriteLine("TOPIC FROM DIALOG: === " + topic);
            System.Diagnostics.Debug.WriteLine("TEXT FROM DIALOG: === " + text);

            //TextField.Text = text;
        }

        public void MW_deleteNote(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Are you sure?";
            string caption = "Delete Note?";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                ((MainViewModel)DataContext).DeleteNote((Note)NotesList.SelectedItem);
                textBox.Text = "";
            }
        }

        //public void MW_editNote(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        ((MainViewModel)DataContext).TextToEdit = TextField.Text;

        //        // Call function foro editing.
        //        ((MainViewModel)DataContext).EditNote((Note)NotesList.SelectedItem);
        //    }
        //    catch (Exception exception)
        //    {
        //        if (exception is FormatException || exception is OverflowException)
        //        {
        //            btnEditNote.IsEnabled = false;
        //        }
        //    }
        //}

        private void NotesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NotesList.SelectedIndex == -1)
            {
                btnDeleteNote.IsEnabled = false;
            }
            else
            {
                Note selected = (Note)NotesList.SelectedItem;
                try
                {
                    btnDeleteNote.IsEnabled = true;
                    ((MainViewModel)DataContext).TextToCreate = selected.Text;
                    textBox.Text = selected.Text;
                    Console.WriteLine("selected: " + selected.Time.ToString() + selected.Text);
                    Console.WriteLine("selected.TimeToShow : {0}", selected.TimeToShow);

                }
                catch (Exception exception)
                {
                    if (exception is FormatException || exception is OverflowException)
                    {
                        btnDeleteNote.IsEnabled = false;
                    }
                }
            }
        }

        private void NotesList_DoubleClicked(object sender, EventArgs e)
        {
            if (NotesList.SelectedIndex == -1)
            {
                //btnEditNote.IsEnabled = false;
                //btnDeleteNote.IsEnabled = false;
            }
            else
            {
                Note selected = (Note)NotesList.SelectedItem;
                try
                {
                    //btnEditNote.IsEnabled = true;
                    //btnDeleteNote.IsEnabled = true;
                    //((MainViewModel)DataContext).TextToCreate = selected.Text;
                    //TextField.Text = selected.Text;

                    Console.WriteLine("Doubleclicked : " + selected.Topic);


                    ((MainViewModel)DataContext).TextToEdit = selected.Text;
                    ((MainViewModel)DataContext).TopicToEdit = selected.Topic;

                    NoteEditor noteEditor = new NoteEditor();

                    noteEditor.TopicField.Text = selected.Topic;
                    noteEditor.TextField.Text = selected.Text;

                    string text = "";
                    string topic = "";
                    noteEditor.NewText += value => text = value;
                    noteEditor.NewTopic += value => topic = value;
                    noteEditor.ShowDialog();

                    if (!String.IsNullOrEmpty(text))
                        ((MainViewModel)DataContext).TextToEdit = text;
                    else
                        return;
                    if (!String.IsNullOrEmpty(topic))
                        ((MainViewModel)DataContext).TopicToEdit = topic;
                    else
                        return;

                    System.Diagnostics.Debug.WriteLine("TEXT FROM DIALOG: === " + text);
                    System.Diagnostics.Debug.WriteLine("TOPIC FROM DIALOG: === " + topic);

                    ((MainViewModel)DataContext).EditNote((Note)NotesList.SelectedItem);

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                }
            }
        }

        private void NotesList_RightClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Right click");
        }

    }
}
