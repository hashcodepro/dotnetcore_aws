echo "Create Queue"
aws --endpoint-url=http://localhost:4566 sqs create-queue --queue-name demo-queue
aws --endpoint-url=http://localhost:4566 sqs create-queue --queue-name sample-queue

echo "Get Queue Attributes"
aws --endpoint-url=http://localhost:4566 sqs get-queue-attributes --queue-url http://localhost:4566/000000000000/demo-queue --attribute-names All

echo "Create Role"
aws --endpoint-url=http://localhost:4566 iam create-role --role-name lambda-dotnet-ex \
	--assume-role-policy-document '{"Version": "2012-10-17", "Statement": [{ "Effect": "Allow", "Principal": {"Service": "lambda.amazonaws.com"}, "Action": "sts:AssumeRole"}]}'

echo "Create Bucket"
aws --endpoint-url=http://localhost:4566 s3 mb s3://demo-bucket

echo "Attach Policy to Role"
aws --endpoint-url=http://localhost:4566 iam attach-role-policy \
	--role-name lambda-dotnet-ex \
	--policy-arn arn:aws:iam::aws:policy/service-role/AWSLambdaBasicExecutionRole

# aws --endpoint-url=http://localhost:4566 iam attach-role-policy \
# 	--role-name lambda-dotnet-ex \
# 	--policy-arn arn:aws:iam::aws:policy/service-role/AWSLambdaSQSQueueExecuionRole

echo "List Attached Role Policies"
aws --endpoint-url=http://localhost:4566 iam list-attached-role-policies --role-name lambda-dotnet-ex

echo "Create Lambda"
aws --endpoint-url=http://localhost:4566 lambda create-function --function-name SQSTriggerAndWrite \
	--zip-file fileb://DemoProject/src/DemoProject/bin/Debug/netcoreapp3.1/publish/DemoProject.zip \
	--handler DemoProject::DemoProject.Function::SQSTriggerAndWrite \
	--runtime dotnetcore3.1 \
	--role arn:aws:iam::000000000000:role/lambda-dotnet-ex

aws --endpoint-url=http://localhost:4566 lambda create-function --function-name PollAndWrite \
	--zip-file fileb://DemoProject/src/DemoProject/bin/Debug/netcoreapp3.1/publish/DemoProject.zip \
	--handler DemoProject::DemoProject.Function::PollAndWrite \
	--runtime dotnetcore3.1 \
	--role arn:aws:iam::000000000000:role/lambda-dotnet-ex

echo "SQS-Trigger-Lambda create-event-source-mapping"
aws --endpoint-url=http://localhost:4566 lambda create-event-source-mapping \
    --function-name SQSTriggerAndWrite \
    --batch-size 5 \
    --event-source-arn arn:aws:sqs:eu-west-1:000000000000:demo-queue
