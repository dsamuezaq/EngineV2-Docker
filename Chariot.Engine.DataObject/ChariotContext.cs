using Chariot.Engine.DataObject.MardisCommon;
using Chariot.Engine.DataObject.MardisCore;
using Chariot.Engine.DataObject.MardisSecurity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chariot.Engine.DataObject
{
    public class ChariotContext : DbContext
    {
        private readonly string _connectionString;
        protected SqlConnection Connection;
        protected SqlConnection connection => Connection ?? (Connection = GetOpenConnection());

        public ChariotContext(DbContextOptions<ChariotContext> options)
                  : base(options)
        {
            _connectionString = options.FindExtension<SqlServerOptionsExtension>().ConnectionString;
  
            //extension.ConnectionString = connectionString;
        }
        public SqlConnection GetOpenConnection(bool mars = false)
        {
            var cs = _connectionString;
            if (mars)
            {
                var scsb = new SqlConnectionStringBuilder(cs)
                {
                    MultipleActiveResultSets = true
                };
                cs = scsb.ConnectionString;
            }
            var connection = new SqlConnection(cs);
            connection.Open();
            return connection;
        }
        public SqlConnection GetClosedConnection()
        {
            var conn = new SqlConnection(_connectionString);
            if (conn.State != ConnectionState.Closed) throw new InvalidOperationException("should be closed!");
            return conn;
        }
        /// <summary>
        /// Table Tasks
        /// Creation :202004016
        /// </summary>
        public DbSet<TaskCampaign> TaskCampaigns { get; set; }
        /// <summary>
        /// Table User system
        /// Creation :20200510
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Table Personal traking in APP mobil system
        /// Creation :20200712
        /// </summary>
        public DbSet<PersonalTraker> PersonalTrakers { get; set; }

        /// <summary>
        /// Users app mobil 
        /// Creation :20200712
        /// </summary>
        public DbSet<Pollster> Pollsters { get; set; }
        /// <summary>
        /// Account for project
        /// Creation :20200712
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        ///  Campaigns for project
        /// Creation :20200712
        /// </summary>
        public DbSet<Campaign> Campaigns { get; set; }

        /// <summary>
        /// Table History Tracking
        /// Creation :20200722
        /// </summary>
        public DbSet<TrackingBranch> TrackingBranches { get; set; }
    }
    }
