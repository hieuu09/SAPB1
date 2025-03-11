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
    public partial class ListBarCode : Form
    {
        public delegate void PassControlBarCode(List<string> sender);
       
        public PassControlBarCode passControlbarCode;
        public ListBarCode(string mahang, string donvitinh)
        {
            InitializeComponent();
            LoadBarCode(mahang, donvitinh);

        }
        public void LoadBarCode(string mahang, string donvitinh)
        {
            try
            {
                SAPbouiCOM.DataTable oDataTable = Globals.GetSapDataTable("EXEC ChangeBarcode '"+ mahang + "', '"+ donvitinh + "'");
                DataTable dt = new DataTable();
                dt.Columns.Add("Mã BarCode");
                dt.Columns.Add("Mã Hàng");
                if (!oDataTable.IsEmpty)
                {
                    for (int i = 0; i < oDataTable.Rows.Count; i++)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr[0] = oDataTable.GetValue("BcdCode", i);
                        dr[1] = oDataTable.GetValue("ItemCode", i);
                        dt.Rows.Add(dr);
                    }

                }
                gridEXBarCode.SetDataBinding(dt, "");
                gridEXBarCode.RetrieveStructure();
            }
            catch { }

        }

        private void btnOkbarCode_Click(object sender, EventArgs e)
        {
            List<string> barcode = new List<string>();

            if (passControlbarCode != null)
            {
                if (gridEXBarCode.Row >= 0)
                {
                    barcode.Add(gridEXBarCode.CurrentRow.Cells[0].Value.ToString());
                    barcode.Add(gridEXBarCode.CurrentRow.Cells[1].Value.ToString());
                    passControlbarCode(barcode);
                }
            }
        }

        private void gridEXBarCode_DoubleClick(object sender, EventArgs e)
        {
            if (gridEXBarCode.Row >= 0)
            {
                btnOkbarCode_Click(sender, e);
                this.Dispose();
            }
        }
    }
}
