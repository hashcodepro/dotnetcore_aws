using System;
using Microsoft.Extensions.Configuration;
using DemoTests.Models;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DemoTests.Drivers.Queues
{
    public class DemoQueue : Queue
    {
        string queueUrl, serviceUrl, accessKey, secretKey;
        BasicAWSCredentials credentials;
        AmazonSQSConfig sqsConfig;
        AmazonSQSClient client;

        public DemoQueue(IConfiguration config)
        {
            queueUrl = config.GetValue<string>("AWS:SQS:DemoQueue");
            serviceUrl = config.GetValue<string>("AWS:ServiceUrl");
            accessKey = config.GetValue<string>("AWS:AccessKey");
            secretKey = config.GetValue<string>("AWS:SecretKey");

            credentials = new BasicAWSCredentials(accessKey, secretKey);
            sqsConfig = new AmazonSQSConfig
            {
                ServiceURL = serviceUrl
            };
            client = new AmazonSQSClient(credentials, sqsConfig);
        }

        // public async Task<SendMessageResponse> publish(string orderData)
        // {
        //     var sendRequest = new SendMessageRequest
        //     {
        //         QueueUrl = queueUrl,
        //         MessageBody = orderData
        //     };

        //     return await client.SendMessageAsync(sendRequest);
        // }

        // public async Task<GetQueueAttributesResponse> getQueueAttributes()
        // {
        //     var request = new GetQueueAttributesRequest
        //     {
        //         QueueUrl = queueUrl,
        //         AttributeNames = new List<string>() { "All" }
        //     };

        //     return await client.GetQueueAttributesAsync(request);
        // }
    }
}
