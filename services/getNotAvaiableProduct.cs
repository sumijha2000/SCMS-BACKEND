

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class getNotAvaiableProduct
    {
        dbServices ds = new dbServices();

        public async Task<responseData> GetNotAvaiableProduct(requestData rData)
        {
            responseData resData = new responseData();

            try
            {
                // Select only public uploader data
                var selectQuery = @"SELECT * FROM mydb.orders WHERE status = 'not available'";
                MySqlParameter[] selectParams = new MySqlParameter[]
                {
                    // If you have any parameters to pass, you can define them here
                };
                var selectResult = ds.executeSQL(selectQuery, selectParams);

                if (selectResult != null && selectResult.Count > 0)
                {
                    List<List<object>> allclass = new List<List<object>>();
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
                    resData.rData["rMessage"] = "No product records found";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
                // Log the exception as needed
            }

            return resData;
        }

     public async Task<responseData> UpdateOrderStatusBySupplier(requestData rData)
{
    responseData resData = new responseData();
    try
    {
        // Generate a unique shipment ID if the status is 'done'
        string shipmentIdBy = null;
        if (rData.addInfo["supplier_status"].ToString() == "done")
        {
            shipmentIdBy = GenerateShipmentId();
        }

        // SQL query to update order status and shipment_idby if status is 'done'
        var updateQuery = @"
            UPDATE Orders
            SET supplier_status = @supplier_status, 
                shipment_idby = CASE WHEN @supplier_status = 'done' THEN @shipment_idby ELSE shipment_idby END
            WHERE id = @id;
        ";

        // Prepare SQL parameters
        MySqlParameter[] myParam = new MySqlParameter[]
        {
            new MySqlParameter("@supplier_status", rData.addInfo["supplier_status"]),
            new MySqlParameter("@shipment_idby", (object)shipmentIdBy ?? DBNull.Value), // Handle null shipment ID
            new MySqlParameter("@id", rData.addInfo["id"])
        };

        // Execute SQL
        var dbData = ds.executeSQL(updateQuery, myParam);

        // Check if any rows were affected
        if (dbData[0].Count() > dbData[0].Count() - 1)
        {
            resData.rStatus = 1;
            resData.rData["rMessage"] = "Order status updated successfully!";
            resData.rData["shipment_idby"] = shipmentIdBy;
        }
        else
        {
            resData.rStatus = -1;
            resData.rData["rMessage"] = "Failed to update order status.";
        }
    }
    catch (Exception ex)
    {
        resData.rStatus = -1;
        resData.rData["rMessage"] = "Failed to update order status: " + ex.Message;
    }
    return resData;
}

// Method to generate a unique shipment ID
private string GenerateShipmentId()
{
    return "SHIP-" + Guid.NewGuid().ToString();
}


public async Task<responseData> UpdateOrderBySupplierStatusByShipmentId(requestData rData)
{
    responseData resData = new responseData();
    try
    {
        // SQL query to update order status based on shipment_id
        var updateQuery = @"
            UPDATE Orders
            SET 
                tracking_statusby = @tracking_statusby,
                date_time = @date_time
            WHERE shipment_idby = @shipment_idby;
        ";

        // Prepare SQL parameters
        MySqlParameter[] myParam = new MySqlParameter[]
        {
            new MySqlParameter("@tracking_statusby", rData.addInfo["tracking_statusby"]),
            new MySqlParameter("@date_time", rData.addInfo["date_time"]),
            new MySqlParameter("@shipment_idby", rData.addInfo["shipment_idby"])
        };

        // Execute SQL
        var dbData = ds.executeSQL(updateQuery, myParam);
        
        // Check if any rows were affected
        if (dbData[0].Count() > dbData[0].Count() - 1)
        {
            resData.rStatus = 1;
            resData.rData["rMessage"] = "Order status updated successfully!";
        }
        else
        {
            resData.rStatus = -1;
            resData.rData["rMessage"] = "Failed to update order status.";
        }
    }
    catch (Exception ex)
    {
        resData.rStatus = -1;
        resData.rData["rMessage"] = "Failed to update order status: " + ex.Message;
    }
    return resData;
}

        


         public async Task<responseData> ConfirmedDeliveries(requestData rData)
        {
            responseData resData = new responseData();

            try
            {
                // Select only public uploader data
                var selectQuery = @"SELECT * FROM mydb.orders WHERE tracking_statusby = 'completed'";
                MySqlParameter[] selectParams = new MySqlParameter[]
                {
                    // If you have any parameters to pass, you can define them here
                };
                var selectResult = ds.executeSQL(selectQuery, selectParams);

                if (selectResult != null && selectResult.Count > 0)
                {
                    List<List<object>> allclass = new List<List<object>>();
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
                    resData.rData["rMessage"] = "No product records found";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
                // Log the exception as needed
            }

            return resData;
        }

    public async Task<responseData> TransactionHistory(requestData rData)
        {
            responseData resData = new responseData();

            try
            {
                // Select only public uploader data
                var selectQuery = @"SELECT * FROM mydb.orders WHERE tracking_status = 'completed'";
                MySqlParameter[] selectParams = new MySqlParameter[]
                {
                    // If you have any parameters to pass, you can define them here
                };
                var selectResult = ds.executeSQL(selectQuery, selectParams);

                if (selectResult != null && selectResult.Count > 0)
                {
                    List<List<object>> allclass = new List<List<object>>();
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
                    resData.rData["rMessage"] = "No product records found";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
                // Log the exception as needed
            }

            return resData;
        }
    public async Task<responseData> TransactionHistorySupplier(requestData rData)
        {
            responseData resData = new responseData();

            try
            {
                // Select only public uploader data
                var selectQuery = @"SELECT * FROM mydb.orders WHERE tracking_statusby = 'completed'";
                MySqlParameter[] selectParams = new MySqlParameter[]
                {
                    // If you have any parameters to pass, you can define them here
                };
                var selectResult = ds.executeSQL(selectQuery, selectParams);

                if (selectResult != null && selectResult.Count > 0)
                {
                    List<List<object>> allclass = new List<List<object>>();
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
                    resData.rData["rMessage"] = "No product records found";
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




