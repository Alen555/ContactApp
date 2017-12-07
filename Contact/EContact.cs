using Contact.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contact
{
    public partial class EContact : Form
    {
        public EContact()
        {
            InitializeComponent();
        }
        contactClass c = new contactClass();

        public void btnAdd_Click(object sender, EventArgs e)
        {
            //Get the value from the input fields
            c.FirstName = txtFirstName.Text;
            c.LastName = txtLastName.Text;
            c.ContactNo = txtContactNo.Text;
            c.Adress = txtAdress.Text;
            c.Gender = cmbGender.Text;

            //Inserting data into Database

            bool sucess = c.Insert(c);
            if (sucess == true)
            {
                MessageBox.Show("New Contact Sucessfully Inserted");
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to add New Contact. Try Again.");
            }
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void EContact_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Clear()
        {
            txtContactID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAdress.Text = "";
            txtContactNo.Text = "";
            cmbGender.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {   
            //Get the data from textboxes
            c.ContactID = int.Parse(txtContactID.Text);
            c.FirstName = txtFirstName.Text;
            c.LastName = txtLastName.Text;
            c.Adress = txtAdress.Text;
            c.ContactNo = txtContactNo.Text;
            c.Gender = cmbGender.Text;

            //Upadate data in the database
            bool sucess = c.Update(c);
            if (sucess==true)
            {
                MessageBox.Show("Contact has been succesfully updated");
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to update Contact. Try Again.");
            }
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get the data from data grid view and load it in to the text boxes
            int rowIndex = e.RowIndex;
            txtContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtContactNo.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtAdress.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();

        
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            c.ContactID = Convert.ToInt32(txtContactID.Text);
            bool success = c.Delete(c);
            if (success==true)
            {
                MessageBox.Show("Contact successfully deleted");
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to Delete Contact. Try Again");
            }
        }

        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the value from text box
            string keyword = txtSearch.Text;

            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl-Contact WHERE FristName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' OR Adress LIKE '%" + keyword + "%'", myconnstr);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
}
