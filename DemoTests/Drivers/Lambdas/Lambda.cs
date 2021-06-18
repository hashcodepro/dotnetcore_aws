using System.Threading.Tasks;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;

namespace DemoTests.Drivers.Lambdas
{
    public class Lambda
    {
        string functionName, serviceUrl, accessKey, secretKey, region;
        BasicAWSCredentials credentials;
        AmazonLambdaConfig lambdaConfig;
        AmazonLambdaClient client;

        public async Task<InvokeResponse> invoke()
        {
            InvokeRequest request = new InvokeRequest
            {
                FunctionName = functionName,
                InvocationType = InvocationType.RequestResponse
            };

            return await client.InvokeAsync(request);
        }
    }
}