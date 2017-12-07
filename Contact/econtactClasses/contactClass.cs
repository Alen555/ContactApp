using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.econtactClasses
{
    class contactClass
    {
        //Getter Setter Properties
        //Acts as Data Carrier in Our Application
        public int ContactID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactNo { get; set; }

        public string Adress { get; set; }

        public string Gender { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        //Selecting Data from Database

        public DataTable Select()
        {
            // Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //SQL Query
                string sql = "SELECT * FROM [tbl-Contact]";
                //Creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Creating SQL DataAdapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
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

        //Inserting data into Database
        public bool Insert (contactClass c)
        {
            //Creating a default return type and setting its value to false
            bool isSucess = false;

            //Connect to Db
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //Sql query for data insert
                string sql = "INSERT INTO [tbl-Contact] (FristName, LastName, ContactNo, Adress, Gender) VALUES (@FristName, @LastName, @ContactNo, @Adress, @Gender)";
                //Sql Command   
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create Paramaters to add data
                cmd.Parameters.AddWithValue("@FristName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Adress", c.Adress);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                //Open Connetion
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows>0)
                {
                    isSucess = true;
                }
                else
                {
                    isSucess = false;
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Sorry", ex);
            }
            finally
            {
                conn.Close();
            }
            return isSucess;
        }

        //Updating the database
        public bool Update(contactClass c)
        {
            //Creating a default return type and setting its value to false
            bool isSucess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //Sql to update data in Db
                string sql = "UPDATE [tbl-Contact] SET FristName=@FristName, LastName=@LastName, ContactNo=@ContactNo, Adress=@Adress, Gender=@Gender WHERE ContactID=@ContactID";
                //Creating Sql Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create paramaters to add value
                cmd.Parameters.AddWithValue("@FristName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Adress", c.Adress);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                //Open Connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows>0)
                {
                    isSucess = true;
                }
                else
                {
                    isSucess = false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return isSucess;
        }

        //Delete data from database
        public bool Delete(contactClass c)
        {
            //Create a default return value
            bool isSucess = false;
            //Create sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //Sql to delete data
                string sql = "DELETE from [tbl-Contact] WHERE ContactID=@ContactID";

                //Create Sql command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                //Open connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows>0)
                {
                    isSucess = true;
                }
                else
                {
                    isSucess = false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return isSucess;
        }


    }
}
