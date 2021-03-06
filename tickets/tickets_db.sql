USE [master]
GO
/****** Object:  Database [tickets]    Script Date: 06.06.2022 23:57:49 ******/
CREATE DATABASE [tickets]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'tickets', FILENAME = N'/var/opt/mssql/data/tickets.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'tickets_log', FILENAME = N'/var/opt/mssql/data/tickets_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [tickets] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [tickets].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [tickets] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [tickets] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [tickets] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [tickets] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [tickets] SET ARITHABORT OFF 
GO
ALTER DATABASE [tickets] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [tickets] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [tickets] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [tickets] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [tickets] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [tickets] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [tickets] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [tickets] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [tickets] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [tickets] SET  ENABLE_BROKER 
GO
ALTER DATABASE [tickets] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [tickets] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [tickets] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [tickets] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [tickets] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [tickets] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [tickets] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [tickets] SET RECOVERY FULL 
GO
ALTER DATABASE [tickets] SET  MULTI_USER 
GO
ALTER DATABASE [tickets] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [tickets] SET DB_CHAINING OFF 
GO
ALTER DATABASE [tickets] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [tickets] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [tickets] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [tickets] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [tickets] SET QUERY_STORE = OFF
GO
USE [tickets]
GO
/****** Object:  Schema [tickets]    Script Date: 06.06.2022 23:57:49 ******/
CREATE SCHEMA [tickets]
GO
/****** Object:  Table [tickets].[events]    Script Date: 06.06.2022 23:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tickets].[events](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](50) NULL,
	[place_id] [int] NULL,
	[tittle] [varchar](50) NULL,
	[date] [datetime] NULL,
	[description] [varchar](max) NULL,
	[preview_link] [varchar](2048) NULL,
	[poster_link] [varchar](2048) NULL,
 CONSTRAINT [events_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [tickets].[places]    Script Date: 06.06.2022 23:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tickets].[places](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[city] [varchar](50) NULL,
	[address] [varchar](255) NULL,
	[tittle] [varchar](50) NULL,
 CONSTRAINT [places_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tickets].[ticketTypes]    Script Date: 06.06.2022 23:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tickets].[ticketTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[event_id] [int] NOT NULL,
	[tittle] [varchar](50) NULL,
	[description] [varchar](50) NULL,
	[price] [decimal](10, 2) NULL,
	[count] [int] NULL,
 CONSTRAINT [ticketTypes_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[event_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[getEvents]    Script Date: 06.06.2022 23:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[getEvents] as (
select e.tittle as Tittle, MIN(tp.price) as Price, p.city as City,
       p.tittle as Place, e.date as Date, e.preview_link as PreviewLink,
       e.poster_link as PosterLink from tickets.events e
           join tickets.ticketTypes tp on e.id=tp.event_id
                join tickets.places p on p.id=e.place_id
                    group by e.tittle, p.city, p.tittle, e.date, e.preview_link, e.poster_link)
GO
/****** Object:  Table [tickets].[artists]    Script Date: 06.06.2022 23:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tickets].[artists](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[description] [varchar](max) NULL,
 CONSTRAINT [artists_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [tickets].[eventArtists]    Script Date: 06.06.2022 23:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tickets].[eventArtists](
	[artist_id] [int] NOT NULL,
	[event_id] [int] NOT NULL,
 CONSTRAINT [eventArtists_pk] PRIMARY KEY CLUSTERED 
(
	[artist_id] ASC,
	[event_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tickets].[eventGenres]    Script Date: 06.06.2022 23:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tickets].[eventGenres](
	[event_id] [int] NOT NULL,
	[genre_id] [int] NOT NULL,
 CONSTRAINT [eventGenres_pk] PRIMARY KEY CLUSTERED 
(
	[event_id] ASC,
	[genre_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tickets].[genres]    Script Date: 06.06.2022 23:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tickets].[genres](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[genre] [varchar](50) NULL,
 CONSTRAINT [genres_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tickets].[payments]    Script Date: 06.06.2022 23:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tickets].[payments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[time] [datetime] NULL,
	[confirmed] [tinyint] NULL,
	[transaction_id] [varchar](32) NULL,
	[sum] [decimal](10, 2) NULL,
	[user_id] [int] NULL,
 CONSTRAINT [payments_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tickets].[paymentTokens]    Script Date: 06.06.2022 23:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tickets].[paymentTokens](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[token] [varchar](32) NULL,
	[card_brand] [varchar](50) NULL,
	[exp_month] [int] NULL,
	[exp_year] [int] NULL,
	[last4] [int] NULL,
 CONSTRAINT [paymentTokens_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tickets].[tickets]    Script Date: 06.06.2022 23:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tickets].[tickets](
	[id] [uniqueidentifier] NOT NULL,
	[event_id] [int] NULL,
	[user_id] [int] NULL,
	[ticketType_id] [int] NULL,
	[payment_id] [int] NULL,
 CONSTRAINT [tickets_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tickets].[users]    Script Date: 06.06.2022 23:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tickets].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](64) NULL,
	[name] [varchar](50) NULL,
	[last_name] [varchar](50) NULL,
	[phone_number] [varchar](15) NULL,
	[password_hash] [varbinary](64) NOT NULL,
	[password_salt] [varbinary](512) NOT NULL,
	[type] [varchar](32) NOT NULL,
	[stripe_id] [varchar](32) NULL,
 CONSTRAINT [users_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [users_email_pk] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [tickets].[userTokens]    Script Date: 06.06.2022 23:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [tickets].[userTokens](
	[user_id] [int] NOT NULL,
	[token_id] [int] NOT NULL,
 CONSTRAINT [table_name_pk] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[token_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [tickets].[payments] ADD  DEFAULT ((0)) FOR [confirmed]
GO
ALTER TABLE [tickets].[eventArtists]  WITH CHECK ADD  CONSTRAINT [eventArtists_artists_id_fk] FOREIGN KEY([artist_id])
REFERENCES [tickets].[artists] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [tickets].[eventArtists] CHECK CONSTRAINT [eventArtists_artists_id_fk]
GO
ALTER TABLE [tickets].[eventArtists]  WITH CHECK ADD  CONSTRAINT [eventArtists_events_id_fk] FOREIGN KEY([event_id])
REFERENCES [tickets].[events] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [tickets].[eventArtists] CHECK CONSTRAINT [eventArtists_events_id_fk]
GO
ALTER TABLE [tickets].[eventGenres]  WITH CHECK ADD  CONSTRAINT [eventGenres_events_id_fk] FOREIGN KEY([event_id])
REFERENCES [tickets].[events] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [tickets].[eventGenres] CHECK CONSTRAINT [eventGenres_events_id_fk]
GO
ALTER TABLE [tickets].[eventGenres]  WITH CHECK ADD  CONSTRAINT [eventGenres_genres_id_fk] FOREIGN KEY([genre_id])
REFERENCES [tickets].[genres] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [tickets].[eventGenres] CHECK CONSTRAINT [eventGenres_genres_id_fk]
GO
ALTER TABLE [tickets].[events]  WITH CHECK ADD  CONSTRAINT [events_places_id_fk] FOREIGN KEY([place_id])
REFERENCES [tickets].[places] ([id])
GO
ALTER TABLE [tickets].[events] CHECK CONSTRAINT [events_places_id_fk]
GO
ALTER TABLE [tickets].[payments]  WITH CHECK ADD  CONSTRAINT [payments_users_id_fk] FOREIGN KEY([user_id])
REFERENCES [tickets].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [tickets].[payments] CHECK CONSTRAINT [payments_users_id_fk]
GO
ALTER TABLE [tickets].[tickets]  WITH CHECK ADD  CONSTRAINT [tickets_payments_id_fk] FOREIGN KEY([payment_id])
REFERENCES [tickets].[payments] ([id])
GO
ALTER TABLE [tickets].[tickets] CHECK CONSTRAINT [tickets_payments_id_fk]
GO
ALTER TABLE [tickets].[tickets]  WITH CHECK ADD  CONSTRAINT [tickets_ticketTypes_id_event_id_fk] FOREIGN KEY([ticketType_id], [event_id])
REFERENCES [tickets].[ticketTypes] ([id], [event_id])
GO
ALTER TABLE [tickets].[tickets] CHECK CONSTRAINT [tickets_ticketTypes_id_event_id_fk]
GO
ALTER TABLE [tickets].[tickets]  WITH CHECK ADD  CONSTRAINT [tickets_users_id_fk] FOREIGN KEY([user_id])
REFERENCES [tickets].[users] ([id])
GO
ALTER TABLE [tickets].[tickets] CHECK CONSTRAINT [tickets_users_id_fk]
GO
ALTER TABLE [tickets].[ticketTypes]  WITH CHECK ADD  CONSTRAINT [ticketTypes_events_id_fk] FOREIGN KEY([event_id])
REFERENCES [tickets].[events] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [tickets].[ticketTypes] CHECK CONSTRAINT [ticketTypes_events_id_fk]
GO
ALTER TABLE [tickets].[userTokens]  WITH CHECK ADD  CONSTRAINT [table_name_paymentTokens_id_fk] FOREIGN KEY([token_id])
REFERENCES [tickets].[paymentTokens] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [tickets].[userTokens] CHECK CONSTRAINT [table_name_paymentTokens_id_fk]
GO
ALTER TABLE [tickets].[userTokens]  WITH CHECK ADD  CONSTRAINT [table_name_users_id_fk] FOREIGN KEY([user_id])
REFERENCES [tickets].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [tickets].[userTokens] CHECK CONSTRAINT [table_name_users_id_fk]
GO
USE [master]
GO
ALTER DATABASE [tickets] SET  READ_WRITE 
GO
