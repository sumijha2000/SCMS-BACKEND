using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class orderservice
    {
        private readonly dbServices ds = new dbServices();

        public async Task<responseData> CreateOrder(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                // Define the query to insert a new order
                var insertQuery = @"
                    INSERT INTO Orders (product_name, quantity, unit, unit_price, customer_name, location)
                    VALUES (@product_name, @quantity, @unit, @unit_price, @customer_name, @location);
                ";

                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@product_name", rData.addInfo["product_name"]),
                    new MySqlParameter("@quantity", rData.addInfo["quantity"]),
                    new MySqlParameter("@unit", rData.addInfo["unit"]),
                    new MySqlParameter("@unit_price", rData.addInfo["unit_price"]),
                    new MySqlParameter("@customer_name", rData.addInfo["customer_name"]),
                    new MySqlParameter("@location", rData.addInfo["location"])
                };

                // Execute the SQL command to insert the new order and get the last inserted ID
                int lastInsertedId = ds.ExecuteInsertAndGetLastId(insertQuery, myParam);

                // Define the query to retrieve the newly created order with the total_price
                var selectQuery = @"
                    SELECT id, product_name, quantity, unit, unit_price, total_price, customer_name, location
                    FROM Orders
                    WHERE id = @orderId;
                ";

                // Define the parameter for the select query
                MySqlParameter[] selectParam = new MySqlParameter[]
                {
                    new MySqlParameter("@orderId", lastInsertedId)
                };

                // Execute the query to retrieve the order details
                var orderData = ds.ExecuteSQLName(selectQuery, selectParam);
                if (orderData != null && orderData.Count > 0 && orderData[0].Length > 0)
                {
                    var order = orderData[0][0];
                    resData.rStatus = 1;
                    resData.rData["orderId"] = order["id"];
                    resData.rData["product_name"] = order["product_name"];
                    resData.rData["quantity"] = order["quantity"];
                    resData.rData["unit"] = order["unit"];
                    resData.rData["unit_price"] = order["unit_price"];
                    resData.rData["total_price"] = order["total_price"];
                    resData.rData["customer_name"] = order["customer_name"];
                    resData.rData["location"] = order["location"];
                    resData.rData["rMessage"] = "Order created successfully!";
                }
                else
                {
                    resData.rStatus = -1;
                    resData.rData["rMessage"] = "Failed to retrieve the order details.";
                }
            }
            catch (Exception ex)
            {
                resData.rStatus = -1;
                resData.rData["rMessage"] = "Order creation failed: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData> UpdateOrderStatus(requestData rData)
{
    responseData resData = new responseData();
    try
    {
        // Generate a unique shipment ID if the status is 'pickup'
        string shipmentId = null;
        if (rData.addInfo["status"].ToString() == "pickup")
        {
            shipmentId = GenerateShipmentId();
        }

        // SQL query to update order status and shipment_id if status is 'pickup'
        var updateQuery = @"
            UPDATE Orders
            SET status = @status, 
                shipment_id = CASE WHEN @status = 'pickup' THEN @shipment_id ELSE NULL END
            WHERE id = @id;
        ";

        // Prepare SQL parameters
        MySqlParameter[] myParam = new MySqlParameter[]
        {
            new MySqlParameter("@status", rData.addInfo["status"]),
            new MySqlParameter("@shipment_id", (object)shipmentId ?? DBNull.Value), // Handle null shipment ID
            new MySqlParameter("@id", rData.addInfo["id"])
        };

        // Execute SQL
        var dbData = ds.executeSQL(updateQuery, myParam);
        
        // Check if any rows were affected
       if (dbData[0].Count() > dbData[0].Count() - 1)
        {
            resData.rStatus = 1;
            resData.rData["rMessage"] = "Order status updated successfully!";
            resData.rData["shipment_id"] = shipmentId;
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



 public async Task<responseData> UpdateOrderStatusByShipmentId(requestData rData)
{
    responseData resData = new responseData();
    try
    {
        // SQL query to update order status and date based on shipment_id
        var updateQuery = @"
            UPDATE Orders
            SET 
                tracking_status = @tracking_status,
                date = @date
            WHERE shipment_id = @shipment_id;
        ";

        // Prepare SQL parameters
        MySqlParameter[] myParam = new MySqlParameter[]
        {
            new MySqlParameter("@tracking_status", rData.addInfo["tracking_status"]),
            new MySqlParameter("@shipment_id", rData.addInfo["shipment_id"]),
            new MySqlParameter("@date", rData.addInfo["date"])
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

        public async Task<responseData> DeleteOrders(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"
                    DELETE FROM mydb.orders 
                    WHERE id = @id;
                ";

                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@id", rData.addInfo["id"])
                };

                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > dbData[0].Count() - 1)
                {
                    resData.rData["rMessage"] = "Orders deleted successfully!";
                }
                else
                {
                    resData.rData["rMessage"] = "Delete failed. Ensure the email exists.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Delete Orders Failed: " + ex.Message;
            }
            return resData;
        }


    }
}
