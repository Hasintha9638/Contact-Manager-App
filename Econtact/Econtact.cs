
using Econtact.econtactclass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact
{
    public partial class Econtact : Form
    {

        contactClass c = new contactClass();

        public Econtact()
        {
            InitializeComponent();
         
            
                txtFirstName.Select();
            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            //Get the values from input fields 
            c.firstName = txtFirstName.Text;
            c.lastName = txtLastName.Text;
            c.email = txtEmail.Text;
            c.contactNo = txtCntNo.Text;
            c.address = txtAddress.Text;
            c.gender = comboGender.Text;

            //Inserting data into database
            bool isSuccess = c.Insert(c);
            if (isSuccess == true)
            {
                MessageBox.Show("New Contact Successfully Inserted ");
                clear();
            }
            else
            {
                MessageBox.Show("Faild to Added new Contact try again");
            }

            //load data table

            DataTable dt = c.Select();
            dgvContact.DataSource = dt;

        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dgvContact.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //update data
            c.contactId = int.Parse(txtCntID.Text);
            c.firstName = txtFirstName.Text;
            c.lastName = txtLastName.Text;
            c.email = txtEmail.Text;
            c.contactNo = txtCntNo.Text;
            c.address = txtAddress.Text;
            c.gender = comboGender.Text;

            bool isSuccess = c.update(c);
            if (isSuccess == true)
            {
                MessageBox.Show("Contact Successfully Updated ");
                clear();
            }
            else
            {
                MessageBox.Show("Faild to Updated  Contact try again");
            }

            //load data table

            DataTable dt = c.Select();
            dgvContact.DataSource = dt;



        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();

        }
        public void clear()
        {
            txtAddress.Text = "";
            txtCntNo.Text = "";
            txtEmail.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            comboGender.Text = "";
            txtCntID.Text = "";
        }
        static int rowindex=-1;
        private void dgvContact_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the data from Data grid view and load it to the textboxes respectively
            //identify the row on which mouse is clicked 
            rowindex = e.RowIndex;
            txtCntID.Text = dgvContact.Rows[rowindex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvContact.Rows[rowindex].Cells[1].Value.ToString();
            txtLastName.Text = dgvContact.Rows[rowindex].Cells[2].Value.ToString();
            txtEmail.Text = dgvContact.Rows[rowindex].Cells[3].Value.ToString();
            txtCntNo.Text = dgvContact.Rows[rowindex].Cells[4].Value.ToString();
            txtAddress.Text = dgvContact.Rows[rowindex].Cells[5].Value.ToString();
            comboGender.Text = dgvContact.Rows[rowindex].Cells[6].Value.ToString();


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {   
            if (rowindex>-1)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to permanently delete this?", "Delete Appoinment", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    c.contactId = int.Parse(txtCntID.Text);

                    bool isSuccess = c.delete(c);
                    if (isSuccess == true)
                    {
                        MessageBox.Show("Contact Successfully Deleted ");
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Faild to Deleted  Contact try again");
                    }
                }
            }
            rowindex = -1;

            //load data table

            DataTable dt = c.Select();
            dgvContact.DataSource = dt;
        }
        static string mystrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //get the value from text value
            string keyword = txtSearch.Text;
            OleDbConnection conn = new OleDbConnection(mystrng);
            string sql = "SELECT * FROM econtact where firstname LIKE '%"+keyword+ "%' OR lastname LIKE '%" + keyword + "%' ";
            OleDbCommand cmd = new OleDbCommand(sql,conn);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvContact.DataSource = dt;
        }

        private void txtFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Down)
            {
                txtLastName.Select();
            }
        }

        private void txtLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtEmail.Select();
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtCntNo.Select();
            }
        }

        private void txtCntNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtAddress.Select();
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                comboGender.Select();
            }
        }

        private void dgvContact_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                c.contactId = int.Parse(txtCntID.Text);

                bool isSuccess = c.delete(c);
                if (isSuccess == true)
                {
                    MessageBox.Show("Contact Successfully Deleted ");
                    clear();
                }
                else
                {
                    MessageBox.Show("Faild to Deleted  Contact try again");
                }

                //load data table

                DataTable dt = c.Select();
                dgvContact.DataSource = dt;
            }
        }
    }
}
