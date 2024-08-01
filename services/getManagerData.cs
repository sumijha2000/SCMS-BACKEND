

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getManagerData
    {
        dbServices ds = new dbServices();

        public async Task<responseData> GetManagerData(requestData rData)
        {
            responseData resData = new responseData();

            try
            {
                // Select only public uploader data
                var selectQuery = @"SELECT * FROM mydb.user_account WHERE role = 'manager'";
                MySqlParameter[] selectParams = new MySqlParameter[]
                {
                    // If you have any parameters to pass, you can define them here
                };
                var selectResult = ds.executeSQL(selectQuery, selectParams);

                if (selectResult != null && selectResult.Count > 0)
                {
                    List<List<object>> allclass = new List<List<object>>();
                    foreach (var row in selectResult)
                    {
                        List<object> rowData = new List<object>();
                        foreach (var item in row)
                        {
                            rowData.Add(item);
                        }
                        allclass.Add(rowData);
                    }
                    resData.rData["rMessage"] = allclass;
                }
                else
                {
                    resData.rData["rMessage"] = "No manager records found";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
                // Log the exception as needed
            }

            return resData;
        }
    }
}




