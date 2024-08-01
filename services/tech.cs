using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class tech
    {
        dbServices ds = new dbServices();
        public async Task<responseData> Tech(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                  var query = @"INSERT INTO  mydb.tech_data(categories, description, videos, video_name) values(@categories, @description, @videos, @video_name)";
                MySqlParameter[] myParam = new MySqlParameter[]
                {



               
                 
                     new MySqlParameter("@categories", rData.addInfo["categories"]),
                     new MySqlParameter("@description", rData.addInfo["description"]),
                    new MySqlParameter("@videos", rData.addInfo["videos"]),
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

          public async Task<responseData> EditTech(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"UPDATE mydb.tech_data SET categories=@categories, description=@description, videos=@videos, video_name=@video_name WHERE idtech_tab=@id";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@categories", rData.addInfo["categories"]),
                    new MySqlParameter("@description", rData.addInfo["description"]),
                    new MySqlParameter("@videos", rData.addInfo["videos"]),
                    new MySqlParameter("@video_name", rData.addInfo["video_name"]),
                    new MySqlParameter("@id", rData.addInfo["id"]),
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

        public async Task<responseData> DeleteTech(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"DELETE FROM mydb.tech_data WHERE idtech_tab=@id";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@id", rData.addInfo["id"]),
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