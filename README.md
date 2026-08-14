# Property Maintenance Triage

A lightweight .NET console application for processing tenant property maintenance requests.

The application reads maintenance tickets from JSON, assigns a priority using simple keyword-based rules, assigns an appropriate contractor, and displays the triage results in a formatted console table.

## Technology

* C# / .NET 8
* Console application / CLI
* `System.Text.Json` for JSON deserialization
* NUnit 3 for unit testing
* Moq for mocking dependencies in service tests

## Features

* Reads maintenance tickets from the provided sample JSON file.
* Allows JSON ticket data to be pasted directly into the CLI.
* Categorises tickets as `Urgent`, `Medium`, or `Low`.
* Assigns tickets to a `Plumber`, `Electrician`, or `GeneralHandyman` based on keyword matching.
* Routes tickets to `PropertyManager` when an appropriate contractor cannot be determined.
* Displays Ticket ID, Address, Assigned Contractor, and Priority in a formatted console table.
* Handles invalid manually entered JSON and allows the user to try again.
* Includes unit tests covering prioritisation, contractor assignment, JSON parsing, edge cases, and triage orchestration.

## Project Structure

```text
PropertyMaintenanceTriage/
├── Cli/
│   └── ConsoleUi.cs
├── Data/
│   └── maintenance-tickets.json
├── Enums/
│   ├── Priority.cs
│   └── ContractorType.cs
├── Helpers/
│   └── KeywordMatcher.cs
├── Models/
│   ├── MaintenanceTicket.cs
│   └── TriageResult.cs
├── Services/
│   ├── ITicketLoader.cs
│   ├── TicketLoader.cs
│   ├── IPriorityService.cs
│   ├── PriorityService.cs
│   ├── IContractorService.cs
│   ├── ContractorService.cs
│   ├── ITriageService.cs
│   └── TriageService.cs
└── Program.cs

PropertyMaintenanceTriage.Tests/
└── Services/
    ├── PriorityServiceTests.cs
    ├── ContractorServiceTests.cs
    ├── TriageServiceTests.cs
    └── TicketLoaderTests.cs
```

## Getting Started

### Prerequisites

Install the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

### Build

From the solution directory:

```bash
dotnet build
```

### Run

Run the console application with:

```bash
dotnet run --project PropertyMaintenanceTriage
```

The CLI presents two input options:

```text
1. Use sample JSON file
2. Paste JSON manually
```

### Option 1 — Sample JSON File

Selecting option `1` loads the supplied `Data/maintenance-tickets.json` file.

The JSON file is configured to be copied to the application's output directory during the build so that it can be located relative to `AppContext.BaseDirectory`.

### Option 2 — Manual JSON Input

Selecting option `2` allows a JSON array of maintenance tickets to be pasted directly into the console.

After pasting the JSON, type:

```text
END
```

on a new line to process the input.

For example:

```json
[
  {
    "ticket_id": "TKT-201",
    "address": "Flat 8, 24 King Street",
    "issue_description": "There is a large water leak under the kitchen sink.",
    "reported_date": "2026-08-13"
  },
  {
    "ticket_id": "TKT-202",
    "address": "15 Victoria Road",
    "issue_description": "The hallway lightbulb has stopped working and needs replacing.",
    "reported_date": "2026-08-13"
  },
  {
    "ticket_id": "TKT-203",
    "address": "Apartment 3, Riverside Court",
    "issue_description": "The front door lock is sticking and is difficult to open.",
    "reported_date": "2026-08-12"
  },
  {
    "ticket_id": "TKT-204",
    "address": "72 Meadow Lane",
    "issue_description": "There is a strong smell of gas coming from the kitchen.",
    "reported_date": "2026-08-12"
  },
  {
    "ticket_id": "TKT-205",
    "address": "Flat 11, Oak House",
    "issue_description": "There is a strange buzzing noise coming from inside the bedroom wall.",
    "reported_date": "2026-08-11"
  },
  {
    "ticket_id": "TKT-206",
    "address": "9 Church Street",
    "issue_description": "The bathroom socket is sparking when something is plugged into it.",
    "reported_date": "2026-08-11"
  }
]
```

If the manually entered JSON is malformed, the application displays an error and allows the user to try again.

## Triage Rules

### Priority

Priority is determined using simple case-insensitive keyword matching, as requested in the exercise.

* `Urgent` is assigned when an urgent keyword is recognised, such as `leak`, `flood`, `gas`, `burst pipe`, `sparking`, or `smoke`.
* `Low` is assigned to recognised minor maintenance issues, such as `lightbulb`, `loose handle`, `paint`, or `cosmetic`.
* `Medium` is the fallback when the issue does not match an `Urgent` or `Low` rule.

Urgent rules are evaluated before Low rules. If a description contains keywords belonging to both groups, the ticket is treated as `Urgent`.

Missing, empty, or whitespace-only issue descriptions also fall back to `Medium`.

### Contractor Assignment

Contractors are selected using case-insensitive keyword matching.

* Plumbing-related issues are assigned to a `Plumber`.
* Electrical issues are assigned to an `Electrician`.
* General maintenance issues are assigned to a `GeneralHandyman`.
* If no contractor rule matches, the ticket is assigned to `PropertyManager` for manual review.

The `PropertyManager` fallback avoids automatically assigning an unknown issue to an inappropriate trade contractor.

## Application Flow

The application follows this process:

```text
JSON file or manual input
        ↓
TicketLoader
        ↓
List<MaintenanceTicket>
        ↓
TriageService
   ├── PriorityService
   └── ContractorService
        ↓
List<TriageResult>
        ↓
ConsoleUi
        ↓
Formatted console output
```

`Program.cs` acts as the application entry point and coordinates the main components.

`ConsoleUi` is responsible for user interaction and console output.

`TicketLoader` handles JSON deserialization.

`PriorityService` and `ContractorService` contain the keyword-based business rules.

`TriageService` coordinates those services and creates the final `TriageResult`.

## Assumptions and Trade-offs

This solution intentionally uses straightforward keyword matching rather than natural-language processing or an external AI service.

`Medium` is used as the fallback priority when no configured priority keyword matches. This avoids automatically treating an unrecognised issue as low priority.

When no contractor can be confidently identified from the configured rules, the ticket is routed to `PropertyManager` for manual review rather than assigning a potentially inappropriate trade.

Contractor rules are evaluated in a fixed order. If an issue contains keywords from multiple contractor categories, the first matching category is selected. A production system may require more sophisticated handling for issues involving multiple trades.

Keyword matching uses simple case-insensitive substring matching. This keeps the implementation straightforward, but it does not interpret the context of the description and may produce false matches in more complex real-world cases.

The application currently assumes that the overall ticket structure is broadly valid. Missing or unexpected issue descriptions are handled through fallback rules, but a production system should include more comprehensive validation for required fields such as ticket ID, address, and reported date.

The exercise does not specify how JSON input should be provided to the CLI, so I chose to support both the supplied sample file and manually pasted JSON. This allows the sample data to be run immediately while also making it easy to test the application with different ticket data.

The application is split into small components for loading data, determining priority, assigning contractors, triaging tickets, and handling CLI interaction. This introduces slightly more structure than a minimal console application requires, but keeps responsibilities separated and makes the business rules easier to test and extend.

The application processes tickets in memory and does not persist triage results to a database.

## Testing

The solution includes NUnit unit tests covering the main application rules.

Run all tests from the solution directory with:

```bash
dotnet test
```

## AI Usage

ChatGPT was used as a development assistant during this exercise.

It was used to:

* Discuss project structure and naming.
* Review implementation decisions.
* Suggest test cases and review unit-test structure.
* Review CLI structure and error-handling approaches.
* Assist with documenting the solution and its assumptions.
