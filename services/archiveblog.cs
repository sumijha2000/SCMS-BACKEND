using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class blogpageservice
    {
        private readonly dbServices _dbServices;

        public blogpageservice(dbServices dbServices)
        {
            _dbServices = dbServices;
        }

        public async Task<responseData> ArchiveBlog(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                // Extract blog ID from requestData
                var blogId = rData.addInfo["blogId"].ToString();

                // Archive the blog by setting the 'archived' column to TRUE
                var query = @"UPDATE mydb.blog_data SET archived = TRUE WHERE id = @blogId";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@blogId", blogId)
                };
                var dbData = _dbServices.executeSQL(query, myParam);

                if (dbData[0].Count > 0)
                {
                    resData.rData["rMessage"] = "Blog archived successfully!";
                }
                else
                {
                    resData.rData["rMessage"] = "Blog not found or already archived.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Archive operation failed: " + ex.Message;
            }
            return resData;
        }
    }
}
