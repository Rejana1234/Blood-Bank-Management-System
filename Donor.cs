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
    public partial class Donor : Form
    {
        //variables
        public string query;
        public SqlCommand cmd;
        SqlConnection conn;
        private bool isNew = false;
        public Donor()
        {
            InitializeComponent();
        }

        private void exeTable()
        {
            //sql_connection
            conn = new SqlConnection(@"Data Source=DESKTOP-APVNB6V;Initial Catalog=BBMSDB;Integrated Security=True");
            conn.Open();

            //query_pannel meaning im inserting my data and executing it
            query = "select * from Donors_b";
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

        private void cleartxtBox()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            comboBlood.Text = "";
            txtLocation.Text = "";
            txtAge.Text = "";
            comboGender.Text = "";
        }

        private void Donor_Load(object sender, EventArgs e)
        {
            exeTable();
        }

        private void dtView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dtView.Rows[e.RowIndex].Cells[0].Value.ToString());
                //  MessageBox.Show(e.RowIndex.ToString());


                //sql_connection
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-APVNB6V;Initial Catalog=BBMSDB;Integrated Security=True");
                conn.Open();

                //query_pannel meaning im inserting my data and executing it

                string query = "select * from Donors_b where ID =" + id;
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                //converting to dataset
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                txtId.Text = dt.Rows[0]["ID"].ToString();
                txtLocation.Text = dt.Rows[0]["Location"].ToString();
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtPhone.Text = dt.Rows[0]["Phone"].ToString();
                txtAge.Text = dt.Rows[0]["Age"].ToString();
                comboBlood.Text = dt.Rows[0]["Blood"].ToString();
                comboGender.Text = dt.Rows[0]["Gender"].ToString();
                conn.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //sql_connection
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-APVNB6V;Initial Catalog=BBMSDB;Integrated Security=True");
                conn.Open();
                //query_pannel meaning im inserting my data and executing it

                if (isNew == false)
                {
                    query = "update Donors_b set Name='" + txtName.Text + "',Location='" + txtLocation.Text + "',Phone='" + txtPhone.Text + "',Age='" + txtAge.Text + "'," +
                        "Gender='" + comboGender.Text + "',Blood='" + comboBlood.Text + "' where ID='" + txtId.Text + "'";
                }
                else
                {
                    query = "insert into Donors_b (Name,Location,Phone,Age,Gender,Blood) values('" + txtName.Text + "','" + txtLocation.Text + "','" + txtPhone.Text + "','" + txtAge.Text + "','" + comboGender.Text + "','" + comboBlood.Text + "')";
                    isNew = false;
                }
                cmd = new SqlCommand(query, conn);

                int row = cmd.ExecuteNonQuery();

                exeTable();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            cleartxtBox();
            isNew = true;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Please select a row first");
            }
            else
            {
                //sql_connection
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-APVNB6V;Initial Catalog=BBMSDB;Integrated Security=True");
                conn.Open();

                //query_pannel meaning im inserting my data and executing it

                string query = "Delete from Donors_b where ID =" + txtId.Text;
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                cleartxtBox();
                exeTable();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cleartxtBox();
            exeTable();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtBoxSearch.Text == "")
            {
                MessageBox.Show("Search box is empty");
            }
            else
            {
                //sql_connection
                conn = new SqlConnection(@"Data Source=DESKTOP-APVNB6V;Initial Catalog=BBMSDB;Integrated Security=True");
                conn.Open();

                //query_pannel meaning im inserting my data and executing it
                if (comboSearch.Text == "Name")
                {
                    query = "select * from Donors_b where Name like '%" + txtBoxSearch.Text + "%'";

                }
                else if (comboSearch.Text == "Location")
                {
                    query = "select * from Donors_b where Location like '%" + txtBoxSearch.Text + "%'";
                }
                else if (comboSearch.Text == "Blood")
                {
                    query = "select * from Donors_b where Blood like '%" + txtBoxSearch.Text + "%'";
                }
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                //converting to dataset
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                dtView.AutoGenerateColumns = false;
                dtView.DataSource = dt;
                dtView.Refresh();
                conn.Close();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }
    }
    
}
