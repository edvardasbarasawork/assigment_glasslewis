using SimpleMigrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassLewisTest.Migrations.Migrations
{
    [Migration(2021_06_14_19_30, "init to Orders")]
    public class __2021_06_14_19_30_Init : Migration
    {
        protected override void Up()
        {
            Execute(@"
				create table [Exchanges]
				(
					[Id] [uniqueidentifier] not null constraint PK_Exchanges primary key clustered,
					[Name] [nvarchar](256) not null constraint UQ_Exchanges_Name unique([Name])
				)
				
				create table [Companies]
				(
					[Id] [uniqueidentifier] not null constraint PK_Companies primary key clustered,
					[ExchangeId] [uniqueidentifier] not null constraint FK_Companies_ExchangeId foreign key (ExchangeId)
																									references [Exchanges] (Id),
					[Ticker] [nvarchar](256) not null,
					[ISIN] [nvarchar](256) not null constraint UQ_Companies_Name unique([ISIN]),
					[Website] [nvarchar](256) not null,
					[Created] [datetime2](0) not null,
					[Deleted] [datetime2](0) null,
					[Updated] [datetime2](0) null,
					index IX_Companies_ExchangeId nonclustered (ExchangeId)
				)
			");
        }

        protected override void Down()
        {
            Execute(@"
				drop table Companies
				drop table Exchanges
			");
        }
    }
}
