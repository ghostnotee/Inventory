using System.Drawing;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Inventory.Data.Settings;

public static class DocumentMapping
{
    public static void MappingClasses()
    {
        BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(b => b.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
        });

        BsonClassMap.RegisterClassMap<Product>(cm =>
        {
            cm.AutoMap();
            cm.MapMember(p => p.Company).SetSerializer(new EnumSerializer<Companies>(BsonType.String));
            cm.UnmapMember(p => p.Category);
            cm.UnmapMember(p => p.Brand);
        });
    }
}