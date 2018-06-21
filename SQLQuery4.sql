Create procedure [dbo].[DeleteUser]  
(  
   @Id int  
)  
as   
begin  
   Delete from UserReg where Id=@Id  
End