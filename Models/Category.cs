using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Company_API.Models;
public class Category
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string? Id { get; set; }
    public string Name { get; set; }
}

