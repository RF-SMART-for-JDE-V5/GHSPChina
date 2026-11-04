namespace ICS.SRChina.Wireless.JDE.Workflow.ShopFloor
{
    using System;
    using ICS.Database.TableObjects.RFS;
    using ICS.Display;
    using ICS.Utilities;
    using ICS.Workflow.Base.WorkflowDesign.Components;

    public partial class SRChinaWorkOrderCompletion
    {
        #region Screen defs

        #endregion

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.CollectBranch = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this._BranchPlantList = new ICS.Workflow.Base.WorkflowDesign.Components.GenericList(this);
            this.CollectShiftCode = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this.CollectEmployee = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this._UserDefinedCodesList = new ICS.Workflow.Base.WorkflowDesign.Components.GenericList(this);
            this.CollectToLocation = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this._LocationMasterList = new ICS.Workflow.Base.WorkflowDesign.Components.GenericList(this);
            this.CollectLabelID = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
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
            this._BranchPlantList.DataColumnForValidation = "MCMCU";
            this._BranchPlantList.ListScreen = this._BranchPlantList;
            this._BranchPlantList.ListScreenParent = this.CollectBranch;
            this._BranchPlantList.ParentScreen = this.CollectBranch;
            this._BranchPlantList.ScreenName = "F0006_List";
            this._BranchPlantList.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._BranchPlantList_LoadEvent);
            // 
            // CollectShiftCode
            // 
            this.CollectShiftCode.BackScreen = this.CollectEmployee;
            this.CollectShiftCode.ListScreen = this._UserDefinedCodesList;
            this.CollectShiftCode.ListScreenControl = this._UserDefinedCodesList;
            this.CollectShiftCode.ScreenName = "CollectShiftCode";
            this.CollectShiftCode.ValidateMethod += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ValidationHandler(this.CollectShiftCode_ValidateMethod);
            // 
            // CollectEmployee
            // 
            this.CollectEmployee.BackScreen = this.CollectBranch;
            this.CollectEmployee.ScreenName = "CollectEmployee";
            this.CollectEmployee.ValidateMethod += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ValidationHandler(this.CollectEmployee_ValidateMethod);
            // 
            // _UserDefinedCodesList
            // 
            this._UserDefinedCodesList.ListScreen = this._UserDefinedCodesList;
            this._UserDefinedCodesList.ListScreenParent = this.CollectShiftCode;
            this._UserDefinedCodesList.ParentScreen = this.CollectShiftCode;
            this._UserDefinedCodesList.ScreenName = "F0005_List";
            this._UserDefinedCodesList.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._UserDefinedCodesList_LoadEvent);
            // 
            // CollectToLocation
            // 
            this.CollectToLocation.ListScreen = this._LocationMasterList;
            this.CollectToLocation.ListScreenControl = this._LocationMasterList;
            this.CollectToLocation.ScreenName = "CollectToLocation";
            this.CollectToLocation.ValidateMethod += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ValidationHandler(this.CollectToLocation_ValidateMethod);
            // 
            // _LocationMasterList
            // 
            this._LocationMasterList.ListScreenParent = this.CollectToLocation;
            this._LocationMasterList.ParentScreen = this.CollectToLocation;
            this._LocationMasterList.ScreenName = "F4100_List";
            this._LocationMasterList.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._LocationMasterList_LoadEvent);
            // 
            // CollectLabelID
            // 
            this.CollectLabelID.ScreenName = "CollectLabelID";
            this.CollectLabelID.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.CollectLabelID_LoadEvent);
            this.CollectLabelID.ValidateMethod += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ValidationHandler(this.CollectLabelID_ValidateMethod);
            // 
            // SRChinaWorkOrderCompletion
            // 
            this.StartScreen = this.CollectBranch;
            this.WorkflowID = "SRChinaWorkOrderCompletion";
        }

        #endregion

        protected GenericScreen CollectBranch;

        protected GenericScreen CollectShiftCode;

        protected GenericScreen CollectToLocation;

        protected GenericScreen CollectLabelID;

        protected GenericList _BranchPlantList;

        protected GenericList _UserDefinedCodesList;

        protected GenericList _LocationMasterList;

        protected GenericScreen CollectEmployee;
    }
}