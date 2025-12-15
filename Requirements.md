# Software Requirements Specification (SRS)
**Project:** Railway Ticketing System (Bulgaria)
**Date:** December 15, 2025

## 1. Functional Requirements (FR)

### Module 1: Search & Schedule
| ID | Requirement Name | Description |
| :--- | :--- | :--- |
| **FR-01** | **Route Selection** | The system shall allow users to select a journey from a predefined schedule of Bulgarian routes (e.g., Sofia -> Plovdiv), which determines the distance automatically. |
| **FR-02** | **Ticket Types** | The system shall support "One-Way" and "Round Trip" tickets. Round Trip tickets shall double the calculated base fare. |
| **FR-03** | **Coupons** | The system shall accept promotional codes ("SUMMER", "STUDENT") to apply flat or percentage-based discounts. |

### Module 2: Pricing Engine
| ID | Requirement Name | Description |
| :--- | :--- | :--- |
| **FR-04** | **Base Pricing** | The system shall calculate the base price using a rate of **$0.50 per km**. |
| **FR-05** | **Saver Time** | A 5% discount shall apply to journeys departing between 09:30 and 16:00, or after 19:30. |
| **FR-06** | **Rush Hour** | Full fare applies to journeys departing before 09:30 or between 16:00 and 19:30. |
| **FR-07** | **Senior Concession** | Passengers with an 'Over 60s' railcard shall receive a **34% discount** on the total fare. |
| **FR-08** | **Family Concession** | Children (<16) traveling with a 'Family' railcard holder shall receive a **50% discount**. |
| **FR-09** | **Child Standard** | Children (<16) traveling without a railcard shall receive a **10% discount**. |

### Module 3: Reservation & Profile Management
| ID | Requirement Name | Description |
| :--- | :--- | :--- |
| **FR-10** | **Profile Configuration** | The system shall allow users to create a profile storing Name, Address, **Age, and Railcard Type** to enable auto-filling during booking. |
| **FR-11** | **History** | The system shall store a history of reservations associated with the user profile. |
| **FR-12** | **Cancellation** | Users shall be able to cancel an active reservation using its unique ID. |
| **FR-13** | **Modification** | Users shall be able to modify the date and time of an active reservation, triggering an automatic price recalculation. |

---

## 2. Non-Functional Requirements (NFR)

| ID | Category | Description |
| :--- | :--- | :--- |
| **NFR-01** | **Architecture** | The system must utilize **Interface-Based Dependency Injection** to decouple Services (Pricing, User, Schedule) from the Console UI. |
| **NFR-02** | **Testability** | Business logic must be isolated in a Class Library (`.Services`) to allow for 100% unit test coverage. |

---

## 3. Gherkin Feature Specifications (Acceptance Criteria)

### Module 1: Train Search & Schedule
**Feature:** Train Search and Selection
* **Scenario:** Viewing the train timetable (Bulgarian Routes)
  * **Given** the system contains a schedule of Bulgarian routes
  * **When** I select "Search Trains" from the main menu
  * **Then** I should see a list including "Sofia -> Plovdiv" and "Burgas -> Sofia"
  * **And** the departure times should be displayed in chronological order

### Module 2: Pricing Engine
**Feature:** Dynamic Ticket Pricing
* **Scenario:** Rush hour travel charges full fare
  * **Given** a train departs at "07:30" (Rush Hour)
  * **And** the ticket type is "One-Way"
  * **When** a user checks the price
  * **Then** the price should match the "Base Price" exactly (no discounts).

* **Scenario:** Senior citizens receive Railcard discounts
  * **Given** a passenger is "65" years old
  * **And** the passenger holds an "Over 60s" rail card
  * **When** the ticket price is calculated
  * **Then** a **34% discount** is applied to the current fare.

### Module 3: Profile System
**Feature:** Profile Integration (Booking Flow)
* **Scenario:** Loading a profile during booking
  * **Given** I have selected a train and ticket type
  * **When** the system asks "Load passenger details?"
  * **And** I select the "Grandma" profile
  * **Then** the "Age" and "Railcard" fields are auto-filled
  * **And** the price is immediately calculated using the "Over 60s" discount.
