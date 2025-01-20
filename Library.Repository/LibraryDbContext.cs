using Domain.Identity;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class LibraryDbContext : IdentityDbContext<BookStoreApplicationUser>
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<BookInOrder> BookInOrders { get; set; }
    public virtual DbSet<BookInShoppingCart> BookInShoppingCarts { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Publisher> Publishers { get; set; }
    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }



}
