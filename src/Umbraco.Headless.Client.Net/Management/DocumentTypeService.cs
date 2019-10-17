using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    internal class DocumentTypeService : IDocumentTypeService
    {
        private readonly IHeadlessConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private DocumentTypeManagementEndpoints _restService;

        public DocumentTypeService(IHeadlessConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        private DocumentTypeManagementEndpoints Service =>
            _restService ?? (_restService = RestService.For<DocumentTypeManagementEndpoints>(_httpClient));

        public async Task<IEnumerable<DocumentType>> GetAll()
        {
            var root = await Service.GetRoot(_configuration.ProjectAlias);
            return root.DocumentTypes.Items;
        }

        public async Task<DocumentType> GetByAlias(string alias) =>
            await Service.ByAlias(_configuration.ProjectAlias, alias);
    }
}