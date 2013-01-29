﻿// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=(local);Initial Catalog=KnowYourTurf_BASE;Trusted_Connection=Yes;`
//     Schema:                 ``
//     Include Views:          `False`

//     Factory Name:          `SqlClientFactory`

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using FluentMigrator;

namespace Migrations
{
    [Migration(1)]
    public class CreateInitialDB : Migration
    {
        public override void Up()
        {
            string sql = System.IO.File.ReadAllText(@"dbfluentmigration\initialdb.sql");
            Execute.Sql(sql);
        }

        public override void Down()
        {
        }
    }
}