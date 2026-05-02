# API Specification: Authentication & Security (JWT)

Security is a core component of this middleware. To ensure data privacy and authorized access between the WhatsApp platform and SAP Business One, I implemented a **JWT (JSON Web Token)** authentication flow.

## 📋 Functional Context
All subsequent API calls to the middleware require a valid Bearer Token in the HTTP Authorization header. The token has a defined expiration time and is signed using the HS256 algorithm to prevent unauthorized access or tampering.

---

## 🔐 Authentication Flow (Login)

**Endpoint:** `POST /api/auth/login`  
**Description:** Exchanges valid system credentials for a temporary access token.

### 📥 Credentials (Input)
The request requires pre-configured service credentials.
```json
{
  "username": "admin_service",
  "password": "PROTECTED_PASSWORD"
}
```
### 📤 Response Structure (Output)

The API provides a standardized security response containing the Bearer Token required for all subsequent transactions.

| Field | Type | Description |
| :--- | :--- | :--- |
| `token` | String | A signed JSON Web Token (JWT) using the HS256 algorithm. |

---

#### Response Example (Success)
```json
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3Nzc3MDAzMDksImlzcyI6IklNUl_BUEkiLCJhdWQiOiJKZWxvdSJ9.XXXX-XXXX-XXXX"
}
