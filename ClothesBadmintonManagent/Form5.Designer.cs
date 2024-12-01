namespace ClothesBadmintonManagent
{
    partial class Form5
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
            this.label1 = new System.Windows.Forms.Label();
            this.GridV_hienthi6 = new System.Windows.Forms.DataGridView();
            this.txtB_search = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.btn_addProduct = new System.Windows.Forms.Button();
            this.btn_updProduct = new System.Windows.Forms.Button();
            this.btn_deleProduct = new System.Windows.Forms.Button();
            this.txtB_idProduct = new System.Windows.Forms.TextBox();
            this.txtB_nameProduct = new System.Windows.Forms.TextBox();
            this.txtB_priceProduct = new System.Windows.Forms.TextBox();
            this.cbB_sizeProduct = new System.Windows.Forms.ComboBox();
            this.txtB_inventProduct = new System.Windows.Forms.TextBox();
            this.picB_imageProduct = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtB_inputPrice = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.GridV_hienthi6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB_imageProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Crimson;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(401, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 32);
            this.label1.TabIndex = 8;
            this.label1.Text = "Get Product Data";
            // 
            // GridV_hienthi6
            // 
            this.GridV_hienthi6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridV_hienthi6.Location = new System.Drawing.Point(-3, 153);
            this.GridV_hienthi6.Name = "GridV_hienthi6";
            this.GridV_hienthi6.RowHeadersWidth = 51;
            this.GridV_hienthi6.RowTemplate.Height = 24;
            this.GridV_hienthi6.Size = new System.Drawing.Size(727, 488);
            this.GridV_hienthi6.TabIndex = 30;
            this.GridV_hienthi6.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridV_hienthi6_CellClick);
            // 
            // txtB_search
            // 
            this.txtB_search.Location = new System.Drawing.Point(83, 104);
            this.txtB_search.Multiline = true;
            this.txtB_search.Name = "txtB_search";
            this.txtB_search.Size = new System.Drawing.Size(412, 22);
            this.txtB_search.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 22);
            this.label2.TabIndex = 32;
            this.label2.Text = "Search:";
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_search.Location = new System.Drawing.Point(534, 104);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(92, 27);
            this.btn_search.TabIndex = 33;
            this.btn_search.Text = "SEARCH";
            this.btn_search.UseVisualStyleBackColor = false;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(772, 77);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(99, 22);
            this.label18.TabIndex = 35;
            this.label18.Text = "ID Product:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(761, 125);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(129, 22);
            this.label19.TabIndex = 36;
            this.label19.Text = "Name Product:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(772, 388);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(116, 22);
            this.label20.TabIndex = 38;
            this.label20.Text = "Size Product:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(772, 534);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(110, 22);
            this.label21.TabIndex = 39;
            this.label21.Text = "SellingPrice:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(785, 484);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 22);
            this.label22.TabIndex = 40;
            this.label22.Text = "Inventory:";
            // 
            // btn_addProduct
            // 
            this.btn_addProduct.Location = new System.Drawing.Point(751, 571);
            this.btn_addProduct.Name = "btn_addProduct";
            this.btn_addProduct.Size = new System.Drawing.Size(80, 28);
            this.btn_addProduct.TabIndex = 41;
            this.btn_addProduct.Text = "ADD";
            this.btn_addProduct.UseVisualStyleBackColor = true;
            this.btn_addProduct.Click += new System.EventHandler(this.btn_addProduct_Click);
            // 
            // btn_updProduct
            // 
            this.btn_updProduct.Location = new System.Drawing.Point(861, 571);
            this.btn_updProduct.Name = "btn_updProduct";
            this.btn_updProduct.Size = new System.Drawing.Size(80, 28);
            this.btn_updProduct.TabIndex = 42;
            this.btn_updProduct.Text = "UPDATE";
            this.btn_updProduct.UseVisualStyleBackColor = true;
            this.btn_updProduct.Click += new System.EventHandler(this.btn_updProduct_Click);
            // 
            // btn_deleProduct
            // 
            this.btn_deleProduct.Location = new System.Drawing.Point(989, 571);
            this.btn_deleProduct.Name = "btn_deleProduct";
            this.btn_deleProduct.Size = new System.Drawing.Size(80, 28);
            this.btn_deleProduct.TabIndex = 43;
            this.btn_deleProduct.Text = "DELETE";
            this.btn_deleProduct.UseVisualStyleBackColor = true;
            this.btn_deleProduct.Click += new System.EventHandler(this.btn_deleProduct_Click);
            // 
            // txtB_idProduct
            // 
            this.txtB_idProduct.Location = new System.Drawing.Point(920, 77);
            this.txtB_idProduct.Multiline = true;
            this.txtB_idProduct.Name = "txtB_idProduct";
            this.txtB_idProduct.Size = new System.Drawing.Size(149, 22);
            this.txtB_idProduct.TabIndex = 44;
            // 
            // txtB_nameProduct
            // 
            this.txtB_nameProduct.Location = new System.Drawing.Point(920, 127);
            this.txtB_nameProduct.Multiline = true;
            this.txtB_nameProduct.Name = "txtB_nameProduct";
            this.txtB_nameProduct.Size = new System.Drawing.Size(149, 22);
            this.txtB_nameProduct.TabIndex = 45;
            // 
            // txtB_priceProduct
            // 
            this.txtB_priceProduct.Location = new System.Drawing.Point(930, 534);
            this.txtB_priceProduct.Multiline = true;
            this.txtB_priceProduct.Name = "txtB_priceProduct";
            this.txtB_priceProduct.Size = new System.Drawing.Size(121, 22);
            this.txtB_priceProduct.TabIndex = 47;
            // 
            // cbB_sizeProduct
            // 
            this.cbB_sizeProduct.FormattingEnabled = true;
            this.cbB_sizeProduct.Items.AddRange(new object[] {
            "S",
            "M",
            "L",
            "XL",
            "XXL"});
            this.cbB_sizeProduct.Location = new System.Drawing.Point(930, 386);
            this.cbB_sizeProduct.Name = "cbB_sizeProduct";
            this.cbB_sizeProduct.Size = new System.Drawing.Size(121, 24);
            this.cbB_sizeProduct.TabIndex = 48;
            // 
            // txtB_inventProduct
            // 
            this.txtB_inventProduct.Location = new System.Drawing.Point(930, 486);
            this.txtB_inventProduct.Multiline = true;
            this.txtB_inventProduct.Name = "txtB_inventProduct";
            this.txtB_inventProduct.Size = new System.Drawing.Size(121, 24);
            this.txtB_inventProduct.TabIndex = 49;
            // 
            // picB_imageProduct
            // 
            this.picB_imageProduct.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.picB_imageProduct.Location = new System.Drawing.Point(798, 155);
            this.picB_imageProduct.Name = "picB_imageProduct";
            this.picB_imageProduct.Size = new System.Drawing.Size(253, 204);
            this.picB_imageProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picB_imageProduct.TabIndex = 37;
            this.picB_imageProduct.TabStop = false;
            this.picB_imageProduct.Click += new System.EventHandler(this.picB_imageProduct_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ClothesBadmintonManagent.Properties.Resources.cac_tay_vot_xuat_sac;
            this.pictureBox2.Location = new System.Drawing.Point(723, 71);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(22, 570);
            this.pictureBox2.TabIndex = 34;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Crimson;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(-3, -1);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(1113, 72);
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(778, 437);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 22);
            this.label3.TabIndex = 50;
            this.label3.Text = "InputPrice:";
            // 
            // txtB_inputPrice
            // 
            this.txtB_inputPrice.Location = new System.Drawing.Point(930, 439);
            this.txtB_inputPrice.Multiline = true;
            this.txtB_inputPrice.Name = "txtB_inputPrice";
            this.txtB_inputPrice.Size = new System.Drawing.Size(121, 22);
            this.txtB_inputPrice.TabIndex = 51;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1109, 641);
            this.Controls.Add(this.txtB_inputPrice);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtB_inventProduct);
            this.Controls.Add(this.cbB_sizeProduct);
            this.Controls.Add(this.txtB_priceProduct);
            this.Controls.Add(this.txtB_nameProduct);
            this.Controls.Add(this.txtB_idProduct);
            this.Controls.Add(this.btn_deleProduct);
            this.Controls.Add(this.btn_updProduct);
            this.Controls.Add(this.btn_addProduct);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.picB_imageProduct);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtB_search);
            this.Controls.Add(this.GridV_hienthi6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox3);
            this.Name = "Form5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form5";
            this.Load += new System.EventHandler(this.Form5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridV_hienthi6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB_imageProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView GridV_hienthi6;
        private System.Windows.Forms.TextBox txtB_search;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.PictureBox picB_imageProduct;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btn_addProduct;
        private System.Windows.Forms.Button btn_updProduct;
        private System.Windows.Forms.Button btn_deleProduct;
        private System.Windows.Forms.TextBox txtB_idProduct;
        private System.Windows.Forms.TextBox txtB_nameProduct;
        private System.Windows.Forms.TextBox txtB_priceProduct;
        private System.Windows.Forms.ComboBox cbB_sizeProduct;
        private System.Windows.Forms.TextBox txtB_inventProduct;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtB_inputPrice;
    }
}