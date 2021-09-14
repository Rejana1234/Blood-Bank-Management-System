using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BloodBank
{
    public partial class Stock : Form
    {

        //variables
        public string query;
        public SqlCommand cmd;
        SqlConnection conn;
        private bool isNew = false;


        public Stock()
        {
            InitializeComponent();
        }

        private void exeTable()
        {
            //sql_connection
            conn = new SqlConnection(@"Data Source=DESKTOP-APVNB6V;Initial Catalog=BBMSDB;Integrated Security=True");
            conn.Open();

            //query_pannel meaning im inserting my data and executing it
            query = "select * from Blood_group";
            cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();

            //converting to dataset
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            //   dtView.AutoGenerateColumns = false;
            dtView.DataSource = dt;
            dtView.Refresh();
            conn.Close();
        }
        

        private void Stock_Load(object sender, EventArgs e)
        {
            exeTable();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            try
            {
                //sql_connection
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-APVNB6V;Initial Catalog=BBMSDB;Integrated Security=True");
                conn.Open();
                //query_pannel meaning im inserting my data and executing it

               
                    query = "update Blood_group set Stock=Stock+"+ txtBoxAmount.Text + " where Blood='" + comboBlood.Text + "'";
           
                cmd = new SqlCommand(query, conn);

                int row = cmd.ExecuteNonQuery();

                exeTable();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDecrease_Click(object sender, EventArgs e)
        {
            try
            {
                //sql_connection
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-APVNB6V;Initial Catalog=BBMSDB;Integrated Security=True");
                conn.Open();
                //query_pannel meaning im inserting my data and executing it


                query = "update Blood_group set Stock=Stock-" + txtBoxAmount.Text + " where Blood='" + comboBlood.Text + "'";

                cmd = new SqlCommand(query, conn);

                int row = cmd.ExecuteNonQuery();

                exeTable();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
