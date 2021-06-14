using SimpleMigrations;

namespace GlassLewisTest.Migrations.Migrations
{
    [Migration(2021_06_14_19_30, "Init companies DB")]
    public class __2021_06_14_19_30_Init : Migration
    {
        protected override void Up()
        {
            Execute(@"
				create table [Exchanges]
				(
					[Id] [uniqueidentifier] not null constraint PK_Exchanges primary key clustered,
					[Name] [nvarchar](256) not null constraint UQ_Exchanges_Name unique([Name])
				);
				
				create table [Companies]
				(
					[Id] [uniqueidentifier] not null constraint PK_Companies primary key clustered,
					[ExchangeId] [uniqueidentifier] not null constraint FK_Companies_ExchangeId foreign key (ExchangeId)
																									references [Exchanges] (Id),
					[Name] [nvarchar](256) not null,
					[Ticker] [nvarchar](256) not null,
					[ISIN] [nvarchar](256) not null constraint UQ_Companies_Name unique([ISIN]),
					[Website] [nvarchar](256) not null,
					[Created] [datetime2](0) not null,
					[Deleted] [datetime2](0) null,
					[Updated] [datetime2](0) null,
					index IX_Companies_ExchangeId nonclustered (ExchangeId)
				);

				insert into Exchanges (Id, Name)
				values (newid(), N'NASDAQ'),
					   (newid(), N'Pink Sheets'),
					   (newid(), N'Euronext Amsterdam'),
					   (newid(), N'Tokyo Stock Exchange'),
					   (newid(), N'Deutsche Börse');
				
				insert into Companies (Id, Name, ExchangeId, Ticker, ISIN, Website, Created)
				values	(newid(), 'Apple Inc.',				(select top 1 Id from Exchanges where Name = N'NASDAQ'),				'AAPL',		'US0378331005',	'http://www.apple.com',			getdate()),
						(newid(), 'British Airways Plc',	(select top 1 Id from Exchanges where Name = N'Pink Sheets'),			'BAIRY',	'US1104193065', '',								getdate()),
						(newid(), 'Heineken NV',			(select top 1 Id from Exchanges where Name = N'Euronext Amsterdam'),	'HEIA',		'NL0000009165', '',								getdate()),
						(newid(), 'Panasonic Corp',			(select top 1 Id from Exchanges where Name = N'Tokyo Stock Exchange'),	'6752',		'JP3866800000', 'http://www.panasonic.co.jp',	getdate()),
						(newid(), 'Porsche Automobil',		(select top 1 Id from Exchanges where Name = N'Deutsche Börse'),		'PAH3',		'DE000PAH0038', 'https://www.porsche.com/',		getdate());
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
