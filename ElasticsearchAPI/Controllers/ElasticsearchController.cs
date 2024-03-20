using ElasticsearchAPI.Log;
using ElasticsearchAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticsearchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElasticsearchController : ControllerBase
    {

        private readonly ElasticsearchService _elasticsearchService;

        public ElasticsearchController(ElasticsearchService elasticsearchService)
        {
            _elasticsearchService = elasticsearchService;
        }   

        [HttpPost("index")]
        public async Task<IActionResult> IndexDocument(MyDocument myDocument)
        {
            // Index a document
            MyDocument document = new MyDocument { Title = myDocument.Title, Content = myDocument.Content };
            await _elasticsearchService.IndexDocumentAsync(document, "hieu", "1");
            return Ok("Document indexed successfully.");
        }

        [HttpGet("index")]
        public async Task<IActionResult> GetDocumentById([FromForm]string indexName)
        {
            // Get a document by ID
            var document = await _elasticsearchService.GetDocumentByIdAsync<MyDocument>(indexName, "1");
            return Ok(document);
        }

        //public string DeviceId { get; set; }
        //public LogType LogType { get; set; }
        //public int LogAction { get; set; }
        //[Keyword]
        //public string LogValue { get; set; } // media id
        //public DateTime LogTime { get; set; }
        //public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        //public string LogDecoded { get; set; }

        //[HttpGet]
        //public async Task<IActionResult> SearchDocuments(string query)
        //{
        //    var searchResponse = await _elasticClient.SearchAsync<DeviceLog>(s => s
        //                             .Query(q => q
        //                             .Match(m => m
        //                             .Field(f => f.Name)
        //                             .Query(query))));

        //    return Ok(searchResponse.Documents);
        //}

        //[HttpDelete]
        //public async Task<IActionResult> DeleteDocument(int id)
        //{
        //    var deleteResponse = await _elasticClient.DeleteAsync<DeviceLog>(id);
        //    return Ok(deleteResponse);
        //}
    }

}
