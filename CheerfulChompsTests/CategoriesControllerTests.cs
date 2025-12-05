using CheerfulChomps.Controllers;
using CheerfulChomps.Data;
using CheerfulChomps.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CheerfulChompsTests;

[TestClass]
public class CategoriesControllerTests
{
    // class level vars shared among tests
    private ApplicationDbContext _context;
    CategoriesController controller;

    // this method runs automatically before each test
    [TestInitialize]
    public void TestInitialize()
    {
        // set up db options using unique db name & instantiate db obj
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);

        // populate mock db w/data before tests
        _context.Category.Add(new Category { CategoryId = 14, Name = "Cat 14" });
        _context.Category.Add(new Category { CategoryId = 56, Name = "Cat Fifty Six" });
        _context.Category.Add(new Category { CategoryId = 92, Name = "A Newer Category" });
        _context.SaveChanges();

        // instantiate controller using mock db so we can run tests
        controller = new CategoriesController(_context);
    }

    #region "Index"
    [TestMethod]
    public void IndexLoadsView()
    {
        // arrange - set up required vars / objs.  moved to TestInitialize
        var controller = new CategoriesController();
        // act - call method and store result.
        // Must cast IActionResult return type to ViewResult to see the View Name
        var result = (ViewResult)controller.Index();

        // assert - evaluate if result matches our expectation
        Assert.AreEqual("Index", result.ViewName);
    }

    [TestMethod]
    public void IndexLoadsCategories()
    {
        // act
        var result = (ViewResult)controller.Index();

        // assert - does view data model match the db table data?
        var categories = _context.Category.OrderBy(c => c.Name).ToList();
        CollectionAssert.AreEqual(categories, (List<Category>)result.Model);
    }
    #endregion

    [TestMethod]
    public void  DeleteRemovesCategory()
    {
        // arrange 
        var controller = _context.Category();
        // act 
        var result = (ViewResult)controller.Index(); 
        // assert 
        Assert.AreEqual("Index", result.ViewName);
    }
