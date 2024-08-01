// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using MySql.Data.MySqlClient;

// namespace COMMON_PROJECT_STRUCTURE_API.services
// {
//     public class getsupplierorderdata
//     {
//         private readonly dbServices ds = new dbServices();

  

      
    


//         public async Task<responseData> GetSupplierTransactionData(requestData rData)
//         {
//             var resData = new responseData();

//            try
//             {
//                 var query = @"
//                     SELECT 
//                         product_name AS ProductName, 
//                         SUM(total_price) AS TotalAmount, 
//                         DATE(date_time) AS Date 
//                     FROM 
//                         mydb.orders 
//                     GROUP BY 
//                         product_name, DATE(date_time)";
                
//                 MySqlParameter[] parameters = new MySqlParameter[] { };

//                 var result = ds.executeSQL(query, parameters);

//                 var ordersByProduct = new Dictionary<string, int>();
//                 var ordersByDate = new Dictionary<DateTime, int>();

//                 if (result != null && result.Count > 0)
//                 {
//                     foreach (var row in result[0])  // Accessing the first result set
//                     {
//                         if (row != null && row.Length == 3)
//                         {
//                             var productName = row[0] as string;
//                             var amount = row[1] as decimal?;
//                             var date = row[2] as DateTime?;

//                             if (productName != null && amount.HasValue)
//                             {
//                                 if (!ordersByProduct.ContainsKey(productName))
//                                 {
//                                     ordersByProduct[productName] = 0;
//                                 }
//                                 ordersByProduct[productName] += (int)amount.Value;
//                             }

//                             if (date.HasValue)
//                             {
//                                 if (!ordersByDate.ContainsKey(date.Value))
//                                 {
//                                     ordersByDate[date.Value] = 0;
//                                 }
//                                 ordersByDate[date.Value] += (int)amount.Value;
//                             }
//                         }
//                         else
//                         {
//                             resData.rData["rMessage"] = "Unexpected number of columns in result set.";
//                             return resData;
//                         }
//                     }

//                     resData.rData["ordersByProduct"] = ordersByProduct;
//                     resData.rData["ordersByDate"] = ordersByDate;
//                 }
//                 else
//                 {
//                     resData.rData["rMessage"] = "No order data found.";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "An error occurred: " + ex.Message;
//             }

//             return resData;
//         }
//     }
// }


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getsupplierorderdata
    {
        private readonly dbServices ds = new dbServices();

        public async Task<responseData> GetSupplierTransactionData(requestData rData)
        {
            var resData = new responseData();

            try
            {
                var query = @"
                    SELECT 
                        product_name AS ProductName, 
                        SUM(total_price) AS TotalAmount, 
                        DATE(date_time) AS Date 
                    FROM 
                        mydb.orders 
                    WHERE 
                        tracking_status = 'Completed'
                    GROUP BY 
                        product_name, DATE(date_time)";
                
                MySqlParameter[] parameters = new MySqlParameter[] { };

                var result = ds.executeSQL(query, parameters);

                var ordersByProduct = new Dictionary<string, int>();
                var ordersByDate = new Dictionary<DateTime, int>();

                if (result != null && result.Count > 0)
                {
                    foreach (var row in result[0])  // Accessing the first result set
                    {
                        if (row != null && row.Length == 3)
                        {
                            var productName = row[0] as string;
                            var amount = row[1] as decimal?;
                            var date = row[2] as DateTime?;

                            if (productName != null && amount.HasValue)
                            {
                                if (!ordersByProduct.ContainsKey(productName))
                                {
                                    ordersByProduct[productName] = 0;
                                }
                                ordersByProduct[productName] += (int)amount.Value;
                            }

                            if (date.HasValue)
                            {
                                if (!ordersByDate.ContainsKey(date.Value))
                                {
                                    ordersByDate[date.Value] = 0;
                                }
                                ordersByDate[date.Value] += (int)amount.Value;
                            }
                        }
                        else
                        {
                            resData.rData["rMessage"] = "Unexpected number of columns in result set.";
                            return resData;
                        }
                    }

                    resData.rData["ordersByProduct"] = ordersByProduct;
                    resData.rData["ordersByDate"] = ordersByDate;
                }
                else
                {
                    resData.rData["rMessage"] = "No order data found.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }

            return resData;
        }
    }
}
