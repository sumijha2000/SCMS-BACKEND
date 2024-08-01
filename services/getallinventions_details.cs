// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MySql.Data.MySqlClient;

// namespace COMMON_PROJECT_STRUCTURE_API.services
// {
//     public class getallinventions_details
//     {
//         dbServices ds = new dbServices();
//         public async Task<responseData> Getallinventions_details(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var query = @"SELECT * FROM mydb.inventions_data";
//                 MySqlParameter[] myParam = new MySqlParameter[] { };

//                 var dbData = ds.executeSQL(query, myParam); 
//                 if (dbData.Any())
//                 {
//                     List<string> messages = new List<string>();

//                     foreach (var rowSet in dbData)
//                     {
//                         foreach (var row in rowSet)
//                         {
//                             List<string> rowData = new List<string>();

//                             foreach (var column in row)
//                             {
//                                 rowData.Add(column.ToString());
//                             }

//                             // Now rowData contains all values from the current row
//                             string rowDataString = string.Join(" - ", rowData);
//                             messages.Add(rowDataString);
//                         }
//                     }

                    
//                     string allinventions = string.Join(Environment.NewLine, messages);

//                     resData.rData["rMessage"] = $"Retrieved All Inventions:\n{allinventions}";
//                 }
//                 else
//                 {
//                     resData.rData["rMessage"] = "No any details here...";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "Exception occurred: " + ex.Message;
//             }
            
            
//             return resData;
//         }
//     }
// }




using System;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getallinventions_details
    {
        dbServices ds = new dbServices();

        public async Task<responseData> Getallinventions_details(requestData rData)
        {
            responseData resData = new responseData();

            try
            {
                // Insert new subscription
                var insertQuery = @"select * from mydb.inventions_data";
                MySqlParameter[] insertParams = new MySqlParameter[]
                {
                     
                };
                var insertResult = ds.executeSQL(insertQuery, insertParams);

                List<List<object>> allclass = new List<List<object>>();
                if(insertResult != null & insertResult.Count>0){
                     foreach(var row in insertResult){
                         List<object> rowData = new List<object>();
                         foreach (var item in row)
                         {
                            rowData.Add(item);
                         }
                         allclass.Add(rowData);
                    }
                resData.rData["rMessage"] = allclass;
                }

                else{
                    resData.rData["rMessage"]="no classes";
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

