using Dapper;
using MasterApplication.DB.Interface;
using MasterApplication.DB.Models;
using MasterApplication.DB.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterApplication.DB.Implements
{

    public class MemberRegistrationDb : BaseDB, IMemberRegistrationDb
    {
        public MemberRegistrationDb(IConfiguration configuration) : base(configuration)
        {

        }
        // personalInfo
        public bool AddPersonalInfo(PersonalInfo personalInfo)
        {
            try
            {
                string query = @"
        INSERT INTO PersonalInfo
        (
            name,
            IcNumber ,
            reportingPeriod,
            unit,
            comd,
            typeOfReport,
            parentArm,
            apptHeld,
            dtOfCommission,
            medCategory,
            dtOfSeniority,
            category,
            awardOfWings,
            instrCategory,
            tosDate,
            lastAppearedWithGebCeb,
            imgUrl,
            createdby,
            createdon,
            isactive,
            isdeleted
        )
        VALUES
        (
            @Name,
            @IcNumber, 
            @ReportingPeriod,
            @Unit,
            @Comd,
            @TypeOfReport,
            @ParentArm,
            @ApptHeld,
            @DtOfCommission,
            @MedCategory,
            @DtOfSeniority,
            @Category,
            @AwardOfWings,
            @InstrCategory,
            @TosDate,
            @LastAppearedWithGebCeb,
            @ImgUrl,
            @CreatedBy,
            @CreatedOn,
            @IsActive,
            @IsDeleted
        )";

                var result = connection.Execute(query, personalInfo);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=PersonalInfoDb,method=AddPersonalInfo");
                throw;
            }
        }

        public bool UpdatePersonalInfo(PersonalInfo personalInfo)
        {
            try
            {
                string query = @"
        UPDATE PersonalInfo
        SET
            name = @Name,
            reportingPeriod = @ReportingPeriod,
            unit = @Unit,
            comd = @Comd,
            typeOfReport = @TypeOfReport,
            parentArm = @ParentArm,
            apptHeld = @ApptHeld,
            dtOfCommission = @DtOfCommission,
            medCategory = @MedCategory,
            dtOfSeniority = @DtOfSeniority,
            category = @Category,
            awardOfWings = @AwardOfWings,
            instrCategory = @InstrCategory,
            tosDate = @TosDate,
            lastAppearedWithGebCeb = @LastAppearedWithGebCeb,
            imgUrl = @ImgUrl,
            updatedby = @UpdatedBy,
            updatedon = @UpdatedOn
        WHERE id = @Id";

                var result = connection.Execute(query, personalInfo);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=PersonalInfoDb,method=UpdatePersonalInfo");
                throw;
            }
        }
        public IEnumerable<PersonalInfo> GetAllPersonalInfo()
        {
            try
            {
                string query = @"
        SELECT 
            id,
            name,
            reportingPeriod,
            unit,
            comd,
            typeOfReport,
            parentArm,
            apptHeld,
            dtOfCommission,
            medCategory,
            dtOfSeniority,
            category,
            awardOfWings,
            instrCategory,
            tosDate,
            lastAppearedWithGebCeb,
            imgUrl,
            createdby,
            updatedby,
            createdon,
            updatedon,
            isactive,
            isdeleted
        FROM PersonalInfo
        WHERE isdeleted = 0";

                return connection.Query<PersonalInfo>(query).ToList();
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=PersonalInfoDb,method=GetAllPersonalInfo");
                throw;
            }
        }

        public PersonalInfo GetPersonalInfoById(int id)
        {
            try
            {
                string query = @"
        SELECT 
            id,
            name,
            reportingPeriod,
            unit,
            comd,
            typeOfReport,
            parentArm,
            apptHeld,
            dtOfCommission,
            medCategory,
            dtOfSeniority,
            category,
            awardOfWings,
            instrCategory,
            tosDate,
            lastAppearedWithGebCeb,
            imgUrl,
            createdby,
            updatedby,
            createdon,
            updatedon,
            isactive,
            isdeleted
        FROM PersonalInfo
        WHERE id = @Id AND isdeleted = 0";

                return connection.QueryFirstOrDefault<PersonalInfo>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=PersonalInfoDb,method=GetPersonalInfoById");
                throw;
            }
        }

        //LastThreeAAAS
        public bool AddLastThreeAAAS(LastThreeAAAS lastThreeAAAS)
        {
            try
            {
                string query = @"
        INSERT INTO LastThreeAAAS
        (
            personalInfoId,
            type,
            [from],
            [to],
            io,
            ro,
            sro,
            createdby,
            createdon,
            isactive,
            isdeleted
        )
        VALUES
        (
            @PersonalInfoId,
            @Type,
            @From,
            @To,
            @IO,
            @RO,
            @SRO,
            @CreatedBy,
            @CreatedOn,
            @IsActive,
            @IsDeleted
        )";

                var result = connection.Execute(query, lastThreeAAAS);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=LastThreeAAASDb,method=AddLastThreeAAAS");
                throw;
            }
        }
        //AccidentDetails
        public bool AddAccidentDetails(AccidentDetails accidentDetails)
        {
            try
            {
                string query = @"
        INSERT INTO AccidentDetails
        (
            personalInfoId,
            dt,
            acNoAndType,
            unitOrLOC,
            blamworthy,
            cause,
            statusPunish,
            createdby,
            createdon,
            isactive,
            isdeleted
        )
        VALUES
        (
            @PersonalInfoId,
            @Dt,
            @AcNoAndType,
            @UnitOrLOC,
            @Blamworthy,
            @Cause,
            @StatusPunish,
            @CreatedBy,
            @CreatedOn,
            @IsActive,
            @IsDeleted
        )";



                var result = connection.Execute(query, accidentDetails);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=AccidentDetailsDb,method=AddAccidentDetails");
                throw;
            }
        }

        //AdvExecRptRaised
        public bool AddAdvExecRptRaised(AdvExecRptRaised advExecRptRaised)
        {
            try
            {
                string query = @"
        INSERT INTO AdvExecRptRaised
        (
            personalInfoId,
            dt,
            unit,
            io,
            ro,
            sro,
            decisionByDG,
            createdby,
            createdon,
            isactive,
            isdeleted
        )
        VALUES
        (
            @PersonalInfoId,
            @Dt,
            @Unit,
            @IO,
            @RO,
            @SRO,
            @DecisionByDG,
            @CreatedBy,
            @CreatedOn,
            @IsActive,
            @IsDeleted
        )";


                var result = connection.Execute(query, advExecRptRaised);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=AdvExecRptRaisedDb,method=AddAdvExecRptRaised");
                throw;
            }
        }

        //PhysService
        public bool AddPhysService(PhysService physService)
        {
            try
            {
                string query = @"
        INSERT INTO PhysService
        (
            personalInfoId,
            previouseIo,
            presentIo,
            previousRo,
            presentRo,
            previousSro,
            presentSro,
            dayServed,
            InitRptOfficer,
            createdby,
            createdon,
            isactive,
            isdeleted
        )
        VALUES
        (
            @PersonalInfoId,
            @PreviouseIo,
            @PresentIo,
            @PreviousRo,
            @PresentRo,
            @PreviousSro,
            @PresentSro,
            @DayServed,
            @InitRptOfficer,
            @CreatedBy,
            @CreatedOn,
            @IsActive,
            @IsDeleted
        )";


                var result = connection.Execute(query, physService);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=PhysServiceDb,method=AddPhysService");
                throw;
            }
        }
        //ForeignVisit
        public bool AddForeignVisit(ForeignVisit foreignVisit)
        {
            try
            {
                string query = @"
        INSERT INTO ForeignVisit
        (
            personalInfoId,
            appt,
            [from],
            [to],
            country,
            remark,
            createdby,
            createdon,
            isactive,
            isdeleted
        )
        VALUES
        (
            @PersonalInfoId,
            @Appt,
            @FromDate,
            @ToDate,
            @Country,
            @Remark,
            @CreatedBy,
            @CreatedOn,
            @IsActive,
            @IsDeleted
        )";

            

                var result = connection.Execute(query, foreignVisit);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=ForeignVisitDb,method=AddForeignVisit");
                throw;
            }
        }
        //DvDtails
        public bool AddDvDtails(DvDtails dvDtails)
        {
            try
            {
                string query = @"
        INSERT INTO DvDtails
        (
            personalInfoId,
            details,
            createdby,
            createdon,
            isactive,
            isdeleted
        )
        VALUES
        (
            @PersonalInfoId,
            @Details,
            @CreatedBy,
            @CreatedOn,
            @IsActive,
            @IsDeleted
        )";

                var result = connection.Execute(query, dvDtails);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=DvDtailsDb,Method=AddDvDtails");
                throw;
            }
        }
        public bool AddServiceFLG(ServiceFLG serviceFLG)
        {
            try
            {
                string query = @"
        INSERT INTO ServiceFLG
        (
            personalInfoId,
            [type],
            dual,
            pilot,
            coPilot,
            nvg,
            sml,
            instr,
            createdby,
            createdon,
            isactive,
            isdeleted
        )
        VALUES
        (
            @PersonalInfoId,
            @Type,
            @Dual,
            @Pilot,
            @CoPilot,
            @Nvg,
            @Sml,
            @Instr,
            @CreatedBy,
            @CreatedOn,
            @IsActive,
            @IsDeleted
        )";

                var result = connection.Execute(query, serviceFLG);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=ServiceFLGDb,Method=AddServiceFLG");
                throw;
            }
        }
        //CourseAvnQual
        public bool AddCourseAvnQual(CourseAvnQual courseAvnQual)
        {
            try
            {
                string query = @"
        INSERT INTO CourseAvnQual
        (
            personalInfoId,
            qualId,
            instt,
            granding,
            remark,
            createdby,
            createdon,
            isactive,
            isdeleted
        )
        VALUES
        (
            @PersonalInfoId,
            @QualId,
            @Instt,
            @Granding,
            @Remark,
            @CreatedBy,
            @CreatedOn,
            @IsActive,
            @IsDeleted
        )";

                var result = connection.Execute(query, courseAvnQual);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=CourseAvnQualDb,Method=AddCourseAvnQual");
                throw;
            }
        }
        //CoreAttributeFlgRating
        public bool AddCoreAttributeFlgRating(CoreAttributeFlgRating coreAttributeFlgRating)
        {
            try
            {
                string query = @"
        INSERT INTO CoreAttributeFlgRating
        (
            personalInfoId,
            coreAttrId,
            io,
            ro,
            sro,
            createdby,
            createdon,
            isactive,
            isdeleted
        )
        VALUES
        (
            @PersonalInfoId,
            @CoreAttrId,
            @Io,
            @Ro,
            @Sro,
            @CreatedBy,
            @CreatedOn,
            @IsActive,
            @IsDeleted
        )";

                var result = connection.Execute(query, coreAttributeFlgRating);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=CoreAttributeFlgRatingDb,Method=AddCoreAttributeFlgRating");
                throw;
            }
        }
        //RecomForEMP
        public bool AddRecomForEMP(RecomForEMP recomForEMP)
        {
            try
            {
                string query = @"
        INSERT INTO RecomForEMP
        (
            personalInfoId,
            recomId,
            io,
            ro,
            sro,
            createdby,
            createdon,
            isactive,
            isdeleted
        )
        VALUES
        (
            @PersonalInfoId,
            @RecomId,
            @Io,
            @Ro,
            @Sro,
            @CreatedBy,
            @CreatedOn,
            @IsActive,
            @IsDeleted
        )";

                var result = connection.Execute(query, recomForEMP);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=RecomForEMPDb,Method=AddRecomForEMP");
                throw;
            }
        }
        //ReportingOfficersDetails
        public bool AddReportingOfficersDetails(ReportingOfficersDetails reportingOfficersDetails)
        {
            try
            {
                string query = @"
        INSERT INTO ReportingOfficersDetails
        (
            personalInfoId,
            ReportingOfficer,
            sign,
            rk,
            name,
            appt,
            dtOfRecdOn,
            dtOfDespOn,
            createdby,
            createdon,
            isactive,
            isdeleted
        )
        VALUES
        (
            @PersonalInfoId,
            @ReportingOfficer,
            @Sign,
            @Rk,
            @Name,
            @Appt,
            @DtOfRecdOn,
            @DtOfDespOn,
            @CreatedBy,
            @CreatedOn,
            @IsActive,
            @IsDeleted
        )";

                var result = connection.Execute(query, reportingOfficersDetails);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=ReportingOfficersDetailsDb,Method=AddReportingOfficersDetails");
                throw;
            }
        }



    }
}
