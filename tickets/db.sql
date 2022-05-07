create database tickets;
use tickets
go

create schema tickets
go
-- events table
create table tickets.events
(
    id           int identity
        constraint events_pk
            primary key,
    type         varchar(50),
    place_id     int,
    tittle       varchar(50),
    date         datetime,
    description  varchar(max),
    preview_link varchar(2048),
    poster_link  varchar(2048)
)
go
-- event places table
create table tickets.places
(
    id     int identity
        constraint places_pk
            primary key,
    city   varchar(50),
    address varchar(255),
    tittle varchar(50)
)
go
-- foreign key for place_id
alter table tickets.events
    add constraint events_places_id_fk
        foreign key (place_id) references tickets.places
go
-- tickets tables
create table tickets.ticketTypes
(
    id          int identity
        constraint ticketTypes_pk
            primary key,
    event_id    int,
    tittle      varchar(50),
    description varchar(50),
    price       decimal(10, 2)
)
go
-- foreign key for event_id
alter table tickets.ticketTypes
    add constraint ticketTypes_events_id_fk
        foreign key (event_id) references tickets.events
go
-- table with artists
create table tickets.artists
(
    id          int identity
        constraint artists_pk
            primary key,
    name        varchar(50),
    description varchar(max)
)
go
-- events and artists reference table
create table tickets.eventArtists
(
    artist_id int
        constraint eventArtists_artists_id_fk
            references tickets.artists,
    event_id  int
        constraint eventArtists_events_id_fk
            references tickets.events,
    constraint eventArtists_pk
        primary key (artist_id, event_id)
)
go
-- genres table
create table tickets.genres
(
    id    int identity
        constraint genres_pk
            primary key,
    genre varchar(50)
)
go
-- events and genres references table
create table tickets.eventGenres
(
    event_id int
        constraint eventGenres_events_id_fk
            references tickets.events,
    genre_id int
        constraint eventGenres_genres_id_fk
            references tickets.genres,
    constraint eventGenres_pk
        primary key (event_id, genre_id)
)
go
-- users table 
create table tickets.users
(
    id            int identity
        constraint users_pk
            primary key,
    email         varchar(64),
    password_hash varchar(64),
    name          varchar(50),
    last_name     varchar(50),
    phone_number  varchar(15)
)
go
-- bought tickets table
create table tickets.tickets
(
    id       uniqueidentifier
        constraint tickets_pk
            primary key,
    event_id int
        constraint tickets_events_id_fk
            references tickets.events,
    user_id  int
        constraint tickets_users_id_fk
            references tickets.users
)
go
-- payments data table
create table tickets.payments
(
    id             int identity
        constraint payments_pk
            primary key,
    user_id        int
        constraint payments_users_id_fk
            references tickets.users,
    event_id       int
        constraint payments_events_id_fk
            references tickets.events,
    time           datetime,
    confirmed      tinyint default 0,
    transaction_id uniqueidentifier
)
go



