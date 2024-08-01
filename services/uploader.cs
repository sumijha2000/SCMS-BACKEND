// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using MySql.Data.MySqlClient;

// namespace COMMON_PROJECT_STRUCTURE_API.services
// {
//     public class uploader
//     {
//         dbServices ds = new dbServices();

//         public async Task<responseData> Uploader(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var query = @"SELECT * FROM mydb.uploader WHERE CONTACT=@CONTACT";
//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {
//                     new MySqlParameter("@CONTACT", rData.addInfo["CONTACT"].ToString()),
//                 };
//                 var dbData = ds.executeSQL(query, myParam);
//                 if (dbData[0].Count == 0)
//                 {
//                     // Contact doesn't exist, insert a new record
//                     // Decode base64-encoded image and file data
//                     string imageBase64 = rData.addInfo["IMAGE"].ToString();
//                     string fileBase64 = rData.addInfo["FILE"].ToString();

//                     byte[] imageBytes;
//                     byte[] fileBytes;
//                     try
//                     {
//                         imageBytes = Convert.FromBase64String(imageBase64);
//                         fileBytes = Convert.FromBase64String(fileBase64);
//                     }
//                     catch (FormatException ex)
//                     {
//                         throw new Exception("Invalid base64 string: " + ex.Message);
//                     }

//                     var insertQuery = @"INSERT INTO mydb.uploader (INVENTION, INVENTOR, CONTACT, INVENTION_DEATAILS, IMAGE, FILE, VISIBILITY) 
//                                         VALUES (@INVENTION, @INVENTOR, @CONTACT, @INVENTION_DEATAILS, @IMAGE, @FILE, @VISIBILITY)";
//                     MySqlParameter[] insertParams = new MySqlParameter[]
//                     {
//                         new MySqlParameter("@INVENTION", rData.addInfo["INVENTION"].ToString()),
//                         new MySqlParameter("@INVENTOR", rData.addInfo["INVENTOR"].ToString()),
//                         new MySqlParameter("@CONTACT", rData.addInfo["CONTACT"].ToString()),
//                         new MySqlParameter("@INVENTION_DEATAILS", rData.addInfo["INVENTION_DEATAILS"].ToString()),
//                         new MySqlParameter("@IMAGE", imageBytes),
//                         new MySqlParameter("@FILE", fileBytes),
//                         new MySqlParameter("@VISIBILITY", rData.addInfo["VISIBILITY"].ToString())
//                     };
//                     var insertResult = ds.executeSQL(insertQuery, insertParams);

//                     resData.rData["rMessage"] = "Uploaded Successfully";
//                 }
//                 else
//                 {
//                     // Contact exists, update the existing record
//                     var updateQuery = @"UPDATE mydb.uploader 
//                                         SET INVENTION=@INVENTION, INVENTOR=@INVENTOR, INVENTION_DEATAILS=@INVENTION_DEATAILS, 
//                                             IMAGE=@IMAGE, FILE=@FILE, VISIBILITY=@VISIBILITY 
//                                         WHERE CONTACT=@CONTACT";
//                     // Decode base64-encoded image and file data
//                     string imageBase64 = rData.addInfo["IMAGE"].ToString();
//                     string fileBase64 = rData.addInfo["FILE"].ToString();

//                     byte[] imageBytes;
//                     byte[] fileBytes;
//                     try
//                     {
//                         imageBytes = Convert.FromBase64String(imageBase64);
//                         fileBytes = Convert.FromBase64String(fileBase64);
//                     }
//                     catch (FormatException ex)
//                     {
//                         throw new Exception("Invalid base64 string: " + ex.Message);
//                     }

//                     MySqlParameter[] updateParams = new MySqlParameter[]
//                     {
//                         new MySqlParameter("@INVENTION", rData.addInfo["INVENTION"].ToString()),
//                         new MySqlParameter("@INVENTOR", rData.addInfo["INVENTOR"].ToString()),
//                         new MySqlParameter("@INVENTION_DEATAILS", rData.addInfo["INVENTION_DEATAILS"].ToString()),
//                         new MySqlParameter("@IMAGE", imageBytes),
//                         new MySqlParameter("@FILE", fileBytes),
//                         new MySqlParameter("@VISIBILITY", rData.addInfo["VISIBILITY"].ToString()),
//                         new MySqlParameter("@CONTACT", rData.addInfo["CONTACT"].ToString())
//                     };
//                     var updateResult = ds.executeSQL(updateQuery, updateParams);

//                     resData.rData["rMessage"] = "updated";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "Upload Failed: " + ex.Message;
//             }
//             return resData;
//         }
//     }
// }

// using System;
// using System.Threading.Tasks;
// using MySql.Data.MySqlClient;

// namespace COMMON_PROJECT_STRUCTURE_API.services
// {
//     public class uploader
//     {
//         private dbServices ds = new dbServices();

//         public async Task<responseData> Uploader(requestData rData)
//         {
//             responseData resData = new responseData();

//             try
//             {
//                 // Check if the CONTACT exists in the database
//                 var query = @"SELECT * FROM mydb.uploader WHERE CONTACT=@CONTACT";
//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {
//                     new MySqlParameter("@CONTACT", rData.addInfo["CONTACT"].ToString()),
//                 };
//                 var dbData = ds.executeSQL(query, myParam);

//                 if (dbData[0].Count == 0)
//                 {
//                     // Contact doesn't exist, insert a new record

//                     var insertQuery = @"INSERT INTO mydb.uploader (INVENTION, INVENTOR, CONTACT, INVENTION_DEATAILS, IMG_NAME, IMAGE, FILE_NAME, FILE, VISIBILITY) 
//                                         VALUES (@INVENTION, @INVENTOR, @CONTACT, @INVENTION_DEATAILS,@IMG_NAME, @IMAGE, @FILE_NAME, @FILE, @VISIBILITY)";
//                     MySqlParameter[] insertParams = new MySqlParameter[]
//                     {
//                         new MySqlParameter("@INVENTION", rData.addInfo["INVENTION"].ToString()),
//                         new MySqlParameter("@INVENTOR", rData.addInfo["INVENTOR"].ToString()),
//                         new MySqlParameter("@CONTACT", rData.addInfo["CONTACT"].ToString()),
//                         new MySqlParameter("@INVENTION_DEATAILS", rData.addInfo["INVENTION_DEATAILS"].ToString()),
//                         new MySqlParameter("@IMG_NAME", rData.addInfo["IMG_NAME"].ToString()),
//                         new MySqlParameter("@IMAGE", rData.addInfo["IMAGE"].ToString()),
//                         new MySqlParameter("@FILE_NAME", rData.addInfo["FILE_NAME"].ToString()),
//                         new MySqlParameter("@FILE", rData.addInfo["FILE"].ToString()),
//                         new MySqlParameter("@VISIBILITY", rData.addInfo["VISIBILITY"].ToString())
//                     };

//                     var insertResult = ds.executeSQL(insertQuery, insertParams);

//                     resData.rData["rMessage"] = "Uploaded Successfully";
//                 }
//                 else
//                 {
//                     // Contact exists, update the existing record
//                     // Your update logic here...
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "Upload Failed: " + ex.Message;
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
//     public class uploader
//     {
//         dbServices ds = new dbServices();
//         public async Task<responseData> Uploader(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var query = @"INSERT INTO mydb.uploader(INVENTION, INVENTOR, CONTACT, INVENTION_DEATAILS, IMG_NAME, IMAGE, FILE_NAME, FILE, VISIBILITY) values(@INVENTION, @INVENTOR, @CONTACT, @INVENTION_DEATAILS, @IMG_NAME, @IMAGE, @FILE_NAME, @FILE, @VISIBILITY)";
//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {

//                   new MySqlParameter("@INVENTION", rData.addInfo["INVENTION"]), 
//                  new MySqlParameter("@INVENTOR", rData.addInfo["INVENTOR"]),
//                  new MySqlParameter("@CONTACT",rData.addInfo["CONTACT"]),
//                  new MySqlParameter("@INVENTION_DEATAILS", rData.addInfo["INVENTION_DEATAILS"]),
//                  new MySqlParameter("@IMG_NAME", rData.addInfo["IMG_NAME"]),
//                  new MySqlParameter("@IMAGE", rData.addInfo["IMAGE"]),
//                  new MySqlParameter("@FILE_NAME", rData.addInfo["FILE_NAME"]),
//                  new MySqlParameter("@FILE", rData.addInfo["FILE"]),
//                  new MySqlParameter("@VISIBILITY", rData.addInfo["VISIBILITY"]),

//                 };
//                 var dbData = ds.executeSQL(query, myParam);
//                 if (dbData[0].Count() > dbData[0].Count()-1)
//                 {
//                     resData.rData["rMessage"] = "UPLOADED SUCCESSFULLY!";
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class uploader
    {
        dbServices ds = new dbServices();

        public async Task<responseData> Uploader(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"INSERT INTO mydb.uploader(INVENTION, INVENTOR, EMAIL, INVENTION_DEATAILS, IMG_NAME, IMAGE, FILE_NAME, FILE, VISIBILITY) VALUES (@INVENTION, @INVENTOR, @EMAIL, @INVENTION_DEATAILS, @IMG_NAME, @IMAGE, @FILE_NAME, @FILE, @VISIBILITY)";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@INVENTION", rData.addInfo["INVENTION"]),
                    new MySqlParameter("@INVENTOR", rData.addInfo["INVENTOR"]),
                    new MySqlParameter("@EMAIL", rData.addInfo["EMAIL"]),
                    new MySqlParameter("@INVENTION_DEATAILS", rData.addInfo["INVENTION_DEATAILS"]),
                    new MySqlParameter("@IMG_NAME", rData.addInfo["IMG_NAME"]),
                    new MySqlParameter("@IMAGE", rData.addInfo["IMAGE"]),
                    new MySqlParameter("@FILE_NAME", rData.addInfo["FILE_NAME"]),
                    new MySqlParameter("@FILE", rData.addInfo["FILE"]),
                    new MySqlParameter("@VISIBILITY", rData.addInfo["VISIBILITY"]),
                };

                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > dbData[0].Count() - 1)
                {
                    resData.rData["rMessage"] = "UPLOADED SUCCESSFULLY!";
                }
                else
                {
                    resData.rData["rMessage"] = "Something went wrong on the server...";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Upload Failed: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData> EditUploader(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"UPDATE mydb.uploader SET INVENTION=@INVENTION, INVENTOR=@INVENTOR, EMAIL=@EMAIL, INVENTION_DEATAILS=@INVENTION_DEATAILS, IMG_NAME=@IMG_NAME, IMAGE=@IMAGE, FILE_NAME=@FILE_NAME, FILE=@FILE, VISIBILITY=@VISIBILITY WHERE id=@id";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@INVENTION", rData.addInfo["INVENTION"]),
                    new MySqlParameter("@INVENTOR", rData.addInfo["INVENTOR"]),
                    new MySqlParameter("@EMAIL", rData.addInfo["EMAIL"]),
                    new MySqlParameter("@INVENTION_DEATAILS", rData.addInfo["INVENTION_DEATAILS"]),
                    new MySqlParameter("@IMG_NAME", rData.addInfo["IMG_NAME"]),
                    new MySqlParameter("@IMAGE", rData.addInfo["IMAGE"]),
                    new MySqlParameter("@FILE_NAME", rData.addInfo["FILE_NAME"]),
                    new MySqlParameter("@FILE", rData.addInfo["FILE"]),
                    new MySqlParameter("@VISIBILITY", rData.addInfo["VISIBILITY"]),
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

        public async Task<responseData> DeleteUploader(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"DELETE FROM mydb.uploader WHERE id=@id";
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

        public async Task<responseData> GetUserDataByContact(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT id, INVENTION, INVENTOR, EMAIL, INVENTION_DEATAILS, IMG_NAME, IMAGE, FILE_NAME, FILE, VISIBILITY FROM mydb.uploader WHERE EMAIL=@EMAIL";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
            new MySqlParameter("@EMAIL", rData.addInfo["EMAIL"])
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


           public async Task<responseData> EditUserData(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"UPDATE mydb.uploader SET INVENTION=@INVENTION, INVENTOR=@INVENTOR, EMAIL=@EMAIL, INVENTION_DEATAILS=@INVENTION_DEATAILS, IMG_NAME=@IMG_NAME, IMAGE=@IMAGE, FILE_NAME=@FILE_NAME, FILE=@FILE, VISIBILITY=@VISIBILITY WHERE EMAIL=@EMAIL";
                MySqlParameter[] myParam = new MySqlParameter[]
                
                {
                    new MySqlParameter("@INVENTION", rData.addInfo["INVENTION"]),
                    new MySqlParameter("@INVENTOR", rData.addInfo["INVENTOR"]),
                    new MySqlParameter("@EMAIL", rData.addInfo["EMAIL"]),
                    new MySqlParameter("@INVENTION_DEATAILS", rData.addInfo["INVENTION_DEATAILS"]),
                    new MySqlParameter("@IMG_NAME", rData.addInfo["IMG_NAME"]),
                    new MySqlParameter("@IMAGE", rData.addInfo["IMAGE"]),
                    new MySqlParameter("@FILE_NAME", rData.addInfo["FILE_NAME"]),
                    new MySqlParameter("@FILE", rData.addInfo["FILE"]),
                    new MySqlParameter("@VISIBILITY", rData.addInfo["VISIBILITY"]),
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

    }
}

