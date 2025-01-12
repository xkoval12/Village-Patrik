

using var client = new HttpClient();

try
{
    var testUri = new Uri("http://localhost:5555/hello");
    var response = await client.GetAsync(testUri);

    // Ensure the response was successful
    response.EnsureSuccessStatusCode();
    
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine(content);
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
