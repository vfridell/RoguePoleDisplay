if object_id('Interaction') is not null
begin
	drop table Interaction;
end

if object_id('Player') is not null
begin
	drop table Player;
end

create table Player
(
	Id int identity primary key,
	Name varchar(20) not null unique,
	QuestionLine1 varchar(20) not null,
	QuestionLine2 varchar(20),
	Answer int not null default 0,
);

create table Interaction
(
	Id int identity primary key,
	Timestamp datetime not null,
	PlayerId int null foreign key references Player(Id),
	Success bit not null,
	ResultValue int not null,
	ResultText varchar(40),
	DisplayText varchar(40),
);

insert into Player (name, questionline1, questionline2, answer) values ('Digit', 'What''s the', 'secret number?', 3);
insert into Player (name, questionline1, questionline2, answer) values ('Foom', 'What''s the velocity', 'of a laden swallow?', 2);
insert into Player (name, questionline1, questionline2, answer) values ('Sparky', 'How many stairs must', 'a man fall down?', 0);
insert into Player (name, questionline1, questionline2, answer) values ('Mr Pibb', 'What time do we', 'let the dogs out?', 0);
insert into Player (name, questionline1, questionline2, answer) values ('Pillfred', 'What is', 'your quest?', 0);
insert into Player (name, questionline1, questionline2, answer) values ('Swiper', 'How many fingers', 'are you holding up?', 0);
insert into Player (name, questionline1, questionline2, answer) values ('Father Time', 'How old are you?', '', 0);
insert into Player (name, questionline1, questionline2, answer) values ('West Wind', 'How much wind', 'has broken today?', 0);
insert into Player (name, questionline1, questionline2, answer) values ('Doc Nasty', 'How many sick bugs', 'have you healed?', 0);