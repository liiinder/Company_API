# API Specification for Company_API v1

## General Information
- **Title:** Company_API v1
- **Version:** 1.0.0
- **Base URLs:**
  - `https://localhost:7227`
  - `http://localhost:5012`

## Endpoints

### Category
#### `GET /api/category`
**Description:** Retrieve all categories.

**Response:**
- `200 OK`

#### `POST /api/category`
**Description:** Create a new category.

**Request Body:**
- Content-Type: `application/json`
- Schema: `Category`

**Responses:**
- `201 Created`
- `400 BadRequest`

#### `PUT /api/category/{id}`
**Description:** Update an existing category.

**Path Parameter:**
- `id` (string, required)

**Request Body:**
- Schema: `Category`

**Responses:**
- `200 OK`
- `400 BadRequest`
- `404 NotFound`

#### `DELETE /api/category/{id}`
**Description:** Delete a category.

**Path Parameter:**
- `id` (string, required)

**Response:**
- `204 NoContent`

------------------------------------------------

### Customer
#### `GET /api/customer`
**Description:** Retrieve all customers.

**Response:**
- `200 OK`

#### `POST /api/customer`
**Description:** Create a new customer.

**Request Body:**
- Schema: `Customer`

**Responses:**
- `201 Created`
- `400 BadRequest`

#### `GET /api/customer/email/{email}`
**Description:** Retrieve customer by email.

**Path Parameter:**
- `email` (string, required)

**Responses:**
- `200 OK`
- `400 BadRequest`
- `404 NotFound`

#### `GET /api/customer/{id}`
**Description:** Retrieve a customer by ID.

**Path Parameter:**
- `id` (string, required)

**Responses:**
- `200 OK`
- `400 BadRequest`
- `404 NotFound`

#### `PUT /api/customer/{id}`
**Description:** Update an existing customer.

**Path Parameter:**
- `id` (string, required)

**Request Body:**
- Schema: `Customer`

**Responses:**
- `200 OK`
- `400 BadRequest`
- `404 NotFound`

#### `DELETE /api/customer/{id}`
**Description:** Delete a customer.

**Path Parameter:**
- `id` (string, required)

**Response:**
- `204 NoContent`
- `404 NotFound`

------------------------------------------------

### Order
#### `GET /api/order`
**Description:** Retrieve all orders.

**Responses:**
- `200 OK`
- `204 NoContent`
- `500 InternalServerError`

#### `POST /api/order`
**Description:** Create a new order.

**Request Body:**
- Schema: `Order`

**Responses:**
- `201 Created`
- `400 BadRequest`
- `404 NotFound`

#### `GET /api/order/customer/{id}`
**Description:** Retrieve orders by customer ID.

**Path Parameter:**
- `id` (string, required)

**Query Parameter:**
- `customerId` (string, optional)

**Responses:**
- `200 OK`
- `404 NotFound`

#### `GET /api/order/{id}`
**Description:** Retrieve an order by ID.

**Path Parameter:**
- `id` (string, required)

**Responses:**
- `200 OK`
- `404 NotFound`

#### `PUT /api/order/{id}`
**Description:** Update an existing order.

**Path Parameter:**
- `id` (string, required)

**Request Body:**
- Schema: `Order`

**Responses:**
- `200 OK`
- `400 BadRequest`
- `404 NotFound`

#### `DELETE /api/order/{id}`
**Description:** Delete an order.

**Path Parameter:**
- `id` (string, required)

**Response:**
- `204 NoContent`

------------------------------------------------

### Product
#### `GET /api/product`
**Description:** Retrieve all products.

**Response:**
- `200 OK`

#### `POST /api/product`
**Description:** Create a new product.

**Request Body:**
- Schema: `Product`

**Responses:**
- `201 Created`
- `400 BadRequest`

#### `GET /api/product/{id}`
**Description:** Retrieve a product by ID.

**Path Parameter:**
- `id` (string, required)

**Responses:**
- `200 OK`
- `404 NotFound`

#### `PUT /api/product/{id}`
**Description:** Update an existing product.

**Path Parameter:**
- `id` (string, required)

**Request Body:**
- Schema: `Product`

**Responses:**
- `200 OK`
- `400 BadRequest`
- `404 NotFound`

#### `DELETE /api/product/{id}`
**Description:** Delete a product.

**Path Parameter:**
- `id` (string, required)

**Responses:**
- `204 NoContent`
- `404 NotFound`

#### `GET /api/product/name/{name}`
**Description:** Retrieve a product by name.

**Path Parameter:**
- `name` (string, required)

**Responses:**
- `200 OK`
- `404 NotFound`

## Schemas
### Category
- `id` (string, nullable)
- `name` (string, required)

### Customer
- `id` (string, nullable)
- `firstName` (string, required)
- `lastName` (string, required)
- `email` (string, required)
- `phoneNumber` (string, nullable)
- `country` (string, nullable)
- `city` (string, nullable)
- `street` (string, nullable)
- `postalCode` (string, nullable)

### Order
- `id` (string, nullable)
- `customerId` (string, required)
- `orderDetails` (array of `OrderDetail`, nullable)

### OrderDetail
- `product` (Product, required)
- `quantity` (integer, required)

### Product
- `id` (string, nullable)
- `name` (string, required)
- `description` (string, required)
- `price` (double, required)
- `status` (boolean, required)
- `category` (Category, nullable)

## Tags
- `Category`
- `Customer`
- `Order`
- `Product`

