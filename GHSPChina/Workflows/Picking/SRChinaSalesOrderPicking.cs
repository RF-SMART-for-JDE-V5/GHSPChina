using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using ICS.SRChina.Wireless.JDE.ERPI;

namespace ICS.SRChina.Wireless.JDE.Workflow.Picking
{
    #region Usings

    using System;
    //using System.Core;
    using ICS.Database.TableObjects.JDE;
    using ICS.Database.TableObjects.RFS;
    using ICS.Display;
    using ICS.ERPI.JDE;
    using ICS.Utilities;
    using ICS.Workflow.Base.WorkflowDesign.Components;
    using SRChinaJDETables = ICS.SRChina.Wireless.JDE.Database.TableObjects;

    #endregion

    public partial class SRChinaSalesOrderPicking : RFSWorkflowDesigner
    {
        #region Fields

        #region Data

        #endregion

        #region DataTable

        private DataTable tableSOItemData;

        public DataTable SOItemDataTable
        {
            get
            {
                if (tableSOItemData != null && tableSOItemData.Columns.Contains("ShortItemNumber") &&
                    tableSOItemData.Columns.Contains("OrderQuantity") &&
                    tableSOItemData.Columns.Contains("ScannedQuantity"))
                {
                    return tableSOItemData;
                }

                tableSOItemData = new DataTable("SOItemDataTable");
                tableSOItemData.Columns.Add(new DataColumn("SalesOrderNumber", typeof(double)));
                tableSOItemData.Columns.Add(new DataColumn("SalesOrderLineNumber", typeof(double)));
                tableSOItemData.Columns.Add(new DataColumn("Location", typeof(string)));
                tableSOItemData.Columns.Add(new DataColumn("ShortItemNumber", typeof(double)));
                tableSOItemData.Columns.Add(new DataColumn("LotNumber", typeof(string)));
                tableSOItemData.Columns.Add(new DataColumn("OrderQuantity", typeof(double)));
                tableSOItemData.Columns.Add(new DataColumn("ScannedQuantity", typeof(double)));
                return tableSOItemData;
            }
        }

        private void GenerateSalesOrderTable(string sSONumber)
        {
            Log.Debug("ENTER GenerateSalesOrderTable");
            double tempOrder = sSONumber.GetDouble();
            SOItemDataTable.Rows.Clear();
            using (var tempSOTable = new SRChinaJDETables.F4211(DataAccess, "temp"))
            {
                if (tempSOTable.DataTableFetch(_BranchPlant, tempOrder, _SalesOrderTable.SDLOCN, _SalesOrderTable.SDLITM, _IncludeStatus))
                {
                    Log.Debug("ENTER GenerateSalesOrderTable -  ifPurchaseOrderDetailTable.DataTableFetch");
                    foreach (DataRow row in tempSOTable.Rows)
                    {
                        tempSOTable.CurrentRow = row;
                        DataRow dr = SOItemDataTable.NewRow();
                        Log.Debug(
                            "ENTER GenerateSalesOrderTable -  ifPurchaseOrderDetailTable.DataTableFetch - Foreach");
                        dr["SalesOrderNumber"] = tempSOTable.SDDOCO;
                        dr["SalesOrderLineNumber"] = row["SDLNID"].ToString();
                        dr["Location"] = tempSOTable.SDLOCN;
                        dr["ShortItemNumber"] = tempSOTable.SDITM;
                        dr["LotNumber"] = tempSOTable.SDLOTN.Trim();
                        dr["OrderQuantity"] = tempSOTable.SDUORG;
                        dr["ScannedQuantity"] = 0;
                        SOItemDataTable.Rows.Add(dr);
                    }

                    tempSOTable.CurrentRow = tempSOTable.Rows[0];
                    Log.Debug(LogDataTable(SOItemDataTable));
                }
            }

            Log.Debug("ENTER GenerateSalesOrderTable -  AFTER ifPurchaseOrderDetailTable.DataTableFetch");
            SOItemDataTable.AcceptChanges();
        }

        private bool GetLineNumber(double shortItemNumber, string location, string lotNumber)
        {
            Log.Debug("GetLineNumber");
            double tempLineNumber;
            foreach (
                DataRow row in
                SOItemDataTable.Rows.Cast<DataRow>()
                    .Where(dr => Double.Parse(dr["ShortItemNumber"].ToString()) == shortItemNumber && dr["LotNumber"].ToString().Contains(lotNumber)
                                                                                                   && dr["Location"].ToString().Contains(location)))
            {
                tempLineNumber = Double.Parse(row["SalesOrderLineNumber"].ToString());
                Log.Debug("tempLineNumber: " + tempLineNumber);
                _LineNumber = tempLineNumber;
                return true;
            }

            return false;
        }

        private void UpdateQuantityScanned(double shortItemNumber, string lotNumber, double lineNumber, double quantity)
        {
            Log.Debug("shortItemNumber: " + shortItemNumber);
            Log.Debug("lotNumber: " + lotNumber);
            Log.Debug("lineNumber: " + lineNumber);
            double tempSOLine;
            Log.Debug("ENTER UpdateQuantityScanned");
            foreach (
                DataRow row in
                SOItemDataTable.Rows.Cast<DataRow>()
                    .Where(dr => Double.Parse(dr["ShortItemNumber"].ToString()) == shortItemNumber && dr["LotNumber"].ToString().Contains(lotNumber)
                                                                                                   && Double.Parse(dr["SalesOrderLineNumber"].ToString()) == lineNumber))
            {
                Log.Debug("ENTER UpdateQuantityScanned - ForEach");
                Log.Debug("lineNumber: " + lineNumber);
                Log.Debug("row[SalesOrderLineNumber] as a String: " + row["SalesOrderLineNumber"]);
                tempSOLine = Double.Parse(row["SalesOrderLineNumber"].ToString());
                Log.Debug("row[SalesOrderLineNumber] after Double.Parse: " + tempSOLine);
                row["ScannedQuantity"] = row["ScannedQuantity"].GetDouble() + quantity;
            }

            SOItemDataTable.AcceptChanges();
            Log.Debug("ENTER UpdateQuantityScanned - AFTER Foreach");
        }

        private bool CheckScannedVsExpectedRow()
        {
            return
                SOItemDataTable.Rows.Cast<DataRow>()
                    .Any(row => row["ScannedQuantity"].GetDouble() > row["OrderQuantity"].GetDouble());
        }

        private void UpdateQuantityScannedSubtract(double shortItemNumber, string lotNumber, double lineNumber, double quantity)
        {
            Log.Debug("ENTER UpdateQuantityScanned");
            foreach (
                DataRow row in
                SOItemDataTable.Rows.Cast<DataRow>()
                    .Where(dr => Double.Parse(dr["ShortItemNumber"].ToString()) == shortItemNumber && dr["LotNumber"].ToString().Contains(lotNumber)
                                                                                                   && Double.Parse(dr["SalesOrderLineNumber"].ToString()) == lineNumber))
            {
                Log.Debug("ENTER UpdateQuantityScannedSubtract - ForEach");
                row["ScannedQuantity"] = row["ScannedQuantity"].GetDouble() - quantity;
            }

            Log.Debug("ENTER UpdateQuantityScannedSubtract - AFTER Foreach");
        }

        private bool CheckScannedVsExpectedAll()
        {
            Log.Debug("ENTER CheckScannedVsExpectedAll");
            return
                SOItemDataTable.Rows.Cast<DataRow>()
                    .All(row => row["ScannedQuantity"].GetDouble() == row["OrderQuantity"].GetDouble());
        }

        public static string LogDataTable(DataTable value)
        {
            using (StringWriter writer = new StringWriter())
            {
                using (DataSet ds = new DataSet())
                {
                    ds.Locale = CultureInfo.InvariantCulture;
                    ds.Tables.Add(value.Copy());
                    ds.Tables[0].Constraints.Clear();

                    ds.WriteXml(writer, XmlWriteMode.IgnoreSchema);
                }

                return writer.ToString();
            }
        }

        #endregion

        #region Tables

        private RFS_DataManipulator tableDataManipulator;

        public RFS_DataManipulator _DataManipulatorTable
        {
            get { return tableDataManipulator ?? (tableDataManipulator = new RFS_DataManipulator(DataAccess, "")); }
        }

        private RFS_PrinterDefinition tablePrinterDefinition;

        public RFS_PrinterDefinition _printersTable
        {
            get { return tablePrinterDefinition ?? (tablePrinterDefinition = new RFS_PrinterDefinition(DataAccess, "")); }
        }

        private F0005 tableUserDefinedCodes;

        public F0005 _UserDefinedCodesTable
        {
            get { return tableUserDefinedCodes ?? (tableUserDefinedCodes = new F0005(DataAccess, "")); }
        }

        private F0006 tableBranchPlant;

        public F0006 _BranchPlantTable
        {
            get { return tableBranchPlant ?? (tableBranchPlant = new F0006(DataAccess, "")); }
        }

        private F0101 tableAddress;

        public F0101 _AddressTable
        {
            get { return tableAddress ?? (tableAddress = new F0101(DataAccess, "")); }
        }

        private F4100 tableLocationMaster;

        public F4100 _LocationMasterTable
        {
            get { return tableLocationMaster ?? (tableLocationMaster = new F4100(DataAccess, "")); }
        }

        private F4101 tableItemMaster;

        public F4101 _ItemMasterTable
        {
            get { return tableItemMaster ?? (tableItemMaster = new F4101(DataAccess, "")); }
        }

        private F4102 tableItemBranch;

        public F4102 _ItemBranchTable
        {
            get { return tableItemBranch ?? (tableItemBranch = new F4102(DataAccess, "")); }
        }

        private SRChinaJDETables.F4104 tableItemCrossReference;

        public SRChinaJDETables.F4104 _ItemCrossReferenceTable
        {
            get { return tableItemCrossReference ?? (tableItemCrossReference = new SRChinaJDETables.F4104(DataAccess, "")); }
        }

        private F4108 tableLotMaster;

        public F4108 _LotMasterTable
        {
            get { return tableLotMaster ?? (tableLotMaster = new F4108(DataAccess, "")); }
        }

        private SRChinaJDETables.F460131 tableStandardPackRules;

        public SRChinaJDETables.F460131 _StandardPackRulesTable
        {
            get { return tableStandardPackRules ?? (tableStandardPackRules = new SRChinaJDETables.F460131(DataAccess, "")); }
        }

        private SRChinaJDETables.F4211 tablesalesOrder;

        public SRChinaJDETables.F4211 _SalesOrderTable
        {
            get { return tablesalesOrder ?? (tablesalesOrder = new SRChinaJDETables.F4211(DataAccess, "")); }
        }

        private SRChinaJDETables.F59RF480 tableSRChinaBoxLabels;

        public SRChinaJDETables.F59RF480 _SRChinaBoxLabelsTable
        {
            get { return tableSRChinaBoxLabels ?? (tableSRChinaBoxLabels = new SRChinaJDETables.F59RF480(DataAccess, "")); }
        }

        #endregion

        #region ERPI

        ICS.ERPI.JDE.SalesOrderPick _pickTx = new SalesOrderPick();

        private ICS.SRChina.Wireless.JDE.ERPI.RFS_TxPSUpdate RFS_TxPSUpdate = new RFS_TxPSUpdate();

        #endregion

        #region Workflow options

        private string[] wfoAllowedDocTypes;

        public string[] _AllowedDocTypes
        {
            get
            {
                return wfoAllowedDocTypes
                       ?? (wfoAllowedDocTypes = WFOptions.RetrieveOptionValue("AllowedDocTypes")
                           .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        private string[] wfoAllowedHoldCodes;

        public string[] _AllowedHoldCodes
        {
            get
            {
                return wfoAllowedHoldCodes
                       ?? (wfoAllowedHoldCodes = WFOptions.RetrieveOptionValue("AllowedHoldCodes")
                           .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        private string wfoTransferP4113Version;

        public string _TransferP4113Version
        {
            get
            {
                return wfoTransferP4113Version
                       ?? (wfoTransferP4113Version = WFOptions.RetrieveOptionValue("TransferP4113Version"));
            }
        }

        private bool? wfoAllowOverrideHardCommits;

        public bool _AllowOverrideHardCommits
        {
            get
            {
                return (bool) (wfoAllowOverrideHardCommits ??
                               (wfoAllowOverrideHardCommits =
                                   WFOptions.GetYesNoWFOptionValue("AllowOverrideHardCommits", false)));
            }
        }

        private string wfoEmailZeroPick;

        public string _EmailZeroPick
        {
            get
            {
                return wfoEmailZeroPick
                       ?? (wfoEmailZeroPick = WFOptions.RetrieveOptionValue("EmailZeroPick"));
            }
        }

        private string wfoEmailE1Error;

        public string _EmailE1Error
        {
            get
            {
                return wfoEmailE1Error
                       ?? (wfoEmailE1Error = WFOptions.RetrieveOptionValue("EmailE1Error"));
            }
        }

        private string wfoOrderBy;

        public string _OrderBy
        {
            get
            {
                return wfoOrderBy
                       ?? (wfoOrderBy = WFOptions.RetrieveOptionValue("OrderBy"));
            }
        }

        private string wfoPickManagerMode;

        public string _PickManagerMode
        {
            get
            {
                return wfoPickManagerMode
                       ?? (wfoPickManagerMode = WFOptions.RetrieveOptionValue("PickManagerMode"));
            }
        }

        private string wfoAvailabilityCheck;

        public string _AvailabilityCheck
        {
            get
            {
                return wfoAvailabilityCheck
                       ?? (wfoAvailabilityCheck = WFOptions.RetrieveOptionValue("AvailabilityCheck"));
            }
        }

        private int? wfoPickManagerModer;

        public int _PickManagerModer
        {
            get
            {
                return (int) (wfoPickManagerModer ??
                              (wfoPickManagerModer =
                                  WFOptions.GetInt32WFOptionValue("PickManagerModer", 0)));
            }
        }

        private bool? wfoSyncTransaction;

        public bool _SyncTransaction
        {
            get
            {
                return (bool) (wfoSyncTransaction ??
                               (wfoSyncTransaction =
                                   WFOptions.GetYesNoWFOptionValue("SyncTransaction", true)));
            }
        }

        private double? wfoZeroPickNextStatus;

        public double _ZeroPickNextStatus
        {
            get
            {
                return (double) (wfoZeroPickNextStatus ??
                                 (wfoZeroPickNextStatus =
                                     WFOptions.GetDoubleWFOptionValue("ZeroPickNextStatus", 555)));
            }
        }

        private int? wfoMaxLabels;

        public int _MaxLabels
        {
            get
            {
                return (int) (wfoMaxLabels ??
                              (wfoMaxLabels =
                                  WFOptions.GetInt32WFOptionValue("MaxLabels", 999)));
            }
        }

        private string wfoDropFilePath;

        public string _DropFilePath
        {
            get
            {
                return wfoDropFilePath
                       ?? (wfoDropFilePath = WFOptions.RetrieveOptionValue("DropFilePath"));
            }
        }

        private string wfoP4205Version;

        public string _P4205Version
        {
            get
            {
                return wfoP4205Version
                       ?? (wfoP4205Version = WFOptions.RetrieveOptionValue("P4205Version"));
            }
        }

        private string wfoDefaultUOM;

        public string _DefaultUOM
        {
            get
            {
                return wfoDefaultUOM
                       ?? (wfoDefaultUOM = WFOptions.RetrieveOptionValue("DefaultUOM"));
            }
        }

        private string[] wfoIncludeStatus;

        public string[] _IncludeStatus
        {
            get
            {
                return (wfoIncludeStatus ??
                        (wfoIncludeStatus =
                            WFOptions.RetrieveOptionValue("IncludeStatus").Split(new char[] {' '})));
            }
        }

        private bool? wfoUniqueDocNumber;

        public bool _UniqueDocNumber
        {
            get
            {
                return (bool) (wfoUniqueDocNumber ??
                               (wfoUniqueDocNumber =
                                   WFOptions.GetYesNoWFOptionValue("UniqueDocNumber", false)));
            }
        }

        private string wfoAllowedLotHoldCodes;

        public string _AllowedLotHoldCodes
        {
            get
            {
                return wfoAllowedLotHoldCodes
                       ?? (wfoAllowedLotHoldCodes = WFOptions.RetrieveOptionValue("AllowedLotHoldCodes"));
            }
        }

        #endregion

        #endregion

        #region Constructors

        public SRChinaSalesOrderPicking(UserSession userSession) :
            base(ref userSession)
        {
            InitializeComponent();
            Transactions.ERP = "PeopleSoft";
        }

        public SRChinaSalesOrderPicking()
            : base()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        #region Data

        private string _BranchPlant;

        private string _DetailBranchPlant;

        private double _PickSlipNumber;

        private string _StagingLocation;

        private string _Location;

        private string _FromLocation;

        private string _VerifyLocation;

        private string _LabelID;

        private string _LastLabelID;

        private double _OrderNumber;

        private string _ItemNumber;

        private string _PrinterAddress;

        private string _DocType;

        private string _Lot;

        private double _ShortItemNumber;

        private double _LineNumber;

        private double _Quantity;

        private string _UnitOfMeasure;

        private string _FromLocationPrint;

        private string _CustomerPONumber;

        private string _InvoiceNumber;

        private string _CustomerPartNumber;

        private double _NumberOfPacks;

        private double _AddressNumber;

        private double _AddressShipTo;

        private string _ContainerType;

        private List<string> _LabelIDList = new List<string>();

        private string[] _StageLocationStatusCodes = null;

        #endregion

        #region Tables

        #endregion

        #region Workflow options

        #endregion

        #endregion

        #region Override Methods

        protected override void Initialize()
        {
        }

        public override void Dispose()
        {
            ScreenMap.ClearAllWorkflows();
            base.Dispose();
        }

        #endregion

        #region Screen methods

        #region CollectBranch

        private bool CollectBranch_ValidateMethod(string sInput)
        {
            if (sInput.Trim().Length <= 0) return true;
            _BranchPlantTable.ResetAll();
            _BranchPlantTable.FetchBranchPlants();
            _BranchPlantTable.BranchPlant = sInput;

            if (_BranchPlantTable.ValidBranchPlant())
            {
                _BranchPlant = _DataManipulatorTable.ManipulateDataForDisplay("F0006", "MCMCU",
                    _BranchPlantTable.MCMCU);
                _DetailBranchPlant = _BranchPlant;
                ScreenMap.Add("Branch", _BranchPlant);
                CollectBranch.ScreenInput = _BranchPlant.Trim();
            }
            else
            {
                throw new RFS_Exception("BRANCH");
            }

            Next(CollectPickSlip);
            return true;
        }

        private void _BranchPlantList_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _BranchPlantTable.ValidBranchPlant();
            _BranchPlantList.DataColumnForValidation = "MCMCU";
            _BranchPlantList.Table = _BranchPlantTable;
        }

        #endregion

        #region CollectPickSlip

        private bool CollectPickSlip_ValidateMethod(string sInput)
        {
            _SalesOrderTable.FetchPickSlips(_BranchPlant, _IncludeStatus, _AllowedDocTypes, UserSession.UserId, _PickManagerMode);
            try
            {
                double dPickSlip = double.Parse(sInput);
            }
            catch
            {
                throw new RFS_Exception("NotNumeric");
            }

            if (!_SalesOrderTable.ValidPickSlip(_BranchPlant, sInput, _IncludeStatus, _AllowedDocTypes, UserSession.UserId, _PickManagerMode, _OrderBy))
            {
                if (!_SalesOrderTable.ValidPickSlip(_BranchPlant, sInput, null, null, UserSession.UserId, "0"))
                {
                    throw new RFS_Exception("INVALIDPickSlip");
                }
                else
                {
                    if (!isValidDocType(_SalesOrderTable.SDDCTO))
                    {
                        throw new RFS_Exception("INVALIDORDERTYPE");
                    }
                    else
                    {
                        throw new RFS_Exception("NoOpenLines");
                    }
                }
            }

            /*if (_OrderBy.Trim().Length > 0)
            {
                _SalesOrderTable.ThisDataTable.DefaultView.Sort = _OrderBy;
            }
            else
            {
                _SalesOrderTable.ThisDataTable.DefaultView.Sort = " LMWSQQ, SDLOCN";
            }
            _SalesOrderTable.CurrentRow = _SalesOrderTable.ThisDataTable.DefaultView[0].Row;
            */
            _OrderNumber = _SalesOrderTable.SDDOCO;
            Log.Debug("GenerateSalesOrderTable");
            GenerateSalesOrderTable(_SalesOrderTable.SDDOCO.ToString());
            Next(CollectPrinter);
            return true;
        }

        private bool isValidDocType(string docType)
        {
            if (_AllowedDocTypes == null || _AllowedDocTypes.Length == 0 || _AllowedDocTypes[0] != "" || _AllowedDocTypes[0] != "*")
            {
                return true;
            }

            // determine whether document type of order is allowed
            // based on the workflow options.

            bool found = false;

            for (int i = 0; i < _AllowedDocTypes.Length; i++)
            {
                if (_AllowedDocTypes[i].ToString().Trim().Length > 0)
                {
                    if (docType.ToUpper().Trim() == _AllowedDocTypes[i].ToString().ToUpper().Trim())
                    {
                        found = true;
                    }
                }
                else
                {
                    found = true;
                }
            }

            return (found);
        }

        private void F4211PickSlip_List_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _SalesOrderTable.FetchPickSlips(_BranchPlant, _IncludeStatus, _AllowedDocTypes, UserSession.UserId, _PickManagerMode);
            F4211PickSlip_List.DataColumnForValidation = "SDPSN";
            F4211PickSlip_List.Table = _SalesOrderTable;
        }

        #endregion

        #region CollectPrinter

        private bool CollectPrinter_ValidateMethod(string sInput)
        {
            if (sInput.Trim().Length == 0)
            {
                CollectPrinter.ScreenInput = "";
            }
            else
            {
                // Validating that the printer exists for the environment in which they are logged.
                if (this.PrintSystem.ValidPrinter(sInput))
                {
                    CollectPrinter.ScreenInput = sInput;
                    _PrinterAddress = PrintSystem.PrinterAddress;
                    ScreenMap.Add("Printer", sInput);
                } // end if
                else
                {
                    throw new RFS_Exception(this.PrintSystem.ErrorMessageVocabulary);
                } // end else
            }

            Next(CollectStagingLocation);
            return true;
        }

        private void _printerList_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            Printer_List.Table = _printersTable;
            Printer_List.DataColumnForValidation = "PrinterName";
            _printersTable.FetchPrintersByUserWorkflow(UserSession.UserId, WorkflowID);
        }

        private void _collectPrinter_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            if ((!String.IsNullOrWhiteSpace(PrintSystem.DefaultPrinter)) &&
                (PrintSystem.ValidPrinter(PrintSystem.DefaultPrinter)))
            {
                ScreenHandler.DefaultText = PrintSystem.DefaultPrinter;
            }
            else
            {
                Error("LabelFormat");
            }
        }

        #endregion

        #region PrintBarCodeLabels

        private bool PrintBarCodeLabels_ValidateMethod(string sInput)
        {
            int numberOfLabels = 0;
            try
            {
                numberOfLabels = int.Parse(sInput);
            }
            catch
            {
                throw new RFS_Exception("NotNumeric");
            }

            if (numberOfLabels < 0)
            {
                throw new RFS_Exception("NegativeNotAllowed");
            }

            /*if (numberOfLabels > _maxLabels)
            {
                throw new RFS_Exception("GreaterThanMax");
            }*/
            if (numberOfLabels > 0)
            {
                printBarCode(numberOfLabels);
            }

            if (_SalesOrderTable.FetchOutstandingPicksLocation(_BranchPlant, _FromLocation, _PickSlipNumber, _IncludeStatus, _AllowedDocTypes,
                    UserSession.UserId, _PickManagerMode))
            {
                Log.Debug("IF _SalesOrderTable.FetchOutstandingPicksLocation");
                _SalesOrderTable.ValidPickSlip(_BranchPlant, _PickSlipNumber.ToString(), _IncludeStatus,
                    _AllowedDocTypes, UserSession.UserId, _PickManagerMode, _OrderBy);
                GenerateSalesOrderTable(_OrderNumber.ToString());
                RestartLoop(CollectLabelID);
            }
            else if (_SalesOrderTable.FetchOutstandingPicks(_BranchPlant, _PickSlipNumber, _IncludeStatus,
                         _AllowedDocTypes,
                         UserSession.UserId, _PickManagerMode))
            {
                Log.Debug("Else If _SalesOrderTable.FetchOutstandingPicks");
                _SalesOrderTable.ValidPickSlip(_BranchPlant, _PickSlipNumber.ToString(), _IncludeStatus,
                    _AllowedDocTypes, UserSession.UserId, _PickManagerMode, _OrderBy);
                _SalesOrderTable.CurrentRow = _SalesOrderTable.ThisDataTable.DefaultView[0].Row;
                GenerateSalesOrderTable(_OrderNumber.ToString());
                _FromLocation = _SalesOrderTable.SDLOCN;
                ScreenMap.Add("FromLocation", _SalesOrderTable.SDLOCN);
                Next(VerifyLocation);
            }
            else
            {
                Log.Debug("Else RestartLoop(CollectPickSlip)");
                RestartLoop(CollectPickSlip);
            }

            return true;
        }

        private bool RemoveOtherLinesForPrint()
        {
            for (int i = 0; i < _SalesOrderTable.Rows.Count; i++)
            {
                if (_SalesOrderTable.CurrentRow != _SalesOrderTable.Rows[i])
                {
                    _SalesOrderTable.Rows[i].Delete();
                }

                _SalesOrderTable.ThisDataTable.AcceptChanges();
            }

            return true;
        }

        private void printBarCode(int numberOfLabels)
        {
            Log.Debug("printBarCode");
            int i = 0;
            while (i < numberOfLabels)
            {
                Log.Debug("printBarCode - While");
                Log.Debug("i = " + i);
                foreach (string labelID in _LabelIDList)
                {
                    Log.Debug("printBarCode - Foreach");
                    _SRChinaBoxLabelsTable.LabelIDFetch(labelID);
                    /*if (
                        !_StandardPackRulesTable.PerPackFetch(_BranchPlant, _ShortItemNumber, _AddressNumber,
                            _AddressShipTo))
                    {
                        throw new RFS_Exception("ADDRESS");
                    }*/
                    Log.Debug("_CustomerPONumber = " + _CustomerPONumber);
                    if (!_ItemCrossReferenceTable.CustomerPartNumberFetch(_ItemNumber, _AddressNumber))
                    {
                        Log.Debug("_ItemCrossReferenceTable");
                        _CustomerPONumber = "";
                    }

                    Log.Debug("_CustomerPONumber = " + _CustomerPONumber);
                    if (!_AddressTable.RetrieveAddress(_SalesOrderTable.SDSHAN.ToString()))
                    {
                        throw new RFS_Exception("ADDRESS");
                    }

                    //_NumberOfPacks = _SalesOrderTable.SDUORG/_StandardPackRulesTable.SPSTPK;
                    _CustomerPartNumber = _ItemCrossReferenceTable.IVCITM;
                    _Quantity = _SRChinaBoxLabelsTable.RFSOQS;
                    _UnitOfMeasure = _SRChinaBoxLabelsTable.RFUOM;
                    _ItemNumber = _SRChinaBoxLabelsTable.RFLITM.ToString();
                    _Lot = _SRChinaBoxLabelsTable.RFLOTN;
                    RemoveOtherLinesForPrint();
                    PrintSystem.Reset();
                    PrintSystem.Branch = _BranchPlant.Trim();
                    PrintSystem.ItemNumber = _ItemNumber;
                    PrintSystem.Location = _FromLocation;
                    PrintSystem.ContainerType = _AddressTable.ABAC21;

                    PrintSystem.DropFilePathOverride = WFOptions.RetrieveOptionValue("DropFilePath");
                    PrintSystem.PrinterAddress = _PrinterAddress = PrintSystem.PrinterAddress;

                    PrintSystem.PrinterName = CollectPrinter.ScreenInput;
                    PrintSystem.Copies = 1;

                    Log.Debug("After print variable set");
                    Log.Debug("_CustomerPONumber = " + _CustomerPONumber);
                    //PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_Container", _collectContainer.ScreenInput, false);
                    //double totalQuantity = _quantity + getItemLotQty(_collectFromLocation.ScreenInput, _collectLot.ScreenInput);
                    PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_TransactionQuantity", _Quantity.ToString(), false);
                    PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_UnitOfMeasure", _UnitOfMeasure, false);
                    PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_FromLocation", _FromLocationPrint, false);
                    PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_TransferLocation", _StagingLocation, false);
                    PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_PrimaryItemNumber", _ItemNumber, false);
                    PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_LotNumber", _Lot, false);
                    PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_CustomerPartNumber", _CustomerPartNumber, false);
                    //PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_NumberOfPacks", _NumberOfPacks.ToString(), false);
                    Log.Debug("_CustomerPONumber = " + _CustomerPONumber);
                    PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_CustomerPONumber", _CustomerPONumber, false);
                    PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_InvoiceNumber", _InvoiceNumber, false);
                    PrintSystem.ExtraPrintData.NewRow();

                    Log.Debug("After PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData");
                    if (!PrintSystem.Print())
                    {
                        DisplayMessage(PrintSystem.ErrorMessageVocabulary);
                    }
                }

                i++;
            }

            _LabelIDList.Clear();
        }

        #endregion

        #region CollectStagingLocation

        private bool CollectStagingLocation_ValidateMethod(string sInput)
        {
            _LocationMasterTable.MCU = _BranchPlant;
            _LocationMasterTable.inLOCN = sInput;
            if (!_LocationMasterTable.ValidateLocation())
            {
                throw new RFS_Exception("LOCATION");
            }

            //TODO - Figuring out policy on stagin location.  Changed to false.
            if (!_LocationMasterTable.ValidateTransferLocation(_BranchPlant, _LocationMasterTable.LMLOCN, false))
            {
                throw new RFS_Exception("NotASOStageLocation");
            }

            // Check stating location status codes. If *ANY OTHER* F4211 line
            //  exists with this status code for collected staging location, 
            //  return an error.  This is to prevent mixing different orders in 
            //  the same location.  However, adding more of the same order is OK,
            //  so we want to make sure we search for lines of a different order/type.
            if ((_StageLocationStatusCodes != null) &&
                (_StageLocationStatusCodes.Length > 0) &&
                (_StageLocationStatusCodes[0].Trim() != string.Empty))
            {
                using (F4211 tblSODetails = new F4211(UserSession.DataAccess, "StageLocCheck_"))
                {
                    tblSODetails.ResetAll();
                    tblSODetails.SDMCU = _BranchPlant;
                    tblSODetails.SDLOCN = _LocationMasterTable.LMLOCN;
                    tblSODetails.Where("AND NOT (", tblSODetails.Columns.SDDOCO, ICS.Database.Condition.EqualTo, _OrderNumber, "");
                    tblSODetails.Where("AND", tblSODetails.Columns.SDDCTO, ICS.Database.Condition.EqualTo, _SalesOrderTable.SDDCTO, ") ");
                    tblSODetails.Where("AND", tblSODetails.Columns.SDNXTR, _StageLocationStatusCodes, "");
                    if (tblSODetails.Fetch())
                    {
                        throw new RFS_Exception("LOCATIONNOTEMPTY");
                    }
                }
            }

            _StagingLocation = _LocationMasterTable.stbllocn;
            _FromLocation = _SalesOrderTable.SDLOCN;
            ScreenMap.Add("StagingLocation", _LocationMasterTable.sscnlocn);
            ScreenMap.Add("FromLocation", _SalesOrderTable.SDLOCN);
            Next(VerifyLocation);
            return true;
        }

        private void StagingLocationsList_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _LocationMasterTable.ResetAll();
            _LocationMasterList.DataColumnForValidation = "LMLOCN";
            _LocationMasterTable.ShowPickStagingLocations(_BranchPlant, _SalesOrderTable.SDDOCO, _SalesOrderTable.SDDCTO, _StageLocationStatusCodes);
            _LocationMasterList.Table = _LocationMasterTable;
        }

        #endregion

        #region VerifyFromLocation

        private bool VerifyLocation_ValidateMethod(string sInput)
        {
            Log.Debug("VerifyLocation_ValidateMethod");
            if (_LocationMasterTable.ValidateLocation(_BranchPlant, sInput))
            {
                Log.Debug("VerifyLocation_ValidateMethodIF - _LocationMasterTable.ValidateLocation");
                if (sInput.Equals(_SalesOrderTable.SDLOCN.Trim()))
                {
                    Log.Debug("VerifyLocation_ValidateMethod - If sInput.Equals(_FromLocation)");
                    UserSession.ScreenHandler.ScreenMap.Add("VerifyLocation", sInput);
                    _VerifyLocation = sInput;
                    Next(CollectLabelID);
                }
                else
                {
                    throw new RFS_Exception("LOCATION");
                }
            }
            else
            {
                Log.Debug("VerifyLocation_ValidateMethodELSE - _LocationMasterTable.ValidateLocation");
                throw new RFS_Exception("LOCATION");
            }

            return true;
        }

        #endregion

        #region CollectLabelID

        private void CollectLabelID_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            Log.Debug("CollectLabelID_LoadEvent");
            _ItemNumber = _SalesOrderTable.SDLITM.ToString();
            ScreenMap.Add("OrderNumber", _OrderNumber.ToString());
            ScreenMap.Add("ItemNumber", _ItemNumber);
            ScreenMap.Add("LastLabelID", _LastLabelID);
            ScreenMap.Add("VerifyLocation", _FromLocation);
        }

        private bool CollectLabelID_ValidateMethod(string sInput)
        {
            Log.Debug("CollectLabelID_ValidateMethod");
            if (!_SRChinaBoxLabelsTable.LabelIDFetch(sInput))
            {
                throw new RFS_Exception("LABELID");
            }

            Log.Debug("CollectLabelID_ValidateMethod - After LabelIDFetch");
            Log.Debug("sInput = " + sInput);
            Log.Debug("*****_LabelIDList******");
            foreach (string LabelID in _LabelIDList)
            {
                Log.Debug("LabelID = " + LabelID);
            }

            if (_LabelIDList.Contains(sInput))
            {
                throw new RFS_Exception("LABELID");
            }

            Log.Debug("CollectLabelID_ValidateMethod - After _LabelIDList.Contains");
            _ItemNumber = _SRChinaBoxLabelsTable.RFLITM;
            _ShortItemNumber = _SRChinaBoxLabelsTable.RFITM;
            _OrderNumber = _SalesOrderTable.SDDOCO;
            _PickSlipNumber = _SalesOrderTable.SDPSN;
            _CustomerPONumber = _SalesOrderTable.SDVR01;
            _InvoiceNumber = _SalesOrderTable.SDDOC.ToString();
            _AddressNumber = _SalesOrderTable.SDAN8;
            _AddressShipTo = _SalesOrderTable.SDSHAN;
            Log.Debug("_OrderNumber = " + _OrderNumber);
            Log.Debug("_ItemNumber = " + _ItemNumber);
            Log.Debug("_CustomerPONumber = " + _CustomerPONumber);
            if (!_SRChinaBoxLabelsTable.VerifyLabelIDSO(_ItemNumber, sInput))
            {
                throw new RFS_Exception("LABELID");
            }
            else
            {
                _Lot = _SRChinaBoxLabelsTable.RFLOTN;
                Log.Debug("_Lot = " + _Lot);
                if (!GetLineNumber(_ShortItemNumber, _FromLocation, _Lot))
                {
                    throw new RFS_Exception("LINENUMBER");
                }

                Log.Debug("_LineNumber: " + _LineNumber);
                if (
                    !_SalesOrderTable.VerifyLot(_BranchPlant, _OrderNumber, _ItemNumber,
                        _StageLocationStatusCodes, _LineNumber, _Lot))
                {
                    throw new RFS_Exception("LOT");
                }

                Log.Debug("_SalesOrderTable.SDLOTN = " + _SalesOrderTable.SDLOTN);
                if (_SRChinaBoxLabelsTable.RFRQCP.Length > 0)
                {
                    if (_SRChinaBoxLabelsTable.RFRQCP.Contains("1"))
                    {
                        throw new RFS_Exception("COMPLETIONFLAG");
                    }
                }

                Log.Debug("_LineNumber: " + _LineNumber);
                Log.Debug(LogDataTable(SOItemDataTable));
                Log.Debug("BEFORE UpdateQuantityScanned");
                UpdateQuantityScanned(_ShortItemNumber, _Lot, _LineNumber, _SRChinaBoxLabelsTable.RFSOQS);
                Log.Debug("CollectLabelID_ValidateMethod - Before CheckScannedVsExpectedAll");
                Log.Debug(LogDataTable(SOItemDataTable));
                if (CheckScannedVsExpectedRow())
                {
                    UpdateQuantityScannedSubtract(_ShortItemNumber, _Lot, _LineNumber, _SRChinaBoxLabelsTable.RFSOQS);
                    Log.Debug(LogDataTable(SOItemDataTable));
                    throw new RFS_Exception("OVERRECEIPT");
                }

                _LabelIDList.Add(sInput);
                if (CheckScannedVsExpectedAll())
                {
                    Log.Debug("CollectLabelID_ValidateMethod - Before SendToERP");
                    SendToERP();
                    Log.Debug("CollectLabelID_ValidateMethod - After SendToERP");
                    if (!Transactions.Error)
                    {
                        foreach (string LabelID in _LabelIDList)
                        {
                            _LabelID = LabelID;
                            UpdateSOCompletionFlag();
                        }

                        _ItemNumber = _SRChinaBoxLabelsTable.RFLITM;
                        _ShortItemNumber = _SRChinaBoxLabelsTable.RFITM;
                        _LastLabelID = _SRChinaBoxLabelsTable.RFLBN;
                        _DocType = _SRChinaBoxLabelsTable.RFDCTO;
                        ScreenMap.Add("LastLabelID", _LastLabelID);
                        Next(PrintBarCodeLabels);
                        return true;
                    }

                    Log.Debug("CollectLabelID_ValidateMethod - After UpdateWOCompletionFlag");
                }

                Log.Debug("CollectLabelID_ValidateMethod - After CheckScannedVsExpectedAll");
                _ItemNumber = _SRChinaBoxLabelsTable.RFLITM;
                _ShortItemNumber = _SRChinaBoxLabelsTable.RFITM;
                _LastLabelID = _SRChinaBoxLabelsTable.RFLBN;
                _DocType = _SRChinaBoxLabelsTable.RFDCTO;
                ScreenMap.Add("LastLabelID", _LastLabelID);
            }

            RestartLoop(CollectLabelID);
            return true;
        }

        private bool CollectLabelID_F2Method(RFSScreenControl sender, ScreenEventArgs e)
        {
            Next(ShortPickWarning);
            return true;
        }

        private bool CollectLabelID_F6Method(RFSScreenControl sender, ScreenEventArgs e)
        {
            Next(PickList);
            return true;
        }

        #endregion

        #region ShortPickWarning

        private bool ShortPickWarning_ValidateMethod(string sInput)
        {
            if (!sInput.GetYesNo())
            {
                Back();
                return true;
            }

            SendToERP();
            if (!Transactions.Error)
            {
                foreach (string LabelID in _LabelIDList)
                {
                    _LabelID = LabelID;
                    UpdateSOCompletionFlag();
                }

                Next(PrintBarCodeLabels);
            }

            //RestartLoop(VerifyLocation);
            return true;
        }

        #endregion

        #region PickList

        private void PickList_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            if (DataAccess.Data.UsersData.Tables.Contains("SOItemDataTable"))
            {
                DataAccess.Data.UsersData.Tables.Remove("SOItemDataTable");
            }

            DataAccess.Data.UsersData.Tables.Add(SOItemDataTable);
        }

        #endregion

        #region SendToERP

        private void setSoldTo()
        {
            Log.Debug("setSoldTo");
            _pickTx.CustSoldTo = _SalesOrderTable.SDAN8.ToString();
            _AddressTable.RetrieveAddress(_SalesOrderTable.SDAN8.ToString());
            _pickTx.CustSoldToDescr = _AddressTable.ABALPH.Trim();
        }

        private void setShipTo()
        {
            Log.Debug("setShipTo");
            _pickTx.CustShipTo = _SalesOrderTable.SDSHAN.ToString();
            _AddressTable.RetrieveAddress(_SalesOrderTable.SDSHAN.ToString());
            _pickTx.CustShipToDescr = _AddressTable.ABALPH.Trim();
        }

        private void setFromLocation()
        {
            Log.Debug("setFromLocation");
            _pickTx.Location = _VerifyLocation; // Keep format the same as in E1 DB.
            if (_LocationMasterTable.ValidateLocation(_SalesOrderTable.SDMCU.Trim(), _SalesOrderTable.SDLOCN))
            {
                _pickTx.FromLocnPutZone = _LocationMasterTable.LMPZON.Trim();
                _pickTx.FromLocnPickZone = _LocationMasterTable.LMKZON.Trim();
                _pickTx.FromLocnReplZone = _LocationMasterTable.LMZONR.Trim();
                _pickTx.FromLocnDimGroup = _LocationMasterTable.LMSTY1.Trim();
            }
        }

        private void setToLocation()
        {
            Log.Debug("setToLocation");
            _pickTx.ToLocation = _StagingLocation; // Keep format the same as in E1 DB.
            if (_LocationMasterTable.ValidateLocation(_SalesOrderTable.SDMCU.Trim(), _StagingLocation))
            {
                _pickTx.ToLocnPutZone = _LocationMasterTable.LMPZON.Trim();
                _pickTx.ToLocnPickZone = _LocationMasterTable.LMKZON.Trim();
                _pickTx.ToLocnReplZone = _LocationMasterTable.LMZONR.Trim();
                _pickTx.ToLocnDimGroup = _LocationMasterTable.LMSTY1.Trim();
            }
        }

        private void setItem()
        {
            Log.Debug("setItem");
            _pickTx.PrimaryItemNumber = _ItemNumber;
            _pickTx.SecondItemNumber = _SalesOrderTable.SDLITM.Trim();
            _pickTx.ShortItemNumber = _SalesOrderTable.SDITM.ToString();
            _pickTx.ThirdItemNumber = _ItemMasterTable.IMAITM.Trim();
            _pickTx.ItemDescription1 = _ItemMasterTable.IMDSC1.Trim();
            _pickTx.ItemDescription2 = _ItemMasterTable.IMDSC2.Trim();
            _ItemBranchTable.ValidateItemBranch(_SalesOrderTable.SDMCU.Trim(), _SalesOrderTable.SDITM);
            _pickTx.ItemDimGroup = _ItemBranchTable.IBPRP6.Trim();
            _pickTx.ItemLotProcType = _ItemBranchTable.IBSRCE;
        }

        private void SendToERP()
        {
            Log.Debug("SendToERP");
            Transactions.Add(_pickTx);
            _pickTx.Initialize();
            //int rowCount = 0;
            int i = 0;

            foreach (DataRow dataRow in SOItemDataTable.Rows)
            {
                Log.Debug("SendToERP - Foreach");
                //double item = Double.Parse(dataRow["ShortItemNumber"].ToString());
                double quantityRemaining = Double.Parse(dataRow["ScannedQuantity"].ToString());
                double line = Convert.ToDouble(_SalesOrderTable.Columns.SDLNID.FormatColumn(dataRow["SalesOrderLineNumber"]));

                if (_SalesOrderTable.DataTableExplodeFetch(_BranchPlant, _OrderNumber, _SalesOrderTable.SDKCOO, line, _IncludeStatus))
                {
                    _ItemNumber = _SalesOrderTable.SDITM.ToString();
                    _ItemMasterTable.FetchItem(_SalesOrderTable.SDITM);

                    //while (quantityRemaining > 0)
                    //{
                    //double tempQty;
                    //Log.Debug("SendToERP - Foreach - while quantityRemaining > 0");
                    /*if (_SalesOrderTable.Rows.Count == rowCount)
                    {
                        Log.Debug("SendToERP - Foreach - while - if _SalesOrderTable.Rows.Count == rowCount");
                        tempQty = quantityRemaining;
                    }
                    else
                    {
                        Log.Debug("SendToERP - Foreach - while - ifElse");
                        tempQty = Math.Min(_SalesOrderTable.SDUORG, quantityRemaining);
                    }*/

                    //_pickTx.Quantity = tempQty.ToString();
                    //quantityRemaining -= tempQty;

                    _pickTx.Quantity = quantityRemaining.ToString();

                    setSoldTo();
                    setShipTo();
                    setFromLocation();
                    setToLocation();
                    setItem();
                    _pickTx.OrderNumber = _SalesOrderTable.SDDOCO.ToString();
                    _pickTx.OrderType = _SalesOrderTable.SDDCTO.Trim();
                    _pickTx.OrderCompany = _SalesOrderTable.SDKCOO.Trim();
                    //_pickTx.LineNumber = (dataRow["SalesOrderLineNumber"].GetDouble()/1000).ToString();
                    _pickTx.LineNumber = _SalesOrderTable.SDLNID.ToString();
                    _pickTx.BranchPlant = _SalesOrderTable.SDMCU.Trim();
                    _pickTx.Lot = dataRow["LotNumber"].ToString();
                    //_pickTx.Quantity = _SalesOrderTable.SDSOQS.ToString();
                    _pickTx.UOM = _SalesOrderTable.SDUOM.Trim();
                    _pickTx.RFUserID = UserSession.UserId;
                    if (i == 0)
                    {
                        _pickTx.Mode = "1";
                    }
                    else
                    {
                        _pickTx.Mode = "C";
                    }

                    _pickTx.TransDate_YYYYMMDD = System.DateTime.Now.ToString("yyyyMMdd");
                    _pickTx.P4205Version = WFOptions.RetrieveOptionValue("P4205Version");
                    _pickTx.ToLocation = _StagingLocation;
                    _pickTx.P4113Version = _TransferP4113Version.Trim();
                    _pickTx.ContainerID = _SalesOrderTable.SDCNID;
                    _pickTx.AddToBoundaryWithData();

                    //if (quantityRemaining > 0)
                    //{
                    //_SalesOrderTable.CurrentRow = _SalesOrderTable.Rows[rowCount];
                    //rowCount++;
                    //}
                    i++;
                    //}
                }
            }

            _pickTx.Mode = "2";
            _pickTx.AddToBoundaryWithData();
            _pickTx.Mode = "3";
            Log.Debug(LogDataTable(SOItemDataTable));
            Transactions.Send(WFOptions.RetrieveOptionValue("SyncTransaction"));
            Log.Debug("SendToERP Complete");
            _FromLocationPrint = _FromLocation;
            DisplayMessages();

            #region Old SendToERP

            /*if (_isFinished || !_cartonEnabled)
            {
                _pickTx.ManuallySplitSalesOrder = " ";
            }
            else
            {
                _pickTx.ManuallySplitSalesOrder = "1";
            }
            if (getPickedQty() == 0 || (!_cartonEnabled && !_forceLine))
            {
                _pickTx.ToCartonID = string.Empty;
                _pickTx.CartonQuantity = string.Empty;
                _pickTx.CustomerCartonId = string.Empty;
                _pickTx.CartonCode = string.Empty;
                _pickTx.CartonP4620Version = string.Empty;
            }
            else
            {
                _pickTx.Mode = "1";
                if (_forceLine && !_cartonEnabled)
                {
                    DataTable cartonTemp = _cartonRecords.DefaultView.ToTable(true, new string[] { "location", "container", "lot" });
                    foreach (DataRow dr in cartonTemp.Rows)
                    {
                        setCartonFromLocation(dr["location"].ToString());
                        _pickTx.ContainerID = dr["container"].ToString();
                        _pickTx.Lot = dr["lot"].ToString();
                        _pickTx.Quantity = ConvertToUOMQty(_collectUnitOfMeasure.ScreenInput,
                                                    _SalesOrderTable.SDUOM,
                                                    getItemLotContainerQty(dr["location"].ToString(), dr["lot"].ToString(), dr["container"].ToString())).ToString();

                        _pickTx.AddToBoundaryWithData();
                        _pickTx.Mode = "C";
                    }
                    cartonTemp.Dispose();
                }

                if (_cartonEnabled)
                {
                    _pickTx.SerialNumberType = _serialNumberType;
                    foreach (DataRow dr in _cartonRecords.Rows)
                    {
                        _pickTx.Pallet = dr["palletId"].ToString();
                        _pickTx.PalletType = dr["palletType"].ToString();

                        if (_jdeCartonTable.GetCarton(_SalesOrderTable.SDMCU.Trim(), dr["carton"].ToString()))
                        {
                            _pickTx.ToCartonID = _jdeCartonTable.CDCRID.ToString();
                            _pickTx.CartonCode = _jdeCartonTable.CDEQTY.Trim();
                        }
                        else
                        {
                            _pickTx.ToCartonID = string.Empty;
                            _pickTx.CartonCode = dr["cartonType"].ToString().Trim();
                        }
                        setCartonFromLocation(dr["location"].ToString());
                        _pickTx.ContainerID = dr["container"].ToString();
                        _pickTx.Lot = dr["lot"].ToString();
                        _pickTx.Quantity = ConvertToUOMQty(_collectUnitOfMeasure.ScreenInput,
                                                    _SalesOrderTable.SDUOM,
                                                    getItemLotContainerQty(dr["location"].ToString(), dr["lot"].ToString(), dr["container"].ToString())).ToString();

                        _pickTx.CartonQuantity = ConvertToUOMQty(_collectUnitOfMeasure.ScreenInput,
                                                   _SalesOrderTable.SDUOM, double.Parse(dr["quantity"].ToString())).ToString();
                        _pickTx.CustomerCartonId = dr["carton"].ToString().Trim();
                        _pickTx.CartonP4620Version = WFOptions.RetrieveOptionValue("CartonP4620Version");

                        _pickTx.AddToBoundaryWithData();
                        _pickTx.Mode = "C";
                    }
                }
                #region End Doc
                _pickTx.Mode = "2";
                #endregion
                #region Clear Cache
                _pickTx.AddToBoundaryWithData();
                _pickTx.Mode = "3";
                #endregion
            }*/

            /*if (!Transactions.Success)
            {
                SendE1Error();
            }*/
            //deletePickReservation();
            //_forceLine = false;

            #endregion
        }

        private void UpdateSOCompletionFlag()
        {
            Log.Debug("UpdateWOCompletionFlag");
            Transactions.Add(RFS_TxPSUpdate);
            RFS_TxPSUpdate.LabelName = _LabelID;
            RFS_TxPSUpdate.RFUserId = UserSession.UserId;
            RFS_TxPSUpdate.Mode = "S";
            Transactions.Send(WFOptions.GetYesNoWFOptionValue("SyncTransaction") ? "Y" : "N");
            DisplayMessages();
        }

        #endregion

        #endregion
    }
}