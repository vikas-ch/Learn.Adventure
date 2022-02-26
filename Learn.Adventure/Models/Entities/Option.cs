using System;
using System.Collections.Generic;
using Learn.Adventure.Attributes;
using Learn.Adventure.Models.Abstractions;
using Learn.Adventure.Models.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Learn.Adventure.Models.Entities
{
    [Collection("option")]
    public class Option : Document
    {
        public ObjectId AdventureId { get; set; }
        // public Node Node { get; set; }
        public ObjectId ParentId { get; set; }
        public string Text { get; set; }
    }

    
}   