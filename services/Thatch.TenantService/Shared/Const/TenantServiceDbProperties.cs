﻿namespace Thatch.TenantService.Shared.Const;

public static class TenantServiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "TenantService";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "TenantService";
}
