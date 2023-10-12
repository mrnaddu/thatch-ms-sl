$name = $args[0]

dotnet new web -n "$name.AuthServer" -o "authserver\$name.AuthServer"
dotnet new classlib -n "$name.Shared.Hosting" -o "shared\$name.Shared.Hosting"
dotnet new console -n "$name.DbMigrator" -o "shared\$name.DbMigrator"
dotnet new webapi -n "$name.AdministrationService" -o "services\$name.AdministrationService"
dotnet new webapi -n "$name.IdentityService" -o "services\$name.IdentityService"
dotnet new webapi -n "$name.TenantService" -o "services\$name.TenantService"
dotnet new sln -n "$name"
dotnet sln ".\$name.sln" add (Get-ChildItem -r **/*.csproj)