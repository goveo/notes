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

namespace Notes.Views
{

    public partial class NoteCreator : Window
    {
        public event Action<string> Topic;
        public event Action<string> Text;

        public void createNoteClicked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("createNoteClicked");
            if (Topic != null && Text != null)
            {
                Topic(TopicField.Text);
                Text(TextField.Text);
            }
            this.Close();
        }
        public NoteCreator()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
