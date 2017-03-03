using Microsoft.EntityFrameworkCore;
using FisherInsuranceApi.Models;
namespace FisherInsuranceApi.Data
{
    public class FisherContext : DbContext
    {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    string connection = "Data Source=C:/AMIS3610/FisherInsuranceApi/fisher-insurance.sqlite;";
    optionsBuilder.UseSqlite(connection);
    }
public DbSet<Claim> Claim { get; set; }
public DbSet<Quote> Quote { get; set; }
    }
}