using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MongoDB.Driver.Linq;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getCultureInfoById
    {
        dbServices ds = new dbServices();
        public async Task<responseData> GetCultureInfoById(requestData rData)
        {
            responseData resData = new responseData();


            try
            {
                var idculture_data = rData.addInfo["idculture_data"];

                // Check if the idculture_data exists in the database
                var idculture_dataExistsQuery = @"SELECT idculture_data FROM  mydb.culture_data WHERE idculture_data=@idculture_data";
                MySqlParameter[] idculture_dataExistsParam = new MySqlParameter[]
                {
                    new MySqlParameter("@idculture_data", idculture_data)
                };

                // Execute SQL query to get data
                var dbData = ds.executeSQL(idculture_dataExistsQuery, idculture_dataExistsParam);

                if (dbData.Any())
                {
                    // idculture_data exists, proceed to retrieve data
                    var query = @"SELECT * FROM  mydb.culture_data WHERE idculture_data=@idculture_data";
                    MySqlParameter[] myParam = new MySqlParameter[]
                    {
                        new MySqlParameter("@idculture_data", idculture_data)
                    };


                    // Execute SQL query to get data
                    var cultureData = ds.executeSQL(query, myParam).FirstOrDefault();
                    // Adding retrieved data to response

                    resData.rData["cultureData"] = cultureData;
                    resData.rData["idculture_data"] = idculture_data; // Adding idculture_data to response
                    resData.rData["rMessage"] = "Retrieved a Culture Data by idculture_data.";

                }
                else
                {
                    // idculture_data doesn't exist in the database
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