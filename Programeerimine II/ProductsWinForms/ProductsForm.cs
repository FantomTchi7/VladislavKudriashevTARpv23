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
        ProductID INT PRIMARY KEY,
        ProductName VARCHAR(50),
        ProductAmount INT,
        ProductPrice DECIMAL(3),
        ProductPicture VARBINARY(MAX));
         */

        SqlCommand command;
        SqlDataAdapter adapter;

        ContextMenu contextMenu = new ContextMenu(
            new MenuItem[] {
                menuItemInsert = new MenuItem("Lisada"),
                menuItemDelete = new MenuItem("Kustuta"),
                menuItemUpdate = new MenuItem("Uuenda")
            }
        );

        int ProductID;
        int ProductRightClickID;

        byte[] bytes;
        MemoryStream ms;
        
        private static MenuItem menuItemInsert;
        private static MenuItem menuItemDelete;
        private static MenuItem menuItemUpdate;

        MenuForm menuForm = new MenuForm();

        public ProductsForm()
        {
            InitializeComponent();
        }
        private void buttonInsert_Click(object sender, EventArgs e)
        {
            connection.Open();
            ProductID = incrementID();
            command = new SqlCommand("INSERT INTO Products(ProductID, ProductName, ProductAmount, ProductPrice, ProductPicture) VALUES (@ID, @Name, @Amount, @Price, @Picture)", connection);
            command.Parameters.AddWithValue("@ID", ProductID);
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
                DataGridView.HitTestInfo hit = dataGridViewProducts.HitTest(e.X, e.Y);
                ProductRightClickID = hit.RowIndex + 1;

                contextMenu.Show(dataGridViewProducts, new Point(e.X, e.Y));
            }
        }
        private void menuItemInsert_Click(object sender, EventArgs e)
        {
            menuForm.ShowDialog();
            connection.Open();
            command = new SqlCommand("INSERT INTO Products(ProductID, ProductName, ProductAmount, ProductPrice, ProductPicture) VALUES (@ID, @Name, @Amount, @Price, @Picture)", connection);
            command.Parameters.AddWithValue("@ID", checkIncrementID(ProductRightClickID));
            command.Parameters.AddWithValue("@Name", MenuForm.textMenuName.Text);
            command.Parameters.AddWithValue("@Amount", MenuForm.textMenuAmount.Text);
            command.Parameters.AddWithValue("@Price", MenuForm.textMenuPrice.Text);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                command.Parameters.AddWithValue("@Picture", imageBytes);
                command.ExecuteNonQuery();
            }

            connection.Close();
            productsTableAdapter.Fill(productsDataSet.Products);
        }
        private void menuItemDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            command = new SqlCommand("DELETE FROM Products WHERE ProductName = @Name AND ProductAmount = @Amount AND ProductPrice = @Price", connection);
            command.Parameters.AddWithValue("@Name", MenuForm.textMenuName.Text);
            command.Parameters.AddWithValue("@Amount", MenuForm.textMenuAmount.Text);
            command.Parameters.AddWithValue("@Price", MenuForm.textMenuPrice.Text);
            
            command.ExecuteNonQuery();

            connection.Close();
            productsTableAdapter.Fill(productsDataSet.Products);
        }
        private void menuItemUpdate_Click(object sender, EventArgs e)
        {
            menuForm.ShowDialog();
            connection.Open();
            command = new SqlCommand("UPDATE Products SET ProductName = @Name, ProductAmount = @Amount, ProductPrice = @Price, ProductPicture = @Picture WHERE ProductID = @ID", connection);
            command.Parameters.AddWithValue("@ID", textBoxID.Text);
            command.Parameters.AddWithValue("@Name", MenuForm.textMenuName.Text);
            command.Parameters.AddWithValue("@Amount", MenuForm.textMenuAmount.Text);
            command.Parameters.AddWithValue("@Price", MenuForm.textMenuPrice.Text);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                command.Parameters.AddWithValue("@Picture", imageBytes);
                command.ExecuteNonQuery();
            }

            connection.Close();
            productsTableAdapter.Fill(productsDataSet.Products);
        }
        private void ProductsForm_Load(object sender, EventArgs e)
        {
            dataGridViewProducts.AutoSize = true;

            menuItemInsert.Click += menuItemInsert_Click;
            menuItemDelete.Click += menuItemDelete_Click;
            menuItemUpdate.Click += menuItemUpdate_Click;

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
        private int incrementID()
        {
            int newID = 1;
            foreach (DataGridViewRow row in dataGridViewProducts.Rows)
            {
                if (row.Cells[0].Value != DBNull.Value)
                {
                    int currentID = Convert.ToInt32(row.Cells[0].Value);
                    if (currentID == newID)
                    {
                        newID++;
                    }
                }
            }
            return newID;
        }
        private int checkIncrementID(int ID)
        {
            for (int i = ID; i < dataGridViewProducts.RowCount; i++)
            {
                if (i == (int)dataGridViewProducts[0, i].Value)
                // System.NullReferenceException: 'Object reference not set to an instance of an object.'
                {
                    return ID;
                } else
                {
                    ID++;
                }
            }
            return ID;
        }
    }
}
