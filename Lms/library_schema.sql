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