namespace Delivery
{
    partial class ListWhs
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
            Janus.Windows.GridEX.GridEXLayout gridEXKho_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListWhs));
            this.gridEXKho = new Janus.Windows.GridEX.GridEX();
            this.btnOk1 = new Janus.Windows.EditControls.UIButton();
            this.btnCancel1 = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridEXKho)).BeginInit();
            this.SuspendLayout();
            // 
            // gridEXKho
            // 
            this.gridEXKho.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.gridEXKho.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridEXKho.ColumnAutoResize = true;
            gridEXKho_DesignTimeLayout.LayoutString = resources.GetString("gridEXKho_DesignTimeLayout.LayoutString");
            this.gridEXKho.DesignTimeLayout = gridEXKho_DesignTimeLayout;
            this.gridEXKho.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.gridEXKho.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.gridEXKho.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.gridEXKho.GroupByBoxVisible = false;
            this.gridEXKho.Location = new System.Drawing.Point(12, 12);
            this.gridEXKho.Name = "gridEXKho";
            this.gridEXKho.Size = new System.Drawing.Size(661, 277);
            this.gridEXKho.TabIndex = 0;
            this.gridEXKho.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            this.gridEXKho.DoubleClick += new System.EventHandler(this.gridEXKho_DoubleClick);
            // 
            // btnOk1
            // 
            this.btnOk1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk1.Location = new System.Drawing.Point(13, 296);
            this.btnOk1.Name = "btnOk1";
            this.btnOk1.Size = new System.Drawing.Size(75, 34);
            this.btnOk1.TabIndex = 1;
            this.btnOk1.Text = "Chọn";
            this.btnOk1.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.btnOk1.Click += new System.EventHandler(this.btnOk1_Click);
            // 
            // btnCancel1
            // 
            this.btnCancel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel1.Location = new System.Drawing.Point(94, 296);
            this.btnCancel1.Name = "btnCancel1";
            this.btnCancel1.Size = new System.Drawing.Size(75, 34);
            this.btnCancel1.TabIndex = 2;
            this.btnCancel1.Text = "Hủy";
            this.btnCancel1.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // ListWhs
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(685, 337);
            this.Controls.Add(this.btnCancel1);
            this.Controls.Add(this.btnOk1);
            this.Controls.Add(this.gridEXKho);
            this.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListWhs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Danh sách kho";
            ((System.ComponentModel.ISupportInitialize)(this.gridEXKho)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.GridEX.GridEX gridEXKho;
        private Janus.Windows.EditControls.UIButton btnOk1;
        private Janus.Windows.EditControls.UIButton btnCancel1;
    }
}