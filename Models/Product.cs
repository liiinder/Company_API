using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Company_API.Models;

[BsonIgnoreExtraElements]
public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string? Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required bool Status { get; set; }
    public Category? Category { get; set; }
}
