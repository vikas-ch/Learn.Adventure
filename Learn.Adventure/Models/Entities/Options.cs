using System;
using System.Collections.Generic;
using Learn.Adventure.Models.Abstractions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Learn.Adventure.Models.Entities
{
    public class Options : Document
    {
        [BsonRepresentation(BsonType.String)]
        public ObjectId AdventureId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public ObjectId ParentId { get; set; }
        public string Text { get; set; }
    }
}   