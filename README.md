# Investment Tracker. Allvue Technical Assessment

## **Overview**

The **Investment Tracker** is an ASP.NET Core web application that allows users to manage their investments. The application tracks investment lots, provides the ability to sell shares using the FIFO (First In, First Out) accounting method, and calculates key metrics such as profits, cost basis per share, and remaining shares after a sale.

---

## **Features**

- **Investment Management**:
  - Add new investment lots with details such as shares, price per share, and purchase date.
  - View a summary of all investment lots in a clean and user-friendly table.

- **Sell Shares**:
  - Sell shares using the **FIFO** method.
  - Calculate:
    1. Remaining number of shares after the sale.
    2. Cost basis per share of the sold shares.
    3. Cost basis per share of the remaining shares.
    4. Total profit or loss of the sale.

- **Interactive Dashboard**:
  - Get detailed insights, after selling shares.

- **Clean Architecture**:
  - Separation of concerns with controllers, models, and database interactions.

---

## **Technologies Used**

- **Framework**: ASP.NET Core
- **Database**: Entity Framework Core with MySQL (or InMemory for testing)
- **Front-End**: Razor Views
- **Unit Testing**: xUnit with Moq
- **Containerization**: Docker and Docker Compose

---

## **Installation**

### Prerequisites
- [.NET 6+ SDK](https://dotnet.microsoft.com/download)
- [MySQL 8.0+](https://www.mysql.com/downloads/)
- [Docker](https://www.docker.com/)
- A text editor or IDE (e.g., [Visual Studio](https://visualstudio.microsoft.com/))

### Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/ealvarezl/InvestmentAllvue.git
   cd InvestmentAllvue\InvestmentTracker
   ```

2. To use Docker:
   - Build and start the containers:
     ```bash
     docker-compose up --build
     ```
   - Access the app at `http://localhost:5000`.

---

## **Usage**

### Adding Investment Lots
1. Navigate to the "Add New Investment Lot" section.
2. Enter:
   - Number of shares.
   - Price per share.
   - Purchase date.
3. Submit the form to add the new lot.

### Selling Shares
1. Go to the "Sell Shares" page.
2. Enter:
   - Number of shares to sell.
   - Price per share.
3. View the calculated metrics:
   - Remaining shares.
   - Cost basis per share (sold and remaining).
   - Total profit or loss.

---

## **Development**

### Running Tests
1. Navigate to the test project directory:
   ```bash
   cd InvestmentTracker.Tests
   ```
2. Run all tests:
   ```bash
   dotnet test
   ```

### Project Structure
- **Controllers**: Business logic for managing investments and user interactions.
- **Models**: Entity definitions for `InvestmentLot` and related calculations.
- **Views**: Razor templates for displaying data to the user.
- **Data**: Database context and seeding logic.

---

## **Contact**

For questions or support, feel free to contact:
- **Name**: Eglis Alvarez
- **Email**: eglisal@gmail.com
--- 

