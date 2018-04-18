﻿using Notes.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notes.Views
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
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

        private void NotesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NotesList.SelectedIndex == -1)
            {
                deleteButton.IsEnabled = false;
            }
            else
            {
                Note selected = (Note)NotesList.SelectedItem;
                try
                {
                    deleteButton.IsEnabled = true;
                    ((MainViewModel)DataContext).TextToCreate = selected.Text;
                    textBox.Text = selected.Text;
                    Console.WriteLine("selected.selected.Text : {0} " + selected.Text);
                    Console.WriteLine("selected.selected.Topic : {0} " + selected.Topic);
                    Console.WriteLine("selected.selected.State : {0} " + selected.State);
                    Console.WriteLine("selected.selected.Time : {0} " + selected.Time.ToString());
                    Console.WriteLine("selected.TimeToShow : {0}", selected.TimeToShow);

                }
                catch (Exception exception)
                {
                    if (exception is FormatException || exception is OverflowException)
                    {
                        deleteButton.IsEnabled = false;
                    }
                }
            }
        }

        private void NotesList_DoubleClicked(object sender, EventArgs e)
        {
            if (NotesList.SelectedIndex == -1)
            {
                deleteButton.IsEnabled = false;
            }
            else
            {
                Note selected = (Note)NotesList.SelectedItem;
                try
                {
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
                    {
                        ((MainViewModel)DataContext).TextToEdit = text;
                    }
                    else
                    {
                        return;
                    }
                    if (!String.IsNullOrEmpty(topic))
                    {
                        ((MainViewModel)DataContext).TopicToEdit = topic;
                    }
                    else
                    {
                        return;
                    }
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

        private void setImportant_Click(object sender, EventArgs e)
        {
            if (NotesList.SelectedIndex == -1)
            {
                deleteButton.IsEnabled = false;
            }
            else
            {
                Note selected = (Note)NotesList.SelectedItem;
                try
                {
                    Console.WriteLine("setImportant_Click");
                    //selected = new ImportantNote(selected);

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                }
            }
        }

    }
}