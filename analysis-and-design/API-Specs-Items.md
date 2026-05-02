# API Specification: Item Catalog Inquiry

This API allows the omnichannel frontend to query the SAP Business One Item Master Data. It supports filtering to provide customers with real-time information regarding pricing, technical specifications, and stock availability.

## 📋 Functional Context
When a user searches for a product via WhatsApp, the middleware executes a GET request. The response aggregates data from the Item Master (OITM), Inventory levels (OITW), and the associated image web repository.

---

## 📥 Request Structure (Input)

**Endpoint:** `GET /api/ConsultaItems/GetItems`  
**Parameter:** `detalle` (String) - Filter keyword for Item Code or Description.

**Example URL:**
`http://api-server:8082/api/ConsultaItems/GetItems?detalle=filter`

---

## 📤 Response Structure (Output)

### Response Field Definitions

| Field | Type | Description |
| :--- | :--- | :--- |
| `ItemCode` | String | Unique SAP Item Identifier. |
| `ItemDesc` | String | Full technical description and specifications. |
| `physical_quantity` | Decimal | Total stock in warehouse. |
| `usable_quantity` | Decimal | Committed stock subtracted (Available to promise). |
| `ItemPrice` | Decimal | Unit price before taxes. |
| `ItemTax` | Decimal | Applied tax percentage (e.g., 15%). |
| `Images` | Array | List of public URLs for product visualization in WhatsApp. |

---

### Response Example (Success)
```json
{
    "App": "ConsultaItems",
    "EndPoint": "/api/ConsultaItems/GetItems",
    "Empresa": "ERP_PROD",
    "ExecutionId": "50e0ee6d-8207-4095-b308-6e5006abe880",
    "Success": true,
    "ErrCodigo": 0,
    "ErrMensaje": "",
    "FechaRespuesta": "2026-04-20T15:18:32.11Z",
    "tabla": [
        {
            "ItemCode": "0.47MF-50V",
            "ItemDescShop": "FILTRO 105N",
            "ItemDesc_Meta": "FILTRO 105N",
            "ItemDesc": "FILTRO 105NEspecificaciones: - Capacitancia: 0.47 microfaradios - Voltaje máximo: 50V",
            "ItemWeight": 0.001,
            "ItemHeight": 0.0,
            "ItemWidth": 0.0,
            "ItemLength": 0.0,
            "physical_quantity": 800.0,
            "usable_quantity": 800.0,
            "ItemPrice": 0.043478,
            "ItemTax": 15.0,
            "ItemDiscount": 0.0,
            "ItemBrand": "UNIVERSAL",
            "Images": [
                {
                    "path": "[https://velascostore.com/img/p/1/1.jpg](https://velascostore.com/img/p/1/1.jpg)",
                    "position": 1,
                    "cover": 1
                }
            ]
        }
    ]
}
