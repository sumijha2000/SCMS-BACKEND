using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class inventions_page
    {
        dbServices ds = new dbServices();
        public async Task<responseData> Inventions_page(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"INSERT INTO mydb.inventions_data(Categories,Description,Videos, video_name) values(@Categories,@Description,@Videos, @video_name)";
                MySqlParameter[] myParam = new MySqlParameter[]
                {


                
                   new MySqlParameter("@Categories",rData.addInfo["Categories"]),
                     new MySqlParameter("@Description", rData.addInfo["Description"]),
                     new MySqlParameter("@Videos", rData.addInfo["Videos"]),
                     new MySqlParameter("@video_name", rData.addInfo["video_name"]),
                     

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

          
        public async Task<responseData> EditInvention(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"UPDATE mydb.inventions_data SET Categories=@Categories, Description=@Description, Videos=@Videos, video_name=@video_name WHERE id=@id";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@Categories", rData.addInfo["Categories"]),
                    new MySqlParameter("@Description", rData.addInfo["Description"]),
                    new MySqlParameter("@Videos", rData.addInfo["Videos"]),
                    new MySqlParameter("@video_name", rData.addInfo["video_name"]),
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

        public async Task<responseData> DeleteInvention(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"DELETE FROM mydb.inventions_data WHERE id=@id";
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