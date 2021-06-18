using System.Threading.Tasks;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;

namespace DemoTests.Drivers.Lambdas
{
    public class PollAndWriteLambda : Lambda
    {
        string functionName, serviceUrl, accessKey, secretKey, region;
        BasicAWSCredentials credentials;
        AmazonLambdaConfig lambdaConfig;
        AmazonLambdaClient client;

        public PollAndWriteLambda(IConfiguration config)
        {
            functionName = config.GetValue<string>("AWS:Lambda:PollAndWrite");
            region = config.GetValue<string>("AWS:Region");
            serviceUrl = config.GetValue<string>("AWS:ServiceUrl");
            accessKey = config.GetValue<string>("AWS:AccessKey");
            secretKey = config.GetValue<string>("AWS:SecretKey");

            credentials = new BasicAWSCredentials(accessKey, secretKey);
            lambdaConfig = new AmazonLambdaConfig
            {
                ServiceURL = serviceUrl,
                RegionEndpoint = Amazon.RegionEndpoint.EUWest1
            };
            client = new AmazonLambdaClient(credentials, lambdaConfig);
        }
    }
}