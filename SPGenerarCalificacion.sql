CREATE PROCEDURE GenerarCalificaciones
    @Cantidad INT = 0, -- Parámetro de entrada, valor por defecto es 0
    @CodigoError INT OUTPUT, -- Código de error como parámetro de salida
    @MensajeError NVARCHAR(4000) OUTPUT -- Mensaje de error como parámetro de salida
AS
BEGIN
    -- Manejo de errores
    BEGIN TRY
        -- Validar si la cantidad es mayor a 0
        IF @Cantidad <= 0
        BEGIN
            SET @CodigoError = 1;
            SET @MensajeError = 'El valor ingresado para la cantidad no es válido.';
            RETURN;
        END

        -- Variables locales
        DECLARE @i INT = 0;
        DECLARE @Nickname NVARCHAR(30);
        DECLARE @Puntuacion DECIMAL(3,2);
        DECLARE @VideojuegoId INT;

        -- Inicializar valores de salida en caso de éxito
        SET @CodigoError = 0;
        SET @MensajeError = NULL;

        -- Bucle para insertar las calificaciones solicitadas
        WHILE @i < @Cantidad
        BEGIN
            -- Generar Nickname aleatorio
            SET @Nickname = dbo.GenerarNicknameAleatorio();

            -- Generar Puntuación aleatoria entre 0 y 5 con dos decimales
            SET @Puntuacion = ROUND((RAND() * 5), 2);

            -- Seleccionar un VideojuegoId aleatorio
            SELECT TOP 1 @VideojuegoId = Id
            FROM Videojuegos
            ORDER BY NEWID();

            -- Insertar la calificación en la base de datos
            INSERT INTO Calificaciones (CalificacionId, Nickname, VideojuegoId, Puntuacion, FechaActualizacion, UsuarioActualizacion)
            VALUES (NEWID(), @Nickname, @VideojuegoId, @Puntuacion, GETDATE(), 'Sistema');

            -- Incrementar el contador
            SET @i = @i + 1;
        END

    END TRY
    BEGIN CATCH
        -- Capturar el error y establecer valores de salida
        SET @CodigoError = ERROR_NUMBER();
        SET @MensajeError = ERROR_MESSAGE();
    END CATCH;
END


