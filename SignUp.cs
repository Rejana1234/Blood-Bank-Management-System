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
    public partial class SignUp : Form
    {

        //variables
        public string query;
        public SqlCommand cmd;
        SqlConnection conn;
        private bool flag = false;
        public SignUp()
        {
            InitializeComponent();
        }

        private void linklblLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {

            if(txtPass.Text == "" || txtName.Text == "" || txtPass.Text == "" || txtPass.Text != txtConfirmPass.Text)
            {
                MessageBox.Show("Wrong Info");
                
            }
            else
            {
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-APVNB6V;Initial Catalog=BBMSDB;Integrated Security=True");
                conn.Open();
                //query_pannel meaning im inserting my data and executing it


                query = "insert into admin_info (Name,Phone,Password) values('" + txtName.Text + "','" + txtPhone.Text + "','" + txtPass.Text + "')";

                cmd = new SqlCommand(query, conn);

                int row = cmd.ExecuteNonQuery();

                Form1 obj = new Form1();
                obj.Show();
                this.Hide();

            }
        
        }
    }
}
