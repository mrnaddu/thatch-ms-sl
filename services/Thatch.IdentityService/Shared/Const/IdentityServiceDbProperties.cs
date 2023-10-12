namespace Thatch.IdentityService.Shared.Const;

public static class IdentityServiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "IdentityService";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "IdentityService";
}
