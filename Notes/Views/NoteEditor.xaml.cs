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

        ButtonChecker buttonChecker;
        public SmartEditButton Button { get; set; }

        public void editNoteClicked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("editNoteClicked");

            //if (NewText != null && NewTopic != null)
            //{
            NewTopic(TopicField.Text);
            NewText(TextField.Text);
            //}
            this.Close();
        }
        public NoteEditor()
        {
            InitializeComponent();

            Button.realButton = EditButton;
            Button.realButton.IsEnabled = false;

            buttonChecker = new ButtonChecker(TopicField.Text, TextField.Text);
        }

        public class SmartEditButton
        {
            public Button realButton;
            public bool isEnabled;
        }
        //interface
        interface IButtonEnabler
        {
            void EnableButton(SmartEditButton button);
        }

        //realsubject
        class ButtonEnabler : IButtonEnabler
        {
            public void EnableButton(SmartEditButton button)
            {
                button.isEnabled = true;
            }
        }

        //proxy
        class ButtonChecker : IButtonEnabler
        {
            string topic;
            string description;
            public ButtonChecker(string topic, string description)
            {
                this.topic = topic;
                this.description = description;
            }
            ButtonEnabler btnEnabler = new ButtonEnabler();
            public void EnableButton(SmartEditButton smartButton)
            {
                if (topic != null && description != null)
                {
                    btnEnabler.EnableButton(smartButton);
                } 
                else
                {
                    smartButton.realButton.IsEnabled = false;
                }
            }
        }

        private void TextChanged(object sender, RoutedEventArgs e)
        {
            buttonChecker.EnableButton(Button);
        }
        private void TitleChanged(object sender, RoutedEventArgs e)
        {
            buttonChecker.EnableButton(Button);
        }

    }
}
