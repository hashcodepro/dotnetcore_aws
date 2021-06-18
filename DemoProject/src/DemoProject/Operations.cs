using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SQS;
using Amazon.SQS.Model;
using Amazon.Runtime;
using Amazon.Lambda.Core;

namespace DemoProject
{
    public class Operations
    {
        string localstack_hostname;
        string bucketName;
        string queueName;
        public Operations()
        {
            localstack_hostname = Environment.GetEnvironmentVariable("LOCALSTACK_HOSTNAME");
            bucketName = "demo-bucket";
            queueName = "sample-queue";
        }

        public BasicAWSCredentials GetCreds()
        {
            return new BasicAWSCredentials("asdf", "zxcv");
        }

        public async Task<string> ReadFromQueue(ILambdaContext context)
        {
            StringBuilder sb = new StringBuilder();

            var sqsClient = new AmazonSQSClient(GetCreds(),
                new AmazonSQSConfig
                {
                    ServiceURL = $"http://{localstack_hostname}:4566"
                });

            ReceiveMessageResponse sqsResponse = await sqsClient.ReceiveMessageAsync(
                new ReceiveMessageRequest
                {
                    QueueUrl = $"http://{localstack_hostname}:4566/000000000000/{queueName}",
                    MaxNumberOfMessages = 10
                });

            foreach (var item in sqsResponse.Messages)
                sb.AppendLine(item.Body);

            return sb.ToString();
        }

        public async Task<PutObjectResponse> PushToBucket(string input, string prefix)
        {
            var s3Client = new AmazonS3Client(GetCreds(),
                new AmazonS3Config
                {
                    ServiceURL = $"http://{localstack_hostname}:4566",
                    ForcePathStyle = true
                });

            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            byte[] byteArray = Encoding.ASCII.GetBytes(input);
            MemoryStream stream = new MemoryStream(byteArray);
            stream.Position = 0;
            PutObjectRequest putRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = $"{prefix}_{timestamp}.txt",
                InputStream = stream
            };

            PutObjectResponse response = await s3Client.PutObjectAsync(putRequest);

            return response;
        }
    }
}