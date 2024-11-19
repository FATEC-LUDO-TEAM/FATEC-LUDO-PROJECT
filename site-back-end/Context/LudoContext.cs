using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class LudoContext : DbContext {
    public DbSet<User> Users { get; set; }
    public DbSet<UserCosmetics> UserCosmetics { get; set; }
    public DbSet<Cosmetic> Cosmetics { get; set; }

    public string DbPath { get; }

    public LudoContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql("Host=bf5zt2pz3q1ghre9ct5y-postgresql.services.clever-cloud.com;Port=50013;Username=uqphekhutoos9bvf3twr;Password=5gdw08Nyzk5cxCzQgc1QLsSPjKbKdO;Database=bf5zt2pz3q1ghre9ct5y");
}