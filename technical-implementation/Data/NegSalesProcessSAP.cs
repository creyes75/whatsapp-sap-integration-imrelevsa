/* 
=============================================================================
CORE BUSINESS LOGIC: Sales Orchestration (Order -> Invoice -> Payment)
PROJECT: SAP Business One Omnichannel Integration
AUTHOR: Carlos Reyes
DESCRIPTION: 
This service orchestrates the complete sales lifecycle in SAP B1. 
It ensures atomic transactions: creating a Sales Order, followed by 
the A/R Invoice, and concluding with the Incoming Payment registration.
=============================================================================
*/

using System;
using System.Collections.Generic;
using WebApi.Models;
using WebApi.Data.Repositories;

namespace WebApi.Data
{
    public class NegSalesProcessSAP
    {
        /// <summary>
        /// Principal orchestrator for registering a sale in SAP.
        /// Sequential Flow: 1. Sales Order -> 2. A/R Invoice -> 3. Incoming Payment.
        /// </summary>
        public RespuestaAPI RegistrarSale_SAP(InputSalesAPI input, RespuestaAPI oRespuesta)
        {
            try
            {
                // ---------------------------------------------------------
                // 1. DATA MAPPING: Prepare SAP Order Object
                // ---------------------------------------------------------
                InputOrderAPI orden = MapToOrder(input);

                // ---------------------------------------------------------
                // 2. CREATE SALES ORDER
                // ---------------------------------------------------------
                NegOrderSAP onegOrder = new NegOrderSAP();
                RespuestaAPI respOrden = onegOrder.RegistrarOrder_SAP(orden, oRespuesta);

                if (respOrden.Success)
                {
                    int orderDocEntry = int.Parse(respOrden.tableOrder[0].DocEntry);
                    
                    // ---------------------------------------------------------
                    // 3. CREATE A/R INVOICE (Linked to the previous Order)
                    // ---------------------------------------------------------
                    NegInvoiceSAP oNegInvoice = new NegInvoiceSAP();
                    RespuestaAPI respFactura = oNegInvoice.RegistrarInvoice_SAP(orderDocEntry, orden, oRespuesta);

                    if (respFactura.Success)
                    {
                        int docEntryFactura = int.Parse(respFactura.tableInvoice[0].DocEntry);
                        decimal totalDocFactura = respFactura.tableInvoice[0].DocTotal;

                        // ---------------------------------------------------------
                        // 4. PROCESS INCOMING PAYMENT (Payment Orchestration)
                        // ---------------------------------------------------------
                        ProcessPayment(input, docEntryFactura, totalDocFactura, oRespuesta);

                        // ---------------------------------------------------------
                        // 5. TRANSACTIONAL TRACEABILITY (Audit Trail)
                        // ---------------------------------------------------------
                        LogFinalTransaction(input, oRespuesta);
                    }
                }

                return oRespuesta;
            }
            catch (Exception ex)
            {
                oRespuesta.Success = false;
                oRespuesta.ErrCodigo = -1000;
                oRespuesta.ErrMensaje = $"Critical Orchestration Error: {ex.Message}";
                return oRespuesta;
            }
        }

        private InputOrderAPI MapToOrder(InputSalesAPI input)
        {
            InputOrderAPI orden = new InputOrderAPI
            {
                CardCode = input.CardCode,
                Comments = input.Comments,
                DocumentLines = new List<DocumentLine>()
            };

            foreach (var item in input.DocumentLines)
            {
                orden.DocumentLines.Add(new DocumentLine
                {
                    ItemCode = item.ItemCode,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    DiscountPercent = item.DiscountPercent
                });
            }

            // Normalization of Shipping Flags
            string deliveryFlag = input.ShippingInfo.IsHomeDelivery.ToUpper();
            orden.U_EnvioDomicilio = (deliveryFlag == "YES" || deliveryFlag == "Y" || deliveryFlag == "SI" || deliveryFlag == "S") ? "SI" : "NO";
            
            orden.U_Ciudad = input.ShippingInfo.City;
            orden.U_Trayecto = input.ShippingInfo.Route;
            orden.U_NumGuiaGen = input.ShippingInfo.TrackingNumber;

            return orden;
        }

        private void ProcessPayment(InputSalesAPI input, int invoiceDocEntry, decimal total, RespuestaAPI oRespuesta)
        {
            // Logic to prepare InputPaymentAPI and call NegPaymentSAP.RegistrarPago_SAP
            // Note: In production, this links the Credit Card Voucher to the Invoice DocEntry.
            NegPaymentSAP oNegPayment = new NegPaymentSAP();
            // ... [Payment logic implementation]
        }

        private void LogFinalTransaction(InputSalesAPI input, RespuestaAPI oRespuesta)
        {
            // Capture generated DocEntrys for financial reconciliation
            int salesOrderDocEntry = int.Parse(oRespuesta.tableOrder?[0]?.DocEntry ?? "0");
            int salesOrderDocNum = int.Parse(oRespuesta.tableOrder?[0]?.DocNum ?? "0");
            int invoiceDocEntry = int.Parse(oRespuesta.tableInvoice?[0]?.DocEntry ?? "0");
            int invoiceDocNum = int.Parse(oRespuesta.tableInvoice?[0]?.DocNum ?? "0");

            NegTransactionLogRepository oNegTraLogRep = new NegTransactionLogRepository();
            oNegTraLogRep.InsertTransactionLog(
                oRespuesta.ExecutionId, 
                "0", 
                input.PaymentCreditCards[0].VoucherNum, 
                salesOrderDocEntry, 
                salesOrderDocNum, 
                invoiceDocEntry, 
                invoiceDocNum, 
                0, 0, // Payment placeholders
                input.CardCode, 
                input.PaymentCreditCards[0].CreditSum
            );
        }
    }
}
