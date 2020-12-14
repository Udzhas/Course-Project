using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Course_Project
{
    /// <summary> 
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        private static BlogerContext blogerContext;
        string connectionString;
        SqlDataAdapter adapter;
        DataTable blogersTable;
        public int Id;
        string sqlQuery = String.Empty;
        public MainForm()
        {

            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            LoadBlogers(sqlQuery);

        }

        public void LoadBlogers(string query)
        {
            string sql;
            if (String.IsNullOrEmpty(query)) 
            {
                sql = "SELECT * FROM Blogers";
            }
            else
            {
                sql = query;
            }
            
            blogersTable = new DataTable();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);
                adapter.InsertCommand = new SqlCommand("sp_InsertBlogers", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50, "Name"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@countofsubs", SqlDbType.Int, 0, "CountOfSubs"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@countofposts", SqlDbType.Int, 0, "CountOfPosts"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@avgviewspostsperday", SqlDbType.Int, 0, "AvgViewsPostsPerDay"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@platform", SqlDbType.Int, 0, "Platform"));
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                parameter.Direction = ParameterDirection.Output;
                connection.Open();
                adapter.Fill(blogersTable);
                dgBlogger.ItemsSource = blogersTable.DefaultView;
                tbxName.Visibility = Visibility.Hidden;
                tbxPosts.Visibility = Visibility.Hidden;
                tbxSubs.Visibility = Visibility.Hidden;
                tbxChoose.Visibility = Visibility.Hidden;
                btnDelete.Visibility = Visibility.Hidden;
                btUpd.Visibility = Visibility.Hidden;
                lbName.Visibility = Visibility.Hidden;
                lbSubs.Visibility = Visibility.Hidden;
                lbPosts.Visibility = Visibility.Hidden;
                lavelChoose.Visibility = Visibility.Hidden;
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
        public void UpdateDB()
        {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter);
            adapter.Update(blogersTable);
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                String query = String.Empty;
                blogersTable = new DataTable();
                if (cbInstagram.IsChecked == true && cbYouTube.IsChecked == false)
                {
                    query = "Select * from Blogers where Platform = 'Instagram' ";
                }
                if (cbYouTube.IsChecked == true && cbInstagram.IsChecked == false)
                {
                    query = "Select * from Blogers where Platform = 'YouTube' ";
                }
                if (cbYouTube.IsChecked == true && cbInstagram.IsChecked == true)
                {
                    query = "Select * from Blogers ";
                }

                
                int minS = 0;
                int maxS = Int32.MaxValue;
                int minP = 0;
                int maxP = Int32.MaxValue;
                if (!String.IsNullOrEmpty(tbxFromSubs.Text) && !String.IsNullOrEmpty(tbxToSubs.Text))
                {
                    minS = Convert.ToInt32(tbxFromSubs.Text);
                    maxS = Convert.ToInt32(tbxToSubs.Text);
                    try {
                        if (minS <= maxS)
                        {
                            if (query.Contains("where"))
                            {
                                query += $"and CountOfSubs >= {minS} and CountOfSubs <= {maxS}";
                            }
                            else
                            {
                                query += $"where CountOfSubs >= {minS} and CountOfSubs <= {maxS}";
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Wrong count of subs. Please try again.");
                    }
                    
                }

                if (!String.IsNullOrEmpty(tbxFromPosts.Text) && !String.IsNullOrEmpty(tbxToPosts.Text))
                {
                    minP = Convert.ToInt32(tbxFromPosts.Text);
                    maxP = Convert.ToInt32(tbxToPosts.Text);
                    try
                    {
                        if (minP <= maxP)
                        {

                            if (query.Contains("where"))
                            {
                                query += $"and CountOfPosts >= {minP} and CountOfPosts <= {maxP}";
                            }
                            else 
                            {
                                query += $"where CountOfPosts >= {minP} and CountOfPosts <= {maxP}";
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Wrong count of posts. Please try again.");
                    }

                }
                sqlQuery = query;
                blogersTable = new DataTable();
                try
                {
                    connection = new SqlConnection(connectionString);
                    SqlCommand command = new SqlCommand(query, connection);
                    adapter = new SqlDataAdapter(command);
                    adapter.InsertCommand = new SqlCommand("sp_InsertBlogers", connection);
                    adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50, "Name"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@countofsubs", SqlDbType.Int, 0, "CountOfSubs"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@countofposts", SqlDbType.Int, 0, "CountOfPosts"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@avgviewspostsperday", SqlDbType.Int, 0, "AvgViewsPostsPerDay"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@platform", SqlDbType.Int, 0, "Platform"));
                    SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                    parameter.Direction = ParameterDirection.Output;
                    connection.Open();
                    adapter.Fill(blogersTable);
                    dgBlogger.ItemsSource = blogersTable.DefaultView;
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
            catch
            {
                MessageBox.Show("Some error occurred!");
            }
            tbxName.Visibility = Visibility.Hidden;
            tbxPosts.Visibility = Visibility.Hidden;
            tbxSubs.Visibility = Visibility.Hidden;
            tbxChoose.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;
            btUpd.Visibility = Visibility.Hidden;
            lbName.Visibility = Visibility.Hidden;
            lbSubs.Visibility = Visibility.Hidden;
            lbPosts.Visibility = Visibility.Hidden;
            lavelChoose.Visibility = Visibility.Hidden;

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void itemNew_Click(object sender, RoutedEventArgs e)
        {
            Item item = new Item();
            item.Owner = this;
            item.Show();
            
        }

        private void itemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgBlogger.SelectedItems != null)
            {
                for (int i = 0; i < dgBlogger.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = dgBlogger.SelectedItems[i] as DataRowView;
                    if (datarowView != null)
                    {
                        
                        DataRow dataRow = (DataRow)datarowView.Row;
                        dataRow.Delete();
                    }
                }
            }
            UpdateDB();
        }

        private void FileExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            sqlQuery = null;
            cbInstagram.IsChecked = false;
            cbYouTube.IsChecked = false;
            tbxFromPosts.Text = String.Empty;
            tbxFromSubs.Text = String.Empty;
            tbxToPosts.Text = String.Empty;
            tbxToSubs.Text = String.Empty;
            LoadBlogers(sqlQuery);
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
                StringBuilder commaDelimitedText = new StringBuilder();
                foreach (DataRow row in blogersTable.Rows)
                {
                    string value = string.Format("{0}, {1}, {2}, {3}, {4}, {5}", row[0], row[1], row[2], row[3], row[4], row[5]);
                    commaDelimitedText.AppendLine(value);
                }
                File.WriteAllText(@"C:\Users\Користувач\source\repos\Course Project\text.txt", commaDelimitedText.ToString());
                MessageBox.Show("Successfully saved");
 
        }

        private void btUpd_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                if (!String.IsNullOrEmpty(tbxName.Text) && !String.IsNullOrEmpty(tbxSubs.Text) && !String.IsNullOrEmpty(tbxPosts.Text) && !String.IsNullOrEmpty(tbxChoose.Text))
                {
                    SqlCommand cmd = new SqlCommand("Update Blogers set Name = @name, CountOfSubs = @countofsubs, CountOfPosts = @countofposts, AvgViewsPostsPerDay = @avgviewspostsperday where Id = @id", connection);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@name", tbxName.Text);
                    cmd.Parameters.AddWithValue("@countofsubs", tbxSubs.Text);
                    cmd.Parameters.AddWithValue("@countofposts", tbxPosts.Text);
                    cmd.Parameters.AddWithValue("@avgviewspostsperday", tbxChoose.Text);
                    cmd.Parameters.AddWithValue("@id", this.Id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    LoadBlogers(sqlQuery);
                }
                
                MessageBox.Show("Successfully updated!");
                
            }
            catch
            {
                MessageBox.Show("Error. Please try again.");
            }
        }
        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }

        private void dgBlogger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                Id = Convert.ToInt32(((DataRowView)dgBlogger.SelectedItem).Row["Id"]);

                string name = ((DataRowView)dgBlogger.SelectedItem).Row["Name"].ToString();
                tbxName.Text = name;

                string subs = ((DataRowView)dgBlogger.SelectedItem).Row["CountOfSubs"].ToString();
                tbxSubs.Text = subs;

                string posts = ((DataRowView)dgBlogger.SelectedItem).Row["CountOfPosts"].ToString();
                tbxPosts.Text = posts;

                if (((DataRowView)dgBlogger.SelectedItem).Row["Platform"].ToString() == "YouTube")
                {
                    lavelChoose.Content = "Average views";
                }
                else if(((DataRowView)dgBlogger.SelectedItem).Row["Platform"].ToString() == "Instagram")
                {
                    lavelChoose.Content = "Posts per day";
                }

                string avgViewsPostsPerDay = ((DataRowView)dgBlogger.SelectedItem).Row["AvgViewsPostsPerDay"].ToString();
                tbxChoose.Text = avgViewsPostsPerDay;

                tbxName.Visibility = Visibility.Visible;
                tbxPosts.Visibility = Visibility.Visible;
                tbxSubs.Visibility = Visibility.Visible;
                tbxChoose.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
                btUpd.Visibility = Visibility.Visible;
                lbName.Visibility = Visibility.Visible;
                lbSubs.Visibility = Visibility.Visible;
                lbPosts.Visibility = Visibility.Visible;
                lavelChoose.Visibility = Visibility.Visible;

            }
            catch
            {
                
            }
          
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("Delete From Blogers where Id = @id", connection);
                cmd.CommandType = CommandType.Text;
      
                
                cmd.Parameters.AddWithValue("@id", this.Id);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                
                LoadBlogers(sqlQuery);
                MessageBox.Show("Successfully deleted!");

            }
            catch
            {
                MessageBox.Show("Error. Please try again.");
            }
        }

        private void itemHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This program performs simple CRUD operations with MS Server database. If you have any questions please DM me. Or view program on GitHub: https://github.com/Udzhas/Course-Project");
        }
    }
}
