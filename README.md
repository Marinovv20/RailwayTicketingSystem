Railway Ticketing Portal 🚂

Software Testing Coursework - Final Submission

> **Note:** Code Coverage is targeted at **>90% for Domain and Services layers**. The UI layer (`RailwayTicketing.App`) is excluded from strict coverage metrics as per industry standard practices for Console Applications.

📖 Project Overview

This project is a C# Console Application designed to simulate a railway ticketing system. It demonstrates **Clean Architecture** principles and a rigorous **Software Testing Lifecycle (STLC)**.

The system supports:
* **Search and Booking** (One-Way & Round Trip).
* **Dynamic Pricing** (Rush Hour, Saver Discounts, Distance-based).
* **Passenger Concessions** (Senior & Family Railcards, Child discounts).
* **Profile Management** (Create Profile, View History, Cancel Reservations).
* **Promotional Logic** (Coupon redemption).

📂 Deliverables & Documentation

Below are links to the required artifacts for the scoring criteria, located in the `docs/` folder.

📝 Requirements Engineering (Lab 01 & 02)
* [Requirements Specification (SRS)](Requirements.md) - *Criteria #1*
* [Gherkin Feature Specifications](Requirements.md#gherkin-feature-specifications) - *Criteria #2*
* [Requirements Checklist](Checklist.md) - *Criteria #3*

🧪 Test Design (Lab 03 & 06)
* [Control Flow Graph (Pricing Logic)](RailFarePricing_CFD.png) - *Criteria #5*
* [Requirement Traceability Matrix (RTM)](TestPlan.md#requirement-traceability-matrix-rtm) - *Criteria #10*
* [Master Test Plan](TestPlan.md) - *Criteria #15*
* [Defect Report](DefectReport.md) - *Criteria #4*

⚙️ Implementation (Lab 05)
* **Source Code** - *Criteria #9*
* **Unit Test Project** - *Criteria #6, 7, 8*

🚀 How to Run

1.  **Prerequisites:** .NET 8 SDK (or .NET 9).
2.  Navigate to the App folder:
    ```bash
    cd RailwayTicketing.App
    ```
3.  Run the application:
    ```bash
    dotnet run
    ```
4.  **Usage:** Follow the console prompts. You must create a profile first.

🧪 How to Test

This project uses **NUnit 3** to verify Statement, Decision, and Condition coverage.

**Run Unit Tests:**
```bash
dotnet test RailwayTicketing.Tests
