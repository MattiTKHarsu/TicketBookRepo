<!-- GETTING STARTED -->
## Getting Started

This file contains short instructions how you can set up the project locally.
To get a local copy up and running follow the simple steps.

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
  
This document doesn't contain instructions for installation of the development environment,
or instructions how to application can be run inside Docker container environment. 



### Installation

_Below is an example of how you can instruct your audience on installing and setting up your app. This template doesn't rely on any external dependencies or services._

Follow the following steps to install TicketStore application to your own environment.

1. Clone the repo
   ```sh
   git clone git@github.com:MattiTKHarsu/TicketBookRepo.git
   ```
2. Check and change correct addres for database correction, this depends on your own environment.
   ```sh
   nano TicketBookRepo/TicketBook/appsettings.json
   ```
3. Change run-directory
   ```sh
   cd TicketBookRepo/TicketBook
   ```
4. Start the Application
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

In every case the response contains either all tickets or filtered list [2], [6]
If the application cannot connect to the database the response contains error code and message.





Use this space to show useful examples of how a project can be used. Additional screenshots, code examples and demos work well in this space. You may also link to more resources.



# TicketBook
