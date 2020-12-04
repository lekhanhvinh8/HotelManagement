create database HotelManagement;
go
use HotelManagement;
go
create table Room_categories(
	id int not null identity(1,1) primary key,
	name nvarchar(max),
	unit_price real,
	max_number_of_guests int,
	num_start_surcharge int,
	surcharge_rate real,
);
create table Guest_categories(
	id int not null identity(1,1) primary key,
	name nvarchar(max),
	coefficient real,
);
create table Rooms(
	id int not null identity(1,1) primary key,
	room_number char(10) not null unique,
	note nvarchar(max),
);
create table Guests(
	id int not null identity(1,1) primary key,
	cmnd int not null unique,
	name nvarchar(max),
	address nvarchar(max),
);

--Add foreign key
go
alter table Rooms add room_category_id int not null;
alter table Guests add guest_category_id int not null;
go
alter table Rooms add constraint roomsFKCategoryID foreign key (room_category_id) references Room_categories(id);
alter table Guests add constraint guestsFKCategoryID foreign key (guest_category_id) references Guest_categories(id);

--Create week entity tables
go
Create table Room_rental_slip(
	id int not null identity(1,1) primary key,
	room_id int not null,
	start_date date not null,
	end_date date not null,

	unique (room_id, start_date, end_date),
	foreign key (room_id) references Rooms(id),
);
Create table Bill(
	room_rental_slip_id int not null,
	guest_id int not null,
	date_of_invoice date not null,
	total_cost real,

	primary key (room_rental_slip_id, guest_id, date_of_invoice),
	foreign key (room_rental_slip_id) references Room_rental_slip(id),
	foreign key (guest_id) references Guests(id),
);
--Create many to many tables
go
Create table In_room(
	guest_id int not null,
	room_rental_slip_id int not null,

	primary key (guest_id, room_rental_slip_id),
	foreign key (guest_id) references Guests(id),
	foreign key (room_rental_slip_id) references Room_rental_slip(id),
);

--add constraint



--Add some sample datas
Insert into Room_categories(name, unit_price, max_number_of_guests, surcharge_rate, num_start_surcharge) values('A', 100000, 3, 1.5, 3);
Insert into Room_categories(name, unit_price, max_number_of_guests, surcharge_rate, num_start_surcharge) values('Vip', 500000, 2, 0, 3);
Insert into Room_categories(name, unit_price, max_number_of_guests, surcharge_rate, num_start_surcharge) values('Super Vip', 1000000, 4, 0, 5);

Insert into Guest_categories(name, coefficient) values('Trong nuoc', 1);
Insert into Guest_categories(name, coefficient) values('Nuoc ngoai', 1.5);
Insert into Guest_categories(name, coefficient) values('Tre em duoi 3 tuoi', 0);
go
insert into Rooms(room_number, note, room_category_id) values('A11', 'Khu A tang 1 phong 1', 1);
insert into Rooms(room_number, note, room_category_id) values('A13', 'Khu A tang 1 phong 3', 2);
insert into Rooms(room_number, note, room_category_id) values('A21', 'Khu A tang 2 phong 1', 2);
insert into Rooms(room_number, note, room_category_id) values('A22', 'Khu A tang 2 phong 2', 3);

insert into Guests(cmnd, name, guest_category_id) values('123456789', 'Khach A', 1);
insert into Guests(cmnd, name, guest_category_id) values('223456789', 'Khach B', 2);
insert into Guests(cmnd, name, guest_category_id) values('323456789', 'Khach C', 2);
insert into Guests(cmnd, name, guest_category_id) values('423456789', 'Khach D', 1);
insert into Guests(cmnd, name, guest_category_id) values('523456789', 'Khach E', 3);
insert into Guests(cmnd, name, guest_category_id) values('623456789', 'Khach F', 3);
go
insert into Room_rental_slip(room_id, start_date, end_date) values(1, '2020-05-01', '2020-06-01');
insert into Room_rental_slip(room_id, start_date, end_date) values(2, '2020-12-12', '2020-12-13');

insert into In_room(room_rental_slip_id, guest_id) values(1, 1);
insert into In_room(room_rental_slip_id, guest_id) values(1, 2);
insert into In_room(room_rental_slip_id, guest_id) values(1, 3);
insert into In_room(room_rental_slip_id, guest_id) values(2, 4);
insert into In_room(room_rental_slip_id, guest_id) values(2, 5);
insert into In_room(room_rental_slip_id, guest_id) values(2, 1);



	
	select sum(rc.unit_price * gc.coefficient) * DATEDIFF(DAY, rr.start_date, rr.end_date)
	from Room_rental_slip rr inner join Rooms rs on rr.id = rs.id
							 inner join Room_categories rc on rs.room_category_id = rc.id
							 inner join In_room ir on rr.id = ir.room_rental_slip_id
							 inner join Guests gs on ir.guest_id = gs.id
							 inner join Guest_categories gc on gs.guest_category_id = gc.id
	where rr.id = 1
	group by rr.id, rr.start_date, rr.end_date, rs.room_number, rc.name, rc.unit_price, rc.surcharge_rate






