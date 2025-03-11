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
    public partial class ListKHL : Form
    {
        public delegate void PassControlKHL(List<string> sender);
        // Create instance (null)
        public PassControlKHL passControlKHL;
        public ListKHL(string CardCode)
        {
            InitializeComponent();
            LoadKHL(CardCode);
        }
        public void LoadKHL(string CardCode)
        {
            try
            {
                SAPbouiCOM.DataTable oDataTable = Globals.GetSapDataTable("SELECT code, name FROM dbo.[@KHUVUCDONGHANG] WITH(NOLOCK) ORDER BY CODE");
                DataTable dt = new DataTable();
                dt.Columns.Add("Mã khu vực");
                dt.Columns.Add("Tên khu vực");
                if (!oDataTable.IsEmpty)
                {
                    for (int i = 0; i < oDataTable.Rows.Count; i++)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr[0] = oDataTable.GetValue("code", i);
                        dr[1] = oDataTable.GetValue("name", i);
                        dt.Rows.Add(dr);
                    }

                }
                gridEXKHL.SetDataBinding(dt, "");
                gridEXKHL.RetrieveStructure();
            }
            catch { }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            List<string> kh = new List<string>();

            if (passControlKHL != null)
            {
                if (gridEXKHL.Row >= 0)
                {
                    kh.Add(gridEXKHL.CurrentRow.Cells[0].Value.ToString());
                    kh.Add(gridEXKHL.CurrentRow.Cells[1].Value.ToString());
                    passControlKHL(kh);
                }
            }
        }

        private void gridEXKHL_DoubleClick(object sender, EventArgs e)
        {
            if (gridEXKHL.Row >= 0)
            {
                btnOk_Click(sender, e);
            }
            this.Dispose();
        }
    }
}
