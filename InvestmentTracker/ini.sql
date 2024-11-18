CREATE DATABASE IF NOT EXISTS InvestmentDB;

USE InvestmentDB;

CREATE TABLE IF NOT EXISTS InvestmentLots (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Shares INT NOT NULL,
    PricePerShare DECIMAL(10, 2) NOT NULL,
    PurchaseDate DATE NOT NULL
);

INSERT INTO InvestmentLots (Shares, PricePerShare, PurchaseDate)
VALUES 
    (100, 20.00, '2024-01-01'),
    (150, 30.00, '2024-02-01'),
    (120, 10.00, '2024-03-01');
