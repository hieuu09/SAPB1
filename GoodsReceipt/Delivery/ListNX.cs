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
    public partial class ListNX : Form
    {
        public delegate void PassControlNX(List<string> sender);

        // Create instance (null)
        public PassControlNX passControlNX;
        public ListNX()
        {
            InitializeComponent();
            LoadNX();
        }
        public void LoadNX()
        {
            try
            {
                SAPbouiCOM.DataTable oDataTable = Globals.GetSapDataTable("SELECT name from [@MANGKINHDOANH] order by name");
                DataTable dt = new DataTable();
                dt.Columns.Add("Lý do nhập/xuất");
                if (!oDataTable.IsEmpty)
                {
                    for (int i = 0; i < oDataTable.Rows.Count; i++)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr[0] = oDataTable.GetValue("name", i);
                        dt.Rows.Add(dr);
                    }

                }
                gridEXNX.SetDataBinding(dt, "");
                gridEXNX.RetrieveStructure();
            }
            catch { }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            List<string> nx = new List<string>();

            if (passControlNX != null)
            {
                if (gridEXNX.Row >= 0)
                {
                    nx.Add(gridEXNX.CurrentRow.Cells[0].Value.ToString());
                    passControlNX(nx);
                }
            }
        }

        private void gridEXNX_DoubleClick(object sender, EventArgs e)
        {
            if (gridEXNX.Row >= 0)
            {
                btnOk_Click(sender, e);
            }
            this.Dispose();
        }
    }
}
