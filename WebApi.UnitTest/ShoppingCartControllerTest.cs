using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebApi.Controllers;

namespace WebApi.UnitTest
{
    [TestFixture]
    public class ShoppingCartControllerTest
    {
        ShoppingCartController _controller;
        IShoppingCartService _service;

        [SetUp]
        public void Setup()
        {
            _service = new ShoppingCartServiceFake();
            _controller = new ShoppingCartController(_service);
        }

        [Test]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.That(okResult.Result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;

            // Assert
            Assert.That(okResult?.Value, Is.TypeOf<List<ShoppingItem>>());
            var items = (List<ShoppingItem>)okResult?.Value;
            Assert.That(items.Count, Is.EqualTo(3));
        }
        
        [Test]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(Guid.NewGuid());
 
            // Assert
            Assert.That(notFoundResult.Result, Is.TypeOf<NotFoundResult>());
        }
 
        [Test]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
 
            // Act
            var okResult = _controller.Get(testGuid);
 
            // Assert
            Assert.That(okResult.Result, Is.TypeOf<OkObjectResult>());
        }
 
        [Test]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
 
            // Act
            var okResult = _controller.Get(testGuid).Result as OkObjectResult;
 
            // Assert
            Assert.That(okResult.Value, Is.TypeOf<ShoppingItem>());
            Assert.That(((ShoppingItem) okResult.Value).Id, Is.EqualTo(testGuid));
        }
        
        [Test]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new ShoppingItem()
            {
                Manufacturer = "Guinness",
                Price = 12.00M
            };
            _controller.ModelState.AddModelError("Name", "Required");
 
            // Act
            var badResponse = _controller.Post(nameMissingItem);
 
            // Assert
            Assert.That(badResponse, Is.TypeOf<BadRequestObjectResult>());
        }
 
 
        [Test]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var testItem = new ShoppingItem()
            {
                Name = "Guinness Original 6 Pack",
                Manufacturer = "Guinness",
                Price = 12.00M
            };
 
            // Act
            var createdResponse = _controller.Post(testItem);
 
            // Assert
            Assert.That(createdResponse, Is.TypeOf<CreatedAtActionResult>());
           
        }
 
        [Test]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new ShoppingItem()
            {
                Name = "Guinness Original 6 Pack",
                Manufacturer = "Guinness",
                Price = 12.00M
            };
 
            // Act
            var createdResponse = _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as ShoppingItem;
 
            // Assert
            Assert.That(item, Is.TypeOf<ShoppingItem>());
            Assert.That(item.Name, Is.EqualTo("Guinness Original 6 Pack"));
        }
        
        [Test]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingGuid = Guid.NewGuid();
 
            // Act
            var badResponse = _controller.Remove(notExistingGuid);
 
            // Assert
            Assert.That(badResponse, Is.TypeOf<NotFoundResult>());
        }
 
        [Test]
        public void Remove_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
 
            // Act
            var okResponse = _controller.Remove(existingGuid);
 
            // Assert
            Assert.That(okResponse, Is.TypeOf<OkResult>());
        }
        
        [Test]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            // Arrange
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
 
            // Act
            var okResponse = _controller.Remove(existingGuid);
 
            // Assert
            Assert.That(_service.GetAllItems().Count(), Is.EqualTo(2));
        }
    }
}