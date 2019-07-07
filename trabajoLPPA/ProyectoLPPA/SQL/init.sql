create database lppa
go

use lppa

go
CREATE TABLE [dbo].[Bitacora] (
    [id]        INT            IDENTITY (1, 1) NOT NULL,
    [mensaje]   NVARCHAR (MAX) NOT NULL,
    [idUsuario] INT            DEFAULT ((0)) NOT NULL,
    [fecha]     DATETIME       DEFAULT (getdate()) NOT NULL,
    [dvh]       NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[Detalle_Rutina] (
    [IdRutina]  INT            NOT NULL,
    [Detalle]   NVARCHAR (MAX) NOT NULL,
    [IdMaquina] BIGINT         NOT NULL
);

CREATE TABLE [dbo].[DIGITO_VERTICAL] (
    [DV_ID]               BIGINT       IDENTITY (1, 1) NOT NULL,
    [DV_NOMBRE_TABLA]     VARCHAR (50) NOT NULL,
    [DV_DIGITO_CALCULADO] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([DV_ID] ASC)
);


CREATE TABLE [dbo].[FAMILIA] (
    [FAM_ID]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [FAM_NOMBRE]    NVARCHAR (300) NOT NULL,
    [FAM_BLOQUEADA] BIT            DEFAULT ((0)) NOT NULL,
    [FAM_DVH]       VARCHAR (50)   NOT NULL,
    PRIMARY KEY CLUSTERED ([FAM_ID] ASC)
);




CREATE TABLE [dbo].[Maquina] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [Nombre] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[PATENTE] (
    [PAT_ID]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [PAT_DESC] NVARCHAR (300) NOT NULL,
    [PAT_DVH]  VARCHAR (50)   NOT NULL,
    PRIMARY KEY CLUSTERED ([PAT_ID] ASC)
);

CREATE TABLE [dbo].[FAMILIA_PATENTE] (
    [FP_PATENTE_ID] BIGINT       NULL,
    [FP_FAMILIA_ID] BIGINT       NULL,
    [FP_DVH]        VARCHAR (50) DEFAULT ('') NOT NULL,
    FOREIGN KEY ([FP_PATENTE_ID]) REFERENCES [dbo].[PATENTE] ([PAT_ID]),
    FOREIGN KEY ([FP_FAMILIA_ID]) REFERENCES [dbo].[FAMILIA] ([FAM_ID])
);

CREATE TABLE [dbo].[Rutina] (
    [Id]      INT          IDENTITY (1, 1) NOT NULL,
    [IdSocio] BIGINT       NOT NULL,
    [Dia]     NVARCHAR (5) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Trainer_Socio] (
    [IdTrainer] INT    NOT NULL,
    [IdSocio]   BIGINT NOT NULL,
    PRIMARY KEY CLUSTERED ([IdTrainer] ASC)
);

CREATE TABLE [dbo].[Usuario] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]   NVARCHAR (100) NOT NULL,
    [Apellido] NVARCHAR (100) NOT NULL,
    [Email]    NVARCHAR (100) NOT NULL,
    [Tipo]     NCHAR (10)     NOT NULL,
    [Alias]    NVARCHAR (MAX) NOT NULL,
    [Password] NVARCHAR (MAX) DEFAULT ((1234)) NOT NULL,
    [Intentos] INT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[USUARIO_FAMILIA] (
    [UF_USUARIO_ID] INT          NULL,
    [UF_FAMILIA_ID] BIGINT       NULL,
    [UF_DVH]        VARCHAR (50) DEFAULT ('') NOT NULL,
    FOREIGN KEY ([UF_USUARIO_ID]) REFERENCES [dbo].[Usuario] ([Id]),
    FOREIGN KEY ([UF_FAMILIA_ID]) REFERENCES [dbo].[FAMILIA] ([FAM_ID])
);

CREATE TABLE [dbo].[USUARIO_PATENTE] (
    [UP_USUARIO_ID] INT          NULL,
    [UP_PATENTE_ID] BIGINT       NULL,
    [UP_DVH]        VARCHAR (50) NOT NULL,
    [UP_BLOQUEADA]  BIT          DEFAULT ((0)) NULL,
    FOREIGN KEY ([UP_USUARIO_ID]) REFERENCES [dbo].[Usuario] ([Id]),
    FOREIGN KEY ([UP_PATENTE_ID]) REFERENCES [dbo].[PATENTE] ([PAT_ID])
);

SET IDENTITY_INSERT [dbo].[Usuario] ON
INSERT INTO [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Tipo], [Alias], [Password], [Intentos]) VALUES (1, N'Cliente', N'Cliente', N'cliente@mail.com', N'S         ', N'Cliente', N'81dc9bdb52d04dc20036dbd8313ed055', 3)
INSERT INTO [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Tipo], [Alias], [Password], [Intentos]) VALUES (2, N'Cliente2', N'Cliente2', N'cliente2@mail.com', N'S         ', N'Cliente2', N'81dc9bdb52d04dc20036dbd8313ed055', 0)
INSERT INTO [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Tipo], [Alias], [Password], [Intentos]) VALUES (3, N'Trainer', N'Trainer', N'trainer@mail.com', N'T         ', N'Trainer', N'81dc9bdb52d04dc20036dbd8313ed055', 0)
INSERT INTO [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Tipo], [Alias], [Password], [Intentos]) VALUES (4, N'Admin', N'Admin', N'admin@mail.com', N'A         ', N'Admin', N'21232f297a57a5a743894a0e4a801fc3', 0)
SET IDENTITY_INSERT [dbo].[Usuario] OFF

INSERT INTO [dbo].[Trainer_Socio] ([IdTrainer], [IdSocio]) VALUES (1, 3)
INSERT INTO [dbo].[Trainer_Socio] ([IdTrainer], [IdSocio]) VALUES (2, 3)

SET IDENTITY_INSERT [dbo].[Maquina] ON
INSERT INTO [dbo].[Maquina] ([Id], [Nombre]) VALUES (1, N'Prensa')
INSERT INTO [dbo].[Maquina] ([Id], [Nombre]) VALUES (2, N'Mancuernas')
INSERT INTO [dbo].[Maquina] ([Id], [Nombre]) VALUES (3, N'Cinta')
INSERT INTO [dbo].[Maquina] ([Id], [Nombre]) VALUES (4, N'Banco Abdominal')
INSERT INTO [dbo].[Maquina] ([Id], [Nombre]) VALUES (5, N'Curl')
INSERT INTO [dbo].[Maquina] ([Id], [Nombre]) VALUES (6, N'Prensa Olimpica')
INSERT INTO [dbo].[Maquina] ([Id], [Nombre]) VALUES (7, N'Poleas')
SET IDENTITY_INSERT [dbo].[Maquina] OFF

SET IDENTITY_INSERT [dbo].[Rutina] ON
INSERT INTO [dbo].[Rutina] ([Id], [IdSocio], [Dia]) VALUES (1, 1, N'L')
INSERT INTO [dbo].[Rutina] ([Id], [IdSocio], [Dia]) VALUES (2, 1, N'MI')
INSERT INTO [dbo].[Rutina] ([Id], [IdSocio], [Dia]) VALUES (3, 1, N'V')
INSERT INTO [dbo].[Rutina] ([Id], [IdSocio], [Dia]) VALUES (4, 2, N'M')
INSERT INTO [dbo].[Rutina] ([Id], [IdSocio], [Dia]) VALUES (5, 2, N'J')
SET IDENTITY_INSERT [dbo].[Rutina] OFF


SET IDENTITY_INSERT [dbo].[DIGITO_VERTICAL] ON
INSERT INTO [dbo].[DIGITO_VERTICAL] ([DV_ID], [DV_NOMBRE_TABLA], [DV_DIGITO_CALCULADO]) VALUES (1, N'BITACORA', N'D0E40375FC026D0DFE38CE39094C9F46')
SET IDENTITY_INSERT [dbo].[DIGITO_VERTICAL] OFF

INSERT INTO [dbo].[Detalle_Rutina] ([IdRutina], [Detalle], [IdMaquina]) VALUES (1, N'Detalle Rutina', 1)
INSERT INTO [dbo].[Detalle_Rutina] ([IdRutina], [Detalle], [IdMaquina]) VALUES (1, N'Detalle Rutina', 2)
INSERT INTO [dbo].[Detalle_Rutina] ([IdRutina], [Detalle], [IdMaquina]) VALUES (1, N'Detalle Rutina', 3)
INSERT INTO [dbo].[Detalle_Rutina] ([IdRutina], [Detalle], [IdMaquina]) VALUES (2, N'Detalle Rutina', 3)
INSERT INTO [dbo].[Detalle_Rutina] ([IdRutina], [Detalle], [IdMaquina]) VALUES (2, N'Detalle Rutina', 4)
INSERT INTO [dbo].[Detalle_Rutina] ([IdRutina], [Detalle], [IdMaquina]) VALUES (3, N'Detalle Rutina', 5)
INSERT INTO [dbo].[Detalle_Rutina] ([IdRutina], [Detalle], [IdMaquina]) VALUES (3, N'Detalle Rutina', 6)
INSERT INTO [dbo].[Detalle_Rutina] ([IdRutina], [Detalle], [IdMaquina]) VALUES (4, N'Detalle Rutina', 5)
INSERT INTO [dbo].[Detalle_Rutina] ([IdRutina], [Detalle], [IdMaquina]) VALUES (1, N'Detalle Rutina', 6)
INSERT INTO [dbo].[Detalle_Rutina] ([IdRutina], [Detalle], [IdMaquina]) VALUES (5, N'Detalle Rutina', 7)
INSERT INTO [dbo].[Detalle_Rutina] ([IdRutina], [Detalle], [IdMaquina]) VALUES (5, N'Detalle Rutina', 1)

SET IDENTITY_INSERT [dbo].[Bitacora] ON
INSERT INTO [dbo].[Bitacora] ([id], [mensaje], [idUsuario], [fecha], [dvh]) VALUES (1, N'Falló la integridad de datos en digito vertical en el row 1', 0, N'2019-07-06 22:22:14', N'624D5A473E16F6F6BA9C1CCD0C9959FF')
INSERT INTO [dbo].[Bitacora] ([id], [mensaje], [idUsuario], [fecha], [dvh]) VALUES (2, N'Se deslogueo Admin que tiene el tipo A', 4, N'2019-07-06 22:22:14', N'0AC664B6AEA46E9D68E7389B8F12B168')
INSERT INTO [dbo].[Bitacora] ([id], [mensaje], [idUsuario], [fecha], [dvh]) VALUES (3, N'Se logueo Admin que tiene el tipo A', 0, N'2019-07-06 22:22:24', N'F53EF6AF47D800925C1DA071EA1AC2B0')
INSERT INTO [dbo].[Bitacora] ([id], [mensaje], [idUsuario], [fecha], [dvh]) VALUES (5, N'Falló la integridad de datos en bitácora en el id 4', 0, N'2019-07-06 22:23:24', N'93ACF8BF9E17EEDD97B668187681A9A1')
INSERT INTO [dbo].[Bitacora] ([id], [mensaje], [idUsuario], [fecha], [dvh]) VALUES (7, N'Falló la integridad de datos en bitácora en el id 4', 0, N'2019-07-06 22:31:27', N'E5C9ACD3EEAEDDB7A0EEBF27FE2E08DF')
INSERT INTO [dbo].[Bitacora] ([id], [mensaje], [idUsuario], [fecha], [dvh]) VALUES (8, N'Falló la integridad de datos en digito vertical en el row 1', 0, N'2019-07-06 22:31:27', N'83728A507C72B22E0F6561863B87E70A')
INSERT INTO [dbo].[Bitacora] ([id], [mensaje], [idUsuario], [fecha], [dvh]) VALUES (9, N'Falló la integridad de datos en digito vertical en el row 1', 0, N'2019-07-06 22:31:47', N'1A738EDCFEFF622DA472EE66ABFF234C')
INSERT INTO [dbo].[Bitacora] ([id], [mensaje], [idUsuario], [fecha], [dvh]) VALUES (10, N'Se deslogueo Admin que tiene el tipo A', 4, N'2019-07-06 22:31:47', N'58C95F4BA517090E55B56B77D22436D7')
INSERT INTO [dbo].[Bitacora] ([id], [mensaje], [idUsuario], [fecha], [dvh]) VALUES (11, N'Se logueo Admin que tiene el tipo A', 0, N'2019-07-06 22:31:52', N'BEAD9076B3825C4362B93772B449964E')
INSERT INTO [dbo].[Bitacora] ([id], [mensaje], [idUsuario], [fecha], [dvh]) VALUES (12, N'Se realizó un backup', 0, N'2019-07-06 22:31:54', N'4BBB34355F6170BFDF8A912F1DC9961C')
INSERT INTO [dbo].[Bitacora] ([id], [mensaje], [idUsuario], [fecha], [dvh]) VALUES (13, N'Se realizó un backup', 0, N'2019-07-06 22:32:03', N'EB442B45A62F8475A7D842D5F22C2057')
INSERT INTO [dbo].[Bitacora] ([id], [mensaje], [idUsuario], [fecha], [dvh]) VALUES (14, N'Falló la integridad de datos en bitácora en el id 6', 0, N'2019-07-06 22:32:21', N'D448E318A33D2A21344B48D102448E6F')
SET IDENTITY_INSERT [dbo].[Bitacora] OFF

go

  create  view vistaBitacora as 
  select bi.fecha,isNull(usu.alias,'system') as alias , bi.mensaje from BITACORA bi
  left join usuario usu on usu.id = bi.idUsuario