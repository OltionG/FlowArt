create database FlowArt
use FlowArt

create table Books(
BookID int identity(1,1),
BookIcon nvarchar(500),
BookTitle nvarchar(500),
BookAuthor nvarchar(500),
BookGenre nvarchar(500),
PublishDate date
)

drop table Books
select * from Books



create table City(
CityID int identity(1,1),
CityName nvarchar(500)
)
insert into City values('Prizren')