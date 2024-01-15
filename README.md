# Guide
## Restaurant Management System
*Human-Computer Interaction 2023*

## Introduction
The Restaurant Management System is an intuitive application created to simplify restaurant operations and improve hospitality business efficiency. This system focuses on enhancing hospitality operations, including managing items, processing orders and procurements, and coordinating tables and staff. All functionalities are designed to make restaurant management easier for both administrators and staff.

## System Description
The Restaurant Management System provides a user-friendly interface for effective management of restaurant operations. It encompasses features such as user management, table coordination, item management, procurement management, and order processing. The system is tailored to facilitate the administration and staff in running the restaurant smoothly.

## Main System Functionalities

### Login to the System
Upon launching the application, a login window appears. Successful login requires entering the correct username and password provided by the system administrator. After a successful login, a new window opens with different options based on the type of user account – administrator or regular user. In case of incorrect credentials, the system displays a login failure notification.

### Administrator
Upon login, the administrator is presented with options on the left side, including user management, item management, table management, settings, and logout.

#### User Management
The administrator can:
- Add, edit, or delete user accounts.
- Filter users by name or surname.

To add a user:
1. Click on "Add User."
2. Complete the required fields.
3. Click "Add" (blocked until all fields are filled).
4. A success notification appears.

To edit or delete a user:
- Click on the respective user's edit (pencil) or delete (trash can) icons.

#### Items Management
The administrator can:
- View, add, edit, or delete items.
- Filter items by category.

To add or edit an item:
1. Click on "Add Item" or the edit icon.
2. Complete or modify item details.
3. Click "Add" or "Update" for success notification.

To delete an item:
- Click on the item's delete icon.

#### Tables Management
The administrator can:
- View, add, edit, or delete tables.
- Filter tables by seating capacity.

To add or edit a table:
1. Click on "Add Table" or the edit icon.
2. Complete or modify table details.
3. Click "Add" or "Update" for success notification.

To delete a table:
- Click on the table's delete icon.

#### Settings
The administrator can:
- Change the system theme or language.

To change the theme or language:
1. Click on the selected theme or language.
2. The selected option loads automatically.
3. Click "Save" to remember the choice for future logins.

### Regular User
Upon login, a regular user is directed to their order history by default. They can create new orders, view existing ones, delete orders, and filter orders by creation date. Users can also view and filter other users' orders but cannot delete them.

#### Orders
To create a new order:
1. Click on "Add Order."
2. Select the item, associated table, and quantity.
3. Click "Add" to add the item to the order.
4. The order appears in the user's order list.

To view order details:
- Click on the magnifying glass icon next to an order.

To delete an order:
- Click on the trash can icon next to an order.

#### Procurements
Regular users can:
- View their procurements.
- Create new procurements.
- Filter procurements by creation date.
- View details of individual procurements.

Procurements are created similarly to orders, with the addition of specifying the purchase price for each item.

#### Settings (Regular User)
Regular users have the option to customize settings such as themes and languages in the same manner as the administrator.


## How to Run
1. Clone or download this repository
2. Open the solution file (hci-restaurant.sln) in Visual Studio or your preferred WPF development environment.
3. Build and run the project to start the game.


## User Interface 
![1](https://github.com/elenadj7/hci-restaurant/assets/92872835/7ec6ed9e-2016-4030-9144-c0becbd9dfb3)
![2](https://github.com/elenadj7/hci-restaurant/assets/92872835/2f39313a-eb4f-41da-9910-4fdfa4acc164)
![3](https://github.com/elenadj7/hci-restaurant/assets/92872835/e033fc40-c8bd-41e0-bb01-8a6bfcd1d66c)
![4](https://github.com/elenadj7/hci-restaurant/assets/92872835/f1843797-61f9-4b38-b2e2-2a098a97fadb)
![5](https://github.com/elenadj7/hci-restaurant/assets/92872835/8c2cb545-b672-483d-94e5-0d1025b67da5)
![6](https://github.com/elenadj7/hci-restaurant/assets/92872835/276b66ac-791a-4e1c-9212-3dec85ef68de)




