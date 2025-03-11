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
    public partial class BinWhsCode : Form
    {
        public Headerbin HeaderBinData { get; private set; }
        public string lineidform1 { get; private set; }
        public double totalquantityall { get; private set; }
        public class Headerbin
        {
            public string LineId { get; set; }
            public ICollection<BinInfo> BinDetails { get; set; }
            public Headerbin()
            {
                BinDetails = new List<BinInfo>();
            }
        }
        public class BinInfo
        {
            public string CodeBin { get; set; }
            public double QuantityBin { get; set; }
        }

        public BinWhsCode(string soluong, string mahang, string makho, string lineid)
        {
            InitializeComponent();
            txtsoluong.Text = soluong;
            txtmahang.Text = mahang;
            txtmakho.Text = makho;
            lineidform1 = lineid;
            HeaderBinData = new Headerbin { LineId = lineid };
           
        }
        public BinWhsCode(string soluong, string mahang, string makho, string lineid, Headerbin existingHeaderbin)
        {
            InitializeComponent();
            txtsoluong.Text = soluong;
            txtmahang.Text = mahang;
            txtmakho.Text = makho;
            lineidform1 = lineid;

            HeaderBinData = existingHeaderbin;
            AllBinWHSCode(makho);
            DisplayExistingBinDetails(HeaderBinData);
        }

        private void DisplayExistingBinDetails(Headerbin headerBin)
        {

            if (headerBin != null && headerBin.BinDetails.Count > 0)
            {
                foreach (var bin in headerBin.BinDetails)
                {
                    string currentBinCode = "";
                    
                    foreach (Janus.Windows.GridEX.GridEXRow row in gridBinLocation.GetRows())
                    {
                        currentBinCode = row.Cells["Bin Location"].Value.ToString();

                        if (!string.IsNullOrEmpty(currentBinCode) && currentBinCode == bin.CodeBin)
                        {
                            row.Cells["Chọn"].Value = true;
                            row.Cells["Số lượng"].Value = bin.QuantityBin;
                            break;
                        }
                    }
                }
            }

        }

        private void BinWhsCode_Load(object sender, EventArgs e)
        {

        }

        private void btnOk1_Click(object sender, EventArgs e)
        {
            try
            {
                double totalQuantity = 0;

                Headerbin headerBin = new Headerbin
                { 
                    LineId = lineidform1 
                };
                
                foreach (Janus.Windows.GridEX.GridEXRow binRow in gridBinLocation.GetCheckedRows())
                {
                    if (binRow.Cells[0].Value.ToString().Equals("True"))
                    {
                        string binCode = binRow.Cells[1].Value.ToString(); 
                        double binQuantity = 0;

                        if (double.TryParse(binRow.Cells[2].Value?.ToString(), out binQuantity))
                        {
                            headerBin.BinDetails.Add(new BinInfo
                            {
                                CodeBin = binCode,
                                QuantityBin = binQuantity
                            });

                            totalQuantity += binQuantity;
                        }
                        else
                        {
                            MessageBox.Show($"Số lượng không hợp lệ cho BinCode: {binCode}");
                            return;
                        }
                    }
                }

                this.totalquantityall = totalQuantity;
                this.HeaderBinData = headerBin;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void cbx_LoaiBin_SelectedValueChanged(object sender, EventArgs e)
        {
            string mahang = txtmahang.Text;
            string makho = txtmakho.Text;

            if (cbx_LoaiBin.SelectedValue == "Default Bin Location")
            {
                DefaultBinLocation(makho);
            }

            if (cbx_LoaiBin.SelectedValue == "ALL BIN")
            {
                AllBinWHSCode(makho);
            }

            if (cbx_LoaiBin.SelectedValue == "Item's Current Bin Locations")
            {
                ItemCurrentBinLocation(makho, mahang);
            }

            if (cbx_LoaiBin.SelectedValue == "Item's Current and Historical Bin Locations")
            {
                ItemCurrentandHistoryBinLocation(makho, mahang);
            } 
            
            if (cbx_LoaiBin.SelectedValue == "Last Bin Location That Received Item")
            {
                LastBinLocationthatrecivedItem(makho, mahang);
            }
        }

        public void DefaultBinLocation(string makho)
        {
            try
            {
                SAPbouiCOM.DataTable oDataTable = Globals.GetSapDataTable("SELECT T2.BinCode, '' [Soluong] FROM OWHS T1 INNER JOIN OBIN T2 ON T1.WhsCode = T2.WhsCode WHERE T1.WhsCode ='" + makho + "' AND T1.DftBinAbs = T2.AbsEntry");
                DataTable dt = new DataTable();
                dt.Columns.Add("Bin Location", typeof(string));
                dt.Columns.Add("Số lượng", typeof(string));
                if (!oDataTable.IsEmpty)
                {
                    for (int i = 0; i < oDataTable.Rows.Count; i++)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr[0] = oDataTable.GetValue("BinCode", i);
                        dr[1] = oDataTable.GetValue("Soluong", i);
                        dt.Rows.Add(dr);
                    }

                }
                gridBinLocation.DataSource = dt;
               
            }
            catch { }

        }

        public void LastBinLocationthatrecivedItem(string makho, string mahang)
        {
            try
            {
                DataTable oDataTable = Globals.GetSapDataTableSQL("SELECT TOP 1 T1.MessageID, T1.ItemCode, T1.LocCode, T0.BinAbs, T2.BinCode, '' [Soluong] " +
                                                                          "FROM OBTL T0 INNER JOIN OILM T1 ON T0.MessageID = T1.MessageID " +
                                                                          "LEFT JOIN OBIN T2 ON T2.AbsEntry = T0.BinAbs WHERE T1.ItemCode = '" + mahang + "' AND T1.LocCode = '" + makho + "'" +
                                                                          "ORDER BY MessageID DESC");
                DataTable dt = new DataTable();
                dt.Columns.Add("Bin Location", typeof(string));
                dt.Columns.Add("Số lượng", typeof(string));
                if (oDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < oDataTable.Rows.Count; i++)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr[0] = oDataTable.Rows[i]["BinCode"];
                        dr[1] = oDataTable.Rows[i]["Soluong"];
                        dt.Rows.Add(dr);
                    }
                }
                gridBinLocation.DataSource = dt;
            }
            catch { }

        }
      
        public void ItemCurrentBinLocation( string makho, string mahang)
        {
            try
            {
                DataTable oDataTable = Globals.GetSapDataTableSQL("EXEC Usp_PL_ItemCurrentBinLocation '" + makho + "', '" + mahang + "'");
                DataTable dt = oDataTable.Copy();
                gridBinLocation.DataSource = dt;
                gridBinLocation.Refresh();
            }
            catch { }

        }

        public void AllBinWHSCode(string makho)
        {
            //try
            //{
            //    SAPbouiCOM.DataTable oDataTable = Globals.GetSapDataTable("SELECT BinCode, '' [Soluong] FROM OBIN T0 WHERE T0.WhsCode = '" + makho + "'");
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add("Bin Location", typeof(string));
            //    dt.Columns.Add("Số lượng", typeof(string));
            //    if (!oDataTable.IsEmpty)
            //    {
            //        for (int i = 0; i < oDataTable.Rows.Count; i++)
            //        {
            //            DataRow dr;
            //            dr = dt.NewRow();
            //            dr[0] = oDataTable.GetValue("BinCode", i);
            //            dr[1] = oDataTable.GetValue("Soluong", i);
            //            dt.Rows.Add(dr);
            //        }
            //    }
            //    gridBinLocation.DataSource = dt;
            //    gridBinLocation.Refresh();
            //}
            //catch { }


            try
            {
                DataTable oDataTable = Globals.GetSapDataTableSQL("SELECT BinCode [Bin Location], '' [Số lượng] FROM OBIN T0 WHERE T0.WhsCode = '" + makho + "'");
                DataTable dt = oDataTable.Copy();
                gridBinLocation.DataSource = dt;
                gridBinLocation.Refresh();
            }
            catch { }

        }

        public void ItemCurrentandHistoryBinLocation(string makho, string mahang)
        {
            try
            {
                DataTable oDataTable = Globals.GetSapDataTableSQL("EXEC Usp_PL_ItemCurrentandHistoryBinLocation '" + makho + "', '" + mahang + "'");
                DataTable dt = oDataTable.Copy();
                gridBinLocation.DataSource = dt;
                gridBinLocation.Refresh();
            }
            catch { }

        }

        private void gridBinLocation_CellValueChanged(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {

            if (e.Column.Position == 0)
            {
                try
                {
                    if (gridBinLocation.CurrentRow != null)
                    {
                        gridBinLocation.CurrentRow.EndEdit();
                        if (gridBinLocation.CurrentRow.Position < gridBinLocation.RowCount - 1)
                        {
                            gridBinLocation.MoveNext();
                        }
                        else
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                }
            }

        }

        private void gridBinLocation_CellEdited(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            if (e.Column.Position == 0)
            {
                try
                {
                    if (gridBinLocation.CurrentRow != null)
                    {
                        gridBinLocation.CurrentRow.EndEdit();
                        if (gridBinLocation.CurrentRow.Position < gridBinLocation.RowCount - 1)
                        {
                            gridBinLocation.MoveNext();
                        }
                        else
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                }
            }
          
            if (e.Column.Key == "Số lượng" || e.Column.Key == "Chọn")
            {
                UpdateAllocatedQuantity();
            }

        }

        private void gridBinLocation_CellUpdated(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            if (e.Column.Position == 0)
            {
                try
                {
                    if (gridBinLocation.CurrentRow != null)
                    {
                        gridBinLocation.CurrentRow.EndEdit();
                        if (gridBinLocation.CurrentRow.Position < gridBinLocation.RowCount - 1)
                        {
                            gridBinLocation.MoveNext();
                        }
                        else
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                }
            }
        }

        private void UpdateAllocatedQuantity()
        {
            decimal totalAllocated = 0;

            foreach (Janus.Windows.GridEX.GridEXRow row in gridBinLocation.GetCheckedRows())
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString().Equals("True", StringComparison.OrdinalIgnoreCase))
                {
                    decimal quantity = 0;

                    if (decimal.TryParse(row.Cells["Số lượng"].Value?.ToString(), out quantity))
                    {
                        totalAllocated += quantity; 
                    }
                }
            }

            txtphanbo.Text = totalAllocated.ToString();
        }

        private void txtphanbo_TextChanged(object sender, EventArgs e)
        {
            txtconlai.Text = (Convert.ToDouble(txtsoluong.Text) - Convert.ToDouble(txtphanbo.Text)).ToString();
        }

        private void gridBinLocation_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {
            var isChecked = e.Row.Cells["Chọn"].Value;

            if (isChecked == null || !(bool)isChecked)
            {
                e.Row.Cells["Số lượng"].Enabled = false;
            }
            else
            {
                e.Row.Cells["Số lượng"].Enabled = true;
            }
        }

    
    }
}
