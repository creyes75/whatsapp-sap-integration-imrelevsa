# API Specification: Bulk Stock Availability Inquiry

This API enables the frontend to perform high-efficiency inventory checks for multiple items in a single request. It is a critical component for the "Available-to-Promise" (ATP) logic within the omnichannel sales flow.

## 📋 Functional Context
To optimize performance and reduce network latency, this endpoint avoids multiple individual calls. It allows the WhatsApp bot or web portal to validate a customer's entire shopping cart availability before proceeding to the checkout or payment stage in SAP Business One.

---

## 📥 Request Structure (Input)

**Endpoint:** `POST /api/ConsultaItems/GetStockAvailability`  
**Description:** Validates real-time inventory levels for a list of specific Item Codes.

### Request Body
The request must contain a JSON object with an array of strings representing the SAP `ItemCodes`.
```json
{
  "ItemCodes": [
    "VANTEC-20A",
    "SIGMA156B",
    "GK7",
    "CT-30"
  ]
}
```
### 📤 Response Structure (Output)

The API returns a collection of stock levels for the requested items, allowing the frontend to perform bulk availability validation.

| Field | Type | Description |
| :--- | :--- | :--- |
| `App` | String | Originating middleware application (ConsultaItems). |
| `ExecutionId` | GUID | Unique transaction identifier for audit logs. |
| `Success` | Boolean | Result of the inventory lookup operation. |
| `tabla` | Array | List of objects containing the specific stock per item. |
| `ItemCode` | String | Unique SAP Item Identifier. |
| `Stock` | Decimal | Real-time available quantity in SAP Business One. |

---
#### Response Example (Success)
```json
{
    "App": "ConsultaItems",
    "EndPoint": "/api/ConsultaItems/GetStockAvailability",
    "Empresa": "IMR",
    "ExecutionId": "c379210d-6755-4b82-914f-e185a857a2c7",
    "Success": true,
    "ErrCodigo": 0,
    "ErrMensaje": "",
    "FechaRespuesta": "2026-04-20T02:18:21.18Z",
    "tabla": [
        {
            "ItemCode": "GK7",
            "Stock": 118.0
        },
        {
            "ItemCode": "VANTEC-20A",
            "Stock": 41.0
        },
        {
            "ItemCode": "CT-30",
            "Stock": 0.0
        }
    ]
}
