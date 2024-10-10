using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace bad4.Services
{
    [BsonIgnoreExtraElements] // This attribute ensures that any extra elements in MongoDB that are not mapped in the class are ignored.
    public class LogEntry
    {
        [BsonElement("Properties")] // Maps the 'Properties' field in MongoDB to the LogProperties class.
        public LogProperties Properties { get; set; }

        [BsonElement("UtcTimeStamp")] // Stores the timestamp of the log entry in UTC format.
        [BsonRepresentation(BsonType.DateTime)] // Represents this field as a BSON DateTime type in MongoDB.
        public DateTime UtcTimeStamp { get; set; }
    }

    [BsonIgnoreExtraElements] // Ignores unmapped elements when deserializing LogProperties from MongoDB.
    public class LogProperties
    {
        [BsonElement("logInfo")] // Maps the 'logInfo' field in MongoDB to the LogInfo class.
        public LogInfo LogInfo { get; set; }

        [BsonElement("RequestPath")] // Logs the path of the HTTP request that triggered this log entry.
        public string RequestPath { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class LogInfo
    {
        [BsonElement("Operation")] // The operation being logged, e.g., "Login", "Update".
        public string Operation { get; set; }

        [BsonElement("Date")] // The date the operation occurred, stored as a string.
        public string Date { get; set; }

        [BsonElement("UserInfo")] // Information about the user that triggered this log entry.
        public UserInfo UserInfo { get; set; }

        [BsonElement("Claim")] // Stores the user's claims, e.g., roles or permissions.
        public Claim[] Claims { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class UserInfo
    {
        [BsonElement("Email")] // The email address of the user triggering the log.
        public string Email { get; set; }

        [BsonElement("UserName")] // The username of the user triggering the log.
        public string UserName { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class Claim
    {
        [BsonElement("Type")] // The type of the claim, e.g., "Role".
        public string Type { get; set; }

        [BsonElement("Value")] // The value of the claim, e.g., "Admin".
        public string Value { get; set; }
    }
}
