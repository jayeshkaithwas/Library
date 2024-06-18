using LibraryManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace LibraryManagementSystem
{
    public partial class SearchBooks : Window
    {
        public SearchBooks()
        {
            InitializeComponent();
            LoadAllBooks();
        }

        private void LoadAllBooks()
        {
            using (var context = new LibraryContext())
            {
                var allBooks = context.Books.ToList();
                DisplayBooks(allBooks);
            }
        }

        private void DisplayBooks(List<Book> books)
        {
            SearchResultsListBox.Items.Clear();
            foreach (var book in books)
            {
                string availability = book.Available ? "Available" : "Not Available";
                SearchResultsListBox.Items.Add($"{book.Title} by {book.Author} - {availability}");
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchTextBox.Text.ToLower();

            using (var context = new LibraryContext())
            {
                // Query the database for books that match the search query
                var searchResults = context.Books
                    .Where(book => book.Title.ToLower().Contains(query) || book.Author.ToLower().Contains(query))
                    .ToList();

                // List all books that do not match the search query
                var otherBooks = context.Books
                    .Where(book => !book.Title.ToLower().Contains(query) && !book.Author.ToLower().Contains(query))
                    .ToList();

                // Clear the previous search results
                SearchResultsListBox.Items.Clear();

                // Display search results followed by other books
                DisplayBooks(searchResults.Concat(otherBooks).ToList());
            }
        }
    }
}
