using System;
using System.Collections.Generic;
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
}
