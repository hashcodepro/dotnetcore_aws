

using System.IO;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;

namespace DemoTests.Drivers.Buckets
{
    public class DemoBucket
    {

        string bucketName, serviceUrl, accessKey, secretKey;
        BasicAWSCredentials credentials;
        AmazonS3Config s3Config;
        AmazonS3Client client;


        public DemoBucket(IConfiguration config)
        {
            bucketName = config.GetValue<string>("AWS:S3:DemoBucket");
            serviceUrl = config.GetValue<string>("AWS:ServiceUrl");
            accessKey = config.GetValue<string>("AWS:AccessKey");
            secretKey = config.GetValue<string>("AWS:SecretKey");

            credentials = new BasicAWSCredentials(accessKey, secretKey);
            s3Config = new AmazonS3Config
            {
                ServiceURL = serviceUrl
            };
            client = new AmazonS3Client(credentials, s3Config);
        }

        public async Task<ListObjectsResponse> listFiles()
        {
            ListObjectsRequest listRequest = new ListObjectsRequest
            {
                BucketName = bucketName
            };
            return (await client.ListObjectsAsync(listRequest));
        }

        public async Task<string> getFirstFile()
        {
            ListObjectsResponse bucketItems = await listFiles();
            GetObjectRequest getObjectRequest = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = bucketItems.S3Objects[0].Key
            };
            GetObjectResponse getObjectResponse = await client.GetObjectAsync(getObjectRequest);
            StreamReader stream = new StreamReader(getObjectResponse.ResponseStream);

            return stream.ReadToEnd();
        }
    }
}