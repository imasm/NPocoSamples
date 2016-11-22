using NPoco;

namespace NPocoSamples.Common
{
    public static class DbInfo
    {
        public const string Name = "Northwind";

        public const string ConnecionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NORTHWIND;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public const string ProviderName = "System.Data.SqlClient";

        public static readonly DatabaseType DatabaseType = DatabaseType.SqlServer2012;
    }
}
