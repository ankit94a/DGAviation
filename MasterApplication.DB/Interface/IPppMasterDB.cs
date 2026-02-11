using MasterApplication.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterApplication.DB.Interface
{
    public interface IPppMasterDB
    {
        public List<PppMaster> GetAllPppMaster();
        public bool AddPppMaster(PppMaster pppMaster);
        public bool UpdatePppMaster(PppMaster pppMaster);
    }
}
