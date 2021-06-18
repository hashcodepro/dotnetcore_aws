using DemoTests.Drivers.Buckets;
using DemoTests.Drivers.Lambdas;
using DemoTests.Drivers.Queues;
using DemoTests.TestData;
using TechTalk.SpecFlow;
using Xunit;

namespace DemoTests.Steps
{
    [Binding]
    public class PollAndWriteSteps
    {
        TestDataRepository _testDataRepo;
        TestDataContext _testDataContext;
        SampleQueue _sampleQueue;
        DemoBucket _demoBucket;
        PollAndWriteLambda _lambda;
        public PollAndWriteSteps(
            TestDataRepository testDataRepo,
            TestDataContext testDataContext,
            SampleQueue sampleQueue,
            DemoBucket demoBucket,
            PollAndWriteLambda lambda
        )
        {
            _testDataRepo = testDataRepo;
            _testDataContext = testDataContext;
            _sampleQueue = sampleQueue;
            _demoBucket = demoBucket;
            _lambda = lambda;
        }

        [Given("multiple order have been placed through SALT funnel")]
        public void GivenMultipleOrdersHaveBeenPlaced()
        {
            _testDataContext.multipleOrders = _testDataRepo.multipleOrders;
        }

        [Given("the order data has been pushed into the Demo Queue")]
        public async void GivenOrderDataHasBeenPushedToQueue()
        {
            await _sampleQueue.publish(_testDataContext.multipleOrders);
        }

        [When("the lambda is triggered externally\\manually")]
        public async void WhenTheLambdaIsTriggered()
        {
            Assert.Equal((await _lambda.invoke()).StatusCode,200);
        }

        [Then("the data in the queue should be polled successfully")]
        public async void ThenTheDataInQueueIsPolled()
        {
            Assert.Equal((await _sampleQueue.getQueueAttributes()).ApproximateNumberOfMessages, 0);
        }

        [Then("a file containing the order data should be placed in the Demo Bucket")]
        public async void ThenFileContainingOrderShouldBePlacedInBucket()
        {
            Assert.Equal((await _demoBucket.listFiles()).MaxKeys, 1);
        }
    }
}