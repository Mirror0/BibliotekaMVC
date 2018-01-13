create table Autor(
	ID int check (ID > 0) primary key not null identity,
    Imie varchar(15),
    Nazwisko varchar(30)
    
);

create table Wydawca(
	ID int check (ID > 0) primary key not null identity,
    Nazwa varchar(30)
);

create table Aktor(
	ID int check (ID > 0) primary key not null identity,
    Imie varchar(15),
    Nazwisko varchar(30),
);

create table Czytelnik(
	ID int check (ID > 0) primary key not null identity,
    Imie varchar(15),
    Nazwisko varchar(30),
    Uzytkownik varchar(30),
    Haslo varchar(255),
    Email varchar(50)
);

create table Slowo_Kluczowe(
	ID int check (ID > 0) primary key not null identity,
    Strona int check (Strona > 0),
    Slowo varchar(30)    
);

create table Film(
	ID int check (ID > 0) primary key not null identity,
    Tytul varchar(100),
	ID_Aktora int check (ID_Aktora > 0),
	foreign key (ID_Aktora) references Aktor(ID) on delete cascade
);

create table Praca_Naukowa(
	ID int check (ID > 0) primary key not null identity,
	Tytul varchar(100),
	ID_Autora int check (ID_Autora > 0),
	foreign key (ID_Autora) references Autor(ID) on delete cascade
);



create table Ksiazka(
	ID int check (ID > 0) primary key not null identity,
    Tytul varchar(100),
    ISBN varchar(20),
    Strony int check (Strony > 0),
    ID_Wydawcy int check (ID_Wydawcy > 0),
	ID_Autora int check (ID_Autora > 0),
	ID_Aktora int check (ID_Aktora > 0),
	ID_Slowo_Kluczowe int check (ID_Slowo_Kluczowe > 0),
    foreign key (ID_Wydawcy) references Wydawca(ID) on delete cascade,
	foreign key (ID_Autora) references Autor(ID) on delete cascade,
	foreign key (ID_Slowo_Kluczowe) references Slowo_Kluczowe(ID) on delete cascade
);

create table Czasopismo(
	ID int check (ID > 0) primary key not null identity,
    Tytul varchar(100),
    ISBN varchar(20),
    Strony int check (Strony > 0),
    ID_Wydawcy int check (ID_Wydawcy > 0),
	ID_Autora int check (ID_Autora > 0),
    foreign key (ID_Wydawcy) references Wydawca(ID) on delete cascade,
	foreign key (ID_Autora) references Autor(ID) on delete cascade

);

create table Wiadomosci(
	ID int check (ID > 0) primary key not null identity,
    Tresc varchar(max),
    ID_Czytelnika int check (ID_Czytelnika > 0),
	foreign key (ID_Czytelnika) references Czytelnik(ID) on delete cascade
);

create table Wypozyczenia_Ksiazki(
	ID int check (ID > 0) primary key not null identity,
    ID_Czytelnika int check (ID_Czytelnika > 0),
    ID_Ksiazki int check (ID_Ksiazki > 0),
    Data_Wypozyczenia datetime,
    Data_Zwrotu datetime,
	foreign key (ID_Czytelnika) references Czytelnik(ID) on delete cascade,
	foreign key (ID_Ksiazki) references Ksiazka(ID)on delete cascade
);

create table Wypozyczenia_Filmu(
	ID int check (ID > 0) primary key not null identity,
    ID_Czytelnika int check (ID_Czytelnika > 0),
    ID_Filmu int check (ID_Filmu > 0),
    Data_Wypozyczenia datetime,
    Data_Zwrotu datetime,
	foreign key (ID_Czytelnika) references Czytelnik(ID) on delete cascade,
    foreign key (ID_Filmu) references Film(ID) on delete cascade
    
);

create table Wypozyczenia_Czasopisma(
	ID int check (ID > 0) primary key not null identity,
    ID_Czytelnika int check (ID_Czytelnika > 0),
    ID_Czasopisma int check (ID_Czasopisma > 0),
    Data_Wypozyczenia datetime,
    Data_Zwrotu datetime,
    foreign key (ID_Czytelnika) references Czytelnik(ID) on delete cascade,
    foreign key (ID_Czasopisma) references Czasopismo(ID) on delete cascade
);


