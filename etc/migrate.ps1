Invoke-Expression "./etc/setup-infra.ps1"

Set-Location "./shared/Thatch.DbMigrator"
dotnet run 
Set-Location "../.."