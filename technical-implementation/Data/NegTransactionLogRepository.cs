/* 
=============================================================================
REPOSITORY LAYER: Transaction Log & Audit Trail
PROJECT: SAP Business One Omnichannel Integration
AUTHOR: Carlos Reyes
DESCRIPTION: 
Handles the persistence of cross-system identifiers. This repository 
links the external Payment Gateway (Nuvei) voucher with internal 
SAP Business One Document Entries for real-time auditability.
=============================================================================
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApi.Data.Repositories
{
    public class NegTransactionLogRepository
    {
        private readonly string _connectionString;

        public NegTransactionLogRepository()
        {
            // The connection string is managed via Web.config for security and environment scalability
            _connectionString = ConfigurationManager.ConnectionStrings["IMR_APICtrl_DB"].ConnectionString;
        }

        /// <summary>
        /// Inserts a detailed trace of the multi-document transaction.
        /// This ensures 100% visibility between the API request and the ERP documents.
        /// </summary>
        public void InsertTransactionLog(
            Guid executionId, 
            string status, 
            string voucherNum, 
            int orderEntry, 
            int orderNum, 
            int invoiceEntry, 
            int invoiceNum, 
            int paymentEntry, 
            int paymentNum, 
            string cardCode, 
            decimal totalAmount)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertApiTransactionTrace", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Link external traceability
                        cmd.Parameters.AddWithValue("@ExecutionId", executionId);
                        cmd.Parameters.AddWithValue("@VoucherNum", voucherNum);
                        
                        // SAP Document Mapping
                        cmd.Parameters.AddWithValue("@OrderDocEntry", orderEntry);
                        cmd.Parameters.AddWithValue("@OrderDocNum", orderNum);
                        cmd.Parameters.AddWithValue("@InvoiceDocEntry", invoiceEntry);
                        cmd.Parameters.AddWithValue("@InvoiceDocNum", invoiceNum);
                        cmd.Parameters.AddWithValue("@PaymentDocEntry", paymentEntry);
                        cmd.Parameters.AddWithValue("@PaymentDocNum", paymentNum);
                        
                        // Business Data
                        cmd.Parameters.AddWithValue("@CardCode", cardCode);
                        cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@LoggedAt", DateTime.Now);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // In a Senior implementation, we log to Event Viewer or a secondary file 
                // to avoid losing the trace if the primary DB is down.
                System.Diagnostics.Trace.WriteLine($"Critical Logging Error: {ex.Message}");
            }
        }
    }
}
