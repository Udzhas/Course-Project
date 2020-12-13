using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
    /// Interaction logic for Item.xaml
    /// </summary>
    public partial class Item : Window
    {
        public int blogerType;
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private MainForm mainForm;
        public Item()
        {
            InitializeComponent();
            
        }

        private void btnAddInst_Click(object sender, RoutedEventArgs e)
        {
            Header.Content = "You are adding new Instagram bloger";
            blogerType = 0;
            label.Content = "Posts per day";
        }

        private void btnAddYouTube_Click(object sender, RoutedEventArgs e)
        {
            Header.Content = "You are adding new YouTube bloger";
            blogerType = 1;
            label.Content = "Average count of views";
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    SqlConnection connection = new SqlConnection(connectionString);
                    String query = "INSERT INTO Blogers(Name, CountOfSubs, CountOfPosts, AvgViewsPostsPerDay, Platform) VALUES(@name, @countofsubs, @countofposts, @avgviewspostsperday, @platform)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", tbxName.Text);
                        command.Parameters.AddWithValue("@countofsubs", int.Parse(tbxCountSubs.Text));
                        command.Parameters.AddWithValue("@countofposts", int.Parse(tbxCountPosts.Text));
                        command.Parameters.AddWithValue("@avgviewspostsperday", int.Parse(tbxLabel.Text));
                        if (blogerType == 0)
                        {
                            command.Parameters.AddWithValue("@platform", "Instagram");
                        }
                        else if (blogerType == 1)
                        {
                            command.Parameters.AddWithValue("@platform", "YouTube");
                        }
                        else
                        {
                            MessageBox.Show("Error");
                        }
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                
                
                (Owner as MainForm).LoadBlogers(null);
            }
            catch
            {
                MessageBox.Show("Error");
            }
            
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
