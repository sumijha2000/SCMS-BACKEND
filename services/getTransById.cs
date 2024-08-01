using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MongoDB.Driver.Linq;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getTransById
    {
        dbServices ds = new dbServices();
        public async Task<responseData> GetTransById(requestData rData)
        {
            responseData resData = new responseData();


            try
            {
                var idtransportation_data = rData.addInfo["idtransportation_data"];

                // Check if the idtransportation_data exists in the database
                var idtransportation_dataExistsQuery = @"SELECT idtransportation_data FROM  mydb.transportation_data WHERE idtransportation_data=@idtransportation_data";
                MySqlParameter[] idtransportation_dataExistsParam = new MySqlParameter[]
                {
                    new MySqlParameter("@idtransportation_data", idtransportation_data)
                };

                // Execute SQL query to get data
                var dbData = ds.executeSQL(idtransportation_dataExistsQuery, idtransportation_dataExistsParam);

                if (dbData.Any())
                {
                    // idtransportation_data exists, proceed to retrieve data
                    var query = @"SELECT * FROM mydb.transportation_data WHERE idtransportation_data=@idtransportation_data";
                    MySqlParameter[] myParam = new MySqlParameter[]
                    {
                        new MySqlParameter("@idtransportation_data", idtransportation_data)
                    };


                    // Execute SQL query to get data
                    var transData = ds.executeSQL(query, myParam).FirstOrDefault();
                    // Adding retrieved data to response

                    resData.rData["transData"] = transData;
                    resData.rData["idtransportation_data"] = idtransportation_data; // Adding idtransportation_data to response
                    resData.rData["rMessage"] = "Retrieved a Transporations Data by idtransportation_data.";

                }
                else
                {
                    // idtransportation_data doesn't exist in the database
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