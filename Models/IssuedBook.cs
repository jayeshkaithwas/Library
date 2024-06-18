using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class IssuedBook
    {
        [Key]
        public int IssueID { get; set; }
        public int BookID { get; set; }
        public int MemberID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public virtual Book Book { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
    }
}
