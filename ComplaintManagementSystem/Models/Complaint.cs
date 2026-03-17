using ComplaintManagementSystem.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ComplaintManagementSystem.Models
{  
    public class Complaint
    {
        [Key]
        public int ComplaintId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(500)]

        public string Description { get; set; }

        public ComplaintStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }


        [ForeignKey("UserId")]

        [ValidateNever]
        public User User { get; set; }
    }

}
