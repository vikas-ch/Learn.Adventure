using System;
using MongoDB.Bson;

namespace Learn.Adventure.Models.Abstractions
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    };

        
}