using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterApplication.DB.Models
{
    public class BaseModel
    {
        public long Id { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; }
    }

    public class DeactivateModel
    {
        public long Id { get; set; }
        public string TableName { get; set; }
    }
    public class CommonAttribute : BaseModel
    {
        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s,.-]{1,}", ErrorMessage = "Name should only contain letters and spaces.")]
        public string Name { get; set; }
        public string AttributeType { get; set; }
        public string ImgUrl { get; set; }
    }
}
