using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class productService
    {
        dbServices ds = new dbServices();

        // Create a new product
        // public async Task<responseData> CreateProduct(requestData rData)
        // {
        //     responseData resData = new responseData();
        //     try
        //     {
        //         var query = @"INSERT INTO mydb.products(product_name, product_description, price) 
        //                       VALUES (@product_name, @product_description, @price)";
        //         MySqlParameter[] myParam = new MySqlParameter[]
        //         {
        //             new MySqlParameter("@product_name", rData.addInfo["product_name"]),
        //             new MySqlParameter("@product_description", rData.addInfo["product_description"]),
        //             new MySqlParameter("@price", rData.addInfo["price"])
        //         };

        //         var dbData = ds.executeSQL(query, myParam);
        //         if (dbData[0].Count() > dbData[0].Count() - 1)
        //         {
        //             resData.rData["rMessage"] = "Product created successfully!";
        //         }
        //         else
        //         {
        //             resData.rData["rMessage"] = "Something went wrong on the server...";
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         resData.rData["rMessage"] = "Create Product Failed: " + ex.Message;
        //     }
        //     return resData;
        // }


public async Task<responseData> CreateProduct(requestData rData)
{
    responseData resData = new responseData();
    try
    {
        // Parse the price as a decimal
        if (!decimal.TryParse(rData.addInfo["price"].ToString(), out decimal price))
        {
            resData.rData["rMessage"] = "Invalid price format.";
            return resData;
        }

        // SQL query including currency
        var query = @"INSERT INTO mydb.products (product_name, product_description, price, currency) 
                      VALUES (@product_name, @product_description, @price, @currency)";

        // Prepare parameters including currency
        MySqlParameter[] myParam = new MySqlParameter[]
        {
            new MySqlParameter("@product_name", rData.addInfo["product_name"]),
            new MySqlParameter("@product_description", rData.addInfo["product_description"]),
            new MySqlParameter("@price", price),
            new MySqlParameter("@currency", rData.addInfo.ContainsKey("currency") ? rData.addInfo["currency"] : "USD")  // Default to USD if not provided
        };

        var dbData = ds.executeSQL(query, myParam);
        if (dbData[0].Count() > dbData[0].Count() - 1)
        {
            resData.rData["rMessage"] = "Product created successfully!";
        }
        else
        {
            resData.rData["rMessage"] = "Something went wrong on the server...";
        }
    }
    catch (Exception ex)
    {
        resData.rData["rMessage"] = "Create Product Failed: " + ex.Message;
    }
    return resData;
}

        // Update an existing product
        // public async Task<responseData> UpdateProduct(requestData rData)
        // {
        //     responseData resData = new responseData();
        //     try
        //     {
        //         var query = @"
        //             UPDATE mydb.products 
        //             SET product_name = @product_name, product_description = @product_description, price = @price
        //             WHERE id = @id;
        //         ";

        //         MySqlParameter[] myParam = new MySqlParameter[]
        //         {
        //             new MySqlParameter("@product_name", rData.addInfo["product_name"]),
        //             new MySqlParameter("@product_description", rData.addInfo["product_description"]),
        //             new MySqlParameter("@price", rData.addInfo["price"]),
        //             new MySqlParameter("@id", rData.addInfo["id"])
        //         };

        //         var dbData = ds.executeSQL(query, myParam);
        //         if (dbData[0].Count() > dbData[0].Count() - 1)
        //         {
        //             resData.rData["rMessage"] = "Product updated successfully!";
        //         }
        //         else
        //         {
        //             resData.rData["rMessage"] = "Update failed. Ensure the product ID exists.";
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         resData.rData["rMessage"] = "Update Product Failed: " + ex.Message;
        //     }
        //     return resData;
        // }

        public async Task<responseData> UpdateProduct(requestData rData)
{
    responseData resData = new responseData();
    try
    {
        // Parse the price as a decimal
        if (!decimal.TryParse(rData.addInfo["price"].ToString(), out decimal price))
        {
            resData.rData["rMessage"] = "Invalid price format.";
            return resData;
        }

        // Check if currency is provided, otherwise default to "USD"
        string currency = rData.addInfo.ContainsKey("currency") ? rData.addInfo["currency"].ToString() : "USD";

        var query = @"
            UPDATE mydb.products 
            SET product_name = @product_name, 
                product_description = @product_description, 
                price = @price,
                currency = @currency
            WHERE id = @id;
        ";

        MySqlParameter[] myParam = new MySqlParameter[]
        {
            new MySqlParameter("@product_name", rData.addInfo["product_name"]),
            new MySqlParameter("@product_description", rData.addInfo["product_description"]),
            new MySqlParameter("@price", price),
            new MySqlParameter("@currency", currency),
            new MySqlParameter("@id", rData.addInfo["id"])
        };

        var dbData = ds.executeSQL(query, myParam);

        // Check if the update operation affected any rows
       if (dbData[0].Count() > dbData[0].Count() - 1)
        {
            resData.rData["rMessage"] = "Product updated successfully!";
        }
        else
        {
            resData.rData["rMessage"] = "Update failed. Ensure the product ID exists.";
        }
    }
    catch (Exception ex)
    {
        resData.rData["rMessage"] = "Update Product Failed: " + ex.Message;
    }
    return resData;
}


        // Delete a product
        public async Task<responseData> DeleteProduct(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"
                    DELETE FROM mydb.products 
                    WHERE id = @id;
                ";

                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@id", rData.addInfo["id"])
                };

                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > dbData[0].Count() - 1)
                {
                    resData.rData["rMessage"] = "Product deleted successfully!";
                }
                else
                {
                    resData.rData["rMessage"] = "Delete failed. Ensure the product ID exists.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Delete Product Failed: " + ex.Message;
            }
            return resData;
        }

        // Get product details
        public async Task<responseData> GetProductDetails(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT product_name, product_description, price FROM mydb.products WHERE id=@id";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@id", rData.addInfo["id"])
                };

                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > 0)
                {
                    resData.rData["rMessage"] = dbData[0]; // Assuming this returns a list of records
                }
                else
                {
                    resData.rData["rMessage"] = "No data found for the given product ID.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Fetch Failed: " + ex.Message;
            }
            return resData;
        }


          public async Task<responseData> GetProductList(requestData rData)
        {
            responseData resData = new responseData();

             try
            {
                // Insert new subscription
                var insertQuery = @"select * from mydb.products";
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
