# API Specification: End-to-End Sales Process (Order & Invoice)

This API manages the complete sales transaction, orchestrating the creation of both the Sales Order and the resulting Invoice in SAP Business One. It integrates inventory, logistics data, and credit card payment confirmation.

## 📋 Functional Context
To provide a seamless experience, this endpoint handles the dual creation of documents. It captures shipping logistics (`ShippingInfo`) and financial reconciliation data (`PaymentCreditCards`) to ensure the transaction is fully processed and ready for delivery.

---

## 📥 Request Structure (Input)

**Endpoint:** `POST /api/Sales/PostSale`  

### Request Body
The payload includes document lines, shipping logic, and payment voucher details.
```json
{
  "CardCode": "C0916566797",
  "Comments": "Trial Purchase - Server Migration Test",
  "DocumentLines": [
    {
      "ItemCode": "AE-VA104-2",
      "Quantity": 1,
      "Price": 14.57,
      "DiscountPercent": 2.00
    }
  ],
  "ShippingInfo": {
    "IsHomeDelivery": "No",
    "City": "GYE",
    "Route": "AL GUASMO",
    "TrackingNumber": "001-00-00002656"
  },
  "PaymentCreditCards": [
    {
      "VoucherNum": "DF-1712817",
      "CreditSum": 16.76
    }
  ]
}
```
### 📤 Response Structure (Output)

The API response provides a comprehensive trace of the created objects in SAP, confirming the successful generation of both the Sales Order and the Invoice.

| Field | Type | Description |
| :--- | :--- | :--- |
| `ExecutionId` | GUID | Unique identifier for system logs and audit trails. |
| `Success` | Boolean | True if both SAP documents were created correctly. |
| `tableOrder` | Array | Contains details of the generated Sales Order (DocEntry, DocNum). |
| `tableInvoice` | Array | Contains details of the generated Invoice linked to the order. |
| `DocTotal` | Decimal | Final transaction amount (including taxes and discounts). |

---

#### Response Example (Success)
```json
{
    "App": "API-Sales",
    "EndPoint": "/api/Sales/PostSale",
    "Empresa": "IMR",
    "ExecutionId": "1d2807d0-5141-48b7-aac6-86ac26aec2f7",
    "Success": true,
    "ErrCodigo": 0,
    "ErrMensaje": "",
    "FechaRespuesta": "2026-04-21T04:15:19.39Z",
    "tableOrder": [
        {
            "DocEntry": "510418",
            "DocNum": "3218858",
            "DocType": "dDocument_Items",
            "DocDate": "2026-04-20T00:00:00Z",
            "CardCode": "C0916566797",
            "CardName": "CARLOS A REYES A",
            "DocTotal": 16.41,
            "Comments": "rial Purchase - Server Migration Test"
        }
    ],
    "tableInvoice": [
        {
            "DocEntry": "446880",
            "DocNum": "705818",
            "DocType": "dDocument_Items",
            "DocDate": "2026-04-20T00:00:00Z",
            "CardCode": "C0916566797",
            "CardName": "CARLOS A REYES A",
            "DocTotal": 16.41
        }
    ]
}
