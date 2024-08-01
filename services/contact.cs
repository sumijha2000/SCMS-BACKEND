using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class contact
    {
        dbServices ds = new dbServices();

        public async Task<responseData> Contact(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var email = rData.addInfo["EMAIL"];

                var query = @"SELECT * FROM mydb.contact WHERE EMAIL=@EMAIL";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@EMAIL", email)
                };
                var dbData = ds.executeSQL(query, myParam);

                if (dbData[0].Count() == 0)
                {
                    // Contact doesn't exist, insert a new record
                    var insertQuery = @"INSERT INTO mydb.contact (FIRST_NAME, LAST_NAME, EMAIL, SUBJECT) VALUES (@FIRST_NAME, @LAST_NAME, @EMAIL, @SUBJECT)";
                    MySqlParameter[] insertParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@FIRST_NAME", rData.addInfo["FIRST_NAME"]),
                        new MySqlParameter("@LAST_NAME", rData.addInfo["LAST_NAME"]),
                        new MySqlParameter("@EMAIL", email),
                        new MySqlParameter("@SUBJECT", rData.addInfo["SUBJECT"])
                    };
                    var insertResult = ds.executeSQL(insertQuery, insertParams);
                    resData.rData["rMessage"] = "New contact added";
                }
                else
                {
                    // Contact exists, update the existing record
                    var updateQuery = @"UPDATE mydb.contact SET FIRST_NAME=@FIRST_NAME, LAST_NAME=@LAST_NAME, SUBJECT=@SUBJECT WHERE EMAIL=@EMAIL";
                    MySqlParameter[] updateParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@FIRST_NAME", rData.addInfo["FIRST_NAME"]),
                        new MySqlParameter("@LAST_NAME", rData.addInfo["LAST_NAME"]),
                        new MySqlParameter("@EMAIL", email),
                        new MySqlParameter("@SUBJECT", rData.addInfo["SUBJECT"])
                    };
                    var updateResult = ds.executeSQL(updateQuery, updateParams);
                    resData.rData["rMessage"] = "Contact details updated";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                resData.rData["rMessage"] = "Error: " + ex.Message;
            }
            return resData;
        }
        

          public async Task<responseData> DeleteContact(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                // Ensure rData and rData.addInfo are not null before accessing
                if (rData != null && rData.addInfo != null)
                {
                    var contactid = rData.addInfo["id"];

                    var deleteQuery = @"DELETE FROM mydb.contact WHERE id=@id";
                    MySqlParameter[] deleteParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@id", contactid)
                    };

                    var deleteResult = ds.executeSQL(deleteQuery, deleteParams);

                    if (deleteResult != null && deleteResult.Count > 0) // Assuming ds.executeSQL returns a list of results
                    {
                        resData.rData["rMessage"] = "Contact deleted successfully";
                    }
                    else
                    {
                        resData.rData["rMessage"] = "Contact not found";
                    }
                }
                else
                {
                    resData.rData["rMessage"] = "Error: Invalid request data or missing 'id'";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                resData.rData["rMessage"] = "Error: " + ex.Message;
            }
            return resData;
        }

    }
    }