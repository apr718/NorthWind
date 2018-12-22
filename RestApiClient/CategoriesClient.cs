using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NorthWindAPIClient.Models;

namespace RestApiClient
{
    public interface ICategoriesClient
    {
        Task<List<Category>> GetCategories();
    }
    public class CategoriesClient : ICategoriesClient
    {
        private HttpClient _client;
      
        public CategoriesClient(HttpClient client)
        {
            _client = client;
        }
        //public async Task<List<Category>> GetCategories()
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        //Blah blah do everything here I want to do. 
        //        //var result = await client.GetAsync("/tweets");
        //        var response = await _client.GetAsync("api/categories");
        //        response.EnsureSuccessStatusCode();

        //        return await response.Content.ReadAsAsync<IEnumerable<Category>>(); 
        //    }
        //}
        public Task<List<Category>> GetCategories()
        {
            throw new NotImplementedException();
        }
    }
}
