# Defect Report - Static Testing & Code Review

**Review Date:** December 15, 2025

### Identified Defects & Resolutions

| ID | Severity | Component | Description | Status |
| :--- | :--- | :--- | :--- | :--- |
| **DEF-01** | Critical | Program.cs / Schedule | **Missing Schedule Data:** The system required manual entry of "Distance" and "Destination," violating Module 1 (Bulgarian Routes). Users could not select "Sofia -> Plovdiv". | **FIXED** (Implemented `ScheduleService`) |
| **DEF-02** | High | UserService.cs | **Data Loss:** `CreateProfile` only accepted Name/Address. Age and Railcard details were discarded, causing the Pricing Engine (Module 2) to fail on Senior discounts. | **FIXED** (Updated method signature) |
| **DEF-03** | Medium | UserService.cs | **Missing Requirement:** The system supported "Cancel" but lacked the "Modify Reservation" feature required by Module 3 specifications. | **FIXED** (Added `ModifyReservation` logic) |
| **DEF-04** | Low | Program.cs | **UI Workflow:** The booking flow did not auto-fill passenger details from the logged-in user profile, forcing repetitive data entry. | **FIXED** (Auto-fill implemented) |
