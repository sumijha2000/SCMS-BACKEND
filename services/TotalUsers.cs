using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class totalUsersService
    {
        private readonly dbServices ds = new dbServices();

        public async Task<responseData> GetTotalUsers(requestData rData)
        {
            var resData = new responseData();

            try
            {
                var query = @"
                    SELECT 
                        COUNT(*) AS totalCount 
                    FROM 
                        mydb.user_account 
                    WHERE 
                        role IN ('manager', 'supplier')";
                
                MySqlParameter[] parameters = new MySqlParameter[] { };

                var result = ds.ExecuteSQLName(query, parameters);

                if (result != null && result.Count > 0)
                {
                    var totalCount = 0;

                    foreach (var row in result[0])
                    {
                        if (row != null && row.Count == 1)
                        {
                            if (int.TryParse(row["totalCount"]?.ToString(), out totalCount))
                            {
                                resData.rData["totalCount"] = totalCount;
                            }
                            else
                            {
                                resData.rData["rMessage"] = "Count value is not a valid integer.";
                                return resData;
                            }
                        }
                        else
                        {
                            resData.rData["rMessage"] = "Unexpected number of columns in result set.";
                            return resData;
                        }
                    }
                }
                else
                {
                    resData.rData["rMessage"] = "No user accounts found.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }

            return resData;
        }
    }
}
