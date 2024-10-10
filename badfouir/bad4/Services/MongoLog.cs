using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Serilog;

namespace bad4.Services
{
    public class LogService
    {
        private readonly IMongoCollection<LogEntry> _logCollection; // The MongoDB collection for storing logs.
        private readonly string connectionString = "mongodb://mongo"; // The connection string for MongoDB.
        private readonly string BakeryDb = "admin"; // The name of the MongoDB database.
        private readonly string collection = "log"; // The name of the MongoDB collection for logs.

        public LogService()
        {
            var client = new MongoClient(connectionString); // Establish connection to MongoDB.
            var database = client.GetDatabase(BakeryDb); // Retrieve the 'admin' database.
            _logCollection = database.GetCollection<LogEntry>(collection); // Retrieve the 'log' collection.
        }

        public async Task<List<LogEntry>> GetAsync()
        {
            // Fetch all log entries from the MongoDB collection.
            var s = _logCollection.Find(_ => true);
            var count = s.CountDocuments(); // Counts the total number of documents.
            return await s.ToListAsync(); // Returns the list of log entries asynchronously.
        }

        public async Task<List<LogEntry>> GetLogs(string user = null, string from = null, string to = null, string operation = null, string claimType = null)
        {
            var filterBuilder = Builders<LogEntry>.Filter; // Creates a MongoDB filter builder.
            var filter = filterBuilder.Empty; // Initializes an empty filter.

            // Filter by username if provided.
            if (!string.IsNullOrEmpty(user))
            {
                filter &= filterBuilder.Eq("Properties.logInfo.UserInfo.UserName", user);
            }

            // Filter by date range if 'from' or 'to' are provided.
            if (!string.IsNullOrEmpty(from))
            {
                DateTime fromDate;
                if (DateTime.TryParse(from, out fromDate))
                {
                    // If 'to' is not provided, set it to the current datetime.
                    if (string.IsNullOrEmpty(to))
                    {
                        to = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
                    }

                    DateTime toDate;
                    if (DateTime.TryParse(to, out toDate))
                    {
                        filter &= filterBuilder.Gte("UtcTimeStamp", fromDate) & // Filter by greater than or equal to 'fromDate'.
                                  filterBuilder.Lte("UtcTimeStamp", toDate); // Filter by less than or equal to 'toDate'.
                    }
                    else
                    {
                        throw new ArgumentException("Invalid 'to' date format.");
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid 'from' date format.");
                }
            }
            else if (!string.IsNullOrEmpty(to))
            {
                DateTime toDate;
                if (DateTime.TryParse(to, out toDate))
                {
                    DateTime januaryFirst = new DateTime(2024, 1, 1);
                    filter &= filterBuilder.Gte("UtcTimeStamp", januaryFirst) & // Filter from January 1, 2024.
                              filterBuilder.Lte("UtcTimeStamp", toDate);
                }
                else
                {
                    throw new ArgumentException("Invalid 'to' date format.");
                }
            }

            // Filter by operation type if provided.
            if (!string.IsNullOrEmpty(operation))
            {
                filter &= filterBuilder.Eq("Properties.logInfo.Operation", operation);
            }

            // Filter by claim type if provided.
            if (!string.IsNullOrEmpty(claimType))
            {
                filter &= filterBuilder.ElemMatch("Properties.logInfo.Claims", Builders<Claim>.Filter.Eq("Type", claimType));
            }

            var logs = _logCollection.Find(filter); // Query MongoDB with the constructed filter.

            return await logs.ToListAsync(); // Return the filtered logs asynchronously.
        }
    }
}
