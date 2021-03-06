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
--add event_id to primary key
alter table tickets.ticketTypes
    alter column event_id int not null

alter table tickets.ticketTypes
    drop constraint ticketTypes_pk

alter table tickets.ticketTypes
    add constraint ticketTypes_pk
        primary key (id, event_id)
go
--ad count to ticketTypes table
alter table tickets.ticketTypes
    add count int
go
--add password_salt to users table
alter table tickets.users
    add password_salt varchar(64)
go

--change password hash and salt types to varbinary 
alter table tickets.users
    drop column password_hash
go

alter table tickets.users
    drop column password_salt
go

alter table tickets.users
    add password_hash varbinary(64)
go

alter table tickets.users
    add password_salt varbinary(64)
go

--add user type 
alter table tickets.users
    add type varchar(32)
go

--make password hash and salt not null 
alter table tickets.users
    alter column password_hash varbinary(64) not null
go

alter table tickets.users
    alter column password_salt varbinary(64) not null
go
--make email primary key
alter table tickets.users
    add constraint users_email_pk
        unique (email)
go
--change password and hash length to 512 
alter table tickets.users
    alter column password_salt varbinary(512) not null
go
--user type not null
alter table tickets.users
    alter column type varchar(32) not null
go
--add ticketTypes id to tickets table 
alter table tickets.tickets
    add ticketType_id int
go

alter table tickets.tickets
    drop constraint tickets_events_id_fk
go

alter table tickets.tickets
    add constraint tickets_ticketTypes_id_event_id_fk
        foreign key (ticketType_id, event_id) references tickets.ticketTypes
go
--add payment_id to tickets table
alter table tickets.tickets
    add payment_id int
go

alter table tickets.tickets
    add constraint tickets_payments_id_fk
        foreign key (payment_id) references tickets.payments
go
--drop unnecessary fields and add sum field in paymnts tables 
alter table tickets.payments
    drop constraint payments_events_id_fk
go

alter table tickets.payments
    drop constraint payments_users_id_fk
go
alter table tickets.payments
    drop column user_id
go

alter table tickets.payments
    drop column event_id
go

alter table tickets.payments
    add sum decimal(10, 2)
go
--enable cascade deletes
alter table tickets.eventArtists
    drop constraint eventArtists_artists_id_fk
go

alter table tickets.eventArtists
    add constraint eventArtists_artists_id_fk
        foreign key (artist_id) references tickets.artists
            on delete cascade
go

alter table tickets.eventArtists
    drop constraint eventArtists_events_id_fk
go

alter table tickets.eventArtists
    add constraint eventArtists_events_id_fk
        foreign key (event_id) references tickets.events
            on delete cascade
go

alter table tickets.eventGenres
    drop constraint eventGenres_events_id_fk
go

alter table tickets.eventGenres
    add constraint eventGenres_events_id_fk
        foreign key (event_id) references tickets.events
            on delete cascade
go

alter table tickets.eventGenres
    drop constraint eventGenres_genres_id_fk
go

alter table tickets.eventGenres
    add constraint eventGenres_genres_id_fk
        foreign key (genre_id) references tickets.genres
            on delete cascade
go


alter table tickets.ticketTypes
    drop constraint ticketTypes_events_id_fk
go

alter table tickets.ticketTypes
    add constraint ticketTypes_events_id_fk
        foreign key (event_id) references tickets.events
            on delete cascade
go

--add user id foreign key to payment table
alter table tickets.payments
    add user_id int
go

alter table tickets.payments
    add constraint payments_users_id_fk
        foreign key (user_id) references tickets.users
            on delete cascade
go

--add table for payment tokens
create table tickets.paymentTokens
(
    id    int identity
        constraint paymentTokens_pk
            primary key,
    token varchar(32)
)
go

create table tickets.table_name
(
    user_id  int
        constraint table_name_users_id_fk
            references tickets.users
            on delete cascade,
    token_id int
        constraint table_name_paymentTokens_id_fk
            references tickets.paymentTokens
            on delete cascade,
    constraint table_name_pk
        primary key (user_id, token_id)
)
go
-- rename table
exec sp_rename 'tickets.table_name', userTokens, 'OBJECT'
go

-- change transaction_id datatype to varachar 32
alter table tickets.payments
    alter column transaction_id varchar(32) null
go

-- add card data to paymentTokens table
alter table tickets.paymentTokens
    add card_brand varchar(50)
go

alter table tickets.paymentTokens
    add exp_month int
go

alter table tickets.paymentTokens
    add exp_year int
go

alter table tickets.paymentTokens
    add last4 int
go
-- add stripe customer id
alter table tickets.users
    add stripe_id varchar(32)
go


