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

namespace CrudOperationUsingStoreProcedure
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        private void Form1_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(@"Data Source=(local);Initial Catalog=Tutorials;Integrated Security=True");
            cn.Open();
            //bind data in data grid view
            GetAllEmployeeRecord();

            //disable delete and update button on load
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        /// <summary>
        /// get all employee records
        /// </summary>
        private void GetAllEmployeeRecord()
        {
            cmd = new SqlCommand("EmployeeCrudOperation", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Employeeid", 0);
            cmd.Parameters.AddWithValue("@EmployeeName", "");
            cmd.Parameters.AddWithValue("@EmployeeSalary", 0);
            cmd.Parameters.AddWithValue("@EmployeeCity", "");
            cmd.Parameters.AddWithValue("@OperationType", "5");
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        /// <summary>
        /// find record by id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btnfind_Click(object sender, EventArgs e)
        {
            if (txtempid.Text != string.Empty)
            {

                cmd = new SqlCommand("EmployeeCrudOperation", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Employeeid", txtempid.Text);
                cmd.Parameters.AddWithValue("@EmployeeName", "");
                cmd.Parameters.AddWithValue("@EmployeeSalary", 0);
                cmd.Parameters.AddWithValue("@EmployeeCity", "");
                cmd.Parameters.AddWithValue("@OperationType", "4");
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtempname.Text = dr["EmployeeName"].ToString();
                    txtempsalary.Text = dr["EmployeeSalary"].ToString();
                    txtempcity.Text = dr["EmployeeCity"].ToString();
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No record found with this id", "No Data Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dr.Close();
            }
            else
            {
                MessageBox.Show("Please enter employee id ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// update record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (txtempcity.Text != string.Empty && txtempid.Text != string.Empty && txtempname.Text != string.Empty && txtempsalary.Text != string.Empty)
            {
                cmd = new SqlCommand("EmployeeCrudOperation", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Employeeid", txtempid.Text);
                cmd.Parameters.AddWithValue("@EmployeeName", txtempname.Text);
                cmd.Parameters.AddWithValue("@EmployeeSalary", txtempsalary.Text);
                cmd.Parameters.AddWithValue("@EmployeeCity", txtempcity.Text);
                cmd.Parameters.AddWithValue("@OperationType", "2");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record update successfully.", "Record Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetAllEmployeeRecord();
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please enter value in all fields", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (txtempid.Text != string.Empty)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this employee ? ", "Delete Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.Yes)
                {

                    cmd = new SqlCommand("EmployeeCrudOperation", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Employeeid", txtempid.Text);
                    cmd.Parameters.AddWithValue("@EmployeeName", "");
                    cmd.Parameters.AddWithValue("@EmployeeSalary", 0);
                    cmd.Parameters.AddWithValue("@EmployeeCity", "");
                    cmd.Parameters.AddWithValue("@OperationType", "3");
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record deleted successfully.", "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetAllEmployeeRecord();
                    txtempcity.Text = "";
                    txtempid.Text = "";
                    txtempname.Text = "";
                    txtempsalary.Text = "";
                    btnDelete.Enabled = false;
                    btnUpdate.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please enter employee id", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// insert into employee table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btnsave_Click(object sender, EventArgs e)
        {
            if (txtempcity.Text != string.Empty  && txtempname.Text != string.Empty && txtempsalary.Text != string.Empty)
            {
                cmd = new SqlCommand("EmployeeCrudOperation", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Employeeid", 0);
                cmd.Parameters.AddWithValue("@EmployeeName", txtempname.Text);
                cmd.Parameters.AddWithValue("@EmployeeSalary", txtempsalary.Text);
                cmd.Parameters.AddWithValue("@EmployeeCity", txtempcity.Text);
                cmd.Parameters.AddWithValue("@OperationType", "1");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record inserted successfully.", "Record Inserted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetAllEmployeeRecord();
                txtempcity.Text = "";
                txtempid.Text = "";
                txtempname.Text = "";
                txtempsalary.Text = "";
            }
            else
            {
                MessageBox.Show("Please enter value in all fields", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
