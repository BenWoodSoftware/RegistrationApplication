# Registration Application

## Getting Started
A SQL deployment script has been generated for use with a local db. If there are any issues, the raw table structure and stored procedures have been provided alongside.
This was generated using SQL Server Express 2017.

### Operations

On Start-Up, the user should be navigated to the /User/Create route. The view should show a field for an email and a password. 
Validation for length and field contents have been used, and will result in red text appearing if they are violated.

Providing there are no errors being displayed, the user can then submit these fields for entry into the database. 
 - If there exists no duplicate email address, the information will be accepted and the user will receive a success message.
 - If a duplicate exists, the details will fail to be inserted into the database and the user will receive a duplicate exists message.
 - If the database fails to insert these values due to command exception, the user will be informed that it could not be added.

### Security concerns
 
 - Stored procedures and Command Parameters have been used to prevent the database from being exposed to malicious/rogue SQL injection. 
 - MVC Validation attributes have been used to prevent XSS abuse in the input fields. 
 - The Password on entry is been salted and hashed with SHA256 to ensure industry appropriate secure storage is respected.
 - The SQL Database restricts the Email to a maximum 320 characters, giving room for any address that fall with character length ranges of  255 + @ + 64.
 - Password is restricted to 64 characters to only allow for SHA256 usage.

## Unit tests

 - Various unit tests have been provided, mainly focusing on validating the model's behavior against various offending inputs. 
 

