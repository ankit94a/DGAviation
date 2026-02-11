using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterApplication.DB.Models
{
    public class PppMaster : BaseModel
    {
        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s,.-]{1,}", ErrorMessage = "Reference should only contain letters and spaces.")]
        public string Reference { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s,.-]{1,}", ErrorMessage = "Sponsor should only contain letters and spaces.")]
        public string Sponsor { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s,.-]{1,}", ErrorMessage = "Nature of project should only contain letters and spaces.")]
        public string NatureOfProject { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s,.-]{1,}", ErrorMessage = "Project details should only contain letters and spaces.")]
        public string ProjectDetails { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s,.-]{1,}", ErrorMessage = "Estimated Cost should only contain numbers.")]
        public string EstimatedCost { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s,.-]{1,}", ErrorMessage = "Cash Out Cost should only contain numbers.")]
        public string CashOutCost { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s,.-]{1,}", ErrorMessage = "Category should only contain letters and spaces.")]
        public string Category { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s,.-]{1,}", ErrorMessage = "Type should only contain letters and spaces.")]
        public string Type { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s,.-]{1,}", ErrorMessage = "Priority details should only contain letters and spaces.")]
        public string Priority { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s,.-]{1,}", ErrorMessage = "Status should only contain letters and spaces.")]
        public string Status { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s,.-]{1,}", ErrorMessage = "Remarks details should only contain letters and spaces.")]
        public string Remarks { get; set; }

    }

}
