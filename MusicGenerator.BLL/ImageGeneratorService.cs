using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using MusicGenerator.BLL.Models;
using OpenAI.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MusicGenerator.BLL
{
    public static class ImageGeneratorService
    {
        
        public static async Task<string> GenerateImageAsync(MusicItem result, IConfiguration configuration)
        {
            //ищем в папке wwwroot/images/ изображение, название которого совпадает с названием песни
            string imagePath = $"wwwroot/images/{result.Title} by {result.Artist} for album {result.Album}.png";
            if (File.Exists(imagePath))
            {
                return imagePath;
            }
            else
            {
                //если изображения нет, генерируем изображение через ai и
                //сохраняем его в папке wwwroot/images/ с названием, совпадающим с названием песни
                // Generate an image from a text prompt
                try
                {
                    string endpoint = configuration["AZURE_OPENAI_ENDPOINT"];
                    string apiKey = configuration["AZURE_OPENAI_API_KEY"];
                    string model = configuration["AZURE_OPENAI_GPT_NAME"];

                    // Create the Azure OpenAI client and convert to IImageGenerator.
                    AzureOpenAIClient azureClient = new(
                        new Uri(endpoint),
                        new AzureKeyCredential(apiKey));

                    var imageClient = azureClient.GetImageClient(model);
                    #pragma warning disable MEAI001 // Type is for evaluation purposes only.
                    IImageGenerator generator = imageClient.AsIImageGenerator();

                    var options = new Microsoft.Extensions.AI.ImageGenerationOptions
                    {
                        MediaType = "image/png"
                    };
                    string prompt = $"An image for song titled {result.Title} of album {result.Album} with artist name on it {result.Artist}";
                    var response = await generator.GenerateImagesAsync(prompt, options);

                    // Save the image to a file.
                    var dataContent = response.Contents.OfType<DataContent>().First();
                    return SaveImage(dataContent, imagePath);
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error, return a default image path, etc.)
                    Console.WriteLine($"Error generating image: {ex.Message}");
                    return "wwwroot/images/default.png"; // Return a default image path in case of error
                }
            }
        }

        private static string SaveImage(DataContent content, string imagePath)
        {
            File.WriteAllBytes(imagePath, content.Data.ToArray());
            return imagePath;
        }
    }
}
