using System;
using System.Linq;
using System.Windows;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem
{
    public partial class IssueBook : Window
    {
        public IssueBook()
        {
            InitializeComponent();
            IssueDatePicker.SelectedDate = DateTime.Now;
        }

        private void IssueButton_Click(object sender, RoutedEventArgs e)
        {
            string bookTitle = BookTitleTextBox.Text;
            string memberName = MemberNameTextBox.Text;

            // Find BookID
            int bookID = 0;
            using (var context = new LibraryContext())
            {
                Book? book = context.Books.FirstOrDefault(b => b.Title == bookTitle)!;
                if (!book.Available)
                {
                    MessageBox.Show("Book is not available for issuing!");
                    return;
                }
                else
                {
                    if (book != null)
                {
                    bookID = book.BookID;
                    book.Available = false;
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Book not found!");
                    return;
                }
                }
            }

            // Find MemberID
            int memberID = 0;
            using (var context = new LibraryContext())
            {
                Member? member = context.Members.FirstOrDefault(m => (m.FirstName + " " + m.LastName) == memberName)!;

                if (member != null)
                {
                    memberID = member.MemberID;
                }
                else
                {
                    MessageBox.Show("Member not found!");
                    return;
                }
            }

            DateTime issueDate = IssueDatePicker.SelectedDate ?? DateTime.Now;

            IssuedBook newIssuedBook = new()
            {
                BookID = bookID,
                MemberID = memberID,
                IssueDate = issueDate,
                // ReturnDate can be null initially
            };

            // Save to database
            using (var context = new LibraryContext())
            {
                context.IssuedBooks.Add(newIssuedBook);
                context.SaveChanges();
            }

            MessageBox.Show("Book issued successfully!");
            this.Close();
        }
    }
}
