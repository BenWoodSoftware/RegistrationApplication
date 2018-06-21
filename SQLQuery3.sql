Create procedure [dbo].[UpdateUserDetails]  
(  
   @Id int,  
   @Email nvarchar (320),  
   @Password char (50)   
)  
as  
begin  
   Update UserReg   
   set Email=@Email,   
   Password=@Password  
   where Id=@Id  
End