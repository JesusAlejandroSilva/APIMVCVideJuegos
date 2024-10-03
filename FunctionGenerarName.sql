CREATE FUNCTION dbo.GenerarNicknameAleatorio()
RETURNS NVARCHAR(30)
AS
BEGIN
    DECLARE @chars NVARCHAR(36) = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
    DECLARE @result NVARCHAR(30) = '';
    DECLARE @i INT = 0;

    WHILE @i < 30
    BEGIN
        -- Seleccionar un carácter aleatorio de la lista
        SET @result = @result + SUBSTRING(@chars, CONVERT(INT, RAND() * LEN(@chars) + 1), 1);
        SET @i = @i + 1;
    END

    RETURN @result;
END
