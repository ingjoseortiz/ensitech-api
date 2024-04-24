using EnsitechLibrary.Controllers;
using EnsitechLibrary.Entities;
using EnsitechLibrary.Models;
using EnsitechLibrary.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Moq;
using System.Security.Claims;

namespace EnsitechLibrary.UnitTests.Controllers
{ 
    public class TestsBooksController
    {
        [Fact]
        public void AddBooks_Returns_OkResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<BooksController>>();
            var mockRepository = new Mock<IBookRepository>(); // Assuming IRepository is the interface for your repository
            var controller = new BooksController(mockLogger.Object, mockRepository.Object);
            var bookDto = new BookDto
            {
                Title = "Test Book",
                Author = "Test Author",
                Genre = "Test Genre",
                InventoryDto = new InventoryDto { Quantity = 10 }
            };

            // Act
            var result = controller.AddBooks(bookDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void AddBooks_Creates_Book_In_Repository()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<BooksController>>();
            var mockRepository = new Mock<IBookRepository>();
            var controller = new BooksController(mockLogger.Object, mockRepository.Object);
            var bookDto = new BookDto
            {
                Title = "Test Book",
                Author = "Test Author",
                Genre = "Test Genre",
                InventoryDto = new InventoryDto { Quantity = 10 }
            };

            // Act
            var result = controller.AddBooks(bookDto) as OkObjectResult;
            var createdBook = result.Value as Book;

            // Assert
            mockRepository.Verify(repo => repo.CreateBook(It.IsAny<Book>()), Times.Once);
            createdBook.Should().NotBeNull();
            createdBook.Title.Should().Be(bookDto.Title);
            createdBook.Author.Should().Be(bookDto.Author);
            createdBook.Genre.Should().Be(bookDto.Genre);
            createdBook.Inventory.Quantity.Should().Be(bookDto.InventoryDto.Quantity);
        }

        [Fact]
        public void RemoveBooks_Returns_OkResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<BooksController>>();
            var mockRepository = new Mock<IBookRepository>(); // Assuming IRepository is the interface for your repository
            var controller = new BooksController(mockLogger.Object, mockRepository.Object);
            var bookDto = new BookDto
            {
                Id = 1, // Example ID
                Title = "Test Book",
                Author = "Test Author",
                Genre = "Test Genre"
            };

            // Act
            var result = controller.RemoveBooks(bookDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void RemoveBooks_Deletes_Book_From_Repository()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<BooksController>>();
            var mockRepository = new Mock<IBookRepository>();
            var controller = new BooksController(mockLogger.Object, mockRepository.Object);
            var bookDto = new BookDto
            {
                Id = 1, // Example ID
                Title = "Test Book",
                Author = "Test Author",
                Genre = "Test Genre"
            };

            // Act
            var result = controller.RemoveBooks(bookDto) as OkObjectResult;
            var removedBook = result.Value as Book;

            // Assert
            mockRepository.Verify(repo => repo.DeleteBook(It.IsAny<int>()), Times.Once);
            removedBook.Should().NotBeNull();
            removedBook.Id.Should().Be(bookDto.Id);
            removedBook.Title.Should().Be(bookDto.Title);
            removedBook.Author.Should().Be(bookDto.Author);
            removedBook.Genre.Should().Be(bookDto.Genre);
        }

        [Fact]
        public void Update_Returns_OkResult_When_Book_Exists()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<BooksController>>();
            var mockRepository = new Mock<IBookRepository>(); // Assuming IRepository is the interface for your repository
            var controller = new BooksController(mockLogger.Object, mockRepository.Object);
            var bookDto = new BookDto
            {
                Id = 1, // Example ID
                Title = "Updated Book Title",
                Author = "Updated Author",
                Genre = "Updated Genre",
                InventoryDto = new InventoryDto { Quantity = 20 } // Example updated quantity
            };
            var existingBook = new Book
            {
                Id = bookDto.Id,
                Title = "Existing Book Title",
                Author = "Existing Author",
                Genre = "Existing Genre",
                Inventory = new Inventory { Quantity = 10 }
            };
            mockRepository.Setup(repo => repo.GetBook(It.IsAny<int>())).Returns(existingBook);

            // Act
            var result = controller.Update(bookDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void Update_Returns_NotFoundResult_When_Book_Does_Not_Exist()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<BooksController>>();
            var mockRepository = new Mock<IBookRepository>();
            var controller = new BooksController(mockLogger.Object, mockRepository.Object);
            var bookDto = new BookDto
            {
                Id = 1, // Example ID
                Title = "Updated Book Title",
                Author = "Updated Author",
                Genre = "Updated Genre",
                InventoryDto = new InventoryDto { Quantity = 20 } // Example updated quantity
            };
            mockRepository.Setup(repo => repo.GetBook(It.IsAny<int>())).Returns((Book)null);

            // Act
            var result = controller.Update(bookDto);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        
        [Fact]
        public void Rent_Returns_OkResult_When_Book_Exists()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<BooksController>>();
            var mockRepository = new Mock<IBookRepository>(); // Assuming IRepository is the interface for your repository
            var controller = new BooksController(mockLogger.Object, mockRepository.Object);
            var bookDto = new BookDto
            {
                Id = 1, // Example ID
                Title = "Existing Book Title",
                Author = "Existing Author",
                Genre = "Existing Genre"
            };
            var existingBook = new Book
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                Author = bookDto.Author,
                Genre = bookDto.Genre
            };
            mockRepository.Setup(repo => repo.GetBook(It.IsAny<int>())).Returns(existingBook);
            var userId = "123"; // Example user ID
            var claims = new Claim[] { new Claim("id", userId) };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = controller.Rent(bookDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void Rent_Returns_NotFoundResult_When_Book_Does_Not_Exist()
        {
            Console.WriteLine("JOE");
            // Arrange
            var mockLogger = new Mock<ILogger<BooksController>>();
            var mockRepository = new Mock<IBookRepository>();
            var controller = new BooksController(mockLogger.Object, mockRepository.Object);
            var bookDto = new BookDto
            {
                Id = 1, // Example ID
                Title = "Non-existent Book Title",
                Author = "Non-existent Author",
                Genre = "Non-existent Genre"
            };
            var test = mockRepository.Setup(repo => repo.GetBook(It.IsAny<int>())).Returns((Book)null);
            Console.WriteLine(test);

            // Act
            var result = controller.Rent(bookDto);

            // Assert
           result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Rent_Invokes_RentBook_Method_In_Repository()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<BooksController>>();
            var mockRepository = new Mock<IBookRepository>();
            var controller = new BooksController(mockLogger.Object, mockRepository.Object);
            var bookDto = new BookDto
            {
                Id = 1, // Example ID
                Title = "Existing Book Title",
                Author = "Existing Author",
                Genre = "Existing Genre"
            };
            var existingBook = new Book
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                Author = bookDto.Author,
                Genre = bookDto.Genre
            };
            mockRepository.Setup(repo => repo.GetBook(It.IsAny<int>())).Returns(existingBook);
            var userId = "123"; // Example user ID
            var claims = new Claim[] { new Claim("id", userId) };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = controller.Rent(bookDto);

            // Assert
            mockRepository.Verify(repo => repo.RentBook(existingBook.Id, Convert.ToInt16(userId)), Times.Once);
        }

        [Fact]
        public void Return_Returns_OkResult_When_Book_Exists()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<BooksController>>();
            var mockRepository = new Mock<IBookRepository>(); // Assuming IRepository is the interface for your repository
            var controller = new BooksController(mockLogger.Object, mockRepository.Object);
            var bookDto = new BookDto
            {
                Id = 1, // Example ID
                Title = "Existing Book Title",
                Author = "Existing Author",
                Genre = "Existing Genre"
            };
            var existingBook = new Book
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                Author = bookDto.Author,
                Genre = bookDto.Genre
            };
            mockRepository.Setup(repo => repo.GetBook(It.IsAny<int>())).Returns(existingBook);

            // Act
            var result = controller.Return(bookDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void Return_Returns_NotFoundResult_When_Book_Does_Not_Exist()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<BooksController>>();
            var mockRepository = new Mock<IBookRepository>();
            var controller = new BooksController(mockLogger.Object, mockRepository.Object);
            var bookDto = new BookDto
            {
                Id = 1, // Example ID
                Title = "Non-existent Book Title",
                Author = "Non-existent Author",
                Genre = "Non-existent Genre"
            };
            mockRepository.Setup(repo => repo.GetBook(It.IsAny<int>())).Returns((Book)null);

            // Act
            var result = controller.Return(bookDto);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Return_Invokes_Return_Method_In_Repository()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<BooksController>>();
            var mockRepository = new Mock<IBookRepository>();
            var controller = new BooksController(mockLogger.Object, mockRepository.Object);
            var bookDto = new BookDto
            {
                Id = 1, // Example ID
                Title = "Existing Book Title",
                Author = "Existing Author",
                Genre = "Existing Genre"
            };
            var existingBook = new Book
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                Author = bookDto.Author,
                Genre = bookDto.Genre
            };
            mockRepository.Setup(repo => repo.GetBook(It.IsAny<int>())).Returns(existingBook);

            // Act
            var result = controller.Return(bookDto);

            // Assert
            mockRepository.Verify(repo => repo.Return(existingBook.Id), Times.Once);
        }

        [Fact]
        public void Rented_Returns_Expected_String()
        {
            // Arrange
            var mockRepository = new Mock<IBookRepository>(); // Assuming IRepository is the interface for your repository
            var controller = new BooksController(null, mockRepository.Object); // Since ILogger is not used in this endpoint, we can pass null
            var expectedString = "Mocked rented books string";
            mockRepository.Setup(repo => repo.Rented()).Returns(expectedString);

            // Act
            var result = controller.Rented();

            // Assert
            result.Should().Be(expectedString);
        }
    }
}