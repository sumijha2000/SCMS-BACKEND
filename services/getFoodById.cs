using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MongoDB.Driver.Linq;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getFoodById
    {
        dbServices ds = new dbServices();
        public async Task<responseData> GetFoodById(requestData rData)
        {
            responseData resData = new responseData();


            try
            {
                var idfood_data = rData.addInfo["idfood_data"];

                // Check if the idfood_data exists in the database
                var idfood_dataExistsQuery = @"SELECT idfood_data FROM  mydb.food_data WHERE idfood_data=@idfood_data";
                MySqlParameter[] idfood_dataExistsParam = new MySqlParameter[]
                {
                    new MySqlParameter("@idfood_data", idfood_data)
                };

                // Execute SQL query to get data
                var dbData = ds.executeSQL(idfood_dataExistsQuery, idfood_dataExistsParam);

                if (dbData.Any())
                {
                    // idfood_data exists, proceed to retrieve data
                    var query = @"SELECT * FROM  mydb.food_data WHERE idfood_data=@idfood_data";
                    MySqlParameter[] myParam = new MySqlParameter[]
                    {
                        new MySqlParameter("@idfood_data", idfood_data)
                    };


                    // Execute SQL query to get data
                    var foodData = ds.executeSQL(query, myParam).FirstOrDefault();
                    // Adding retrieved data to response

                    resData.rData["foodData"] = foodData;
                    resData.rData["idfood_data"] = idfood_data; // Adding idfood_data to response
                    resData.rData["rMessage"] = "Retrieved a Culture Data by idfood_data.";

                }
                else
                {
                    // idfood_data doesn't exist in the database
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