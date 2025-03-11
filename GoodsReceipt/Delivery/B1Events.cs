using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery
{

    public class B1Events
    {
        private SAPbouiCOM.Application SBO_Application;
        private SAPbobsCOM.Company oCompany;
        private bool bPopUp; //  Flag to indicate if the modal form is open.
        private SAPbouiCOM.Form oFormPopUp; 

        public B1Events(SAPbouiCOM.Application SBO_Application, SAPbobsCOM.Company oCompany)
        {
            this.SBO_Application = SBO_Application;
            this.oCompany = oCompany;
            SBO_Application.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(SBO_Application_MenuEvent);
            SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SBO_Application_ItemEvent);
            SBO_Application.ProgressBarEvent += new SAPbouiCOM._IApplicationEvents_ProgressBarEventEventHandler(SBO_Application_ProgressBarEvent);
            SBO_Application.StatusBarEvent += new SAPbouiCOM._IApplicationEvents_StatusBarEventEventHandler(SBO_Application_StatusBarEvent);
            SBO_Application.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(SBO_Application_FormDataEvent);
            SBO_Application.RightClickEvent += new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(SBO_Application_RightClickEvent);
            SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent);
        }
        private void SBO_Application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            switch (EventType)
            {
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    //Exit Add-On
                    System.Windows.Forms.Application.Exit();
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_FontChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    System.Windows.Forms.Application.Exit();
                    break;
                default:
                    break;
            }
        }
        private void SBO_Application_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            Delivery d = new Delivery(SBO_Application, oCompany);
            d.MenuEvent(pVal, BubbleEvent);
        }
        private void SBO_Application_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            switch (pVal.EventType)
            {
                case SAPbouiCOM.BoEventTypes.et_VALIDATE:
                    VALIDATE(FormUID, pVal, BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_FORM_CLOSE:
                    if ((FormUID == "dDate" || FormUID == "dUser") & bPopUp)
                        bPopUp = false;
                    else
                        FORM_CLOSE(FormUID, pVal, BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST:
                    CHOOSE_FROM_LIST(FormUID, pVal, BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_PICKER_CLICKED:
                    PICKER_CLICKED(FormUID, pVal, BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_GOT_FOCUS:
                    GOT_FOCUS(FormUID, pVal, BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_FORM_LOAD:
                    FORM_LOAD(FormUID, pVal, BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_FORM_RESIZE:
                    FORM_RESIZE(FormUID, pVal, BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
                    //ITEM_PRESSED(FormUID, pVal, out BubbleEvent,ref oFormPopUp,ref bPopUp);
                    ITEM_PRESSED(FormUID, pVal, out BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_COMBO_SELECT:
                    COMBO_SELECT(FormUID, pVal, BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_CLICK:
                    CLICK(FormUID, pVal, BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_KEY_DOWN:
                    KEY_DOWN(FormUID, pVal, BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_DOUBLE_CLICK:
                    DOUBLE_CLICK(FormUID, pVal, BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_MATRIX_LOAD:
                    //MATRIX_LOAD(FormUID, pVal, BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_MATRIX_LINK_PRESSED:
                    MATRIX_LINK_PRESSED(FormUID, pVal, BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_LOST_FOCUS:
                    LOST_FOCUS(FormUID, pVal, BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_FORM_ACTIVATE:
                    FORM_ACTIVATE(FormUID, pVal, BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_FORM_DEACTIVATE:
                    if (bPopUp & (FormUID == "dDate" || FormUID == "dUser"))
                    {
                        oFormPopUp.Select(); //  Select the modal form
                        BubbleEvent = false;
                    }
                    else
                    FORM_DEACTIVATE(FormUID, pVal, BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_DATASOURCE_LOAD :
                    DATASOURCE_LOAD(FormUID, pVal, BubbleEvent);
                    break;
                default:
                    break;
            }
        }
        private void SBO_Application_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
            switch (BusinessObjectInfo.EventType)
            {
                case SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD:
                    FORM_DATA_ADD(BusinessObjectInfo.FormUID, BusinessObjectInfo, out BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD:
                     FORM_DATA_LOAD(BusinessObjectInfo.FormUID, BusinessObjectInfo, BubbleEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE:
                    FORM_DATA_UPDATE(BusinessObjectInfo.FormUID, BusinessObjectInfo, out BubbleEvent);
                    break;
            }
        }
        private void FORM_DEACTIVATE(string FormUID, SAPbouiCOM.ItemEvent pVal,  bool BubbleEvent)
        {
            
        }

        
        private void SBO_Application_RightClickEvent(ref SAPbouiCOM.ContextMenuInfo eventInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
        }

        private void SBO_Application_ProgressBarEvent(ref SAPbouiCOM.ProgressBarEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
        }
        private void SBO_Application_StatusBarEvent(string Text, SAPbouiCOM.BoStatusBarMessageType MessageType)
        {

        }

        #region ItemEvent
        private void VALIDATE(string FormUID, SAPbouiCOM.ItemEvent pVal, bool BubbleEvent)
        {

        }
        private void CHOOSE_FROM_LIST(string FormUID, SAPbouiCOM.ItemEvent pVal, bool BubbleEvent)
        {
        }
        private void PICKER_CLICKED(string FormUID, SAPbouiCOM.ItemEvent pVal, bool BubbleEvent)
        {
            BubbleEvent = true;
        }
        private void GOT_FOCUS(string FormUID, SAPbouiCOM.ItemEvent pVal, bool BubbleEvent)
        {

        }
        private void FORM_LOAD(string FormUID, SAPbouiCOM.ItemEvent pVal, bool BubbleEvent)
        {
        }
        private void ITEM_PRESSED(string FormUID, SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
        }
        private void COMBO_SELECT(string FormUID, SAPbouiCOM.ItemEvent pVal, bool BubbleEvent)
        {
        }
        private void CLICK(string FormUID, SAPbouiCOM.ItemEvent pVal, bool BubbleEvent)
        {
        }
        private void KEY_DOWN(string FormUID, SAPbouiCOM.ItemEvent pVal, bool BubbleEvent)
        {

        }
        private void DOUBLE_CLICK(string FormUID, SAPbouiCOM.ItemEvent pVal, bool BubbleEvent)
        {
        }
        private void FORM_CLOSE(string FormUID, SAPbouiCOM.ItemEvent pVal, bool BubbleEvent)
        {

        }
        private void FORM_DATA_ADD(string FormUID, SAPbouiCOM.BusinessObjectInfo events, out bool BubbleEvent)
        {
            BubbleEvent = true;
        }
        private void FORM_DATA_LOAD(string FormUID, SAPbouiCOM.BusinessObjectInfo events, bool BubbleEvent)
        {
        }
        private void FORM_DATA_UPDATE(string FormUID, SAPbouiCOM.BusinessObjectInfo events, out bool BubbleEvent)
        {
            BubbleEvent = true;
        }
        private void MATRIX_LOAD(string FormUID, SAPbouiCOM.ItemEvent pVal, bool BubbleEvent)
        {

        }
        private void MATRIX_LINK_PRESSED(string FormUID, SAPbouiCOM.ItemEvent pVal, bool BubbleEvent)
        {

        }
        private void LOST_FOCUS(string FormUID, SAPbouiCOM.ItemEvent pVal, bool BubbleEvent)
        {
        }
        private void FORM_RESIZE(string FormUID, SAPbouiCOM.ItemEvent pVal, bool BubbleEvent)
        {

        }
        private void FORM_ACTIVATE(string FormUID, SAPbouiCOM.ItemEvent pVal, bool BubbleEvent)
        {

        }
        private void FORM_DEACTIVATE(string FormUID, SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
        }
        private void DATASOURCE_LOAD(string FormUID, SAPbouiCOM.ItemEvent pVal, bool BubbleEvent)
        {

        }
        #endregion
    }

}
