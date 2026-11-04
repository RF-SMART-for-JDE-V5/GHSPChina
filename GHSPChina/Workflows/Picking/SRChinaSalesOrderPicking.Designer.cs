namespace ICS.SRChina.Wireless.JDE.Workflow.Picking
{
    using System;
    using ICS.Database.TableObjects.RFS;
    using ICS.Display;
    using ICS.Utilities;
    using ICS.Workflow.Base.WorkflowDesign.Components;

    public partial class SRChinaSalesOrderPicking
    {
        #region Screen defs

        #endregion

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.CollectBranch = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this._BranchPlantList = new ICS.Workflow.Base.WorkflowDesign.Components.GenericList(this);
            this.CollectPickSlip = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this.F4211PickSlip_List = new ICS.Workflow.Base.WorkflowDesign.Components.GenericList(this);
            this.CollectPrinter = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this.Printer_List = new ICS.Workflow.Base.WorkflowDesign.Components.GenericList(this);
            this.CollectStagingLocation = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this._LocationMasterList = new ICS.Workflow.Base.WorkflowDesign.Components.GenericList(this);
            this.VerifyLocation = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this.CollectLabelID = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this.ShortPickWarning = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this.PickList = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this.PrintBarCodeLabels = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            // 
            // CollectBranch
            // 
            this.CollectBranch.ListScreen = this._BranchPlantList;
            this.CollectBranch.ListScreenControl = this._BranchPlantList;
            this.CollectBranch.ScreenName = "CollectBranch";
            this.CollectBranch.ValidateMethod += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ValidationHandler(this.CollectBranch_ValidateMethod);
            // 
            // _BranchPlantList
            // 
            this._BranchPlantList.ListScreenParent = this.CollectBranch;
            this._BranchPlantList.ParentScreen = this.CollectBranch;
            this._BranchPlantList.ScreenName = "_BranchPlantList";
            this._BranchPlantList.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._BranchPlantList_LoadEvent);
            // 
            // CollectPickSlip
            // 
            this.CollectPickSlip.ListScreen = this.F4211PickSlip_List;
            this.CollectPickSlip.ListScreenControl = this.F4211PickSlip_List;
            this.CollectPickSlip.ScreenName = "CollectPickSlip";
            this.CollectPickSlip.ValidateMethod += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ValidationHandler(this.CollectPickSlip_ValidateMethod);
            // 
            // F4211PickSlip_List
            // 
            this.F4211PickSlip_List.ListScreenParent = this.CollectPickSlip;
            this.F4211PickSlip_List.ParentScreen = this.CollectPickSlip;
            this.F4211PickSlip_List.ScreenName = "F4211PickSlip_List";
            this.F4211PickSlip_List.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.F4211PickSlip_List_LoadEvent);
            // 
            // CollectPrinter
            // 
            this.CollectPrinter.ListScreen = this.Printer_List;
            this.CollectPrinter.ListScreenControl = this.Printer_List;
            this.CollectPrinter.ScreenName = "CollectPrinter";
            this.CollectPrinter.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._collectPrinter_LoadEvent);
            this.CollectPrinter.ValidateMethod += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ValidationHandler(this.CollectPrinter_ValidateMethod);
            // 
            // Printer_List
            // 
            this.Printer_List.ListScreenParent = this.CollectPrinter;
            this.Printer_List.ParentScreen = this.CollectPrinter;
            this.Printer_List.ScreenName = "Printer_List";
            this.Printer_List.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._printerList_LoadEvent);
            // 
            // CollectStagingLocation
            // 
            this.CollectStagingLocation.ListScreen = this._LocationMasterList;
            this.CollectStagingLocation.ListScreenControl = this._LocationMasterList;
            this.CollectStagingLocation.ScreenName = "CollectStagingLocation";
            this.CollectStagingLocation.ValidateMethod += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ValidationHandler(this.CollectStagingLocation_ValidateMethod);
            // 
            // _LocationMasterList
            // 
            this._LocationMasterList.ListScreenParent = this.CollectStagingLocation;
            this._LocationMasterList.ParentScreen = this.CollectStagingLocation;
            this._LocationMasterList.ScreenName = "F4100_List";
            this._LocationMasterList.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.StagingLocationsList_LoadEvent);
            // 
            // VerifyLocation
            // 
            this.VerifyLocation.ScreenName = "VerifyLocation";
            this.VerifyLocation.ValidateMethod += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ValidationHandler(this.VerifyLocation_ValidateMethod);
            // 
            // CollectLabelID
            // 
            this.CollectLabelID.ScreenName = "CollectLabelID";
            this.CollectLabelID.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.CollectLabelID_LoadEvent);
            this.CollectLabelID.ValidateMethod += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ValidationHandler(this.CollectLabelID_ValidateMethod);
            this.CollectLabelID.F2Method += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.FunctionKeyHandler(this.CollectLabelID_F2Method);
            this.CollectLabelID.F6Method += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.FunctionKeyHandler(this.CollectLabelID_F6Method);
            // 
            // ShortPickWarning
            // 
            this.ShortPickWarning.ScreenName = "ShortPickWarning";
            this.ShortPickWarning.ValidateMethod += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ValidationHandler(this.ShortPickWarning_ValidateMethod);
            // 
            // PickList
            // 
            this.PickList.NextScreen = this.CollectLabelID;
            this.PickList.ScreenName = "PickList";
            this.PickList.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.PickList_LoadEvent);
            // 
            // PrintBarCodeLabels
            // 
            this.PrintBarCodeLabels.BackScreen = this.CollectLabelID;
            this.PrintBarCodeLabels.ScreenName = "PrintBarCodeLabels";
            this.PrintBarCodeLabels.ValidateMethod += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ValidationHandler(this.PrintBarCodeLabels_ValidateMethod);
            // 
            // SRChinaSalesOrderPicking
            // 
            this.StartScreen = this.CollectBranch;
            this.WorkflowID = "SRChinaSalesOrderPicking";
        }

        #endregion

        protected GenericScreen CollectBranch;

        protected GenericScreen CollectPickSlip;

        protected GenericScreen CollectPrinter;

        protected GenericScreen CollectStagingLocation;

        protected GenericScreen VerifyLocation;

        protected GenericScreen CollectLabelID;

        protected GenericScreen ShortPickWarning;

        protected GenericList _LocationMasterList;

        protected GenericList _BranchPlantList;

        protected GenericScreen PickList;

        protected GenericList F4211PickSlip_List;

        protected GenericList Printer_List;

        protected GenericScreen PrintBarCodeLabels;
    }
}