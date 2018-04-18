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
    public partial class NoteEditor : Window
    {
        public event Action<string> NewText;
        public event Action<string> NewTopic;

        public void editNoteClicked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("editNoteClicked");
            if (NewText != null && NewTopic != null)
            {
                NewTopic(TopicField.Text);
                NewText(TextField.Text);
            }
            this.Close();
        }
        public NoteEditor()
        {
            InitializeComponent();
        }
    }
}
