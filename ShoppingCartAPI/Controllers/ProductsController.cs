using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPI.Models;
using MongoDB.Driver;
using MongoDB.Bson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingCartAPI.Controllers
{
    //[EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        [HttpGet]
        public List<Product> GetProducts()
        {
            try
            {
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.products_collection =
                Models.MongoHelper.database.GetCollection<Models.Product>("Products");
                var productDocuments = Models.MongoHelper.products_collection.Find(_ => true).ToList();
                return productDocuments;
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("single")]
        public Product GetProductById(ObjectId _id)
        {

            try
            {
                Models.MongoHelper.ConnectToMongoService();
                var filter = Builders<Models.Product>.Filter.Eq("_id", _id);
                Product product = Models.MongoHelper.database.GetCollection<Models.Product>("Products").FindSync(filter).Single();

                return product;
            }
            catch
            {
                return null;
            }
        }


        [HttpGet("category")]
        public List<Product> GetProductsByCategory(string categoryName)
        {
            try
            {
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.products_collection =
                    Models.MongoHelper.database.GetCollection<Models.Product>("Products");

                var filter = Builders<Models.Product>.Filter.Eq("Category", categoryName);
                var result = Models.MongoHelper.products_collection.FindSync(filter).ToList();
                return result;
            }
            catch
            {
                return null;
            }
        }


        [HttpPost]
        public Product Post([FromBody] Product product)
        {
            try
            {
                Models.MongoHelper.ConnectToMongoService();


                Models.MongoHelper.database.GetCollection<Models.Product>("Products").InsertOneAsync(product);

                return product;
            }
            catch
            {
                return null;
            }
        }

        [HttpPut("{_id}")]
        public Product Put(String _id,[FromBody] Product product)
        {
            var parsedObjectId = new ObjectId(_id);
            try
            {
                Models.MongoHelper.ConnectToMongoService();
                var filter = Builders<Models.Product>.Filter.Eq("_id", parsedObjectId);

                Models.MongoHelper.database.GetCollection<Models.Product>("Products").ReplaceOneAsync(filter, product);

                return product;
            }
            catch
            {
                return null;
            }
        }

        [HttpDelete("{_id}")]
        public string Delete(string _id)
        {
            var parsedObjectId = new ObjectId(_id);
            try
            {
                Models.MongoHelper.ConnectToMongoService();
                var filter = Builders<Models.Product>.Filter.Eq("_id", parsedObjectId);

                Models.MongoHelper.database.GetCollection<Models.Product>("Products").DeleteOne(filter);

                return "success";
            }
            catch
            {
                return "Error";
            }
        }
    }
}
