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
    public partial class Form0 : Form
    {
        public Form0()
        {
            InitializeComponent();
        }
        private void btn_accept_Click(object sender, EventArgs e)
        {
            // Password validation
            if (!IsValidPassword(txtB_Password.Text))
            {
                MessageBox.Show("Password must contain at least one uppercase letter, one special character, and one number.");
                return; // Dừng lại nếu mật khẩu không hợp lệ
            }

            string connectionString = @"Data Source=LAPTOP-I70VJAFS\SQLEXPRESS;Initial Catalog=Hig;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Lấy giới tính từ Radio Buttons
                string gender = "";
                if (rad_male.Checked)
                {
                    gender = "Male";
                }
                else if (rad_female.Checked)
                {
                    gender = "Female";
                }
                else
                {
                    MessageBox.Show("Enter selection gender.");
                    return; // Dừng lại nếu chưa chọn giới tính
                }

                // Thêm thông tin khách hàng vào bảng Customer
                string insertCustomerQuery = "INSERT INTO Customer (CustomerName, Gender, BirthofDate, Phone, Gmail) " +
                                             "VALUES (@CustomerName, @Gender, @BirthofDate, @Phone, @Gmail); " +
                                             "SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(insertCustomerQuery, connection);
                command.Parameters.AddWithValue("@CustomerName", txtB_CustomerName.Text);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@BirthofDate", dtp_BirthofDate.Value);
                command.Parameters.AddWithValue("@Phone", txtB_Phone.Text);
                command.Parameters.AddWithValue("@Gmail", txtB_email.Text);

                // Lấy CustomerID mới thêm
                int customerId = Convert.ToInt32(command.ExecuteScalar());

                // Thêm tài khoản đăng nhập vào bảng Users
                string salt = GenerateSalt();
                string hashedPassword = HashPassword(txtB_Password.Text, salt);
                string insertUserQuery = "INSERT INTO Users (UserName, PasswordHash, Salt, Role, CustomerID) " +
                                         "VALUES (@UserName, @PasswordHash, @Salt, 'User', @CustomerID)";

                SqlCommand userCommand = new SqlCommand(insertUserQuery, connection);
                userCommand.Parameters.AddWithValue("@UserName", txtB_UserName.Text);
                userCommand.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                userCommand.Parameters.AddWithValue("@Salt", salt);
                userCommand.Parameters.AddWithValue("@CustomerID", customerId);

                userCommand.ExecuteNonQuery();
                MessageBox.Show("Register Successfull!");
            }
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
        private bool IsValidPassword(string password)
        {
            // Kiểm tra các điều kiện mật khẩu
            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(ch => "!@#$%^&*()_+-=[]{}|;':\",.<>?/~`".Contains(ch));

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }
        private static string GenerateSalt()
        {
            // Tạo mảng byte ngẫu nhiên để làm salt
            byte[] saltBytes = new byte[16];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            // Chuyển đổi mảng byte sang chuỗi Base64
            return Convert.ToBase64String(saltBytes);
        }
        private static string HashPassword(string password, string salt)
        {
            // Kết hợp mật khẩu với salt
            string saltedPassword = password + salt;

            // Chuyển đổi chuỗi mật khẩu đã kết hợp thành mảng byte
            byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);

            // Sử dụng SHA256 để băm mật khẩu
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(saltedPasswordBytes);
                // Chuyển đổi mảng byte thành chuỗi Base64
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
