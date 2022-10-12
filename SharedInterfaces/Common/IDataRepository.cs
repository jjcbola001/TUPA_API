using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedInterfaces.Common
{
	public interface IDataRepository
	{

		void CommitTransaction();
		Task UpdateDatabase();
		void BeginTransaction(int isolationLevel);
		void BeginTransaction();
		void RollbackTransaction();
		Object OpenConnection();
		void CloseConnection();
		void OpenConnection(Object dbConnection);
		void OpenConnection(string connectionString);

		DateTime GetServerDate();

	}
}
