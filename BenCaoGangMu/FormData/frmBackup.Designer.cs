namespace BenCaoGangMu.FormData
{
    partial class frmBackup
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
            this.cboDbName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectedFold = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lblTip = new System.Windows.Forms.Label();
            this.lblmsg = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnLogBack = new System.Windows.Forms.Button();
            this.btnDFback = new System.Windows.Forms.Button();
            this.btnDBBack = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cboDbName
            // 
            this.cboDbName.FormattingEnabled = true;
            this.cboDbName.Location = new System.Drawing.Point(306, 200);
            this.cboDbName.Name = "cboDbName";
            this.cboDbName.Size = new System.Drawing.Size(216, 20);
            this.cboDbName.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 32;
            this.label2.Text = "请选择要备份的数据库：";
            // 
            // btnSelectedFold
            // 
            this.btnSelectedFold.Location = new System.Drawing.Point(549, 238);
            this.btnSelectedFold.Name = "btnSelectedFold";
            this.btnSelectedFold.Size = new System.Drawing.Size(75, 23);
            this.btnSelectedFold.TabIndex = 31;
            this.btnSelectedFold.Text = "浏览";
            this.btnSelectedFold.UseVisualStyleBackColor = true;
            this.btnSelectedFold.Click += new System.EventHandler(this.btnSelectedFold_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(306, 240);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(216, 21);
            this.txtPath.TabIndex = 30;
            this.txtPath.Text = "D:\\DataBackup ";
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Location = new System.Drawing.Point(112, 243);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(173, 12);
            this.lblTip.TabIndex = 29;
            this.lblTip.Text = "请选择备份文件保存的文件夹：";
            // 
            // lblmsg
            // 
            this.lblmsg.AutoSize = true;
            this.lblmsg.ForeColor = System.Drawing.Color.Red;
            this.lblmsg.Location = new System.Drawing.Point(143, 284);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(0, 12);
            this.lblmsg.TabIndex = 28;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(584, 323);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(116, 43);
            this.btnClose.TabIndex = 27;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnLogBack
            // 
            this.btnLogBack.Location = new System.Drawing.Point(424, 323);
            this.btnLogBack.Name = "btnLogBack";
            this.btnLogBack.Size = new System.Drawing.Size(116, 43);
            this.btnLogBack.TabIndex = 26;
            this.btnLogBack.Text = "事务日志备份";
            this.btnLogBack.UseVisualStyleBackColor = true;
            this.btnLogBack.Click += new System.EventHandler(this.btnLogBack_Click);
            // 
            // btnDFback
            // 
            this.btnDFback.Location = new System.Drawing.Point(264, 323);
            this.btnDFback.Name = "btnDFback";
            this.btnDFback.Size = new System.Drawing.Size(116, 43);
            this.btnDFback.TabIndex = 25;
            this.btnDFback.Text = "差异备份";
            this.btnDFback.UseVisualStyleBackColor = true;
            this.btnDFback.Click += new System.EventHandler(this.btnDFback_Click);
            // 
            // btnDBBack
            // 
            this.btnDBBack.Location = new System.Drawing.Point(104, 323);
            this.btnDBBack.Name = "btnDBBack";
            this.btnDBBack.Size = new System.Drawing.Size(116, 43);
            this.btnDBBack.TabIndex = 24;
            this.btnDBBack.Text = "完整备份";
            this.btnDBBack.UseVisualStyleBackColor = true;
            this.btnDBBack.Click += new System.EventHandler(this.btnDBBack_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.DarkViolet;
            this.label1.Location = new System.Drawing.Point(111, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(446, 100);
            this.label1.TabIndex = 23;
            this.label1.Text = "   1 备份文件默认保存在 D:\\DataBackup 文件夹中\r\n   2 备份文件的名字格式为：\r\n       原数据库名称_备份时的日期_db.bak\r" +
    "\n       例如：CCTV_20120618_db.bak";
            // 
            // frmBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 427);
            this.Controls.Add(this.cboDbName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSelectedFold);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.lblmsg);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnLogBack);
            this.Controls.Add(this.btnDFback);
            this.Controls.Add(this.btnDBBack);
            this.Controls.Add(this.label1);
            this.Name = "frmBackup";
            this.Text = "数据备份";
            this.Load += new System.EventHandler(this.frmBackup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboDbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectedFold;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Label lblmsg;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnLogBack;
        private System.Windows.Forms.Button btnDFback;
        private System.Windows.Forms.Button btnDBBack;
        private System.Windows.Forms.Label label1;
    }
}