using TechTalk.SpecFlow;
using Xunit;
using DemoTests.Models;
using DemoTests.TestData;
using DemoTests.Drivers.Queues;
using System.Threading.Tasks;
using DemoTests.Drivers.Buckets;

namespace DemoTests.Steps
{
    [Binding]
    public sealed class SQSTriggerAndWriteSteps
    {
        TestDataRepository _testDataRepo;
        TestDataContext _testDataContext;
        DemoQueue _demoQueue;
        DemoBucket _demoBucket;
        public SQSTriggerAndWriteSteps(
            TestDataRepository testDataRepo,
            TestDataContext testDataContext,
            DemoQueue demoQueue,
            DemoBucket demoBucket)
        {
            _testDataRepo = testDataRepo;
            _testDataContext = testDataContext;
            _demoQueue = demoQueue;
            _demoBucket = demoBucket;
        }

        [Given("an order is placed successfully from SALT Funnel")]
        public void GivenOrderIsPlacedFromSALT()
        {
            _testDataContext.sampleOrder = _testDataRepo.sampleOrder;
        }

        [When("the order data is pushed into the queue")]
        public async void WhenTheOrderDataIsPushedIntoTheQueue()
        {
            await _demoQueue.publish(_testDataContext.sampleOrder);
        }

        [Then("the lambda should be triggered")]
        public async void ThenTheLambdaShouldBeTriggered()
        {
            Assert.Equal((await _demoBucket.listFiles()).MaxKeys, 1);
        }

        [Then("a file should be created with order data and placed into the s3 bucket")]
        public async void ThenAFileShouldBeCreatedAndPutInS3Bucket()
        {
            var fileContent = await _demoBucket.getFirstFile();
            Assert.Equal(fileContent, _testDataContext.sampleOrder);
        }
    }
}
