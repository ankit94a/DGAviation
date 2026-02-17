using MasterApplication.DB.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterApplication.DB.Interface
{
    public interface IMemberRegistrationDb
    {
        // personal info
        public bool AddPersonalInfo(PersonalInfo personalInfo);
        public bool UpdatePersonalInfo(PersonalInfo personalInfo);
        public IEnumerable<PersonalInfo> GetAllPersonalInfo();
        public PersonalInfo GetPersonalInfoById(int id);

        //LastThreeAAAS
        //public bool AddLastThreeAAAS(LastThreeAAAS lastThreeAAAS);

        ////AccidentDetails

        //public bool AddAccidentDetails(AccidentDetails accidentDetails);
        public Task<bool> SaveAAAAndAccident(AAAAndAccidentRequest request);

        //Advverse Executive Report Raised
        public bool AddAdvExecRptRaised(AdvExecRptRaised advExecRptRaised);
        //PhysService
        public bool AddPhysService(PhysService physService);
        //AddForeignVisit
        public bool AddForeignVisit(ForeignVisit foreignVisit);
       //dv details(Entire service)
        public bool AddDvDtails(DvDtails dvDtails);
      //total service flg
        public bool AddServiceFLG(ServiceFLG serviceFLG);
        // courses and avn specifi qual
        public bool AddCourseAvnQual(CourseAvnQual courseAvnQual);
        // part 2 attributes related to flg and recom for EMP

        public bool AddCoreAttributeFlgRating(CoreAttributeFlgRating coreAttributeFlgRating);
        // recom for EMP

        public bool AddRecomForEMP(RecomForEMP recomForEMP);
        // perticulars of reporting officers with sign and date

        public bool AddReportingOfficersDetails(ReportingOfficersDetails reportingOfficersDetails);

    }
}
