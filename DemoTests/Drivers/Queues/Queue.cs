using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace DemoTests.Drivers.Queues
{
    public class Queue
    {
        string queueUrl, serviceUrl, accessKey, secretKey;
        BasicAWSCredentials credentials;
        AmazonSQSConfig sqsConfig;
        AmazonSQSClient client;
        public async Task<SendMessageResponse> publish(string orderData)
        {
            var sendRequest = new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = orderData
            };

            return await client.SendMessageAsync(sendRequest);
        }

        public async Task<GetQueueAttributesResponse> getQueueAttributes()
        {
            var request = new GetQueueAttributesRequest
            {
                QueueUrl = queueUrl,
                AttributeNames = new List<string>() { "All" }
            };

            return await client.GetQueueAttributesAsync(request);
        }
    }
}