# Medical Practice Management System

A cross-platform desktop and mobile application built with **.NET MAUI** and **C#** designed to streamline medical practice workflows including patient onboarding, physician directory management, and appointment scheduling.

---

## Features

### Patient Management
* Register new patients with demographic and contact details.
* View and search comprehensive patient records.

### Physician Management
* Maintain a directory of licensed healthcare providers and specialists.
* Manage physician profiles and specialties.

### Appointment Scheduling
* Schedule, view, and organize patient appointments with assigned physicians.
* Real-time list views of scheduled medical visits.

### Centralized Data Management
* Unified data controller (`DataManager.cs`) managing runtime state and persistence logic.

---

## Tech Stack

* **Framework:** .NET MAUI (.NET 8.0 / .NET 9.0)
* **Language:** C# 12 / XAML
* **Architecture:** Model-View / Page-Based Navigation (`AppShell`)
* **IDE:** Visual Studio 2022 (with *.NET Multi-platform App UI development* workload)
* **Supported Platforms:** Windows 10/11, macOS (Catalyst), Android, iOS

---

## Repository Structure

```text
medical-practice-management/
├── medical-practice-management.sln           # Visual Studio Solution File
└── medical-practice-management/             # Main Project Directory
    ├── Platforms/                            # Native OS implementations (Android, iOS, MacCatalyst, Windows)
    ├── Properties/                           # Launch settings & assembly properties
    ├── Resources/                            # Assets (App icons, Fonts, Images, Raw files, Styles)
    │
    ├── Models & Data/
    │   ├── Patient.cs                        # Patient data entity model
    │   ├── Physician.cs                      # Physician data entity model
    │   ├── Appointment.cs                    # Appointment data entity model
    │   └── DataManager.cs                    # Centralized data repository/controller
    │
    ├── Pages & Views/
    │   ├── MainPage.xaml / .cs               # Primary dashboard & navigation host
    │   ├── AddPatientPage.xaml / .cs         # Patient registration form
    │   ├── ViewPatientsPage.xaml / .cs       # Patient listing view
    │   ├── AddPhysicianPage.xaml / .cs       # Physician onboarding form
    │   ├── ViewPhysiciansPage.xaml / .cs     # Physician directory view
    │   ├── AddAppointmentPage.xaml / .cs     # Appointment booking form
    │   └── ViewAppointmentsPage.xaml / .cs   # Scheduled appointments list
    │
    ├── Core App Infrastructure/
    │   ├── App.xaml / .cs                    # Application root & styling resources
    │   ├── AppShell.xaml / .cs               # Navigation shell structure
    │   ├── MauiProgram.cs                    # Dependency injection & app bootstrapper
    │   └── GlobalXmlns.cs                    # Global namespace definitions
    │
    └── Configuration/
        └── medical-practice-management.csproj # Project file & package references
```

---

## Getting Started

### Prerequisites

* [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (v17.8+) with the **.NET Multi-platform App UI development** workload installed.
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or higher.

### Installation & Run

1. **Clone the Repository**
   ```bash
   git clone https://github.com/kjl23e/medical-practice-management.git
   cd medical-practice-management
   ```

2. **Open Solution**
   Double-click `medical-practice-management.sln` to open the project in Visual Studio.

3. **Restore Dependencies**
   Build the solution to automatically restore all required NuGet packages:
   * Right-click the Solution in **Solution Explorer** → **Rebuild Solution**.

4. **Select Debug Target & Launch**
   * Choose your desired target platform (e.g., `Windows Machine`, `Android Emulator`, or `iOS Local/Remote Device`) from the top toolbar menu.
   * Press `F5` or click **Start Debugging**.

---
