using System;
using System.Collections.Generic;
using System.Text;

namespace MasterApplication.DB.Models
{
    public class PersonalInfo : BaseModel
    {
        public bool IsNewRegistration { get; set; }
        public string IcNumber { get; set; }

        public string Name { get; set; } = null!;

        public string ReportingPeriod { get; set; }

        public string Unit { get; set; }

        public string Comd { get; set; }

        public string TypeOfReport { get; set; }

        public string ParentArm { get; set; }

        public string ApptHeld { get; set; }

        public DateTime DtOfCommission { get; set; }
        public string MedCategory { get; set; }

        public DateTime DtOfSeniority { get; set; }

        public string Category { get; set; }

        public string AwardOfWings { get; set; }

        public string InstrCategory { get; set; }

        public DateTime TosDate { get; set; }

        public string LastAppearedWithGebCeb { get; set; }
        

        public string ImgUrl { get; set; }
      
    }
   
    public class LastThreeAAAS : BaseModel
    {
        public int PersonalInfoId { get; set; }

        public string Type { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string IO { get; set; }

        public string RO { get; set; }

        public string SRO { get; set; }
    }
    public class AccidentDetails : BaseModel
    {
        public long? PersonalInfoId { get; set; }

        public DateTime? Dt { get; set; }

        public string AcNoAndType { get; set; } = null!;

        public string UnitOrLOC { get; set; }

        public string Blamworthy { get; set; }

        public string Cause { get; set; }

        public string StatusPunish { get; set; }
    }
    public class AAAAndAccidentRequest
    {
        public List<LastThreeAAAS> LastThreeAAAS { get; set; }
        public List<AccidentDetails> AccidentDetails { get; set; }
    }

    public class AdvExecRptRaised : BaseModel
    {
        public long? PersonalInfoId { get; set; }

        public string Dt { get; set; } = null!;

        public string Unit { get; set; } = null!;

        public string IO { get; set; }

        public string RO { get; set; }

        public string SRO { get; set; }

        public string DecisionByDG { get; set; }
    }
    public class PhysService : BaseModel
    {
        public long PersonalInfoId { get; set; }

        public string PreviouseIo { get; set; } = null!;

        public string PresentIo { get; set; } = null!;

        public string PreviousRo { get; set; }

        public string PresentRo { get; set; }

        public string PreviousSro { get; set; }

        public string PresentSro { get; set; }

        public string DayServed { get; set; }

        public string InitRptOfficer { get; set; }
    }
    public class ForeignVisit : BaseModel
    {
        public long PersonalInfoId { get; set; }

        public string Appt { get; set; } = null!;

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Country { get; set; }

        public string Remark { get; set; }
    }
    public class DvDtails : BaseModel
    {
        public long? PersonalInfoId { get; set; }
        public string Details { get; set; }
    }
    public class ServiceFLG : BaseModel
    {
        public long? PersonalInfoId { get; set; }
        public string Type { get; set; }
        public string Dual { get; set; }
        public string Pilot { get; set; }
        public string CoPilot { get; set; }
        public string Nvg { get; set; }
        public string Sml { get; set; }
        public string Instr { get; set; }
    }
    public class CourseAvnQual : BaseModel
    {
        public long? PersonalInfoId { get; set; }
        public int? QualId { get; set; }
        public string Instt { get; set; }
        public string Granding { get; set; }
        public string Remark { get; set; }
    }
    public class CoreAttributeFlgRating : BaseModel
    {
        public long PersonalInfoId { get; set; }
        public int CoreAttrId { get; set; }
        public int Io { get; set; }
        public int Ro { get; set; }
        public int Sro { get; set; }
    }
    public class RecomForEMP : BaseModel
    {
        public long? PersonalInfoId { get; set; }
        public int RecomId { get; set; }   
        public string Io { get; set; }
        public string Ro { get; set; }
        public string Sro { get; set; }
    }
    public class ReportingOfficersDetails : BaseModel
    {
        public long PersonalInfoId { get; set; }
        public string ReportingOfficer { get; set; }
        public string Sign { get; set; }
        public string Rk { get; set; }
        public string Name { get; set; }
        public string Appt { get; set; }
        public DateTime? DtOfRecdOn { get; set; }
        public DateTime? DtOfDespOn { get; set; }
    }

}

