USE [master]
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'ISM')
BEGIN
    DROP DATABASE ISM;  
END;
	CREATE DATABASE ISM;
GO

USE ISM
GO

--Create tables
create table [Roles] (
	role_id int not null identity(1,1) primary key,
	role_name nvarchar(255) not null,
	created_date datetime,
	modified_date datetime
)
go
create table [Campus] (
	campus_id int not null identity(1,1) primary key,
	campus_name nvarchar(255) not null,
	[description] text,
	created_date datetime,
	modified_date datetime
)
go
create table [Programs] (
	program_id int not null identity(1,1) primary key,
	[program_name] nvarchar(255) not null,
	[description] text,
	created_date datetime,
	modified_date datetime
)
go
create table [Articles] (
	article_id int not null identity(1,1) primary key,
	[type] nvarchar(255) not null,
	title nvarchar(255) not null,
	content text not null,
	created_date datetime,
	modified_date datetime
)
go
create table [FAQs] (
	faq_id int not null identity(1,1) primary key,
	question text not null,
	answer text not null,
	created_date datetime,
	modified_date datetime
)
go
create table [Users] (
	[user_id] int not null identity(1,1) primary key,
	role_id int not null,
	studentGroup_id int,
	email nvarchar(255) not null,
	account nvarchar(255) not null,
	[password] nvarchar(255) not null,
	[status] bit not null,
	isFirstLoggedIn bit not null,
	[start_date] date,
	[end_date] date,
	fullname nvarchar(255),
	nationality nvarchar(255),
	DOB date,
	gender bit,
	emergency_contact nvarchar(255),
	created_date datetime,
	modified_date datetime
)
go
create table [Student_Handbook] (
	student_handbook_id int not null identity(1,1) primary key,
	campus_id int not null,
	title nvarchar(255) not null,
	content text not null,
	created_date datetime,
	modified_date datetime
)
go
create table [Local_Recommendation] (
	local_recommendation_id int not null identity(1,1) primary key,
	campus_id int not null,
	title nvarchar(255) not null,
	content nvarchar(255) not null,
	created_date datetime,
	modified_date datetime
)
go
create table [Insurances] (
	insurance_id int not null identity(1,1) primary key,
	student_id int not null,
	picture nvarchar(255),
	[start_date] date not null,
	[expried_date] date not null,
	created_date datetime,
	modified_date datetime
)
go
create table [Pre_Approval_Visa_Letter] (
	pre_approval_visa_letter_id int not null identity(1,1) primary key,
	student_id int not null,
	home_univercity nvarchar(255) not null,
	visa_type nvarchar(255) not null,
	visa_period nvarchar(255) not null,
	apply_receive nvarchar(255),
	created_date datetime,
	modified_date datetime
)
go
create table [Visa] (
	visa_id int not null identity(1,1) primary key,
	student_id int not null,
	picture nvarchar(255) not null,
	[start_date] date not null,
	[expired_date] date not null,
	date_entry date not null,
	entry_port nvarchar(255) not null,
	created_date datetime,
	modified_date datetime
)
go
create table [Passports] (
	passport_id int not null identity(1,1) primary key,
	student_id int not null,
	picture nvarchar(255) not null,
	passport_number nvarchar(255) not null,
	[start_date] date not null,
	[expired_date] date not null,
	issuing_authority nvarchar(255) not null,
	created_date datetime,
	modified_date datetime
)
go
create table [Accommodation_Notification] (
	accommodation_notification_id int not null identity(1,1) primary key,
	student_id int not null,
	IsMonth bit not null,
	day_before int not null,
	IsNotify bit not null,
	created_date datetime,
	modified_date datetime
)
go
create table [Flights] (
	flight_id int not null identity(1,1) primary key,
	student_id int not null,
	flight_number_a nvarchar(255) not null,
	arrival_date_a date not null,
	arrival_time_a time not null,
	airport_departure_a nvarchar(255) not null,
	airport_arrival_a nvarchar(255) not null,
	picture_a nvarchar(255) not null,
	flight_number_d nvarchar(255),
	arrival_date_d date,
	arrival_time_d time,
	airport_departure_d nvarchar(255),
	airport_arrival_d nvarchar(255),
	picture_d nvarchar(255),
	created_date datetime,
	modified_date datetime
)
go
create table [Contact_Information] (
	contact_information_id int not null identity(1,1) primary key,
	staff_id int not null,
	email nvarchar(255) not null,
	telephone nvarchar(255) not null,
	picture nvarchar(255) not null,
	position nvarchar(255) not null,
	created_date datetime,
	modified_date datetime
)
go
create table [Notification_Information] (
	notification_information_id int not null identity(1,1) primary key,
	[user_id] int not null,
	content text not null,
	created_date datetime,
	modified_date datetime
)
go
create table [Notification_Configuration] (
	notification_configuration_id int not null identity(1,1) primary key,
	studentGroup_id int not null,
	[type] nvarchar(255) not null,
	days_before int,
	hours_before int,
	deadline datetime,
	created_date datetime,
	modified_date datetime
)
go
create table [General_Agenda] (
	general_agenda_id int not null identity(1,1) primary key,
	studentGroup_id int not null,
	content text not null,
	note text,
	created_date datetime,
	modified_date datetime
)
go
create table [Detailed_Agenda] (
	detailed_agenda_id int not null identity(1,1) primary key,
	studentGroup_id int not null,
	[date] date not null,
	[time_start] time not null,
	[time_end] time not null,
	timezone nvarchar(255) not null,
	venue nvarchar(255) not null,
	PIC nvarchar(255) not null,
	content text not null,
	created_date datetime,
	modified_date datetime
)
go
create table [Transportations] (
	transportations_id int not null identity(1,1) primary key,
	studentGroup_id int not null,
	[date] date not null,
	[time] time not null,
	bus nvarchar(255) not null,
	driver nvarchar(255) not null,
	itinerary nvarchar(255) not null,
	supporter nvarchar(255) not null,
	note text,
	created_date datetime,
	modified_date datetime
)
go
create table [ORT_Schedule] (
	ort_schedule_id int not null identity(1,1) primary key,
	student_id int not null,
	program_id int not null,
	content text not null,
	[date] date not null,
	[time] time not null,
	[location] nvarchar(255) not null,
	requirement nvarchar(255) not null,
	created_date datetime,
	modified_date datetime
)
go
create table [ORT_Material_Slide] (
	ort_material_slide_id int not null identity(1,1) primary key,
	program_id int not null,
	student_id int not null,
	content text not null,
	material nvarchar(255) not null,
	created_date datetime,
	modified_date datetime
)
go
create table [Orientation_Materials] (
	orientation_materials_id int not null identity(1,1) primary key,
	studentGroup_id int not null,
	content text not null,
	note text,
	created_date datetime,
	modified_date datetime
)
go
create table [Current_Accommodation] (
	current_accommodation_id int not null identity(1,1) primary key,
	student_id int not null,
	studentGroup_id int not null,
	[type] nvarchar(255) not null,
	[location] nvarchar(255) not null,
	[description] text,
	fee float,
	picture nvarchar(255),
	note text,
	created_date datetime,
	modified_date datetime
)
go
create table [Register_Accommodation] (
	register_accommodation_id int not null identity(1,1) primary key,
	student_id int not null,
	fullname nvarchar(255) not null,
	email nvarchar(255) not null,
	home_univercity nvarchar(255),
	exchange_campus nvarchar(255),
	accommodation_option nvarchar(255) not null,
	cost_per_month float not null,
	room_size float not null,
	room_type nvarchar(255) not null,
	distance float not null,
	other_request nvarchar(255),
	created_date datetime,
	modified_date datetime
)
go
create table [Student_Group] (
	student_group_id int not null identity(1,1) primary key,
	program_id int not null,
	campus_id int not null,
	[year] int not null,
	duration_start date not null,
	duration_end date not null,
	home_univercity nvarchar(255),
	note text,
	created_date datetime,
	modified_date datetime
)
go
create table [Coordinators] (
	coordinator_id int not null identity(1,1) primary key,
	staff_id int not null,
	studentGroup_id int not null,
	created_date datetime,
	modified_date datetime
)
go
create table [Meeting_AvailableTime] (
	meeting_available_time_id int not null identity(1,1) primary key,
	staff_id int not null,
	[date] date not null,
	start_time time not null,
	end_time time not null,
	created_date datetime,
	modified_date datetime
)
go
create table [Meeting_Schedule] (
	meeting_schedule_id int not null identity(1,1) primary key,
	staff_id int not null,
	student_id int not null,
	note text not null,
	IsAccepted bit not null,
	created_date datetime,
	modified_date datetime
)
go

--Set foreign key
alter table [Users]
add foreign key (role_id) references [Roles](role_id),
	foreign key (studentGroup_id) references [Student_Group](student_group_id)
go
alter table [Student_Handbook]
add foreign key (campus_id) references [Campus](campus_id)
go
alter table [Local_Recommendation]
add foreign key (campus_id) references [Campus](campus_id)
go
alter table [Insurances]
add foreign key (student_id) references [Users]([user_id])
go
alter table [Pre_Approval_Visa_Letter]
add foreign key (student_id) references [Users]([user_id])
go
alter table [Visa]
add foreign key (student_id) references [Users]([user_id])
go
alter table [Passports]
add foreign key (student_id) references [Users]([user_id])
go
alter table [Accommodation_Notification]
add foreign key (student_id) references [Users]([user_id])
go
alter table [Flights]
add foreign key (student_id) references [Users]([user_id])
go
alter table [Contact_Information]
add foreign key (staff_id) references [Users]([user_id])
go
alter table [Notification_Information]
add foreign key ([user_id]) references [Users]([user_id])
go
alter table [Notification_Configuration]
add foreign key (studentGroup_id) references [Student_Group](student_group_id)
go
alter table [General_Agenda]
add foreign key (studentGroup_id) references [Student_Group](student_group_id)
go
alter table [Detailed_Agenda]
add foreign key (studentGroup_id) references [Student_Group](student_group_id)
go
alter table [Transportations]
add foreign key (studentGroup_id) references [Student_Group](student_group_id)
go
alter table [ORT_Schedule]
add foreign key (student_id) references [Users]([user_id]),
	foreign key (program_id) references [Programs](program_id)
go
alter table [ORT_Material_Slide]
add foreign key (student_id) references [Users]([user_id]),
	foreign key (program_id) references [Programs](program_id)
go
alter table [Orientation_Materials]
add foreign key (studentGroup_id) references [Student_Group](student_group_id)
go
alter table [Current_Accommodation]
add foreign key (student_id) references [Users]([user_id]),
	foreign key (studentGroup_id) references [Student_Group](student_group_id)
go
alter table [Register_Accommodation]
add foreign key (student_id) references [Users]([user_id])
go
alter table [Student_Group]
add foreign key (program_id) references [Programs](program_id),
	foreign key (campus_id) references [Campus](campus_id)
go
alter table [Coordinators]
add foreign key (staff_id) references [Users]([user_id]),
	foreign key (studentGroup_id) references [Student_Group](student_group_id)
go
alter table [Meeting_AvailableTime]
add foreign key (staff_id) references [Users]([user_id])
go
alter table [Meeting_Schedule]
add foreign key (staff_id) references [Users]([user_id]),
	foreign key (student_id) references [Users]([user_id])
go

--create trigger
--Accommodation_Notification table
create trigger IAccommodation_Notification on Accommodation_Notification
after insert
as
begin
	update Accommodation_Notification
	set created_date = GETDATE()
	where accommodation_notification_id in (select inserted.accommodation_notification_id from inserted);
end
go
create trigger UAccommodation_Notification on Accommodation_Notification
after update
as
begin
	update Accommodation_Notification
	set modified_date = GETDATE()
	where accommodation_notification_id in (select inserted.accommodation_notification_id from inserted);
end
go
--Articles table
create trigger IArticle on Articles
after insert
as
begin
	update Articles
	set created_date = GETDATE()
	where article_id in (select inserted.article_id from inserted);
end
go
create trigger UArticle on Articles
after update
as
begin
	update Articles
	set modified_date = GETDATE()
	where article_id in (select inserted.article_id from inserted);
end
go
--Campus table
create trigger ICampus on Campus
after insert
as
begin
	update Campus
	set created_date = GETDATE()
	where campus_id in (select inserted.campus_id from inserted);
end
go
create trigger UCampus on Campus
after update
as
begin
	update Campus
	set modified_date = GETDATE()
	where campus_id in (select inserted.campus_id from inserted);
end
go
--Contact_Information table
create trigger IContact_Information on Contact_Information
after insert
as
begin
	update Contact_Information
	set created_date = GETDATE()
	where contact_information_id in (select inserted.contact_information_id from inserted);
end
go
create trigger UContact_Information on Contact_Information
after update
as
begin
	update Contact_Information
	set modified_date = GETDATE()
	where contact_information_id in (select inserted.contact_information_id from inserted);
end
go
--Coordinators table
create trigger ICoordinator on Coordinators
after insert
as
begin
	update Coordinators
	set created_date = GETDATE()
	where coordinator_id in (select inserted.coordinator_id from inserted);
end
go
create trigger UCoordinator on Coordinators
after update
as
begin
	update Coordinators
	set modified_date = GETDATE()
	where coordinator_id in (select inserted.coordinator_id from inserted);
end
go
--Current_Accommodation table
create trigger ICurrent_Accommodation on Current_Accommodation
after insert
as
begin
	update Current_Accommodation
	set created_date = GETDATE()
	where current_accommodation_id in (select inserted.current_accommodation_id from inserted);
end
go
create trigger UCurrent_Accommodation on Current_Accommodation
after update
as
begin
	update Current_Accommodation
	set modified_date = GETDATE()
	where current_accommodation_id in (select inserted.current_accommodation_id from inserted);
end
go
--Detailed_Agenda table
create trigger IDetailed_Agenda on Detailed_Agenda
after insert
as
begin
	update Detailed_Agenda
	set created_date = GETDATE()
	where detailed_agenda_id in (select inserted.detailed_agenda_id from inserted);
end
go
create trigger UDetailed_Agenda on Detailed_Agenda
after update
as
begin
	update Detailed_Agenda
	set modified_date = GETDATE()
	where detailed_agenda_id in (select inserted.detailed_agenda_id from inserted);
end
go
--FAQs table
create trigger IFAQ on FAQs
after insert
as
begin
	update FAQs
	set created_date = GETDATE()
	where faq_id in (select inserted.faq_id from inserted);
end
go
create trigger UFAQ on FAQs
after update
as
begin
	update FAQs
	set modified_date = GETDATE()
	where faq_id in (select inserted.faq_id from inserted);
end
go
--Flights table
create trigger IFlight on Flights
after insert
as
begin
	update Flights
	set created_date = GETDATE()
	where flight_id in (select inserted.flight_id from inserted);
end
go
create trigger UFlight on Flights
after update
as
begin
	update Flights
	set modified_date = GETDATE()
	where flight_id in (select inserted.flight_id from inserted);
end
go
--General_Agenda table
create trigger IGeneral_Agenda on General_Agenda
after insert
as
begin
	update General_Agenda
	set created_date = GETDATE()
	where general_agenda_id in (select inserted.general_agenda_id from inserted);
end
go
create trigger UGeneral_Agenda on General_Agenda
after update
as
begin
	update General_Agenda
	set modified_date = GETDATE()
	where general_agenda_id in (select inserted.general_agenda_id from inserted);
end
go
--Insurances table
create trigger IInsurance on Insurances
after insert
as
begin
	update Insurances
	set created_date = GETDATE()
	where insurance_id in (select inserted.insurance_id from inserted);
end
go
create trigger UInsurance on Insurances
after update
as
begin
	update Insurances
	set modified_date = GETDATE()
	where insurance_id in (select inserted.insurance_id from inserted);
end
go
--Local_Recommendation table
create trigger ILocal_Recommendation on Local_Recommendation
after insert
as
begin
	update Local_Recommendation
	set created_date = GETDATE()
	where local_recommendation_id in (select inserted.local_recommendation_id from inserted);
end
go
create trigger ULocal_Recommendation on Local_Recommendation
after update
as
begin
	update Local_Recommendation
	set modified_date = GETDATE()
	where local_recommendation_id in (select inserted.local_recommendation_id from inserted);
end
go
--Meeting_AvailableTime table
create trigger IMeeting_AvailableTime on Meeting_AvailableTime
after insert
as
begin
	update Meeting_AvailableTime
	set created_date = GETDATE()
	where meeting_available_time_id in (select inserted.meeting_available_time_id from inserted);
end
go
create trigger UMeeting_AvailableTime on Meeting_AvailableTime
after update
as
begin
	update Meeting_AvailableTime
	set modified_date = GETDATE()
	where meeting_available_time_id in (select inserted.meeting_available_time_id from inserted);
end
go
--Meeting_Schedule table
create trigger IMeeting_Schedule on Meeting_Schedule
after insert
as
begin
	update Meeting_Schedule
	set created_date = GETDATE()
	where meeting_schedule_id in (select inserted.meeting_schedule_id from inserted);
end
go
create trigger UMeeting_Schedule on Meeting_Schedule
after update
as
begin
	update Meeting_Schedule
	set modified_date = GETDATE()
	where meeting_schedule_id in (select inserted.meeting_schedule_id from inserted);
end
go
--Notification_Configuration table
create trigger INotification_Configuration on Notification_Configuration
after insert
as
begin
	update Notification_Configuration
	set created_date = GETDATE()
	where notification_configuration_id in (select inserted.notification_configuration_id from inserted);
end
go
create trigger UNotification_Configuration on Notification_Configuration
after update
as
begin
	update Notification_Configuration
	set modified_date = GETDATE()
	where notification_configuration_id in (select inserted.notification_configuration_id from inserted);
end
go
--Notification_Information table
create trigger INotification_Information on Notification_Information
after insert
as
begin
	update Notification_Information
	set created_date = GETDATE()
	where notification_information_id in (select inserted.notification_information_id from inserted);
end
go
create trigger UNotification_Information on Notification_Information
after update
as
begin
	update Notification_Information
	set modified_date = GETDATE()
	where notification_information_id in (select inserted.notification_information_id from inserted);
end
go
--Orientation_Materials table
create trigger IOrientation_Materials on Orientation_Materials
after insert
as
begin
	update Orientation_Materials
	set created_date = GETDATE()
	where orientation_materials_id in (select inserted.orientation_materials_id from inserted);
end
go
create trigger UOrientation_Materials on Orientation_Materials
after update
as
begin
	update Orientation_Materials
	set modified_date = GETDATE()
	where orientation_materials_id in (select inserted.orientation_materials_id from inserted);
end
go
--ORT_Material_Slide table
create trigger IORT_Material_Slide on ORT_Material_Slide
after insert
as
begin
	update ORT_Material_Slide
	set created_date = GETDATE()
	where ort_material_slide_id in (select inserted.ort_material_slide_id from inserted);
end
go
create trigger UORT_Material_Slide on ORT_Material_Slide
after update
as
begin
	update ORT_Material_Slide
	set modified_date = GETDATE()
	where ort_material_slide_id in (select inserted.ort_material_slide_id from inserted);
end
go
--ORT_Schedule table
create trigger IORT_Schedule on ORT_Schedule
after insert
as
begin
	update ORT_Schedule
	set created_date = GETDATE()
	where ort_schedule_id in (select inserted.ort_schedule_id from inserted);
end
go
create trigger UORT_Schedule on ORT_Schedule
after update
as
begin
	update ORT_Schedule
	set modified_date = GETDATE()
	where ort_schedule_id in (select inserted.ort_schedule_id from inserted);
end
go
--Passports table
create trigger IPassport on Passports
after insert
as
begin
	update Passports
	set created_date = GETDATE()
	where passport_id in (select inserted.passport_id from inserted);
end
go
create trigger UPassport on Passports
after update
as
begin
	update Passports
	set modified_date = GETDATE()
	where passport_id in (select inserted.passport_id from inserted);
end
go
--Pre_Approval_Visa_Letter table
create trigger IPre_Approval_Visa_Letter on Pre_Approval_Visa_Letter
after insert
as
begin
	update Pre_Approval_Visa_Letter
	set created_date = GETDATE()
	where pre_approval_visa_letter_id in (select inserted.pre_approval_visa_letter_id from inserted);
end
go
create trigger UPre_Approval_Visa_Letter on Pre_Approval_Visa_Letter
after update
as
begin
	update Pre_Approval_Visa_Letter
	set modified_date = GETDATE()
	where pre_approval_visa_letter_id in (select inserted.pre_approval_visa_letter_id from inserted);
end
go
--Programs table
create trigger IProgram on Programs
after insert
as
begin
	update Programs
	set created_date = GETDATE()
	where program_id in (select inserted.program_id from inserted);
end
go
create trigger UProgram on Programs
after update
as
begin
	update Programs
	set modified_date = GETDATE()
	where program_id in (select inserted.program_id from inserted);
end
go
--Register_Accommodation table
create trigger IRegister_Accommodation on Register_Accommodation
after insert
as
begin
	update Register_Accommodation
	set created_date = GETDATE()
	where register_accommodation_id in (select inserted.register_accommodation_id from inserted);
end
go
create trigger URegister_Accommodation on Register_Accommodation
after update
as
begin
	update Register_Accommodation
	set modified_date = GETDATE()
	where register_accommodation_id in (select inserted.register_accommodation_id from inserted);
end
go
--Roles table
create trigger IRole on Roles
after insert
as
begin
	update Roles
	set created_date = GETDATE()
	where role_id in (select inserted.role_id from inserted);
end
go
create trigger URole on Roles
after update
as
begin
	update Roles
	set modified_date = GETDATE()
	where role_id in (select inserted.role_id from inserted);
end
go
--Student_Group table
create trigger IStudent_Group on Student_Group
after insert
as
begin
	update Student_Group
	set created_date = GETDATE()
	where student_group_id in (select inserted.student_group_id from inserted);
end
go
create trigger UStudent_Group on Student_Group
after update
as
begin
	update Student_Group
	set modified_date = GETDATE()
	where student_group_id in (select inserted.student_group_id from inserted);
end
go
--Student_Handbook table
create trigger IStudent_Handbook on Student_Handbook
after insert
as
begin
	update Student_Handbook
	set created_date = GETDATE()
	where student_handbook_id in (select inserted.student_handbook_id from inserted);
end
go
create trigger UStudent_Handbook on Student_Handbook
after update
as
begin
	update Student_Handbook
	set modified_date = GETDATE()
	where student_handbook_id in (select inserted.student_handbook_id from inserted);
end
go
--Transportations table
create trigger ITransportation on Transportations
after insert
as
begin
	update Transportations
	set created_date = GETDATE()
	where transportations_id in (select inserted.transportations_id from inserted);
end
go
create trigger UTransportation on Transportations
after update
as
begin
	update Transportations
	set modified_date = GETDATE()
	where transportations_id in (select inserted.transportations_id from inserted);
end
go
--Users table
create trigger IUser on Users
after insert
as
begin
	update Users
	set created_date = GETDATE()
	where [user_id] in (select inserted.[user_id] from inserted);
end
go
create trigger UUser on Users
after update
as
begin
	update Users
	set modified_date = GETDATE()
	where [user_id] in (select inserted.[user_id] from inserted);
end
go
--Visa table
create trigger IVisa on Visa
after insert
as
begin
	update Visa
	set created_date = GETDATE()
	where visa_id in (select inserted.visa_id from inserted);
end
go
create trigger UVisa on Visa
after update
as
begin
	update Visa
	set modified_date = GETDATE()
	where visa_id in (select inserted.visa_id from inserted);
end
go