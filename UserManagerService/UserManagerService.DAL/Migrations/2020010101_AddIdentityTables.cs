﻿using FluentMigrator;

namespace UserManagerService.DAL.Migrations
{
    [Migration(2020010101)]
    public class AddIdentityTables : Migration
    {
        public override void Up()
        {
            var identityTables = ResourceManager.GetResource("UserManagerService.DAL.Scripts.IdentityTables.sql");
            Execute.Sql(identityTables);

            Create.Column("FirstName")
                .OnTable("AspNetUsers")
                .AsString().Nullable();

            Create.Column("MiddleName")
                .OnTable("AspNetUsers")
                .AsString().Nullable();

            Create.Column("LastName")
                .OnTable("AspNetUsers")
                .AsString().Nullable();

            Create.Column("IsActive")
                .OnTable("AspNetUsers")
                .AsBoolean().NotNullable()
                .WithDefaultValue(false);
        }

        public override void Down()
        {
            var identityTables = ResourceManager.GetResource("UserManagerService.DAL.Scripts.DropIdentityTables.sql");
            Execute.Script(identityTables);

            Delete.Column("FirstName").FromTable("AspNetUsers");
            Delete.Column("MiddleName").FromTable("AspNetUsers");
            Delete.Column("LastName").FromTable("AspNetUsers");
            Delete.Column("IsActive").FromTable("AspNetUsers");
        }
    }
}
