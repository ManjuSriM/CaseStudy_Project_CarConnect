--CarConnect
Use CarConnect

Create table Customer 
(
CustomerId int primary key,
FirstName varchar(50),
LastName varchar(50),
Email varchar(100) unique not null,
PhoneNumber bigint unique check (len(PhoneNumber)=10),
Address varchar(255),
Username varchar(50) unique not null,
Password varchar(255) not null,
RegistrationDate datetime not null
)
Go

Create table Vehicle 
(
VehicleId int primary key,
Model varchar(50) not null,
Make varchar(50) not null,
Year int not null,
Color varchar(30),
RegistrationNumber varchar(50) unique not null,
Availability bit check (Availability in (0, 1)),
DailyRate int not null
)
Go

create table Reservation 
(
ReservationId int primary key,
CustomerId int not null,
VehicleId int not null,
StartDate datetime not null,
EndDate datetime not null,
TotalCost int not null,
Status varchar(15) check (Status in ('Pending', 'Confirmed', 'Completed')),
foreign key (CustomerId) references Customer(CustomerId) on delete cascade on update cascade,
foreign key (VehicleId) references Vehicle(VehicleId) on delete cascade on update cascade
)
Go

Create table Admin 
(
AdminId int primary key,
FirstName varchar(50),
LastName varchar(50),
Email varchar(100) not null,
PhoneNumber bigint unique check (len(PhoneNumber)=10),
Username varchar(50) unique not null,
Password varchar(255) not null,
Role varchar(15) check (Role in ('Super Admin', 'Fleet Manager')),
JoinDate datetime not null
)
Go

Insert into Customer 
values
(1, 'Aarav', 'Singh', 'aarav.singh@gmail.com', 9876543210, 'Mumbai, India', 'aarav', 'password123', '2021-05-05'),
(2, 'Priya', 'Patel', 'priya.patel@gmail.com', 9876543211, 'Delhi, India', 'priya', 'password234', '2021-07-18'),
(3, 'Kunal', 'Gupta', 'kunal.gupta@gmail.com', 9876543212, 'Bangalore, India', 'kunal', 'password345', '2021-08-05'),
(4, 'Isha', 'Mehta', 'isha.mehta@gmail.com', 9876543213, 'Kolkata, India', 'isha', 'password456', '2022-02-18'),
(5, 'Ravi', 'Verma', 'ravi.verma@gmail.com', 9876543214, 'Chennai, India', 'ravi', 'password567', '2022-04-25'),
(6, 'Neelam', 'Yadav', 'neelam.yadav@gmail.com', 9876543215, 'Hyderabad, India', 'neelam', 'password678', '2022-06-01'),
(7, 'Saurabh', 'Sharma', 'saurabh.sharma@gmail.com', 9876543216, 'Pune, India', 'saurabh', 'password789', '2022-09-30'),
(8, 'Ananya', 'Singh', 'ananya.singh@gmail.com', 9876543217, 'Ahmedabad, India', 'ananya', 'password890', '2022-10-20'),
(9, 'Aman', 'Gupta', 'aman.gupta@gmail.com', 9876543218, 'Jaipur, India', 'aman', 'password901', '2022-11-10'),
(10, 'Simran', 'Kumar', 'simran.kumar@gmail.com', 9876543219, 'Lucknow, India', 'simran', 'password012', '2023-01-12'),
(11, 'Ritika', 'Joshi', 'ritika.joshi@gmail.com', 9876543220, 'Gurgaon, India', 'ritika', 'password1234', '2023-02-20'),
(12, 'Vikas', 'Shukla', 'vikas.shukla@gmail.com', 9876543221, 'Noida, India', 'vikas', 'password2345', '2023-03-04'),
(13, 'Sanya', 'Verma', 'sanya.verma@gmail.com', 9876543222, 'Kochi, India', 'sanya', 'password3456', '2023-04-15'),
(14, 'Rohan', 'Patel', 'rohan.patel@gmail.com', 9876543223, 'Indore, India', 'rohan', 'password4567', '2023-07-21'),
(15, 'Maya', 'Rao', 'maya.rao@gmail.com', 9876543224, 'Surat, India', 'maya', 'password5678', '2023-08-25')

Insert into Vehicle 
values
(1, 'Swift', 'Maruti', 2020, 'Red', 'DL01AB1234', 1, 1500),
(2, 'Alto', 'Maruti', 2019, 'Blue', 'DL02CD5678', 1, 1200),
(3, 'I20', 'Hyundai', 2021, 'White', 'DL03EF9101', 1, 1800),
(4, 'Verna', 'Hyundai', 2022, 'Black', 'DL04GH1122', 0, 2200),
(5, 'Fortuner', 'Toyota', 2021, 'Silver', 'DL05IJ3344', 1, 3500),
(6, 'Polo', 'Volkswagen', 2020, 'Green', 'DL06KL5566', 1, 2000),
(7, 'XUV700', 'Mahindra', 2023, 'Yellow', 'DL07MN7788', 0, 2800),
(8, 'Creta', 'Hyundai', 2022, 'Grey', 'DL08OP9900', 1, 2100),
(9, 'Seltos', 'Kia', 2021, 'Purple', 'DL09QR2233', 1, 2400),
(10, 'Honda City', 'Honda', 2020, 'Brown', 'DL10ST4455', 1, 1700),
(11, 'Range Rover', 'Land Rover', 2021, 'Blue', 'DL11UV6677', 0, 5000),
(12, 'Tiago', 'Tata', 2021, 'Orange', 'DL12WX8899', 1, 1300),
(13, 'EcoSport', 'Ford', 2020, 'Brown', 'DL13YZ1001', 1, 1600),
(14, 'Wagon R', 'Maruti', 2019, 'Red', 'DL14AB2345', 1, 1400.00),
(15, 'Scorpio', 'Mahindra', 2022, 'Black', 'DL15CD6789', 1, 3000)

Insert into Reservation 
values
(1, 1, 2, '2021-05-06', '2021-05-08', 3000, 'Confirmed'),
(2, 2, 5, '2021-07-20', '2021-07-22', 6000, 'Pending'),
(3, 3, 7, '2021-08-06', '2021-08-08', 4800, 'Completed'),
(4, 4, 9, '2022-02-20', '2022-02-22', 4200, 'Confirmed'),
(5, 5, 3, '2022-04-26', '2022-04-28', 3600, 'Confirmed'),
(6, 6, 6, '2022-06-02', '2022-06-05', 5400, 'Pending'),
(7, 7, 8, '2022-10-01', '2022-10-03', 4800, 'Completed'),
(8, 8, 12, '2022-10-21', '2022-10-24', 4200, 'Pending'),
(9, 9, 4, '2022-11-12', '2022-11-15', 3900, 'Confirmed'),
(10, 10, 1, '2023-01-13', '2023-01-15', 3600, 'Completed'),
(11, 11, 10, '2023-02-22', '2023-02-25', 4800, 'Pending'),
(12, 12, 15, '2023-03-05', '2023-03-08', 5700, 'Confirmed'),
(13, 13, 14, '2023-04-16', '2023-04-19', 4500, 'Confirmed'),
(14, 14, 13, '2023-07-22', '2023-07-25', 3900, 'Completed'),
(15, 15, 11, '2023-08-26', '2023-08-29', 6000, 'Confirmed')

Insert into Admin 
values
(1, 'Anil', 'Deshmukh', 'anil.deshmukh@admin.com', 9876543201, 'anild', 'adminpass1', 'Super Admin', '2020-06-01'),
(2, 'Nisha', 'Kumar', 'nisha.kumar@admin.com', 9876543202, 'nishak', 'adminpass2', 'Fleet Manager', '2021-07-01'),
(3, 'Rajesh', 'Singh', 'rajesh.singh@admin.com', 9876543203, 'rajeshs', 'adminpass3', 'Super Admin', '2020-08-15'),
(4, 'Neha', 'Sharma', 'neha.sharma@admin.com', 9876543204, 'nehas', 'adminpass4', 'Fleet Manager', '2021-08-10'),
(5, 'Ajay', 'Gupta', 'ajay.gupta@admin.com', 9876543205, 'ajayg', 'adminpass5', 'Super Admin', '2020-09-01'),
(6, 'Rina', 'Mehta', 'rina.mehta@admin.com', 9876543206, 'rinam', 'adminpass6', 'Fleet Manager', '2021-09-05'),
(7, 'Deepak', 'Verma', 'deepak.verma@admin.com', 9876543207, 'deepakv', 'adminpass7', 'Super Admin', '2020-10-01'),
(8, 'Simran', 'Bansal', 'simran.bansal@admin.com', 9876543208, 'simranb', 'adminpass8', 'Fleet Manager', '2021-10-15'),
(9, 'Arun', 'Joshi', 'arun.joshi@admin.com', 9876543209, 'arunj', 'adminpass9', 'Super Admin', '2020-11-01'),
(10, 'Shivani', 'Rao', 'shivani.rao@admin.com', 9876543210, 'shivanir', 'adminpass10', 'Fleet Manager', '2021-11-01'),
(11, 'Manish', 'Yadav', 'manish.yadav@admin.com', 9876543211, 'manishy', 'adminpass11', 'Super Admin', '2020-12-01'),
(12, 'Kiran', 'Patel', 'kiran.patel@admin.com', 9876543212, 'kiranp', 'adminpass12', 'Fleet Manager', '2021-12-10'),
(13, 'Raghav', 'Shukla', 'raghav.shukla@admin.com', 9876543213, 'raghavs', 'adminpass13', 'Super Admin', '2021-01-01'),
(14, 'Vandana', 'Singh', 'vandana.singh@admin.com', 9876543214, 'vandanav', 'adminpass14', 'Fleet Manager', '2021-02-01'),
(15, 'Pooja', 'Reddy', 'pooja.reddy@admin.com', 9876543215, 'poojar', 'adminpass15', 'Super Admin', '2021-03-01')

Select * from Customer
Select * from Reservation