using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ComplaintManagementSystem.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public List<Complaint> Complaints { get; set; }
    }

}
