Create DATABASE AlumniRdsDb

use AlumniRdsDb;

create table UserPrivacySetting
(
    userPrivacySettingId INT IDENTITY(1,1) PRIMARY KEY,
    displayName varchar(50),
    settingCode varchar(50) UNIQUE,
    createdDateTime DATETIME not null DEFAULT GETDATE(),

)

CREATE TABLE [User]
(
    userId INT IDENTITY(1,1) PRIMARY KEY,
    firstName varchar(50),
    lastName varchar(50),
    emailAddress varchar(50) not null,
    password varchar(50) not null,
    isVerified bit,
    createdDateTime DATETIME not null DEFAULT GETDATE(),
    lastSignedInDateTime DateTime ,
    departmentId int,
    entranceYear varchar(4),
    graduateYear varchar(4),
    informationDetail NVARCHAR(Max),
    userPrivacySettingId int not null,
    FOREIGN KEY (userPrivacySettingId) REFERENCES UserPrivacySetting(userPrivacySettingId),
);

Create Table UserRegistration
(
    userRegistrationId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    registrationCode VARCHAR(50) NOT NULL,
    userId INT NOT NULL,
    createDateTime DATETIME NOT NULL DEFAULT GETDATE(),
    -- Assuming additional constraints and foreign keys
    FOREIGN KEY (userId) REFERENCES [User](userId),
)


Create Table JobPostType
(
    jobPostTypeId Int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    displayName VARCHAR(50) NOT NULL,
    typeCode VARCHAR(50) UNIQUE NOT NULL,
    createDateTime DATETIME NOT NULL DEFAULT GETDATE(),

);

Create Table JobPost
(
    jobPostId Int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    title VARCHAR(50) NOT NULL,
    informationDetail NVARCHAR(Max),
    userId INT NOT NULL,
    jobPostTypeId Int Not Null,
    createDateTime DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN Key (userId) REFERENCES [User](userId),
    FOREIGN Key (jobPostTypeId) REFERENCES [JobPostType](jobPostTypeId),
);

Create Table Department
(
    departmentId Int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    departmentCode VARCHAR(50) UNIQUE NOT NULL,
    displayName VARCHAR(50) NOT NULL,
    createDateTime DATETIME NOT NULL DEFAULT GETDATE(),
);

Create Table [Role]
(
    roleId Int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    displayName VARCHAR(50) NOT NULL,
    roleCode VARCHAR(50) NOT NULL,
    createDateTime DATETIME NOT NULL DEFAULT GETDATE(),
);


Create Table [UserRole]
(
    userRoleId Int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    userId Int NOT NULL,
    roleId Int NOT NULL,
    createDateTime DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN Key (userId) REFERENCES [User](userId),
    FOREIGN Key (roleId) REFERENCES [Role](roleId),

);
