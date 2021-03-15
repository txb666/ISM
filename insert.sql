use [ISM]
go
insert into Roles(role_name) values('Admin')
insert into Roles(role_name) values('Staff')
insert into Roles(role_name) values('Degree')
insert into Roles(role_name) values('Mobility')
go
insert into Users(role_id,email,account,[password],[status],isFirstLoggedIn,fullname) values(1,'admin@gmail.com','admin','admin',1,0,'thanh')
insert into Users(role_id,email,account,[password],[status],isFirstLoggedIn,fullname) values(2,'staff@gmail.com','staff','staff',1,0,'ban')
insert into Users(role_id,email,account,[password],[status],isFirstLoggedIn,fullname) values(2,'staff1@gmail.com','staff1','staff',1,0,'hai')
insert into Users(role_id,email,account,[password],[status],isFirstLoggedIn,fullname) values(2,'staff2@gmail.com','staff2','staff',1,0,'bach')
insert into Users(role_id,email,account,[password],[status],isFirstLoggedIn,fullname) values(3,'degree@gmail.com','degree','degree',1,0,'An')
go
insert into Programs([program_name]) values('IT Training')
go
insert into Campus(campus_name) values('FHN')
go
insert into Student_Group(program_id,campus_id,[year],duration_start,duration_end) values(1,1,2021,'01-01-2021','01-01-2026')
go
insert into Coordinators(staff_id,studentGroup_id) values(2,1)
insert into Coordinators(staff_id,studentGroup_id) values(3,1)
insert into Coordinators(staff_id,studentGroup_id) values(4,1)
go
insert into Passports(student_id,picture,passport_number,[start_date],expired_date,issuing_authority) values(5,'pic url','PASSPORT123','01-01-2019','01-01-2025','ABC')