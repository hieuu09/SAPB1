
namespace Delivery
{
    partial class ListBarCode
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
            Janus.Windows.GridEX.GridEXLayout gridEXBarCode_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListBarCode));
            this.gridEXBarCode = new Janus.Windows.GridEX.GridEX();
            this.btnCancelbarCode = new Janus.Windows.EditControls.UIButton();
            this.btnOkbarCode = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridEXBarCode)).BeginInit();
            this.SuspendLayout();
            // 
            // gridEXBarCode
            // 
            this.gridEXBarCode.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.gridEXBarCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridEXBarCode.ColumnAutoResize = true;
            gridEXBarCode_DesignTimeLayout.LayoutString = resources.GetString("gridEXBarCode_DesignTimeLayout.LayoutString");
            this.gridEXBarCode.DesignTimeLayout = gridEXBarCode_DesignTimeLayout;
            this.gridEXBarCode.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.gridEXBarCode.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.gridEXBarCode.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.gridEXBarCode.GroupByBoxVisible = false;
            this.gridEXBarCode.Location = new System.Drawing.Point(12, 12);
            this.gridEXBarCode.Name = "gridEXBarCode";
            this.gridEXBarCode.Size = new System.Drawing.Size(661, 277);
            this.gridEXBarCode.TabIndex = 1;
            this.gridEXBarCode.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            this.gridEXBarCode.DoubleClick += new System.EventHandler(this.gridEXBarCode_DoubleClick);
            // 
            // btnCancelbarCode
            // 
            this.btnCancelbarCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelbarCode.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelbarCode.Location = new System.Drawing.Point(119, 295);
            this.btnCancelbarCode.Name = "btnCancelbarCode";
            this.btnCancelbarCode.Size = new System.Drawing.Size(75, 34);
            this.btnCancelbarCode.TabIndex = 4;
            this.btnCancelbarCode.Text = "Hủy";
            this.btnCancelbarCode.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // btnOkbarCode
            // 
            this.btnOkbarCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOkbarCode.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOkbarCode.Location = new System.Drawing.Point(28, 295);
            this.btnOkbarCode.Name = "btnOkbarCode";
            this.btnOkbarCode.Size = new System.Drawing.Size(75, 34);
            this.btnOkbarCode.TabIndex = 3;
            this.btnOkbarCode.Text = "Chọn";
            this.btnOkbarCode.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.btnOkbarCode.Click += new System.EventHandler(this.btnOkbarCode_Click);
            // 
            // ListBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(685, 337);
            this.Controls.Add(this.btnCancelbarCode);
            this.Controls.Add(this.btnOkbarCode);
            this.Controls.Add(this.gridEXBarCode);
            this.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ListBarCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Danh sách Mã vạch";
            ((System.ComponentModel.ISupportInitialize)(this.gridEXBarCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.GridEX.GridEX gridEXBarCode;
        private Janus.Windows.EditControls.UIButton btnCancelbarCode;
        private Janus.Windows.EditControls.UIButton btnOkbarCode;
    }
}