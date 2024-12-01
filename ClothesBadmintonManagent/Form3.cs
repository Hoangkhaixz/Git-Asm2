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

namespace ClothesBadmintonManagent
{
    public partial class Form3 : Form
    {
        string connectstring = @"Data Source=LAPTOP-I70VJAFS\SQLEXPRESS;Initial Catalog=Hig;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt = new DataTable();
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectstring);
            try
            {
                con.Open();
                cmd = cmd = new SqlCommand("select * from Customer", con);
                adt = new SqlDataAdapter(cmd);
                adt.Fill(dt);
                grView_hienthi.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grView_hienthi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure that the click is on a valid row (not header or empty)
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = grView_hienthi.Rows[e.RowIndex];

                // Retrieve values from the selected row and display them on the form
                txtB_idCustomer.Text = selectedRow.Cells[0].Value.ToString();         // ID Customer
                txtB_nameCustomer.Text = selectedRow.Cells[1].Value.ToString();       // Name Customer
                if (selectedRow.Cells[2].Value.ToString() == "FEMALE")
                {
                    rad_fema.Checked = true;  // Set Male gender radio button
                }
                else if (selectedRow.Cells[2].Value.ToString() == "MALE")
                {
                    rad_male.Checked = true;  // Set Female gender radio button
                }
                txtB_dateCustomer.Text = selectedRow.Cells[3].Value.ToString();        // Date of Birth
                txtB_phoneCustomer.Text = selectedRow.Cells[4].Value.ToString();      // Phone Number
                txtB_emailCustomer.Text = selectedRow.Cells[5].Value.ToString();      // Email
            }
        }

        private void btn_addCustomer_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường dữ liệu không được để trống
            if (string.IsNullOrWhiteSpace(txtB_nameCustomer.Text))
            {
                MessageBox.Show("Customer name cannot be empty.");
                txtB_nameCustomer.Focus();
                return;
            }

            if (!rad_fema.Checked && !rad_male.Checked)
            {
                MessageBox.Show("Please select a gender.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtB_dateCustomer.Text))
            {
                MessageBox.Show("Date of birth cannot be empty.");
                txtB_dateCustomer.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtB_phoneCustomer.Text))
            {
                MessageBox.Show("Phone number cannot be empty.");
                txtB_phoneCustomer.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtB_emailCustomer.Text))
            {
                MessageBox.Show("Email cannot be empty.");
                txtB_emailCustomer.Focus();
                return;
            }

            // Kiểm tra định dạng ngày
            DateTime BirthofDate;
            if (!DateTime.TryParseExact(txtB_dateCustomer.Text, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out BirthofDate))
            {
                MessageBox.Show("Invalid date format. Please use yyyy-MM-dd.");
                txtB_dateCustomer.Focus();
                return;
            }

            // Kiểm tra định dạng email
            if (!IsValidEmail(txtB_emailCustomer.Text))
            {
                MessageBox.Show("Invalid email format.");
                txtB_emailCustomer.Focus();
                return;
            }

            // Kiểm tra định dạng số điện thoại (chỉ chứa số)
            if (!txtB_phoneCustomer.Text.All(char.IsDigit))
            {
                MessageBox.Show("Phone number must contain only digits.");
                txtB_phoneCustomer.Focus();
                return;
            }

            // Chuẩn bị dữ liệu
            string CustomerName = txtB_nameCustomer.Text.Trim();
            string Gender = rad_fema.Checked ? "FEMALE" : "MALE";
            string Phone = txtB_phoneCustomer.Text.Trim();
            string Gmail = txtB_emailCustomer.Text.Trim();

            string insertQuery = "INSERT INTO Customer (CustomerName, Gender, BirthofDate, Phone, Gmail) " +
                                 "VALUES (@CustomerName, @Gender, @BirthofDate, @Phone, @Gmail); " +
                                 "SELECT SCOPE_IDENTITY();";

            using (SqlCommand cmd = new SqlCommand(insertQuery, con))
            {
                cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@BirthofDate", BirthofDate);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Gmail", Gmail);

                try
                {
                    con.Open();
                    // Lấy giá trị ID mới được thêm vào
                    int newCustomerID = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close(); 

                    // Hiển thị thông báo và làm mới DataGridView
                    MessageBox.Show($"Customer added successfully! New CustomerID: {newCustomerID}");
                    LoadCustomerData();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL Error: " + ex.Message);
                }
            }
        }
        private void btn_updateCustomer_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn trong DataGridView không
            if (grView_hienthi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to update.");
                return;
            }

            // Kiểm tra các trường không được để trống
            if (string.IsNullOrWhiteSpace(txtB_idCustomer.Text))
            {
                MessageBox.Show("Customer ID cannot be empty.");
                txtB_idCustomer.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtB_nameCustomer.Text))
            {
                MessageBox.Show("Customer name cannot be empty.");
                txtB_nameCustomer.Focus();
                return;
            }

            if (!rad_fema.Checked && !rad_male.Checked)
            {
                MessageBox.Show("Please select a gender.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtB_dateCustomer.Text))
            {
                MessageBox.Show("Date of birth cannot be empty.");
                txtB_dateCustomer.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtB_phoneCustomer.Text))
            {
                MessageBox.Show("Phone number cannot be empty.");
                txtB_phoneCustomer.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtB_emailCustomer.Text))
            {
                MessageBox.Show("Email cannot be empty.");
                txtB_emailCustomer.Focus();
                return;
            }

            // Kiểm tra định dạng ngày
            DateTime BirthofDate;
            if (!DateTime.TryParseExact(txtB_dateCustomer.Text, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out BirthofDate))
            {
                // Kiểm tra giá trị hợp lệ
                string[] dateParts = txtB_dateCustomer.Text.Split('-');
                if (dateParts.Length != 3 ||
                    !int.TryParse(dateParts[0], out int year) ||
                    !int.TryParse(dateParts[1], out int month) ||
                    !int.TryParse(dateParts[2], out int day) ||
                    month < 1 || month > 12 ||
                    day < 1 || day > DateTime.DaysInMonth(year, month))
                {
                    MessageBox.Show("Invalid date format or logical date values. Please use yyyy-MM-dd.");
                    txtB_dateCustomer.Focus();
                    return;
                }
            }

            // Kiểm tra định dạng email
            if (!IsValidEmail(txtB_emailCustomer.Text))
            {
                MessageBox.Show("Invalid email format.");
                txtB_emailCustomer.Focus();
                return;
            }

            // Kiểm tra định dạng số điện thoại (chỉ chứa số)
            if (!txtB_phoneCustomer.Text.All(char.IsDigit))
            {
                MessageBox.Show("Phone number must contain only digits.");
                txtB_phoneCustomer.Focus();
                return;
            }

            // Chuẩn bị dữ liệu
            string CustomerID = txtB_idCustomer.Text.Trim();
            string CustomerName = txtB_nameCustomer.Text.Trim();
            string Gender = rad_fema.Checked ? "FEMALE" : "MALE";
            string Phone = txtB_phoneCustomer.Text.Trim();
            string Gmail = txtB_emailCustomer.Text.Trim();

            string updateQuery = "UPDATE Customer SET CustomerName = @CustomerName, Gender = @Gender, BirthofDate = @BirthofDate, Phone = @Phone, Gmail = @Gmail " +
                                "WHERE CustomerID = @CustomerID";

            using (SqlCommand cmd = new SqlCommand(updateQuery, con))
            {
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@BirthofDate", BirthofDate);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Gmail", Gmail);

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        // Làm mới DataGridView
                        LoadCustomerData();
                        MessageBox.Show("Customer updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Customer not found or no changes were made.");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL Error: " + ex.Message);
                }
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void btn_deleCustomer_Click(object sender, EventArgs e)
        {
            if (grView_hienthi.SelectedRows.Count > 0)
            {
                // Lấy giá trị CustomerID của dòng được chọn
                string CustomerID = grView_hienthi.SelectedRows[0].Cells[0].Value.ToString();

                // Xác nhận trước khi xóa
                var confirmResult = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // Tạo câu lệnh SQL DELETE
                    string deleteQuery = "DELETE FROM Customer WHERE CustomerID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            // Làm mới lại DataGridView
                            LoadCustomerData();
                            MessageBox.Show("Customer deleted successfully!");
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("SQL Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete");
            }
        }
        private void LoadCustomerData()
        {
            dt.Clear();
            adt.Fill(dt);
            grView_hienthi.DataSource = dt;
        }

        private void btn_exitCustomer_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Are you want to exit..?",
               "Warning",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);

            if (exit == DialogResult.No)
            {
                MessageBox.Show("STAY..!");
            }
            else
            {
                Hide();
            }
        }
    }
}
