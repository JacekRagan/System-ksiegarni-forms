trzeba miec swoj serwer lokalny albo inny :p
!!!!!!!!!!!!!!!!!!!!!SQL!!!!!!!!!!!!!!!!!!!!!!
create database ksiegarnia;
create table ksiazka(
    nazwa varchar(255),
    wypozyczona boolean default false,
    autor varchar(255),
    id int not null auto_increment primary key
);
