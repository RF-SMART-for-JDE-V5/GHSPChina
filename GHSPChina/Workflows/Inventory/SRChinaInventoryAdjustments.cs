/*
    $Archive:   \\filer\rfsow\pvcsproj\RFSmart3\archives\Base\RTTC_Plugins\ICS\Workflow\Base\JDE\Inventory\SRChinaInventoryAdjustments.cs-arc  $
    $Revision: 3894 $
    $Author: mhefti@ICSNT $
    $Date: 2014-07-29 11:29:09 -0600 (Tue, 29 Jul 2014) $ 
   $Log:   \\filer\rfsow\pvcsproj\RFSmart3\archives\Base\RTTC_Plugins\ICS\Workflow\Base\JDE\Inventory\SRChinaInventoryAdjustments.cs-arc  $
 * 
 *    Rev 1.164   Sep 10 2010 14:26:34   cfernandez
 * CC033083 - Modified to Trim the Reason Code when selected from a list to fix issue when trying to validate.
 * 
 *    Rev 1.163   Mar 04 2009 15:00:46   cfernandez
 * 28953 - Modified to not collect Lot Expiration Date if lot is balnk and the lot process type is optional.
 * 
 *    Rev 1.162   Nov 03 2008 15:41:10   ghripto
 * BE013638: DUOM project - Added secondary/dual qty and secondary/dual uom fields.
 * 
 *    Rev 1.161   12 Dec 2007 12:29:18   rgreene
 * BD023675: SRChinaInventoryAdjustments - Collect Location - F4100Empty_List 
 * 
 *    Rev 1.160   10 Dec 2007 13:25:12   rgreene
 * BE021727: JDEReceiptToContainer - CollectLotSerialExpDate 
 * 
 *    Rev 1.159   12 Nov 2007 11:26:54   rgreene
 * BE015634: Change the F0006_List to more meaningful information 
 * 
 *    Rev 1.158   09 Nov 2007 10:25:44   rgreene
 * BD023317: QTP415 - SRChinaInventoryAdjustments - CollectReasonCode (F0005_List) 
 * BD023307: QTP415 - SRChinaInventoryAdjustments - CollectBranch (F0006_List)
 * 
 *    Rev 1.157   09 Nov 2007 09:46:28   rgreene
 *  BD023366: SRChinaInventoryAdjustments -Positive SelectPrinter FRFS_List 
 * 
 *    Rev 1.156   09 Nov 2007 09:03:02   rgreene
 * BD023365: SRChinaInventoryAdjustments -Positive UnitOfMeasure F41002_List 
 * 
 *    Rev 1.155   09 Nov 2007 08:58:00   rgreene
 *  BD023337: SRChinaInventoryAdjustments - Collect Location - Emptylist Screen 
 * 
 *    Rev 1.154   09 Nov 2007 08:43:22   rgreene
 * BD023324: SRChinaInventoryAdjustments - F41021_List Screen 
 * 
 *    Rev 1.153   19 Sep 2007 09:57:18   rgreene
 * BE018401: Inventory Adjustments - Lot Expiration Date - 8.0 
 * 
 *    Rev 1.152   14 Aug 2007 11:10:44   rgreene
 * BD022274
 * 
 *    Rev 1.151   02 Jul 2007 14:32:40   rgreene
 * BE018401: Inventory Adjustments - Lot Expiration Date - 8.0 
 * 
 *    Rev 1.150   11 May 2007 10:54:52   rgreene
 * BD020971: JDE Inventory Adjustments 
 * 
 *    Rev 1.149   Feb 19 2007 12:26:32   rgreene
 * BE015927: AllowHeldAdjustments
 * 
 *    Rev 1.148   Jul 28 2006 15:59:00   rgreene
 * BE015927: AllowHeldAdjustments
 * 
 *    Rev 1.147   Jun 12 2006 13:50:46   pmonaco
 *  BE016606 - Update to .NET 2.0
 * 
 *    Rev 1.146   May 24 2006 10:16:38   pmonaco
 * BD016405, BD016405, BD016403, BD016397 - Update to remove held lot check at lot entry screen and only check after location has been entered.
 * 
 *    Rev 1.145   May 22 2006 14:56:12   pmonaco
 * BD016013 - Workflows printer selection modified to use Print System validprinter method.  This checks to make sure the form matches the printer model before printing.
 * 
 *    Rev 1.144   May 17 2006 11:38:30   pmonaco
 * BD016196 - Added check if Item location is valid before checking lot status.
 * 
 *    Rev 1.143   May 15 2006 16:30:10   rgreene
 *  BD016157  SRChinaInventoryAdjustments - CollectSerialNumber Workflow Error 
 * 
 *    Rev 1.142   May 11 2006 08:56:10   rgreene
 * BD015894: Doco - Missing 3 Screens - During documentation, discovered that when doing a negative adjustment, the expired lot workflow options were not being used. Corrected.
 * 
 *    Rev 1.141   May 10 2006 09:52:52   rgreene
 * BD015929: AvailabilityCheck
 * 
 *    Rev 1.140   May 03 2006 17:35:32   rgreene
 *  BD015766: Wright Medical - Availability Calculation
 * 
 *    Rev 1.139   Apr 24 2006 13:43:42   rgreene
 * BD015596: No Exit/back button
 * 
 *    Rev 1.138   20 Apr 2006 10:53:28   lsagan
 * BD011946: Reason code is not updated in OW. Trim spaces before sending to OW
 * 
 *    Rev 1.137   Apr 10 2006 14:52:52   rgreene
 * Replace ERROR methods with Chris Gray's "Cleaner" method
 * 
 *    Rev 1.136   Apr 05 2006 13:23:44   rgreene
 * Code clean up dealing with item number
 * 
 *    Rev 1.135   Feb 07 2006 13:05:44   rgreene
 * BD014606: Inv Adj - Problems with lot number
 * 
 *    Rev 1.134   Feb 01 2006 17:03:56   rgreene
 * BD014532: SRChinaInventoryAdjustments - Item Availability lookup only works once
 * 
 *    Rev 1.133   Jan 18 2006 11:19:12   rgreene
 * BD014278: S/O by Item pick, IBSRCE = 4
 * BD013984: QA/QTP - S/O by Item - IBSRCE = 7
 * During testing, discovered that inventory adjustments was not handling IBSRC=4 correctly when the user left the s/n blank.
 * 
 *    Rev 1.132   Jan 16 2006 08:44:46   rgreene
 * BE013720 - remove Psft Name
 * 
 *    Rev 1.131   Nov 30 2005 16:18:08   rgreene
 * BD013719 Expired lot allowed in inv. adj. when workflow option should prevent it 
 * 
 *    Rev 1.130   Nov 29 2005 15:01:52   rgreene
 * BD013717 Inventory Adjustments
 * 
 *    Rev 1.129   Nov 22 2005 14:29:18   rgreene
 * BD013542 - issue found with pos adj. 
 * 
 *    Rev 1.128   Nov 21 2005 14:58:58   rgreene
 * BD013542 Negative Inv Adj version with qty > 1 and serialized items
 * 
 *    Rev 1.127   Nov 21 2005 14:09:50   rgreene
 * BD013541 Negative Adj using "Positive" version for serialized items
 * 
 *    Rev 1.126   Nov 01 2005 10:29:40   rgreene
 * BE013345 Workflow Logging
 * 
 *    Rev 1.125   Oct 25 2005 13:57:36   rgreene
 * BD013222 Serial number optional (IBSRCE = '4') - Positive Adjustment
 * 
 *    Rev 1.124   Oct 25 2005 13:50:38   rgreene
 * BD013223 IA - serialized item allows lower case lot numbers
 * 
 *    Rev 1.123   Oct 19 2005 16:14:52   rgreene
 * BE013117 - remove this enhancement and allow the negative quantity to be entered
 * 
 *    Rev 1.122   Oct 18 2005 16:57:10   rgreene
 * BE013117 Adjustments - do not allow a negative quantity to be entered
 * 
 *    Rev 1.121   Oct 12 2005 16:23:26   rgreene
 * BD013044 Negative Adjustments sending transaction with wrong sign
 * 
 *    Rev 1.120   Oct 11 2005 09:22:34   rgreene
 *  BE013015 Availability Calcs
 * 
 *    Rev 1.119   Oct 07 2005 18:46:20   mstueck
 * BD012989 IA12988  checkin trigger test
 * 
 *    Rev 1.118   Oct 07 2005 18:30:58   mstueck
 * 12988 make fixes to negative processing 
 * 
 *    Rev 1.117   Oct 06 2005 09:37:50   rgreene
 * BD012947 Inventory Adjustment - SuggestFromPrimary not working
 * 
 *    Rev 1.116   Sep 15 2005 13:23:12   rgreene
 * BD012672 - remove the lot expiration date screens
 * 
 *    Rev 1.115   Sep 15 2005 07:27:10   rgreene
 * Issue with expiration dates in F4108 discovered when testing RFS WOW 
 * 
 *    Rev 1.114   Sep 09 2005 17:02:54   cgray
 * BE012618 - Made sure the Lot\Serial Expiration Date was being populated to send with the transaction.
 * 
 *    Rev 1.113   Sep 08 2005 09:34:10   cgray
 * BE012618 - Added the fields necessary to create the string to send for WOW.
 * 
 *    Rev 1.112   31 Aug 2005 08:52:26   rgreene
 * BE012145: Blank Explanation value
 * 
 *    Rev 1.111   Aug 26 2005 15:50:24   rgreene
 * BE012421: on-hand/available balance checking - along with clean up of F41021 derived class 
 * 
 *    Rev 1.110   Aug 22 2005 17:24:46   PMonaco
 * BD012441 - Print check and messages display.
 * 
 *    Rev 1.109   Aug 17 2005 10:26:28   rgreene
 * BD012231: Inv Adj/Reclass allows adj to expired lots
 * 
 *    Rev 1.108   Aug 12 2005 14:54:56   rgreene
 *  BD012230: Inventory Adjustments "printer" screens
 * 
 *    Rev 1.107   Aug 10 2005 11:42:02   rgreene
 * Serialized Inventory Processing Mods
 * 
 *    Rev 1.106   Aug 06 2005 20:39:22   rgreene
 * BE012145: Blank Explanation value
 * 
 *    Rev 1.105   Aug 02 2005 15:52:28   rgreene
 * BD012288: Barcode Print After Transaction Sent to ERP
 * 
 *    Rev 1.104   Jul 27 2005 15:17:26   rgreene
 * BD012227: Inv Adj
 * 
 *    Rev 1.103   Jul 27 2005 13:15:20   rgreene
 * Clean up the file open/close logic for new change to class. This omits the table object array that we were using 
 * 
 *    Rev 1.102   Jun 22 2005 09:44:30   rgreene
 * BD011693: Prime Location Displayed in Distribution Workflows
 * 
 *    Rev 1.101   Jun 14 2005 17:51:44   rgreene
 * BD011693: Prime Location Displayed in Distribution Workflows
 * 
 *    Rev 1.100   Jun 07 2005 09:39:06   rgreene
 * Clean up comments
 * 
 *    Rev 1.99   May 30 2005 20:41:30   rgreene
 * clean up code a bit
 * 
 *    Rev 1.98   May 27 2005 16:21:02   rgreene
 * Remove "magic Number" dealing with lot process type and replace it with enumerator
 * 
 *    Rev 1.97   May 24 2005 13:35:52   rgreene
 * BE011399: Inv Adj for seriial ized items screen flow
 * 
 *    Rev 1.96   May 23 2005 15:28:04   rgreene
 * 11534
 * 
 *    Rev 1.95   May 19 2005 09:58:16   rgreene
 * BD011483: Unable to do positive inventory adj on a serialized item with zero balance
 * 
 *    Rev 1.94   May 19 2005 09:25:50   lsagan
 * BD011474: added display of lot exp date on the expired lot warning screen
 * 
 *    Rev 1.93   May 19 2005 07:21:00   rgreene
 * Issues found during massive qa
 * 
 *    Rev 1.92   May 18 2005 17:09:18   mstueck
 * BD011316  add code to always flip the sign of the qty if the option is set to be a negative adjustment.
 * 
 *    Rev 1.91   May 18 2005 16:30:30   rgreene
 * BD011249: Inventory Adj - uom
 * 
 *    Rev 1.90   May 18 2005 12:31:48   rgreene
 * BD011404: OW error on IA for serialized item with qty greater than 1
 * 
 *    Rev 1.89   May 17 2005 21:08:58   rgreene
 * BD011433: Erroneous location validation error on transfer of serialized item
 * 
 *    Rev 1.88   May 17 2005 19:24:32   rgreene
 * BD011429: IA incorrectly allows blank reason code
 * 
 *    Rev 1.87   May 17 2005 18:52:48   lsagan
 * BD011402: display error message when blank uom is invalid
 * 
 *    Rev 1.86   May 17 2005 16:14:44   rgreene
 * Issues with clearing buffers
 * 
 *    Rev 1.85   May 17 2005 15:04:40   rgreene
 * BD011384: Inv Adj - Serialized item - Collect Serial Screen
 * 
 *    Rev 1.84   May 17 2005 14:13:58   rgreene
 * BD011401: Inv Adj - Negative - Lot item
 * 
 *    Rev 1.83   May 16 2005 22:58:42   rgreene
 * BD011385: Inv Adj - Serial number letters are not converted to uppercase
 * 
 *    Rev 1.82   May 16 2005 22:02:52   rgreene
 * BD011303: Cycle Count Direct: System does not error on invalid value for qty - Rework quantity logic in jde workflows
 * 
 *    Rev 1.81   May 16 2005 17:01:44   rgreene
 * BD011273: IA: Usability on Item Search
 * 
 *    Rev 1.80   May 14 2005 00:41:30   rgreene
 * BD011187: IA - Shelf Life Days
 * 
 *    Rev 1.79   May 13 2005 20:46:14   rgreene
 * BD011323: Cycle Count Random: Add button
 * 
 *    Rev 1.78   May 13 2005 10:02:08   PMonaco
 * If converting string to int make sure string is not blank.  BD011347.
 * 
 *    Rev 1.77   May 13 2005 02:09:02   rgreene
 * QA Changes
 * 
 *    Rev 1.76   May 13 2005 01:30:56   rgreene
 * Change to lot/serial number logic
 * 
 *    Rev 1.75   May 11 2005 14:39:34   PMonaco
 * Chages to use ERP user name for RFUserID in transaction.  BE011300
 * 
 *    Rev 1.74   May 11 2005 10:49:58   rgreene
 * BD011249: Inventory Adj - uom
 * 
 *    Rev 1.73   May 11 2005 09:50:16   rgreene
 * BD011205
 * 
 *    Rev 1.72   May 11 2005 04:50:32   rgreene
 * QA Issues with lot type
 * 
 *    Rev 1.71   May 10 2005 16:24:38   rgreene
 * QA Issues with Elizabeth
 */

/* Screen Flow
 * 
 * 
 * Normal (non-lot, non-serialized item)      Lot Control Item         Serialized Item
 *                                      -----------------            
 *                                         -   Branch      -                     
 *                                         -----------------            
 *                                                |                              
 *                                         -----------------            
 *                                         -  Explanation  -                
 *                                        -----------------            
 *                                                 |                              
 *                                         -----------------            
 *                                         -  Reason Code  -            
 *                                          -----------------            
 *                                           |                        
 *                                         ------------------            
 *                                     -  Item Number    -
 *                                    -------------------
 *              |                              |                     |      
 *      -------------------                  -------------------        ------------------
 *      -    Location     -                     -   Lot Number    -        -    Location    -    
 *      -------------------                     -------------------        ------------------
 *             |                              |                     |
 *      -------------------                  -------------------         ------------------
 *      -    Quantity     -                  -    Location     -        -  Serial Number - 
 *      -------------------                     -------------------        ------------------
 *              |                              |                     
 *      -------------------                     -------------------
 *      - Unit of Measure -                     -    Quantity     - 
 *      -------------------                     -------------------
 *                                          |
 *                                    -------------------
 *                                              - Unit of Measure -
 *                                              ------------------- 
 */

using System;
using ICS.Database.TableObjects.JDE;
using ICS.Database.TableObjects.RFS;
using ICS.Display;
using ICS.ERPI.JDE;
using ICS.Utilities;
using ICS.Workflow.Base.WorkflowDesign.Components;

namespace ICS.SRChina.Wireless.JDE.Workflow.Inventory
{
    /// ************************************************************
    /// Adjustment entry 
    ///    All quantities are entered as positive amounts, the 
    ///    workflow options control whether this is a positive or 
    ///    a negative adjustment.
    /// ************************************************************
    public class SRChinaInventoryAdjustments : ICS.Workflow.Base.WorkflowDesign.Components.RFSWorkflowDesigner
    {
        #region Class Level defs

        #region Tables

        // define tables used
        //private F0006 _BranchPlantTable; // jde Branch plant master
        private F0005 _UserDefinedCodesTable; // jde user defined codes

        private F4101 _ItemMasterTable; // jde item master

        private F4102 _ItemBranchTable; // jde item Branch

        private F4100 _LocationMasterTable; // jde location master

        private F41021 _ItemLocationTable; // jde item/location

        private F4104 _ItemCrossReferenceTable; // jde item cross reference

        private F4108 _LotMasterTable; // jde lot master

        private F41002 _UnitOfMeasureConversionTable1; // jde unit of measure conversion table

        private F41003 _UnitOfMeasureConversionTable2; // jde default u/m conversion

        private RFS_DataManipulator _DataManipulatorTable; // RFSMART3 ERP Data Manipulator

        private RFS_WorkFlowOption _WorkFlowOption;

        private F41001 _WarehouseConstants; // Warehouse Constants Table

        private RFS_PrinterDefinition _DefinedPrintersTable; // RFS Defined Printers

        private RFS_PrintXref _PrinterCrossReferenceTable;

        private F46L001 _LicensePlateNumberingConstantsTable;

        ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen _PrintBarCodeLabels;

        ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen _SelectPrinter;

        #endregion

        #region Screens

        // define data collection screens
        //ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen _CollectBranch;
        ICS.Workflow.Base.JDE.Components.BranchPlantScreen _CollectBranch;

        ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen _CollectExplanation;

        ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen _CollectReasonCode;

        ICS.Workflow.Base.JDE.Components.ItemScreen _CollectItemNumber;

        //ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen _CollectItemNumber;
        ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen _CollectLocation;

        ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen _CollectLotNumber;

        ICS.Workflow.Base.JDE.Components.QuantityScreen _CollectAdjustmentQuantity;

        ICS.Workflow.Base.JDE.Components.UnitOfMeasureScreen _CollectUnitOfMeasure;

        //ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen _CollectAdjustmentQuantity;
        //ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen _CollectUnitOfMeasure;
        ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen _ExpiredLotWarning;

        ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen _CollectSerialNumber;

        // list/lookup screens
        ICS.Workflow.Base.WorkflowDesign.Components.GenericList _BranchPlantList; // Branch/plant search

        ICS.Workflow.Base.WorkflowDesign.Components.GenericList _UserDefinedCodesList; // user defined codes

        ICS.Workflow.Base.WorkflowDesign.Components.GenericList _ItemBalanceList; // item balance list

        ICS.Workflow.Base.WorkflowDesign.Components.GenericList _LocationMasterList; // location master list

        ICS.Workflow.Base.WorkflowDesign.Components.GenericList _EmptyLocationsList;

        ICS.Workflow.Base.WorkflowDesign.Components.GenericList _UnitOfMeasureConversionList;

        ICS.Workflow.Base.WorkflowDesign.Components.GenericList _DefinedPrintersList;

        #endregion

        #region Collected/Working Data

        private int _SerialNumbersScanned;

        private string _BranchPlant; // Branch/plant

        private string _Explanation; // explanation

        private string _AdjustmentReasonCode; // reason code

        private double _ShortItemNumber; // jde short item number

        private string _PrimaryItemNumber; // primary item number

        private string _ItemNumber;

        private string _LotProcessType; // lot control flag

        private string _Location; // location

        private string _LotStatus; // lot status

        private string _LotNumber; // lot number

        private string _TransactionQuantity; // adjustment quantity

        private string _UnitOfMeasure; // unit of measure

        private string _ExpirationDate; // lot expiration date

        private string _DefaultUnitOfMeasure; // unit of measure originally displayed on screen. Used to check the 
        // AllowUOMOverride workflow option

        private bool _FromSerialNumberEnter;

        private string _ListLookupType;

        private double _OnHandQuantity; // quantity on hand

        private bool _FromSerialNumberEntry;

        private string _Copies;

        private string _PrinterName;

        private string _PrinterAddress;

        private string[] _AllowedLotHoldCodes = null;

        protected GenericScreen _CollectLotExpirationDate;

        protected ICS.Workflow.Base.JDE.Components.DualQuantityScreen _CollectDualQuantity; // allowed lot hold codes

        private double _DualQuantity = 0;

        private string _DualUnitOfMeasure = "";

        private string _UOMListCalledFrom = "";

        #endregion

        protected ICS.Workflow.Base.JDE.Components.DualUnitOfMeasureScreen _CollectDualUnitOfMeasure;

        #region ERPI

        private InvAdjustment _AdjustmentTransaction = new InvAdjustment();

        #endregion

        #endregion

        public SRChinaInventoryAdjustments(UserSession userSession)
            :
            base(ref userSession)
        {
            InitializeComponent();
            Transactions.ERP = "PeopleSoft";
        }

        #region Dispose

        public override void Dispose()
        {
            UserSession.ScreenHandler.ScreenMap.ClearAllWorkflows();
            //Clear Transactions 
            Transactions.ClearTransactions();
            UserSession.ScreenHandler.ScreenMap.Clear("Error");
            base.Dispose();
        }

        #endregion

        #region Initialize

        protected override void Initialize()
        {
            #region Tables

            //_BranchPlantTable = new F0006(UserSession.DataAccess, "");
            _UserDefinedCodesTable = new F0005(UserSession.DataAccess, "");
            _LocationMasterTable = new F4100(UserSession.DataAccess, "");
            _ItemLocationTable = new F41021(UserSession.DataAccess, "");
            _ItemMasterTable = new F4101(UserSession.DataAccess, "");
            _ItemBranchTable = new F4102(UserSession.DataAccess, "");
            _UnitOfMeasureConversionTable1 = new F41002(UserSession.DataAccess, "");
            _UnitOfMeasureConversionTable2 = new F41003(UserSession.DataAccess, "");
            _LotMasterTable = new F4108(UserSession.DataAccess, "");
            _DataManipulatorTable = new RFS_DataManipulator(UserSession.DataAccess, "");
            _ItemCrossReferenceTable = new F4104(UserSession.DataAccess, "");
            _WorkFlowOption = new RFS_WorkFlowOption(UserSession.DataAccess, "");
            _WarehouseConstants = new F41001(UserSession.DataAccess, "");
            _DefinedPrintersTable = new RFS_PrinterDefinition(UserSession.DataAccess, "");
            _PrinterCrossReferenceTable = new RFS_PrintXref(UserSession.DataAccess, "");
            _LicensePlateNumberingConstantsTable = new F46L001(UserSession.DataAccess, "");

            #endregion

            #region Screens

            // screens

            #endregion

            #region Screen events

            // Collect Branch screen events
            // Collect Explanation screen events
            // Collect ReasonCode screen events
            // Collect ItemNumber screen events
            // Collect Location screen events
            // Collect Lot number screen events
            // Collect Quantity screen events
            // Collect Unit of Measure screen events
            // Expired Lot Warning
            // Events For PrintBarCodeLabels
            // Events For SelectPrinter
            // Events For CollectSerialNumber

            #endregion

            // load workflow options
            _WorkFlowOption.LoadWorkflowOptions(UserSession.ScreenHandler.WorkflowID,
                UserSession.ScreenHandler.Environment,
                UserSession.ScreenHandler.Language,
                UserSession.ScreenHandler.Version);
            ClearTransaction();
            // load the processing option for allowing adjustments from held lots
            char[] Delimiter = {' '};
            string sTemp = WFOptions.RetrieveOptionValue("AllowedLotHoldCodes");
            _AllowedLotHoldCodes = sTemp.Split(Delimiter);
            // display the initial screen   
        }

        #endregion

        #region CollectBranch

        private void CollectBranch_LeavingEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _BranchPlant = _DataManipulatorTable.FormatData("F0006", "MCMCU", _CollectBranch.BranchPlantsTable.MCMCU);
        }

        private void BranchPlantList_EnterEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _CollectBranch.BranchPlantsTable.UpdateCurrentRow();
            _BranchPlant = _CollectBranch.BranchPlantsTable.MCMCU;
        }

        private void BranchPlantList_BackEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            Back(_CollectBranch);
        }

        #endregion

        #region CollectExplanation

        private void Explanation_EnterEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _Explanation = (string) e.UserInput;
        }

        #endregion

        #region CollectReasonCode

        private void ReasonCode_EnterEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _AdjustmentReasonCode = e.UserInput.ToString().ToUpper();
            ReasonCodeEnter();
        }

        private void ReasonCodeEnter()
        {
            if (_AdjustmentReasonCode.Trim().Length != 0)
            {
                if (!_UserDefinedCodesTable.ValidateUDC("42  ", "RC", _AdjustmentReasonCode))
                {
                    Error("ReasonCode", _AdjustmentReasonCode);
                    return;
                }
            }
            else
            {
                if (!_WorkFlowOption.GetYesNoWFOptionValue("AllowBlankReasonCode"))
                {
                    Error("BlanksNotAllowed");
                }
            }
        }

        private void UserDefinedCodesList_EnterEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _UserDefinedCodesTable.UpdateCurrentRow();
            _AdjustmentReasonCode = this._UserDefinedCodesTable.DRKY.ToString().Trim();
            ReasonCodeEnter();
        }

        private void UserDefinedCodesList_BackEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            Back(_CollectReasonCode);
        }

        #endregion

        #region CollectItemNumber

        public bool CollectItemNumber_ValidateMethod(string sItemNumber)
        {
            Log.Debug("CollectItemNumber_ValidateMethod");
            Log.Debug("sItemNumber = " + sItemNumber);
            if (sItemNumber.Contains("="))
            {
                Log.Debug("CollectItemNumber_ValidateMethod - Parse");
                var split = sItemNumber.Split('=');
                _ItemNumber = split[0];
                _LotNumber = split[1];
            }
            else
            {
                _ItemNumber = sItemNumber;
                _LotNumber = "";
            }

            Log.Debug("_ItemNumber = " + _ItemNumber);
            Log.Debug("_LotNumber = " + _LotNumber);
            _WarehouseConstants.DeterminePrimaryItem(_BranchPlant);
            Log.Debug("_DesignatedPrimaryItemNumber = " + _WarehouseConstants._DesignatedPrimaryItemNumber);
            _PrimaryItemNumber = _ItemMasterTable.RetrieveItem(_BranchPlant, _ItemNumber,
                _WarehouseConstants._DesignatedPrimaryItemNumber.ToString());

            if (_PrimaryItemNumber != null)
            {
                if (_ItemBranchTable.ValidateItemBranch(_BranchPlant, _ItemMasterTable.IMITM))
                {
                    _ShortItemNumber = _ItemMasterTable.IMITM;
                    _LotStatus = _ItemMasterTable.IMLOTS;
                    _LotProcessType = _ItemBranchTable.IBSRCE;
                    ScreenMap.Add("ITEMNUMBER", _ItemNumber);
                }
                else
                {
                    throw new RFS_Exception("ITEMNUMBER");
                }
            }
            else
            {
                throw new RFS_Exception("ITEMNUMBER");
            }

            Log.Debug("_ItemNumber = " + _ItemNumber);
            Log.Debug("_LotNumber = " + _LotNumber);
            Log.Debug("_PrimaryItemNumber = " + _PrimaryItemNumber);
            return true;
        }

        private void ItemNumber_LeavingEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            if (ALotControlledItem())
            {
                Next(_CollectLotNumber);
            }
            else
            {
                _LotNumber = " ";
                DefaultPrimary();
                Next(_CollectLocation);
            }
        }

        private void ItemNumber_ListEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _ListLookupType = "Item";
            this._ItemLocationTable.ResetAll();
            _ItemLocationTable.RetrieveBranchItemRecords(_BranchPlant, _WorkFlowOption.RetrieveOptionValue("ShowZeroOnHand").ToString().ToUpper().Trim());
        }

        #endregion

        #region CollectLocation

        /// <summary>
        /// Validate the adjustment location 
        ///      if the workflow option is set to not allow a blank location, but the user has entered a blank, 
        ///         issue error
        ///      if the location entered is not a valid location in the F4100 location master table 
        ///         issue error (if lot control is turned off, this routine returns true
        ///      if this is a negative adjustment, validate that the item, lot, location exists for this branch. if not, 
        ///         issue error
        ///      if the workflow option is set to NOT allow held adjustments, but the user is attempting to adjust inventory
        ///      from a held lot, 
        ///         issue error
        ///      Finally, if all edits have passed and this is a serialized item, the process the transaction by calling 
        ///         the PROCESSUOM routine with a quantity of either +1 or -1 and the item's primary U/M
        ///         
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Location_EnterEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _Location = e.UserInput.ToString().ToUpper();
            LocationEnterKey();
        }

        private void Location_BackEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _LotNumber = "";
        }

        private void LocationEnterKey()
        {
            // if the location entered is blank, and the workflow option is set to not allow
            // blanks, issue an error
            if ((!_WorkFlowOption.GetYesNoWFOptionValue("AllowBlankLocation")) &&
                (_Location.Trim().Length == 0))
            {
                Error("BLANKSNOTALLOWED");
                return;
            }

            _LocationMasterTable.inLOCN = _Location;
            // validate the location entered and issue an error if invalid
            if (!_LocationMasterTable.ValidateLocation(_BranchPlant, _Location))
            {
                Error("LOCATION", _Location);
                return;
            }

            UserSession.ScreenHandler.ScreenMap.Add("Location", _LocationMasterTable.sscnlocn);

            if (!_ItemLocationTable.ValidateLotLocn(_CollectBranch.BranchPlantsTable.MCMCU, _CollectItemNumber.ItemsTable.IMITM, _LotNumber, _LocationMasterTable.LMLOCN))
            {
                if (_WorkFlowOption.RetrieveOptionValue("AdjustmentType").ToString() == "N")
                {
                    if ((_LotProcessType.Trim().Length == 0) ||
                        ((Convert.ToInt16(_LotProcessType) >= 0) &&
                         (Convert.ToInt16(_LotProcessType) <= Convert.ToInt16(LotSerialDesignations.LotRequired))))
                    {
                        Error("LOTLOCATION", _Location);
                        return;
                    }
                }

                _ItemLocationTable.LIPQOH = 0;
                _ItemLocationTable.LILOTS = " ";
            }

            bool LotMasterIsValid = _LotMasterTable.RetrieveLotMaster(_BranchPlant, _ShortItemNumber, _LotNumber);

            _Location = _LocationMasterTable.stbllocn;
            _OnHandQuantity = _ItemLocationTable.LIPQOH;
            _LotStatus = _ItemLocationTable.LILOTS;
            // if the processing option is set to not allow held lot adjustments, issue error
            if (!AllowHeldLotProcessing())
            {
                Error("HELDLOT", _Location);
                return;
            }

            if ((LotMasterIsValid && (DateTimeUtils.JDEJulian(_LotMasterTable.IOMMEJ)) < DateTime.Today))
            {
                if (!_WorkFlowOption.GetYesNoWFOptionValue("AllowExpiredLotAdj"))
                {
                    Error("EXPIREDLOT", _LotNumber);
                    _LotNumber = "";
                    return;
                }

                if (_WorkFlowOption.GetYesNoWFOptionValue("WarnForExpiredLot"))
                {
                    Next(_ExpiredLotWarning);
                    return;
                }
            }

            Next(_CollectAdjustmentQuantity);
        }

        private void CollectLocation_KeyEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            switch (e.KeyPressed)
            {
                case ActionKey.F2: // user hits F2 - display empty locations
                    if (e.KeyPressed == ActionKey.F2)
                    {
                        _LocationMasterTable.ShowEmptyLocations(_BranchPlant);
                        _EmptyLocationsList.Show();
                    }

                    break;

                case ActionKey.Enter:
                    _Location = e.UserInput.ToString().ToUpper();
                    LocationEnterKey();
                    break;
                default:
                    Error("JDEWrongKey");
                    break;
            }
        }

        #region EmptyLocationsList_EnterEvent

        private void EmptyLocationsList_EnterEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            // user has selected a record from the list   
            _LocationMasterTable.UpdateCurrentRow();
            _Location = _LocationMasterTable.LMLOCN.ToString();
            UserSession.ScreenHandler.ScreenMap.Add("LOCATION", _Location);
            Next(_CollectAdjustmentQuantity);
        }

        private void EmptyLocationsList_BackEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            Back(_CollectLocation);
        }

        #endregion

        private void Location_ListEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _ItemLocationTable.ResetAll();
            _ListLookupType = "Loc";

            if (_LotNumber.Trim().Length > 0)
            {
                _ItemLocationTable.RetrieveLotSerialNumberRecords(_BranchPlant, _CollectItemNumber.ItemsTable.IMITM, _LotNumber, _WorkFlowOption.RetrieveOptionValue("ShowZeroOnHand").ToString().ToUpper().Trim());
            }
            else
            {
                _ItemLocationTable.RetrieveItemLocationRecords(_BranchPlant, _CollectItemNumber.ItemsTable.IMITM, _WorkFlowOption.RetrieveOptionValue("ShowZeroOnHand").ToString().ToUpper().Trim());
            }
        }

        private void LocationMasterList_EnterEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _LocationMasterTable.UpdateCurrentRow();
            _Location = _LocationMasterTable.LMLOCN;
            UserSession.ScreenHandler.ScreenMap.Add("LOCATION", _Location);
            Next(_CollectAdjustmentQuantity);
        }

        private void LocationMasterList_BackEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            Back(_CollectLocation);
        }

        #endregion

        #region CollectLotNumber

        /// <summary>
        /// If this is a lot number or serial number required item, and the user has entered blanks, 
        ///      issue error
        /// Otherwise, take the lot number entered and validate against the F41021 item balance table and the F4108 lot master table
        /// if the workflow option is set to not allow adjustments from held lots, and this is a held lot
        ///      issue error
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void _CollectLotNumber_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            ScreenHandler.DefaultText = _LotNumber;
        }

        private void LotNumber_EnterEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            if (String.IsNullOrEmpty(_LotNumber))
            {
                _LotNumber = e.UserInput.ToString().ToUpper();
            }

            UserSession.ScreenHandler.ScreenMap.Add("LotNumber", _LotNumber);
            LotNumberEnterKey();
        }

        private void CollectLotNumber_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            ScreenHandler.DefaultText = _LotNumber;
        }

        private void LotNumberEnterKey()
        {
            #region Lot or s/n required - validate that user has not entered blanks

            if (_LotProcessType.Length > 0)
            {
                if (Convert.ToInt16(_LotProcessType) == Convert.ToInt16(LotSerialDesignations.LotRequired))
                {
                    if (_LotNumber.Length == 0)
                    {
                        Error("LotNumber");
                        return;
                    }
                }
            }

            #endregion

            #region Validate lot entered against F41021 Item balance and F4108 Lot Master

            _ShortItemNumber = _CollectItemNumber.ItemsTable.IMITM;
            bool ItemLocationIsValid = _ItemLocationTable.ValidateItemLot(_BranchPlant, _ShortItemNumber, _LotNumber);
            bool LotMasterIsValid = _LotMasterTable.RetrieveLotMaster(_BranchPlant, _ShortItemNumber, _LotNumber);

            #endregion

            #region Positive adjustments

            if (_WorkFlowOption.RetrieveOptionValue("AdjustmentType").ToString() == "P")
            {
                if ((LotMasterIsValid && (DateTimeUtils.JDEJulian(_LotMasterTable.IOMMEJ)) < DateTime.Today))
                {
                    if (!_WorkFlowOption.GetYesNoWFOptionValue("AllowExpiredLotAdj"))
                    {
                        Error("EXPIREDLOT", _LotNumber);
                        _LotNumber = "";
                        return;
                    }

                    if (_WorkFlowOption.GetYesNoWFOptionValue("WarnForExpiredLot"))
                    {
                        Next(_ExpiredLotWarning);
                        return;
                    }
                }

                if (_FromSerialNumberEnter)
                {
                    _UnitOfMeasure = _CollectItemNumber.ItemsTable.IMUOM1;
                    _FromSerialNumberEntry = true;
                    if (Convert.ToDouble(_TransactionQuantity) > 0)
                        SendToERP("1");
                    else
                        SendToERP("-1");
                    _SerialNumbersScanned += 1;
                    UserSession.ScreenHandler.ScreenMap.Add("SERIALNUMBERSCAN", _SerialNumbersScanned.ToString());
                    Next(this._PrintBarCodeLabels);
                    return;
                }

                if (!LotMasterIsValid)
                {
                    if (_LotNumber.Length == 0 && (Convert.ToInt16(_LotProcessType) == Convert.ToInt16(LotSerialDesignations.LotOptional)))
                    {
                        // Do not proceed to _CollectLotExpirationDate
                        // if lot number is blank and lot process type is optional (IBSRCE = 0) - SCR 028953
                    }
                    else
                    {
                        Next(_CollectLotExpirationDate);
                        return;
                    }
                }

                DefaultPrimary();
                return;
            }

            #endregion

            #region Negative adjustment

            if (!ItemLocationIsValid)
            {
                Error("LOTNUMBER", _LotNumber);
                return;
            }

            //   Lot look up will fail for lot optional items where no lots exist.  
            //   DO NOT try and check the dates for invalid records.
            if (LotMasterIsValid)
            {
                _LotNumber = _ItemLocationTable.LILOTN.ToString();
                _ExpirationDate = DateTimeUtils.JDEJulian(_LotMasterTable.IOMMEJ).ToString("yyyyMMdd");
                UserSession.ScreenHandler.ScreenMap.Add("LotNumber", _LotNumber);
                if (LotMasterIsValid)
                {
                    UserSession.ScreenHandler.ScreenMap.Add("ExpDate", DateTimeUtils.JDEJulian(_LotMasterTable.IOMMEJ).ToShortDateString());
                }

                // if the processing option is set to not allow adjustments to expired lots
                // and the lot is expired - issue error
                if ((LotMasterIsValid && (DateTimeUtils.JDEJulian(_LotMasterTable.IOMMEJ)) < DateTime.Today))
                {
                    if (!_WorkFlowOption.GetYesNoWFOptionValue("AllowExpiredLotAdj"))
                    {
                        Error("EXPIREDLOT", _LotNumber);
                        return;
                    }

                    if (_WorkFlowOption.GetYesNoWFOptionValue("WarnForExpiredLot"))
                    {
                        Next(_ExpiredLotWarning);
                        return;
                    }
                }
            }

            _OnHandQuantity = _ItemLocationTable.LIPQOH;
            DefaultPrimary();

            #endregion
        }

        private void LotNumber_ListEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _ItemLocationTable.ResetAll();
            _ListLookupType = "Lot";
            if (_LotNumber.Trim().Length > 0)
            {
                _ItemLocationTable.RetrieveLotSerialNumberRecords(_BranchPlant, _CollectItemNumber.ItemsTable.IMITM, _LotNumber, _WorkFlowOption.RetrieveOptionValue("ShowZeroOnHand").ToString().ToUpper().Trim());
            }
            else
            {
                _ItemLocationTable.RetrieveItemLocationRecords(_BranchPlant, _CollectItemNumber.ItemsTable.IMITM, _WorkFlowOption.RetrieveOptionValue("ShowZeroOnHand").ToString().ToUpper().Trim());
            }
        }

        #endregion

        #region CollectSerialNumber

        #region CollectSerialNumber_EnterEvent

        /// <summary>
        /// Some Comments
        /// </summary>
        private void CollectSerialNumber_EnterEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _LotNumber = e.UserInput.ToString().ToUpper();
            SerialNumberEnterKey();
        }

        private void SerialNumberEnterKey()
        {
            UserSession.ScreenHandler.ScreenMap.Add("LotNumber", _LotNumber);
            // determine if the serial number scanned exists 
            _ItemLocationTable.ResetAll();

            _OnHandQuantity = 0;
            bool bResult = _ItemLocationTable.ValidateLotLocn(_DataManipulatorTable.FormatData("F0006", "MCMCU", _CollectBranch.BranchPlantsTable.MCMCU),
                _CollectItemNumber.ItemsTable.IMITM, _LotNumber, _Location);
            if (bResult == true)
            {
                _LotStatus = _ItemLocationTable.LILOTS;
                _OnHandQuantity = _ItemLocationTable.LIPQOH;
            }

            // validate unit of measure
            double dQty = 1;
            if (Convert.ToDouble(_TransactionQuantity) < 0)
                dQty = dQty * -1;

            if (dQty < 0)
            {
                if ((!bResult) || (_OnHandQuantity <= 0))
                {
                    Error("INVALIDSERIALNUMBER", _LotNumber);
                    return;
                }
            }
            else
            {
                if ((_OnHandQuantity != 0) && (Convert.ToDouble(_TransactionQuantity) > 0))
                {
                    if ((_LotProcessType.Trim().Length > 0) && (_LotNumber.Trim().Length == 0) &&
                        ((Convert.ToInt16(_LotProcessType) == Convert.ToInt16(LotSerialDesignations.SerialOptional))
                         || (Convert.ToInt16(_LotProcessType) == Convert.ToInt16(LotSerialDesignations.SystemAssignedSerialDate))
                         || (Convert.ToInt16(_LotProcessType) == Convert.ToInt16(LotSerialDesignations.SystemAssignedSerialNumber))))
                    {
                        // discovered during testing, the above s/n conditions should be passed through
                    }
                    else
                    {
                        Error("SNEXISTS", _LotNumber);
                        return;
                    }
                }
            }

            // if the processing option is set to not allow held lot adjustments, issue error
            if (!AllowHeldLotProcessing())
            {
                Error("HELDLOT", _LotNumber);
                return;
            }

            // check to see if this is an expired lot and whether options allow us to process
            _LotMasterTable.ResetAll();
            _LotMasterTable.IOMCU = _CollectBranch.BranchPlantsTable.MCMCU;
            _LotMasterTable.IOITM = _CollectItemNumber.ItemsTable.IMITM;
            _LotMasterTable.IOLOTN = _LotNumber;
            if (_LotMasterTable.Fetch())
            {
                if ((_LotMasterTable.Rows.Count > 0) &&
                    (DateTimeUtils.JDEJulian(_LotMasterTable.IOMMEJ) < DateTime.Today) &&
                    (!_WorkFlowOption.GetYesNoWFOptionValue("AllowExpiredLots")))
                {
                    Error("EXPIREDLOT", _LotNumber);
                    return;
                }
                else
                {
                    _ExpirationDate = DateTimeUtils.JDEJulian(_LotMasterTable.IOMMEJ).ToString("yyyyMMdd");
                }

                ;
            }

            // for negative adjustments, need to verify availability
            bool bAvail = true;

            double QuantityAvailable = 0;
            if (dQty < 0) // negative adjustment
            {
                switch (_WorkFlowOption.RetrieveOptionValue("AvailabilityCheck").ToString().ToUpper().Trim())
                {
                    case "A": // available quantity
                        QuantityAvailable = Convert.ToDouble(_ItemLocationTable.CurrentRow["Availability"].ToString());
                        break;
                    case "O": // on hand quantity
                        QuantityAvailable = _ItemLocationTable.LIPQOH;
                        break;
                    case "I":
                        QuantityAvailable = Convert.ToDouble(_TransactionQuantity);
                        if (QuantityAvailable < 0)
                            QuantityAvailable = QuantityAvailable * -1;
                        break;
                }

                if (QuantityAvailable <= 0)
                {
                    bAvail = false;
                }
            }
            else
            {
                if (QuantityAvailable > 0)
                {
                    bAvail = false;
                }
            }

            if (!bAvail)
            {
                Error("AVAILABLEQTY", _LotNumber);
                return;
            }

            _FromSerialNumberEntry = true;
            _UnitOfMeasure = _CollectItemNumber.ItemsTable.IMUOM1;

            SendToERP(dQty.ToString());
            _SerialNumbersScanned += 1;
            UserSession.ScreenHandler.ScreenMap.Add("SERIALNUMBERSCAN", _SerialNumbersScanned.ToString());
        }

        #endregion

        #region CollectSerialNumber_ListEvent

        /// <summary>
        /// Some Comments
        /// </summary>
        private void CollectSerialNumber_ListEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _ListLookupType = "SerialNumber";
            _ItemLocationTable.RetrieveItemSerialRecords(_BranchPlant, _CollectItemNumber.ItemsTable.IMITM, _Location, _WorkFlowOption.RetrieveOptionValue("ShowZeroOnHand").ToString().ToUpper().Trim());
        }

        #endregion

        #endregion

        #region CollectLotSerialExpDate

        private void LotSerialExpDate_EnterEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            DateTimeUtils dtu = new DateTimeUtils();
            if (e.UserInput.ToString().Trim().Length > 0)
            {
                if (!DateTimeUtils.ValidateDate((string) e.UserInput))
                {
                    Error("EXPDATE", e.UserInput.ToString());
                    return;
                }
            }

            _ExpirationDate = (string) e.UserInput;
            _ExpirationDate = dtu.YearAsString + dtu.MonthOfYearAsString + dtu.DayOfMonthAsString;

            if ((DateTimeUtils.JDEJulian(Convert.ToDecimal(DateTimeUtils.ConvertToJulian(_ExpirationDate)))) < DateTime.Today)
            {
                Error("EXPDATE", e.UserInput.ToString());
                return;
            }

            UserSession.ScreenHandler.ScreenMap.Add("ExpDate", _ExpirationDate);
            if (_FromSerialNumberEnter == false)
            {
                DefaultPrimary();
                Next(_CollectLocation);
            }
            else
            {
                _FromSerialNumberEntry = true;
                _UnitOfMeasure = _CollectItemNumber.ItemsTable.IMUOM1;
                if (Convert.ToDouble(_TransactionQuantity) > 0)
                    SendToERP("1");
                else
                    SendToERP("-1");
                _SerialNumbersScanned += 1;
                UserSession.ScreenHandler.ScreenMap.Add("SERIALNUMBERSCAN", _SerialNumbersScanned.ToString());
                Next(this._PrintBarCodeLabels);
            }
        }

        #endregion

        #region CollectAdjustmentQty

        private void AdjustmentQty_LeavingEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            // Call calculator function
            try
            {
                _TransactionQuantity = _CollectAdjustmentQuantity.Quantity.ToString();

                if (_WorkFlowOption.RetrieveOptionValue("AdjustmentType").ToString() == "N")
                {
                    double dQty = Convert.ToDouble(_TransactionQuantity);
                    dQty *= -1;
                    _TransactionQuantity = dQty.ToString();
                }

                UserSession.ScreenHandler.ScreenMap.Add("QUANTITY", _TransactionQuantity);
                UserSession.ScreenHandler.ScreenMap.Add("SNRequired", Math.Abs(Convert.ToDouble(_TransactionQuantity)).ToString());
                _DefaultUnitOfMeasure = _CollectItemNumber.ItemsTable.DefaultUOM(_WorkFlowOption.RetrieveOptionValue("DefaultUOM").ToString());
                UserSession.ScreenHandler.DefaultText = _DefaultUnitOfMeasure;
            }
            catch
            {
                Error("INVALIDQUANTITY", e.UserInput.ToString());
            }
        }

        #endregion

        #region CollectUnitOfMeasure

        private void CollectUnitOfMeasure_EnterEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            // user has inputted/manually defined a UoM from the CollectUnitOfMeasure screen; 

            _UOMListCalledFrom = "CollectUnitOfMeasure";
            _UnitOfMeasure = (string) e.UserInput.ToString().ToUpper();

            if (!ProcessUOMEnter())
            {
                Error(_CollectUnitOfMeasure.ErrorMessage);
            }
        }

        private bool ValidateUnitOfMeasure(string sUOM)
        {
            _UnitOfMeasure = sUOM.ToUpper();
            return ProcessUOMEnter();
        }

        private bool ProcessUOMEnter()
        {
            double ConversionFactor = 0;
            // validate unit of measure         
            double TransQuantity = Convert.ToDouble(_TransactionQuantity);

            bool Continue = true;
            if (!_WorkFlowOption.GetYesNoWFOptionValue("AllowUOMOverride"))
            {
                if (_UnitOfMeasure != _DefaultUnitOfMeasure)
                {
                    Continue = false;
                    _CollectUnitOfMeasure.ErrorMessage = "UOMOverride";
                    return false;
                }
            }

            if (Continue)
            {
                if (_UnitOfMeasure != _CollectItemNumber.ItemsTable.IMUOM1)
                {
                    ConversionFactor = _UnitOfMeasureConversionTable1.ConvertToPrimaryUOM(_BranchPlant, _ShortItemNumber, _UnitOfMeasure, _CollectItemNumber.ItemsTable.IMUOM1.ToString());
                    Log.WriteLine(5, "Conversion Factor after call to ConvertToPrimaryUOM = " + ConversionFactor.ToString());
                    Log.WriteLine(5, "Branch/Plant = " + _BranchPlant);
                    Log.WriteLine(5, "Short Item Number = " + _ShortItemNumber.ToString());
                    Log.WriteLine(5, "From U/M = " + _UnitOfMeasure.ToString());
                    Log.WriteLine(5, "To U/M = " + _CollectItemNumber.ItemsTable.IMUOM1.ToString());

                    if (ConversionFactor == 0)
                    {
                        ConversionFactor = _UnitOfMeasureConversionTable2.ConvertToPrimaryUOM(_UnitOfMeasure, _CollectItemNumber.ItemsTable.IMUOM1.ToString());
                    }
                }
                else
                {
                    ConversionFactor = 1;
                }

                Log.WriteLine(5, "Conversion Factor = " + ConversionFactor.ToString());
                if (ConversionFactor != 0)
                {
                    // for serialized items, we be done here
                    if ((_LotProcessType.Trim().Length > 0) &&
                        (Convert.ToInt16(_LotProcessType) >= Convert.ToInt16(LotSerialDesignations.SerialOptional)))
                    {
                        Next(_CollectSerialNumber);
                        return true;
                    }

                    Log.WriteLine(5, "Transaction Quantity before applying conversion factor = " + TransQuantity.ToString());

                    TransQuantity = TransQuantity * ConversionFactor;
                    Log.WriteLine(5, "Transaction Quantity after applying conversion factor = " + TransQuantity.ToString());

                    // for a negative adjustment, we need to flip the sign for availability comparison
                    double dWrkQty = TransQuantity;
                    // for negative adjustments, need to verify availability   
                    bool bAvail = true;
                    double QuantityAvailable = 0;

                    // only check availability on negative adjustments
                    if (dWrkQty < 0)
                    {
                        switch (_WorkFlowOption.RetrieveOptionValue("AvailabilityCheck").ToString().ToUpper().Trim())
                        {
                            case "A": // available quantity
                                QuantityAvailable = Convert.ToDouble(_ItemLocationTable.CurrentRow["Availability"].ToString());
                                break;
                            case "O": // on hand quantity
                                QuantityAvailable = _ItemLocationTable.LIPQOH;
                                break;
                            case "I":
                                QuantityAvailable = Convert.ToDouble(_TransactionQuantity);
                                if (QuantityAvailable < 0)
                                    QuantityAvailable = QuantityAvailable * -1;
                                break;
                        }
                    }

                    Log.WriteLine(5, "Available Quantity = " + QuantityAvailable.ToString());

                    if (dWrkQty < 0) // negative adjustment
                    {
                        if (Math.Abs(QuantityAvailable) < Math.Abs(dWrkQty))
                        {
                            bAvail = false;
                        }
                    }

                    if (!bAvail)
                    {
                        if ((_LotProcessType.Trim().Length > 0) &&
                            (Convert.ToInt16(_LotProcessType) == Convert.ToInt16(LotSerialDesignations.SerialRequired)))
                        {
                            UserSession.ScreenHandler.DefaultText = (_UnitOfMeasure);
                        }
                        else
                        {
                            UserSession.ScreenHandler.DefaultText = (_UnitOfMeasure);
                        }

                        _CollectUnitOfMeasure.ErrorMessage = "AVAILABLEQTY";
                        return false;
                    }
                    else
                    {
                        _FromSerialNumberEntry = false;
                        // convert quantity to primary for JDE update
                        _TransactionQuantity = TransQuantity.ToString();
                        _UnitOfMeasure = _CollectItemNumber.ItemsTable.IMUOM1;

                        return true;
                    }
                }
                else
                {
                    this._CollectUnitOfMeasure.ErrorMessage = "UNITOFMEASURE";
                    return false;
                }
            }
            else
            {
                this._CollectUnitOfMeasure.ErrorMessage = "UNITOFMEASURE";
                return false;
            }
        }

        #region CollectUnitOfMeasure_ListEvent

        private void CollectUnitOfMeasure_ListEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _UOMListCalledFrom = "CollectUnitOfMeasure";
        }

        #endregion

        #region UnitOfMeasureConversionList_EnterEvent

        private void UnitOfMeasureConversionList_EnterEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            // user has selected a record from the list   
            _UnitOfMeasureConversionTable1.UpdateCurrentRow();

            switch (_UOMListCalledFrom)
            {
                case "CollectDualUnitOfMeasure":
                    if (_CollectDualUnitOfMeasure.Validate(_UnitOfMeasureConversionTable1.UMUM.ToString()))
                    {
                        CollectDualUnitOfMeasure_LeavingEvent(sender, new ICS.Display.ScreenEventArgs(ActionKey.Enter, new ICS.Display.InputHandling.Input(_DualUnitOfMeasure)));
                    }
                    else
                    {
                        Error(_CollectDualUnitOfMeasure.ErrorMessage);
                    }

                    break;
                case "CollectUnitOfMeasure":
                    _UnitOfMeasure = _UnitOfMeasureConversionTable1.UMUM.ToString();
                    if (!ProcessUOMEnter())
                    {
                        Error(_CollectUnitOfMeasure.ErrorMessage);
                    }

                    break;
            }
        }

        private void UnitOfMeasureConversionList_BackEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            switch (_UOMListCalledFrom)
            {
                case "CollectDualUnitOfMeasure":
                    Back(_CollectDualUnitOfMeasure);
                    break;
                case "CollectUnitOfMeasure":
                    Back(_CollectUnitOfMeasure);
                    break;
            }
        }

        #endregion

        #endregion

        #region CollectDualQuantity

        #region CollectDualQuantity_LoadEvent

        private void CollectDualQuantity_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            if (!_CollectItemNumber.IsDualQtyRequired)
            {
                _CollectDualQuantity.CancelLoad = true;
                _CollectDualUnitOfMeasure.CancelLoad = true;
                SendToERP(_TransactionQuantity);
                Next(_PrintBarCodeLabels);
            }
            else
            {
                _CollectDualQuantity.CancelLoad = false;
                _CollectDualUnitOfMeasure.CancelLoad = false;
            }
        }

        #endregion

        #region CollectDualQuantity_LeavingEvent

        private void CollectDualQuantity_LeavingEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _DualQuantity = _CollectDualQuantity.Quantity;
        }

        #endregion

        #endregion

        #region CollectDualUnitOfMeasure

        #region CollectDualUnitOfMeasure_LeavingEvent

        private void CollectDualUnitOfMeasure_LeavingEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _DualUnitOfMeasure = _CollectDualUnitOfMeasure.UnitOfMeasure;
            SendToERP(_TransactionQuantity);
        }

        #endregion

        #region CollectDualUnitOfMeasure_ListEvent

        private void CollectDualUnitOfMeasure_ListEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _UOMListCalledFrom = "CollectDualUnitOfMeasure";
        }

        #endregion

        #endregion

        #region SendToERP

        private void setToLocation()
        {
            _AdjustmentTransaction.Location = _Location;
            if (_LocationMasterTable.ValidateLocation(_CollectBranch.BranchPlantsTable.MCMCU.Trim(), _Location))
            {
                _AdjustmentTransaction.ToLocnPutZone = _LocationMasterTable.LMPZON.Trim();
                _AdjustmentTransaction.ToLocnPickZone = _LocationMasterTable.LMKZON.Trim();
                _AdjustmentTransaction.ToLocnReplZone = _LocationMasterTable.LMZONR.Trim();
                _AdjustmentTransaction.ToLocnDimGroup = _LocationMasterTable.LMSTY1.Trim();
            }
        }

        private void setItem()
        {
            _AdjustmentTransaction.PrimaryItemNumber = _PrimaryItemNumber.ToString();
            _AdjustmentTransaction.ShortItem = _ShortItemNumber.ToString();
            _AdjustmentTransaction.SecondItem = _CollectItemNumber.ItemsTable.IMLITM.ToString();
            _AdjustmentTransaction.ThirdItem = _CollectItemNumber.ItemsTable.IMAITM.ToString();
            _AdjustmentTransaction.ItemDescription1 = _CollectItemNumber.ItemsTable.IMDSC1; // Needed for WOW
            _AdjustmentTransaction.ItemDescription2 = _CollectItemNumber.ItemsTable.IMDSC2; // Needed for WOW
            _ItemBranchTable.ValidateItemBranch(_CollectBranch.BranchPlantsTable.MCMCU.Trim(), _ShortItemNumber);
            _AdjustmentTransaction.ItemDimGroup = _ItemBranchTable.IBPRP6.Trim();
            _AdjustmentTransaction.ItemLotProcType = _ItemBranchTable.IBSRCE;
        }

        private void SendToERP(string Quantity)
        {
            Transactions.Add(_AdjustmentTransaction);
            _AdjustmentTransaction.Initialize();
            setItem();
            setToLocation();
            _AdjustmentTransaction.BranchPlant = _CollectBranch.BranchPlantsTable.MCMCU;
            _AdjustmentTransaction.GLExplanation = _Explanation;
            _AdjustmentTransaction.ReasonCode = _AdjustmentReasonCode.Trim();
            _AdjustmentTransaction.Location = _Location;
            _AdjustmentTransaction.LotStatus = _LotStatus;
            _AdjustmentTransaction.Lot = _LotNumber;

            // Needed for WOW
            if (_CollectItemNumber.ItemBranchesTable.IBSRCE.Trim() == "")
            {
                _AdjustmentTransaction.LotProcessType = " ";
            }
            else
            {
                _AdjustmentTransaction.LotProcessType = _CollectItemNumber.ItemBranchesTable.IBSRCE;
            }

            _AdjustmentTransaction.TransactionQty = Quantity;
            _AdjustmentTransaction.UOM = _UnitOfMeasure;

            #region Dual Unit Of Measure

            _AdjustmentTransaction.SecondaryQty = _DualQuantity.ToString();
            _AdjustmentTransaction.SecondaryUOM = _DualUnitOfMeasure;

            #endregion

            _AdjustmentTransaction.LotExpDate = _ExpirationDate.ToString();
            _AdjustmentTransaction.RFSUserID = UserSession.CurrentUser.LoginName;
            _AdjustmentTransaction.TransDate_YYYYMMDD = DateTime.Now.ToString("yyyyMMdd");
            _AdjustmentTransaction.Version = _WorkFlowOption.RetrieveOptionValue("ERPUpdateVersion").ToString().Trim();
            Transactions.Send(_WorkFlowOption.RetrieveOptionValue("SyncTransaction").ToString());
            DisplayMessages();
        }

        #endregion

        #region ClearTransaction

        // clear out transactional fields and return to item number screen
        private void ClearTransaction()
        {
            _ShortItemNumber = 0;
            _PrimaryItemNumber = "";
            _LotProcessType = "";
            _Location = "";
            _LotStatus = "";
            _LotNumber = "";
            _TransactionQuantity = "";
            _UnitOfMeasure = "";
            _ExpirationDate = "";
            _LotProcessType = "";
            // clear out screen map
            UserSession.ScreenHandler.ScreenMap.Clear("SERIALNUMBERSCAN");
            UserSession.ScreenHandler.ScreenMap.Clear("LOCATION");
            UserSession.ScreenHandler.ScreenMap.Clear("ITEMNUMBER");
            UserSession.ScreenHandler.ScreenMap.Clear("LotNumber");
            UserSession.ScreenHandler.ScreenMap.Clear("ExpDate");
            UserSession.ScreenHandler.ScreenMap.Clear("QUANTITY");
            UserSession.ScreenHandler.ScreenMap.Clear("UnitOfMeasure");
            UserSession.ScreenHandler.DefaultText = "";
            _SerialNumbersScanned = 0;

            _DualQuantity = 0;
            _DualUnitOfMeasure = "";
        }

        #endregion

        #region ExpiredLotWarning_EnterEvent

        private void ExpiredLotWarning_EnterEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            if (e.UserInput.ToBool())
            {
                DefaultPrimary();
                Next(_CollectLocation);
            }
            else
            {
                Back();
            }
        }

        #endregion

        #region ItemLocationList Events

        private void ItemLocationList_EnterEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            // user has selected a record from the list

            this._ItemLocationTable.UpdateCurrentRow();

            _LocationMasterTable.MCU = _BranchPlant;
            _LocationMasterTable.inLOCN = _ItemLocationTable.LILOCN.ToString();
            switch (_ListLookupType)
            {
                case "Item":
                    _CollectItemNumber.ItemsTable.ResetAll();
                    _CollectItemNumber.ItemsTable.IMITM = _ItemLocationTable.LIITM;
                    _CollectItemNumber.ItemsTable.Fetch();
                    if (_CollectItemNumber.Validate(new ICS.Display.InputHandling.Input(this._CollectItemNumber.ItemsTable.IMLITM)))
                    {
                        ItemNumber_LeavingEvent(sender, e);
                    }
                    else
                    {
                        Error(_CollectItemNumber.ErrorMessage);
                    }

                    break;
                case "Loc":
                    _Location = _ItemLocationTable.LILOCN;
                    LocationEnterKey();
                    break;
                case "Lot":
                    _LotNumber = _ItemLocationTable.LILOTN.ToString();
                    this.UserSession.ScreenHandler.ScreenMap.Add("LotNumber", this._LotNumber);
                    LotNumberEnterKey();
                    break;
                case "SerialNumber":
                    _LotNumber = _ItemLocationTable.LILOTN.ToString();
                    SerialNumberEnterKey();
                    break;
            }
        }

        private void ItemLocationList_BackEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            switch (_ListLookupType)
            {
                case "Item":
                    Back(_CollectItemNumber);
                    break;
                case "Loc":
                    Back(_CollectLocation);
                    break;
                case "Lot":
                    Back(_CollectLotNumber);
                    break;
                case "SerialNumber":
                    Back(_CollectSerialNumber);
                    break;
            }
        }

        #endregion

        #region PrintBarCodeLabels

        #region PrintBarCodeLabels_EnterEvent

        /// <summary>
        /// Some Comments
        /// </summary>
        private void PrintBarCodeLabels_EnterEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _Copies = e.UserInput.ToString().Trim();
            //bypass printing if user enteres 0 or blank on collect label qty screen
            if (_Copies.Length <= 0 || _Copies == "0")
            {
                if ((_FromSerialNumberEntry) && (_SerialNumbersScanned < Math.Abs(Convert.ToInt32(_TransactionQuantity))))
                {
                    Back(_CollectSerialNumber);
                    return;
                }

                ClearTransaction();
                Back(_CollectItemNumber);
                return;
            }

            try
            {
                decimal labelQty = Convert.ToDecimal(_Copies);

                if (labelQty < 0)
                {
                    throw new ArgumentException("Negative label qty entered");
                }

                if (_WorkFlowOption.RetrieveOptionValue("MaxLabels").ToString().Trim().Length > 0)
                {
                    if (Convert.ToInt16(_Copies.Trim()) > Convert.ToInt16(_WorkFlowOption.RetrieveOptionValue("MaxLabels").ToString().Trim()))
                    {
                        Error("MAXEXCEEDED", e.UserInput.ToString());
                        return;
                    }
                }

                this.PrintSystem.Reset();
                this.PrintSystem.Branch = _BranchPlant;
                this.PrintSystem.ItemNumber = _PrimaryItemNumber;
                this.PrintSystem.Location = _LocationMasterTable.stbllocn;
                if (this.PrintSystem.DefaultPrinter.Trim().Length == 0)
                    Error("LabelFormat");
                else if (PrintSystem.ValidPrinter(PrintSystem.DefaultPrinter))
                    UserSession.ScreenHandler.DefaultText = this.PrintSystem.DefaultPrinter.ToString();
            }
            catch
            {
                Error("QUANTITY", e.UserInput.ToString());
            }
        }

        #endregion

        #endregion

        #region SelectPrinter

        #region SelectPrinter_EnterEvent

        /// <summary>
        /// Some Comments
        /// </summary>
        private void SelectPrinter_EnterEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            //_PrinterAddress = e.UserInput.ToString();
            _PrinterName = e.UserInput.ToString().Trim();
            SelectPrinterEnter();
        }

        private void SelectPrinterEnter()
        {
            if (this.PrintSystem.ValidPrinter(_PrinterName))
            {
                _PrinterAddress = PrintSystem.PrinterAddress;
                PrintBarCode();
                if ((_FromSerialNumberEntry) && (_SerialNumbersScanned < Convert.ToInt32(_TransactionQuantity)))
                {
                    Back(_CollectSerialNumber);
                    return;
                }

                ClearTransaction();
                Back(_CollectItemNumber);
            }
            else
            {
                Error(this.PrintSystem.ErrorMessageVocabulary);
            }
        }

        #endregion

        #endregion

        #region CollectPrinter_ListEvent

        private void DefinedPrinters_EnterEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _DefinedPrintersTable.UpdateCurrentRow();
            _PrinterName = _DefinedPrintersTable.PrinterName;
            SelectPrinterEnter();
        }

        private void DefinedPrinters_BackEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            Back(_SelectPrinter);
        }

        #endregion

        #region PrintBarCode

        private void PrintBarCode()
        {
            PrintSystem.DropFilePathOverride = _WorkFlowOption.RetrieveOptionValue("DropFilePath").ToString();
            PrintSystem.PrinterAddress = _PrinterAddress;
            PrintSystem.PrinterName = _PrinterName;

            try
            {
                PrintSystem.Copies = Convert.ToInt32(_Copies);
            }
            catch
            {
                PrintSystem.Copies = 0;
            }

            F4104 itemXRef = new F4104(UserSession.DataAccess, "");
            itemXRef.ResetAll();
            itemXRef.IVITM = _ShortItemNumber;
            itemXRef.IVXRT = "C";
            if (itemXRef.Fetch())
            {
                PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_ItemXRef", itemXRef.IVCITM, false);
            }

            PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_ThirdItemNumber", _CollectItemNumber.ItemsTable.IMAITM, false);
            PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_LongItemNumber", _CollectItemNumber.ItemsTable.IMLITM, false);
            PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_LotNumber", _LotNumber, false);
            PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_TransactionQuantity", _TransactionQuantity.ToString(), false);
            PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_UnitOfMeasure", _UnitOfMeasure, false);
            PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_LocationMasterTable", _LocationMasterTable.sscnlocn, false);
            PrintSystem.ExtraPrintData.AddColumnAndSetDefaultData("_PrimaryItemNumber", _PrimaryItemNumber, false);
            PrintSystem.ExtraPrintData.NewRow();

            if (!PrintSystem.Print())
            {
                DisplayMessage(PrintSystem.ErrorMessageVocabulary);
            }
        }

        #endregion

        #region ALotControlledItem

        private bool ALotControlledItem()
        {
            bool ReturnValue = false;
            // check to see if this is a lot controlled item, and if so, display the lot
            // verification screen
            if (_LotProcessType.ToString().Trim().Length > 0)
            {
                if ((Convert.ToInt16(_LotProcessType) >= 0) &&
                    (Convert.ToInt16(_LotProcessType) <= Convert.ToInt16(LotSerialDesignations.LotRequired)))
                {
                    ReturnValue = true;
                }
            }

            return ReturnValue;
        }

        #endregion

        #region DefaultPrimary

        private void DefaultPrimary()
        {
            _LocationMasterTable.ResetAll();
            _LocationMasterTable.inLOCN = _ItemLocationTable.RetrievePrimaryLocation(_BranchPlant, _ShortItemNumber);
            _LocationMasterTable.MCU = this._BranchPlant;
            if (_LocationMasterTable.ValidateLocation())
            {
                UserSession.ScreenHandler.ScreenMap.Add("PrimaryLocation", _LocationMasterTable.sscnlocn);
            }

            // if the workflow option to default in the primary location is on,
            // then we need to retrieve this item's primary location in the current
            // branch/plant
            if (_WorkFlowOption.GetYesNoWFOptionValue("SuggestFromPrimary"))
                UserSession.ScreenHandler.DefaultText = _LocationMasterTable.sscnlocn;
        }

        #endregion

        #region AllowHeldLotProcessing

        private bool AllowHeldLotProcessing()
        {
            bool ReturnValue = false;
            if ((_AllowedLotHoldCodes[0].Trim() == "*") || (_LotStatus.Trim().Length == 0))
                return (true);

            for (int i = 0; i < _AllowedLotHoldCodes.Length; i++)
            {
                if (_LotStatus.Trim() == _AllowedLotHoldCodes[i].ToString().Trim())
                {
                    ReturnValue = true;
                    i = _AllowedLotHoldCodes.Length; // force end
                } // end if
            } // end for

            return (ReturnValue);
        }

        #endregion

        public SRChinaInventoryAdjustments()
            : base()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this._LocationMasterList = new ICS.Workflow.Base.WorkflowDesign.Components.GenericList(this);
            this._ExpiredLotWarning = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this._CollectLocation = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this._ItemBalanceList = new ICS.Workflow.Base.WorkflowDesign.Components.GenericList(this);
            this._CollectLotNumber = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this._CollectAdjustmentQuantity = new ICS.Workflow.Base.JDE.Components.QuantityScreen(this);
            this._CollectItemNumber = new ICS.Workflow.Base.JDE.Components.ItemScreen(this);
            this._CollectBranch = new ICS.Workflow.Base.JDE.Components.BranchPlantScreen(this);
            this._BranchPlantList = new ICS.Workflow.Base.WorkflowDesign.Components.GenericList(this);
            this._CollectExplanation = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this._CollectReasonCode = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this._UserDefinedCodesList = new ICS.Workflow.Base.WorkflowDesign.Components.GenericList(this);
            this._CollectUnitOfMeasure = new ICS.Workflow.Base.JDE.Components.UnitOfMeasureScreen(this);
            this._UnitOfMeasureConversionList = new ICS.Workflow.Base.WorkflowDesign.Components.GenericList(this);
            this._CollectDualQuantity = new ICS.Workflow.Base.JDE.Components.DualQuantityScreen(this);
            this._CollectDualUnitOfMeasure = new ICS.Workflow.Base.JDE.Components.DualUnitOfMeasureScreen(this);
            this._PrintBarCodeLabels = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this._SelectPrinter = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this._DefinedPrintersList = new ICS.Workflow.Base.WorkflowDesign.Components.GenericList(this);
            this._CollectSerialNumber = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            this._EmptyLocationsList = new ICS.Workflow.Base.WorkflowDesign.Components.GenericList(this);
            this._CollectLotExpirationDate = new ICS.Workflow.Base.WorkflowDesign.Components.GenericScreen(this);
            // 
            // _LocationMasterList
            // 
            this._LocationMasterList.ScreenName = "F4100_List";
            this._LocationMasterList.EnterValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.LocationMasterList_EnterEvent);
            this._LocationMasterList.BackValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.LocationMasterList_BackEvent);
            this._LocationMasterList.ExitValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.LocationMasterList_BackEvent);
            this._LocationMasterList.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._LocationMasterList_LoadEvent);
            this._LocationMasterList.LeavingEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._LocationMasterList_LeavingEvent);
            // 
            // _ExpiredLotWarning
            // 
            this._ExpiredLotWarning.NextScreen = this._CollectLocation;
            this._ExpiredLotWarning.ScreenName = "ExpiredLotWarning";
            this._ExpiredLotWarning.EnterValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.ExpiredLotWarning_EnterEvent);
            // 
            // _CollectLocation
            // 
            this._CollectLocation.ListScreen = this._ItemBalanceList;
            this._CollectLocation.ListScreenControl = this._ItemBalanceList;
            this._CollectLocation.NextScreen = this._CollectAdjustmentQuantity;
            this._CollectLocation.ScreenName = "CollectLocation";
            this._CollectLocation.EnterValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.Location_EnterEvent);
            this._CollectLocation.BackValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.Location_BackEvent);
            this._CollectLocation.ListEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.Location_ListEvent);
            this._CollectLocation.KeyEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.CollectLocation_KeyEvent);
            // 
            // _ItemBalanceList
            // 
            this._ItemBalanceList.ListScreenParent = this._CollectSerialNumber;
            this._ItemBalanceList.ParentScreen = this._CollectSerialNumber;
            this._ItemBalanceList.ScreenName = "F41021_List";
            this._ItemBalanceList.EnterValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.ItemLocationList_EnterEvent);
            this._ItemBalanceList.BackValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.ItemLocationList_BackEvent);
            this._ItemBalanceList.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._ItemBalanceList_LoadEvent);
            // 
            // _CollectLotNumber
            // 
            this._CollectLotNumber.ListScreen = this._ItemBalanceList;
            this._CollectLotNumber.ListScreenControl = this._ItemBalanceList;
            this._CollectLotNumber.NextScreen = this._CollectLocation;
            this._CollectLotNumber.ScreenName = "CollectLotNumber";
            this._CollectLotNumber.EnterValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.LotNumber_EnterEvent);
            this._CollectLotNumber.ListEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.LotNumber_ListEvent);
            this._CollectLotNumber.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._CollectLotNumber_LoadEvent);
            // 
            // _CollectAdjustmentQuantity
            // 
            this._CollectAdjustmentQuantity.ItemScreen = this._CollectItemNumber;
            this._CollectAdjustmentQuantity.NextScreen = this._CollectUnitOfMeasure;
            this._CollectAdjustmentQuantity.ScreenName = "CollectAdjustmentQuantity";
            this._CollectAdjustmentQuantity.LeavingEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.AdjustmentQty_LeavingEvent);
            // 
            // _CollectItemNumber
            // 
            this._CollectItemNumber.BranchScreen = this._CollectBranch;
            this._CollectItemNumber.ItemBranchesTable = null;
            this._CollectItemNumber.ItemsTable = null;
            this._CollectItemNumber.ListScreen = this._ItemBalanceList;
            this._CollectItemNumber.ListScreenControl = this._ItemBalanceList;
            this._CollectItemNumber.NextScreen = this._CollectLocation;
            this._CollectItemNumber.ScreenName = "CollectItemNumber";
            this._CollectItemNumber.ListEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.ItemNumber_ListEvent);
            this._CollectItemNumber.LeavingEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.ItemNumber_LeavingEvent);
            this._CollectItemNumber.ValidateMethod += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ValidationHandler(this.CollectItemNumber_ValidateMethod);
            // 
            // _CollectBranch
            // 
            this._CollectBranch.ListScreen = this._BranchPlantList;
            this._CollectBranch.ListScreenControl = this._BranchPlantList;
            this._CollectBranch.NextScreen = this._CollectExplanation;
            this._CollectBranch.ScreenName = "CollectBranch";
            this._CollectBranch.WarehouseConstantsTable = null;
            this._CollectBranch.LeavingEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.CollectBranch_LeavingEvent);
            this._CollectBranch.BranchPlantsList.BackScreen = this._CollectBranch;
            this._CollectBranch.BranchPlantsList.ScreenName = "F0006_List";
            // 
            // _BranchPlantList
            // 
            this._BranchPlantList.ListScreenParent = this._CollectBranch;
            this._BranchPlantList.ParentScreen = this._CollectBranch;
            this._BranchPlantList.ScreenName = "F0006_List";
            this._BranchPlantList.EnterValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.BranchPlantList_EnterEvent);
            this._BranchPlantList.BackValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.BranchPlantList_BackEvent);
            this._BranchPlantList.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._BranchPlantList_LoadEvent);
            // 
            // _CollectExplanation
            // 
            this._CollectExplanation.NextScreen = this._CollectReasonCode;
            this._CollectExplanation.ScreenName = "CollectExplanation";
            this._CollectExplanation.EnterValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.Explanation_EnterEvent);
            // 
            // _CollectReasonCode
            // 
            this._CollectReasonCode.ListScreen = this._UserDefinedCodesList;
            this._CollectReasonCode.ListScreenControl = this._UserDefinedCodesList;
            this._CollectReasonCode.NextScreen = this._CollectItemNumber;
            this._CollectReasonCode.ScreenName = "CollectReasonCode";
            this._CollectReasonCode.EnterValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.ReasonCode_EnterEvent);
            // 
            // _UserDefinedCodesList
            // 
            this._UserDefinedCodesList.ListScreenParent = this._CollectReasonCode;
            this._UserDefinedCodesList.ParentScreen = this._CollectReasonCode;
            this._UserDefinedCodesList.ScreenName = "F0005_List";
            this._UserDefinedCodesList.EnterValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.UserDefinedCodesList_EnterEvent);
            this._UserDefinedCodesList.BackValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.UserDefinedCodesList_BackEvent);
            this._UserDefinedCodesList.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._UserDefinedCodesList_LoadEvent);
            // 
            // _CollectUnitOfMeasure
            // 
            this._CollectUnitOfMeasure.BranchScreen = this._CollectBranch;
            this._CollectUnitOfMeasure.DisableToleranceCheck = true;
            this._CollectUnitOfMeasure.DualQtyScreen = null;
            this._CollectUnitOfMeasure.ItemScreen = this._CollectItemNumber;
            this._CollectUnitOfMeasure.ListScreen = this._UnitOfMeasureConversionList;
            this._CollectUnitOfMeasure.ListScreenControl = this._UnitOfMeasureConversionList;
            this._CollectUnitOfMeasure.NextScreen = this._CollectDualQuantity;
            this._CollectUnitOfMeasure.OriginalQtyScreen = this._CollectAdjustmentQuantity;
            this._CollectUnitOfMeasure.OriginalUOMScreen = null;
            this._CollectUnitOfMeasure.ScreenName = "CollectUnitOfMeasure";
            this._CollectUnitOfMeasure.ListEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.CollectUnitOfMeasure_ListEvent);
            this._CollectUnitOfMeasure.ValidateMethod += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ValidationHandler(this.ValidateUnitOfMeasure);
            this._CollectUnitOfMeasure.UOMConversionsList.ScreenName = "F41002_List";
            // 
            // _UnitOfMeasureConversionList
            // 
            this._UnitOfMeasureConversionList.ListScreenParent = this._CollectDualUnitOfMeasure;
            this._UnitOfMeasureConversionList.ParentScreen = this._CollectDualUnitOfMeasure;
            this._UnitOfMeasureConversionList.ScreenName = "F41002_List";
            this._UnitOfMeasureConversionList.EnterValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.UnitOfMeasureConversionList_EnterEvent);
            this._UnitOfMeasureConversionList.BackValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.UnitOfMeasureConversionList_BackEvent);
            this._UnitOfMeasureConversionList.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._UnitOfMeasureConversionList_LoadEvent);
            // 
            // _CollectDualQuantity
            // 
            this._CollectDualQuantity.IsDualQuantityScreen = true;
            this._CollectDualQuantity.ItemScreen = this._CollectItemNumber;
            this._CollectDualQuantity.NextScreen = this._CollectDualUnitOfMeasure;
            this._CollectDualQuantity.ScreenName = "CollectDualQuantity";
            this._CollectDualQuantity.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.CollectDualQuantity_LoadEvent);
            this._CollectDualQuantity.LeavingEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.CollectDualQuantity_LeavingEvent);
            // 
            // _CollectDualUnitOfMeasure
            // 
            this._CollectDualUnitOfMeasure.BranchScreen = this._CollectBranch;
            this._CollectDualUnitOfMeasure.DisableToleranceCheck = true;
            this._CollectDualUnitOfMeasure.DualQtyScreen = this._CollectDualQuantity;
            this._CollectDualUnitOfMeasure.IsDualUnitOfMeasureScreen = true;
            this._CollectDualUnitOfMeasure.ItemScreen = this._CollectItemNumber;
            this._CollectDualUnitOfMeasure.ListScreen = this._UnitOfMeasureConversionList;
            this._CollectDualUnitOfMeasure.ListScreenControl = this._UnitOfMeasureConversionList;
            this._CollectDualUnitOfMeasure.NextScreen = this._PrintBarCodeLabels;
            this._CollectDualUnitOfMeasure.OriginalQtyScreen = this._CollectAdjustmentQuantity;
            this._CollectDualUnitOfMeasure.OriginalUOMScreen = this._CollectUnitOfMeasure;
            this._CollectDualUnitOfMeasure.ScreenName = "CollectDualUnitofMeasure";
            this._CollectDualUnitOfMeasure.ListEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.CollectDualUnitOfMeasure_ListEvent);
            this._CollectDualUnitOfMeasure.LeavingEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.CollectDualUnitOfMeasure_LeavingEvent);
            this._CollectDualUnitOfMeasure.UOMConversionsList.ScreenName = "F41002_List";
            // 
            // _PrintBarCodeLabels
            // 
            this._PrintBarCodeLabels.NextScreen = this._SelectPrinter;
            this._PrintBarCodeLabels.ScreenName = "PrintBarCodeLabels";
            this._PrintBarCodeLabels.EnterValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.PrintBarCodeLabels_EnterEvent);
            // 
            // _SelectPrinter
            // 
            this._SelectPrinter.ListScreen = this._DefinedPrintersList;
            this._SelectPrinter.ListScreenControl = this._DefinedPrintersList;
            this._SelectPrinter.ScreenName = "SelectPrinter";
            this._SelectPrinter.EnterValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.SelectPrinter_EnterEvent);
            // 
            // _DefinedPrintersList
            // 
            this._DefinedPrintersList.ListScreenParent = this._SelectPrinter;
            this._DefinedPrintersList.ParentScreen = this._SelectPrinter;
            this._DefinedPrintersList.ScreenName = "FRFS_Printers";
            this._DefinedPrintersList.EnterValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.DefinedPrinters_EnterEvent);
            this._DefinedPrintersList.BackValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.DefinedPrinters_BackEvent);
            this._DefinedPrintersList.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._DefinedPrintersList_LoadEvent);
            // 
            // _CollectSerialNumber
            // 
            this._CollectSerialNumber.ListScreen = this._ItemBalanceList;
            this._CollectSerialNumber.ListScreenControl = this._ItemBalanceList;
            this._CollectSerialNumber.NextScreen = this._PrintBarCodeLabels;
            this._CollectSerialNumber.ScreenName = "CollectSerialNumber";
            this._CollectSerialNumber.EnterValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.CollectSerialNumber_EnterEvent);
            this._CollectSerialNumber.ListEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.CollectSerialNumber_ListEvent);
            // 
            // _EmptyLocationsList
            // 
            this._EmptyLocationsList.ScreenName = "F4100Empty_List";
            this._EmptyLocationsList.EnterValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.EmptyLocationsList_EnterEvent);
            this._EmptyLocationsList.BackValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this.EmptyLocationsList_BackEvent);
            this._EmptyLocationsList.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._EmptyLocationsList_LoadEvent);
            this._EmptyLocationsList.LeavingEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._EmptyLocationsList_LeavingEvent);
            // 
            // _CollectLotExpirationDate
            // 
            this._CollectLotExpirationDate.NextScreen = this._CollectLocation;
            this._CollectLotExpirationDate.ScreenName = "CollectLotExpirationDate";
            this._CollectLotExpirationDate.EnterValidationEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._CollectLotExpirationDate_EnterValidationEvent);
            this._CollectLotExpirationDate.LoadEvent += new ICS.Workflow.Base.WorkflowDesign.Components.RFSScreenControl.ScreenControlEventHandler(this._CollectLotExpirationDate_LoadEvent);
            // 
            // SRChinaInventoryAdjustments
            // 
            this.StartScreen = this._CollectBranch;
            this.WorkflowID = "SRChinaInventoryAdjustment";
        }

        private void _EmptyLocationsList_LeavingEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
        }

        private void _EmptyLocationsList_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
        }

        private void _DefinedPrintersList_LeavingEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
        }

        private void _UnitOfMeasureConversionList_LeavingEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
        }

        private void _UserDefinedCodesList_LeavingEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
        }

        private void _BranchPlantList_LeavingEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
        }

        private void _ItemBalanceList_LeavingEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
        }

        private void _ItemBalanceList_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
        }

        private void _LocationMasterList_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
        }

        private void _LocationMasterList_LeavingEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
        }

        private void _BranchPlantList_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _CollectBranch.BranchPlantsTable.ResetAll();
            _CollectBranch.BranchPlantsTable.FetchBranchPlants();
        }

        private void _UserDefinedCodesList_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _UserDefinedCodesTable.ResetAll();
            _UserDefinedCodesTable.DRSY = "42  ";
            _UserDefinedCodesTable.DRRT = "RC";
            _UserDefinedCodesTable.Fetch();
        }

        private void _UnitOfMeasureConversionList_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _UnitOfMeasureConversionTable1.ResetAll();
            _UnitOfMeasureConversionTable1.UMITM = _CollectItemNumber.ItemsTable.IMITM;
            _UnitOfMeasureConversionTable1.Fetch();
        }

        private void _DefinedPrintersList_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            _DefinedPrintersTable.FetchPrintersByUserWorkflow(UserSession.UserId, WorkflowID);
        }

        private void _CollectLotExpirationDate_EnterValidationEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            DateTimeUtils dtu = new DateTimeUtils();
            _ExpirationDate = "";
            try
            {
                if (e.UserInput.ToString().Trim().Length > 0)
                {
                    // validate date
                    if (!DateTimeUtils.ValidateDate(e.UserInput.ToString(), WFOptions.RetrieveOptionValue("DateFormat")))
                    {
                        LoadFieldInError(e.UserInput.ToString());
                        Error("EXPDATE");
                        return;
                    }

                    _ExpirationDate = dtu.YearAsString + dtu.MonthOfYearAsString + dtu.DayOfMonthAsString;
                    UserSession.ScreenHandler.ScreenMap.Add("ExpDate", e.UserInput.ToString());
                    if ((DateTimeUtils.JDEJulian(Convert.ToDecimal(DateTimeUtils.ConvertToJulian(_ExpirationDate)))) < DateTime.Today)
                    {
                        LoadFieldInError(e.UserInput.ToString());
                        Error("EXPDATE");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex);
                LoadFieldInError(e.UserInput.ToString());
                Error("EXPDATE");
                return;
            }

            DefaultPrimary();
            Next(_CollectLocation);
        }

        private void _CollectLotExpirationDate_LoadEvent(RFSScreenControl sender, ScreenEventArgs e)
        {
            ScreenMap.Add("DateFormat", WFOptions.RetrieveOptionValue("DateFormat").ToString());
        }
    }
}