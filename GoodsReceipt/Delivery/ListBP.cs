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
    public partial class ListBP : Form
    {
        public delegate void PassControlBP(List<string> sender);

        // Create instance (null)
        public PassControlBP passControlBP;
        public ListBP(string user, string mancc)
        {
            InitializeComponent();
            LoadBP(user, mancc);
        }
        public void LoadBP(string user, string mancc)
        {
            try
            {
                DataTable oDataTable = Globals.GetSapDataTableSQL("select T0.CardCode , CardName from OCRD T0 where CardType = 'S'AND validFor ='Y' AND ((T0.CardCode + CardName) LIKE N'%" + mancc+"%')");
                DataTable dt = new DataTable();
                dt.Columns.Add("Mã Nhà cung cấp");
                dt.Columns.Add("Tên Nhà cung cấp");
                if (oDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < oDataTable.Rows.Count; i++)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr[0] = oDataTable.Rows[i]["CardCode"];
                        dr[1] = oDataTable.Rows[i]["CardName"];
                        dt.Rows.Add(dr);
                    }

                }
                gridEXBP.SetDataBinding(dt, "");
                gridEXBP.RetrieveStructure();
            }
            catch { }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<string> business = new List<string>();

            if (passControlBP != null)
            {
                if (gridEXBP.Row >= 0)
                {
                    business.Add(gridEXBP.CurrentRow.Cells[0].Value.ToString());
                    business.Add(gridEXBP.CurrentRow.Cells[1].Value.ToString());
                    passControlBP(business);
                }
            }
        }

        private void gridEXBP_DoubleClick(object sender, EventArgs e)
        {
            if (gridEXBP.Row >= 0)
            {
                btnOK_Click(sender, e);
            }
            this.Dispose();
        }
    }
}
