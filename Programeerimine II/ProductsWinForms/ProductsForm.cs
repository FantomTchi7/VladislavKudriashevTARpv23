using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProductsWinForms
{
    public partial class ProductsForm : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Products;Integrated Security=True");
        SqlCommand command;
        SqlDataAdapter adapter;
        public ProductsForm()
        {
            InitializeComponent();
        }
        private void buttonInsert_Click(object sender, EventArgs e)
        {
            connection.Open();
            command = new SqlCommand("INSERT INTO Products(ProductName, ProductAmount, ProductPrice) VALUES (@Name, @Amount, @Price)", connection);
            command.Parameters.AddWithValue("@Name", textBoxName.Text);
            command.Parameters.AddWithValue("@Amount", textBoxAmount.Text);
            command.Parameters.AddWithValue("@Price", textBoxPrice.Text);
            command.ExecuteNonQuery();

            connection.Close();
            productsTableAdapter.Fill(productsDataSet.Products);
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            command = new SqlCommand("DELETE FROM Products WHERE ProductName = @Name AND ProductAmount = @Amount AND ProductPrice = @Price", connection);
            command.Parameters.AddWithValue("@Name", textBoxName.Text);
            command.Parameters.AddWithValue("@Amount", textBoxAmount.Text);
            command.Parameters.AddWithValue("@Price", textBoxPrice.Text);
            command.ExecuteNonQuery();

            command = new SqlCommand("DBCC CHECKIDENT ('Products', RESEED, 0)", connection);
            command.ExecuteNonQuery();

            connection.Close();
            productsTableAdapter.Fill(productsDataSet.Products);
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            command = new SqlCommand("UPDATE Products SET ProductName = @Name, ProductAmount = @Amount, ProductPrice = @Price WHERE ProductID = @ID", connection);
            command.Parameters.AddWithValue("@ID", textBoxID.Text);
            command.Parameters.AddWithValue("@Name", textBoxName.Text);
            command.Parameters.AddWithValue("@Amount", textBoxAmount.Text);
            command.Parameters.AddWithValue("@Price", textBoxPrice.Text);
            command.ExecuteNonQuery();

            connection.Close();
            productsTableAdapter.Fill(productsDataSet.Products);
        }
        private void ProductsForm_Load(object sender, EventArgs e)
        {
            dataGridViewProducts.AutoSize = true;
            Width = dataGridViewProducts.Width + 25;
            // TODO: This line of code loads data into the 'productsDataSet.Products' table. You can move, or remove it, as needed.
            productsTableAdapter.Fill(productsDataSet.Products);
        }
    }
}
