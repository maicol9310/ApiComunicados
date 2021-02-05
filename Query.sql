create database StoreDB
go
use StoreDB
go

create table Administrador(
	ID int identity(1,1) primary key not null,
	FirstName varchar(50) not null,
	LastName varchar(50) not null,
	UserName varchar(32) not null,
	Password varchar(32) not null,
    idRol int NULL
)

create table Usuario(
	ID int identity(1,1) primary key not null,
	FirstName varchar(50) not null,
	LastName varchar(50) not null,
	UserName varchar(32) not null,
	Password varchar(32) not null,
	idRol int NULL
)

create table ExternaInterna(
	ID int identity(1,1) primary key not null,
	Name varchar(255) not null
)

create table UsuarioComunicado(
	ID int identity(1,1) primary key not null,
	UsuarioID int not null,
	Consecutivo varchar(50) not null,
	ExternaInternaID int not null,
	Texto varchar(50) null,
	constraint FK_Usuario_UsuarioComunicado foreign key (UsuarioID) references Usuario(ID),
	constraint FK_ExternaInterna_UsuarioComunicado foreign key (ExternaInternaID) references ExternaInterna(ID)
)