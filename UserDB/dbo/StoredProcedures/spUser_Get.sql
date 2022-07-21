CREATE PROCEDURE [dbo].[spUser_Get]
	@Id int
AS
begin
	Select Id, FirstName, LastName from dbo.[User]
	where Id = @Id;
end
