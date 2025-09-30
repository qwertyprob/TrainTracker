# TrainTracker

## Project Overview
TrainTracker is a web application built with **ASP.NET MVC** and **MariaDB** that simulates real-time train monitoring.  
The application loads train data from a provided JSON file, mimicking a WebSocket data stream.  
Users can view trains, track delays, and log incidents.

---

## Objective
- Display a list of currently running trains.
- Highlight trains with significant delays.
- Allow users to log incidents with details.
- Store and retrieve incident history from a database.

---

## Provided Data
The sample JSON file includes the following key fields:
- `data[0].name` – Train name
- `data[0].returnValue.train` – Train number
- `data[0].returnValue.arrivingTime` – Delay time (in minutes)
- `data[0].returnValue.nextStopObj` – Next stop information

---

## Technical Requirements
- **ASP.NET MVC (C#)** for backend and frontend
- **MariaDB** for storing trains and incidents
- Simulated real-time updates by periodically reloading JSON data
- Display the following for each train:
    - Train name
    - Train number
    - Delay time (if available)
    - Next station name

---

## Functional Requirements
- Highlight trains with a delay greater than 10 minutes
- Show a timestamp of the last update
- Allow users to log incidents for delayed trains, including:
    - Username
    - Reason for delay
    - Additional comments
- Save incident data to the database
- Show a visual indicator for trains with logged incidents
- Provide access to incident history per train

---

## Evaluation Criteria
- Code clarity and maintainability
- Proper use of ASP.NET MVC and MariaDB
- Functional completeness
- Usability and responsiveness of the UI
- Correct handling of simulated real-time updates

---
