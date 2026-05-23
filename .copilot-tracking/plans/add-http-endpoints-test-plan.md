# HVE Implementation Plan: Interactive REST Endpoint Client Testing
**Task ID**: HVE-TASK-002  
**Project**: customer-management  
**Status**: APPROVED & EXECUTED  

---

## 1. Context & Objectives
We are adding an interactive, high-quality `.http` request testing file to the `customer-management` project. This file will allow developers and AI models to test all implemented REST API endpoints in `Program.cs` directly inside the IDE using REST Client tools.

Endpoints to cover:
1. GET `/api/customers` (Retrieve all customers)
2. GET `/api/customers/{id}` (Retrieve specific customer by ID, including negative path test)
3. POST `/api/customers` (Create a valid customer, validation check for missing names, validation check for missing email)
4. PUT `/api/customers/{id}` (Update existing customer details, validation check, not found check)
5. POST `/api/customers/{id}/notes` (Add a valid interaction note, validation check, not found check)
6. DELETE `/api/customers/{id}` (Delete existing customer, not found check)

---

## 2. Confidence Markers & Risks
* **Validated**: The API uses standard Minimal API endpoint routes that map to a `CustomerRepository` singleton. The web container is running and exposes port `8000`.
* **Assumed**: Standard port configuration and host `http://localhost:8000` is active.
* **Risks**: None. Creating a `.http` file does not alter code execution.

---

## 3. Proposed Changes

### [NEW] customer-management.http
Create `customer-management.http` at the root of the project with fully annotated REST test suites, negative/positive branches, and validation scenarios.

---

## 4. Verification Plan

### Manual Verification
- Execute `make test` inside the dev container to confirm unit/integration tests are stable.
- Verify that the API responds to local HTTP calls.

---

*🤖 Processed under the guidelines of HVE-Core RPI Workflow.*
