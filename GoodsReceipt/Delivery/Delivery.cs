using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using Delivery.Model;
using Delivery;
using static Delivery.BinWhsCode;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;


namespace Delivery
{
    public partial class Delivery : System.Windows.Forms.Form
    {
        private SAPbouiCOM.Application SBO_Application;
        private SAPbobsCOM.Company oCompany;
        private List<Headerbin> headerBins = new List<Headerbin>();

        public Delivery(SAPbouiCOM.Application SBO_Application, SAPbobsCOM.Company oCompany)
        {
            this.SBO_Application = SBO_Application;
            this.oCompany = oCompany;
        }
       
        public Delivery()
        {

            InitializeComponent();
            string userCode = Globals.SapCompany.UserName;
            Loaitiente();
            taxdate.Value = DateTime.Today;
            Dateghino.Value = DateTime.Today;
            DateDKHV.Value = DateTime.Today;
            this.KeyDown += new KeyEventHandler(GoodReceipt_KeyDown);
            this.KeyPreview = true;

        }

        private void Delivery_Load(object sender, EventArgs e)
        {
            string userCode = Globals.SapCompany.UserName;
           // int usedid = Globals.SapCompany.UserSignature;
            // SetPermissions_Buttonlist(userCode);
            
            txtsophieu.KeyPress += new KeyPressEventHandler(txtsophieu_KeyPress);
            txttygia.KeyPress += new KeyPressEventHandler(txtsophieu_KeyPress);
            txtsongaydukien.KeyPress += new KeyPressEventHandler(txtsophieu_KeyPress);
        }

        private void txtsophieu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }
       
        private void GoodReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                txtsophieu.Text = "";
                editDC.Text = "";
                editKH.Text = "";
                editKHL.Text = "";
                editKho.Text = "";
                editMST.Text = "";
                txttygia.Text = "";
                cbxtien.Text = "VND";
                editGC.Text = "";
                txtsongaydukien.Text = "";

                gridEXInv.DataSource = null;
                gridEXDL.DataSource = null;
                gridEXInv.Refresh();
            }
        }

        private void SetPermissions_Buttonlist(string userCode)
        {
            try
            {
                SAPbouiCOM.DataTable oDataTable = Globals.GetSapDataTable("EXEC PermissionsButtonAddon '" + userCode + "'");

                if (oDataTable.Rows.Count > 0)
                {
                    btnnhap.Enabled = true;
                    SearchOPDNDraft.Enabled = false;
                    btnserch_import.Enabled = false;
                    btnsearchOPDN.Enabled = false;
                }
                else
                {
                    btnPORPDN.Enabled = false;
                    btnnhap.Enabled = false;
                    btnserch_import.Enabled = false;
                    btnsearchOPDN.Enabled = false;
                }

            }
            catch { }
        }

        private void OpenDelivery()
        {
            Thread sf = new Thread(new ThreadStart(callForm));
            sf.SetApartmentState(ApartmentState.STA);
            sf.Start();

        }
        
        private static void callForm()
        {
            System.Windows.Forms.Application.Run(new Delivery());
        }


        private void Loaitiente()
        {
            try
            {
                SAPbouiCOM.DataTable oDataTable = Globals.GetSapDataTable("SELECT trim(CurrCode) [BPLId], trim(CurrCode) [BPLName] FROM OCRN");
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("BPLId", typeof(string));
                dt.Columns.Add("BPLName", typeof(string));
                for (int i = 0; i < oDataTable.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    try
                    {
                        dr["BPLId"] = oDataTable.GetValue("BPLId", i).ToString().Trim();
                    }
                    catch (Exception ex)
                    {
                        dr["BPLId"] = "";
                        Console.WriteLine("Error parsing CurrCode: " + ex.Message);
                    }
                    try
                    {
                        dr["BPLName"] = oDataTable.GetValue("BPLName", i).ToString().Trim();
                    }
                    catch (Exception ex)
                    {
                        dr["BPLName"] = "";
                        Console.WriteLine("Error parsing CurrCode: " + ex.Message);
                    }

                    dt.Rows.Add(dr);
                }
                cbxtien.DisplayMember = "BPLName";
                cbxtien.ValueMember = "BPLId";
                cbxtien.DataSource = dt;
                cbxtien.Enabled = true;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["BPLName"].ToString() == "VND")
                    {
                        cbxtien.SelectedValue = row["BPLId"];
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in combobox method: " + ex.Message);
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu cho ComboBox: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadReserveInvoice()
        {
            try
            {
                int txtsophieuvl = int.Parse(txtsophieu.Text);

                SAPbouiCOM.DataTable oDataTable = Globals.GetSapDataTable("exec DHM_TO_PNK1 '" + editKH.Text + "', 'I'," + txtsophieuvl.ToString() + "");
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("Mã hàng hóa", typeof(String));
                dt.Columns.Add("Tên hàng hóa", typeof(String));
                dt.Columns.Add("Số lượng", typeof(Double));
                dt.Columns.Add("Số thùng", typeof(Double));
                dt.Columns.Add("Số Series", typeof(String));
                dt.Columns.Add("Số chứng từ", typeof(String));
                dt.Columns.Add("Ngày hóa đơn", typeof(String));
                dt.Columns.Add("Lý do nhập/xuất", typeof(String));
                dt.Columns.Add("Diễn giải", typeof(String));
                dt.Columns.Add("Tồn thực", typeof(Double));
                dt.Columns.Add("Exchange", typeof(Double));
                dt.Columns.Add("DocEntry", typeof(Int32));
                dt.Columns.Add("LineNum", typeof(Int32));
                dt.Columns.Add("ObjType", typeof(Int32));
                dt.Columns.Add("DocType", typeof(String));
                dt.Columns.Add("Khách hàng lẻ", typeof(String));
                dt.Columns.Add("KKC", typeof(String));
                dt.Columns.Add("Sort", typeof(String));
                dt.Columns.Add("KJ", typeof(String));
                dt.Columns.Add("Solo", typeof(String));
                dt.Columns.Add("quantitylo", typeof(Double));
                if (!oDataTable.IsEmpty)
                {
                    for (int i = 0; i < oDataTable.Rows.Count; i++)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr[0] = oDataTable.GetValue("ItemCode", i);
                        dr[1] = oDataTable.GetValue("Dscription", i);
                        dr[2] = oDataTable.GetValue("Quantity", i);
                        dr[3] = oDataTable.GetValue("Package", i);
                        dr[4] = oDataTable.GetValue("Series", i);
                        dr[5] = oDataTable.GetValue("DocNo", i);
                        dr[6] = oDataTable.GetValue("DocDate", i);
                        dr[7] = oDataTable.GetValue("DocEntry", i);
                        dr[8] = oDataTable.GetValue("Series", i);
                        dr[9] = oDataTable.GetValue("OnHand", i);
                        dr[10] = oDataTable.GetValue("ObjType", i);
                        dr[11] = oDataTable.GetValue("DocEntry", i);
                        dr[12] = oDataTable.GetValue("LineNum", i);
                        dr[13] = oDataTable.GetValue("ObjType", i);
                        dr[15] = oDataTable.GetValue("CardName", i);
                        dr[16] = oDataTable.GetValue("ItemCode", i);
                        dr[17] = oDataTable.GetValue("DocEntry", i);
                        dr[18] = oDataTable.GetValue("DocEntry", i);
                        dr[19] = oDataTable.GetValue("lo", i);
                        dr[20] = oDataTable.GetValue("Sllo", i);
                        dt.Rows.Add(dr);
                    }

                }
                gridEXInv.DataSource = dt;
            }
            catch (Exception ex) { }

        }

        public void Search_DHM_To_Import()
        {

            try
            {
                int txtsophieuvl = 0;

                if (string.IsNullOrEmpty(txtsophieu.Text))
                {
                    txtsophieuvl = 0;
                }
                else
                {
                    txtsophieuvl = int.Parse(txtsophieu.Text);
                }

                DataTable oDataTable = Globals.GetSapDataTableSQL("exec DHM_TO_PhieuNhapKhau '" + editKH.Text + "', 'I'," + txtsophieuvl.ToString() + "");
                System.Data.DataTable dt = new System.Data.DataTable();

                dt.Columns.Add("Mã hàng hóa", typeof(String));
                dt.Columns.Add("Mã phúc long", typeof(String));
                dt.Columns.Add("Tên hàng hóa", typeof(String));
                dt.Columns.Add("Số lượng", typeof(Double));
                dt.Columns.Add("Số lượng tổng", typeof(Double)); //price
                dt.Columns.Add("price", typeof(Double));
                dt.Columns.Add("DVT", typeof(String));
                dt.Columns.Add("Số chứng từ", typeof(Int32));
                dt.Columns.Add("Ngày hóa đơn", typeof(String));
                dt.Columns.Add("Diễn giải", typeof(String));
                dt.Columns.Add("DocEntry", typeof(Int32));
                dt.Columns.Add("LineNum", typeof(Int32));
                dt.Columns.Add("ObjType", typeof(Int32));
                dt.Columns.Add("BaseEntry", typeof(Int32));
                dt.Columns.Add("BaseLine", typeof(Int32));
                dt.Columns.Add("BaseType", typeof(Int32));
                dt.Columns.Add("MaBarCode", typeof(String));
                dt.Columns.Add("MaKH", typeof(String));
                dt.Columns.Add("TenKH", typeof(String)); // ngoai thua thi dc 
                dt.Columns.Add("Ngày thực nhập", typeof(String));
                dt.Columns.Add("Kho", typeof(String));
                dt.Columns.Add("Bin Kho", typeof(String));
                dt.Columns.Add("Số lượng khả dụng", typeof(Double));
                dt.Columns.Add("Loại chứng từ", typeof(string));

                if (oDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < oDataTable.Rows.Count; i++)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr[0] = oDataTable.Rows[i]["ItemCode"];
                        dr[1] = oDataTable.Rows[i]["U_MPL1"];
                        dr[2] = oDataTable.Rows[i]["Dscription"];
                        dr[3] = oDataTable.Rows[i]["QuantityRemain"];
                        dr[4] = oDataTable.Rows[i]["SLTongtemDHM"];
                        dr[5] = oDataTable.Rows[i]["Price"];
                        dr[6] = oDataTable.Rows[i]["UomCode"];
                        dr[7] = oDataTable.Rows[i]["DocNo"];
                        dr[8] = oDataTable.Rows[i]["DocDate"];
                        dr[9] = oDataTable.Rows[i]["UomCode"];
                        dr[10] = oDataTable.Rows[i]["DocEntry"];
                        dr[11] = oDataTable.Rows[i]["LineNum"];
                        dr[12] = oDataTable.Rows[i]["ObjType"];
                        dr[13] = oDataTable.Rows[i]["BaseEntry"];
                        dr[14] = oDataTable.Rows[i]["BaseLine"];
                        dr[15] = oDataTable.Rows[i]["BaseType"];
                        dr[16] = oDataTable.Rows[i]["Mabarcode"];
                        dr[17] = oDataTable.Rows[i]["CardCode"]; // sai thu tu dc  lay, lay so thu tu gan vơi
                        dr[18] = oDataTable.Rows[i]["CardName"];
                        dr[19] = oDataTable.Rows[i]["DocDate"];
                        dr[20] = oDataTable.Rows[i]["lo"];
                        dr[21] = oDataTable.Rows[i]["lo"];
                        dr[22] = oDataTable.Rows[i]["QuantityRemain"];
                        dr[23] = oDataTable.Rows[i]["Loaichungtu"];
                        dt.Rows.Add(dr);
                    }

                }
                gridEXInv.DataSource = null;
                gridEXInv.DataSource = dt;

                if (dt != null && dt.Rows.Count > 0  && txtsophieuvl > 0)
                {
                    //editTKH.Text = string.Empty; //editKH.Text = string.Empty; //docdate.Text = string.Empty; //taxdate.Text = string.Empty; //docduedate.Text = string.Empty; //txttygia.Text = string.Empty;
                    DataTable oDataTableheader = Globals.GetSapDataTableSQL("exec Usp_PL_HeaderPhieunhapkhau " + txtsophieuvl.ToString());

                    if (oDataTableheader.Rows.Count > 0)
                    {
                        string cardName = oDataTableheader.Rows[0]["CardName"].ToString();
                        string cardCode = oDataTableheader.Rows[0]["CardCode"].ToString();
                        string docdateDHM = oDataTableheader.Rows[0]["DocDate"].ToString();
                        string taxdateDHM = oDataTableheader.Rows[0]["TaxDate"].ToString();
                        string docduedateDHM = oDataTableheader.Rows[0]["DocDueDate"].ToString();
                        string doccur = oDataTableheader.Rows[0]["DocCur"].ToString();
                        string docrate = oDataTableheader.Rows[0]["DocRate"].ToString();

                        editTKH.Text = cardName;
                        editKH.Text = cardCode;

                        //if (DateTime.TryParse(docdateDHM, out DateTime docDate1))
                        //{
                        //    docdate.Text = docDate1.ToString("yyyy-MM-dd");
                        //}
                        //if (DateTime.TryParse(taxdateDHM, out DateTime taxdate1))
                        //{
                        //    taxdate.Text = taxdate1.ToString("yyyy-MM-dd");
                        //}
                        if (DateTime.TryParse(docduedateDHM, out DateTime docduedate1))
                        {
                            docduedate.Text = docduedate1.ToString("yyyy-MM-dd");
                        }
                        if (double.TryParse(docrate, out double docrate1))
                        {
                            txttygia.Text = docrate1.ToString();
                        }
                        LoaitienteNCC(doccur);

                    }
                    else { }
                }
                else
                {
                    docdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    taxdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    docduedate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    txtsongaydukien.Text = string.Empty;
                    txttygia.Text = string.Empty;
                    Loaitiente();
                }

            }
            catch (Exception ex) { }
        
        }


        private void LoaitienteNCC(string doccur)
        {
            try
            {
                SAPbouiCOM.DataTable oDataTable = Globals.GetSapDataTable("SELECT trim(CurrCode) AS BPLId, trim(CurrCode) AS BPLName FROM OCRN");
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("BPLId", typeof(string));
                dt.Columns.Add("BPLName", typeof(string));
                if (!string.IsNullOrEmpty(doccur))
                {
                    DataRow dr = dt.NewRow();
                    dr["BPLId"] = doccur;
                    dr["BPLName"] = doccur; 
                    dt.Rows.Add(dr);
                }
                for (int i = 0; i < oDataTable.Rows.Count; i++)
                {
                    string bplId = oDataTable.GetValue("BPLId", i)?.ToString().Trim() ?? "";
                    if (!dt.AsEnumerable().Any(row => row["BPLId"].ToString() == bplId)) // Tránh trùng lặp
                    {
                        DataRow dr = dt.NewRow();
                        dr["BPLId"] = bplId;
                        dr["BPLName"] = oDataTable.GetValue("BPLName", i)?.ToString().Trim() ?? "";
                        dt.Rows.Add(dr);
                    }
                }
                cbxtien.DisplayMember = "BPLName";
                cbxtien.ValueMember = "BPLId";
                cbxtien.DataSource = dt;
                cbxtien.Enabled = true;
                cbxtien.SelectedValue = doccur;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in combobox method: " + ex.Message);
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu cho ComboBox: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SearchImport_Goodreceipt()
        {
            try
            {
                int txtsophieuvl = 0;
                if (string.IsNullOrEmpty(txtsophieu.Text))
                {
                    txtsophieuvl = 0;
                }
                else
                {
                    txtsophieuvl = int.Parse(txtsophieu.Text);
                }

                
                DataTable oDataTable = Globals.GetSapDataTableSQL("exec ImportReceipt_To_GoodReceipt '" + editKH.Text + "', 'I'," + txtsophieuvl.ToString() + "");

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("Mã hàng hóa", typeof(String));
                dt.Columns.Add("Mã phúc long", typeof(String));
                dt.Columns.Add("Tên hàng hóa", typeof(String));
                dt.Columns.Add("Số lượng", typeof(Double));
                dt.Columns.Add("Số lượng tổng", typeof(Double)); //price
                dt.Columns.Add("price", typeof(Double));
                dt.Columns.Add("DVT", typeof(String));
                dt.Columns.Add("Số chứng từ", typeof(Int32));
                dt.Columns.Add("Ngày hóa đơn", typeof(String));
                dt.Columns.Add("Diễn giải", typeof(String));
                dt.Columns.Add("DocEntry", typeof(Int32));
                dt.Columns.Add("LineNum", typeof(Int32));
                dt.Columns.Add("ObjType", typeof(Int32));
                dt.Columns.Add("BaseEntry", typeof(Int32));
                dt.Columns.Add("BaseLine", typeof(Int32));
                dt.Columns.Add("BaseType", typeof(Int32));
                dt.Columns.Add("MaBarCode", typeof(String));
                dt.Columns.Add("MaKH", typeof(String));
                dt.Columns.Add("TenKH", typeof(String));
                dt.Columns.Add("Ngày thực nhập", typeof(String));
                dt.Columns.Add("Kho", typeof(String));
                dt.Columns.Add("Bin Kho", typeof(String));
                dt.Columns.Add("STT", typeof(String));
                dt.Columns.Add("Số lượng khả dụng", typeof(double));
                dt.Columns.Add("Loại chứng từ", typeof(string));

                if (oDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < oDataTable.Rows.Count; i++)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr[0] = oDataTable.Rows[i]["ItemCode"];
                        dr[1] = oDataTable.Rows[i]["U_MPL1"];
                        dr[2] = oDataTable.Rows[i]["Dscription"];
                        dr[3] = oDataTable.Rows[i]["QuantityRemainPNK"];
                        dr[4] = oDataTable.Rows[i]["QuantityPNK"];
                        dr[5] = oDataTable.Rows[i]["Price"];
                        dr[6] = oDataTable.Rows[i]["UomCode"];
                        dr[7] = oDataTable.Rows[i]["DocNo"];
                        dr[8] = oDataTable.Rows[i]["DocDate"];
                        dr[9] = oDataTable.Rows[i]["UomCode"];
                        dr[10] = oDataTable.Rows[i]["DocEntry"];
                        dr[11] = oDataTable.Rows[i]["LineNum"];
                        dr[12] = oDataTable.Rows[i]["ObjType"];
                        dr[13] = oDataTable.Rows[i]["BaseEntry"];
                        dr[14] = oDataTable.Rows[i]["BaseLine"];
                        dr[15] = oDataTable.Rows[i]["BaseType"];
                        dr[16] = oDataTable.Rows[i]["Mabacode"];
                        dr[17] = oDataTable.Rows[i]["CardCode"];
                        dr[18] = oDataTable.Rows[i]["CardName"];
                        dr[19] = oDataTable.Rows[i]["DocDate"];
                        dr[20] = oDataTable.Rows[i]["lo"];
                        dr[21] = oDataTable.Rows[i]["lo"];
                        dr[22] = oDataTable.Rows[i]["STT"];
                        dr[23] = oDataTable.Rows[i]["QuantityRemainPNK"];
                        dr[24] = oDataTable.Rows[i]["Loaichungtu"];
                        dt.Rows.Add(dr);
                    }

                }
                gridEXInv.DataSource = dt;
                headerBins.Clear();
                
                if (dt != null && dt.Rows.Count > 0 && txtsophieuvl > 0)
                {
                    //editTKH.Text = string.Empty; //editKH.Text = string.Empty; //docdate.Text = string.Empty; //taxdate.Text = string.Empty; //docduedate.Text = string.Empty; //txttygia.Text = string.Empty;
                    SAPbouiCOM.DataTable oDataTableheader = Globals.GetSapDataTable("exec [dbo].[Usp_PL_HeaderPhieunhapKho] " + txtsophieuvl.ToString());

                    if (oDataTableheader.Rows.Count > 0)
                    {
                        string cardName = oDataTableheader.GetValue("CardName", 0).ToString();
                        string cardCode = oDataTableheader.GetValue("CardCode", 0).ToString();
                        string docdateDHM = oDataTableheader.GetValue("DocDate", 0).ToString();
                        string taxdateDHM = oDataTableheader.GetValue("TaxDate", 0).ToString();
                        string docduedateDHM = oDataTableheader.GetValue("DocDueDate", 0).ToString();
                        string ngayghino = oDataTableheader.GetValue("U_NGN", 0)?.ToString()?? DateTime.Now.ToString("yyyy-MM-dd");
                        string ngaydkhangve = oDataTableheader.GetValue("U_NDKHV", 0)?.ToString() ?? DateTime.Now.ToString("yyyy-MM-dd");
                        string ngaydkthanhtoan = oDataTableheader.GetValue("U_NDKTT", 0)?.ToString() ?? DateTime.Now.ToString("yyyy-MM-dd");

                        string doccur = oDataTableheader.GetValue("DocCur", 0).ToString();
                        string docrate = oDataTableheader.GetValue("DocRate", 0).ToString();
                        string songayno = oDataTableheader.GetValue("U_SNN", 0)?.ToString() ?? "0";
                        string Lonhapkhau = oDataTableheader.GetValue("U_LNK", 0)?.ToString()??"";
                        string khuvucdonghang = oDataTableheader.GetValue("U_KVDH", 0)?.ToString() ?? "";
                        string nameKVDH = oDataTableheader.GetValue("NameKHDH", 0)?.ToString() ?? "";

                        editTKH.Text = cardName;
                        editKH.Text = cardCode;
                        editKHL.Text = khuvucdonghang;
                        editMST.Text = nameKVDH;

                        //if (DateTime.TryParse(docdateDHM, out DateTime docDate1))
                        //{
                        //    docdate.Text = docDate1.ToString("yyyy-MM-dd");
                        //}
                        //if (DateTime.TryParse(taxdateDHM, out DateTime taxdate1))
                        //{
                        //    taxdate.Text = taxdate1.ToString("yyyy-MM-dd");
                        //}
                        //if (DateTime.TryParse(docduedateDHM, out DateTime docduedate1))
                        //{
                        //    docduedate.Text = docduedate1.ToString("yyyy-MM-dd");
                        //}
                        if (DateTime.TryParse(ngayghino, out DateTime ngayghino11))
                        {
                            Dateghino.Text = ngayghino11.ToString("yyyy-MM-dd");
                        }
                        if (DateTime.TryParse(ngaydkhangve, out DateTime ngaydkhangve11))
                        {
                            DateDKHV.Text = ngaydkhangve11.ToString("yyyy-MM-dd");
                        }
                        if (DateTime.TryParse(ngaydkthanhtoan, out DateTime ngaydkthanhtoan11))
                        {
                            datedkthanhtoan.Text = ngaydkthanhtoan11.ToString("yyyy-MM-dd");
                        }

                        txtsongaydukien.Text = songayno;
                        editDC.Text = Lonhapkhau;

                        if (double.TryParse(docrate, out double docrate1))
                        {
                            txttygia.Text = docrate1.ToString();
                        }
                        LoaitienteNCC(doccur);

                    }
                    else { }
                }
                else
                {
                    docdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    taxdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    docduedate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    DateDKHV.Text = DateTime.Now.ToString("yyyy-MM-dd"); ;
                    datedkthanhtoan.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    Dateghino.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    txtsongaydukien.Text = string.Empty;
                    txttygia.Text = string.Empty;
                    Loaitiente();
                }

            }
            catch (Exception ex) { }
        }

        public void LoadDelivery(string DocEntry)
        {
            try
            {

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("DocEntry", typeof(String));

                DataRow dr;
                dr = dt.NewRow();
                dr[0] = DocEntry;
                dt.Rows.Add(dr);
                gridEXDL.DataSource = dt;

                //gridEXInv.SetDataBinding(dt, "");
                //gridEXInv.RetrieveStructure();
            }
            catch { }

        }

        private void PassBP(List<string> sender)
        {
            // Set de text of the textbox to the value of the textbox of form 2
            try
            {
                if (sender.Count > 0)
                {
                    editKH.Text = sender[0].ToString();
                    editTKH.Text = sender[1].ToString();
                }
            }
            catch { }
        }
      
        private void PassWhs(List<string> sender)
        {
            // Set de text of the textbox to the value of the textbox of form 2
            try
            {
                if (sender.Count > 0)
                {
                    editKho.Text = sender[0].ToString();
                }
            }
            catch { }
        }
      
        private void PassKHL(List<string> sender)
        {
            // Set de text of the textbox to the value of the textbox of form 2
            try
            {
                if (sender.Count > 0)
                {
                    editKHL.Text = sender[0].ToString();
                    editMST.Text = sender[1].ToString();
                }
            }
            catch { }
        }
       
        private void PassBarCode(List<string> sender)
        {
            try
            {
                if (sender.Count > 0)
                {
                    gridEXInv.CurrentRow.Cells[17].Value = sender[0].ToString();
                }
            }
            catch { }
        }

        private void PassWhsCodeLine(List<string> sender)
        {
            try
            {
                if (sender.Count > 0)
                {
                    gridEXInv.CurrentRow.Cells[21].Value = sender[0].ToString();
                }
            }
            catch { }
        }

        public void MenuEvent(SAPbouiCOM.MenuEvent pVal, bool BubbleEvent)
        {
            if (pVal.BeforeAction == false)
            {
                switch (pVal.MenuUID)
                {
                    case "Delivery": OpenDelivery(); break;

                }
            }
        }

        private void editKho_ButtonClick(object sender, EventArgs e)
        {
            string makhovan = editKho.Text;
            ListWhs whs = new ListWhs(makhovan);
            whs.passControlWhs = new ListWhs.PassControlWhs(PassWhs);
            if (whs.ShowDialog(this) == DialogResult.OK)
            {
                whs.passControlWhs = new ListWhs.PassControlWhs(PassWhs);
            }
            else
            {

            }
            whs.Dispose();
        }
       
        private void editKH_ButtonClick(object sender, EventArgs e)
        {
            string user = Globals.SapCompany.UserName;
            string manccc = editKH.Text;
            ListBP bp = new ListBP(user, manccc);
            bp.passControlBP = new ListBP.PassControlBP(PassBP);
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (bp.ShowDialog(this) == DialogResult.OK)
            {
                bp.passControlBP = new ListBP.PassControlBP(PassBP);
            }
            else
            {

            }
            bp.Dispose();
        }
       
        private void editKKK_ButtonClick(object sender, EventArgs e)
        {
            string user = Globals.SapCompany.UserName;
            string manccform = editKH.Text;
            ListBP bp = new ListBP(user, manccform);
            bp.passControlBP = new ListBP.PassControlBP(PassBP);
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (bp.ShowDialog(this) == DialogResult.OK)
            {
                bp.passControlBP = new ListBP.PassControlBP(PassBP);
            }
            else
            {

            }
            bp.Dispose();
        }
       
        private void editKHL_ButtonClick(object sender, EventArgs e)
        {
            ListKHL kh = new ListKHL(editKH.Text);
            kh.passControlKHL = new ListKHL.PassControlKHL(PassKHL);
            if (kh.ShowDialog(this) == DialogResult.OK)
            {
                kh.passControlKHL = new ListKHL.PassControlKHL(PassKHL);
            }
            else
            {

            }
            kh.Dispose();
        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            LoadReserveInvoice();
            //LoadReserveInvoice1();
        }

        private void gridEXInv_CellEdited(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            if (e.Column.Position == 0)
            {
               

                try
                {
                    if (gridEXInv.CurrentRow != null)
                    {
                        gridEXInv.CurrentRow.EndEdit();
                        if (gridEXInv.CurrentRow.Position < gridEXInv.RowCount - 1)
                        {
                            gridEXInv.MoveNext();
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

        private void gridEXInv_CellValueChanged(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            if (e.Column.Position == 0)
            {
                string check = gridEXInv.CurrentRow.Cells[0].Value.ToString();
                if (check == "True")
                {
                    gridEXInv.CurrentRow.Cells[20].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    gridEXInv.Refresh();
                    gridEXInv.CurrentRow.EndEdit();
                    gridEXInv.MoveFirst();
                }
                else { gridEXInv.CurrentRow.Cells[20].Value = ""; }
                try
                {
                    gridEXInv.Refresh();
                    gridEXInv.CurrentRow.EndEdit();
                    gridEXInv.MoveFirst();
                }
                catch { }

            }


            if (e.Column.Position == 0)
            {
                try
                {
                    if (gridEXInv.CurrentRow != null)
                    {
                        gridEXInv.CurrentRow.EndEdit();
                        if (gridEXInv.CurrentRow.Position < gridEXInv.RowCount - 1)
                        {
                            gridEXInv.MoveNext();
                        }
                        else { }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                }

            }

        }

        private void gridEXInv_CellUpdated(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
           
            if (e.Column.Position == 0)
            {
                try
                {
                    if (gridEXInv.CurrentRow != null)
                    {
                        gridEXInv.CurrentRow.EndEdit();
                        if (gridEXInv.CurrentRow.Position < gridEXInv.RowCount - 1)
                        {
                            gridEXInv.MoveNext();
                        }
                        else { }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                }

            }
        }

        private void uiButton4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn tạo phiếu nhập kho?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                CreateDLN();
            }
            // CreateDLN();
        }

        private void CreateDLN()
        {
            string CardCode, ReasonNX, Customer, TaxCode, Address, Branch, Remark, Warehouse, Print, TypeExport, Comments, loaitien;
            string BatchNumber = "";
            double tygia;
            double BatchQuantity = 0;
            DateTime DocDate, TaxDate, DocDueDate;
            try
            {
                CardCode = editKH.Text;
                Customer = editKHL.Text;
                TaxCode = editMST.Text;
                Address = editDC.Text;
                Comments = editGC.Text;
                loaitien = cbxtien.Text;
                if (string.IsNullOrEmpty(txttygia.Text)) { tygia = 1; }
                else { tygia = Convert.ToDouble(txttygia.Text); }

                //  try { Branch = cbbCN.SelectedValue.ToString(); } catch { Branch = ""; }
                Remark = editGC.Text;
                Warehouse = editKho.Text;

                DocDate = taxdate.Value;
                TaxDate = DateDKHV.Value;
                DocDueDate = Dateghino.Value;
                if (CardCode.Equals(""))
                {
                    MessageBox.Show("Hãy nhập mã khách hàng."); return;
                }
                if (Warehouse.Equals(""))
                {
                    MessageBox.Show("Hãy nhập kho nhập hàng."); return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString()); return;
            }

            string Series, DocNo;
            SAPbobsCOM.Documents dl = default(SAPbobsCOM.Documents);
            dl = (SAPbobsCOM.Documents)Globals.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes);
            dl.DocDate = DateTime.ParseExact(DocDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);
            dl.DocDueDate = DateTime.ParseExact(DocDueDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);
            dl.TaxDate = DateTime.ParseExact(TaxDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);
            dl.CardCode = CardCode;
            dl.Comments = Comments;
            if (loaitien != "VND")
            {
                dl.DocCurrency = loaitien;
                dl.DocRate = tygia;
            }
            else { }
            // dl.BPL_IDAssignedToInvoice = Int32.Parse(Branch);
            //// dl.UserFields.Fields.Item("U_category").Value = ReasonNX;
            // dl.UserFields.Fields.Item("U_KH").Value = Customer;
            // dl.UserFields.Fields.Item("U_MST").Value = TaxCode;
            // dl.UserFields.Fields.Item("U_Address").Value = Address;
            // dl.UserFields.Fields.Item("U_ITKH").Value = Print;
            // dl.UserFields.Fields.Item("U_KX").Value = TypeExport;
            foreach (Janus.Windows.GridEX.GridEXRow CheckItem in gridEXInv.GetCheckedRows())
            {
                if (CheckItem.Cells[0].Value.ToString().Equals("True"))
                {
                    try { Series = CheckItem.Cells[6].Value.ToString(); }
                    catch { Series = ""; }
                    //dl.Lines.UserFields.Fields.Item("U_Tenvattu").Value = Series;
                    try { DocNo = CheckItem.Cells[7].Value.ToString(); }
                    catch { DocNo = ""; }
                    //dl.Lines.UserFields.Fields.Item("U_Tenvattu").Value = DocNo;
                    dl.Lines.ItemCode = CheckItem.Cells[1].Value.ToString();
                    dl.Lines.Quantity = Double.Parse(CheckItem.Cells[3].Value.ToString());
                    dl.Lines.Price = Double.Parse(CheckItem.Cells[3].Value.ToString());
                    dl.Lines.BaseEntry = Int32.Parse(CheckItem.Cells[12].Value.ToString());
                    dl.Lines.BaseLine = Int32.Parse(CheckItem.Cells[13].Value.ToString());
                    dl.Lines.BaseType = Int32.Parse(CheckItem.Cells[14].Value.ToString());
                    dl.Lines.WarehouseCode = Warehouse;
                    // dl.Lines.UserFields.Fields.Item("U_Docnum").Value = DocNo;
                    // dl.Lines.UserFields.Fields.Item("U_series").Value = Series;

                    try
                    {
                        bool isBatchManaged = CheckIfItemIsBatchManaged(CheckItem.Cells[1].Value.ToString());

                        if (isBatchManaged)
                        {
                            if (CheckItem != null && CheckItem.Cells.Count > 20 && CheckItem.Cells[20].Value != null)
                            {
                                BatchNumber = CheckItem.Cells[20].Value.ToString();
                            }

                            if (CheckItem != null && CheckItem.Cells.Count > 21 && CheckItem.Cells[21].Value != null &&
                                !string.IsNullOrEmpty(CheckItem.Cells[21].Value.ToString()))
                            {
                                BatchQuantity = Double.Parse(CheckItem.Cells[21].Value.ToString()); // Giả sử ô thứ 21 chứa số lượng lô
                            }

                            if (string.IsNullOrEmpty(BatchNumber))
                            {
                                MessageBox.Show($"Mặt hàng {CheckItem.Cells[1].Value.ToString()} này được quản lý theo lô. Vui lòng nhập mã lô.",
                                                "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (!string.IsNullOrEmpty(BatchNumber) && BatchQuantity > 0)
                            {
                                if (BatchQuantity == dl.Lines.Quantity)
                                {
                                    dl.Lines.BatchNumbers.BatchNumber = BatchNumber;
                                    dl.Lines.BatchNumbers.Quantity = BatchQuantity;
                                    dl.Lines.BatchNumbers.Add(); // Thêm lô vào danh sách lô của dòng hiện tại
                                }
                                else
                                {
                                    MessageBox.Show($"Tổng số lượng của lô {CheckItem.Cells[1].Value.ToString()} phải bằng với số lượng của dòng. Vui lòng kiểm tra và nhập lại.",
                                                    "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi thêm lô: " + ex.Message);
                    }

                    dl.Lines.Add();
                }
            }
            try
            {
                dl.Add();
            }
            catch (Exception ex) { }
            int Messagecode;
            string Messagename;
            string DocEntry = "";
            Globals.SapCompany.GetLastError(out Messagecode, out Messagename);
            if (Messagecode != 0)
            {
                MessageBox.Show(Messagename);
            }
            else
            {
                DocEntry = Globals.SapCompany.GetNewObjectKey();
                LoadReserveInvoice();
                LoadDelivery(DocEntry);
            }
        }

        private bool CheckIfItemIsBatchManaged(string itemCode)
        {
            bool isBatchManaged = false;
            SAPbobsCOM.Recordset recordset = null;
            try
            {
                recordset = (SAPbobsCOM.Recordset)Globals.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string query = $"SELECT ManBtchNum FROM OITM WHERE ItemCode = '{itemCode}'";
                recordset.DoQuery(query);

                if (!recordset.EoF)
                {
                    var batchManagedField = recordset.Fields.Item("ManBtchNum").Value;

                    if (batchManagedField != null && batchManagedField.ToString().Equals("Y", StringComparison.OrdinalIgnoreCase))
                    {
                        isBatchManaged = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kiểm tra thông tin lô từ OITM cho mặt hàng {itemCode}: {ex.Message}");
            }
            finally
            {
                if (recordset != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(recordset);
                    recordset = null;
                }
            }
            return isBatchManaged;
        }

        private void gridEX2_ColumnButtonClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            string DocEntry = ""; // mục đihc là khi click vào phiếu thì truy cập vào phiếu nhập kho
            try
            {
                DocEntry = gridEXDL.CurrentRow.Cells[0].Value.ToString();
                Globals.SapApplication.OpenForm((BoFormObjectEnum)20, null, DocEntry);
            }
            catch (Exception ex)
            {

            }
        }

        private void gridEXInv_EditingCell(object sender, Janus.Windows.GridEX.EditingCellEventArgs e)
        {
            //if (e.Column.Position == 17)
            //{
            //    try
            //    {
            //        var mahang = gridEXInv.CurrentRow.Cells[1].Value.ToString();
            //        var donvitinh = gridEXInv.CurrentRow.Cells[7].Value.ToString();

            //        ListBarCode br = new ListBarCode(mahang, donvitinh);
            //        br.passControlbarCode = new ListBarCode.PassControlBarCode(PassBarCode);
            //        if (br.ShowDialog(this) == DialogResult.OK)
            //        {
            //            br.passControlbarCode = new ListBarCode.PassControlBarCode(PassBarCode);
            //        }
            //        else { }
            //        br.Dispose();
            //    }
            //    catch { }
            //}

        }

        private void dateNN_ValueChanged(object sender, EventArgs e)
        {
            //DateGH.Text = taxdate.Text;
            //Dateghino.Text = taxdate.Text;
        }

        private void Create_Imported()
        {
            string CardCode, lonhapkhau, Customer, TaxCode, Address, Remark, Warehouse, Comments, loaitien, khuvucdonghang;
            string ngaydukienhangve, ngayghino, ngaydukienthanhtoan;
            int songayno;
            double tygia;
            DateTime DocDate, TaxDate, DocDueDate;
            int usersign = Globals.SapCompany.UserSignature;
            try
            {
                CardCode = editKH.Text;
                Customer = editKHL.Text;
                TaxCode = editMST.Text;
                Address = editDC.Text;
                Comments = editGC.Text;
                loaitien = cbxtien.Text;
                if (string.IsNullOrEmpty(txttygia.Text)) { tygia = 1; }
                else { tygia = Convert.ToDouble(txttygia.Text); }

                Remark = editGC.Text;
                Warehouse = editKho.Text;
                lonhapkhau = editDC.Text;
                khuvucdonghang = editKHL.Text;

                if (string.IsNullOrEmpty(txtsongaydukien.Text))
                {
                    songayno = 0;
                }
                else { songayno = int.Parse(txtsongaydukien.Text); }
               // songayno = txtsongaydukien.Text;

                ngaydukienhangve = DateTime.ParseExact(DateDKHV.Text, "dd/MM/yyyy", null).ToString("yyyy-MM-dd");
                ngayghino = DateTime.ParseExact(Dateghino.Text, "dd/MM/yyyy", null).ToString("yyyy-MM-dd");
                ngaydukienthanhtoan = DateTime.ParseExact(datedkthanhtoan.Text, "dd/MM/yyyy", null).ToString("yyyy-MM-dd");

                DocDate = docdate.Value;
                TaxDate = taxdate.Value;
                DocDueDate = docduedate.Value;

                if (CardCode.Equals(""))
                {
                    MessageBox.Show("Hãy nhập mã nhà cung cấp."); return;
                }
                if (Warehouse.Equals(""))
                {
                    MessageBox.Show("Hãy nhập kho nhập hàng."); return;
                }
                if (lonhapkhau.Equals(""))
                {
                    MessageBox.Show("Hãy nhập lô nhập khẩu"); return;
                }
                if (khuvucdonghang.Equals(""))
                {
                    MessageBox.Show("Hãy nhập khu vực đóng hàng"); return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString()); return;
            }

            string Series, DocNo;

            HashSet<string> selectedDocEntries = new HashSet<string>();
            HashSet<string> SelectObject = new HashSet<string>();

            foreach (Janus.Windows.GridEX.GridEXRow CheckItem in gridEXInv.GetCheckedRows())
            {
                if (CheckItem.Cells[0].Value.ToString().Equals("True"))
                {
                    string docEntry = CheckItem.Cells[11].Value.ToString();
                    string objtecsap = CheckItem.Cells[13].Value.ToString();
                    selectedDocEntries.Add(docEntry);
                    SelectObject.Add(objtecsap);
                }
            }

            if (selectedDocEntries.Count > 1)
            {
                MessageBox.Show("Bạn không thể lưa chọn nhiều đơn hàng mua để lên 1 phiếu nhập khẩu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!SelectObject.Contains("22"))
            {
                MessageBox.Show("Tạo phiếu nhập khẩu phải dựa trên đơn hàng mua", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            foreach (Janus.Windows.GridEX.GridEXRow CheckItem in gridEXInv.GetCheckedRows())
            {
                if (CheckItem.Cells[0].Value.ToString().Equals("True"))
                {
                    string mahang = CheckItem.Cells[1].Value.ToString();
                    double slnhap = Double.Parse(CheckItem.Cells[4].Value.ToString());
                    double slkhadung = Double.Parse(CheckItem.Cells[24].Value.ToString());
                    if (slnhap > slkhadung)
                    {
                        int rowIndex = CheckItem.RowIndex + 1;

                        MessageBox.Show($"Mã hàng {mahang} dòng số {rowIndex} số lượng thực nhập không được lơn hơn số lượng cho phép", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }


            string connectionString = ConfigurationManager.ConnectionStrings["KIU887bbbbbb"].ConnectionString; ;

            using (SqlConnection connzzzz = new SqlConnection(connectionString))
            {
                connzzzz.Open();

                using (SqlTransaction transaction = connzzzz.BeginTransaction())
                {
                    try
                    {
                        string sqlHeader = @"
                    INSERT INTO Import_OPNK (DocDate, DocDueDate, TaxDate, CardCode, Comments, U_LNK, U_NDKHV, U_NGN, U_NDKTT, U_SNN, U_KVDH, DocCurrency, DocRate, UserSign, TimeNK, objtype)
                    OUTPUT INSERTED.DocEntry VALUES (@DocDate, @DocDueDate, @TaxDate, @CardCode, @Comments, @U_LNK, @U_NDKHV, @U_NGN, @U_NDKTT, @U_SNN, @U_KVDH, @DocCurrency, @DocRate, @UserSign, @TimeNK, @objtype)";

                        SqlCommand cmdHeader = new SqlCommand(sqlHeader, connzzzz, transaction);

                        cmdHeader.Parameters.AddWithValue("@DocDate", DocDate);
                        cmdHeader.Parameters.AddWithValue("@DocDueDate", DocDueDate);
                        cmdHeader.Parameters.AddWithValue("@TaxDate", TaxDate);
                        cmdHeader.Parameters.AddWithValue("@CardCode", CardCode);
                        cmdHeader.Parameters.AddWithValue("@Comments", Comments);
                        cmdHeader.Parameters.AddWithValue("@U_LNK", lonhapkhau);
                        cmdHeader.Parameters.AddWithValue("@U_NDKHV", ngaydukienhangve);
                        cmdHeader.Parameters.AddWithValue("@U_NGN", ngayghino);
                        cmdHeader.Parameters.AddWithValue("@U_NDKTT", ngaydukienthanhtoan);
                        cmdHeader.Parameters.AddWithValue("@U_SNN", songayno);
                        cmdHeader.Parameters.AddWithValue("@U_KVDH", khuvucdonghang);
                        cmdHeader.Parameters.AddWithValue("@DocCurrency", loaitien);
                        cmdHeader.Parameters.AddWithValue("@DocRate", tygia); 
                        cmdHeader.Parameters.AddWithValue("@UserSign", usersign);
                        cmdHeader.Parameters.AddWithValue("@TimeNK", DateTime.Now);
                        cmdHeader.Parameters.AddWithValue("@objtype", 2025);

                        int docEntry = (int)cmdHeader.ExecuteScalar();

                        int lineID = 1; 
                        foreach (Janus.Windows.GridEX.GridEXRow CheckItem in gridEXInv.GetCheckedRows())
                        {
                            if (CheckItem.Cells[0].Value.ToString().Equals("True"))
                            {
                                string sqlLine = @"
                                INSERT INTO Import_PNK1 (DocEntry, LineID, ItemCode, Quantity, Price, BaseEntry, BaseLine, BaseType, WarehouseCode, U_MPL1, UoMEntry, BarCode, TimeNK)
                                VALUES (@DocEntry, @LineID, @ItemCode, @Quantity, @Price, @BaseEntry, @BaseLine, @BaseType, @WarehouseCode, @U_MPL1, @UoMEntry, @BarCode, @TimeNK)";

                                SqlCommand cmdLine = new SqlCommand(sqlLine, connzzzz, transaction);

                                // Gán tham số cho LineTable
                                cmdLine.Parameters.AddWithValue("@DocEntry", docEntry);
                                cmdLine.Parameters.AddWithValue("@LineID", lineID++); 
                                cmdLine.Parameters.AddWithValue("@ItemCode", CheckItem.Cells[1].Value.ToString());
                                cmdLine.Parameters.AddWithValue("@Quantity", Double.Parse(CheckItem.Cells[4].Value.ToString()));
                                cmdLine.Parameters.AddWithValue("@Price", Double.Parse(CheckItem.Cells[6].Value.ToString()));
                                cmdLine.Parameters.AddWithValue("@BaseEntry", Int32.Parse(CheckItem.Cells[11].Value.ToString()));
                                cmdLine.Parameters.AddWithValue("@BaseLine", Int32.Parse(CheckItem.Cells[12].Value.ToString()));
                                cmdLine.Parameters.AddWithValue("@BaseType", Int32.Parse(CheckItem.Cells[13].Value.ToString()));
                                cmdLine.Parameters.AddWithValue("@WarehouseCode", Warehouse);
                                cmdLine.Parameters.AddWithValue("@U_MPL1", CheckItem.Cells[2].Value.ToString());
                                cmdLine.Parameters.AddWithValue("@UoMEntry", GetUomEntry(CheckItem.Cells[7].Value.ToString()));
                                cmdLine.Parameters.AddWithValue("@BarCode", CheckItem.Cells.Count > 17 ? CheckItem.Cells[17].Value?.ToString() ?? string.Empty : string.Empty);
                                //cmdLine.Parameters.AddWithValue("@TimeNK", CheckItem.Cells[20].Value.ToString());
                                cmdLine.Parameters.AddWithValue("@TimeNK", DateTime.Now);

                                cmdLine.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show($"Phiếu nhập khẩu đã được tạo thành công số phiếu: {docEntry}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Search_DHM_To_Import();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Lỗi khi thêm dữ liệu nhập khẩu: " + ex.Message);
                    }
                }
            }

            
        }

        public List<OPDN> ParseBinLocations(string binKhoInput)
        {
            var allocations = new List<OPDN>();
            var binEntries = binKhoInput.Split(',');

            foreach (var entry in binEntries)
            {
                var parts = entry.Split(':');
                if (parts.Length == 2)
                {
                    allocations.Add(new OPDN
                    {
                        BinLocation = parts[0].Trim(),
                        SoLuong = Double.Parse(parts[1].Trim())
                    });
                }
            }

            return allocations;
        }

        private int GetBinAbsEntry(string binLocation)
        {
            DataTable dt = Globals.GetSapDataTableSQL($"SELECT AbsEntry FROM OBIN WHERE BinCode = N'{binLocation}'");

            if (dt.Rows.Count > 0 && dt.Columns.Contains("AbsEntry"))
            {
                return int.Parse(dt.Rows[0]["AbsEntry"].ToString());
            }
            else
            {
                Globals.SapApplication.StatusBar.SetText($"Không tìm thấy bin trên'{binLocation}'", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return 0;
            }
        }

        private int GetUomEntry(string UomCode)
        {
            DataTable dtcode = Globals.GetSapDataTableSQL($"SELECT T0.UomEntry FROM OUOM T0 WHERE T0.UomCode = N'{UomCode}'");

            if (dtcode.Rows.Count > 0 && dtcode.Columns.Contains("UomEntry"))
            {
                return int.Parse(dtcode.Rows[0]["UomEntry"].ToString());
            }
            else
            {
                Globals.SapApplication.StatusBar.SetText($"Không tìm thấy UomEntry trên'{UomCode}'", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return 0;
            }
        }
      
        private void btnnhap_Click(object sender, EventArgs e)
        {
            bool hasSelectedItem = false;
            foreach (Janus.Windows.GridEX.GridEXRow row in gridEXInv.GetCheckedRows())
            {
                if (row.Cells[0].Value.ToString().Equals("True"))
                {
                    hasSelectedItem = true;
                    break;
                }
            }

            if (!hasSelectedItem)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một mặt hàng để tạo phiếu nhập khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn tạo phiếu nhập khẩu?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Create_Imported();
            }
        }

        private void SearchOPDNDraft_Click(object sender, EventArgs e)
        {
            Search_DHM_To_Import();
        }

        private void Create_Import_To_GoodsReceipt()
        {
            string CardCode, Customer, TaxCode, Address, Remark, Warehouse, Comments, loaitien;
            string ngaydukienhangve, ngayghino, ngaydukienthanhtoan, songayno, lonhapkhau, KhuVucDH;
            double tygia;
            int sophieunhap = 0;
            DateTime DocDate, TaxDate, DocDueDate;
            string sochung = "";

            try
            {
                CardCode = editKH.Text;
                Customer = editKHL.Text;
                TaxCode = editMST.Text;
                Address = editDC.Text;
                Comments = editGC.Text;
                loaitien = cbxtien.Text;
                Remark = editGC.Text;
                Warehouse = editKho.Text;
                DocDate = docdate.Value;
                TaxDate = taxdate.Value;
                DocDueDate = docduedate.Value;
                lonhapkhau = editDC.Text;
                KhuVucDH = editKHL.Text;

                ngaydukienhangve = DateTime.ParseExact(DateDKHV.Text, "dd/MM/yyyy", null).ToString("yyyy-MM-dd");
                ngayghino = DateTime.ParseExact(Dateghino.Text, "dd/MM/yyyy", null).ToString("yyyy-MM-dd");
                ngaydukienthanhtoan = DateTime.ParseExact(datedkthanhtoan.Text, "dd/MM/yyyy", null).ToString("yyyy-MM-dd");

                if (!int.TryParse(txtsophieu.Text, out sophieunhap))
                {
                    MessageBox.Show("Vui lòng nhập một số nhập khẩu hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtsophieu.Focus();
                }
                else
                {
                    sophieunhap = Convert.ToInt32(txtsophieu.Text);
                }

                if (string.IsNullOrEmpty(txttygia.Text)) { tygia = 1; }
                else { tygia = Convert.ToDouble(txttygia.Text); }

                if (string.IsNullOrEmpty(txtsongaydukien.Text))
                {
                    songayno = "0";
                }
                else { songayno = txtsongaydukien.Text; }

                if (CardCode.Equals(""))
                {
                    MessageBox.Show("Hãy nhập mã nhà cung cấp"); return;
                }

                if (sophieunhap == 0)
                {
                    MessageBox.Show("Hãy nhập số phiếu nhập khẩu."); return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString()); return;
            }

            SAPbobsCOM.Documents dl = default(SAPbobsCOM.Documents);
            dl = (SAPbobsCOM.Documents)Globals.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes);

            dl.DocDate = DateTime.ParseExact(DocDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);
            dl.DocDueDate = DateTime.ParseExact(DocDueDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);
            dl.TaxDate = DateTime.ParseExact(TaxDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);           
            dl.CardCode = CardCode;
            dl.Comments = Comments;

            dl.UserFields.Fields.Item("U_LNK").Value = lonhapkhau;
            dl.UserFields.Fields.Item("U_NDKHV").Value = ngaydukienhangve;
            dl.UserFields.Fields.Item("U_NGN").Value = ngayghino;
            dl.UserFields.Fields.Item("U_NDKTT").Value = ngaydukienthanhtoan;
            dl.UserFields.Fields.Item("U_SNN").Value = songayno;
            dl.UserFields.Fields.Item("U_Draftkey").Value = sophieunhap.ToString();
            dl.UserFields.Fields.Item("U_KVDH").Value = KhuVucDH;

            if (loaitien != "VND")
            {
                dl.DocCurrency = loaitien;
                dl.DocRate = tygia;
            }else { }
            
            HashSet<string> selecteBaseEntryImport = new HashSet<string>();
            HashSet<string> SelectObjectpnk = new HashSet<string>();

            foreach (Janus.Windows.GridEX.GridEXRow CheckItem in gridEXInv.GetCheckedRows())
            {
                if (CheckItem.Cells[0].Value.ToString().Equals("True"))
                {
                    string docEntry = CheckItem.Cells[8].Value.ToString();
                    string objtecsappnk = CheckItem.Cells[13].Value.ToString();

                    selecteBaseEntryImport.Add(docEntry);
                    SelectObjectpnk.Add(objtecsappnk);
                }
            }

            if (selecteBaseEntryImport.Count > 1)
            {
                MessageBox.Show("Bạn không thể lưa chọn nhiều phiếu nhập khẩu để lên đơn hàng mua", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!SelectObjectpnk.Contains("2025"))
            {
                MessageBox.Show("Bạn không thể tạo phiếu nhập kho từ đơn hàng mua", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (Janus.Windows.GridEX.GridEXRow CheckItem in gridEXInv.GetCheckedRows())
            {
                if (CheckItem.Cells[0].Value.ToString().Equals("True"))
                {
                    string mahang = CheckItem.Cells[1].Value.ToString();
                    double slnhap = Double.Parse(CheckItem.Cells[4].Value.ToString());
                    double slkhadung = Double.Parse(CheckItem.Cells[24].Value.ToString());
                    if (slnhap > slkhadung)
                    {
                        int rowIndex = CheckItem.RowIndex + 1;

                        MessageBox.Show($"Mã hàng {mahang} dòng số {rowIndex} số lượng thực nhập không được lơn hơn số lượng cho phép nhập kho", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            foreach (Janus.Windows.GridEX.GridEXRow CheckItem in gridEXInv.GetCheckedRows())
            {
                if (CheckItem.Cells[0].Value.ToString().Equals("True"))
                {
                    string sothutu = CheckItem.Cells[23].Value.ToString();

                    dl.Lines.ItemCode = CheckItem.Cells[1].Value.ToString();
                    dl.Lines.Quantity = Double.Parse(CheckItem.Cells[4].Value.ToString());
                    dl.Lines.Price = Double.Parse(CheckItem.Cells[6].Value.ToString());
                    dl.Lines.BaseEntry = Int32.Parse(CheckItem.Cells[14].Value.ToString());
                    dl.Lines.BaseLine = Int32.Parse(CheckItem.Cells[15].Value.ToString());
                    dl.Lines.BaseType = Int32.Parse(CheckItem.Cells[16].Value.ToString());
                    dl.Lines.WarehouseCode = CheckItem.Cells[21].Value.ToString();
                    dl.Lines.UserFields.Fields.Item("U_MPL1").Value = CheckItem.Cells[2].Value.ToString();
                    dl.Lines.UoMEntry = GetUomEntry(CheckItem.Cells[7].Value.ToString());

                    try
                    {
                        bool isBarcodeManaged = CheckIfItemIsBarcodeManaged(CheckItem.Cells[1].Value.ToString());
                        if (isBarcodeManaged)
                        {
                            string barcode = "";

                            if (CheckItem != null && CheckItem.Cells.Count > 17 && CheckItem.Cells[17].Value != null)
                            {
                                barcode = CheckItem.Cells[17].Value?.ToString() ?? string.Empty;
                            }

                            dl.Lines.BarCode = barcode;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi thêm mã barcode: " + ex.Message);
                    }

                    try
                    {
                        bool isBincodeManaged = CheckIWhscodeBINManaged(CheckItem.Cells[21].Value.ToString());
                        string Bincodewwhs = CheckItem.Cells[22].Value?.ToString() ?? string.Empty;

                        if ((isBincodeManaged == true && string.IsNullOrWhiteSpace(Bincodewwhs)) || (isBincodeManaged == true && Bincodewwhs == "0"))
                        {
                            MessageBox.Show($"Bạn cần nhập bin cho kho {CheckItem.Cells[21].Value.ToString()}", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch { }

                    Headerbin objtectlineid = headerBins.FirstOrDefault(h => h.LineId == sothutu); // lây bin

                    if (objtectlineid != null)
                    {
                        foreach (var bin in objtectlineid.BinDetails)
                        {
                            int binAbsEntry = GetBinAbsEntry(bin.CodeBin);
                            dl.Lines.BinAllocations.BinAbsEntry = binAbsEntry;
                            dl.Lines.BinAllocations.Quantity = bin.QuantityBin;
                            dl.Lines.BinAllocations.Add();
                        }
                    }
                    else { }
                    
                    dl.Lines.Add();
                }
            }

            try
            {
                dl.Add();
            }
            catch (Exception ex) { }
            int Messagecode;
            string Messagename;
            string DocEntry = "";
            Globals.SapCompany.GetLastError(out Messagecode, out Messagename);
            if (Messagecode != 0)
            {
                MessageBox.Show(Messagename);
            }
            else
            {
                DocEntry = Globals.SapCompany.GetNewObjectKey();
                SearchImport_Goodreceipt();
                LoadDelivery(DocEntry);
            }
        }

        private bool CheckIfItemIsBarcodeManaged(string itemCode)
        {
            bool isBarcodeManaged = false;
            SAPbobsCOM.Recordset recordset = null;

            try
            {
                recordset = (SAPbobsCOM.Recordset)Globals.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string query = $"SELECT T0.ItemCode FROM OITM T0 INNER JOIN OBCD T1 ON T0.ItemCode = T1.ItemCode WHERE T1.ItemCode = '{itemCode}'";
                recordset.DoQuery(query);

                if (!recordset.EoF)
                {
                    isBarcodeManaged = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kiểm tra thông tin mã barcode từ OITM cho mặt hàng {itemCode}: {ex.Message}");
            }
            finally
            {
                if (recordset != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(recordset);
                    recordset = null;
                }
            }
            return isBarcodeManaged;
        }

        private bool CheckIWhsManagedBin(string whsCode)
        {
            bool isBarcodeManaged = false;
            SAPbobsCOM.Recordset recordset = null;

            try
            {
                recordset = (SAPbobsCOM.Recordset)Globals.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string query = $"SELECT T0.WhsCode FROM OBIN T0  WHERE T1.whsCode = '{whsCode}'";
                recordset.DoQuery(query);

                if (!recordset.EoF)
                {
                    isBarcodeManaged = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kiểm tra thông tin bin của kho {whsCode}: {ex.Message}");
            }
            finally
            {
                if (recordset != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(recordset);
                    recordset = null;
                }
            }
            return isBarcodeManaged;
        }

        private void CeateGR_Draft_To_Goodreceiptp()
        {
            string CardCode, ReasonNX, Customer, TaxCode, Address, Branch, Remark, Warehouse, Print, TypeExport, Comments, loaitien;
            string BatchNumber = "";
            double tygia;
            double BatchQuantity = 0;
            int sophieunhap;
            DateTime DocDate, TaxDate, DocDueDate;
            try
            {
                CardCode = editKH.Text;
                Customer = editKHL.Text;
                TaxCode = editMST.Text;
                Address = editDC.Text;
                Comments = editGC.Text;
                loaitien = cbxtien.Text;
                sophieunhap = Convert.ToInt32(txtsophieu.Text);


                if (string.IsNullOrEmpty(txttygia.Text)) { tygia = 1; }
                else { tygia = Convert.ToDouble(txttygia.Text); }

                //  try { Branch = cbbCN.SelectedValue.ToString(); } catch { Branch = ""; }
                Remark = editGC.Text;
                Warehouse = editKho.Text;

                DocDate = taxdate.Value;
                TaxDate = DateDKHV.Value;
                DocDueDate = Dateghino.Value;
                if (CardCode.Equals(""))
                {
                    MessageBox.Show("Hãy nhập mã khách hàng."); return;
                }
                if (Warehouse.Equals(""))
                {
                    MessageBox.Show("Hãy nhập kho nhập hàng."); return;
                }
                if (sophieunhap == 0)
                {
                    MessageBox.Show("Hãy nhập số phiếu nhập kho nháp."); return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString()); return;
            }


            string Series, DocNo;
            SAPbobsCOM.Documents dl = default(SAPbobsCOM.Documents);
            dl = (SAPbobsCOM.Documents)Globals.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);

            if (dl.GetByKey(sophieunhap))
            {
                try
                {
                    dl.DocDate = DateTime.ParseExact(DocDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);
                    dl.DocDueDate = DateTime.ParseExact(DocDueDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);
                    dl.TaxDate = DateTime.ParseExact(TaxDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);
                    dl.UserFields.Fields.Item("U_DraftKey").Value = sophieunhap;

                    dl.CardCode = CardCode;
                    dl.Comments = Comments;

                    if (loaitien != "VND")
                    {
                        dl.DocCurrency = loaitien;
                        dl.DocRate = tygia;
                    }
                    else { }

                    foreach (Janus.Windows.GridEX.GridEXRow CheckItem in gridEXInv.GetCheckedRows())
                    {
                        if (CheckItem.Cells[0].Value.ToString().Equals("True"))
                        {
                            try { Series = CheckItem.Cells[6].Value.ToString(); }
                            catch { Series = ""; }
                            try { DocNo = CheckItem.Cells[7].Value.ToString(); }
                            catch { DocNo = ""; }
                            dl.Lines.ItemCode = CheckItem.Cells[1].Value.ToString();
                            dl.Lines.Quantity = Double.Parse(CheckItem.Cells[3].Value.ToString());
                            dl.Lines.Price = Double.Parse(CheckItem.Cells[3].Value.ToString());
                            dl.Lines.BaseEntry = Int32.Parse(CheckItem.Cells[22].Value.ToString());
                            dl.Lines.BaseLine = Int32.Parse(CheckItem.Cells[23].Value.ToString());
                            dl.Lines.BaseType = Int32.Parse(CheckItem.Cells[24].Value.ToString());
                            dl.Lines.WarehouseCode = Warehouse;

                            try
                            {
                                bool isBatchManaged = CheckIfItemIsBatchManaged(CheckItem.Cells[1].Value.ToString());

                                if (isBatchManaged)
                                {
                                    if (CheckItem != null && CheckItem.Cells.Count > 20 && CheckItem.Cells[20].Value != null)
                                    {
                                        BatchNumber = CheckItem.Cells[20].Value.ToString();
                                    }

                                    if (CheckItem != null && CheckItem.Cells.Count > 21 && CheckItem.Cells[21].Value != null &&
                                        !string.IsNullOrEmpty(CheckItem.Cells[21].Value.ToString()))
                                    {
                                        BatchQuantity = Double.Parse(CheckItem.Cells[21].Value.ToString()); // Giả sử ô thứ 21 chứa số lượng lô
                                    }

                                    if (string.IsNullOrEmpty(BatchNumber))
                                    {
                                        MessageBox.Show($"Mặt hàng {CheckItem.Cells[1].Value.ToString()} này được quản lý theo lô. Vui lòng nhập mã lô.",
                                                        "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }

                                    if (!string.IsNullOrEmpty(BatchNumber) && BatchQuantity > 0)
                                    {
                                        if (BatchQuantity == dl.Lines.Quantity)
                                        {
                                            dl.Lines.BatchNumbers.BatchNumber = BatchNumber;
                                            dl.Lines.BatchNumbers.Quantity = BatchQuantity;
                                            dl.Lines.BatchNumbers.Add(); // Thêm lô vào danh sách lô của dòng hiện tại
                                        }
                                        else
                                        {
                                            MessageBox.Show($"Tổng số lượng của lô {CheckItem.Cells[1].Value.ToString()} phải bằng với số lượng của dòng. Vui lòng kiểm tra và nhập lại.",
                                                            "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi khi thêm lô: " + ex.Message);
                            }

                            dl.Lines.Add();
                        }
                    }

                    int saveResult = dl.SaveDraftToDocument();

                    if (saveResult == 0)
                    {
                        string DocEntry = Globals.SapCompany.GetNewObjectKey();
                        MessageBox.Show($"Document created successfully from draft. DocEntry: {DocEntry}");
                        SearchImport_Goodreceipt();
                        LoadDelivery(DocEntry);
                    }
                    else
                    {
                        Globals.SapCompany.GetLastError(out int errCode, out string errMsg);
                        MessageBox.Show($"Error converting draft to document: {errMsg}");
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }

            }

            else
            {
                MessageBox.Show("Draft not found. Please verify the draft number.");
            }
        }

        private void btnPORPDN_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn tạo phiếu nhập kho?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Create_Import_To_GoodsReceipt();
            }
        }

        private void TKDraftToNK_Click(object sender, EventArgs e)
        {
            SearchImport_Goodreceipt();
        }

        private void btnhap_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn tạo phiếu nhập kho?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //  CreateGoodReceiptDraft_To_Goodreceipt();
                CeateGR_Draft_To_Goodreceiptp();
            }
        }

        private void btnserch_import_Click(object sender, EventArgs e)
        {
            Loadsophieuimport();
        }

        public void Loadsophieuimport()
        {
            string carcode;
            int sodonhang = 0;
            DateTime tungay, denngay;
            carcode = editKH.Text;
            tungay = txtTungay.Value;
            denngay = txtdenngay.Value;
            if (string.IsNullOrEmpty(txtsophieu.Text))
            {
                sodonhang = 0;
            }
            else { sodonhang = int.Parse(txtsophieu.Text); }

            try
            {
                DataTable oDatable = Globals.GetSapDataTableSQL("EXEC  Search_Phieunhapkhau '" + carcode + "', " + sodonhang.ToString() + ", '" + tungay + "', '" + denngay + "' ");
                System.Data.DataTable dt = new System.Data.DataTable();

                dt.Columns.Add("DocEntry", typeof(String));
                dt.Columns.Add("NCCDHM", typeof(String));
                dt.Columns.Add("TenNCC", typeof(String));
                dt.Columns.Add("NgayDHM", typeof(String));
                dt.Columns.Add("Loaiphieu", typeof(String));

                if (oDatable.Rows.Count > 0)
                {
                    for (int i = 0; i < oDatable.Rows.Count; i++)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        //dr[0] = oDatable.GetValue("DocEntry", i);
                        dr[0] = oDatable.Rows[i]["DocEntry"].ToString();
                        //dr[1] = oDatable.GetValue("CardCode", i);
                        dr[1] =  oDatable.Rows[i]["CardCode"].ToString();
                        //dr[2] = oDatable.GetValue("CardName", i);
                        dr[2] =  oDatable.Rows[i]["CardName"].ToString();
                        //dr[3] = oDatable.GetValue("ngaydhm", i);
                        dr[3] = oDatable.Rows[i]["ngaydhm"].ToString();
                        //dr[4] = oDatable.GetValue("LoaiPhieuNK", i);
                        dr[4] = oDatable.Rows[i]["LoaiPhieuNK"].ToString();
                        dt.Rows.Add(dr);
                    }
                }
                gridEXDL.DataSource = dt;
                //gridEXInv.SetDataBinding(dt, "");
                //gridEXInv.RetrieveStructure();
            }
            catch { }

        }

        private void btnsearchOPDN_Click(object sender, EventArgs e)
        {
            Loadsophieu_Goodreceipt();
        }

        public void Loadsophieu_Goodreceipt()
        {
            string carcode;
            int sodonhang = 0;
            DateTime tungay, denngay;
            carcode = editKH.Text;
            tungay = txtTungay.Value;
            denngay = txtdenngay.Value;
            if (string.IsNullOrEmpty(txtsophieu.Text))
            {
                sodonhang = 0;
            }
            else { sodonhang = int.Parse(txtsophieu.Text); }

            try
            {
                SAPbouiCOM.DataTable oDatable = Globals.GetSapDataTable("EXEC  SearchListGoodReceipt '" + carcode + "', " + sodonhang.ToString() + ", '" + tungay + "', '" + denngay + "' ");
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("DocEntry", typeof(String));
                dt.Columns.Add("NCCDHM", typeof(String));
                dt.Columns.Add("TenNCC", typeof(String));
                dt.Columns.Add("NgayDHM", typeof(String));
                dt.Columns.Add("Loaiphieu", typeof(String));

                if (!oDatable.IsEmpty)
                {
                    for (int i = 0; i < oDatable.Rows.Count; i++)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr[0] = oDatable.GetValue("DocEntry", i);
                        dr[1] = oDatable.GetValue("CardCode", i);
                        dr[2] = oDatable.GetValue("CardName", i);
                        dr[3] = oDatable.GetValue("dateimport", i);
                        dr[4] = oDatable.GetValue("loaiphieuGE", i);
                        dt.Rows.Add(dr);
                    }
                }
                gridEXDL.DataSource = dt;
                //gridEXInv.SetDataBinding(dt, "");
                //gridEXInv.RetrieveStructure();
            }
            catch { }

        }

        private void editKH_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(editKH.Text))
            {
                editTKH.Text = string.Empty;
            }
            //else
            //{
            //    editKH_ButtonClick(sender, e);
            //}
        }

        private void editKHL_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(editKHL.Text))
            {
                editMST.Text = string.Empty;
               // editDC.Text = string.Empty;
            }

        }

        private void brnSerch_Impoerted_Click(object sender, EventArgs e)
        {
            CallGood_Imported();
        }

        private void CallGood_Imported()
        {
            int sophieunhap = 0;
            string makh = "";
            makh = editKH.Text;

            if (!int.TryParse(txtsophieu.Text, out sophieunhap))
            {
                txtsophieu.Focus();
            }
            else { sophieunhap = Convert.ToInt32(txtsophieu.Text); }

            try
            {
                DataTable oDataTable = Globals.GetSapDataTableSQL("EXEC SeachGoods_Import "+ sophieunhap + ", '"+makh+"' ");
                DataTable dt = oDataTable.Copy();
                gridEXInv.DataSource = dt;

            }
            catch (Exception ex) { }
        }

        private void txtsongaydukien_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Dateghino.Value != null && DateTime.TryParse(Dateghino.Value.ToString(), out DateTime ngayGhiNo))
                {
                    int soNgayNo = 0;
                    if (!string.IsNullOrWhiteSpace(txtsongaydukien.Text) && int.TryParse(txtsongaydukien.Text, out int soNgayNhap))
                    {
                        soNgayNo = soNgayNhap;
                    }

                    DateTime ngayDuKienThanhToan = ngayGhiNo.AddDays(soNgayNo);
                    datedkthanhtoan.Value = ngayDuKienThanhToan;
                    datedkthanhtoan.CustomFormat = "dd/MM/yyyy";
               
                }
                else
                {
                    MessageBox.Show("Ngày ghi nợ không hợp lệ!", "Lỗi ngày", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridEXInv_ColumnButtonClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            if (e.Column.Key == "Kho")
            {
                var currentRow = gridEXInv.CurrentRow;
                if (currentRow == null)
                {
                    return;
                }
                var isChecked = currentRow.Cells["Chọn"].Value;
                if (isChecked == null || !(bool)isChecked)
                { return; }

                try
                {
                    gridEXInv.UpdateData();
                    string makho = "";
                    makho = gridEXInv.CurrentRow.Cells[21].Value.ToString();

                    ListWhs whs = new ListWhs(makho);
                    whs.passControlWhs = new ListWhs.PassControlWhs(PassWhsCodeLine);
                    if (whs.ShowDialog(this) == DialogResult.OK)
                    {
                        whs.passControlWhs = new ListWhs.PassControlWhs(PassWhsCodeLine);
                    }
                    else { }
                    whs.Dispose();
                }
                catch { }
            }

            //if (e.Column.Position == 17)
            //{
            //    try
            //    {
            //        var mahang = gridEXInv.CurrentRow.Cells[1].Value.ToString();
            //        var donvitinh = gridEXInv.CurrentRow.Cells[7].Value.ToString();

            //        ListBarCode br = new ListBarCode(mahang, donvitinh);
            //        br.passControlbarCode = new ListBarCode.PassControlBarCode(PassBarCode);
            //        if (br.ShowDialog(this) == DialogResult.OK)
            //        {
            //            br.passControlbarCode = new ListBarCode.PassControlBarCode(PassBarCode);
            //        }
            //        else { }
            //        br.Dispose();
            //    }
            //    catch { }
            //}


            if (e.Column.Key == "Bin Kho")
            {
                var currentRow = gridEXInv.CurrentRow;
                if (currentRow == null)
                {
                    return;
                }
                var isChecked = currentRow.Cells["Kho"].Value as string ?? "";
                if (string.IsNullOrWhiteSpace(isChecked))
                {
                    return;
                }

                DataTable oDataTable = Globals.GetSapDataTableSQL("SELECT BinActivat FROM OWHS T0 WHERE T0.WhsCode ='" + isChecked + "'");
                if (oDataTable.Rows.Count > 0)
                {
                    string binActivation = oDataTable.Rows[0]["BinActivat"].ToString();
                    if (binActivation.ToUpper() == "N" || binActivation.ToUpper() != "Y")
                    {
                        MessageBox.Show($"Kho " + isChecked + " không quản lí theo bin");
                        return;
                    }
                }

                try
                {
                    if (currentRow != null)
                    {
                        string soLuong = currentRow.Cells["Số lượng"]?.Value?.ToString() ?? "0";
                        var mahang = gridEXInv.CurrentRow?.Cells[1]?.Value?.ToString() ?? "";
                        var makho = gridEXInv.CurrentRow.Cells[21]?.Value?.ToString() ?? "";
                        var lineid = currentRow.Cells["STT"]?.Value?.ToString() ?? "0";

                        Headerbin existingHeaderbin = headerBins.FirstOrDefault(h => h.LineId == lineid);
                        BinWhsCode binForm;

                        if (existingHeaderbin != null)
                        {
                            binForm = new BinWhsCode(soLuong, mahang, makho, lineid, existingHeaderbin);
                        }
                        else
                        {
                            binForm = new BinWhsCode(soLuong, mahang, makho, lineid);
                        }

                        if (binForm.ShowDialog() == DialogResult.OK)
                        {
                            var updatedHeaderBin = binForm.HeaderBinData;
                            double slphanbo = binForm.totalquantityall;
                            currentRow.Cells["Bin Kho"].Value = slphanbo;

                            var existingItem = headerBins.FirstOrDefault(h => h.LineId == updatedHeaderBin.LineId);
                            if (existingItem != null)
                            {
                                headerBins.Remove(existingItem);
                                headerBins.Add(updatedHeaderBin);
                            }
                            else
                            {
                                headerBins.Add(updatedHeaderBin);
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
                }

            }


        }

        private bool CheckIWhscodeBINManaged(string Whscode)
        {
            bool isBinManaged = false;

            DataTable oDataTable = Globals.GetSapDataTableSQL("SELECT BinActivat FROM OWHS T0 WHERE T0.WhsCode ='" + Whscode + "'");
            if (oDataTable.Rows.Count > 0)
            {
                string binActivation = oDataTable.Rows[0]["BinActivat"]?.ToString();
                if (binActivation.ToUpper() == "Y")
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        //private void chekkhocuartoi()
        //{
        //if (e.Column.Position == 20)   //SELECT BinActivat FROM OWHS T0 WHERE T0.WhsCode ='1A'
        //{
        //    try
        //    {
        //        gridEXInv.UpdateData();
        //        string makho = "";
        //        makho = gridEXInv.CurrentRow.Cells[20].Value.ToString();

        //        ListWhs whs = new ListWhs(makho);
        //        whs.passControlWhs = new ListWhs.PassControlWhs(PassWhsCodeLine);
        //        if (whs.ShowDialog(this) == DialogResult.OK)
        //        {
        //            whs.passControlWhs = new ListWhs.PassControlWhs(PassWhsCodeLine);
        //        }
        //        else { }
        //        whs.Dispose();
        //    }
        //    catch { }
        //}

        //if (e.Column.Position == 21)
        //{
        //    try
        //    {
        //        var currentRow = gridEXInv.CurrentRow;
        //        if (currentRow != null)
        //        {
        //            string soLuong = currentRow.Cells["Số lượng"]?.Value?.ToString() ?? "0";
        //            var mahang = gridEXInv.CurrentRow?.Cells[1]?.Value?.ToString() ?? "";
        //            var makho = gridEXInv.CurrentRow.Cells[20]?.Value?.ToString() ?? "";
        //            var lineid = currentRow.Cells["STT"]?.Value?.ToString() ?? "0";

        //            Headerbin existingHeaderbin = headerBins.FirstOrDefault(h => h.LineId == lineid);
        //            BinWhsCode binForm;

        //            if (existingHeaderbin != null)
        //            {
        //                binForm = new BinWhsCode(soLuong, mahang, makho, lineid, existingHeaderbin);
        //            }
        //            else
        //            {
        //                binForm = new BinWhsCode(soLuong, mahang, makho, lineid);
        //            }

        //            if (binForm.ShowDialog() == DialogResult.OK)
        //            {
        //                var updatedHeaderBin = binForm.HeaderBinData;
        //                double slphanbo = binForm.totalquantityall;
        //                currentRow.Cells["Bin Kho"].Value = slphanbo;

        //                var existingItem = headerBins.FirstOrDefault(h => h.LineId == updatedHeaderBin.LineId);
        //                if (existingItem != null)
        //                {
        //                    headerBins.Remove(existingItem);
        //                    headerBins.Add(updatedHeaderBin);
        //                }
        //                else
        //                {
        //                    headerBins.Add(updatedHeaderBin);
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
        //    }

        //}
        //}

        private void gridEXInv_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {
            //var isChecked = e.Row.Cells["Chọn"].Value as bool?;

            //if (isChecked == true)
            //{
            //    e.Row.Cells["Kho"].Enabled = true;
            //    e.Row.Cells["Số thùng"].Enabled = true;
            //    e.Row.Cells["Số lượng"].Enabled = true;
            //}
            //else
            //{
            //    e.Row.Cells["Kho"].Enabled = false;
            //    e.Row.Cells["Số thùng"].Enabled = false;
            //    e.Row.Cells["Số lượng"].Enabled = false;
            //}
        }
   
    }
}
