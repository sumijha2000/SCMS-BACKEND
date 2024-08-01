// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Threading.Tasks;
// using MySql.Data.MySqlClient;

// namespace COMMON_PROJECT_STRUCTURE_API.services
// {
//     public class usernewaccount
//     {
//         dbServices ds = new dbServices();

//         public async Task<responseData> UserNewAccount(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var query = @"INSERT INTO mydb.user_account(name, email, role, password) VALUES (@name, @email, @role, @password)";
//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {
//                     new MySqlParameter("@name", rData.addInfo["name"]),
//                     new MySqlParameter("@email", rData.addInfo["email"]),
//                     new MySqlParameter("@role", rData.addInfo["role"]),
//                     new MySqlParameter("@password", rData.addInfo["password"]),

//                 };

//                 var dbData = ds.executeSQL(query, myParam);
//                 if (dbData[0].Count() > dbData[0].Count() - 1)
//                 {
//                     resData.rData["rMessage"] = "CREATE ACCOUNT SUCCESSFULLY!";
//                 }
//                 else
//                 {
//                     resData.rData["rMessage"] = "Something went wrong on the server...";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "Create Account Failed: " + ex.Message;
//             }
//             return resData;
//         }

//         public async Task<responseData> UpdateUserAccount(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var query = @"
//             UPDATE mydb.user_account 
//             SET name = @name, role = @role, password = @password 
//             WHERE email = @email;
//         ";

//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {
//             new MySqlParameter("@name", rData.addInfo["name"]),
//             new MySqlParameter("@email", rData.addInfo["email"]),
//             new MySqlParameter("@role", rData.addInfo["role"]),
//             new MySqlParameter("@password", rData.addInfo["password"])
//                 };

//                 var dbData = ds.executeSQL(query, myParam);
//                   if (dbData[0].Count() > dbData[0].Count() - 1)
//                 {
//                     resData.rData["rMessage"] = "Account updated successfully!";
//                 }
//                 else
//                 {
//                     resData.rData["rMessage"] = "Update failed. Ensure the email exists.";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "Update Account Failed: " + ex.Message;
//             }
//             return resData;
//         }

//         public async Task<responseData> DeleteUserAccount(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var query = @"
//             DELETE FROM mydb.user_account 
//             WHERE email = @email;
//         ";

//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {
//             new MySqlParameter("@email", rData.addInfo["email"])
//                 };

//                 var dbData = ds.executeSQL(query, myParam);
//                   if (dbData[0].Count() > dbData[0].Count() - 1)
//                 {
//                     resData.rData["rMessage"] = "Account deleted successfully!";
//                 }
//                 else
//                 {
//                     resData.rData["rMessage"] = "Delete failed. Ensure the email exists.";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "Delete Account Failed: " + ex.Message;
//             }
//             return resData;
//         }


//     }
// }


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class usernewaccount
    {
        dbServices ds = new dbServices();

        public async Task<responseData> UserNewAccount(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                // Hash the password before storing it
                var hashedPassword = HashUtility.ComputeSha256Hash(rData.addInfo["password"].ToString());

                var query = @"INSERT INTO mydb.user_account(name, email, role, password) VALUES (@name, @email, @role, @password)";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@name", rData.addInfo["name"]),
                    new MySqlParameter("@email", rData.addInfo["email"]),
                    new MySqlParameter("@role", rData.addInfo["role"]),
                    new MySqlParameter("@password", hashedPassword),
                };

                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > dbData[0].Count() - 1)
                {
                    resData.rData["rMessage"] = "CREATE ACCOUNT SUCCESSFULLY!";
                }
                else
                {
                    resData.rData["rMessage"] = "Something went wrong on the server...";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Create Account Failed: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData> UpdateUserAccount(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                // Hash the password before updating it
                var hashedPassword = HashUtility.ComputeSha256Hash(rData.addInfo["password"].ToString());

                var query = @"
                UPDATE mydb.user_account 
                SET name = @name, role = @role, password = @password 
                WHERE email = @email;
                ";

                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@name", rData.addInfo["name"]),
                    new MySqlParameter("@email", rData.addInfo["email"]),
                    new MySqlParameter("@role", rData.addInfo["role"]),
                    new MySqlParameter("@password", hashedPassword)
                };

                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > dbData[0].Count() - 1)
                {
                    resData.rData["rMessage"] = "Account updated successfully!";
                }
                else
                {
                    resData.rData["rMessage"] = "Update failed. Ensure the email exists.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Update Account Failed: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData> DeleteUserAccount(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"
                DELETE FROM mydb.user_account 
                WHERE email = @email;
                ";

                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@email", rData.addInfo["email"])
                };

                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Count() > dbData[0].Count() - 1)
                {
                    resData.rData["rMessage"] = "Account deleted successfully!";
                }
                else
                {
                    resData.rData["rMessage"] = "Delete failed. Ensure the email exists.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Delete Account Failed: " + ex.Message;
            }
            return resData;
        }
    }
}
