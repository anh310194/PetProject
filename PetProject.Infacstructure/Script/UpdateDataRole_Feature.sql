update t set t.RoleName = 'Country'
from dbo.Role t where t.Id = 1
go
update t set t.FeatureName = 'AddCountry', Description = 'Add new country'
from dbo.Feature t where t.Id = 1
go
update t set t.FeatureName = 'UpdateCountry', Description = 'Update country'
from dbo.Feature t where t.Id = 2
go
update t set t.FeatureName = 'DeleteCountry', Description = 'Delete Country'
from dbo.Feature t where t.Id = 3
go
update t set t.FeatureName = 'ReadCountry', Description = 'Read All countries'
from dbo.Feature t where t.Id = 4