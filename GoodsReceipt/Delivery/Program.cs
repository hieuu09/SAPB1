using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Delivery
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (!Globals.SetApplication())
            {
                if (Globals.SapApplication != null)
                    Globals.SapApplication.StatusBar.SetText("ImportReceipt Error connected!", SAPbouiCOM.BoMessageTime.bmt_Short,
                        SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                Environment.Exit(0);
                return;
            }
            Globals.AddMenuItems("String", "2304", "Delivery", "Phiếu nhập khẩu", 0, "");
            B1Events b1e = new B1Events(Globals.SapApplication, Globals.SapCompany);
            Globals.SapApplication.StatusBar.SetText("ImportReceipt FOXAI Connected!", SAPbouiCOM.BoMessageTime.bmt_Short,
                   SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            Application.Run();
        }
    }
}
