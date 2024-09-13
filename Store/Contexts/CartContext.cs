using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Contexts;

public class CartContext : DbContext
{
    public CartContext(DbContextOptions<CartContext> options) 
    : base(options) => Database.EnsureCreated();
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Good> Goods { get; set; }
    public DbSet<Payment> Payments { get; set; }
}