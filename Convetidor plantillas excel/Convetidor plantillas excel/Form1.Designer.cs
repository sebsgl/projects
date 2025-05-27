namespace Convetidor_plantillas_excel
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_selctFile = new Button();
            lblpath = new Label();
            dtgridInfo = new DataGridView();
            btnGenerarReporte = new Button();
            ((System.ComponentModel.ISupportInitialize)dtgridInfo).BeginInit();
            SuspendLayout();
            // 
            // btn_selctFile
            // 
            btn_selctFile.Location = new Point(38, 50);
            btn_selctFile.Name = "btn_selctFile";
            btn_selctFile.Size = new Size(154, 29);
            btn_selctFile.TabIndex = 0;
            btn_selctFile.Text = "seleccionar archivo";
            btn_selctFile.UseVisualStyleBackColor = true;
            btn_selctFile.Click += button1_Click;
            // 
            // lblpath
            // 
            lblpath.AutoSize = true;
            lblpath.Location = new Point(210, 54);
            lblpath.Name = "lblpath";
            lblpath.Size = new Size(50, 20);
            lblpath.TabIndex = 1;
            lblpath.Text = "label1";
            // 
            // dtgridInfo
            // 
            dtgridInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgridInfo.Location = new Point(38, 177);
            dtgridInfo.Name = "dtgridInfo";
            dtgridInfo.RowHeadersWidth = 51;
            dtgridInfo.RowTemplate.Height = 29;
            dtgridInfo.Size = new Size(598, 191);
            dtgridInfo.TabIndex = 2;
            // 
            // btnGenerarReporte
            // 
            btnGenerarReporte.Location = new Point(516, 390);
            btnGenerarReporte.Name = "btnGenerarReporte";
            btnGenerarReporte.Size = new Size(154, 29);
            btnGenerarReporte.TabIndex = 3;
            btnGenerarReporte.Text = "Generar reporte ";
            btnGenerarReporte.UseVisualStyleBackColor = true;
            btnGenerarReporte.Click += button1_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(787, 450);
            Controls.Add(btnGenerarReporte);
            Controls.Add(dtgridInfo);
            Controls.Add(lblpath);
            Controls.Add(btn_selctFile);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dtgridInfo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_selctFile;
        private Label lblpath;
        private DataGridView dtgridInfo;
        private Button btnGenerarReporte;
    }
}
