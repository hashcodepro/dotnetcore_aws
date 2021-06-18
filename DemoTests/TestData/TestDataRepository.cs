using DemoTests.Models;
using System.Text.Json;
using System.IO;

namespace DemoTests.TestData
{
    public class TestDataRepository
    {
        string jsonString;
        string folderPath = "TestData";

        public string sampleOrder => File.ReadAllText($"{folderPath}\\SampleOrder.json");
        // public Order getSampleOrder() => JsonSerializer.Deserialize<Order>(sampleOrder);

        public string multipleOrders => File.ReadAllText($"{folderPath}\\MultipleOrders.json");
    }
}