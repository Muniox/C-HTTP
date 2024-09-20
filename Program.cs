namespace C__HTTP
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (var httpClient = new HttpClient())
            {
                await httpClient.GetAsync("http://jsonplaceholder.typicode.com/posts");
            }


        }
    }
}
