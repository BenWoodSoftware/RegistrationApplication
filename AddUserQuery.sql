Create procedure [dbo].[AddNewUser]  
( 
   @Email nvarchar (320),  
   @Password char (50)   
)  
as  
begin
   Insert into UserReg values(@Email,@Password)  
End