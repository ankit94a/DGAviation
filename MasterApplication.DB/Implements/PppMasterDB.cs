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
    public class PppMasterDB : BaseDB,IPppMasterDB
    {
        public PppMasterDB(IConfiguration configuration) : base(configuration) { }

        public List<PppMaster> GetAllPppMaster()
        {
            try
            {
                string query = string.Format("select * from pppmaster where isactive = 1 order by id desc");
                return connection.Query<PppMaster>(query).ToList();
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=PppMasterDB,method=GetAllPppMaster");
                throw;
            }
        }
        public bool AddPppMaster(PppMaster pppMaster)
        {
            try
            {
                string query = string.Format(@"insert into pppmaster (reference,sponsor,natureOfProject,projectDetails,estimatedCost,cashOutCost,category,type,priority,status,remarks,createdby,createdon,isactive,isDeleted) values (@reference,@sponsor,@natureOfProject,@projectDetails,@estimatedCost,@cashOutCost,@category
,@type,@priority,@status,@remarks,@createdby,@createdon,@isactive,@isDeleted)");
                var result = connection.Execute(query, pppMaster);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=PppMasterDB,method=AddPppMaster");
                throw;
            }
        }

        public bool UpdatePppMaster(PppMaster pppMaster)
        {
            try
            {
                if (pppMaster.UpdatedOn == default)
                    pppMaster.UpdatedOn = DateTime.Now;

                string query = @"UPDATE pppmaster SET reference = @Reference,sponsor=@Sponsor,natureOfProject=@natureOfProject,projectDetails=@projectDetails,estimatedCost=@estimatedCost,cashOutCost=@cashOutCost,category=@category,type=@type,priority=@priority,status=@status,remarks=@remarks,updatedby = @UpdatedBy,updatedon = @UpdatedOn,isactive = @IsActive,isdeleted = @IsDeleted WHERE id = @Id";
                var result = connection.Execute(query, pppMaster);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=PppMasterDB, Method=UpdatePppMaster");
                throw;
            }
        }
    }
}
