using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static string Topic { get; set; }
        public static string Text { get; set; }

        ButtonChecker buttonChecker;

        public void editNoteClicked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("editNoteClicked");
            
            NewTopic(TopicField.Text);
            NewText(TextField.Text);
            this.Close();
        }
        public NoteEditor()
        {
            InitializeComponent();
            
            buttonChecker = new ButtonChecker();
        }

        //Proxy

        interface IButtonEnabler
        {
            void EnableButton(Button button);
        }
        
        class ButtonEnabler : IButtonEnabler
        {
            public void EnableButton(Button button)
            {
                button.IsEnabled = true;
            }
        }
        
        class ButtonChecker : IButtonEnabler
        {
            ButtonEnabler btnEnabler = new ButtonEnabler();

            public void EnableButton(Button button)
            {
                if (!String.IsNullOrEmpty(Topic) && !String.IsNullOrEmpty(Text))
                {
                    btnEnabler.EnableButton(button);
                    Debug.WriteLine("Button enabled");
                } 
                else
                {
                    button.IsEnabled = false;
                }
            }
        }
        //Proxy ends

        private void TextChanged(object sender, RoutedEventArgs e)
        {
            Topic = ((TextBox)sender).Text;
            buttonChecker.EnableButton(EditButton);
            Debug.WriteLine("Topic : " + Topic);
        }
        private void TitleChanged(object sender, RoutedEventArgs e)
        {
            Text = ((TextBox)sender).Text;
            buttonChecker.EnableButton(EditButton);
            Debug.WriteLine("Text : " + Text);
        }

    }
}
