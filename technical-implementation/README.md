# Technical Implementation & Payment Integration

As the **Lead Developer**, I was responsible for the secure middleware architecture that connects the omnichannel front-end with the financial backend.

### 💻 Implementation Highlights
- **Nuvei API Integration:** Implemented secure API calls to Nuvei for payment link generation and status polling.
- **Tokenization & Security:** Managed sensitive transaction data using industry-best practices for API authentication and secure headers.
- **Asynchronous Processing:** Built a robust handler to process Nuvei Webhooks, ensuring SAP B1 is updated only after a successful transaction confirmation.
- **SAP Service Layer:** Automated the creation of Sales Orders and Down Payments (Down Payment Invoices) upon payment approval.

### 🔐 Sanitization Note
Proprietary logic for internal tax calculation and Nuvei Private Keys have been replaced with placeholders (`{NUVEI_API_KEY}`, etc.) to maintain confidentiality while demonstrating architectural flow.
