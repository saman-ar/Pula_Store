using Data.AppContext;
using Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Data.Repo
{
    //public class TestRepo
    //{
    //    private readonly AppDbContext _context;

    //    public TestRepo(AppDbContext context)
    //    {
    //        _context = context;
    //    }
       
    //    private void ForEfTest()
    //    {
  
    //        var book0 = new Book();

    //        var opts = new DbContextOptionsBuilder<AppDbContext>();

    //        opts.UseSqlServer("your connection string",
    //                          op => op.EnableRetryOnFailure(3, TimeSpan.FromSeconds(2), null));

    //        //_context.Books.FromSql(" my sql command").Include(b => b.Reviews).ToList();
    //        _context.Database.ExecuteSqlCommand("");
    //        _context.Entry(book0).Reload();

    //        _context.Attach<Book>(book0);

    //        _context.Entry(book0).Collection(c => c.Reviews).Load();
    //        _context.Entry(book0).Reference(c => c.AuthorsLink /*if it be single*/ ).Load();

    //        var s = _context.Entry(book0).Property(b => b.Reviews).CurrentValue;
    //        var res = _context.Entry(book0).Property(b => b.Reviews).IsModified;
    //        var ress = _context.Entry(book0).Property(b => b.Reviews).OriginalValue;

    //        _context.Find<Book>(10);

    //        var book = new Book();
    //        //_context.Entry(book).
    //        //                    Collection(b => b.Reviews)
    //        //                    .Query()
    //        //                    .Select(r => new { id = r.BookId }).ToList();
    //        //.Load();


    //        var strategy = _context.Database.CreateExecutionStrategy();
    //        strategy.Execute(() =>
    //        {
    //            var option = new DbContextOptions<AppDbContext>();//set other options 
    //            using (var stContext = new AppDbContext(option))
    //            {
    //                using (var transaction = stContext.Database.BeginTransaction())
    //                {

    //                    /// add your multiple queries and muiltiple savechange methods 

    //                    transaction.Commit();
    //                }
    //            }
    //        });

    //    }

    //    private void ForLinqTest()
    //    {
    //        var query = from p in _context.Products
    //                    where p.Description == ""
    //                    orderby p.Description
    //                    //select new { d = p.Brand, id = p.Category };
    //                    group p by p.Brand;

    //        var query0 = from p in _context.Products
    //                     where p.Description == ""
    //                     orderby p.Description
    //                     //orderby p.Description ascending orderby p.Name descending
    //                     group p by p.Brand into g
    //                     where g.Count() > 3
    //                     select g;

    //        ///join
    //        var query01 = from p in _context.Products
    //                      join c in _context.Categories on p.Id equals c.ParentId
    //                      select new { name = p.Name, id = c.Id };

    //        ///join in Ext method linq
    //        var query02 = _context.Products
    //                    .Join(_context.Categories,
    //                            p => p.Id, c => c.ParentId,
    //                            (product, category) => new { Name = product.Name, id = category.Id });

    //        ///group join in Ext method linq
    //        var query03 = _context.Products
    //                    .GroupJoin(_context.Categories,
    //                            p => p.Id, c => c.ParentId,
    //                            (product, categories) => new { Name = product.Name, count = categories.Count() });

    //        ///cross join in Ext method linq
    //        var query04 = _context.Products.SelectMany(p => _context.Categories,
    //                            (product, category) =>
    //                             new { productName = product.Name, categoryName = category.Name });

    //        ///group join
    //        var query2 = from p in _context.Products
    //                     join c in _context.Categories on p.Id equals c.ParentId into g
    //                     select new { name = p.Name, count = g.Count() };

    //        ///cross join
    //        var query3 = from p in _context.Products
    //                     from c in _context.Categories
    //                     select new { Name = p.Name, productList = c.Products };

    //    }
    //}

    //public class Book
    //{
    //    public int BookId { get; set; }
    //    public ICollection<Review> Reviews { get; set; }

    //    //[DatabaseGenerated(DatabaseGeneratedOption.]
    //    public string AuthorsLink { get; set; }
    //}
    //public class Review
    //{

    //}

}
