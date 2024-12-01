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
    public partial class Form4 : Form
    {
        string connectstring = @"Data Source=LAPTOP-I70VJAFS\SQLEXPRESS;Initial Catalog=Hig;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt = new DataTable();
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectstring);
            try
            {
                con.Open();
                cmd = new SqlCommand("select * from Employee", con);
                adt = new SqlDataAdapter(cmd);
                adt.Fill(dt);
                grView_hienthi7.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grView_hienthi7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure that the click is on a valid row (not header or empty)
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = grView_hienthi7.Rows[e.RowIndex];

                // Retrieve values from the selected row and display them on the form
                txtB_idEmployee.Text = selectedRow.Cells[0].Value.ToString();         // ID Customer
                txtB_nameEmployee.Text = selectedRow.Cells[1].Value.ToString();       // Name Customer
                if (selectedRow.Cells[2].Value.ToString() == "FEMALE")
                {
                    rad_femaEmp.Checked = true;  // Set fema gender radio button
                }
                else if (selectedRow.Cells[2].Value.ToString() == "MALE")
                {
                    rad_maleEmp.Checked = true;  // Set male gender radio button
                }
                txtB_emailEmp.Text = selectedRow.Cells[3].Value.ToString();        // Email
                txtB_phoneEmp.Text = selectedRow.Cells[4].Value.ToString();      // Phone Number
                txtB_dateEmp.Text = selectedRow.Cells[5].Value.ToString();      // Date
            }
        }
        private void btn_addEmp_Click(object sender, EventArgs e)
        {
            // Validate user inputs
            if (string.IsNullOrWhiteSpace(txtB_idEmployee.Text) ||
                string.IsNullOrWhiteSpace(txtB_nameEmployee.Text) ||
                string.IsNullOrWhiteSpace(txtB_emailEmp.Text) ||
                string.IsNullOrWhiteSpace(txtB_phoneEmp.Text) ||
                string.IsNullOrWhiteSpace(txtB_dateEmp.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            int ID;
            // Check if ID is a valid integer and not negative
            if (!int.TryParse(txtB_idEmployee.Text, out ID) || ID < 0)
            {
                MessageBox.Show("Employee ID must be a non-negative integer.");
                return;
            }

            string Name = txtB_nameEmployee.Text;
            string Gender = rad_femaEmp.Checked ? "FEMALE" : "MALE";
            string Email = txtB_emailEmp.Text;
            string Phone = txtB_phoneEmp.Text;
            DateTime BirthofDate;

            // Validate and parse the date
            if (!DateTime.TryParse(txtB_dateEmp.Text, out BirthofDate))
            {
                MessageBox.Show("Invalid date format. Please use yyyy-MM-dd.");
                return;
            }

            // Validate email format
            if (!IsValidEmail(Email))
            {
                MessageBox.Show("Invalid email format.");
                return;
            }

            // Validate phone number (just as an example, can be customized)
            if (!IsValidPhoneNumber(Phone))
            {
                MessageBox.Show("Invalid phone number format.");
                return;
            }

            // Create and execute the SQL INSERT command
            string insertQuery = "INSERT INTO Employee (EmployeeID, EmployeeName, Gender, Email, Phone, BirthofDate) VALUES (@EmployeeID, @EmployeeName, @Gender, @Email, @Phone, @BirthofDate)";

            using (SqlConnection con = new SqlConnection(connectstring)) // Use the correct connection string
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", ID);
                    cmd.Parameters.AddWithValue("@EmployeeName", Name);
                    cmd.Parameters.AddWithValue("@Gender", Gender);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Phone", Phone);
                    cmd.Parameters.AddWithValue("@BirthofDate", BirthofDate);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        // Optionally, refresh the DataGridView after adding
                        LoadEmployeeData(); // Assuming you have this method to load data into the DataGridView
                        MessageBox.Show("Employee added successfully!");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Database error: " + ex.Message);
                    }
                }
            } 
        }
       
        private void btn_updateEmp_Click(object sender, EventArgs e)
        {
            if (grView_hienthi7.SelectedRows.Count > 0)
            {
                // Lấy giá trị từ các trường
                string EmployeeID = txtB_idEmployee.Text;
                string EmployeeName = txtB_nameEmployee.Text;
                string Gender = rad_femaEmp.Checked ? "FEMALE" : "MALE";
                DateTime BirthofDate;
                string Email = txtB_emailEmp.Text;
                string Phone = txtB_phoneEmp.Text;

                // Chuyển đổi chuỗi ngày thành kiểu DateTime
                if (!DateTime.TryParse(txtB_dateEmp.Text, out BirthofDate))
                {
                    MessageBox.Show("Invalid date format. Please use yyyy-MM-dd.");
                    return;
                }

                // Kiểm tra các trường hợp rỗng hoặc không hợp lệ
                if (string.IsNullOrWhiteSpace(EmployeeID) ||
                    string.IsNullOrWhiteSpace(EmployeeName) ||
                    string.IsNullOrWhiteSpace(Email) ||
                    string.IsNullOrWhiteSpace(Phone))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                // Kiểm tra định dạng email hợp lệ
                if (!IsValidEmail(Email))
                {
                    MessageBox.Show("Invalid email format.");
                    return;
                }

                // Kiểm tra định dạng số điện thoại hợp lệ (ví dụ chỉ chứa số và có độ dài 10)
                if (!IsValidPhoneNumber(Phone))
                {
                    MessageBox.Show("Invalid phone number format.");
                    return;
                }

                // Tạo câu lệnh SQL UPDATE
                string updateQuery = "UPDATE Employee SET EmployeeName = @EmployeeName, Gender = @Gender, Email = @Email, Phone = @Phone, BirthofDate = @BirthofDate WHERE EmployeeID = @EmployeeID";

                using (SqlConnection con = new SqlConnection(connectstring)) // Sử dụng đúng chuỗi kết nối
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                        cmd.Parameters.AddWithValue("@EmployeeName", EmployeeName);
                        cmd.Parameters.AddWithValue("@Gender", Gender);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Phone", Phone);
                        cmd.Parameters.AddWithValue("@BirthofDate", BirthofDate);

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            // Làm mới DataGridView sau khi cập nhật
                            LoadEmployeeData(); // Giả sử bạn có phương thức này để làm mới dữ liệu
                            MessageBox.Show("Employee updated successfully!");
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
                MessageBox.Show("Please select a row to update");
            }
        }
        private void btn_deleEmp_Click(object sender, EventArgs e)
        {
            if (grView_hienthi7.SelectedRows.Count > 0)
            {
                // Lấy giá trị CustomerID của dòng được chọn
                string EmployeeID = grView_hienthi7.SelectedRows[0].Cells[0].Value.ToString();

                // Xác nhận trước khi xóa
                var confirmResult = MessageBox.Show("Are you sure you want to delete this employee?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // Tạo câu lệnh SQL DELETE
                    string deleteQuery = "DELETE FROM Employee WHERE EmployeeID = @EmployeeID";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            // Làm mới lại DataGridView
                            LoadEmployeeData();
                            MessageBox.Show("Employee deleted successfully!");
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

        private void btn_exitEmp_Click(object sender, EventArgs e)
        {
            DialogResult exitz = MessageBox.Show("Are you want to exit..?",
               "Warning",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);

            if (exitz == DialogResult.No)
            {
                MessageBox.Show("Stay");
            }
            else
            {
                Hide();
            }
        }
        // Method to validate email format
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

        // Method to validate phone number format (customize as needed)
        private bool IsValidPhoneNumber(string phone)
        {
            return phone.All(char.IsDigit) && phone.Length == 10; // Example validation for a 10-digit number
        }
        private void LoadEmployeeData()
        {
            dt.Clear();
            adt.Fill(dt);
            grView_hienthi7.DataSource = dt;
        }

    }
}
