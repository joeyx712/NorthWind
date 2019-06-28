using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWind.Controllers;
using NorthWind.Models;

namespace NorthWind.Tests.Controllers
{
    [TestClass]
    public class ProductsConrollerTest
    {
        [TestMethod]
        public async Task GetAllProducts_ShouldReturnAllProducts()
        {
            // Arrange
            var controller = new ProductsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            // Act
            var result = await controller.GetProducts();
            var result_test = result as OkNegotiatedContentResult<List<ProductModel>>;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result_test.Content.Count,78);

        }
    }
}
