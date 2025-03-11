
namespace Delivery
{
    partial class BinWhsCode
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
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem4 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem5 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.GridEX.GridEXLayout gridBinLocation_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BinWhsCode));
            this.btnCancel1 = new Janus.Windows.EditControls.UIButton();
            this.btnOk1 = new Janus.Windows.EditControls.UIButton();
            this.cbx_LoaiBin = new Janus.Windows.EditControls.UIComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtconlai = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtphanbo = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtsoluong = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtmahang = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtmakho = new Janus.Windows.GridEX.EditControls.EditBox();
            this.gridBinLocation = new Janus.Windows.GridEX.GridEX();
            ((System.ComponentModel.ISupportInitialize)(this.gridBinLocation)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel1
            // 
            this.btnCancel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel1.Location = new System.Drawing.Point(148, 535);
            this.btnCancel1.Name = "btnCancel1";
            this.btnCancel1.Size = new System.Drawing.Size(75, 34);
            this.btnCancel1.TabIndex = 4;
            this.btnCancel1.Text = "Hủy";
            this.btnCancel1.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // btnOk1
            // 
            this.btnOk1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk1.Location = new System.Drawing.Point(30, 535);
            this.btnOk1.Name = "btnOk1";
            this.btnOk1.Size = new System.Drawing.Size(75, 34);
            this.btnOk1.TabIndex = 3;
            this.btnOk1.Text = "Chọn";
            this.btnOk1.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.btnOk1.Click += new System.EventHandler(this.btnOk1_Click);
            // 
            // cbx_LoaiBin
            // 
            this.cbx_LoaiBin.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "Default Bin Location";
            uiComboBoxItem1.Value = "Default Bin Location";
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "Last Bin Location That Received Item";
            uiComboBoxItem2.Value = "Last Bin Location That Received Item";
            uiComboBoxItem3.FormatStyle.Alpha = 0;
            uiComboBoxItem3.IsSeparator = false;
            uiComboBoxItem3.Text = "Item\'s Current Bin Locations";
            uiComboBoxItem3.Value = "Item\'s Current Bin Locations";
            uiComboBoxItem4.FormatStyle.Alpha = 0;
            uiComboBoxItem4.IsSeparator = false;
            uiComboBoxItem4.Text = "Item\'s Current and Historical Bin Locations";
            uiComboBoxItem4.Value = "Item\'s Current and Historical Bin Locations";
            uiComboBoxItem5.FormatStyle.Alpha = 0;
            uiComboBoxItem5.IsSeparator = false;
            uiComboBoxItem5.Text = "ALL BIN";
            uiComboBoxItem5.Value = "ALL BIN";
            this.cbx_LoaiBin.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2,
            uiComboBoxItem3,
            uiComboBoxItem4,
            uiComboBoxItem5});
            this.cbx_LoaiBin.Location = new System.Drawing.Point(123, 11);
            this.cbx_LoaiBin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbx_LoaiBin.Name = "cbx_LoaiBin";
            this.cbx_LoaiBin.Size = new System.Drawing.Size(331, 27);
            this.cbx_LoaiBin.TabIndex = 64;
            this.cbx_LoaiBin.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cbx_LoaiBin.SelectedValueChanged += new System.EventHandler(this.cbx_LoaiBin_SelectedValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(18, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 19);
            this.label12.TabIndex = 65;
            this.label12.Text = "Tự phân bổ ";
            // 
            // txtconlai
            // 
            this.txtconlai.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtconlai.Location = new System.Drawing.Point(708, 131);
            this.txtconlai.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtconlai.Name = "txtconlai";
            this.txtconlai.ReadOnly = true;
            this.txtconlai.Size = new System.Drawing.Size(128, 27);
            this.txtconlai.TabIndex = 67;
            this.txtconlai.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // txtphanbo
            // 
            this.txtphanbo.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtphanbo.Location = new System.Drawing.Point(468, 131);
            this.txtphanbo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtphanbo.Name = "txtphanbo";
            this.txtphanbo.ReadOnly = true;
            this.txtphanbo.Size = new System.Drawing.Size(128, 27);
            this.txtphanbo.TabIndex = 68;
            this.txtphanbo.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            this.txtphanbo.TextChanged += new System.EventHandler(this.txtphanbo_TextChanged);
            // 
            // txtsoluong
            // 
            this.txtsoluong.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsoluong.Location = new System.Drawing.Point(123, 51);
            this.txtsoluong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtsoluong.Name = "txtsoluong";
            this.txtsoluong.ReadOnly = true;
            this.txtsoluong.Size = new System.Drawing.Size(128, 27);
            this.txtsoluong.TabIndex = 69;
            this.txtsoluong.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 19);
            this.label1.TabIndex = 70;
            this.label1.Text = "Số lượng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(297, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 19);
            this.label2.TabIndex = 71;
            this.label2.Text = "Số lượng đã phân bổ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(637, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 19);
            this.label3.TabIndex = 72;
            this.label3.Text = "Còn lại";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 19);
            this.label4.TabIndex = 74;
            this.label4.Text = "Mã hàng";
            // 
            // txtmahang
            // 
            this.txtmahang.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmahang.Location = new System.Drawing.Point(123, 90);
            this.txtmahang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtmahang.Name = "txtmahang";
            this.txtmahang.ReadOnly = true;
            this.txtmahang.Size = new System.Drawing.Size(128, 27);
            this.txtmahang.TabIndex = 73;
            this.txtmahang.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 19);
            this.label5.TabIndex = 76;
            this.label5.Text = "Mã kho";
            // 
            // txtmakho
            // 
            this.txtmakho.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmakho.Location = new System.Drawing.Point(124, 130);
            this.txtmakho.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtmakho.Name = "txtmakho";
            this.txtmakho.ReadOnly = true;
            this.txtmakho.Size = new System.Drawing.Size(128, 27);
            this.txtmakho.TabIndex = 75;
            this.txtmakho.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // gridBinLocation
            // 
            this.gridBinLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridBinLocation.ColumnAutoResize = true;
            gridBinLocation_DesignTimeLayout.LayoutString = resources.GetString("gridBinLocation_DesignTimeLayout.LayoutString");
            this.gridBinLocation.DesignTimeLayout = gridBinLocation_DesignTimeLayout;
            this.gridBinLocation.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.gridBinLocation.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.gridBinLocation.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.gridBinLocation.GroupByBoxVisible = false;
            this.gridBinLocation.Location = new System.Drawing.Point(12, 170);
            this.gridBinLocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridBinLocation.Name = "gridBinLocation";
            this.gridBinLocation.RecordNavigator = true;
            this.gridBinLocation.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
            this.gridBinLocation.Size = new System.Drawing.Size(978, 339);
            this.gridBinLocation.TabIndex = 77;
            this.gridBinLocation.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.gridBinLocation.UpdateMode = Janus.Windows.GridEX.UpdateMode.CellUpdate;
            this.gridBinLocation.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            this.gridBinLocation.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(this.gridBinLocation_CellValueChanged);
            this.gridBinLocation.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.gridBinLocation_FormattingRow);
            this.gridBinLocation.CellEdited += new Janus.Windows.GridEX.ColumnActionEventHandler(this.gridBinLocation_CellEdited);
            this.gridBinLocation.CellUpdated += new Janus.Windows.GridEX.ColumnActionEventHandler(this.gridBinLocation_CellUpdated);
            // 
            // BinWhsCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1002, 581);
            this.Controls.Add(this.gridBinLocation);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtmakho);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtmahang);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtsoluong);
            this.Controls.Add(this.txtphanbo);
            this.Controls.Add(this.txtconlai);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbx_LoaiBin);
            this.Controls.Add(this.btnCancel1);
            this.Controls.Add(this.btnOk1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BinWhsCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phân bổ vị trí";
            this.Load += new System.EventHandler(this.BinWhsCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridBinLocation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Janus.Windows.EditControls.UIButton btnCancel1;
        private Janus.Windows.EditControls.UIButton btnOk1;
        private Janus.Windows.EditControls.UIComboBox cbx_LoaiBin;
        private System.Windows.Forms.Label label12;
        private Janus.Windows.GridEX.EditControls.EditBox txtconlai;
        private Janus.Windows.GridEX.EditControls.EditBox txtphanbo;
        private Janus.Windows.GridEX.EditControls.EditBox txtsoluong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.EditControls.EditBox txtmahang;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.GridEX.EditControls.EditBox txtmakho;
        private Janus.Windows.GridEX.GridEX gridBinLocation;
    }
}