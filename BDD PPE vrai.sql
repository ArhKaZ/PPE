DROP DATABASE IF exists Escp_Game; 
CREATE DATABASE Escp_Game;
USE Escp_Game;

CREATE TABLE Clients(
id INT Primary Key auto_increment, 
nom VARCHAR(50),
prenom VARCHAR(50),
telephone INT(10),
mail VARCHAR(80),
credit INT, 
dateNaissance DATE, 
photo VARCHAR(50),
NbPartie INT
)ENGINE INNODB;

CREATE TABLE Ville (
id INT primary key, 
nom VARCHAR(50)
)ENGINE INNODB;

CREATE TABLE Theme(
id INT Primary Key auto_increment, 
nom VARCHAR(120)
)ENGINE INNODB;

CREATE TABLE Salle (
id INT PRIMARY KEY, 
idLieu INT, 
idTheme int,
FOREIGN KEY (idLieu) REFERENCES Ville (id), 
FOREIGN KEY (idTheme) REFERENCES Theme (id)
)ENGINE INNODB;


CREATE TABLE Avis(
id INT Primary Key auto_increment, 
idClient INT, 
note INT, 
commentaire VARCHAR(255),
idTheme INT, 
FOREIGN KEY (idClient) REFERENCES Clients (id), 
FOREIGN KEY (idTheme) REFERENCES Theme (id)
)ENGINE INNODB;


CREATE TABLE Utilisateur (
id INT PRIMARY KEY, 
roleUser char(10), 
idVille INT,
identifiant VARCHAR(40),
mdp VARCHAR(30), 
FOREIGN KEY(idVille) REFERENCES Ville (id)
)ENGINE INNODB;


CREATE TABLE Obstacle(
id INT Primary Key auto_increment, 
nom VARCHAR(50),
photo VARCHAR(40), 
commentaire VARCHAR(120),
difficulte INT, 
prix INT, 
theme INT, 
FOREIGN KEY (theme) REFERENCES Theme (id)
)ENGINE INNODB; 


CREATE TABLE Reservation (
dateRes Datetime, 
id INT PRIMARY KEY,
idClient INT,
idSalle INT, 
prix INT,
idTechnicien INT, 
nbClient int, 
idTheme INT,
FOREIGN KEY (idClient) REFERENCES Clients (id),
FOREIGN KEY (idSalle) REFERENCES Salle (id),
FOREIGN KEY (idTheme) REFERENCES Theme (id),
FOREIGN KEY (idTechnicien) REFERENCES Utilisateur (id)
)ENGINE INNODB;

CREATE TABLE Transactions (
id INT PRIMARY KEY, 
operation CHAR(1), 
montant INT, 
reservation INT, 
idClient INT,
dateTransac DATETIME,
FOREIGN KEY (reservation) REFERENCES Reservation (id),
FOREIGN KEY (idClient) REFERENCES Clients (id)
)ENGINE INNODB;

CREATE TABLE Placement_obstacle(
num_placement INT,
reservation INT,
obstacle INT, 
PRIMARY KEY (num_placement, reservation, obstacle),
FOREIGN KEY (reservation) REFERENCES Reservation (id),
FOREIGN KEY (obstacle) REFERENCES Obstacle (id)
)ENGINE INNODB;

CREATE TABLE Date_ferie(
datepot DATE, 
type_date CHAR(1),
PRIMARY KEY (datepot, type_date)
);


#------------------INSERT------------------#

insert into Clients VALUES (1, 'feige', 'hugo', '0606060606', "hugo.feige@saintmichelannecy.fr", 0, '2000-05-05',"image.jpg",0);
insert into Clients VALUES (2, 'luiset', 'sylvain', '0706060606', "sylvain.luiset@saintmichelannecy.fr", 200, '2000-07-07',"image.jpg",12);
insert into Clients VALUES (3, 'girard', 'antoine', '0806060606', "antoine.girard@saintmichelannecy.fr", 400, '2000-08-08',"image.jpg",5);

insert into Theme values (1, "Noel"  );
insert into Theme values (2, "Halloween" );
insert into Theme values (3, "Lac d'Annecy"  );

insert into Obstacle (id, nom, photo, commentaire,difficulte, prix, theme ) VALUES (1, 'puzzle',"image.jpg", "pjpjpjpjpjpjpjpj", 3,12,1 );
insert into Obstacle (id, nom, photo, commentaire,difficulte, prix, theme ) VALUES (2, 'cadena numérique',"image.jpg", "pjpjpjpjpjpjpjpj", 5,35,2 );
insert into Obstacle (id, nom, photo, commentaire,difficulte, prix, theme ) VALUES (3, 'rébus',"image.jpg", "pjpjpjpjpjpjpjpj", 1,5,3 );

insert into Avis VALUES (1,1,5, "Super comme obstacle",1);
insert into Avis VALUES (2,2,1, "nul nul nul",2);
insert into Avis VALUES (3,1,3, "en sah, ça va",3);

insert into Ville VALUES (1, "Annecy");
insert into Ville VALUES (2, "Chamonix");
insert into Ville VALUES (3, "Thonon-les-bains");

insert into Utilisateur values (1, "admin", 1, "...", "...");
insert into Utilisateur values (2, "reposable", 2, "...", "...");
insert into Utilisateur values (3, "client", 3, "...", "...");

insert into Salle values (1,1,1);
insert into Salle values (2,2,2);
insert into Salle values (3,3,3);

insert into Reservation values ("2020-06-06", 1, 1, 2, 30, 1, 5, 2);
insert into Reservation values ("2020-10-10", 2, 3, 1, 45, 1, 8, 1);
insert into Reservation values ("2020-11-11", 3, 2, 3, 15, 2, 3, 3);

insert into Transactions values (1,"D", 30, 1, 1, '2020-11-13');
insert into Transactions values (2,"C", 45, 2, 3,'2020-11-13');
insert into Transactions values (3,"C", 15, 3, 2,'2020-11-13');

insert into Placement_obstacle values (1,1,1);
insert into Placement_obstacle values (2,2,2);
insert into Placement_obstacle values (3,3,3);

insert into Date_ferie values ('2000-05-05', 'F');
insert into Date_ferie values ('2000-12-12', 'F');
insert into Date_ferie values ('2000-09-11', 'V');

select * from Clients;
select * from Theme;
select * from Obstacle;
select * from Avis;
select * from Clients;
select * from Ville;
select * from Utilisateur;
select * from Salle;
select * from Reservation;
select * from Transactions;
select * from Placement_obstacle;
select * from Date_ferie;