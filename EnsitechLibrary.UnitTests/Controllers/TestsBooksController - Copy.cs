//using EnsitechLibrary.Controllers;
//using EnsitechLibrary.Entities;
//using EnsitechLibrary.Services;
//using FluentAssertions;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;
//using Moq;

//namespace EnsitechLibrary.UnitTests.Controllers
//{
//    public class TestsBooksController
//    {

//        [Fact]
//        public async Task Books()
//        {
//            // Arrange
//            var loggerMock = new Mock<ILogger<BooksController>>();

//            var IBookRepositoryMock = new Mock<IBookRepository<Book>>();
//            var mockSet = new Mock<DbSet<Book>>();
//            //var booksController = new BookRepository();
//            var booksController = new BooksController(loggerMock, IBookRepositoryMock);

//            // Act
//            var results = await booksController.GetBooks();

//            // Assert

//            results.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
//        }

//        //[Fact]
//        //public void CreateBook_AddsBookToContext()
//        //{
//        //    // Arrange
//        //    var options = new DbContextOptionsBuilder<BookDbContext>()
//        //        .UseInMemoryDatabase(databaseName: "TestDb")
//        //        .Options;

//        //    var mockSet = new BookDbContext(options);

//        //    var repository = new BookRepository(mockSet);
//        //    var book = new Book { Title = "Test Book", Author = "Test Author" };

//        //    // Act
//        //    repository.CreateBook(book);

//        //    // Assert
//        //    Assert.Contains(book, mockSet.books);

//        //}
//        //private readonly Mock<BookRepository> _itemRepositoryMock;

//        //[Fact]
//        //public void DeleteBook_deletesBookFromContext()
//        //{
//        //    // Arrange
//        //    var bookToAdd = new Book { Id= 1, Title = "Test Book", Author = "Test Author" };
//        //    var Id = 1;

//        //    var booksData = new List<Book>(); // Empty list to simulate database

//        //    var mockSet = new Mock<DbSet<Book>>();
//        //    mockSet.Setup(m => m.Add(It.IsAny<Book>())).Callback<Book>(booksData.Add);

//        //    var mockContext = new Mock<BookDbContext>();
//        //    //mockContext.Setup(c => c.books).Returns(mockSet.Object);

//        //    var repository = new BookRepository(mockContext.Object);

//        //    // Act
//        //    repository.DeleteBook(Id);

//        //    // Assert
//        //    booksData.Should().ContainSingle(); // Ensure only one book is added
//        //    booksData.First().Title.Should().Be("Test Book"); // Ensure correct book is added
//        //    booksData.First().Author.Should().Be("Test Author"); // Ensure correct author is added
//        //    booksData.First().Id.Should().Be(Id);
//        //}


//    }
//}