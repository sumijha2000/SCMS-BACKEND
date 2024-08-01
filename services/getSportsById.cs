using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MongoDB.Driver.Linq;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getSportsById
    {
        dbServices ds = new dbServices();
        public async Task<responseData> GetSportsById(requestData rData)
        {
            responseData resData = new responseData();


            try
            {
                var idsports_data = rData.addInfo["idsports_data"];

                // Check if the idsports_data exists in the database
                var idsports_dataExistsQuery = @"SELECT idsports_data FROM  mydb.sports_data WHERE idsports_data=@idsports_data";
                MySqlParameter[] idsports_dataExistsParam = new MySqlParameter[]
                {
                    new MySqlParameter("@idsports_data", idsports_data)
                };

                // Execute SQL query to get data
                var dbData = ds.executeSQL(idsports_dataExistsQuery, idsports_dataExistsParam);

                if (dbData.Any())
                {
                    // idsports_data exists, proceed to retrieve data
                    var query = @"SELECT * FROM  mydb.sports_data WHERE idsports_data=@idsports_data";
                    MySqlParameter[] myParam = new MySqlParameter[]
                    {
                        new MySqlParameter("@idsports_data", idsports_data)
                    };


                    // Execute SQL query to get data
                    var sportsData = ds.executeSQL(query, myParam).FirstOrDefault();
                    // Adding retrieved data to response

                    resData.rData["sportsData"] = sportsData;
                    resData.rData["idsports_data"] = idsports_data; // Adding idsports_data to response
                    resData.rData["rMessage"] = "Retrieved a Sports Data by idsports_data.";

                }
                else
                {
                    // idsports_data doesn't exist in the database
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