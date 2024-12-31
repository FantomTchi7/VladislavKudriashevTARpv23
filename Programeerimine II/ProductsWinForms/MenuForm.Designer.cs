namespace ProductsWinForms
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textMenuName = new System.Windows.Forms.TextBox();
            textMenuAmount = new System.Windows.Forms.TextBox();
            textMenuPrice = new System.Windows.Forms.TextBox();
            buttonApply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textMenuName
            // 
            textMenuName.Location = new System.Drawing.Point(25, 25);
            textMenuName.Name = "textMenuName";
            textMenuName.Size = new System.Drawing.Size(100, 20);
            textMenuName.TabIndex = 0;
            // 
            // textMenuAmount
            // 
            textMenuAmount.Location = new System.Drawing.Point(25, 51);
            textMenuAmount.Name = "textMenuAmount";
            textMenuAmount.Size = new System.Drawing.Size(100, 20);
            textMenuAmount.TabIndex = 1;
            // 
            // textMenuPrice
            // 
            textMenuPrice.Location = new System.Drawing.Point(25, 77);
            textMenuPrice.Name = "textMenuPrice";
            textMenuPrice.Size = new System.Drawing.Size(100, 20);
            textMenuPrice.TabIndex = 2;
            // 
            // buttonApply
            // 
            buttonApply.Location = new System.Drawing.Point(25, 103);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new System.Drawing.Size(75, 23);
            buttonApply.TabIndex = 3;
            buttonApply.Text = "Rakenda";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += buttonApply_Click;
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(buttonApply);
            this.Controls.Add(textMenuPrice);
            this.Controls.Add(textMenuAmount);
            this.Controls.Add(textMenuName);
            this.Name = "MenuForm";
            this.Text = "MenuForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public static System.Windows.Forms.TextBox textMenuName;
        public static System.Windows.Forms.TextBox textMenuAmount;
        public static System.Windows.Forms.TextBox textMenuPrice;
        public static System.Windows.Forms.Button buttonApply;
    }
}