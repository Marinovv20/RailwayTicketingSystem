# Validation, Verification, and Testing Plan

## 1.0 General Information
* **System:** Railway Ticketing Portal (Bulgaria)
* **Version:** 1.0
* **Scope:** Validation of Pricing Logic (White-box), Verification of Schedule Integrity (Black-box), and User Modification Flows.

## 2.0 Test Evaluation

### Requirement Traceability
See [RequirementTraceabilityMatrix.csv](docs/RTM.csv) for full mapping.

### Exit Criteria
* **100%** Pass rate on High Priority Unit Tests (Pricing & Schedule).
* **0** Critical severity defects in the Defect Report.
* **>90%** Code Coverage on Business Logic layers.

### 2.1 Code Coverage Results
The following coverage metrics were captured using **Coverlet** during the final build. The architecture separates UI from Logic, allowing high coverage on the core services.

| Module | Line | Branch | Method | Status |
| :--- | :--- | :--- | :--- | :--- |
| **RailwayTicketing.Services** | **94.5%** | **91.2%** | **100%** | ✅ PASS |
| **RailwayTicketing.Core** | 100% | N/A | 100% | ✅ PASS |
| *RailwayTicketing.App (UI)* | *33.6%* | *41.1%* | *82.1%* | *Excluded* |
| **Total (Weighted)** | **76.0%** | **66.1%** | **94.0%** | |

> **Note:** The UI layer (Console) is excluded from strict coverage targets as per industry standard for Console Applications.

---

## 3.0 Test Description

### 3.1 Unit Testing (White-Box)
* **Target:** `PricingService.cs` and `ScheduleService.cs`
* **Technique:** Condition Coverage (CC) & Path Testing.
* **Objective:** Verify complex nested Boolean logic for discounts.
    * `IF (Time < 09:30 OR Time > 16:00) THEN Price = Full`
    * `IF (Age >= 60 AND Card == Over60s) THEN Discount = 34%`
* **Tools:** NUnit, Coverlet.

### 3.2 Functional Testing (Black-Box)
* **Target:** `ScheduleService.cs` (Route Integrity)
* **Technique:** Equivalence Partitioning (Valid Routes vs Invalid Inputs).
* **Data Set (Bulgarian Routes):**
    * **Peak Test:** Route #1 (Sofia -> Plovdiv) @ **07:30** -> Expect **Full Base Price**.
    * **Saver Test:** Route #1 (Sofia -> Plovdiv) @ **12:00** -> Expect **5% Discount**.
    * **Route Lookup:** Route #2 (Burgas -> Sofia) -> Verify correct distance (380km) loads automatically.

### 3.3 Integration Testing
* **Target:** `UserService` + `PricingService` Integration.
* **Scenario:** Verify "Modify Reservation" workflow updates the price dynamically.
    1.  **Create Booking:** Sofia -> Plovdiv @ 07:30 (Peak Price: $75.00).
    2.  **Save:** Confirm reservation exists in History.
    3.  **Modify:** User changes time to **12:00** (Saver).
    4.  **Verify:** System calls `PricingService` again.
    5.  **Result:** Price updates to **$71.25** (Saver rate) automatically.
