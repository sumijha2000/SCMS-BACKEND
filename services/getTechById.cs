using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MongoDB.Driver.Linq;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getTechById
    {
        dbServices ds = new dbServices();
        public async Task<responseData> GetTechById(requestData rData)
        {
            responseData resData = new responseData();


            try
            {
                var idtech_tab = rData.addInfo["idtech_tab"];

                // Check if the idtech_tab exists in the database
                var idtech_tabExistsQuery = @"SELECT idtech_tab FROM  mydb.tech_data WHERE idtech_tab=@idtech_tab";
                MySqlParameter[] idtech_tabExistsParam = new MySqlParameter[]
                {
                    new MySqlParameter("@idtech_tab", idtech_tab)
                };

                // Execute SQL query to get data
                var dbData = ds.executeSQL(idtech_tabExistsQuery, idtech_tabExistsParam);

                if (dbData.Any())
                {
                    // idtech_tab exists, proceed to retrieve data
                    var query = @"SELECT * FROM  mydb.tech_data WHERE idtech_tab=@idtech_tab";
                    MySqlParameter[] myParam = new MySqlParameter[]
                    {
                        new MySqlParameter("@idtech_tab", idtech_tab)
                    };


                    // Execute SQL query to get data
                    var techData = ds.executeSQL(query, myParam).FirstOrDefault();
                    // Adding retrieved data to response

                    resData.rData["techData"] = techData;
                    resData.rData["idtech_tab"] = idtech_tab; // Adding idtech_tab to response
                    resData.rData["rMessage"] = "Retrieved a Culture Data by idtech_tab.";

                }
                else
                {
                    // idtech_tab doesn't exist in the database
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