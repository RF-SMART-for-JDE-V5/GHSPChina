using System.Linq;
using System.Security.Policy;

namespace ICS.SRChina.Wireless.JDE.Workflow.ShopFloor
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
    using ICS.SRChina.Wireless.JDE.ERPI;
    using SRChinaJDETables = ICS.SRChina.Wireless.JDE.Database.TableObjects;

    #endregion

    public partial class SRChinaWorkOrderCompletion : RFSWorkflowDesigner
    {
        #region Fields

        #region Data

        #endregion

        #region Tables

        private RFS_DataManipulator tableDataManipulator;

        public RFS_DataManipulator _DataManipulatorTable
        {
            get { return tableDataManipulator ?? (tableDataManipulator = new RFS_DataManipulator(DataAccess, "")); }
        }

        private F0101 tableAddressBook;

        public F0101 _AddressBookTable
        {
            get { return tableAddressBook ?? (tableAddressBook = new F0101(DataAccess, "")); }
        }

        private F00095 tableJDEReservations;

        public F00095 _JDEReservationsTable
        {
            get { return tableJDEReservations ?? (tableJDEReservations = new F00095(DataAccess, "")); }
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

        private F4108 tableLotMaster;

        public F4108 _LotMasterTable
        {
            get { return tableLotMaster ?? (tableLotMaster = new F4108(DataAccess, "")); }
        }

        private SRChinaJDETables.F3112 tableRoutingInstructions;

        public SRChinaJDETables.F3112 _RoutingInstructionsTable
        {
            get { return tableRoutingInstructions ?? (tableRoutingInstructions = new SRChinaJDETables.F3112(DataAccess, "")); }
        }

        private F4801 tableWorkOrder;

        public F4801 _WorkOrderTable
        {
            get { return tableWorkOrder ?? (tableWorkOrder = new F4801(DataAccess, "")); }
        }

        private F41001 tableWarehouseConstants;

        public F41001 _WarehouseConstantsTable
        {
            get { return tableWarehouseConstants ?? (tableWarehouseConstants = new F41001(DataAccess, "")); }
        }

        private F41002 tableUnitOfMeasureConversion1;

        public F41002 _UnitOfMeasureConversionTable1
        {
            get { return tableUnitOfMeasureConversion1 ?? (tableUnitOfMeasureConversion1 = new F41002(DataAccess, "")); }
        }

        private F41003 tableUnitOfMeasureConversion2;

        public F41003 _UnitOfMeasureConversionTable2
        {
            get { return tableUnitOfMeasureConversion2 ?? (tableUnitOfMeasureConversion2 = new F41003(DataAccess, "")); }
        }

        private SRChinaJDETables.F59RF480 tableSRChinaBoxLabels;

        public SRChinaJDETables.F59RF480 _SRChinaBoxLabelsTable
        {
            get { return tableSRChinaBoxLabels ?? (tableSRChinaBoxLabels = new SRChinaJDETables.F59RF480(DataAccess, "")); }
        }

        #endregion

        #region ERPI

        private ICS.ERPI.JDE.WOCompletion _WOCompletionTransaction = new WOCompletion();

        private ICS.ERPI.JDE.WOTracking _WOTrackingTransaction = new WOTracking();

        private RFS_TxPSUpdate RFS_TxPSUpdate = new RFS_TxPSUpdate();

        #endregion

        #region Workflow options

        #endregion

        #endregion

        #region Constructors

        public SRChinaWorkOrderCompletion(UserSession userSession) :
            base(ref userSession)
        {
            InitializeComponent();
            Transactions.ERP = "PeopleSoft";
        }

        public SRChinaWorkOrderCompletion()
            : base()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        #region Data

        private string _BranchPlant;

        private string _DetailBranchPlant;

        private string _ShiftCode;

        private string _ToLocation;

        private string _LabelID;

        private string _ItemNumber = " ";

        private string _WorkOrderNumber = " ";

        private string _LastLabelID = " ";

        private string _EmployeeNumber;

        private string _UnitOfMeasure;

        private double _ShortItemNumber;

        private string _WorkCenter;

        private string _DocumentType;

        private string _CurrentOperationSequence;

        private string _Status;

        private string _LotNumber;

        private string _LotExpirationDate;

        private double _StopHHMM = 0;

        private double _StartHHMM = 0;

        private string _StartTime = " ";

        private string _StopTime = " ";

        private string _Hours;

        private string _TypeOfHours;

        private string _PayPoint = "B";

        private double _ConvertedTransactionQuantity;

        private string _ERPUpdateVersion;

        private DateTime _Today = DateTime.Now;

        private DateTime _ExpirationDate;

        #endregion

        #region Tables

        #endregion

        #region Workflow options

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

        private string[] wfoAllowedWorkOrderStatus;

        public string[] _AllowedWorkOrderStatus
        {
            get
            {
                return wfoAllowedWorkOrderStatus
                       ?? (wfoAllowedWorkOrderStatus = WFOptions.RetrieveOptionValue("AllowedWorkOrderStatus")
                           .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        private bool? wfoAllowOverComplete;

        public bool _AllowOverComplete
        {
            get
            {
                return (bool) (wfoAllowOverComplete ??
                               (wfoAllowOverComplete =
                                   WFOptions.GetYesNoWFOptionValue("AllowOverComplete", false)));
            }
        }

        private double? wfoExpirationDays;

        public double _ExpirationDays
        {
            get
            {
                return (int) (wfoExpirationDays ??
                              (wfoExpirationDays =
                                  WFOptions.GetInt32WFOptionValue("ExpirationDays", 1)));
            }
        }

        private string wfoERPCompleteUpdateVersion;

        public string _ERPCompleteUpdateVersion
        {
            get
            {
                return wfoERPCompleteUpdateVersion
                       ?? (wfoERPCompleteUpdateVersion = WFOptions.RetrieveOptionValue("ERPUpdateVersion"));
            }
        }

        private int? wfoMethodToRecordTime;

        public int _MethodToRecordTime
        {
            get
            {
                return (int) (wfoMethodToRecordTime ??
                              (wfoMethodToRecordTime =
                                  WFOptions.GetInt32WFOptionValue("MethodToRecordTime", 2)));
            }
        }

        private string wfoERPPartialUpdateVersion;

        public string _ERPPartialUpdateVersion
        {
            get
            {
                return wfoERPPartialUpdateVersion
                       ?? (wfoERPPartialUpdateVersion = WFOptions.RetrieveOptionValue("ERPPartialUpdateVersion"));
            }
        }

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
                Next(CollectEmployee);
            }
            else
            {
                throw new RFS_Exception("BRANCH");
            }

            return true;
        }

        private void _BranchPlantList_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _BranchPlantTable.ValidBranchPlant();
            _BranchPlantList.DataColumnForValidation = "MCMCU";
            _BranchPlantList.Table = _BranchPlantTable;
        }

        #endregion

        #region CollectEmployee

        private bool CollectEmployee_ValidateMethod(string sInput)
        {
            _EmployeeNumber = sInput;
            _AddressBookTable.ResetAll();
            if (!_AddressBookTable.RetrieveAddress(_EmployeeNumber))
                throw new RFS_Exception("EMPLOYEENUMBER");
            Next(CollectShiftCode);
            return true;
        }

        #endregion

        #region CollectShiftCode

        private bool CollectShiftCode_ValidateMethod(string sInput)
        {
            if (_UserDefinedCodesTable.ValidateUDC("00  ", "SH", sInput.ToUpper()))
            {
                _ShiftCode = _UserDefinedCodesTable.DRKY;
                _TypeOfHours = _UserDefinedCodesTable.DRKY;
                _Status = _UserDefinedCodesTable.DRKY;
                ScreenMap.Add("ShiftCode", _ShiftCode);
                Next(CollectToLocation);
            }
            else
            {
                throw new RFS_Exception("SHIFTCODE");
            }

            return true;
        }

        private void _UserDefinedCodesList_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _UserDefinedCodesTable.FetchUDCs("00 ", "SH");
            _UserDefinedCodesList.DataColumnForValidation = "DRKY";
            _UserDefinedCodesList.Table = _UserDefinedCodesTable;
        }

        #endregion

        #region CollectToLocation

        private bool CollectToLocation_ValidateMethod(string sInput)
        {
            _LocationMasterTable.ResetAll();
            _LocationMasterTable.MCU = _BranchPlant;
            _LocationMasterTable.LMLOCN = sInput;
            _LocationMasterTable.inLOCN = sInput;
            _ToLocation = sInput;
            ScreenMap.Add("ToLocation", _ToLocation);

            if (!_LocationMasterTable.ValidateLocation())
            {
                throw new RFS_Exception("LOCATION");
            }

            Next(CollectLabelID);
            return true;
        }

        private void _LocationMasterList_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _LocationMasterTable.GetLocationsForBranch(_BranchPlant);
            _LocationMasterList.DataColumnForValidation = "LMLOCN";
            _LocationMasterList.Table = _LocationMasterTable;
        }

        #endregion

        #region CollectLabelID

        private void CollectLabelID_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            ScreenMap.Add("WorkOrderNumber", _WorkOrderNumber);
            ScreenMap.Add("ItemNumber", _ItemNumber);
            ScreenMap.Add("LastLabelID", _LastLabelID);
        }

        private bool CollectLabelID_ValidateMethod(string sInput)
        {
            double ConversionFactor = 0;
            Log.Debug("CollectLabelID_ValidateMethod");
            if (!_SRChinaBoxLabelsTable.LabelIDFetch(sInput))
            {
                throw new RFS_Exception("LABELID");
            }

            Log.Debug("CollectLabelID_ValidateMethod - After LabelIDFetch");
            _WorkOrderNumber = _SRChinaBoxLabelsTable.RFDOCO.ToString();
            _ItemNumber = _SRChinaBoxLabelsTable.RFLITM;
            if (!_WorkOrderTable.DoesWorkOrderExist(Double.Parse(_WorkOrderNumber)))
            {
                throw new RFS_Exception("WORKORDERNUMBER");
            }

            if (
                _JDEReservationsTable.DoJDERecordLocksExist("F4801", _WorkOrderNumber, _SRChinaBoxLabelsTable.RFDCTO,
                    _WorkOrderTable.WACO))
            {
                throw new RFS_Exception("RECORDRESERVED");
            }

            Log.Debug("CollectLabelID_ValidateMethod - After DoesWorkOrderExist");
            if (_SRChinaBoxLabelsTable.VerifyLabelIDWO(Double.Parse(_WorkOrderNumber), _ItemNumber, sInput))
            {
                Log.Debug("CollectLabelID_ValidateMethod - If VerifyLabelIDWO");
                if (_SRChinaBoxLabelsTable.RFLPREP.Length > 0)
                {
                    if (Double.Parse(_SRChinaBoxLabelsTable.RFLPREP) == 1)
                    {
                        throw new RFS_Exception("COMPLETIONFLAG");
                    }
                }

                Log.Debug("CollectLabelID_ValidateMethod - After RFLPREP");
                if (_WorkOrderTable.WASOQS > _SRChinaBoxLabelsTable.RFSOQS)
                {
                    Log.Debug("CollectLabelID_ValidateMethod - If WASOQS>RFSOQS");
                    if (!_AllowOverComplete)
                    {
                        Log.Debug("CollectLabelID_ValidateMethod - !_AllowOverComplete");
                        throw new RFS_Exception("OVERCOMPLETE");
                    }
                }

                if (!_WorkOrderTable.IsWorkOrderValid(Double.Parse(_WorkOrderNumber), _AllowedWorkOrderStatus))
                {
                    Log.Debug("CollectLabelID_ValidateMethod - _AllowedWorkOrderStatus");
                    throw new RFS_Exception("STATUS");
                }

                _UnitOfMeasure = _SRChinaBoxLabelsTable.RFUOM;
                _ShortItemNumber = _SRChinaBoxLabelsTable.RFITM;
                _LotNumber = _SRChinaBoxLabelsTable.RFLOTN;
                _DocumentType = _SRChinaBoxLabelsTable.RFDCTO;
                _ExpirationDate = _Today.AddDays(_ExpirationDays);
                _ConvertedTransactionQuantity = _SRChinaBoxLabelsTable.RFSOQS;
                Log.Debug("Today - " + _Today);
                Log.Debug("ExpirationDate - " + _ExpirationDate);
                Log.Debug("CollectLabelID_ValidateMethod - Before SendToERP");
                if (_UnitOfMeasure != _ItemMasterTable.IMUOM1)
                {
                    ConversionFactor =
                        _UnitOfMeasureConversionTable1.ConvertToPrimaryUOM(_BranchPlant, _ShortItemNumber, _UnitOfMeasure,
                            _ItemMasterTable.IMUOM1.ToString());
                    Log.WriteLine(5, "Conversion Factor after call to ConvertToPrimaryUOM = " + ConversionFactor.ToString());
                    Log.WriteLine(5, "Branch/Plant = " + _BranchPlant);
                    Log.WriteLine(5, "Short Item Number = " + _ShortItemNumber.ToString());
                    Log.WriteLine(5, "From U/M = " + _UnitOfMeasure.ToString());
                    Log.WriteLine(5, "To U/M = " + _ItemMasterTable.IMUOM1.ToString());
                    if (ConversionFactor == 0)
                    {
                        ConversionFactor =
                            _UnitOfMeasureConversionTable2.ConvertToPrimaryUOM(_UnitOfMeasure, _ItemMasterTable.IMUOM1.ToString());
                    }
                }
                else
                {
                    ConversionFactor = 1;
                }

                if (!_RoutingInstructionsTable.FetchLastRouting(Double.Parse(_WorkOrderNumber), _PayPoint))
                {
                    throw new RFS_Exception("PAYPOINT");
                }

                _CurrentOperationSequence = _RoutingInstructionsTable.WLOPSQ.ToString();
                _Hours = _RoutingInstructionsTable.WLRUNL.ToString();
                _WorkCenter = _RoutingInstructionsTable.WLMCU;
                GetERPUpdateVersion(_UnitOfMeasure, ConversionFactor);
                //SendToERP();
                SendToERPTracking(_SRChinaBoxLabelsTable.RFSOQS);
                Log.Debug("CollectLabelID_ValidateMethod - After SendToERP");
                if (!Transactions.Error)
                {
                    UpdateWOCompletionFlag();
                }

                Log.Debug("CollectLabelID_ValidateMethod - After UpdateWOCompletionFlag");
                _WorkOrderNumber = _SRChinaBoxLabelsTable.RFDOCO.ToString();
                _ItemNumber = _SRChinaBoxLabelsTable.RFITM.ToString();
                _LastLabelID = sInput;
            }

            RestartLoop(CollectLabelID);
            return true;
        }

        #endregion

        #region GetERPUpdateVersion

        /// <summary>
        /// OverCompleted checks to see if the OverComplete workflow option is set to 'Y', which
        /// indicates that the work order should be checked for over completion.  Over completion
        /// is determined if the scrap plus transaction (completed) quantity is greater
        /// than the difference of the work order's WAUORG, WASOQS, and WASOCN respectively
        /// </summary>
        /// <returns>Returns true if work order has been over completed and the OverComplete
        /// workflow option is set to 'Y'; otherwise, false</returns>
        private void GetERPUpdateVersion(string sUOM, double dConvFactor)
        {
            double dTotalTxDocoQty;

            // If the UOM passed matches the document's UOM, do not fetch the UOM conversion as it
            // was already passed in to the method
            if (sUOM != _WorkOrderTable.WAUOM)
            {
                // Converting the total transactional quantity to the document UOM by getting the
                // conversion from document to primary and dividing the result to go back to doc
                // because I figured it may be easier to find a conversion to primary than to doc.
                dConvFactor = _UnitOfMeasureConversionTable1.ConvertUnitOfMeasure(_BranchPlant,
                    _ShortItemNumber,
                    _WorkOrderTable.WAUOM,
                    _ItemMasterTable.IMUOM1);
                if (dConvFactor == 0)
                {
                    throw (new RFS_Exception("Unable to convert the WAUOM to the item's primary\n" +
                                             "in order to correctly check for over completion.",
                        "INVOVERCOMPCONV"));
                } // end if
            } // end if

            // Converting the Tx Qty, which includes the completed and scrapped quantity in the
            // primary UOM, to the Doco UOM for over-completion check
            //dTotalTxDocoQty = _ConvertedTransactionQuantity / dConvFactor;

            // If this is a partial completion, then use the partial completion WFO version
            _ERPUpdateVersion =
                WFOptions.RetrieveOptionValue((_ConvertedTransactionQuantity < (_WorkOrderTable.WAUORG -
                                                                                _WorkOrderTable.WASOQS -
                                                                                _WorkOrderTable.WASOCN))
                    ? "ERPPartialUpdateVersion"
                    : "ERPCompleteUpdateVersion");
            Log.Debug("_ERPUpdateVersion: " + _ERPUpdateVersion);
        } // end OverCompleted

        #endregion

        #region SendToERP

        /*
        private void setToLocation()
        {
            Log.Debug("setToLocation");
            _WOCompletionTransaction.Location = _ToLocation.Trim();
            if (_LocationMasterTable.ValidateLocation(_BranchPlant, _ToLocation))
            {
                _WOCompletionTransaction.ToLocnPutZone = _LocationMasterTable.LMPZON.Trim();
                _WOCompletionTransaction.ToLocnPickZone = _LocationMasterTable.LMKZON.Trim();
                _WOCompletionTransaction.ToLocnReplZone = _LocationMasterTable.LMZONR.Trim();
                _WOCompletionTransaction.ToLocnDimGroup = _LocationMasterTable.LMSTY1.Trim();
            }
        }
        private void setItem()
        {
            Log.Debug("setItem");
            _WOCompletionTransaction.PrimaryItemNumber = _ItemNumber;
            _WOCompletionTransaction.ShortItemNumber = _SRChinaBoxLabelsTable.RFITM.ToString();
            _WOCompletionTransaction.SecondItemNumber = _SRChinaBoxLabelsTable.RFLITM;
            _WOCompletionTransaction.ThirdItemNumber = _WorkOrderTable.WAITM.ToString();
            _WOCompletionTransaction.ItemDescription1 = _ItemMasterTable.IMDSC1; // Needed for WOW
            _WOCompletionTransaction.ItemDescription2 = _ItemMasterTable.IMDSC1; // Needed for WOW
            _ItemBranchTable.ValidateItemBranch(_BranchPlantTable.MCMCU.Trim(), _SRChinaBoxLabelsTable.RFITM);
            _WOCompletionTransaction.ItemDimGroup = _ItemBranchTable.IBPRP6.Trim();
            _WOCompletionTransaction.ItemLotProcType = _ItemBranchTable.IBSRCE;
        }

        private void setLot()
        {
            Log.Debug("setLot");
            _LotMasterTable.RetrieveLotMaster(_SRChinaBoxLabelsTable.RFLOTN);
            _WOCompletionTransaction.Lot = _SRChinaBoxLabelsTable.RFLOTN;
            _WOCompletionTransaction.LotStatus = _LotMasterTable.IOLOTS;
            _WOCompletionTransaction.LotGrade = _LotMasterTable.IOLOTG;
            //_WOCompletionTransaction.LotPotency = _LotMasterTable.IOLOTP.ToString();
        }
        */
        /// <summary>
        /// SendToERP no longer used.  Now using SendToERPTracking
        /// </summary>
        private void SendToERP()
        {
            Log.Debug("SendToERP");
            Transactions.Add(_WOCompletionTransaction);
            _WOCompletionTransaction.Initialize();
            _WOCompletionTransaction.Branch = _BranchPlant;
            _WOCompletionTransaction.ShiftCode = _ShiftCode.Trim();
            _WOCompletionTransaction.DocumentNumber = _WorkOrderNumber;
            _WOCompletionTransaction.DocumentType = _SRChinaBoxLabelsTable.RFDCTO;
            _WOCompletionTransaction.QuantityCompleted = _SRChinaBoxLabelsTable.RFSOQS.ToString();

            _WOCompletionTransaction.CompletedUOM = _SRChinaBoxLabelsTable.RFUOM;
            //_WOCompletionTransaction.SecondaryQtyCompleted = _CollectDualQuantity.Quantity.ToString();
            //_WOCompletionTransaction.SecondaryCompletedUOM = _CollectDualUnitOfMeasure.UnitOfMeasure;
            //_WOCompletionTransaction.QuantityScrapped = _ScrapQuantity.ToString();
            //_WOCompletionTransaction.ScrappedUOM = _ScrapUnitOfMeasure;
            _WOCompletionTransaction.LotExpDate_YYYYMMDD = _ExpirationDate.ToString();
            _WOCompletionTransaction.RFUserID = UserSession.CurrentUser.LoginName;
            _WOCompletionTransaction.TransDate_YYYYMMDD = DateTime.Now.ToString("yyyyMMdd");
            //_WOCompletionTransaction.Version = _ERPUpdateVersion.Trim();
            //_WOCompletionTransaction.LicensePlateNumber = _LicensePlate;
            //_WOCompletionTransaction.NewOrExistingLPN = _LicensePlateProcessingType;

            Transactions.Send(WFOptions.GetYesNoWFOptionValue("SyncTransaction") ? "Y" : "N");
            Log.Debug("SendToERP Complete");
            DisplayMessages();
        }

        private void SendToERPTracking(double Quantity)
        {
            if (!_ItemMasterTable.FetchItem(_ShortItemNumber))
            {
                throw new RFS_Exception("ITEMNUMBER");
            }

            Transactions.Add(_WOTrackingTransaction);
            _WOTrackingTransaction.Initialize();
            _WOTrackingTransaction.Branch = _BranchPlant.ToString();
            _WOTrackingTransaction.WorkCenter = _WorkCenter;
            _WOTrackingTransaction.EmployeeNumber = _EmployeeNumber.ToString();
            _WOTrackingTransaction.ShiftCode = _ShiftCode.Trim();
            _WOTrackingTransaction.DocumentNumber = _WorkOrderNumber.ToString();
            _WOTrackingTransaction.DocumentType = _DocumentType;
            _WOTrackingTransaction.CurrentOperationSequence = _CurrentOperationSequence;
            _WOTrackingTransaction.WOStatus = _Status.Trim();
            _WOTrackingTransaction.OperationType = _RoutingInstructionsTable.WLOPSC;
            _WOTrackingTransaction.Location = _ToLocation;
            double WorkQuantity = 0;
            if (_UnitOfMeasure != _WorkOrderTable.WAUOM)
            {
                WorkQuantity = _UnitOfMeasureConversionTable1.ConvertToDocumentUOM(_BranchPlant,
                    _ShortItemNumber, _UnitOfMeasure,
                    _WorkOrderTable.WAUOM, Quantity,
                    _ItemMasterTable.IMUOM1);
                // if the return value was zero, then a conversion was not found
                if (WorkQuantity != 0)
                {
                    Quantity = WorkQuantity;
                    _UnitOfMeasure = _WorkOrderTable.WAUOM;
                }
                else
                {
                    // no conversion was found, check the secondary u/m conversion
                    if (_UnitOfMeasureConversionTable2.ConvertToPrimaryUOM(_UnitOfMeasure, _ItemMasterTable.IMUOM1.ToString()) != 0)
                    {
                        WorkQuantity = _UnitOfMeasureConversionTable1.ConvertToDocumentUOM(_BranchPlant,
                            _ShortItemNumber, _ItemMasterTable.IMUOM1,
                            _WorkOrderTable.WAUOM, Quantity, _ItemMasterTable.IMUOM1);
                        // if no conversion was found, don't change the quantity                                                              

                        if (WorkQuantity != 0)
                        {
                            Quantity = WorkQuantity;
                            _UnitOfMeasure = _WorkOrderTable.WAUOM;
                        }
                    }
                }
            }

            _WOTrackingTransaction.QuantityCompleted = Quantity.ToString();
            _WOTrackingTransaction.CompletedUOM = _UnitOfMeasure;
            _WOTrackingTransaction.QuantityScrapped = "0";
            _WOTrackingTransaction.ScrappedUOM = _UnitOfMeasure;
            // Return to either hours or end time.....
            /*switch (WFOptions.RetrieveOptionValue("MethodToRecordTime").Trim())
            {
                case "1": // capture start/stop time
                    // handle calculation differently if times overlap after midnight
                    if (_StartHHMM > _StopHHMM)
                        _WOTrackingTransaction.Hours = (_StopHHMM + (24 - _StartHHMM)).ToString();
                    else
                        _WOTrackingTransaction.Hours = (_StopHHMM - _StartHHMM).ToString();

                    _WOTrackingTransaction.StartTime = _StartTime;
                    _WOTrackingTransaction.EndTime = _StopTime;
                    break;

                case "2": // capture total hours
                    _WOTrackingTransaction.Hours = _Hours;
                    break;
            }*/
            _WOTrackingTransaction.Hours = "";
            _WOTrackingTransaction.TypeOfHours = "";
            _WOTrackingTransaction.Lot = _LotNumber;
            _WOTrackingTransaction.LotExpDate_YYYYMMDD = _ExpirationDate.ToString();
            _WOTrackingTransaction.RFUserID = UserSession.CurrentUser.LoginName;
            _WOTrackingTransaction.TransDate_YYYYMMDD = System.DateTime.Now.ToString("yyyyMMdd");
            _WOTrackingTransaction.Version = _ERPUpdateVersion;
            //_WOTrackingTransaction.LicensePlateNumber = _LicensePlate;
            //_WOTrackingTransaction.NewOrExistingLPN = _LicensePlateProcessingType;
            Transactions.Send(WFOptions.RetrieveOptionValue("SyncTransaction"));
            DisplayMessages();
            _LotNumber = _WOTrackingTransaction.ReturnField("Lot", 0, 0);
            _LotExpirationDate = _WOTrackingTransaction.ReturnField("LotExpDate_YYYYMMDD", 0, 0);
            //_LicensePlate = _WOTrackingTransaction.ReturnField("LicensePlateNumber", 0, 0);
        }

        private void UpdateWOCompletionFlag()
        {
            Log.Debug("UpdateWOCompletionFlag");
            Transactions.Add(RFS_TxPSUpdate);
            RFS_TxPSUpdate.LabelName = _SRChinaBoxLabelsTable.RFLBN;
            RFS_TxPSUpdate.RFUserId = _SRChinaBoxLabelsTable.RFUSER;
            RFS_TxPSUpdate.Mode = "W";
            Transactions.Send(WFOptions.GetYesNoWFOptionValue("SyncTransaction") ? "Y" : "N");
            DisplayMessages();
        }

        #endregion

        #endregion
    }
}