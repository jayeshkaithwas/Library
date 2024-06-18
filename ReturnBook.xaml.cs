using System;
using System.Linq;
using System.Windows;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem
{
    public partial class ReturnBook : Window
    {
        public ReturnBook()
        {
            InitializeComponent();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            string bookTitle = BookTitleTextBox.Text;
            string memberName = MemberNameTextBox.Text;

            using (var context = new LibraryContext())
            {
                // Find Book
                Book? book = context.Books.FirstOrDefault(b => b.Title == bookTitle)!;
                if (book == null)
                {
                    MessageBox.Show("Book not found!");
                    return;
                }

                // Find Member
                Member? member = context.Members.FirstOrDefault(m => (m.FirstName + " " + m.LastName) == memberName)!;
                if (member == null)
                {
                    MessageBox.Show("Member not found!");
                    return;
                }

                // Find IssuedBook entry
                IssuedBook? issuedBook = context.IssuedBooks.FirstOrDefault(ib => ib.BookID == book.BookID && ib.MemberID == member.MemberID && ib.ReturnDate == null)!;
                if (issuedBook == null)
                {
                    MessageBox.Show("This book is not currently issued to this member!");
                    return;
                }

                // Update ReturnDate and set book as available
                issuedBook.ReturnDate = DateTime.Now;
                book.Available = true;

                // Save changes to the database
                context.SaveChanges();

                MessageBox.Show("Book returned successfully!");
                this.Close();
            }
        }
    }
}
