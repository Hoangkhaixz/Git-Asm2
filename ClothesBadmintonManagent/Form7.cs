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
    public partial class Form7 : Form
    {
        string connectstring = @"Data Source=LAPTOP-I70VJAFS\SQLEXPRESS;Initial Catalog=Hig;Integrated Security=True;TrustServerCertificate=True";
        DataTable dt = new DataTable();
        public Form7()
        {
            InitializeComponent();
        }
        private void btn_load_Click(object sender, EventArgs e)
        {
            LoadStatisticData();
        }

        private void LoadStatisticData()
        {
            using (SqlConnection connection = new SqlConnection(connectstring))
            {
                try
                {
                    connection.Open();

                    // Truy vấn SQL để lọc dữ liệu theo ngày
                    string query = @"
                    SELECT 
                        StatisticID,
                        CustomerProductID,
                        EmployeeID,
                        ProductID,
                        QuantitySold,
                        SaleDate,
                        TotalPrice,
                        InputPrice,
                        (TotalPrice - InputPrice) AS Profit
                    FROM Statistic
                    WHERE SaleDate BETWEEN @StartDate AND @EndDate";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@StartDate", dTP_start.Value.Date);
                    command.Parameters.AddWithValue("@EndDate", dTP_end.Value.Date);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Hiển thị dữ liệu lên DataGridView
                    dGv_Statistic.DataSource = dt;

                    // Tính toán tổng doanh thu, tổng chi phí, và lãi/lỗ
                    CalculateTotals(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error");
                }
            }
        }
        private void CalculateTotals(DataTable dt)
        {
            decimal totalRevenue = 0;
            decimal totalCost = 0;
            decimal totalProfit = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["TotalPrice"] != DBNull.Value)
                    totalRevenue += Convert.ToDecimal(row["TotalPrice"]);

                if (row["InputPrice"] != DBNull.Value)
                    totalCost += Convert.ToDecimal(row["InputPrice"]);

                if (row["Profit"] != DBNull.Value)
                    totalProfit += Convert.ToDecimal(row["Profit"]);
            }

            // Giả sử BaseCost là một tỷ lệ phần trăm của TotalCost (ví dụ: 80%)
            decimal baseCost = totalCost * 0.8m;

            // Hiển thị giá trị lên các TextBox
            txtB_TotalRevenue.Text = totalRevenue.ToString("C2"); // Dạng tiền tệ
            txtB_TotalCost.Text = totalCost.ToString("C2");
            txtB_TotalBaseCost.Text = baseCost.ToString("C2");
            txtB_Profit.Text = totalProfit.ToString("C2");

            // Thêm MessageBox hiển thị thông tin tổng hợp
            MessageBox.Show(
                $"Summary:\n" +
                $"- Total Revenue: {totalRevenue:C2}\n" +
                $"- Total Cost: {totalCost:C2}\n" +
                $"- Base Cost: {baseCost:C2}\n" +
                $"- Profit: {totalProfit:C2}",
                "Calculation Results",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        private void btn_dele_Click(object sender, EventArgs e)
        {
            {
                DialogResult result = MessageBox.Show(
                "Are you sure you want to delete all data in the Statistic table??",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectstring))
                    {
                        try
                        {
                            connection.Open();

                            // Lệnh SQL để xóa tất cả dữ liệu trong bảng Statistic
                            string query = "DELETE FROM Statistic";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                int rowsAffected = command.ExecuteNonQuery();
                                MessageBox.Show($"Deleted successfully {rowsAffected} data stream.", "Delete data");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("An error occurred while deleting data.: " + ex.Message, "Error");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Cancel delete operation.", "Cancel delete");
                }
            }
        }
    }
}
