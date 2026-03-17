using ComplaintManagementSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ComplaintManagementSystem.Models.ViewModels
{
    public class EditComplaintVM
    {
        public int ComplaintId { get; set; }
        public ComplaintStatus Status { get; set; }

        // Display-only
        public string UserName { get; set; }
    }
}
