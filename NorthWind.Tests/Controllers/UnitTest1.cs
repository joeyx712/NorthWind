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
        public async Task A_GetAllProducts_ShouldReturnAllProducts()
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
            Assert.AreEqual(result_test.Content.Count, 77);
        }

        [TestMethod]
        public async Task B_PostNewProduct_ShouldReturnNewProduct()
        {
            //Arragnge
            var controller = new ProductsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            var NewProduct = new Products()
            {
                ProductName = "Test Product",
                SupplierID = 1,
                CategoryID = 1,
                QuantityPerUnit = "Test Quantity",
                UnitPrice = 100,
                UnitsInStock = 100,
                UnitsOnOrder = 50,
                ReorderLevel = 10,
                Discontinued = false
            };
            //Act
            var result = await controller.PostProducts(NewProduct);
            var result_test = result as CreatedAtRouteNegotiatedContentResult<Products>;
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result_test.Content.ProductName, "Test Product");
        }

        [TestMethod]
        public async Task C_PutProduct_ShoudlReturnNonContent()
        {
            //Arranage
            var controller = new ProductsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            var NewProduct = new Products()
            {
                ProductID = 1,
                ProductName = "Chai Modified",
                SupplierID = 1,
                CategoryID = 1,
                QuantityPerUnit = "10 boxes x 20 bags",
                UnitPrice = 18,
                UnitsInStock = 39,
                UnitsOnOrder = 0,
                ReorderLevel = 10,
                Discontinued = false
            };
            //Act
            var result = await controller.PutProducts(1, NewProduct);
            var result_test = result as StatusCodeResult;
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result_test.StatusCode, System.Net.HttpStatusCode.NoContent);
        }

        [TestMethod]
        public async Task D_DeleteNewProduct_ShouldReturnOK()
        {
            //Arrange
            var controller = new ProductsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();
            //Act
            var result = await controller.DeleteProducts(78);
            var result_test = result as OkNegotiatedContentResult<Products>;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result_test.Content.ProductName, "Test Product");
        }
    }
}
