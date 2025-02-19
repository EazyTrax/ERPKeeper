﻿using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPKeeperCore.Enterprise.DBContext
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ERPCoreDbContext>
    {
        public ERPCoreDbContext CreateDbContext(string[] args)
        {
            return new ERPCoreDbContext("NA");
        }
    }
}
