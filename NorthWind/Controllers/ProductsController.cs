﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NorthWind;
using NorthWind.Models;

namespace NorthWind.Controllers
{
    public class ProductsController : ApiController
    {
        private NorthwindEntities db = new NorthwindEntities();

        // GET: api/Products
        [ResponseType(typeof(Products))]
        public async Task<IHttpActionResult> GetProducts()
        {
            var products = await db.Products.Include(p => p.ProductName)
                .Select(p => new ProductModel()
                {
                    ProductId = p.ProductID,
                    ProductName = p.ProductName,
                    Discontinued = p.Discontinued,
                    QuantityPerUnit = p.QuantityPerUnit
                }).ToListAsync();
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        // GET: api/Products/5
        [ResponseType(typeof(Products))]
        public async Task<IHttpActionResult> GetProducts(int id)
        {
            //Products products = await db.Products.FindAsync(id);
            var products = await db.Products.Include(p => p.ProductName)
                .Select(p => new ProductModel()
                {
                    ProductId = p.ProductID,
                    ProductName = p.ProductName,
                    Discontinued = p.Discontinued,
                    QuantityPerUnit = p.QuantityPerUnit
                }).SingleOrDefaultAsync(p => p.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProducts(int id, Products products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != products.ProductID)
            {
                return BadRequest();
            }

            db.Entry(products).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Products))]
        public async Task<IHttpActionResult> PostProducts(Products products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(products);
            await db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = products.ProductID }, products);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Products))]
        public async Task<IHttpActionResult> DeleteProducts(int id)
        {
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            db.Products.Remove(products);
            await db.SaveChangesAsync();

            return Ok(products);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductsExists(int id)
        {
            return db.Products.Count(e => e.ProductID == id) > 0;
        }
    }
}