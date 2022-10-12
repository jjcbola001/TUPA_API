using Dapper;
using Data.Model;
using Data.Model.Common;
using SharedInterfaces;
using SharedInterfaces.Ilogging;
using SharedInterfaces.IService;
using SharedModels;
using SharedUtilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TitleService : ITitle
    {
        private readonly ILogging _Logging;

        private readonly string _ConnString;
        public TitleService(ILogging logging)
        {

            _Logging = logging;
            _ConnString = ConfigurationUtility.GetConnectionStrings().PrimaryDatabaseConnectionString;

        }

        public async Task<ResponseModel<TitleList>> PostTitle(TitleList data)
        {
            var result = new ResponseModel<TitleList>();

            try
            {
                using (var con = new SqlConnection(_ConnString))
                {
                    var param = new DynamicParameters();
                  
                    foreach (var i in data.Titles)
                    {
                        param.Add("@RoD_ID", i.RoD_ID);
                        param.Add("@RoD_Name", i.RoD_Name);
                        param.Add("@Title_Number", i.Title_Number);
                        param.Add("@Title_Type", i.Title_Type);
                        param.Add("@NumberOfCoOwner", i.NumberOfCoOwner);
                        param.Add("@Owner_Name", i.Owner_Name);
                        param.Add("@Location", i.Location);

                        var response = await con.QuerySingleOrDefaultAsync<TitleList>("sp_SaveTitle", param, commandType: System.Data.CommandType.StoredProcedure);
                    }
                    
                    result.ReturnStatus = true;
                    result.ReturnMessage = "success";
                }
            }
            catch (Exception ex)
            {
                _Logging.LogError(nameof(PostTitle), ex);
            }
            finally
            {

            }
    
            return result;
        }
    }
}
