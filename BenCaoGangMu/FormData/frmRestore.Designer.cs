namespace BenCaoGangMu.FormData
{
    partial class frmRestore
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoContinue = new System.Windows.Forms.RadioButton();
            this.rdoEnd = new System.Windows.Forms.RadioButton();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnLogBack = new System.Windows.Forms.Button();
            this.btnDFback = new System.Windows.Forms.Button();
            this.btnDBBack = new System.Windows.Forms.Button();
            this.btnSelectedFile = new System.Windows.Forms.Button();
            this.txtBackFileName = new System.Windows.Forms.TextBox();
            this.lblTip = new System.Windows.Forms.Label();
            this.lblmsg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 339);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 47;
            this.label3.Text = "还原选项：";
            // 
            // txtDBName
            // 
            this.txtDBName.Location = new System.Drawing.Point(242, 299);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(216, 21);
            this.txtDBName.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 302);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 12);
            this.label2.TabIndex = 45;
            this.label2.Text = "请输入还原后的数据库名：";
            // 
            // rdoContinue
            // 
            this.rdoContinue.AutoSize = true;
            this.rdoContinue.Checked = true;
            this.rdoContinue.Location = new System.Drawing.Point(335, 337);
            this.rdoContinue.Name = "rdoContinue";
            this.rdoContinue.Size = new System.Drawing.Size(71, 16);
            this.rdoContinue.TabIndex = 44;
            this.rdoContinue.TabStop = true;
            this.rdoContinue.Text = "继续还原";
            this.rdoContinue.UseVisualStyleBackColor = true;
            // 
            // rdoEnd
            // 
            this.rdoEnd.AutoSize = true;
            this.rdoEnd.Location = new System.Drawing.Point(242, 337);
            this.rdoEnd.Name = "rdoEnd";
            this.rdoEnd.Size = new System.Drawing.Size(71, 16);
            this.rdoEnd.TabIndex = 43;
            this.rdoEnd.Text = "结束还原";
            this.rdoEnd.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(515, 386);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(116, 43);
            this.btnClose.TabIndex = 42;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnLogBack
            // 
            this.btnLogBack.Location = new System.Drawing.Point(355, 386);
            this.btnLogBack.Name = "btnLogBack";
            this.btnLogBack.Size = new System.Drawing.Size(116, 43);
            this.btnLogBack.TabIndex = 41;
            this.btnLogBack.Text = "事务日志还原";
            this.btnLogBack.UseVisualStyleBackColor = true;
            this.btnLogBack.Click += new System.EventHandler(this.btnLogBack_Click);
            // 
            // btnDFback
            // 
            this.btnDFback.Location = new System.Drawing.Point(195, 386);
            this.btnDFback.Name = "btnDFback";
            this.btnDFback.Size = new System.Drawing.Size(116, 43);
            this.btnDFback.TabIndex = 40;
            this.btnDFback.Text = "差异还原";
            this.btnDFback.UseVisualStyleBackColor = true;
            this.btnDFback.Click += new System.EventHandler(this.btnDFback_Click);
            // 
            // btnDBBack
            // 
            this.btnDBBack.Location = new System.Drawing.Point(35, 386);
            this.btnDBBack.Name = "btnDBBack";
            this.btnDBBack.Size = new System.Drawing.Size(116, 43);
            this.btnDBBack.TabIndex = 39;
            this.btnDBBack.Text = "完整还原";
            this.btnDBBack.UseVisualStyleBackColor = true;
            this.btnDBBack.Click += new System.EventHandler(this.btnDBBack_Click);
            // 
            // btnSelectedFile
            // 
            this.btnSelectedFile.Location = new System.Drawing.Point(485, 270);
            this.btnSelectedFile.Name = "btnSelectedFile";
            this.btnSelectedFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectedFile.TabIndex = 38;
            this.btnSelectedFile.Text = "浏览";
            this.btnSelectedFile.UseVisualStyleBackColor = true;
            this.btnSelectedFile.Click += new System.EventHandler(this.btnSelectedFile_Click);
            // 
            // txtBackFileName
            // 
            this.txtBackFileName.Location = new System.Drawing.Point(242, 272);
            this.txtBackFileName.Name = "txtBackFileName";
            this.txtBackFileName.Size = new System.Drawing.Size(216, 21);
            this.txtBackFileName.TabIndex = 37;
            this.txtBackFileName.Text = " ";
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Location = new System.Drawing.Point(48, 275);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(125, 12);
            this.lblTip.TabIndex = 36;
            this.lblTip.Text = "请选择要还原的文件：";
            // 
            // lblmsg
            // 
            this.lblmsg.AutoSize = true;
            this.lblmsg.ForeColor = System.Drawing.Color.Red;
            this.lblmsg.Location = new System.Drawing.Point(106, 244);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(0, 12);
            this.lblmsg.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.DarkViolet;
            this.label1.Location = new System.Drawing.Point(45, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(515, 200);
            this.label1.TabIndex = 34;
            this.label1.Text = "   1 备份文件默认保存在 D:\\DataBackup 文件夹中\r\n   2 备份文件的名字格式为：\r\n       原数据库名称_备份时的日期_db.bak\r" +
    "\n       例如：CCTV_20120618_db.bak\r\n   3 还原数据，应从上述的文件夹中选择日期\r\n       最近的备份文件，进行还原\r\n " +
    "  4 还原后的数据库保存在原数据库所在的文件夹中\r\n       ";
            // 
            // frmRestore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 488);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDBName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rdoContinue);
            this.Controls.Add(this.rdoEnd);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnLogBack);
            this.Controls.Add(this.btnDFback);
            this.Controls.Add(this.btnDBBack);
            this.Controls.Add(this.btnSelectedFile);
            this.Controls.Add(this.txtBackFileName);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.lblmsg);
            this.Controls.Add(this.label1);
            this.Name = "frmRestore";
            this.Text = "数据还原";
            this.Load += new System.EventHandler(this.frmRestore_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDBName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdoContinue;
        private System.Windows.Forms.RadioButton rdoEnd;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnLogBack;
        private System.Windows.Forms.Button btnDFback;
        private System.Windows.Forms.Button btnDBBack;
        private System.Windows.Forms.Button btnSelectedFile;
        private System.Windows.Forms.TextBox txtBackFileName;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Label lblmsg;
        private System.Windows.Forms.Label label1;
    }
}