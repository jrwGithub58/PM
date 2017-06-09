CREATE TABLE PasswordAccount (
    Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE,
    AccountName TEXT NULL,
    Url TEXT NULL,
    UserName TEXT NULL,
    Password TEXT NULL,
    AccountOwner TEXT NULL,
	SecurityQuestions TEXT NULL
)

