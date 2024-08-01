using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MongoDB.Driver.Linq;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getScienceById
    {
        dbServices ds = new dbServices();
        public async Task<responseData> GetScienceById(requestData rData)
        {
            responseData resData = new responseData();


            try
            {
                var idscience_data = rData.addInfo["idscience_data"];

                // Check if the idscience_data exists in the database
                var idscience_dataExistsQuery = @"SELECT idscience_data FROM  mydb.science_data WHERE idscience_data=@idscience_data";
                MySqlParameter[] idscience_dataExistsParam = new MySqlParameter[]
                {
                    new MySqlParameter("@idscience_data", idscience_data)
                };

                // Execute SQL query to get data
                var dbData = ds.executeSQL(idscience_dataExistsQuery, idscience_dataExistsParam);

                if (dbData.Any())
                {
                    // idscience_data exists, proceed to retrieve data
                    var query = @"SELECT * FROM  mydb.science_data WHERE idscience_data=@idscience_data";
                    MySqlParameter[] myParam = new MySqlParameter[]
                    {
                        new MySqlParameter("@idscience_data", idscience_data)
                    };


                    // Execute SQL query to get data
                    var scienceData = ds.executeSQL(query, myParam).FirstOrDefault();
                    // Adding retrieved data to response

                    resData.rData["scienceData"] = scienceData;
                    resData.rData["idscience_data"] = idscience_data; // Adding idscience_data to response
                    resData.rData["rMessage"] = "Retrieved a Science Data by idscience_data.";

                }
                else
                {
                    // idscience_data doesn't exist in the database
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