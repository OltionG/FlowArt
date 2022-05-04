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

/*
create table Songs(
SongID int identity(1,1),
SongIcon nvarchar(500),
SongTitle nvarchar(500),
SongArtist nvarchar(500),
SongGenre nvarchar(500),
Album nvarchar(500),
ReleaseDate date
)
*/