using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Course_Project
{
    class BlogerContext
    {
        

        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        
        public void AddInstagramBlogger(string name, int countOfSubs, int countOfPosts, int postsPerDay)
        {
            string sql = $"Insert into Blogers([Name],[CountOfSubs],[CountOfPosts],[AvgViewsPostsPerDay],[Platform]) values ({name},{countOfSubs},{countOfPosts},{postsPerDay},'Instagram')";
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public void AddYouTubeBloger(string name, int countOfSubs, int countOfPosts, int averageCountOFViewers)
        {
            string sql = $"Insert into Blogers([Name],[CountOfSubs],[CountOfPosts],[AvgViewsPostsPerDay],[Platform]) values ({name},{countOfSubs},{countOfPosts},{averageCountOFViewers},'YouTube')";
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

    }
}
