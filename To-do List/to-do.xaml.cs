using System;
using System.Windows.Controls;

namespace To_do_List
{
    public partial class to_do : UserControl
    {
        public event EventHandler Completed;

        public to_do()
        {
            InitializeComponent();
        }

        private void Edit_btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            todo_txtbox.IsReadOnly = !todo_txtbox.IsReadOnly;
        }

        private void Completed_btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            todo_txtbox.IsEnabled = !todo_txtbox.IsEnabled;
            Edit_btn.IsEnabled = !Edit_btn.IsEnabled;

            Completed?.Invoke(this, EventArgs.Empty);
        }

    }
}
