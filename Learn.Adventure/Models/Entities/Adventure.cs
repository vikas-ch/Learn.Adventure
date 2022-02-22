using System.Collections.Generic;
using Learn.Adventure.Attributes;
using Learn.Adventure.Models.Abstractions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Learn.Adventure.Models.Entities
{
    [Collection("adventure")]
    public class Adventure : Document
    {
        public string AdventureName { get; set; }
    }
}