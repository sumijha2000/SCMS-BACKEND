// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MySql.Data.MySqlClient;

// namespace COMMON_PROJECT_STRUCTURE_API.services
// {
//     public class blog_page
//     {
//         dbServices ds = new dbServices();
//         public async Task<responseData> Blog_page(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var query = @"INSERT INTO mydb.blog_data(imageName,image, head, blog_det) values(@imageName,@image, @head, @blog_det)";
//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {

//                   new MySqlParameter("@imageName", rData.addInfo["imageName"]), 
//                  new MySqlParameter("@image", rData.addInfo["image"]),
//                  new MySqlParameter("@head",rData.addInfo["head"]),
//                      new MySqlParameter("@blog_det", rData.addInfo["blog_det"]),

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



// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MySql.Data.MySqlClient;

// namespace COMMON_PROJECT_STRUCTURE_API.services
// {
//     public class blog_page
//     {
//         dbServices ds = new dbServices();
        
//         public async Task<responseData> Blog_page(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var query = @"INSERT INTO mydb.blog_data(imageName, image, head, blog_det) VALUES (@imageName, @image, @head, @blog_det)";
//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {
//                     new MySqlParameter("@imageName", rData.addInfo["imageName"]),
//                     new MySqlParameter("@image", rData.addInfo["image"]),
//                     new MySqlParameter("@head", rData.addInfo["head"]),
//                     new MySqlParameter("@blog_det", rData.addInfo["blog_det"]),
//                 };
//                 var dbData = ds.executeSQL(query, myParam);
//                 if (dbData[0].Count() > dbData[0].Count() - 1)
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
//                 resData.rData["rMessage"] = "Add Failed: " + ex.Message;
//             }
//             return resData;
//         }

//         public async Task<responseData> EditBlog(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var query = @"UPDATE mydb.blog_data SET imageName=@imageName, image=@image, head=@head, blog_det=@blog_det WHERE id=@id";
//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {
//                     new MySqlParameter("@imageName", rData.addInfo["imageName"]),
//                     new MySqlParameter("@image", rData.addInfo["image"]),
//                     new MySqlParameter("@head", rData.addInfo["head"]),
//                     new MySqlParameter("@blog_det", rData.addInfo["blog_det"]),
//                     new MySqlParameter("@id", rData.addInfo["id"]),
//                 };
//                 var dbData = ds.executeSQL(query, myParam);
//                 if (dbData[0].Count() > dbData[0].Count() - 1)
//                 {
//                     resData.rData["rMessage"] = "UPDATE SUCCESSFUL!";
//                 }
//                 else
//                 {
//                     resData.rData["rMessage"] = "Update failed.";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "Update Failed: " + ex.Message;
//             }
//             return resData;
//         }

//         public async Task<responseData> DeleteBlog(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var query = @"DELETE FROM mydb.blog_data WHERE id=@id";
//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {
//                     new MySqlParameter("@id", rData.addInfo["id"]),
//                 };
//                 var dbData = ds.executeSQL(query, myParam);
//                 if (dbData[0].Count() > dbData[0].Count() - 1)
//                 {
//                     resData.rData["rMessage"] = "DELETE SUCCESSFUL!";
//                 }
//                 else
//                 {
//                     resData.rData["rMessage"] = "Delete failed.";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "Delete Failed: " + ex.Message;
//             }
//             return resData;
//         }
//     }
// }


//final 
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MySql.Data.MySqlClient;

// namespace COMMON_PROJECT_STRUCTURE_API.services
// {
//     public class blog_page
//     {
//         dbServices ds = new dbServices();

//         public async Task<responseData> Blog_page(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var query = @"INSERT INTO mydb.blog_data(imageName, image, head, blog_det) VALUES (@imageName, @image, @head, @blog_det)";
//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {
//                     new MySqlParameter("@imageName", rData.addInfo["imageName"]),
//                     new MySqlParameter("@image", rData.addInfo["image"]),
//                     new MySqlParameter("@head", rData.addInfo["head"]),
//                     new MySqlParameter("@blog_det", rData.addInfo["blog_det"]),
//                 };
//                 var dbData = ds.executeSQL(query, myParam);
//                 if (dbData[0].Count() > dbData[0].Count() - 1)
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
//                 resData.rData["rMessage"] = "Add Failed: " + ex.Message;
//             }
//             return resData;
//         }

//         public async Task<responseData> EditBlog(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var query = @"UPDATE mydb.blog_data SET imageName=@imageName, image=@image, head=@head, blog_det=@blog_det WHERE id=@id";
//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {
//                     new MySqlParameter("@imageName", rData.addInfo["imageName"]),
//                     new MySqlParameter("@image", rData.addInfo["image"]),
//                     new MySqlParameter("@head", rData.addInfo["head"]),
//                     new MySqlParameter("@blog_det", rData.addInfo["blog_det"]),
//                     new MySqlParameter("@id", rData.addInfo["id"]),
//                 };
//                 var dbData = ds.executeSQL(query, myParam);
//                 if (dbData[0].Count() > dbData[0].Count() - 1)
//                 {
//                     resData.rData["rMessage"] = "UPDATE SUCCESSFUL!";
//                 }
//                 else
//                 {
//                     resData.rData["rMessage"] = "Update failed.";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "Update Failed: " + ex.Message;
//             }
//             return resData;
//         }

//         public async Task<responseData> DeleteBlog(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var query = @"DELETE FROM mydb.blog_data WHERE id=@id";
//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {
//                     new MySqlParameter("@id", rData.addInfo["id"]),
//                 };
//                 var dbData = ds.executeSQL(query, myParam);
//                 if (dbData[0].Count() > dbData[0].Count() - 1)
//                 {
//                     resData.rData["rMessage"] = "DELETE SUCCESSFUL!";
//                 }
//                 else
//                 {
//                     resData.rData["rMessage"] = "Delete failed.";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "Delete Failed: " + ex.Message;
//             }
//             return resData;
//         }

//        public async Task<responseData> ArchiveBlog(requestData rData)
// {
//     responseData resData = new responseData();
//     try
//     {
//         var query = @"UPDATE mydb.blog_data SET archived=1 WHERE id=@id";
//         MySqlParameter[] myParam = new MySqlParameter[]
//         {
//             new MySqlParameter("@id", rData.addInfo["id"]),
//         };
//         var dbData = ds.executeSQL(query, myParam); // Call the executeSQL method with the parameters
//         if (dbData[0].Count() > 0) // Check if the update was successful
//         {
//             resData.rData["rMessage"] = "Blog archived successfully!";
//         }
//         else
//         {
//             resData.rData["rMessage"] = "Blog archiving failed.";
//         }
//     }
//     catch (Exception ex)
//     {
//         resData.rData["rMessage"] = "Archive Failed: " + ex.Message;
//     }
//     return resData;
// }
// public async Task<responseData> GetBlogInfo(requestData rData)
// {
//     responseData resData = new responseData();
//     try
//     {
//         var query = @"SELECT id, imageName, image, head, blog_det FROM mydb.blog_data WHERE archived=0";
//         var dbData = ds.executeSQL(query, null); // Call the executeSQL method with no parameters
//         if (dbData[0].Count() > 0)
//         {
//             resData.rData["rMessage"] = dbData[0]; // Set the fetched data to the response
//         }
//         else
//         {
//             resData.rData["rMessage"] = "No blogs available.";
//         }
//     }
//     catch (Exception ex)
//     {
//         resData.rData["rMessage"] = "Fetch Failed: " + ex.Message;
//     }
//     return resData;
// }


// public async Task<responseData> GetArchivedBlogs(requestData rData)
// {
//     responseData resData = new responseData();
//     try
//     {
//         var query = @"SELECT id, imageName, image, head, blog_det FROM mydb.blog_data WHERE archived=1";
//         var dbData = ds.executeSQL(query, null); // Call the executeSQL method with no parameters
//         if (dbData[0].Count() > 0)
//         {
//             resData.rData["rMessage"] = dbData[0]; // Set the fetched data to the response
//         }
//         else
//         {
//             resData.rData["rMessage"] = "No archived blogs available.";
//         }
//     }
//     catch (Exception ex)
//     {
//         resData.rData["rMessage"] = "Fetch Failed: " + ex.Message;
//     }
//     return resData;
// }

//     }
// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class blog_page
    {
        dbServices ds = new dbServices();

        public async Task<responseData> Blog_page(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"INSERT INTO mydb.blog_data(imageName, image, head, blog_det) VALUES (@imageName, @image, @head, @blog_det)";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@imageName", rData.addInfo["imageName"]),
                    new MySqlParameter("@image", rData.addInfo["image"]),
                    new MySqlParameter("@head", rData.addInfo["head"]),
                    new MySqlParameter("@blog_det", rData.addInfo["blog_det"]),
                };
                var dbData = ds.executeSQL(query, myParam);
               if (dbData[0].Count() > dbData[0].Count() - 1) // Check if the insertion was successful
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

        public async Task<responseData> EditBlog(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"UPDATE mydb.blog_data SET imageName=@imageName, image=@image, head=@head, blog_det=@blog_det WHERE id=@id";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@imageName", rData.addInfo["imageName"]),
                    new MySqlParameter("@image", rData.addInfo["image"]),
                    new MySqlParameter("@head", rData.addInfo["head"]),
                    new MySqlParameter("@blog_det", rData.addInfo["blog_det"]),
                    new MySqlParameter("@id", rData.addInfo["id"]),
                };
                var dbData = ds.executeSQL(query, myParam);
               if (dbData[0].Count() > dbData[0].Count() - 1) // Check if the update was successful
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

        public async Task<responseData> DeleteBlog(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"DELETE FROM mydb.blog_data WHERE id=@id";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@id", rData.addInfo["id"]),
                };
                var dbData = ds.executeSQL(query, myParam);
               if (dbData[0].Count() > dbData[0].Count() - 1) // Check if the deletion was successful
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

        public async Task<responseData> ArchiveBlog(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"UPDATE mydb.blog_data SET archived=1 WHERE id=@id";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@id", rData.addInfo["id"]),
                };
                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > dbData[0].Count() - 1) // Check if the update was successful
                {
                    resData.rData["rMessage"] = "Blog archived successfully!";
                }
                else
                {
                    resData.rData["rMessage"] = "Blog archiving failed.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Archive Failed: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData> GetBlogInfo(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT id, imageName, image, head, blog_det FROM mydb.blog_data WHERE archived=0";
                var dbData = ds.executeSQL(query, null);  // Call the executeSQL method with no parameters
                if (dbData[0].Count() > 0)  // Check if there are any blogs to return
                {
                    resData.rData["rMessage"] = dbData[0];  // Set the fetched data to the response
                }
                else
                {
                    resData.rData["rMessage"] = "No blogs available.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Fetch Failed: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData> GetArchivedBlogs(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT id, imageName, image, head, blog_det FROM mydb.blog_data WHERE archived=1";
                var dbData = ds.executeSQL(query, null);  // Call the executeSQL method with no parameters
                if (dbData[0].Count() > dbData[0].Count() - 1) // Check if there are any archived blogs to return
                {
                    resData.rData["rMessage"] = dbData[0];  // Set the fetched data to the response
                }
                else
                {
                    resData.rData["rMessage"] = "No archived blogs available.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Fetch Failed: " + ex.Message;
            }
            return resData;
        }

          public async Task<responseData> LikeBlog(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"UPDATE mydb.blog_data SET likes = likes + 1 WHERE id = @id";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@id", rData.addInfo["id"]),
                };
                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > dbData[0].Count() - 1) // Check if the update was successful
                {
                    resData.rData["rMessage"] = "Blog liked successfully!";
                }
                else
                {
                    resData.rData["rMessage"] = "Blog like failed.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Like Failed: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData> DislikeBlog(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"UPDATE mydb.blog_data SET dislikes = dislikes + 1 WHERE id = @id";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@id", rData.addInfo["id"]),
                };
                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > dbData[0].Count() - 1) // Check if the update was successful
                {
                    resData.rData["rMessage"] = "Blog disliked successfully!";
                }
                else
                {
                    resData.rData["rMessage"] = "Blog dislike failed.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Dislike Failed: " + ex.Message;
            }
            return resData;
        }

        
    }
}
