CREATE DATABASE CuentaDeAhorros
GO

USE CuentaDeAhorros 
GO

--CREACION DE LAS TABLAS
CREATE TABLE dbo.Clientes  
   (Id int PRIMARY KEY NOT NULL,  
   NombreCliente nvarchar(100) NOT NULL,  
   EmailCliente nvarchar(100) NOT NULL)  
GO

CREATE TABLE dbo.CuentaDeAhorros 
   (Id int PRIMARY KEY NOT NULL,  
   MontoInicial DECIMAL(18,2) NOT NULL,  
   Saldo DECIMAL(18,2) NOT NULL,  
   ClienteCuenta int not null)
GO

CREATE TABLE dbo.Transacciones 
   (Id int PRIMARY KEY NOT NULL,  
   IdCuenta int NOT NULL,  
   TipoTransaccion nvarchar(20) NOT NULL,
   Monto DECIMAL(18,2) NOT NULL)
GO

--PROCEDIMIENTOS PARA CLIENTE
CREATE PROCEDURE [dbo].[SaveCliente]
(
@Id INT,
@NombreCliente NVARCHAR(100),
@EmailCliente NVARCHAR(100),
@ReturnCode NVARCHAR(20) OUTPUT
)
AS
BEGIN
    SET @ReturnCode = 'C200'
    IF(@Id <> 0)
    BEGIN
        IF EXISTS (SELECT 1 FROM Clientes WHERE EmailCliente = @EmailCliente AND Id <> @Id)
        BEGIN
            SET @ReturnCode = 'C201'
            RETURN
        END
        

        UPDATE Clientes SET
        NombreCliente = @NombreCliente,
        EmailCliente = @EmailCliente
        WHERE Id = @Id

        SET @ReturnCode = 'C200'
    END
    ELSE
    BEGIN
        IF EXISTS (SELECT 1 FROM Clientes WHERE EmailCliente = @EmailCliente)
        BEGIN
            SET @ReturnCode = 'C201'
            RETURN
        END

        INSERT INTO Clientes (NombreCliente,EmailCliente)
        VALUES (@NombreCliente,@EmailCliente)

        SET @ReturnCode = 'C200'
    END
END

CREATE PROCEDURE [dbo].[GetClientes]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Clientes(NOLOCK) ORDER BY Id ASC
END

CREATE PROCEDURE [dbo].[DeleteCliente]
(
@Id INT,
@ReturnCode NVARCHAR(20) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET @ReturnCode = 'C200'
    IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Id = @Id)
    BEGIN
        SET @ReturnCode ='C203'
        RETURN
    END
    ELSE
    BEGIN
        DELETE FROM Clientes WHERE Id = @Id
        SET @ReturnCode = 'C200'
        RETURN
    END
END

--PROCEDIMIENTOS PARA CUENTA

CREATE PROCEDURE[dbo].[AddCuenta]
(
  @MontoInicial decimal(18,2),
  @Saldo decimal(18,2),
  @ClienteCuenta int,
  @ReturnCode NVARCHAR(20) OUTPUT
)
AS
BEGIN
        IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Id = @ClienteCuenta)
        BEGIN
            SET @ReturnCode = 'C201'
            RETURN
        END

        INSERT INTO CuentasDeAhorro (MontoInicial,Saldo,ClienteCuenta)
        VALUES (@MontoInicial,@Saldo,@ClienteCuenta)

        SET @ReturnCode = 'C200'
END

CREATE PROCEDURE [dbo].[GetCuentas]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM CuentasDeAhorro (NOLOCK) ORDER BY Id ASC
END

--PROCEDIMIENTOS PARA TRANSACCIONES

ALTER PROCEDURE[dbo].[AddTransaccion]
(
 @IdCuenta int,
 @TipoTransaccion nvarchar(50),
 @Monto decimal(18,2),
 @ReturnCode NVARCHAR(20) OUTPUT
)
AS
BEGIN
        IF NOT EXISTS (SELECT 1 FROM CuentasDeAhorro WHERE Id = @IdCuenta)
        BEGIN
            SET @ReturnCode = 'C201'
            RETURN
        END

        INSERT INTO Transacciones (IdCuenta,TipoTransaccion,Monto)
        VALUES (@IdCuenta,@TipoTransaccion,@Monto)

		IF @TipoTransaccion = 'deposito'
		BEGIN
		    UPDATE CuentasDeAhorro SET
            Saldo = Saldo + @Monto
            WHERE Id = @IdCuenta
		END

		IF @TipoTransaccion = 'retiro'
		BEGIN
		    UPDATE CuentasDeAhorro SET
            Saldo = Saldo - @Monto
            WHERE Id = @IdCuenta
		END

        SET @ReturnCode = 'C200'
END

CREATE PROCEDURE [dbo].[GetTransacciones]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Transacciones(NOLOCK) ORDER BY Id ASC
END

