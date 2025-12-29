# TrainTracker

## Project Overview

**TrainTracker** is a web application for real-time train monitoring.  
It consists of a **Next.js frontend**, **ASP.NET Core Web API backend**, and **MariaDB database**.

- The frontend fetches train and incident data from the backend API.
- Train data is loaded from a JSON file, simulating a WebSocket stream.
- Users can view trains, track delays, and log incidents.

---

## Objective

- Display a list of currently running trains.
- Highlight trains with significant delays.
- Allow users to log incidents with details.
- Store and retrieve incident history from the database via the API.

---

## Provided Data

The sample JSON file includes the following key fields:

- `data[0].name` – Train name
- `data[0].returnValue.train` – Train number
- `data[0].returnValue.arrivingTime` – Delay time (in minutes)
- `data[0].returnValue.nextStopObj` – Next stop information

---

## Technical Stack

- **Frontend:** Next.js (React)
- **Backend:** ASP.NET Core Web API (C#)
- **Database:** MariaDB
- **Real-time simulation:** Periodically reloading JSON data in backend

---

## Frontend Features

- Display train list with:
  - Train name
  - Train number
  - Delay time (if available)
  - Next station name
- Highlight trains delayed more than 10 minutes
- Show a timestamp of the last update
- Visual indicator for trains with logged incidents
- Responsive and user-friendly UI

---

## Backend Features

- API endpoints:
  - `GET /api/train` – fetch current trains
  - `GET /api/incident` – fetch incidents
  - `POST /api/incident` – add incident for a train
- Data validation using **FluentValidation**
- Automatic database migrations on startup
- Background services to simulate train updates and delays

---

## Incident Logging

Users can log incidents for delayed trains, including:

- Username
- Reason for delay
- Additional comments

All incident data is stored in MariaDB and displayed in the frontend with train-specific indicators.

---

## Functional Requirements

- Highlight trains with delay > 10 minutes
- Show last update timestamp
- Log incidents via API
- Visualize trains with incidents
- Fetch and display incident history per train

---

## Dashboard/Desktop
![Desktop](/screenshots/1.jpg)
![Desktop](/screenshots/2.jpg)

## Mobile
![Mobile](/screenshots/3.jpg)
![Mobile](/screenshots/4.jpg)


