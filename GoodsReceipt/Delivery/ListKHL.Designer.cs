namespace Delivery
{
    partial class ListKHL
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
            Janus.Windows.GridEX.GridEXLayout gridEXKHL_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListKHL));
            this.gridEXKHL = new Janus.Windows.GridEX.GridEX();
            this.btnOk = new Janus.Windows.EditControls.UIButton();
            this.btnCancel = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridEXKHL)).BeginInit();
            this.SuspendLayout();
            // 
            // gridEXKHL
            // 
            this.gridEXKHL.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.gridEXKHL.ColumnAutoResize = true;
            gridEXKHL_DesignTimeLayout.LayoutString = resources.GetString("gridEXKHL_DesignTimeLayout.LayoutString");
            this.gridEXKHL.DesignTimeLayout = gridEXKHL_DesignTimeLayout;
            this.gridEXKHL.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.gridEXKHL.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.gridEXKHL.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.gridEXKHL.GroupByBoxVisible = false;
            this.gridEXKHL.Location = new System.Drawing.Point(15, 15);
            this.gridEXKHL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEXKHL.Name = "gridEXKHL";
            this.gridEXKHL.Size = new System.Drawing.Size(902, 379);
            this.gridEXKHL.TabIndex = 0;
            this.gridEXKHL.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            this.gridEXKHL.DoubleClick += new System.EventHandler(this.gridEXKHL_DoubleClick);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(15, 401);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(84, 40);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Chọn";
            this.btnOk.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(106, 401);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 40);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // ListKHL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(930, 456);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gridEXKHL);
            this.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListKHL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Danh sách khu vực đóng hàng";
            ((System.ComponentModel.ISupportInitialize)(this.gridEXKHL)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.GridEX.GridEX gridEXKHL;
        private Janus.Windows.EditControls.UIButton btnOk;
        private Janus.Windows.EditControls.UIButton btnCancel;
    }
}