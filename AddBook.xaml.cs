using LibraryManagementSystem.Models;
using System;
using System.Linq;
using System.Windows;

namespace LibraryManagementSystem
{
    public partial class AddBook : Window
    {
        public AddBook()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new LibraryContext())
            {
                string title = TitleTextBox.Text.Trim();
                string author = AuthorTextBox.Text.Trim();

                // Check if a book with the same title and author already exists
                bool bookExists = context.Books.Any(b => b.Title == title && b.Author == author);

                if (bookExists)
                {
                    MessageBox.Show("The book with the same title and author is already registered.", "Book Already Registered", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    var book = new Book
                    {
                        Title = title,
                        Author = author,
                        PublishedDate = DateTime.Now, // or any other date
                        Available = true
                    };

                    context.Books.Add(book);
                    context.SaveChanges();

                    MessageBox.Show("Book Added Successfully");
                }
            }
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TitlePlaceholder.Visibility = string.IsNullOrWhiteSpace(TitleTextBox.Text) ? Visibility.Visible : Visibility.Hidden;
            AuthorPlaceholder.Visibility = string.IsNullOrWhiteSpace(AuthorTextBox.Text) ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
