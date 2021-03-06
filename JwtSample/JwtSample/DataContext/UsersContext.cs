﻿using JwtSample.DataContext.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace JwtSample.DataContext
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions options) : base(options) { }
        public UsersContext() : base() { }
        public DbSet<User> Users { get; set; }
    }
}
