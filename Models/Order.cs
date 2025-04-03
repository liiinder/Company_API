using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Company_API.Models;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string? Id { get; set; }
    public string CustomerId { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; }
}

