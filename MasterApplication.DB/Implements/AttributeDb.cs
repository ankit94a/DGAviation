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
    public class AttributeDb : BaseDB, IAttributeDB
    {
        public AttributeDb(IConfiguration configuration) : base(configuration)
        {

        }

        public List<Category> GetAllCategory()
        {
            try
            {
                string query = string.Format("select * from pppcategory where isactive = 1 order by id desc");
                return connection.Query<Category>(query).ToList();
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=AttributeDb,method=GetAllCategory");
                throw;
            }
        }
        public bool AddCategory(Category category)
        {
            try
            {
                string query = string.Format(@"insert into pppcategory (name,createdby,createdon,isactive,isDeleted) values (@name,@createdby,@createdon,@isactive,@isDeleted)");
                var result = connection.Execute(query, category);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=AttributeDb,method=AddCategory");
                throw;
            }
        }

        public bool UpdateCategory(Category category)
        {
            try
            {
                if (category.UpdatedOn == default)
                    category.UpdatedOn = DateTime.Now;

                string query = @"UPDATE pppcategory SET name = @Name,updatedby = @UpdatedBy,updatedon = @UpdatedOn,isactive = @IsActive,isdeleted = @IsDeleted WHERE id = @Id";
                var result = connection.Execute(query, category);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=AttributeDb, Method=UpdateCategory");
                throw;
            }
        }


        public List<NatureOfProject> GetAllNatureOfProject()
        {
            try
            {
                string query = string.Format("select * from natureofproject where isactive = 1 order by id desc");
                return connection.Query<NatureOfProject>(query).ToList();
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=AttributeDb,method=GetAllNatureOfProject");
                throw;
            }
        }
        public bool AddNatureOfProject(NatureOfProject natureOfProject)
        {
            try
            {
                string query = string.Format(@"insert into natureofproject (name,createdby,createdon,isactive,isDeleted) values (@name,@createdby,@createdon,@isactive,@isDeleted)");
                var result = connection.Execute(query, natureOfProject);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=AttributeDb,method=AddNatureOfProject");
                throw;
            }
        }
        public bool UpdateNatureOfProject(NatureOfProject natureOfProject)
        {
            try
            {

                string query = @"UPDATE natureofproject SET name = @Name,updatedby = @UpdatedBy,updatedon = @UpdatedOn,isactive = @IsActive,isdeleted = @IsDeleted WHERE id = @Id";
                var result = connection.Execute(query, natureOfProject);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=AttributeDb, Method=UpdateNatureOfProject");
                throw;
            }
        }
        public List<ProjectStatus> GetAllProjectStauts()
        {
            try
            {
                string query = string.Format("select * from projectstatus where isactive = 1 order by id desc");
                return connection.Query<ProjectStatus>(query).ToList();
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=AttributeDb,method=GetAllProjectStauts");
                throw;
            }
        }
        public bool AddProjectStauts(ProjectStatus status)
        {
            try
            {
                string query = string.Format(@"insert into projectstatus (name,createdby,createdon,isactive,isDeleted) values (@name,@createdby,@createdon,@isactive,@isDeleted)");
                var result = connection.Execute(query, status);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=AttributeDb,method=AddProjectStauts");
                throw;
            }
        }
        public bool UpdateProjectStauts(ProjectStatus status)
        {
            try
            {
                string query = @"UPDATE projectstatus SET name = @Name,updatedby = @UpdatedBy,updatedon = @UpdatedOn,isactive = @IsActive,isdeleted = @IsDeleted WHERE id = @Id";
                var result = connection.Execute(query, status);
                return result > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=AttributeDb, Method=UpdateProjectStauts");
                throw;
            }
        }
        public bool DeleteDynamic(DeactivateModel data)
        {
            try
            {
                string query = $"UPDATE {data.TableName} SET isactive = 0 WHERE id = @id";
                return connection.Execute(query, new { id = data.Id }) > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, $"Class=AttributeDB,Delete from ,table = {data.TableName}");
                throw;
            }
        }
    }
}
