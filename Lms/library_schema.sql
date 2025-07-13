CREATE DATABASE LMSLiteDb;
USE LMSLiteDb;
CREATE TABLE BookMaster (
    BookId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Author NVARCHAR(100) NOT NULL,
    ISBN NVARCHAR(20) NOT NULL,
    TotalCopies INT NOT NULL,
    AvailableCopies INT NOT NULL
);

CREATE TABLE MemberMaster (
    MemberId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Mobile NVARCHAR(15) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE BookIssue (
    IssueId INT IDENTITY(1,1) PRIMARY KEY,
    BookId INT NOT NULL FOREIGN KEY REFERENCES BookMaster(BookId),
    MemberId INT NOT NULL FOREIGN KEY REFERENCES MemberMaster(MemberId),
    IssueDate DATE NOT NULL,
    DueDate DATE NOT NULL,
    ReturnDate DATE NULL,
    FineAmount DECIMAL(10, 2) NULL
);

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(200) NOT NULL,
	Role NVARCHAR(20) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

--Seeding Data
--================================================================
-- BookMaster
INSERT INTO BookMaster(Title, Author, ISBN, TotalCopies, AvailableCopies)
VALUES 
('Wings of Fire', 'A.P.J. Abdul Kalam', '9788173711466', 10, 10),
('The White Tiger', 'Aravind Adiga', '9788172238476', 6, 6),
('India After Gandhi', 'Ramachandra Guha', '9780330505543', 5, 5),
('Train to Pakistan', 'Khushwant Singh', '9780143065883', 7, 7),
('The Discovery of India', 'Jawaharlal Nehru', '9780143031031', 9, 9),
('The Guide', 'R.K. Narayan', '9788185986173', 6, 6),
('Gitanjali', 'Rabindranath Tagore', '9788171676767', 5, 5),
('Interpreter of Maladies', 'Jhumpa Lahiri', '9780395927205', 8, 8),
('The Namesake', 'Jhumpa Lahiri', '9780006551805', 10, 10),
('A Suitable Boy', 'Vikram Seth', '9780060786526', 4, 4);


-- MemberMaster
INSERT INTO MemberMaster(FullName, Mobile, Email, IsActive)
VALUES 
('Rahul Sharma',      '9876543210', 'rahul.sharma@example.com', 1),
('Priya Mehta',       '9823456789', 'priya.mehta@example.com', 1),
('Vikram Joshi',      '9812345678', 'vikram.joshi@example.com', 1),
('Anjali Patel',      '9898989898', 'anjali.patel@example.com', 1),
('Suresh Reddy',      '9934567890', 'suresh.reddy@example.com', 1),
('Neha Kulkarni',     '9765432109', 'neha.kulkarni@example.com', 1),
('Amitabh Verma',     '9741234567', 'amitabh.verma@example.com', 1),
('Kavita Nair',       '9687456321', 'kavita.nair@example.com', 1),
('Manoj Chauhan',     '9666666666', 'manoj.chauhan@example.com', 1),
('Divya Iyer',        '9878901234', 'divya.iyer@example.com', 1);

-- User
INSERT INTO Users (Username, Password, Role, IsActive)
VALUES 
('RavindraWadile', '@RavindraWadile', 'Admin', 1),
('PramodAdmin',     '@PramodAdmin', 'Admin', 1);

GO
CREATE PROCEDURE sp_GetDashboardData
AS
BEGIN
    SELECT 
        (SELECT COUNT(*) FROM MemberMaster) AS TotalMembers,
        (SELECT COUNT(*) FROM BookMaster) AS TotalBooks,
        (SELECT COUNT(*) FROM BookIssue) AS IssuedBooks
END
--user Master
    --Written directly SQL query to manager user master
-- Book Master
--sp_AddBook
GO
CREATE PROCEDURE sp_AddBook
    @Title NVARCHAR(200),
    @Author NVARCHAR(100),
    @ISBN NVARCHAR(20),
    @TotalCopies INT,
    @AvailableCopies INT,
    @Category NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO BookMaster (Title, Author, ISBN, TotalCopies, AvailableCopies, Category)
    VALUES (@Title, @Author, @ISBN, @TotalCopies, @AvailableCopies, @Category);

    SELECT SCOPE_IDENTITY();
END

--sp_UpdateBook
GO
CREATE PROCEDURE sp_UpdateBook
    @BookId INT,
    @Title NVARCHAR(200),
    @Author NVARCHAR(100),
    @ISBN NVARCHAR(20),
    @TotalCopies INT,
    @AvailableCopies INT,
    @Category NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE BookMaster
    SET Title = @Title,
        Author = @Author,
        ISBN = @ISBN,
        TotalCopies = @TotalCopies,
        AvailableCopies = @AvailableCopies,
        Category = @Category
    WHERE BookId = @BookId;
END

--sp_DeleteBook
GO
CREATE PROCEDURE sp_DeleteBook
    @BookId INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM BookMaster WHERE BookId = @BookId;
END
-- sp_GetAllBooks
CREATE PROCEDURE sp_GetAllBooks
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        BookId, Title, Author, ISBN, TotalCopies, AvailableCopies, Category
    FROM BookMaster;
END
GO
-- sp_GetBookById
CREATE PROCEDURE sp_GetBookById
    @BookId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        BookId, Title, Author, ISBN, TotalCopies, AvailableCopies, Category
    FROM BookMaster
    WHERE BookId = @BookId;
END
GO

--Member Master
--sp_GetAllMembers
Go
CREATE PROCEDURE sp_GetAllMembers
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        MemberId,
        FullName,
        Mobile,
        Email,
        IsActive
    FROM 
        MemberMaster
    ORDER BY 
        FullName;
END;

--sp_GetMemberById
GO
CREATE PROCEDURE sp_GetMemberById
    @MemberId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        MemberId,
        FullName,
        Mobile,
        Email,
        IsActive
    FROM 
        MemberMaster
    WHERE 
        MemberId = @MemberId;
END


--sp_UpdateMember
GO
CREATE PROCEDURE sp_UpdateMember
    @MemberId INT,
    @FullName NVARCHAR(100),
    @Mobile NVARCHAR(15),
    @Email NVARCHAR(100),
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE MemberMaster
    SET
        FullName = @FullName,
        Mobile = @Mobile,
        Email = @Email,
        IsActive = @IsActive
    WHERE
        MemberId = @MemberId;
END

--sp_AddMember
GO
CREATE PROCEDURE sp_AddMember
    @FullName NVARCHAR(100),
    @Mobile NVARCHAR(15),
    @Email NVARCHAR(100),
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO MemberMaster (FullName, Mobile, Email, IsActive)
    VALUES (@FullName, @Mobile, @Email, @IsActive);

    SELECT SCOPE_IDENTITY() AS NewMemberId;
END


--sp_DeleteMember
GO
CREATE PROCEDURE sp_DeleteMember
    @MemberId INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM MemberMaster WHERE MemberId = @MemberId;
END

--sp_IssueBook
GO
CREATE PROCEDURE sp_IssueBook
    @BookId INT,
    @MemberId INT,
    @ResultMessage NVARCHAR(200) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @AvailableCopies INT;

        SELECT @AvailableCopies = AvailableCopies
        FROM BookMaster
        WHERE BookId = @BookId;

        IF @AvailableCopies IS NULL
        BEGIN
            SET @ResultMessage = 'Book not found.';
            ROLLBACK;
            RETURN;
        END

        IF @AvailableCopies <= 0
        BEGIN
            SET @ResultMessage = 'Book is not available.';
            ROLLBACK;
            RETURN;
        END

        -- Insert new issue
        INSERT INTO BookIssue (BookId, MemberId, IssueDate, DueDate)
        VALUES (@BookId, @MemberId, GETDATE(), DATEADD(DAY, 7, GETDATE()));

        -- Update book stock
        UPDATE BookMaster
        SET AvailableCopies = AvailableCopies - 1
        WHERE BookId = @BookId;

        COMMIT;
        SET @ResultMessage = 'Book issued successfully.';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        SET @ResultMessage = 'An error occurred during book issue.';
    END CATCH
END
GO

--sp_ReturnBook

CREATE PROCEDURE sp_ReturnBook
    @IssueId INT,
    @ResultMessage NVARCHAR(200) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE 
            @BookId INT,
            @DueDate DATE,
            @ReturnDate DATE = GETDATE(),
            @Fine DECIMAL(10,2) = 0;

        SELECT @BookId = BookId, @DueDate = DueDate
        FROM BookIssue
        WHERE IssueId = @IssueId AND ReturnDate IS NULL;

        IF @BookId IS NULL
        BEGIN
            SET @ResultMessage = 'No valid issue found or book already returned.';
            ROLLBACK;
            RETURN;
        END

        -- Calculate fine if overdue
        IF @ReturnDate > @DueDate
        BEGIN
            SET @Fine = DATEDIFF(DAY, @DueDate, @ReturnDate) * 10;
        END

        -- Update return details
        UPDATE BookIssue
        SET ReturnDate = @ReturnDate,
            FineAmount = @Fine
        WHERE IssueId = @IssueId;

        -- Increment available copies
        UPDATE BookMaster
        SET AvailableCopies = AvailableCopies + 1
        WHERE BookId = @BookId;

        COMMIT;
        SET @ResultMessage = 'Book returned successfully.';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        SET @ResultMessage = 'An error occurred during book return.';
    END CATCH
END
GO
--Dashboard
--sp_Report_BookMasterIssuedLast7Days
GO
CREATE PROCEDURE sp_Report_BookMasterIssuedLast7Days
AS
BEGIN
    SELECT 
        DATENAME(weekday, IssueDate) AS DayOfWeek,
        COUNT(*) AS IssuedCount
    FROM BookIssue
    WHERE IssueDate >= DATEADD(DAY, -6, CAST(GETDATE() AS DATE))
    GROUP BY DATENAME(weekday, IssueDate)
END

--sp_Report_BookMasterIssuedByCategory
GO
CREATE PROCEDURE sp_Report_BookMasterIssuedByCategory
AS
BEGIN
    WITH TotalBooksPerCategory AS (
        SELECT 
            Category,
            SUM(TotalCopies) AS TotalBooks
        FROM BookMaster
        GROUP BY Category
    ),
    BooksIssuedPerCategory AS (
        SELECT 
            b.Category,
            COUNT(*) AS BooksIssued
        FROM BookIssue bi
        INNER JOIN BookMaster b ON bi.BookId = b.BookId
        GROUP BY b.Category
    )

    SELECT 
        tb.Category,
        ISNULL(bi.BooksIssued, 0) AS BooksIssued,
        tb.TotalBooks,
        CAST(ISNULL(bi.BooksIssued, 0) * 100.0 / NULLIF(tb.TotalBooks, 0) AS DECIMAL(5, 2)) AS Percentage
    FROM TotalBooksPerCategory tb
    LEFT JOIN BooksIssuedPerCategory bi ON tb.Category = bi.Category
    ORDER BY tb.Category
END
--sp_GetDashboardStats
GO
CREATE OR ALTER PROCEDURE sp_GetDashboardStats
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        ISNULL(SUM(B.TotalCopies), 0) AS TotalBooks,
        ISNULL(SUM(B.AvailableCopies), 0) AS AvailableBooks,
        ISNULL(COUNT(I.IssueId), 0) AS IssuedBooks,
        ISNULL(SUM(CASE 
                    WHEN I.ReturnDate IS NULL AND I.DueDate < GETDATE() THEN 1 
                    ELSE 0 
                  END), 0) AS OverdueBooks
    FROM BookMaster AS B
    LEFT JOIN BookIssue AS I ON B.BookId = I.BookId;
END;


--sp_GetUnreturnedIssues
GO
CREATE PROCEDURE sp_GetUnreturnedIssues
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        bi.IssueId,
        bi.BookId,
        bi.MemberId,
        bi.IssueDate,
        bi.DueDate,
        b.Title AS BookTitle,
        m.FullName AS MemberName
    FROM BookIssue bi
    INNER JOIN BookMaster b ON bi.BookId = b.BookId
    INNER JOIN MemberMaster m ON bi.MemberId = m.MemberId
    WHERE bi.ReturnDate IS NULL
END

--sp_ReportOverdueBookMaster
Go
CREATE PROCEDURE sp_ReportOverdueBookMaster
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        i.IssueId,
        b.Title AS BookTitle,
        m.FullName AS MemberName,
        i.IssueDate,
        i.DueDate,
        DATEDIFF(DAY, i.DueDate, GETDATE()) AS DaysOverdue
    FROM BookIssue i
    INNER JOIN BookMaster b ON i.BookId = b.BookId
    INNER JOIN MemberMaster m ON i.MemberId = m.MemberId
    WHERE i.ReturnDate IS NULL AND i.DueDate < GETDATE()
    ORDER BY i.DueDate ASC
END

--sp_Report_BookHistory
GO
CREATE PROCEDURE sp_Report_BookHistory
    @BookId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        i.IssueId,
        m.FullName AS MemberName,
        i.IssueDate,
        i.DueDate,
        i.ReturnDate,
        i.FineAmount
    FROM BookIssue i
    INNER JOIN MemberMaster m ON i.MemberId = m.MemberId
    WHERE i.BookId = @BookId
    ORDER BY i.IssueDate DESC;
END
-- sp_Report_BooksIssuedLast7Days
CREATE PROCEDURE sp_Report_BooksIssuedLast7Days
AS
BEGIN
    SELECT 
        DATENAME(weekday, IssueDate) AS DayOfWeek,
        COUNT(*) AS IssuedCount
    FROM BookIssue
    WHERE IssueDate >= DATEADD(DAY, -6, CAST(GETDATE() AS DATE))
    GROUP BY DATENAME(weekday, IssueDate)
END
GO
