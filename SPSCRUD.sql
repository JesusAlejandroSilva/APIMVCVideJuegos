CREATE PROCEDURE sp_GetVideojuegoById
    @Id INT
AS
BEGIN
    SELECT Id, Nombre, Compania, AnioLanzamiento, Precio, Puntaje
    FROM Videojuegos
    WHERE Id = @Id;
END
CREATE PROCEDURE sp_CreateVideojuego
    @Nombre NVARCHAR(100),
    @Compania NVARCHAR(100),
    @Anio INT,
    @Precio DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Videojuegos (Nombre, Compania, AnioLanzamiento, Precio, Puntaje, FechaActualizacion, Usuario)
    VALUES (@Nombre, @Compania, @Anio, @Precio, 0, GETDATE(), SYSTEM_USER);

    SELECT SCOPE_IDENTITY(); -- Devuelve el ID generado
END
CREATE PROCEDURE sp_UpdateVideojuego
    @Id INT,
    @Nombre NVARCHAR(100),
    @Compania NVARCHAR(100),
    @Anio INT,
    @Precio DECIMAL(10,2)
AS
BEGIN
    UPDATE Videojuegos
    SET Nombre = @Nombre,
        Compania = @Compania,
        AnioLanzamiento = @Anio,
        Precio = @Precio,
        FechaActualizacion = GETDATE(),
        Usuario = SYSTEM_USER
    WHERE Id = @Id;
END
CREATE PROCEDURE sp_DeleteVideojuego
    @Id INT
AS
BEGIN
    DELETE FROM Videojuegos
    WHERE Id = @Id;
END
