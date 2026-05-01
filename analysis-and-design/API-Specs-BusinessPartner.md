# API Specification: Business Partner Creation
**Integration Point:** WhatsApp Bot to SAP Business One Middleware

To ensure data integrity between the WhatsApp front-end and SAP B1, I designed a custom API contract. This mapping handles specific Ecuadorian tax requirements (UDFs) and multi-address structures.

### 📥 Request Payload (Input)
The middleware receives a JSON object with the customer's personal information and tax classification.

| Field | Type | Description |
| :--- | :--- | :--- |
| `FederalTaxId` | String | Tax ID (RUC/Cédula). |
| `CardName` | String | Legal Name or Business Name. |
| `U_TIPO_RUC` | String (1) | Custom field for Taxpayer type. |
| `Direcciones` | Array | List of billing and shipping addresses. |

**Example:**
```json
{
    "FederalTaxId": "0999999999",
    "CardName": "Business Partner Trial",
    "Phone1": "0999999999",
    "E_Mail": "example@domain.com",
    "U_TIPO_RUC": "N",
    "U_TIPO_ID": "C",
    "Direcciones": [
        {
            "AddressName": "PRINCIPAL",
            "Street": "MAIN ST 123",
            "City": "GUAYAQUIL",
            "Country": "EC"
        }
    ]
}

## 📤 Response Structure (Output)
The API provides a standardized response. The inclusion of metadata like `ExecutionId` and `ErrCodigo` ensures full traceability for auditing and error handling.

### Response Field Definitions

| Field | Type | Description |
| :--- | :--- | :--- |
| `App` | String | Originating middleware application name. |
| `EndPoint` | String | Internal route hit by the request. |
| `ExecutionId` | GUID | Unique transaction identifier for system logs and audit trails. |
| `Success` | Boolean | Final result of the operation. `true` if SAP B1 created the record. |
| `ErrCodigo` | Integer | System error code (0 = Success). |
| `ErrMensaje` | String | Detailed error description from SAP DI API / Service Layer. |
| `tabla` | Array | Contains the generated SAP object details (CardCode, Addresses, etc.). |
| `AddressType` | Enum | Internal SAP mapping: `bo_ShipTo` (Shipping) and `bo_BillTo` (Billing). |

**Response Example (Success):**
```json
{
    "App": "API-Middleware-SAP",
    "EndPoint": "/api/BPartner/PostBPartner",
    "Empresa": "PROD_ERP",
    "ExecutionId": "8067a6cd-ab71-4863-8003-9874cd0cf62e",
    "Success": true,
    "ErrCodigo": 0,
    "ErrMensaje": "",
    "FechaRespuesta": "2025-10-03T04:57:54.46Z",
    "tabla": [
        {
            "CardCode": "C0987654321",
            "CardName": "Standardized Customer Example",
            "CardType": "cCustomer",
            "EmailAddress": "customer.test@example.com",
            "Phone": "0987654321",
            "Valid": "Y",
            "Address": [
                {
                    "AddressName": "ENVIO",
                    "RowNum": 0,
                    "AddressType": "bo_ShipTo",
                    "Street": "Delivery Route 456",
                    "City": "GUAYAQUIL",
                    "Country": "EC"
                },
                {
                    "AddressName": "PRINCIPAL",
                    "RowNum": 0,
                    "AddressType": "bo_BillTo",
                    "Street": "Main Street Ave 123",
                    "City": "GUAYAQUIL",
                    "Country": "EC"
                }
            ]
        }
    ]
}
