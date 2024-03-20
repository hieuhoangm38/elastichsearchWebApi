using Nest;
using System.Threading.Tasks;
using System;
using Elasticsearch.Net;

namespace ElasticsearchAPI.Services
{
    public class ElasticsearchService
    {
        private readonly ElasticClient _elasticClient;

        public ElasticsearchService()
        {
            var uri = new Uri("http://localhost:9200"); // Địa chỉ của Elasticsearch trong Docker
            var connectionSettings = new ConnectionSettings(uri);
            _elasticClient = new ElasticClient(connectionSettings);
        }

        public async Task IndexDocumentAsync<T>(T document, string indexName, string id) where T : class
        {
            var indexResponse = await _elasticClient.IndexAsync(document, i => i
                .Index(indexName)
                .Id(id)
                .Refresh(Refresh.WaitFor)
            );

            if (!indexResponse.IsValid)
            {
                if (indexResponse.ServerError != null)
                {
                    // Log thông tin lỗi từ server
                    Console.WriteLine($"Elasticsearch Server Error: {indexResponse.ServerError.Error}");
                }
                else if (indexResponse.OriginalException != null)
                {
                    // Log thông tin lỗi từ exception
                    Console.WriteLine($"Exception: {indexResponse.OriginalException.Message}");
                }
                else
                {
                    // Log thông tin lỗi khác nếu có
                    Console.WriteLine("Unknown error occurred while indexing document.");
                }
            }
        }
            
        public async Task<T> GetDocumentByIdAsync<T>(string indexName, string id) where T : class
        {
            var response = await _elasticClient.GetAsync<T>(id, g => g.Index(indexName));
            return response.Source;
        }
    }
}
