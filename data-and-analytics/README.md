# Data Integrity & Financial Analytics: WhatsApp to SAP B1 Ecosystem

For this project, my **Data Analyst & Systems Architect** focus was on ensuring that every transaction processed through Nuvei was correctly reflected in SAP Business One, maintaining a single source of truth.

### 📊 Areas of Focus
- **Transaction Reconciliation:** SQL scripts to compare Nuvei transaction logs against SAP B1 'Incoming Payments' to identify discrepancies.
- **Conversion Analytics:** Analysis of the "Drop-off" rate at the payment stage to optimize the bot's flow.
- **Audit Logs:** Designed a centralized logging table in SQL Server to track every integration attempt, payload, and response status for troubleshooting.

---

### 🛠 Technical Architecture & API Specifications
To support these analytics, I designed a robust middleware with a standardized communication protocol. Each endpoint was built to ensure data integrity and real-time synchronization.

*Detailed documentation of the integration layers:*

*   **[Security & Identity (JWT)](./analysis-and-design/API-Specs-Auth.md):** Secure authentication flow for external requests.
*   **[Catalog & Metadata](./analysis-and-design/API-Specs-Items.md):** Real-time mapping of SAP Item Master Data.
*   **[Inventory ATP (Available-to-Promise)](./analysis-and-design/API-Specs-StockAvailability.md):** Bulk stock validation logic.
*   **[CRM Integration](./analysis-and-design/API-Specs-BusinessPartner.md):** Customer identification and synchronization.
*   **[Transactional Core (Order to Invoice)](./analysis-and-design/API-Specs-SalesOrder.md):** The end-to-end sales orchestration flow.

---

### 📂 Included Resources
*   **Generic SQL templates** for transaction auditing and data validation.
*   **API Payloads** for cross-system troubleshooting and mapping.
