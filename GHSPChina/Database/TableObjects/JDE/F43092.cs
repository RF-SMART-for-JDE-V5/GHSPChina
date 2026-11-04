using ICS.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS.SRChina.Wireless.JDE.Database.TableObjects
{
    /// <summary>
    /// A derived class for JDE Table - F43092
    /// </summary>
    public class F43092 : ICS.Database.TableObjects.JDE.F43092
    {
        #region Constructors

        /// <summary>
        /// Initializes a new F43092
        /// </summary>
        /// <remarks>Used for Enumerator</remarks>
        public F43092() : base()
        {
        }

        /// <summary>
        /// Initializes a new F43092 for the provided Data Access Layer and Table ID
        /// </summary>
        /// <param name="dbConnections">Database Access to the Data and System Tables</param>
        /// <param name="tableID">ID appended to the front of the table name to allow for multiple instances of the same table to exist in the UserDS</param>
        public F43092(DAL dbConnections, string tableID) : base(dbConnections, tableID)
        {
        }

        #endregion

        #region ShowRoutingSteps

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BranchPlant">Branch</param>
        /// <param name="Document">Document Number</param>
        /// <param name="DocumentType">Document Type</param>
        /// <param name="Environment">Enterprise 1 Environment</param>
        /// <param name="LineNumber">Line Number</param>
        /// <returns>True if records found.</returns>
        public bool ShowRoutingSteps(string BranchPlant, string Document, string DocumentType, string Environment, double LineNumber)
        {
            ResetAll();
            string owner = _DataAccess.Data.TableOwner;
            string ctlowner = "";
            TableOwnerOverride.ResetAll();
            TableOwnerOverride.TableName = "F0005";
            TableOwnerOverride.EnvironmentID = Environment;
            bool bFnd = TableOwnerOverride.Fetch();
            if (bFnd)
            {
                ctlowner = TableOwnerOverride.OwnerName.ToString();
            }
            else
            {
                ctlowner = owner;
            }

            string sSQL = "SELECT DISTINCT(PXOPRC), DRDL01 FROM ";
            sSQL = sSQL + owner + ".F43092 t1, ";
            sSQL = sSQL + ctlowner + ".F0005 t2 WHERE ";
            sSQL = sSQL + "PXMCU = '" + BranchPlant + "' AND PXDOCO = " + Document + " and PXDCTO = '" + DocumentType + "' ";
            sSQL = sSQL + " and PXOPRC = ltrim(DRKY) and DRRT = 'OC' ";
            sSQL = sSQL + " and PXLNID = " + LineNumber.ToString();
            sSQL = sSQL + " ORDER BY PXOPRC";
            SQL = sSQL;
            return (Fetch());
        }

        #endregion
    }
}