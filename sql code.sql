USE CRUDWinform;

CREATE TABLE Employee
(
    EmployeeId INT PRIMARY KEY IDENTITY(1, 1),
    EmployeeName NVARCHAR(50) NOT NULL,
    EmployeeSalary NUMERIC(10, 0) NOT NULL,
    EmployeeCity NVARCHAR(20) NOT NULL
);

INSERT INTO dbo.Employee
(
    EmployeeName,
    EmployeeSalary,
    EmployeeCity
)
VALUES
(   N'Nguyen Ngu',  -- EmployeeName - nvarchar(50)
    10000000, -- EmployeeSalary - numeric(10, 0)
    N'Da Nang'   -- EmployeeCity - nvarchar(20)
    )
GO

ALTER PROC EmployeeCrud
    @EmployeeId INT,
    @EmployeeName NVARCHAR(50),
    @EmployeeSalary NUMERIC(10, 0),
    @EmployeeCity NVARCHAR(20),
    @OperationType INT

-- 1 insert
-- 2 update
-- 3 Delete
-- 4 Select
-- 5 Sellect all
AS
BEGIN
    SET NOCOUNT ON;

    IF @OperationType = 1
    BEGIN
        INSERT INTO dbo.Employee
        (
            EmployeeName,
            EmployeeSalary,
            EmployeeCity
        )
        VALUES
        (   
			@EmployeeName,   -- EmployeeName - nvarchar(50)
            @EmployeeSalary, -- EmployeeSalary - numeric(10, 0)
            @EmployeeCity    -- EmployeeCity - nvarchar(20)
        );
    END;
    ELSE IF @OperationType = 2
    BEGIN
        UPDATE dbo.Employee
        SET EmployeeName = @EmployeeName,
            EmployeeSalary = @EmployeeSalary,
            EmployeeCity = @EmployeeCity
		WHERE EmployeeId = @EmployeeId
    END
	ELSE IF @OperationType = 3
	BEGIN
	    DELETE FROM dbo.Employee WHERE EmployeeId = @EmployeeId
	END
	ELSE IF @OperationType = 4
	BEGIN
	    SELECT * FROM dbo.Employee WHERE EmployeeId= @EmployeeId
	END
	ELSE
	BEGIN
	    SELECT * FROM dbo.Employee
	END
END;
