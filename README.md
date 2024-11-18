# **Investment Tracker**  
**Allvue Technical Assessment**

## **Overview**  

The **Investment Tracker** is a web application built with ASP.NET Core, designed to help users efficiently manage their investments. It enables tracking of investment lots, selling shares using the **FIFO (First In, First Out)** accounting method, and provides critical insights such as profits, cost basis per share, and remaining shares after each sale.

---

## **Key Features**  

### **Investment Management**
- Add new investment lots with details like:
  - Number of shares.
  - Price per share.
  - Purchase date.
- View a summary of all lots in a clean, tabular format.

### **Selling Shares**
- Sell shares with the **FIFO** method.
- Calculate key metrics:
  1. **Remaining Shares**: Total shares left after the sale.
  2. **Cost Basis**: Per share of sold and remaining shares.
  3. **Profit or Loss**: Total financial gain or loss from the sale.

### **Dashboard Insights**
- Interactive summaries displayed post-sale, showing metrics like remaining shares, cost basis, and profit/loss.

### **Clean Architecture**
- Well-structured separation of concerns with controllers, models, Razor views, and database interactions.

---

## **Technologies Used**

- **Framework**: ASP.NET Core
- **Database**: MySQL with Entity Framework Core
- **Frontend**: Razor Views
- **Containerization**: Docker and Docker Compose

---

## **Getting Started**  

### **Prerequisites**  
Ensure you have the following installed:
- [.NET 6+ SDK](https://dotnet.microsoft.com/download)
- [MySQL 8.0+](https://www.mysql.com/downloads/)
- [Docker](https://www.docker.com/)
- An IDE or text editor (e.g., [Visual Studio](https://visualstudio.microsoft.com/))

---

### **Installation**  

#### **Using Docker**  
1. Clone the repository:
   ```bash
   git clone https://github.com/ealvarezl/InvestmentAllvue.git
   cd InvestmentAllvue/InvestmentTracker
   ```

2. Build and start the application:
   ```bash
   docker-compose up --build -d
   ```

3. Open your browser and access the application at:
   ```
   http://localhost:5000
   ```


---

## **Usage**  

### **Adding Investment Lots**
1. Navigate to the "Add New Investment Lot" page.
2. Fill out the form:
   - Enter the **number of shares**, **price per share**, and **purchase date**.
   - Submit the form to add the lot to the database.

### **Selling Shares**
1. Go to the "Sell Shares" page.
2. Enter:
   - **Number of shares to sell**.
   - **Price per share** at which the sale is happening.
3. Submit the form to view key metrics:
   - Remaining shares.
   - Cost basis for sold and remaining shares.
   - Total profit or loss.

---

## **Development**  

### **Project Structure**
- **Controllers**: Manages investment logic and handles user input.
- **Models**: Defines the `InvestmentLot` entity and calculations for metrics.
- **Views**: Razor templates for rendering the user interface.
- **Data**: Manages database context and seeding logic.

---

## **Known Limitations**

- Only supports the **FIFO** accounting method for selling shares.
- Shares purchased after **March 31, 2024**, are marked with a warning and may not be eligible for sales as per the application’s logic.

---

## **Contact**  

For questions, feedback, or support, please reach out to:  
- **Name**: Eglis Alvarez  
- **Email**: eglisal@gmail.com  

--- 
