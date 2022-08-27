IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[GetFeaturesByUser]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[GetFeaturesByUser]
END
GO
Create proc GetFeaturesByUser
@UserId bigint
as
select FeatureId 
from dbo.RoleFeature where roleId in (select RoleId from dbo.UserRole where UserId = @userId)