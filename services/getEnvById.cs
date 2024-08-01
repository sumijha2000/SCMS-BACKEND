using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MongoDB.Driver.Linq;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getEnvById
    {
        dbServices ds = new dbServices();
        public async Task<responseData> GetEnvById(requestData rData)
        {
            responseData resData = new responseData();


            try
            {
                var idenvironment_data = rData.addInfo["idenvironment_data"];

                // Check if the idenvironment_data exists in the database
                var idenvironment_dataExistsQuery = @"SELECT idenvironment_data FROM  mydb.environment_data WHERE idenvironment_data=@idenvironment_data";
                MySqlParameter[] idenvironment_dataExistsParam = new MySqlParameter[]
                {
                    new MySqlParameter("@idenvironment_data", idenvironment_data)
                };

                // Execute SQL query to get data
                var dbData = ds.executeSQL(idenvironment_dataExistsQuery, idenvironment_dataExistsParam);

                if (dbData.Any())
                {
                    // idenvironment_data exists, proceed to retrieve data
                    var query = @"SELECT * FROM  mydb.environment_data WHERE idenvironment_data=@idenvironment_data";
                    MySqlParameter[] myParam = new MySqlParameter[]
                    {
                        new MySqlParameter("@idenvironment_data", idenvironment_data)
                    };


                    // Execute SQL query to get data
                    var environmentData = ds.executeSQL(query, myParam).FirstOrDefault();
                    // Adding retrieved data to response

                    resData.rData["environmentData"] = environmentData;
                    resData.rData["idenvironment_data"] = idenvironment_data; // Adding idenvironment_data to response
                    resData.rData["rMessage"] = "Retrieved a Enviroment Data by idenvironment_data.";

                }
                else
                {
                    // idenvironment_data doesn't exist in the database
                    resData.rData["rMessage"] = "Not Found...";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Exception occurred: " + ex.Message;
            }


            return resData;
        }
    }
}