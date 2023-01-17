<!-- GETTING STARTED -->
## Getting Started

This file contains short instructions how you can set up the project locally.
To get a local copy up and running, follow the simple steps.

### Prerequisites

First you need to ensure that you have .NET Core 6.0 or later version of the development
platform. On top of that you will need MS SQL Server database, and it will be recommended
that MS SQL Server compatible database browser is installed.

Initiate database TicketStore and two tables, Devices and Tickets, into database. 

1. Create database
   ```sh
   CREATE DATABASE TicketStore
   ```
2. Initiate tables. The repository contains two files, devices.csv and tickets.csv,
   which contain necessary data.
  
Attention: This document doesn't contain instructions for installation of the development environment,
or instructions how to application can be run inside Docker container environment. 



### Installation

Follow the following steps to install TicketStore application to your own environment.

1. Clone the repo
   ```sh
   git clone git@github.com:MattiTKHarsu/TicketBookRepo.git
   ```
2. Check and change correct address for database connection, this parameter depends on your own environment.
   ```sh
   nano TicketBookRepo/TicketBook/appsettings.json
   ```
3. Change to run-directory
   ```sh
   cd TicketBookRepo/TicketBook
   ```
4. Start the Application. (Of course you can run the Application inside development environment as well.)
   ```sh
   dotnet run watch
   ```

<!-- USAGE EXAMPLES -->
## Usage

The API contains following interfaces, which you can use by Postman tool:

1. GET all tickets, https://localhost:7105/api/ticket

2. GET one ticket, https://localhost:7105/api/ticket/"id"

3. POST add one ticket, https://localhost:7105/api/ticket

4. PUT update one ticket,  https://localhost:7105/api/ticket

5. DEL remove ticket,  https://localhost:7105/api/ticket/"id"

6. GET device related tickets, https://localhost:7105/api/ticket/device/"id"

In every case the response contains either all tickets or filtered list [2], [6].
If the application cannot connect to the database the response contains error code and message.


Attention. If you want to use "Code First" principle for creating database, remove all Migration related files and create new files
following commands. (Current files are dedicated only for original developer.)

1. Create the initial Migration file inside project directory.
   ```sh
   dotnet ef migrations add CreateInitial
   ```
2. Update the database. This command creates the database and defined tables.
   ```sh
   dotnet ef database update
   ```
3. If you want to use Wizard to initialize values to Device and Ticket tables, then created tables have to be removed first.
   Following commands have to be run inside Azure Data Studio.
   ```sh
   DROP TABLE Devices;
   DROP TABLE Tickets;
   ```
