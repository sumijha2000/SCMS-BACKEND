using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class inventor_page
    {
        dbServices ds = new dbServices();
        public async Task<responseData> Inventor_page(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"INSERT INTO mydb.inventors_data(imageName,Image,Name,Description) values(@imageName,@Image,@Name,@Description)";
                MySqlParameter[] myParam = new MySqlParameter[]
                {

                new MySqlParameter("@imageName", rData.addInfo["imageName"]),
                 new MySqlParameter("@Image", rData.addInfo["Image"]),
                 new MySqlParameter("@Name",rData.addInfo["Name"]),
                new MySqlParameter("@Description", rData.addInfo["Description"]),

                };
                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > dbData[0].Count()-1)
                {
                    resData.rData["rMessage"] = "ADD SUCCESSFULLY!";
                }
                else
                {
                
                    resData.rData["rMessage"] = "Something went wrong on server...";

                }



            }
            catch (Exception ex)
            {

                resData.rData["rMessage"] = "Add Failed" + ex.Message;
            }
            return resData;
        }

        public async Task<responseData> EditInventor(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"UPDATE mydb.inventors_data SET imageName=@imageName, Image=@Image, Name=@Name, Description=@Description WHERE id=@id";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@imageName", rData.addInfo["imageName"]),
                    new MySqlParameter("@Image", rData.addInfo["Image"]),
                    new MySqlParameter("@Name", rData.addInfo["Name"]),
                    new MySqlParameter("@Description", rData.addInfo["Description"]),
                    new MySqlParameter("@id", rData.addInfo["id"])
                };
                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > dbData[0].Count() - 1)
                {
                    resData.rData["rMessage"] = "UPDATE SUCCESSFUL!";
                }
                else
                {
                    resData.rData["rMessage"] = "Update failed.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Update Failed: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData> DeleteInventor(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"DELETE FROM mydb.inventors_data WHERE id=@id";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@id", rData.addInfo["id"])
                };
                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > dbData[0].Count() - 1)
                {
                    resData.rData["rMessage"] = "DELETE SUCCESSFUL!";
                }
                else
                {
                    resData.rData["rMessage"] = "Delete failed.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Delete Failed: " + ex.Message;
            }
            return resData;
        }
    }
}