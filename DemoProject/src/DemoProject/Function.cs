using System;
using System.Text;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.S3.Model;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DemoProject
{
    public class Function
    {

        /// <summary>
        /// A simple function that reads data from a queue and writes to an s3 bucket
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task PollAndWrite(ILambdaContext context)
        {
            Operations ops = new Operations();

            context.Logger.LogLine("Reading from queue...");
            string text = await ops.ReadFromQueue(context);

            context.Logger.LogLine("Creating file and pushing to s3 bucket...");
            PutObjectResponse response = await ops.PushToBucket(text, "manualInvoke");

            context.Logger.LogLine($"Put Object Response - Status Code : {response.HttpStatusCode}");
            context.Logger.LogLine("Function name: " + context.FunctionName);
            context.Logger.LogLine("RemainingTime: " + context.RemainingTime);
            context.Logger.LogLine("LogGroupName: " + context.LogGroupName);
        }

        public async Task SQSTriggerAndWrite(SQSEvent sqsEvent, ILambdaContext context)
        {
            context.Logger.LogLine($"Beginning to process {sqsEvent.Records.Count} records...");
            context.Logger.LogLine($"Record Count : {sqsEvent.Records.Count}");

            StringBuilder sb = new StringBuilder();
            Operations ops = new Operations();

            foreach (var record in sqsEvent.Records)
            {
                context.Logger.LogLine($"Message ID: {record.MessageId}");
                context.Logger.LogLine($"Event Source: {record.EventSource}");

                sb.AppendLine(record.Body);
            }

            PutObjectResponse response = await ops.PushToBucket(sb.ToString(), "sqsTrigger");

            context.Logger.LogLine($"Response Status Code : {response.HttpStatusCode}");
            context.Logger.LogLine("Function name: " + context.FunctionName);
            context.Logger.LogLine("RemainingTime: " + context.RemainingTime);
            context.Logger.LogLine("LogGroupName: " + context.LogGroupName);
            context.Logger.LogLine("Processing complete.");
        }
    }
}
