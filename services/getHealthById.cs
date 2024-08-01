using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MongoDB.Driver.Linq;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getHealthById
    {
        dbServices ds = new dbServices();
        public async Task<responseData> GetHealthById(requestData rData)
        {
            responseData resData = new responseData();


            try
            {
                var idhealth_data = rData.addInfo["idhealth_data"];

                // Check if the idhealth_data exists in the database
                var idhealth_dataExistsQuery = @"SELECT idhealth_data FROM  mydb.health_data WHERE idhealth_data=@idhealth_data";
                MySqlParameter[] idhealth_dataExistsParam = new MySqlParameter[]
                {
                    new MySqlParameter("@idhealth_data", idhealth_data)
                };

                // Execute SQL query to get data
                var dbData = ds.executeSQL(idhealth_dataExistsQuery, idhealth_dataExistsParam);

                if (dbData.Any())
                {
                    // idhealth_data exists, proceed to retrieve data
                    var query = @"SELECT * FROM  mydb.health_data WHERE idhealth_data=@idhealth_data";
                    MySqlParameter[] myParam = new MySqlParameter[]
                    {
                        new MySqlParameter("@idhealth_data", idhealth_data)
                    };


                    // Execute SQL query to get data
                    var healthData = ds.executeSQL(query, myParam).FirstOrDefault();
                    // Adding retrieved data to response

                    resData.rData["healthData"] = healthData;
                    resData.rData["idhealth_data"] = idhealth_data; // Adding idhealth_data to response
                    resData.rData["rMessage"] = "Retrieved a Culture Data by idhealth_data.";

                }
                else
                {
                    // idhealth_data doesn't exist in the database
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