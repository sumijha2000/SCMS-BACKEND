// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MySql.Data.MySqlClient;

// namespace COMMON_PROJECT_STRUCTURE_API.services
// {
//     public class communication
//     {
//         dbServices ds = new dbServices();
//         public async Task<responseData> Communication(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var query = @"INSERT INTO mydb.communication_data(categories, description, video, video_name) values(@categories,@description,@video, @video_name)";
//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {



                
//                    new MySqlParameter("@categories",rData.addInfo["categories"]),
//                      new MySqlParameter("@description", rData.addInfo["description"]),
//                      new MySqlParameter("@video", rData.addInfo["video"]),
//                      new MySqlParameter("@video_name", rData.addInfo["video_name"]),

//                 };
//                 var dbData = ds.executeSQL(query, myParam);
//                 if (dbData[0].Count() > dbData[0].Count()-1)
//                 {
//                     resData.rData["rMessage"] = "ADD SUCCESSFULLY!";
//                 }
//                 else
//                 {
                
//                     resData.rData["rMessage"] = "Something went wrong on server...";

//                 }



//             }
//             catch (Exception ex)
//             {

//                 resData.rData["rMessage"] = "Add Failed" + ex.Message;
//             }
//             return resData;
//         }
//     }
// }


using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class communication
    {
        dbServices ds = new dbServices();

        public async Task<responseData> Communication(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"INSERT INTO mydb.communication_data(categories, description, video, video_name) VALUES (@categories, @description, @video, @video_name)";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@categories", rData.addInfo["categories"]),
                    new MySqlParameter("@description", rData.addInfo["description"]),
                    new MySqlParameter("@video", rData.addInfo["video"]),
                    new MySqlParameter("@video_name", rData.addInfo["video_name"]),
                };
                var dbData = ds.executeSQL(query, myParam);
                 if (dbData[0].Count() > dbData[0].Count() - 1)
                {
                    resData.rData["rMessage"] = "ADD SUCCESSFUL!";
                }
                else
                {
                    resData.rData["rMessage"] = "Something went wrong on server...";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Add Failed: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData> EditCommunication(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"UPDATE mydb.communication_data SET categories=@categories, description=@description, video=@video, video_name=@video_name WHERE idcommunication_data=@idcommunication_data";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@categories", rData.addInfo["categories"]),
                    new MySqlParameter("@description", rData.addInfo["description"]),
                    new MySqlParameter("@video", rData.addInfo["video"]),
                    new MySqlParameter("@video_name", rData.addInfo["video_name"]),
                    new MySqlParameter("@idcommunication_data", rData.addInfo["idcommunication_data"]),
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

        public async Task<responseData> DeleteCommunication(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"DELETE FROM mydb.communication_data WHERE idcommunication_data=@idcommunication_data";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@idcommunication_data", rData.addInfo["idcommunication_data"]),
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
