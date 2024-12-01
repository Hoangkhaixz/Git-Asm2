using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClothesBadmintonManagent
{
    public partial class Form6 : Form
    {
        private int CustomerID;
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataAdapter adt;
        private DataTable dt = new DataTable();
        private string connectionString = @"Data Source=LAPTOP-I70VJAFS\SQLEXPRESS;Initial Catalog=Hig;Integrated Security=True;TrustServerCertificate=True";

        public Form6(int customerID)
        {
            InitializeComponent();
            CustomerID = customerID;
            LoadCustomerName();
        }
        private void LoadCustomerName()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CustomerName FROM Customer WHERE CustomerID = @CustomerID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", CustomerID);
                string customerName = command.ExecuteScalar().ToString();
                txtB_nameCustomer.Text = "Hi, " + customerName + " Welcome to CLothesBadminton..!";
            }
        }
        private void Form6_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                string query = "SELECT ProductID, ProductName, SizeProduct,InputPrice ,SellingPrice, InventoryPrice, ProductImage FROM Products";
                SqlCommand cmd = new SqlCommand(query, con);
                adt = new SqlDataAdapter(cmd);
                adt.Fill(dt);
                GrView_spUser.DataSource = dt;

                // Ẩn InputPrice khỏi DataGridView
                if (dt.Columns.Contains("InputPrice"))
                {
                    GrView_spUser.Columns["InputPrice"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void GrView_spUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra nếu chỉ số dòng hợp lệ
            {
                // Lấy dòng được chọn
                DataGridViewRow selectedRow = GrView_spUser.Rows[e.RowIndex];

                // Hiển thị dữ liệu trong TextBox và ComboBox
                try
                {
                    txtB_idSP.Text = selectedRow.Cells["ProductID"].Value.ToString();
                    txtB_nameSP.Text = selectedRow.Cells["ProductName"].Value.ToString();
                    cbB_sizeSP.SelectedItem = selectedRow.Cells["SizeProduct"].Value.ToString(); // Hiển thị size trong ComboBox
                    txtB_sellSP.Text = selectedRow.Cells["SellingPrice"].Value.ToString();
                    txtB_inventorySP.Text = selectedRow.Cells["InventoryPrice"].Value.ToString();

                    // Hiển thị hình ảnh trong PictureBox
                    if (selectedRow.Cells["ProductImage"].Value != DBNull.Value)
                    {
                        byte[] imageData = (byte[])selectedRow.Cells["ProductImage"].Value; // Lấy dữ liệu hình ảnh
                        if (imageData != null && imageData.Length > 0)
                        {
                            using (MemoryStream ms = new MemoryStream(imageData)) // Chuyển đổi byte array thành hình ảnh
                            {
                                picB_image.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            picB_image.Image = null; // Hoặc hình ảnh mặc định
                        }
                    }
                    else
                    {
                        picB_image.Image = null; // Hoặc hình ảnh mặc định
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message); // Hiển thị thông báo lỗi nếu có
                }
            }
        }
        private void btn_search_Click(object sender, EventArgs e)
        {
            string productId = txtB_searchSP.Text.Trim(); // Lấy ID sản phẩm từ TextBox

            if (!string.IsNullOrEmpty(productId)) // Kiểm tra ID không rỗng
            {
                using (SqlConnection con = new SqlConnection(connectionString)) // Tạo kết nối mới
                {
                    try
                    {
                        con.Open(); // Mở kết nối
                        string query = "SELECT ProductID, ProductName, SizeProduct, InventoryPrice, SellingPrice, ProductImage FROM Products WHERE ProductID = @ProductID";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@ProductID", productId); // Thêm tham số ProductID
                            SqlDataReader reader = cmd.ExecuteReader(); // Thực thi truy vấn

                            if (reader.Read()) // Nếu có dữ liệu trả về
                            {
                                string id = reader["ProductID"].ToString();
                                string name = reader["ProductName"].ToString();
                                string size = reader["SizeProduct"].ToString();
                                decimal inventoryQuantity = reader.GetDecimal(reader.GetOrdinal("InventoryPrice"));
                                decimal sellingPrice = reader.GetDecimal(reader.GetOrdinal("SellingPrice"));

                                // Hiển thị thông tin sản phẩm
                                MessageBox.Show($"Product information:\nID: {id}\nTên: {name}\nKích thước: {size}\nSố lượng tồn kho: {inventoryQuantity}\nGiá bán: {sellingPrice:C}");
                            }
                            else
                            {
                                MessageBox.Show("No product found with ID: " + productId);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Database error: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter product ID to search.");
            }
        }
        private void SaveToStatistic(string productId, int customerId, int employeeId, int quantitySold, decimal totalPrice, decimal costPrice, DateTime saleDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            INSERT INTO Statistic (ProductID, CustomerProductID, EmployeeID, QuantitySold, SaleDate, TotalPrice, InputPrice)
            VALUES (@ProductID, @CustomerProductID, @EmployeeID, @QuantitySold, @SaleDate, @TotalPrice, @InputPrice)"; // Added CostPrice

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productId);
                    command.Parameters.AddWithValue("@CustomerProductID", customerId);
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    command.Parameters.AddWithValue("@QuantitySold", quantitySold);
                    command.Parameters.AddWithValue("@SaleDate", saleDate);
                    command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                    command.Parameters.AddWithValue("@InputPrice", costPrice); // Added CostPrice parameter
                    command.ExecuteNonQuery();
                }
            }
        }

        private void btn_pay_Click(object sender, EventArgs e)
        {
            if (GrView_spUser.SelectedRows.Count > 0) // Kiểm tra xem có dòng nào được chọn không
            {
                var selectedProductRow = GrView_spUser.SelectedRows[0];
                string productId = selectedProductRow.Cells["ProductID"].Value.ToString();
                string productname = selectedProductRow.Cells["ProductName"].Value.ToString();
                decimal sellingPrice = Convert.ToDecimal(selectedProductRow.Cells["SellingPrice"].Value);

                // Truy xuất giá nhập từ cột bị ẩn
                decimal costPrice = Convert.ToDecimal(selectedProductRow.Cells["InputPrice"].Value);
                decimal inventoryQuantity = Convert.ToDecimal(selectedProductRow.Cells["InventoryPrice"].Value);

                if (inventoryQuantity <= 0)
                {
                    MessageBox.Show("Product is out of stock. Unable to checkout.");
                    return;
                }

                if (!int.TryParse(txtB_Quantity.Text, out int quantitySold) || quantitySold <= 0)
                {
                    MessageBox.Show("Please enter a valid sales quantity.");
                    return;
                }
                if (quantitySold > inventoryQuantity)
                {
                    MessageBox.Show("Sales quantity must not exceed inventory quantity.");
                    return;
                }

                decimal totalPrice = sellingPrice * quantitySold;
                decimal totalCost = costPrice * quantitySold; // Tính tổng chi phí
                DateTime saleDate = DateTime.Now;

                // Lấy tên khách hàng từ TextBox txtB_nameCustomer
                string customerName = txtB_nameCustomer.Text.Replace("Hi, ", "").Replace(" Welcome to CLothesBadminton..!", "").Trim();

                // Thêm thông tin tên khách hàng vào hộp thoại xác nhận thanh toán
                DialogResult result = MessageBox.Show(
                    $"Customer Name: {customerName}\n\n" +
                    $"Are you sure you want to pay for the product:\n\n" +
                    $"ID: {productId}\n" +
                    $"Name: {productname}\n" +
                    $"SellingPrice: {sellingPrice:C}\n" +
                    $"CostPrice: {costPrice:C}\n" +
                    $"Inventory: {inventoryQuantity}\n" +
                    $"TotalPrice: {totalPrice:C}\n\n" +
                    "Nhấn OK để xác nhận.",
                    "Xác Nhận Thanh Toán",
                    MessageBoxButtons.OKCancel);

                if (result == DialogResult.OK)
                {
                    // Thanh toán thành công
                    MessageBox.Show("Successful payment for product: " + productname);

                    // Cập nhật số lượng tồn kho
                    UpdateInventory(productId, inventoryQuantity - quantitySold);

                    // Lưu thông tin vào bảng Statistic
                    int employeeId = 1; // Giá trị EmployeeID tạm thời, thay bằng giá trị đúng trong hệ thống
                    SaveToStatistic(productId, CustomerID, employeeId, quantitySold, totalPrice, costPrice, saleDate);
                }
            }
            else
            {
                MessageBox.Show("Please select a product to checkout.");
            }
        }
        private void UpdateInventory(string productId, decimal newQuantity)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Products SET InventoryPrice = @NewQuantity WHERE ProductID = @ProductID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewQuantity", newQuantity);
                    command.Parameters.AddWithValue("@ProductID", productId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
