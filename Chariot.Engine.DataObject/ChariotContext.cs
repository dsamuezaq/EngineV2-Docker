using Chariot.Engine.DataObject.MardisCommon;
using Chariot.Engine.DataObject.MardisCore;
using Chariot.Engine.DataObject.MardisOrders;
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
using Z.EntityFramework.Extensions;
using Chariot.Engine.DataObject.Procedure;
using Dapper;

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

        /// <summary>
        /// Table relacion usuario Campañia
        /// Creation :20200729
        /// </summary>
        public DbSet<UserCampaign> UserCampaigns { get; set; }


        /// <summary>
        /// Table vendedores App industrial
        /// Creation :20200812
        /// </summary>
        public DbSet<Salesman> Salesmans { get; set; }


        /// <summary>
        ///  Table rubros y items App industrial
        /// Creation :20200812
        /// </summary>
        public DbSet<Items> Itemss { get; set; }


        /// <summary>
        ///  Table rubros y items App industrial
        /// Creation :20200812
        /// </summary>
        public DbSet<Client> Clients { get; set; }

        /// <summary>
        ///  Table productos App industrial
        /// Creation :20200812
        /// </summary>
        public DbSet<Product> ProductOrders { get; set; }


        /// <summary>
        ///  Table depostos App industrial
        /// Creation :20200812
        /// </summary>
        public DbSet<Deposit> Deposits { get; set; }

        /// <summary>
        /// Tabla de Locales
        ///      Creation :20200910
        /// </summary>
        public DbSet<Branch> Branches { get; set; }

        public DbSet<Country> Countries { get; set; }

        /// <summary>
        /// Tabla de Provincias
        /// </summary>
        public DbSet<Province> Provinces { get; set; }


        /// <summary>
        /// Tabla de Distritos 
        /// </summary>
        public DbSet<District> Districts { get; set; }

        /// <summary>
        /// Tabla de Sectores
        /// </summary>
        public DbSet<Sector> Sectors { get; set; }


        /// <summary>
        /// Tabla de Parroquias
        /// </summary>
        public DbSet<Parish> Parishes { get; set; }

        /// <summary>
        /// Tabla de Personas
        /// </summary>
        public DbSet<Person> Persons { get; set; }


        /// <summary>
        /// Tabla de Profile
        /// </summary>
        public DbSet<Profile> Profiles { get; set; }

        /// <summary>
        /// Tabla de AuthorizationProfile
        /// </summary>
        public DbSet<AuthorizationProfile> AuthorizationProfiles { get; set; }

        /// <summary>
        /// Tabla de Menu
        /// </summary>
        public DbSet<Menu> Menus { get; set; }

        /// <summary>
        /// Tabla de Menu
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Orders
        /// </summary>
        public DbSet<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Tabla de Inventory
        /// </summary>
        public DbSet<Inventory> Inventories { get; set; }

        /// <summary>
        /// Inventory_detail
        /// </summary>
        public DbSet<Inventory_detail> Inventory_details { get; set; }

        /// <summary>
        /// Estados de tarea
        /// </summary>
        public DbSet<StatusTask> StatusTasks { get; set; }


        /// <summary>
        /// Estados de tarea
        /// </summary>
        public DbSet<StatusTaskAccount> StatusTaskAccounts { get; set; }

        //     public DbSet<SP_dato_tracking_encuestadores> SP_dato_tracking { get; set; }

        /// <summary>
        /// Estados de tarea
        /// </summary>
        public DbSet<Visitas> RegistroVisitalocales { get; set; }
        /// <summary>
        /// Secuencial de ordenes
        /// </summary>
        public DbSet<SequenceOrder> SequenceOrders { get; set; }

        /// <summary>
        /// Secuencial de ordenes
        /// </summary>
        public DbSet<PagoCartera> PagoCarteras { get; set; }
        /// <summary>
        /// Secuencial de ordenes
        /// </summary>
        public DbSet<FacturasEntregadas> FacturasEntregadas { get; set; }
        /// <summary>
        /// Secuencial de ordenes
        /// </summary>
        public DbSet<Devolucion> Devoluciones { get; set; }
        /// <summary>
        /// Secuencial de ordenes
        /// </summary>
        public DbSet<DevolucionFactura> DevolucionFacturas { get; set; }
        /// <summary>
        /// Secuencial de ordenes
        /// </summary>
        public DbSet<vw_pagoxcarteraDevolucion> pagosxcarteraDevolucion { get; set; }
        /// <summary>
        /// Secuencial de ordenes
        /// </summary>
        public DbSet<vw_pagoxcarteraDevolucion_factura> pagoxcarteraDevolucion_factura { get; set; }
        /// <summary>
        /// Tabla Invoice
        /// </summary>
        public DbSet<Invoice> Invoices { get; set; }

        /// <summary>
        /// Tabla Invoice Detail
        /// </summary>
        public DbSet<Invoice_detail> Invoice_details { get; set; }

        /// <summary>
        /// Tabla Central Warenhouse
        /// </summary>
        public DbSet<Central_Warenhouse> Central_Warenhouses { get; set; }

        /// <summary>
        /// Tabla Movil Warenhouse
        /// </summary>
        public DbSet<Movil_Warenhouse> Movil_Warenhouses { get; set; }

        /// <summary>
        /// Tabla Distributor
        /// </summary>
        public DbSet<Distributor> Distributors { get; set; }

        /// <summary>
        /// Tabla Distributor y vendedor
        /// </summary>
        public DbSet<SALESMAN_DISTRIBUTOR> SALESMAN_DISTRIBUTORS { get; set; }

        /// <summary>
        /// Tabla Central Warenhouse Resume
        /// </summary>
        public DbSet<Central_Warenhouse_Resume> Central_Warenhouse_Resumes { get; set; }

        /// <summary>
        /// Tabla Movil Warenhouse Resume
        /// </summary>
        public DbSet<Movil_Warenhouse_Resume> Movil_Warenhouse_Resumes { get; set; }

        /// <summary>
        /// Tabla Movil Warenhouse Resume
        /// </summary>
        public DbSet<Log_Cierre_Dia> Log_Cierre_Dias { get; set; }

        /// <summary>
        /// Secuencial de ordenes
        /// </summary>
        public DbSet<UserPollster> UserPollsters { get; set; }
        public IEnumerable<T> Query<T>(string query) where T : class
        {
            return connection.Query<T>(query);
        }
    }
}
