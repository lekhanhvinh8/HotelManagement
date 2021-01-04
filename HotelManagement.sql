--drop database HotelManagement;
create database HotelManagement;
go
use HotelManagement;
go
create table RoomCategories(
	Id int not null identity(1,1) primary key,
	Name nvarchar(max),
	UnitPrice real,
	MaxNumberOfGuests int,
	NumStartSurcharge int,
	SurchargeRate real,
);
create table GuestCategories(
	Id int not null identity(1,1) primary key,
	Name nvarchar(max),
	Coefficient real,
);
create table Rooms(
	Id int not null identity(1,1) primary key,
	RoomNumber char(10) not null unique,
	Note nvarchar(max),
);
create table Guests(
	Id int not null identity(1,1) primary key,
	CMND int not null unique,
	Name nvarchar(max),
	Address nvarchar(max),
);

--Add foreign key
go
alter table Rooms add RoomCategoryId int not null;
alter table Guests add GuestCategoryId int not null;
go
alter table Rooms add constraint roomsFKCategoryID foreign key (roomCategoryId) references RoomCategories(id);
alter table Guests add constraint guestsFKCategoryID foreign key (guestCategoryId) references GuestCategories(id);

--Create week entity tables
go
Create table RoomRentalSlip(
	Id int not null identity(1,1) primary key,
	RoomId int not null,
	StartDate datetime not null,
	EndDate datetime not null,

	unique (RoomId, StartDate, EndDate),
	foreign key (RoomId) references Rooms(Id),
);
Create table Invoice(
	RoomRentalSlipId int not null,
	GuestId int not null,
	DateOfInvoice datetime not null,
	TotalCost real,

	primary key (RoomRentalSlipId, GuestId, DateOfInvoice),
	foreign key (RoomRentalSlipId) references RoomRentalSlip(Id),
	foreign key (GuestId) references Guests(Id),
);
--Create many to many tables
go
Create table InRoom(
	GuestId int not null,
	RoomRentalSlipId int not null,

	primary key (GuestId, RoomRentalSlipId),
	foreign key (GuestId) references Guests(Id),
	foreign key (RoomRentalSlipId) references RoomRentalSlip(Id),
);

--add constraint



--Add some sample datas
Insert into RoomCategories(Name, UnitPrice, MaxNumberOfGuests, SurchargeRate, NumStartSurcharge) values('A', 100000, 3, 1.5, 3);
Insert into RoomCategories(Name, UnitPrice, MaxNumberOfGuests, SurchargeRate, NumStartSurcharge) values('Vip', 500000, 2, 0, 3);
Insert into RoomCategories(Name, UnitPrice, MaxNumberOfGuests, SurchargeRate, NumStartSurcharge) values('Super Vip', 1000000, 4, 0, 5);

Insert into GuestCategories(Name, Coefficient) values('Trong nuoc', 1);
Insert into GuestCategories(Name, Coefficient) values('Nuoc ngoai', 1.5);
Insert into GuestCategories(Name, Coefficient) values('Tre em duoi 3 tuoi', 0);
go
insert into Rooms(RoomNumber, Note, RoomCategoryId) values('A11', 'Khu A tang 1 phong 1', 1);
insert into Rooms(RoomNumber, Note, RoomCategoryId) values('A13', 'Khu A tang 1 phong 3', 2);
insert into Rooms(RoomNumber, Note, RoomCategoryId) values('A21', 'Khu A tang 2 phong 1', 2);
insert into Rooms(RoomNumber, Note, RoomCategoryId) values('A22', 'Khu A tang 2 phong 2', 3);

insert into Guests(CMND, Name, GuestCategoryId) values('123456789', 'Khach A', 1);
insert into Guests(CMND, Name, GuestCategoryId) values('223456789', 'Khach B', 2);
insert into Guests(CMND, Name, GuestCategoryId) values('323456789', 'Khach C', 2);
insert into Guests(CMND, Name, GuestCategoryId) values('423456789', 'Khach D', 1);
insert into Guests(CMND, Name, GuestCategoryId) values('523456789', 'Khach E', 3);
insert into Guests(CMND, Name, GuestCategoryId) values('623456789', 'Khach F', 3);
go
insert into RoomRentalSlip(RoomId, StartDate, EndDate) values(1, '2020-05-01', '2020-06-01');
insert into RoomRentalSlip(RoomId, StartDate, EndDate) values(2, '2020-12-12', '2020-12-13');

insert into InRoom(RoomRentalSlipId, GuestId) values(1, 1);
insert into InRoom(RoomRentalSlipId, GuestId) values(1, 2);
insert into InRoom(RoomRentalSlipId, GuestId) values(1, 3);
insert into InRoom(RoomRentalSlipId, GuestId) values(2, 4);
insert into InRoom(RoomRentalSlipId, GuestId) values(2, 5);
insert into InRoom(RoomRentalSlipId, GuestId) values(2, 1);

/*
select sum(rc.unit_price * gc.coefficient) * DATEDIFF(DAY, rr.start_date, rr.end_date)
from Room_rental_slip rr inner join Rooms rs on rr.id = rs.id
							inner join Room_categories rc on rs.room_category_id = rc.id
							inner join In_room ir on rr.id = ir.room_rental_slip_id
							inner join Guests gs on ir.guest_id = gs.id
							inner join Guest_categories gc on gs.guest_category_id = gc.id
where rr.id = 1
group by rr.id, rr.start_date, rr.end_date, rs.room_number, rc.name, rc.unit_price, rc.surcharge_rate
*/






