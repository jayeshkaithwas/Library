using LibraryManagementSystem.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace LibraryManagementSystem
{
    public partial class ManageMembers : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<Member> _members = new ObservableCollection<Member>();

        public ObservableCollection<Member> Members
        {
            get { return _members; }
            set
            {
                _members = value;
                OnPropertyChanged(nameof(Members));
            }
        }

        public ManageMembers()
        {
            InitializeComponent();
            DataContext = this;
            LoadMembers();
        }

        private void LoadMembers()
        {
            using (var context = new LibraryContext())
            {
                var members = context.Members.ToList();
                foreach (var member in members)
                {
                    Members.Add(member);
                }
            }
        }

        private void ShowAddMemberForm_Click(object sender, RoutedEventArgs e)
        {
            AddMemberForm.Visibility = Visibility.Visible;
        }

        private void AddMember_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text.Trim();
            string lastName = LastNameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string phoneNumber = PhoneNumberTextBox.Text.Trim();

            using (var context = new LibraryContext())
            {
                // Check if a member with the same FirstName and LastName already exists
                bool memberExists = context.Members.Any(m => m.FirstName == firstName && m.LastName == lastName);

                if (memberExists)
                {
                    MessageBox.Show($"The member {firstName} {lastName} is already registered.", "Member Already Registered", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    var newMember = new Member { FirstName = firstName, LastName = lastName, Email = email, PhoneNumber = phoneNumber };

                    context.Members.Add(newMember);
                    context.SaveChanges();

                    Members.Add(newMember);

                    // Clear input fields
                    FirstNameTextBox.Clear();
                    LastNameTextBox.Clear();
                    EmailTextBox.Clear();
                    PhoneNumberTextBox.Clear();

                    // Hide the form
                    AddMemberForm.Visibility = Visibility.Collapsed;
                }
            }
        }


        private void CancelAddMember_Click(object sender, RoutedEventArgs e)
        {
            // Hide the form without adding a member
            AddMemberForm.Visibility = Visibility.Collapsed;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
