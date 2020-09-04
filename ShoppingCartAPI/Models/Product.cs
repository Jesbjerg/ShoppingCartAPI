using System;
using MongoDB.Bson;

namespace ShoppingCartAPI.Models
{
    public class Product
    {
            public ObjectId _id { get; set; }
            
            public String Name { get; set; }

            public Double Price { get; set; }

            public String Description { get; set; }

            public String Category { get; set; }

       
    }
}
