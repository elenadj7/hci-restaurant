CALL AddUser('johnDoe', 'johnDoe', 'John', 'Doe', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('aliceSmith', 'aliceSmith', 'Alice', 'Smith', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('bobJohnson', 'bobJohnson', 'Bob', 'Johnson', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('emilyWilliams', 'emilyWilliams', 'Emily', 'Williams', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('michaelBrown', 'michaelBrown', 'Michael', 'Brown', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('emmaJones', 'emmaJones', 'Emma', 'Jones', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('williamDavis', 'williamDavis', 'William', 'Davis', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('oliviaMiller', 'oliviaMiller', 'Olivia', 'Miller', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('jamesWilson', 'jamesWilson', 'James', 'Wilson', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('sophiaMoore', 'sophiaMoore', 'Sophia', 'Moore', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('benjaminTaylor', 'benjaminTaylor', 'Benjamin', 'Taylor', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('avaAnderson', 'avaAnderson', 'Ava', 'Anderson', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('masonThomas', 'masonThomas', 'Mason', 'Thomas', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('isabellaJackson', 'isabellaJackson', 'Isabella', 'Jackson', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('liamWhite', 'liamWhite', 'Liam', 'White', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('charlotteHarris', 'charlotteHarris', 'Charlotte', 'Harris', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('ethanMartin', 'ethanMartin', 'Ethan', 'Martin', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('ameliaLee', 'ameliaLee', 'Amelia', 'Lee', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('noahClark', 'noahClark', 'Noah', 'Clark', FLOOR(RAND()*(4000-900+1))+900);
CALL AddUser('oliverLewis', 'oliverLewis', 'Oliver', 'Lewis', FLOOR(RAND()*(4000-900+1))+900);
insert into User(username, password, name, surname, salary, role) values("admin", "admin", "Admin", "Admin", 8888, 1);
insert into Settings(user_username, theme, language) values("admin", "Dark", "English");


insert into Category(id, name) values(1, "Article");
insert into Category(id, name) values(2, "Dish");
insert into Category(id, name) values(3, "Drink");

CALL AddItem('Eggs', ROUND(RAND() * 10, 2), 'Fresh eggs', 1, 1);
CALL AddItem('Sugar', ROUND(RAND() * 10, 2), 'Belgian sugar', 1, 1);
CALL AddItem('Flour', ROUND(RAND() * 10, 2), 'Organic wheat flour', 1, 1);
CALL AddItem('Tomatoes', ROUND(RAND() * 10, 2), 'Fresh tomatoes', 1, 1);

CALL AddItem('Margherita Pizza', 12.99, 'Traditional Italian pizza', 1, 2);
CALL AddItem('Grilled Salmon', 17.50, 'Freshly grilled salmon', 1, 2);
CALL AddItem('Chicken Alfredo', 15.99, 'Creamy chicken pasta', 1, 2);
CALL AddItem('Caesar Salad', 9.99, 'Classic Caesar salad', 1, 2);

CALL AddItem('Coca-Cola', 2.50, 'Refreshing cola drink', 1, 3);
CALL AddItem('Iced Tea', 2.00, 'Chilled iced tea', 1, 3);
CALL AddItem('Orange Juice', 3.00, 'Freshly squeezed orange juice', 1, 3);
CALL AddItem('Lemonade', 2.50, 'Homemade lemonade', 1, 3);

CALL AddItem('Milk', ROUND(RAND() * 10, 2), 'Fresh whole milk', 1, 1);
CALL AddItem('Butter', ROUND(RAND() * 10, 2), 'Creamy unsalted butter', 1, 1);
CALL AddItem('Garlic', ROUND(RAND() * 10, 2), 'Fresh garlic cloves', 1, 1);
CALL AddItem('Basil', ROUND(RAND() * 10, 2), 'Fragrant basil leaves', 1, 1);

CALL AddItem('Spaghetti Bolognese', 14.50, 'Classic Italian pasta', 1, 2);
CALL AddItem('Hamburger', 10.99, 'Juicy beef patty with toppings', 1, 2);
CALL AddItem('Sushi Platter', 24.99, 'Assorted sushi selection', 1, 2);
CALL AddItem('Vegetable Stir Fry', 12.99, 'Fresh vegetables in soy sauce', 1, 2);

CALL AddItem('Coffee', 3.50, 'Freshly brewed coffee', 1, 3);
CALL AddItem('Red Wine', 18.00, 'Full-bodied red wine', 1, 3);
CALL AddItem('Mojito', 7.50, 'Refreshing minty cocktail', 1, 3);
CALL AddItem('Iced Latte', 4.00, 'Chilled espresso with milk', 1, 3);
