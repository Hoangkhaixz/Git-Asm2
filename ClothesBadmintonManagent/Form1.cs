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
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=LAPTOP-I70VJAFS\SQLEXPRESS;Initial Catalog=Hig;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection con;
        //SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
            //AddAdminAccount();
        }
        private void btn_register_Click(object sender, EventArgs e)
        {
            Form0 form0 = new Form0();
            form0.Show();
        }

        private void AddAdminAccount()
        {
            string adminUserName = "admin"; // Tên tài khoản admin
            string adminPassword = "@Admin123"; // Mật khẩu của admin

            // Tạo Salt ngẫu nhiên
            string salt = GenerateSalt();

            // Băm mật khẩu với Salt
            string hashedPassword = HashPassword(adminPassword, salt);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (UserName, PasswordHash, Salt, Role) VALUES (@UserName, @PasswordHash, @Salt, 'Admin')";
                SqlCommand command = new SqlCommand(query, conn);

                // Truyền tham số
                command.Parameters.AddWithValue("@UserName", adminUserName);
                command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                command.Parameters.AddWithValue("@Salt", salt);

                conn.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Admin account added successfully.");
            }
        }
        private void btn_login_Click(object sender, EventArgs e)
        {
            // Check if the username or password fields are empty
            if (string.IsNullOrWhiteSpace(txtB_username.Text) || string.IsNullOrWhiteSpace(txtB_pass.Text))
            {
                MessageBox.Show("Please enter your username and password.");
                return;
            }

            // Check username length
            if (txtB_username.Text.Length < 5)
            {
                MessageBox.Show("Username must be at least 5 characters long.");
                return;
            }

            // Check if password meets complexity requirements
            if (txtB_pass.Text.Length < 8 ||
                !txtB_pass.Text.Any(char.IsUpper) ||
                !txtB_pass.Text.Any(char.IsLower) ||
                !txtB_pass.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Password must be at least 8 characters long, include an uppercase letter, a lowercase letter, and a number.");
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT PasswordHash, Salt, Role, CustomerID FROM Users WHERE UserName = @UserName";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserName", txtB_username.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHash = reader["PasswordHash"].ToString();
                                string storedSalt = reader["Salt"].ToString();
                                string role = reader["Role"].ToString();
                                int customerID = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"]) : 0;

                                // Hash lại mật khẩu nhập vào với salt từ database
                                string hashedInputPassword = HashPassword(txtB_pass.Text, storedSalt);

                                if (hashedInputPassword == storedHash)
                                {
                                    MessageBox.Show("Login Successfull!");

                                    if (role == "Admin")
                                    {
                                        Form2 form2 = new Form2();
                                        form2.Show();
                                    }
                                    else
                                    {
                                        // Mở Form khách hàng với tên khách hàng
                                        Form6 form6 = new Form6(customerID);
                                        form6.Show();
                                        this.Hide();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Wrong password!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("No found the account!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to SQL Server: " + ex.Message);
            }
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
    }
}
