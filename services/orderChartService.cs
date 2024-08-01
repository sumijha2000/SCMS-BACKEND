using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class orderChartService
    {
        private readonly dbServices ds = new dbServices();

        public async Task<responseData> GetOrderStatusCounts(requestData rData)
        {
            var resData = new responseData();

            try
            {
                var query = @"
                    SELECT 
                        tracking_status, 
                        COUNT(*) AS count 
                    FROM 
                        mydb.orders 
                    WHERE 
                        tracking_status IN ('Completed', 'Ongoing')
                    GROUP BY 
                        tracking_status";
                
                MySqlParameter[] parameters = new MySqlParameter[] { };

                var result = ds.ExecuteSQLName(query, parameters);

                var statusCounts = new Dictionary<string, int>();

                if (result != null && result.Count > 0)
                {
                    foreach (var row in result[0])
                    {
                        if (row != null && row.Count == 2)
                        {
                            var status = row["tracking_status"] as string;
                            if (status == null)
                            {
                                resData.rData["rMessage"] = "Status value is null.";
                                return resData;
                            }

                            if (int.TryParse(row["count"]?.ToString(), out int count))
                            {
                                statusCounts[status] = count;
                            }
                            else
                            {
                                resData.rData["rMessage"] = "Count value is not a valid integer.";
                                return resData;
                            }
                        }
                        else
                        {
                            resData.rData["rMessage"] = "Unexpected number of columns in result set.";
                            return resData;
                        }
                    }

                    resData.rData["statusCounts"] = statusCounts;
                }
                else
                {
                    resData.rData["rMessage"] = "No orders found.";
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
