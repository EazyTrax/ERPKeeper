using ERPKeeperCore.Enterprise.Models.Security;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ERPKeeperCore.Enterprise.Models.Storage
{
    public class NoteItem
    {
        [Key]
        public Guid Id { get; set; }
        public Guid TopicId { get; set; }
        public DateTime Date { get; set; }
        public string? Note { get; set; }
        public string? PreNote { get; set; }
        public Guid MemberId { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }

        private const string ApiKey = "";

        public async Task UpdateFormalTextAsync()
        {
            var prompt = $"Rewrite more formal and  correct spell:\n{this.Note}";
            try
            {
                var response = await GetChatCompletionResponse(prompt);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var updatedNote = ParseResponse(responseBody);

                    if (!string.IsNullOrEmpty(updatedNote))
                    {
                        Note = updatedNote;
                        Console.WriteLine($"Updated Note: {updatedNote}");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        private async Task<HttpResponseMessage> GetChatCompletionResponse(string prompt)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);

            var request = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            return await httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", request);
        }

        private string ParseResponse(string responseBody)
        {
            try
            {
                var jsonDoc = JsonDocument.Parse(responseBody);

                // Extract the content of the response
                var messageContent = jsonDoc
                    .RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                return messageContent?.Trim() ?? string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing response: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
