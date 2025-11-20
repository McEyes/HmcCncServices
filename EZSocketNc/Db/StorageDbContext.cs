
using JAgentServiceProtocol;

using SQLite.CodeFirst;

using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite.EF6;
using System.IO;
using System.Runtime.Remoting.Contexts;

namespace EZSocketNc.Db
{
	public class StorageDbContext : DbContext
	{

		private static string connectionString = "Data Source=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "storage.db");
		//		<connectionStrings>
		//  <add name = "MyContext"
		//	   providerName="System.Data.SQLite" 
		//       connectionString="Data Source=MyDatabase.sqlite;Version=3;" />
		//</connectionStrings>
		private static StorageDbContext _dbcontext = null;

		public static StorageDbContext Instance
		{
			get
			{
				if (_dbcontext == null)
				{
                    //Database.SetInitializer(new CreateDatabaseIfNotExists<StorageDbContext>());
                    //DbConnection dbConnection2 = SQLiteProviderFactory.Instance.CreateConnection();
                    //dbConnection2.ConnectionString = connectionString;
                    //using (var context = new StorageDbContext(dbConnection2))
                    //{
                    //    context.Database.Initialize(force: true); // 确保数据库被初始化
                    //    context.Database.CompatibleWithModel(true);
                    //}

                    DbConnection dbConnection = SQLiteProviderFactory.Instance.CreateConnection();
                    dbConnection.ConnectionString = connectionString;
                    _dbcontext = new StorageDbContext(dbConnection);

				}
				return _dbcontext;
			}
		}

		private StorageDbContext(DbConnection conn) : base(conn,true)
		{
		}

		public DbSet<DBEntity> LocalStorages { get; set; }
		public DbSet<CmdRetryEntity> CmdStorages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 如果数据表存在多列组合主键时，需要进行描述
            // modelBuilder.Entity<DeptInfo>().HasKey(t => new { t.DeptId, t.DeptCode});

            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<StorageDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
            // CodeFirst创建数据表
            //Database.SetInitializer(new CreateDatabaseIfNotExists<StorageDbContext>());
            //Database.Initialize(force: true);
        }
    }
}
