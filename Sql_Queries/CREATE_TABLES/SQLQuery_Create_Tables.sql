create table [TRAIN]
(
	[Id] int identity,
	[Train_Name] nvarchar(30),
	constraint PK_TRAIN_Id primary key([Id]),
	constraint UQ_TRAIN_Train_Name unique([Train_Name])
);

create table [STATION]
(
	[Id] INT identity,
	[Station_Name] nvarchar(30),
	constraint PK_STATION_Id primary key([Id]),
	constraint UQ_STATION_Station_Name unique([Station_Name])
);

create table [NODE]
(
	[Id] int identity,
	[Previous_Station] int,
	[Current_Station] int,
	[Next_Station] int,
	constraint PK_NODE_Id primary key([Id]),
	constraint FK_NODE_Previous_Station foreign key([Previous_Station]) references [TRAIN]([Id]),
	constraint FK_NODE_Current_Station foreign key([Current_Station]) references [TRAIN]([Id]),
	constraint FK_NODE_Next_Station foreign key([Next_Station]) references [TRAIN]([Id])
);

create table [ROUTE]
(
	[Id] int identity,
	[Route_Name] nvarchar(50),
	constraint PK_ROUTE_Id primary key([Id]),
	constraint UQ_ROUTE_Route_Name unique([Route_Name])
);

create table [PATH]
(
	[Id] int identity,
	[Train_Id] int,
	[Node_Id] int,
	[Arrival_WeekDay] tinyint,
	[Departure_WeekDay] tinyint,
	[Arrival_ExactDate] datetime2,
	[Departure_ExactDate] datetime2,
	[Route_Id] int,
	constraint PK_PATH_Id primary key([Id]),
	constraint FK_PATH_Train_Id foreign key([Train_Id]) references [TRAIN]([Id]),
	constraint FK_PATH_Node_Id foreign key([Node_Id]) references [NODE]([Id]),
	constraint FK_PATH_Route_Id foreign key([Route_Id]) references [ROUTE]([Id])
);