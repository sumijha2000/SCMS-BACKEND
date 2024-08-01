using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class supplierProfileService
    {
        dbServices ds = new dbServices();

        public async Task<responseData> CreateSupplierProfile(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"INSERT INTO mydb.supplier_profiles(name, email, phone, company_name, address, image) 
                              VALUES (@name, @email, @phone, @company_name, @address, @image)";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@name", rData.addInfo["name"]),
                    new MySqlParameter("@email", rData.addInfo["email"]),
                    new MySqlParameter("@phone", rData.addInfo["phone"]),
                    new MySqlParameter("@company_name", rData.addInfo["company_name"]),
                    new MySqlParameter("@address", rData.addInfo["address"]),
                   
                    new MySqlParameter("@image", rData.addInfo["image"])
                };

                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > dbData[0].Count() - 1)
                {
                    resData.rData["rMessage"] = "CREATE PROFILE SUCCESSFULLY!";
                }
                else
                {
                    resData.rData["rMessage"] = "Something went wrong on the server...";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Create Profile Failed: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData> UpdateSupplierProfile(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"
                    UPDATE mydb.supplier_profiles 
                    SET name = @name, phone = @phone, company_name = @company_name, address = @address, image = @image
                    WHERE email = @email;
                ";

                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@name", rData.addInfo["name"]),
                    new MySqlParameter("@email", rData.addInfo["email"]),
                    new MySqlParameter("@phone", rData.addInfo["phone"]),
                    new MySqlParameter("@company_name", rData.addInfo["company_name"]),
                    new MySqlParameter("@address", rData.addInfo["address"]),
                    new MySqlParameter("@image", rData.addInfo["image"])
                };

                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > dbData[0].Count() - 1)
                {
                    resData.rData["rMessage"] = "Profile updated successfully!";
                }
                else
                {
                    resData.rData["rMessage"] = "Update failed. Ensure the email exists.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Update Profile Failed: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData> DeleteSupplierProfile(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"
                    DELETE FROM mydb.supplier_profiles 
                    WHERE email = @email;
                ";

                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@email", rData.addInfo["email"])
                };

                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > dbData[0].Count() - 1)
                {
                    resData.rData["rMessage"] = "Profile deleted successfully!";
                }
                else
                {
                    resData.rData["rMessage"] = "Delete failed. Ensure the email exists.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Delete Profile Failed: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData> GetSupplierProfileData(requestData rData)
{
    responseData resData = new responseData();
    try
    {
        var query = @"SELECT name, email, phone, company_name, address, image FROM mydb.supplier_profiles WHERE email=@email";
        MySqlParameter[] myParam = new MySqlParameter[]
        {
            new MySqlParameter("@email", rData.addInfo["email"])
        };

        var dbData = ds.executeSQL(query, myParam);
        if (dbData[0].Count() > 0)
        {
            resData.rData["rMessage"] = dbData[0]; // Assuming this returns a list of records
        }
        else
        {
            resData.rData["rMessage"] = "No data found for the given email.";
        }
    }
    catch (Exception ex)
    {
        resData.rData["rMessage"] = "Fetch Failed: " + ex.Message;
    }
    return resData;
}

    }
}
