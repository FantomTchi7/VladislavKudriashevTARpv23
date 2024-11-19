using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ProductsWinForms
{
    public partial class ProductsForm : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Products;Integrated Security=True");

        /*
        CREATE TABLE Products (
        ProductID INT PRIMARY KEY IDENTITY(1,1),
        ProductName VARCHAR(50),
        ProductAmount INT,
        ProductPrice DECIMAL(3),
        ProductPicture VARBINARY(MAX));
         */

        SqlCommand command;
        SqlDataAdapter adapter;
        int currentMouseOverRow;
        ContextMenu contextMenu = new ContextMenu(
        new MenuItem[] { new MenuItem("Kustuta") }
        );
        byte[] bytes;
        MemoryStream ms;
        public ProductsForm()
        {
            InitializeComponent();
        }
        private void buttonInsert_Click(object sender, EventArgs e)
        {
            connection.Open();
            command = new SqlCommand("INSERT INTO Products(ProductName, ProductAmount, ProductPrice, ProductPicture) VALUES (@Name, @Amount, @Price, @Picture)", connection);
            command.Parameters.AddWithValue("@Name", textBoxName.Text);
            command.Parameters.AddWithValue("@Amount", textBoxAmount.Text);
            command.Parameters.AddWithValue("@Price", textBoxPrice.Text);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                command.Parameters.AddWithValue("@Picture", imageBytes);
                command.ExecuteNonQuery();
            }

            connection.Close();
            productsTableAdapter.Fill(productsDataSet.Products);
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            if (textBoxID.Text == null)
            {
                command = new SqlCommand("DELETE FROM Products WHERE ProductName = @Name AND ProductAmount = @Amount AND ProductPrice = @Price", connection);
                command.Parameters.AddWithValue("@Name", textBoxName.Text);
                command.Parameters.AddWithValue("@Amount", textBoxAmount.Text);
                command.Parameters.AddWithValue("@Price", textBoxPrice.Text);
            } else
            {
                command = new SqlCommand("DELETE FROM Products WHERE ProductID = @ProductID", connection);
                command.Parameters.AddWithValue("@ProductID", textBoxID.Text);
            }
            command.ExecuteNonQuery();

            command = new SqlCommand("DBCC CHECKIDENT ('Products', RESEED, 0)", connection);
            command.ExecuteNonQuery();

            connection.Close();
            productsTableAdapter.Fill(productsDataSet.Products);
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            command = new SqlCommand("UPDATE Products SET ProductName = @Name, ProductAmount = @Amount, ProductPrice = @Price, ProductPicture = @Picture WHERE ProductID = @ID", connection);
            command.Parameters.AddWithValue("@ID", textBoxID.Text);
            command.Parameters.AddWithValue("@Name", textBoxName.Text);
            command.Parameters.AddWithValue("@Amount", textBoxAmount.Text);
            command.Parameters.AddWithValue("@Price", textBoxPrice.Text);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                command.Parameters.AddWithValue("@Picture", imageBytes);
                command.ExecuteNonQuery();
            }

            connection.Close();
            productsTableAdapter.Fill(productsDataSet.Products);
        }
        private void dataGridViewProducts_ClientSizeChanged(object sender, EventArgs e)
        {
            labelProductID.Top = dataGridViewProducts.Bottom + 10;
            textBoxID.Top = dataGridViewProducts.Bottom + 7;

            buttonInsert.Top = dataGridViewProducts.Bottom + 36;
            buttonDelete.Top = dataGridViewProducts.Bottom + 36;
            buttonUpdate.Top = dataGridViewProducts.Bottom + 36;

            pictureBox.Width = dataGridViewProducts.Right - pictureBox.Left;

            Width = dataGridViewProducts.Right + SystemInformation.VerticalScrollBarWidth + 24;
            Height = dataGridViewProducts.Bottom + 122;
            Refresh();
        }
        private void dataGridViewProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bytes = (byte[]) dataGridViewProducts[4, e.RowIndex].Value;
            using (ms = new MemoryStream(bytes))
            {
                pictureBox.Image = Image.FromStream(ms);
            }
        }
        private void dataGridViewProducts_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenu.Show(dataGridViewProducts, new Point(e.X, e.Y));
            }
        }
        private void ProductsForm_Load(object sender, EventArgs e)
        {
            dataGridViewProducts.AutoSize = true;

            labelProductID.Top = dataGridViewProducts.Bottom + 10;
            textBoxID.Top = dataGridViewProducts.Bottom + 7;

            buttonInsert.Top = dataGridViewProducts.Bottom + 36;
            buttonDelete.Top = dataGridViewProducts.Bottom + 36;
            buttonUpdate.Top = dataGridViewProducts.Bottom + 36;

            pictureBox.Width = dataGridViewProducts.Right - pictureBox.Left;

            Width = dataGridViewProducts.Right + SystemInformation.VerticalScrollBarWidth + 24;
            Height = dataGridViewProducts.Bottom + 122;

            // TODO: This line of code loads data into the 'productsDataSet.Products' table. You can move, or remove it, as needed.
            productsTableAdapter.Fill(productsDataSet.Products);
        }
    }
}
