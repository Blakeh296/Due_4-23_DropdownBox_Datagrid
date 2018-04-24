using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSharp_AW2012_ComboBox_Datagrid_Form;
using System.Data.SqlClient;

namespace CSharp_AW2012_ComboBox_Datagrid_Form
{
    public partial class DataLookup : Form
    {
        const string connString = @"Server=PL11\MTCDEVDB;Database=AdventureWorks2012;Trusted_Connection=True;";
        SqlConnection sqlConn = new SqlConnection(connString);


        //SqlConn GivMe = new SqlConn();

        public DataLookup()
        {
            InitializeComponent();
        }


        private void DataLookup_Load(object sender, EventArgs e)
        {
            SqlDataAdapter sqlDa = new SqlDataAdapter("CustomerList", sqlConn);
            DataTable dtCustomers = new DataTable();
            int CustomerID;
            string CustomerName;

            try
            {
                sqlDa.Fill(dtCustomers);

                foreach (DataRow drCustomer in dtCustomers.Rows)
                {               // Array numbers correspond to the column order in the stored procedure
                    CustomerID = int.Parse(drCustomer.ItemArray[0].ToString());
                    CustomerName = drCustomer.ItemArray[1].ToString();
                    cbActiveCustomers.Items.Add(new ComboObject(CustomerID, CustomerName));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



       

        private void cbActiveCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboObject currentObject = (ComboObject)cbActiveCustomers.SelectedItem;
            int CustomerID = currentObject.CustomerID;
            DataTable dtOrders = new DataTable();

          
            try
            {
               // dataGridView1.DataSource = GivMe.OrdersByName(CustomerID);

                SqlCommand sqlComm = new SqlCommand("sp_CustomerSales", sqlConn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlParameter prmCustomerID = new SqlParameter("@CustID", CustomerID);
                sqlComm.Parameters.Add(prmCustomerID);

                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlComm);

                sqlDa.Fill(dtOrders);

                textBox1.Text = CustomerID.ToString();

                dataGridView1.DataSource = dtOrders;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

         public class ComboObject
        {
            int cID;
            string cName;

            public ComboObject(int CustomerID, string CustName)
            {
                cID = CustomerID;
                cName = CustName;
            }

            public string CustomerName
            {
                get { return cName; }
                set { cName = value; }
            }

            public int CustomerID
            {
                get { return cID; }
                set { cID = value; }
                
            }

            public override string ToString()
            {
                return this.CustomerName;
            }
        }


        private void btnPerform_Click(object sender, EventArgs e)
        {
            DataTable dtUpdate = new DataTable();

            int CustomerID = int.Parse(textBox1.Text);


            try
            {
                SqlCommand sqlComm = new SqlCommand("sp_CustomerSales", sqlConn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlParameter prmCustomerID = new SqlParameter("@CustID", CustomerID);
                sqlComm.Parameters.Add(prmCustomerID);

                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlComm);

                sqlDa.Fill(dtUpdate);

                textBox1.Text = CustomerID.ToString();

                dataGridView1.DataSource = dtUpdate;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
