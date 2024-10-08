CREATE DATABASE videojuegosBD;
GO

USE videojuegosBD;
GO

CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,  
    Nombre NVARCHAR(100) NOT NULL,       
    CorreoElectronico NVARCHAR(255) NOT NULL UNIQUE,  
    Contrasena NVARCHAR(255) NOT NULL,     
    FechaActualizacion DATETIME DEFAULT GETDATE(), 
    UsuarioActualizacion NVARCHAR(100) 
);
GO

CREATE TABLE Videojuegos (
    IdVideojuego INT IDENTITY(1,1) PRIMARY KEY,   
    Nombre NVARCHAR(200) NOT NULL,           
    Compania NVARCHAR(150) NOT NULL,       
    AnioLanzamiento INT CHECK (AnioLanzamiento >= 1950 AND AnioLanzamiento <= YEAR(GETDATE())),
    Precio DECIMAL(10, 2) NOT NULL, 
    Puntaje DECIMAL(3, 2) DEFAULT 0.00 CHECK (Puntaje BETWEEN 0 AND 5), 
    FechaActualizacion DATETIME DEFAULT GETDATE()
    UsuarioActualizacion NVARCHAR(100) 
);
GO

CREATE TABLE Calificaciones (
    IdCalificacion UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY, 
    Nickname NVARCHAR(100) NOT NULL, 
    IdVideojuego INT NOT NULL, 
    Puntuacion DECIMAL(3, 2) NOT NULL CHECK (Puntuacion BETWEEN 0 AND 5),  
    FechaActualizacion DATETIME DEFAULT GETDATE(), 
    UsuarioActualizacion NVARCHAR(100),   
    CONSTRAINT FK_Calificaciones_Videojuegos FOREIGN KEY (IdVideojuego)
    REFERENCES Videojuegos(IdVideojuego) 
);
GO
