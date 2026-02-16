using System;
using System.Collections.Generic;
using System.Text;

namespace MasterApplication.DB.Models
{
    public class PersonalInfo : BaseModel
    {
        public string Name { get; set; } = null!;

        public string ReportingPeriod { get; set; }

        public string Unit { get; set; }

        public string Comd { get; set; }

        public string TypeOfReport { get; set; }

        public string ParentArm { get; set; }

        public string ApptHeld { get; set; }

        public string DtOfCommission { get; set; }

        public string MedCategory { get; set; }

        public string DtOfSeniority { get; set; }

        public string Category { get; set; }

        public string AwardOfWings { get; set; }

        public string InstrCategory { get; set; }

        public DateTime TosDate { get; set; }

        public string LastAppearedWithGebCeb { get; set; }

        public string ImgUrl { get; set; }
        public int? RoleId { get; set; }

        public int? RoleType { get; set; }
    }
}

