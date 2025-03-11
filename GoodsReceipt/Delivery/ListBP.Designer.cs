namespace Delivery
{
    partial class ListBP
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
            Janus.Windows.GridEX.GridEXLayout gridEXBP_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListBP));
            this.btnOK = new Janus.Windows.EditControls.UIButton();
            this.btnCancel = new Janus.Windows.EditControls.UIButton();
            this.gridEXBP = new Janus.Windows.GridEX.GridEX();
            ((System.ComponentModel.ISupportInitialize)(this.gridEXBP)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(14, 424);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(84, 36);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "Chọn";
            this.btnOK.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(105, 424);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 36);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // gridEXBP
            // 
            this.gridEXBP.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.gridEXBP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridEXBP.ColumnAutoResize = true;
            gridEXBP_DesignTimeLayout.LayoutString = resources.GetString("gridEXBP_DesignTimeLayout.LayoutString");
            this.gridEXBP.DesignTimeLayout = gridEXBP_DesignTimeLayout;
            this.gridEXBP.DynamicFiltering = true;
            this.gridEXBP.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.gridEXBP.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.gridEXBP.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.gridEXBP.GroupByBoxVisible = false;
            this.gridEXBP.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.gridEXBP.Location = new System.Drawing.Point(14, 14);
            this.gridEXBP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridEXBP.Name = "gridEXBP";
            this.gridEXBP.Size = new System.Drawing.Size(770, 403);
            this.gridEXBP.TabIndex = 2;
            this.gridEXBP.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            this.gridEXBP.DoubleClick += new System.EventHandler(this.gridEXBP_DoubleClick);
            // 
            // ListBP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(798, 474);
            this.Controls.Add(this.gridEXBP);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListBP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Danh sách NCC";
            ((System.ComponentModel.ISupportInitialize)(this.gridEXBP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton btnOK;
        private Janus.Windows.EditControls.UIButton btnCancel;
        private Janus.Windows.GridEX.GridEX gridEXBP;
    }
}