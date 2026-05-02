### 🛠️ Technical Highlights & Payment Integration

As the **Lead Developer**, I architected the secure middleware connecting the omnichannel front-end with the SAP financial backend.

*   **Nuvei API Integration:** Orchestrated secure API calls for payment link generation and real-time status polling.
*   **Asynchronous Webhook Handling:** Built a robust handler to process **Nuvei Webhooks**, ensuring SAP B1 records are updated only upon verified transaction confirmation.
*   **SAP Service Layer Automation:** Automated the creation of Sales Orders and Down Payment Invoices (A/R) triggered by successful payment approval.
*   **Security & Tokenization:** Managed sensitive transaction data using industry best practices for API authentication, secure headers, and JWT-based session management.

---

### 🛡️ Intellectual Property & Sanitization Note

> [!IMPORTANT]
> **Proprietary Rights & NDA Compliance:**
> To respect the Intellectual Property (IP) and strict Non-Disclosure Agreements (NDA) of the stakeholders involved, the source code provided in this repository is a **technical abstraction**. 
>
> **What has been sanitized/omitted:**
> *   **Core Proprietary Logic:** Internal tax calculation algorithms and SAP Service Layer connectivity logic.
> *   **Security Credentials:** Nuvei Private Keys and API Secrets have been replaced with placeholders (e.g., `{NUVEI_API_KEY}`) to maintain confidentiality while demonstrating architectural flow.
> *   **Infrastructure:** Internal company server configurations and production environment variables.
