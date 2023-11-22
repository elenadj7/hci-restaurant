insert into User(username, password, name, surname, salary, role) values("elenadj7", "elena123", "Elena", "Djurdjevic", 1234, 0);
insert into User(username, password, name, surname, salary, role) values("marko123", "marko123", "Marko", "Markovic", 3456, 0);
insert into User(username, password, name, surname, salary, role) values("nikola456", "nikola456", "Nikola", "Nikolic", 1254, 0);
insert into User(username, password, name, surname, salary, role) values("djordje888", "djordje888", "Djordje", "Djordjevic", 2124, 0);
insert into User(username, password, name, surname, salary, role) values("milica321", "milica321", "Milica", "Milic", 1242, 0);

insert into User(username, password, name, surname, salary, role) values("admin", "admin123", "Admin", "Admin", 8888, 1);

insert into Category(id, name) values(1, "Artikl");
insert into Category(id, name) values(2, "Jelo");
insert into Category(id, name) values(3, "Pice");

insert into Item(id, name, price, description, is_available, category_id) values(1, "Secer", 1.8, null, 1, 1);
insert into Item(id, name, price, description, is_available, category_id) values(2, "Brasno", 30, null, 1, 1);
insert into Item(id, name, price, description, is_available, category_id) values(3, "Jaja", 12.2, null, 1, 1);
insert into Item(id, name, price, description, is_available, category_id) values(4, "So", 3.5, null, 0, 1);

insert into Item(id, name, price, description, is_available, category_id) values(5, "Coca Cola", 3.5, null, 0, 3);
insert into Item(id, name, price, description, is_available, category_id) values(6, "Fanta", 3.5, null, 0, 3);
insert into Item(id, name, price, description, is_available, category_id) values(7, "Caj", 2, null, 0, 3);
insert into Item(id, name, price, description, is_available, category_id) values(8, "Kafa", 1.5, null, 0, 3);


