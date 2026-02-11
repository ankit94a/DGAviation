using MasterApplication.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterApplication.DB.Interface
{
    public interface IAttributeDB
    {
        public List<Category> GetAllCategory();
        public bool AddCategory(Category category);
        public bool UpdateCategory(Category category);
        public List<NatureOfProject> GetAllNatureOfProject();
        public bool AddNatureOfProject(NatureOfProject natureOfProject);
        public bool UpdateNatureOfProject(NatureOfProject natureOfProject);
        public List<ProjectStatus> GetAllProjectStauts();
        public bool AddProjectStauts(ProjectStatus status);
        public bool UpdateProjectStauts(ProjectStatus status);
        public bool DeleteDynamic(DeactivateModel data);
    }
}
