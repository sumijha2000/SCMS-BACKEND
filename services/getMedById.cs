using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MongoDB.Driver.Linq;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getMedById
    {
        dbServices ds = new dbServices();
        public async Task<responseData> GetMedById(requestData rData)
        {
            responseData resData = new responseData();


            try
            {
                var idmedicine_data = rData.addInfo["idmedicine_data"];

                // Check if the idmedicine_data exists in the database
                var idmedicine_dataExistsQuery = @"SELECT idmedicine_data FROM  mydb.medicine_data WHERE idmedicine_data=@idmedicine_data";
                MySqlParameter[] idmedicine_dataExistsParam = new MySqlParameter[]
                {
                    new MySqlParameter("@idmedicine_data", idmedicine_data)
                };

                // Execute SQL query to get data
                var dbData = ds.executeSQL(idmedicine_dataExistsQuery, idmedicine_dataExistsParam);

                if (dbData.Any())
                {
                    // idmedicine_data exists, proceed to retrieve data
                    var query = @"SELECT * FROM  mydb.medicine_data WHERE idmedicine_data=@idmedicine_data";
                    MySqlParameter[] myParam = new MySqlParameter[]
                    {
                        new MySqlParameter("@idmedicine_data", idmedicine_data)
                    };


                    // Execute SQL query to get data
                    var medData = ds.executeSQL(query, myParam).FirstOrDefault();
                    // Adding retrieved data to response

                    resData.rData["medData"] = medData;
                    resData.rData["idmedicine_data"] = idmedicine_data; // Adding idmedicine_data to response
                    resData.rData["rMessage"] = "Retrieved a Medical Data by idmedicine_data.";

                }
                else
                {
                    // idmedicine_data doesn't exist in the database
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