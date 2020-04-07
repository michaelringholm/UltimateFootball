using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using dk.commentor.starterproject.dal.cosmos;
using Microsoft.Azure.Cosmos;

namespace dk.fp.pinfo.dal.cosmos
{
    public class CosmosDBFacade<T> {
        private string endpoint;
        private string key;
        private string databaseId;
        private string containerId;
        private Database database;
        public Container Container {get;private set;}
        private CosmosClient client;
        
        private int throughput;
        private readonly Func<T, string> idSelector;
        private readonly string prefix;                

        public CosmosDBFacade(string endpoint, string key, string databaseId, string containerId, Func<T,String> idSelector, int throughput = 400, string partitionKey = "/partitionKey")
        {
            this.endpoint = endpoint;
            this.key = key;
            this.databaseId = databaseId;
            this.containerId = containerId;
            this.throughput = throughput;
            this.idSelector = idSelector;
            this.prefix = typeof(T).Name;
            client = new CosmosClient(endpoint, key, new CosmosClientOptions {ConnectionMode = ConnectionMode.Direct });            
            var databaseResponse = client.CreateDatabaseIfNotExistsAsync(databaseId).ConfigureAwait(false).GetAwaiter().GetResult();
            if((databaseResponse.StatusCode != HttpStatusCode.OK) && (databaseResponse.StatusCode != HttpStatusCode.Created)) throw new Exception($"Database creation failed with status code {databaseResponse.StatusCode}");
            database = databaseResponse.Database;
            var containerResponse = database.CreateContainerIfNotExistsAsync(containerId, partitionKey, throughput).ConfigureAwait(false).GetAwaiter().GetResult();
            if((containerResponse.StatusCode != HttpStatusCode.OK) && (containerResponse.StatusCode != HttpStatusCode.Created)) throw new Exception($"Container creation failed with status code {containerResponse.StatusCode}");
            Container = containerResponse.Container;
        }

        private string generateCosmosId(T i)
        {
            return $"{prefix}_{idSelector(i)}";
        }

        private List<CosmosItem> ConvertToCosmosItems(List<T> items)
        {
            if(items?.Count <= 0) return null;
            return items.Select(i => new CosmosItem{ id=generateCosmosId(i), Data = i }).ToList();
        }

        private CosmosItem ConvertToCosmosItem(T item)
        {
            return new CosmosItem{ id=generateCosmosId(item), Data = item };
        }        

        public async Task CreateItemAsync(T item)
        {
            var cosmosItem = ConvertToCosmosItem(item);
            await Container.CreateItemAsync(cosmosItem);
        }

        public void BulkCreate(List<T> items, int bulkSize)
        {
            var cosmosItems = ConvertToCosmosItems(items);
            var tasks = new List<Task<ItemResponse<CosmosItem>>>();
            foreach(var cosmosItem in cosmosItems) {
                var task = Container.CreateItemAsync(cosmosItem);
                tasks.Add(task);
                if(tasks.Count%bulkSize==0) {
                    Task.WaitAll(tasks.ToArray());    
                    tasks.Clear();
                }
            }           
            Task.WaitAll(tasks.ToArray());
        }

        public async Task<List<T>> GetItems(string query, string partitionKey, int maxItemCount) {
            var queryDefinition = new QueryDefinition(query);
            var requestOptions = new QueryRequestOptions()
            {
                //PartitionKey = new PartitionKey(partitionKey),
                MaxItemCount = maxItemCount
            };
            List<T> items = new List<T>();
            var iterator = Container.GetItemQueryIterator<T>(queryDefinition, requestOptions: requestOptions);
            while(iterator.HasMoreResults) {
                var nextItems = (await iterator.ReadNextAsync()).ToList();
                items.AddRange(nextItems);
            }
            return items;
        }

        public async Task<T> GetItem(string query, string partitionKey) {
            var queryDefinition = new QueryDefinition(query);
            var requestOptions = new QueryRequestOptions()
            {
                //PartitionKey = new PartitionKey(partitionKey),
                MaxItemCount = 1
            };
            var iterator = Container.GetItemQueryIterator<T>(queryDefinition, requestOptions: requestOptions);
            if(iterator.HasMoreResults) {
                var item = (await iterator.ReadNextAsync()).First();
                return item;
            }
            return default(T);
        }        

        public Task<DatabaseResponse> DropDatabaseAsync()
        {
            return database.DeleteAsync();
        }        
    }
}