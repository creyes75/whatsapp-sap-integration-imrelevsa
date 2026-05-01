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
