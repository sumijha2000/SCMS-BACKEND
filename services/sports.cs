using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class sports
    {
        dbServices ds = new dbServices();
        public async Task<responseData> Sports(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                  var query = @"INSERT INTO mydb.sports_data(categories, description, video, video_name) values(@categories, @description, @video, @video_name)";
                MySqlParameter[] myParam = new MySqlParameter[]
                {



                
                     new MySqlParameter("@categories", rData.addInfo["categories"]),
                     new MySqlParameter("@description", rData.addInfo["description"]),
                    new MySqlParameter("@video", rData.addInfo["video"]),
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

         public async Task<responseData> EditSports(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"UPDATE mydb.sports_data SET categories=@categories, description=@description, video=@video, video_name=@video_name WHERE idsports_data=@id";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@categories", rData.addInfo["categories"]),
                    new MySqlParameter("@description", rData.addInfo["description"]),
                    new MySqlParameter("@video", rData.addInfo["video"]),
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

        public async Task<responseData> DeleteSports(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"DELETE FROM mydb.sports_data WHERE idsports_data=@id";
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