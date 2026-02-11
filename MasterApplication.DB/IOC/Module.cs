using MasterApplication.DB.Implements;
using MasterApplication.DB.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterApplication.DB.IOC
{
	public static class Module
	{
		public static Dictionary<Type, Type> GetTypes()
		{
			var dic = new Dictionary<Type, Type>
			{
				{typeof(IAttributeDB), typeof(AttributeDb) },
				{typeof(IPppMasterDB), typeof(PppMasterDB) },
                {typeof(IUserDB), typeof(UserDB) },
				//{typeof(IAttributeDB), typeof(AttributeDB) },
				//{typeof(IEmerDB), typeof(EmerDB) },
				//{typeof(IPolicyDB), typeof(PolicyDB) },
				//{typeof(IFileDB), typeof(FileDB) },
				//{typeof(ItechnicalAoAiDB), typeof(TechnicalAoAiDB) },
				//{typeof(IRoleOfMagDb), typeof(RoleOfMagDb) }

			};
			return dic;
		}
	}
}
