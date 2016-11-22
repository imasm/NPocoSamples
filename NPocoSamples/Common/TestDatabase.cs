using System;
using System.Data.Common;
using NPoco;

namespace NPocoSamples.Common
{
    internal class TestDatabase : Database
    {
        public TestDatabase(string connectionName) : base(connectionName)
        {
        }

        protected override void OnExecutingCommand(DbCommand cmd)
        {
            Console.WriteLine(FormatCommand(cmd));
            base.OnExecutingCommand(cmd);
        }
    }
}
