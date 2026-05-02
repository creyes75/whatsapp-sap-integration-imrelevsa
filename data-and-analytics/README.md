# Data Integrity & Financial Analytics: WhatsApp to SAP B1 Ecosystem

For this project, my focus as a **Senior Business Systems Analyst & Data Architect** was on ensuring that every transaction processed through the middleware was correctly reflected in SAP Business One, maintaining a single source of truth and 100% financial integrity.

### 📊 Areas of Focus
- **Transaction Reconciliation:** SQL scripts to compare payment gateway logs (Nuvei) against SAP B1 'Incoming Payments' to identify discrepancies.
- **Conversion Analytics:** Analysis of the "Drop-off" rate at the checkout stage to optimize the bot's flow and user experience.
- **Audit Logs:** Designed a centralized logging architecture in SQL Server to track every integration attempt, payload, and response status.

---

### 🛡️ System Traceability (Database Design)
To ensure accountability and rapid troubleshooting, I implemented a centralized log table (`ApiExecutionLog`). This allows for deep-dive auditing by capturing the full state of every API request and response in real-time.

**Key Schema Features:**
| Field | Type | Purpose |
| :--- | :--- | :--- |
| `ExecutionId` | uniqueidentifier | Unique GUID to trace a single transaction across multiple systems. |
| `RequestPayload` | nvarchar(max) | Stores the complete input JSON for historical replay or auditing. |
| `ResponsePayload`| nvarchar(max) | Stores the ERP response to verify data consistency. |
| `ErrorCode` | nvarchar(25) | Facilitates rapid filtering of failed transactions in production. |

> **[View SQL DDL: `create-audit-log-table.sql`](./data-analytics/create-audit-log-table.sql)**

---

### 🛠️ Technical Architecture & API Specifications
I designed a robust middleware with a standardized communication protocol to ensure data integrity and real-time synchronization between the mobile front-end and the ERP.

*   **[Security & Identity (JWT)](./analysis-and-design/API-Specs-Auth.md):** Secure authentication flow for external requests.
*   **[Catalog & Metadata](./analysis-and-design/API-Specs-Items.md):** Real-time mapping of SAP Item Master Data.
*   **[Inventory ATP (Available-to-Promise)](./analysis-and-design/API-Specs-StockAvailability.md):** Bulk stock validation logic.
*   **[CRM Integration](./analysis-and-design/API-Specs-BusinessPartner.md):** Customer identification and synchronization.
*   **[Transactional Core (Order to Invoice)](./analysis-and-design/API-Specs-SalesOrder.md):** End-to-end sales orchestration flow.

---

### 📂 Analytics & Audit Resources
Detailed scripts and analytical frameworks regarding the data lifecycle:

*   **[SQL: Transaction Reconciliation](./data-analytics/transaction-reconciliation.sql):** Auditing SAP vs. Payment Gateway.
*   **[Analysis: Conversion Funnel](./data-analytics/sales-funnel-analysis.md):** Business metrics and bot performance.
*   **[Dashboard: Integration Health](./data-analytics/integration-health-dashboard.md):** Error classification and mitigation strategies.

---

### 💡 Why this matters
This architecture doesn't just move data; it provides **Business Intelligence**. By logging every `ExecutionId`, we reduced the Mean Time to Repair (MTTR) from hours to minutes and ensured that the Finance department has a clear audit trail for every dollar earned via digital channels.
