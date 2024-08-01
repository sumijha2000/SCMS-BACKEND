using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MongoDB.Driver.Linq;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getFashionById
    {
        dbServices ds = new dbServices();
        public async Task<responseData> GetFashionById(requestData rData)
        {
            responseData resData = new responseData();


            try
            {
                var idfashion_data = rData.addInfo["idfashion_data"];

                // Check if the idfashion_data exists in the database
                var idfashion_dataExistsQuery = @"SELECT idfashion_data FROM  mydb.fashion_data WHERE idfashion_data=@idfashion_data";
                MySqlParameter[] idfashion_dataExistsParam = new MySqlParameter[]
                {
                    new MySqlParameter("@idfashion_data", idfashion_data)
                };

                // Execute SQL query to get data
                var dbData = ds.executeSQL(idfashion_dataExistsQuery, idfashion_dataExistsParam);

                if (dbData.Any())
                {
                    // idfashion_data exists, proceed to retrieve data
                    var query = @"SELECT * FROM  mydb.fashion_data WHERE idfashion_data=@idfashion_data";
                    MySqlParameter[] myParam = new MySqlParameter[]
                    {
                        new MySqlParameter("@idfashion_data", idfashion_data)
                    };


                    // Execute SQL query to get data
                    var fashionData = ds.executeSQL(query, myParam).FirstOrDefault();
                    // Adding retrieved data to response

                    resData.rData["fashionData"] = fashionData;
                    resData.rData["idfashion_data"] = idfashion_data; // Adding idfashion_data to response
                    resData.rData["rMessage"] = "Retrieved a Culture Data by idfashion_data.";

                }
                else
                {
                    // idfashion_data doesn't exist in the database
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