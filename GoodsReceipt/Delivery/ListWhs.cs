using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Delivery
{
    public partial class ListWhs : Form
    {
        public delegate void PassControlWhs(List<string> sender);

        // Create instance (null)
        public PassControlWhs passControlWhs;
        public ListWhs(string makho)
        {
            InitializeComponent();
            LoadWhs(makho);
        }
        public void LoadWhs(string makho)
        {
            try
            {
                SAPbouiCOM.DataTable oDataTable = Globals.GetSapDataTable("select WhsCode, WhsName from OWHS WITH(NOLOCK) where InActive = 'N' AND ((WhsCode + WhsName) LIKE N'%" + makho+"%')");
                DataTable dt = new DataTable();
                dt.Columns.Add("Mã kho hàng");
                dt.Columns.Add("Tên kho hàng");
                if (!oDataTable.IsEmpty)
                {
                    for (int i = 0; i < oDataTable.Rows.Count; i++)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr[0] = oDataTable.GetValue("WhsCode", i);
                        dr[1] = oDataTable.GetValue("WhsName", i);
                        dt.Rows.Add(dr);
                    }

                }
                gridEXKho.SetDataBinding(dt, "");
                gridEXKho.RetrieveStructure();
            }
            catch { }

        }

        private void btnOk1_Click(object sender, EventArgs e)
        {
            List<string> whs = new List<string>();

            if (passControlWhs != null)
            {
                if (gridEXKho.Row >= 0)
                {
                    whs.Add(gridEXKho.CurrentRow.Cells[0].Value.ToString());
                    whs.Add(gridEXKho.CurrentRow.Cells[1].Value.ToString());
                    passControlWhs(whs);
                }
            }
        }

        private void gridEXKho_DoubleClick(object sender, EventArgs e)
        {
            if (gridEXKho.Row >= 0)
            {
                btnOk1_Click(sender, e);
                this.Dispose();
            }
        }
    }
}
