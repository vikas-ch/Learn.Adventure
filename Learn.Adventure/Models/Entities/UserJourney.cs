using System.Collections.Generic;
using Learn.Adventure.Attributes;
using Learn.Adventure.Models.Abstractions;
using Microsoft.VisualBasic;
using MongoDB.Bson;

namespace Learn.Adventure.Models.Entities
{
    [Collection("user-journey")]
    public class UserJourney : Document
    {
        public ObjectId UserId { get; set; }
        public ObjectId AdventureId { get; set; }
        // public UserSelectedNode Node { get; set; }
        public List<string> SelectedOptions { get; set; }
    }
}