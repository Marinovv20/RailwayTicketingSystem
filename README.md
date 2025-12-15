# 🚄 BG-Rail: Ticketing System Architecture

![Coverage](https://img.shields.io/badge/coverage-94%25-blue) ![Framework](https://img.shields.io/badge/.NET-8.0-purple)

**A clean-architecture simulation of the Bulgarian railway booking ecosystem.**
This repository serves as the final submission for the **Software Testing & Quality Assurance** module. It demonstrates rigorous adherence to STLC (Software Testing Life Cycle) phases, from Requirements Engineering to Automated Regression Testing.

---

## 📑 Table of Contents
1. [System Architecture](#-system-architecture)
2. [Key Features](#-key-features)
3. [Project Artifacts](#-project-artifacts)
4. [Installation & Usage](#-installation--usage)
5. [Testing Strategy](#-testing-strategy)

---

## 🏗 System Architecture
The solution follows a strict **Interface-Based Dependency Injection** pattern to ensure testability.
* **Core.Models:** Domain entities (Trips, Passengers, Routes).
* **Core.Interfaces:** Contracts for Dependency Inversion (`IScheduleService`, `IPricingService`).
* **Services:** Pure business logic (Pricing rules, Schedule lookups).
* **App:** The Console UI presentation layer.

---

## ✨ Key Features
| Module | Functionality |
| :--- | :--- |
| **Scheduling** | Real-time lookup of Bulgarian routes (e.g., **Sofia ↔ Plovdiv**) with specific departure slots. |
| **Dynamic Pricing** | Algorithms handling **Peak/Saver** times, **Railcards** (Senior/Family), and **Mileage** rates. |
| **User Profile** | Persistent profile state allowing auto-fill of **Age/Card** details during booking. |
| **Reservation Mgmt** | Full lifecycle support: **Book ➡ Modify (Date/Time) ➡ Cancel** with history tracking. |

---

## 📂 Project Artifacts
All documentation required for grading is located in the `/docs` directory.

### Phase 1: Requirements
* [Software Requirements Specification (SRS)](Requirements.md) - *Criteria #1*
* [Gherkin Feature Files](Requirements.md) - *Criteria #2*
* [Requirements Checklist](Checklist.md) - *Criteria #3*

### Phase 2: Test Engineering
* [Master Test Plan](TestPlan.md) - *Criteria #15*
* [Traceability Matrix (RTM)](docs/RTM.xlsx) - *Criteria #10*
* [Defect Report](DefectReport.md) - *Criteria #4*

---

## 🚀 Installation & Usage

**Prerequisites:** .NET 8.0 SDK

1.  **Clone the repository**
    ```bash
    git clone [https://github.com/YourUsername/RailwayTicketing.git](https://github.com/YourUsername/RailwayTicketing.git)
    ```
2.  **Run the Application**
    ```bash
    cd RailwayTicketing.App
    dotnet run
    ```
3.  **Operation Mode**
    * Follow the on-screen prompts to set up your User Profile.
    * Select **Option 1** to view the live Bulgarian train schedule.
    * Select **Option 3** to test the new "Modify Reservation" feature.

---

## 🧪 Testing Strategy
**Framework:** NUnit 3
**Methodology:** White-box Testing (Statement & Branch Coverage)

To execute the test suite:
```bash
dotnet test RailwayTicketing.Tests
