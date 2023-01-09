using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class FileSharingContext : IdentityDbContext
    {

        public FileSharingContext(DbContextOptions<FileSharingContext> options) : base(options)
        {
        }

        public DbSet<AclModel> AclModels { get; set; }
        public DbSet<TextFileModel> TextFileModels { get; set; }

    }
}
