using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace To_do_List
{
    public partial class MainWindow : Window
    {
        private string filePath = "save/";

        public MainWindow()
        {
            InitializeComponent();
            Calendar.SelectedDate = DateTime.Now;
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            if (todolist_stack_panel.Children.Count < 7)
            {
                to_do newToDo = new to_do { Margin = new System.Windows.Thickness(0, 5, 0, 0) };
                newToDo.Completed += ToDo_Completed;
                todolist_stack_panel.Children.Add(newToDo);
                UpdateProgressBar();
            }
        }

        private void ToDo_Completed(object sender, EventArgs e)
        {
            to_do toDoControl = sender as to_do;
            if (todolist_stack_panel.Children.Contains(toDoControl))
            {
                todolist_stack_panel.Children.Remove(toDoControl);
                todolist_completed_stack_panel.Children.Add(toDoControl);
            }
            else
            {
                todolist_completed_stack_panel.Children.Remove(toDoControl);
                todolist_stack_panel.Children.Add(toDoControl);
            }
            UpdateProgressBar();
        }

        private void UpdateProgressBar()
        {
            int totalItems = todolist_stack_panel.Children.Count + todolist_completed_stack_panel.Children.Count;
            if (totalItems > 0)
            {
                double completedPercentage = (double)todolist_completed_stack_panel.Children.Count / totalItems * 100.0;
                ProgressBar.Value = completedPercentage;
            }
            else
            {
                ProgressBar.Value = 0;
            }
        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            if (todolist_stack_panel.Children.Count == 0)
                return;

            string fileName = filePath + Calendar.SelectedDate.Value.ToString("yyyy-MM-dd") + ".txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (var item in todolist_stack_panel.Children)
                    {
                        if (item is to_do t)
                            writer.WriteLine(t.todo_txtbox.Text);
                    }
                }

                MessageBox.Show("To-do list saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving to-do list: " + ex.Message);
            }
        }

        private void Load_btn_Click(object sender, RoutedEventArgs e)
        {
            string fileName = filePath + Calendar.SelectedDate.Value.ToString("yyyy-MM-dd") + ".txt";

            try
            {
                todolist_stack_panel.Children.Clear();
                todolist_completed_stack_panel.Children.Clear();

                string[] lines = File.ReadAllLines(fileName);

                foreach (string line in lines)
                {
                    to_do newToDo = new to_do { Margin = new Thickness(0, 5, 0, 0) };
                    newToDo.todo_txtbox.Text = line;
                    newToDo.Completed += ToDo_Completed;

                    todolist_stack_panel.Children.Add(newToDo);
                }

                MessageBox.Show("To-do list loaded successfully.");
                UpdateProgressBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading to-do list: " + ex.Message);
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Load_btn_Click(null, null);

        }
    }


}

