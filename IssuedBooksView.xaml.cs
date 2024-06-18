using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem
{
    public partial class IssuedBooksView : Window
    {
        public IssuedBooksView()
        {
            InitializeComponent();
            LoadIssuedBooks();
        }

        private void LoadIssuedBooks()
        {
            using var context = new LibraryContext();
            var issuedBooks = context.IssuedBooks
                .Include(ib => ib.Book)
                .Include(ib => ib.Member)
                .Select(ib => new
                {
                    BookTitle = ib.Book.Title,
                    MemberName = ib.Member.FirstName + " " + ib.Member.LastName,
                    ib.IssueDate,
                    ib.ReturnDate
                })
                .ToList();

            IssuedBooksDataGrid.ItemsSource = issuedBooks;
        }
    }
}
