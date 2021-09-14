using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBank
{
   
    public partial class Form1 : Form
    {

        string username = "tanvir";
        string password = "1235";
        public string query;
        public SqlCommand cmd;
        SqlConnection conn;
        int id = 10;

        public Form1()
        {
            InitializeComponent();
        }



        private void btnLogin_Click(object sender, EventArgs e)
        { 
            
            //sql_connection
            conn = new SqlConnection(@"Data Source=DESKTOP-APVNB6V;Initial Catalog=BBMSDB;Integrated Security=True");
            conn.Open();

            //query_pannel meaning im inserting my data and executing it
            query = "select * from admin_info where Name ='" + txtBoxUsername.Text + "' and password ='"+txtBoxPass.Text+"'";
            cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();


            //converting to dataset
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count != 0)
            {
                Dashboard obj = new Dashboard();
                obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
            conn.Close();
        }

        private void linklblCreateAcc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUp obj = new SignUp();
            obj.Show();
            this.Hide();
        }
    }
}
