

using Newtonsoft.Json;
using System.Web;

namespace C__HTTP
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.GetAsync("http://jsonplaceholder.typicode.com/posts");
                var json = await result.Content.ReadAsStringAsync();

                var posts = JsonConvert.DeserializeObject<List<Post>>(json);

                var selectedPost = posts?.First(p => p.Id == 30);
                Console.WriteLine($"Title : {selectedPost?.Title}");
                Console.WriteLine($"Body: {selectedPost?.Body}");

                selectedPost.Title = "test title";
                selectedPost.Body = "test body";

                Console.WriteLine($"Title : {selectedPost?.Title}");
                Console.WriteLine($"Body: {selectedPost?.Body}");

                //Console.WriteLine(posts);

                var postJsonContent = new StringContent(JsonConvert.SerializeObject(selectedPost));

                var postResult = await httpClient
                    .PostAsync("http://jsonplaceholder.typicode.com/posts", postJsonContent);


                // Własne zapytanie aby sprecyzowac naglowki i content ręcznie
                using (var postRequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://jsonplaceholder.typicode.com/posts"))
                {
                    postRequestMessage.Headers.Add("accept", "application/json");
                    //postRequestMessage.Headers.Add("someheader", "somevalue");
                    postRequestMessage.Content = postJsonContent;

                    var post2Result = await httpClient.SendAsync(postRequestMessage);
                };

                var queryParams = HttpUtility.ParseQueryString(string.Empty);
                queryParams["postId"] = "1";
                queryParams["someParam"] = "someVAlue";

                var formattedParams = queryParams.ToString();

                Console.WriteLine($"Formated params: {formattedParams}");
            }


        }
    }
}
