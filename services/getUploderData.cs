// using System;
// using System.Data.Common;
// using System.Threading.Tasks;
// using MySql.Data.MySqlClient;

// namespace COMMON_PROJECT_STRUCTURE_API.services
// {
//     public class getUploderData
//     {
//         dbServices ds = new dbServices();

//         public async Task<responseData> GetUploderData(requestData rData)
//         {
//             responseData resData = new responseData();

//             try
//             {
//                 // Insert new subscription
//                 var insertQuery = @"select * from mydb.uploader";
//                 MySqlParameter[] insertParams = new MySqlParameter[]
//                 {
                     
//                 };
//                 var insertResult = ds.executeSQL(insertQuery, insertParams);

//                 List<List<object>> allclass = new List<List<object>>();
//                 if(insertResult != null & insertResult.Count>0){
//                      foreach(var row in insertResult){
//                          List<object> rowData = new List<object>();
//                          foreach (var item in row)
//                          {
//                             rowData.Add(item);
//                          }
//                          allclass.Add(rowData);
//                     }
//                 resData.rData["rMessage"] = allclass;
//                 }

//                 else{
//                     resData.rData["rMessage"]="no classes";
//                 }    
            
//             }
//                catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "An error occurred: " + ex.Message;
//                 // Log the exception as needed
//             }

//             return resData;
//         }
//     }
// }

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getUploderData
    {
        dbServices ds = new dbServices();

        public async Task<responseData> GetUploderData(requestData rData)
        {
            responseData resData = new responseData();

            try
            {
                // Select only public uploader data
                var selectQuery = @"SELECT * FROM mydb.uploader WHERE VISIBILITY = 'public'";
                MySqlParameter[] selectParams = new MySqlParameter[]
                {
                    // If you have any parameters to pass, you can define them here
                };
                var selectResult = ds.executeSQL(selectQuery, selectParams);

                List<List<object>> allclass = new List<List<object>>();
                if (selectResult != null && selectResult.Count > 0)
                {
                    foreach (var row in selectResult)
                    {
                        List<object> rowData = new List<object>();
                        foreach (var item in row)
                        {
                            rowData.Add(item);
                        }
                        allclass.Add(rowData);
                    }
                    resData.rData["rMessage"] = allclass;
                }
                else
                {
                    resData.rData["rMessage"] = "No public records found";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
                // Log the exception as needed
            }

            return resData;
        }
    }
}
