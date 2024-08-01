using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getUserRolData
    {
        private readonly dbServices ds = new dbServices();

       

        public async Task<responseData> GetUserRoleCounts(requestData rData)
        {
            var resData = new responseData();

            try
            {
                var query = @"
                    SELECT 
                        role, 
                        COUNT(*) AS count 
                    FROM 
                        mydb.user_account 
                    GROUP BY 
                        role";
                
                MySqlParameter[] parameters = new MySqlParameter[] { };

                var result = ds.executeSQL(query, parameters);

                var roleCounts = new Dictionary<string, int>();

                if (result != null && result.Count > 0)
                {
                    foreach (var row in result[0])  // Accessing the first result set
                    {
                        if (row != null && row.Length == 2)
                        {
                            var role = row[0] as string;
                            if (role == null)
                            {
                                resData.rData["rMessage"] = "Role value is null.";
                                return resData;
                            }

                            if (int.TryParse(row[1]?.ToString(), out int count))
                            {
                                roleCounts[role] = count;
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

                    resData.rData["roleCounts"] = roleCounts;
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
