using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MongoDB.Driver.Linq;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getCommInfoById
    {
        dbServices ds = new dbServices();
        public async Task<responseData> GetCommInfoById(requestData rData)
        {
            responseData resData = new responseData();


            try
            {
                var id = rData.addInfo["id"];

                // Check if the id exists in the database
                var idExistsQuery = @"SELECT id FROM mydb.communication_data WHERE id=@id";
                MySqlParameter[] idExistsParam = new MySqlParameter[]
                {
                    new MySqlParameter("@id", id)
                };

                // Execute SQL query to get data
                var dbData = ds.executeSQL(idExistsQuery, idExistsParam);

                if (dbData.Any())
                {
                    // id exists, proceed to retrieve data
                    var query = @"SELECT * FROM mydb.communication_data WHERE id=@id";
                    MySqlParameter[] myParam = new MySqlParameter[]
                    {
                        new MySqlParameter("@id", id)
                    };


                    // Execute SQL query to get data
                    var commData = ds.executeSQL(query, myParam).FirstOrDefault();
                    // Adding retrieved data to response

                    resData.rData["commData"] = commData;
                    resData.rData["id"] = id; // Adding id to response
                    resData.rData["rMessage"] = "Retrieved a Communication Data by id.";

                }
                else
                {
                    // id doesn't exist in the database
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