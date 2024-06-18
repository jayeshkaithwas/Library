using System.Windows;

namespace LibraryManagementSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            AddBook addBookWindow = new();
            addBookWindow.ShowDialog();
        }
        private void SearchBooks_Click(object sender, RoutedEventArgs e)
        {
            SearchBooks searchBooksWindow = new();
            searchBooksWindow.ShowDialog();
        }

        private void ManageMembers_Click(object sender, RoutedEventArgs e)
        {
            ManageMembers ManageMembersWindow = new();
            ManageMembersWindow.ShowDialog();
        }
        private void IssueBook_Click(object sender, RoutedEventArgs e)
        {
            IssueBook issueBookWindow = new();
            issueBookWindow.ShowDialog();
        }
        private void ViewIssuedBooks_Click(object sender, RoutedEventArgs e)
        {
            IssuedBooksView IssuedBooksViewWindow = new();
            IssuedBooksViewWindow.Show();
        }
        private void ReturnBookButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnBook returnBookWindow = new();
            returnBookWindow.ShowDialog();
        }


    }
}
