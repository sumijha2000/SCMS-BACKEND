using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MongoDB.Driver.Linq;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getPoliticsById
    {
        dbServices ds = new dbServices();
        public async Task<responseData> GetPoliticsById(requestData rData)
        {
            responseData resData = new responseData();


            try
            {
                var idpolitics_data = rData.addInfo["idpolitics_data"];

                // Check if the idpolitics_data exists in the database
                var idpolitics_dataExistsQuery = @"SELECT idpolitics_data FROM  mydb.politics_data WHERE idpolitics_data=@idpolitics_data";
                MySqlParameter[] idpolitics_dataExistsParam = new MySqlParameter[]
                {
                    new MySqlParameter("@idpolitics_data", idpolitics_data)
                };

                // Execute SQL query to get data
                var dbData = ds.executeSQL(idpolitics_dataExistsQuery, idpolitics_dataExistsParam);

                if (dbData.Any())
                {
                    // idpolitics_data exists, proceed to retrieve data
                    var query = @"SELECT * FROM  mydb.politics_data WHERE idpolitics_data=@idpolitics_data";
                    MySqlParameter[] myParam = new MySqlParameter[]
                    {
                        new MySqlParameter("@idpolitics_data", idpolitics_data)
                    };


                    // Execute SQL query to get data
                    var politicData = ds.executeSQL(query, myParam).FirstOrDefault();
                    // Adding retrieved data to response

                    resData.rData["politicData"] = politicData;
                    resData.rData["idpolitics_data"] = idpolitics_data; // Adding idpolitics_data to response
                    resData.rData["rMessage"] = "Retrieved a Politics Data by idpolitics_data.";

                }
                else
                {
                    // idpolitics_data doesn't exist in the database
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