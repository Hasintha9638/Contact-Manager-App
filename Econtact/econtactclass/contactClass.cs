using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact.econtactclass
{
    class contactClass
    {

        //act as data carier in our appliction
        //getter and Setter

        public int contactId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string contactNo { get; set; }
        public string address { get; set; }
        public string gender { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
     

        //selecting data from database;
        public DataTable Select()
        {
            //step1:database Configration
            OleDbConnection conn = new OleDbConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //step 2:writing SQL
                string sql = "SELECT * FROM econtact";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }


        //inserting data into database

        public bool Insert(contactClass c)
        {
            //creating    a default return type and setting its value to false
            bool isSuccess = false;
            //step 1:connect data base
            OleDbConnection conn = new OleDbConnection(myconnstrng);
            try
            {   //step 2:create a SQL query to insert data
                string sql = "INSERT INTO econtact(firstname,lastname,email,contactno,address,gender) VALUES (@firstName,@lastName,@email,@contactNo,@address,@gender)";
                //Creating SQL Command using  sql and conn
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                //create parameters to add data
                cmd.Parameters.AddWithValue("@firstName",c.firstName);
                cmd.Parameters.AddWithValue("@lasttName", c.lastName);
                cmd.Parameters.AddWithValue("@email", c.email);
                cmd.Parameters.AddWithValue("@contactNo", c.contactNo);
                cmd.Parameters.AddWithValue("@address", c.address);
                cmd.Parameters.AddWithValue("@gender", c.gender);

                //connection Open here
                conn.Open(); 
                int row = cmd.ExecuteNonQuery();

                //if the query runs successfully then the value of rows will be greater than zero else its value will be zero
                if (row>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;

        }

        //method to update data in data base from application

        public bool update(contactClass c)
        {
            //creating    a default return type and setting its value to false
            bool isSuccess = false;
            //step 1:connect data base
            OleDbConnection conn = new OleDbConnection(myconnstrng);
            try
            {   //step 2:create a SQL query to update data
                string sql = "UPDATE econtact SET firstname=@firstName,lastname=@lastName,email=@email,contactno=@contactNo,address=@address,gender=@gender WHERE ID=@contactId";
                //Creating SQL Command using  sql and conn
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                //create parameters to add data
                cmd.Parameters.AddWithValue("@firstName", c.firstName);
                cmd.Parameters.AddWithValue("@lasttName", c.lastName);
                cmd.Parameters.AddWithValue("@email", c.email);
                cmd.Parameters.AddWithValue("@contactNo", c.contactNo);
                cmd.Parameters.AddWithValue("@address", c.address);
                cmd.Parameters.AddWithValue("@gender", c.gender);
                cmd.Parameters.AddWithValue("@contactId", c.contactId);

                //connection Open here
                conn.Open();
                int row = cmd.ExecuteNonQuery();

                //if the query runs successfully then the value of rows will be greater than zero else its value will be zero
                if (row > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;

        }

        //Method to Deletee drom Application
        public bool delete(contactClass c)
        {
            //creating    a default return type and setting its value to false
            bool isSuccess = false;
            //step 1:connect data base
            OleDbConnection conn = new OleDbConnection(myconnstrng);

            try
            {   //step 2:create a SQL query to delete data
                string sql = "DELETE FROM econtact WHERE ID=@contactId";
                //Creating SQL Command using  sql and conn
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                //create parameters to add data

                cmd.Parameters.AddWithValue("@contactId", c.contactId);

                //connection Open here
                conn.Open();
                int row = cmd.ExecuteNonQuery();

                //if the query runs successfully then the value of rows will be greater than zero else its value will be zero
                if (row > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }


            return isSuccess;

        }
       
    }
}
