﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace L01_2021CL650_2021SC601.Models
{
    public class restauranteContext : DbContext
    {
        public restauranteContext(DbContextOptions<restauranteContext> options) : base(options)
        { }

        public DbSet<motoristas> motoristas { get; set; }
        public DbSet<platos> platos { get; set; }
        public DbSet<pedidos> pedidos { get; set;}
    }
}
