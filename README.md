# Product Management Dashboard

## Introduction
> This is a simple CRUD product management dashboard project. Products can be added, retrieved, updated and deleted via OpenAPI. On the frontend, product data table and a bar chart for product quantity are displayed. The application is dockerized for easier development and testing. 

## Tech Stack:
- Frontend: React
- Backend: .Net Core
- Database: Sqlite3

## Setup

1. Download repository
```
git clone git@github.com:511234/ProductMgtDashboard.git
```

2. Run the following commands to build and run docker container:
```
cd ProductMgtDashboard
docker-compose up -d
```

3. Access OpenAPI documentation to test different endpoints at:  
http://localhost:8001

4. Access Frontend to view Product data table and Product Quantity bar chart at:  
http://localhost:5173

## Endpoints:
`GET /api/Product`
> Get a list of products

`POST /api/Product`
> Create a new product

`PUT /api/Product`
> Update a product

`GET /api/Product/{productCode}`
> Get a product by its product code

`DELETE /api/Product/{productCode}`
> Delete a product by its product code

`GET /api/Product/quantity/all`
> Get a list of product categories and each of their total quantity