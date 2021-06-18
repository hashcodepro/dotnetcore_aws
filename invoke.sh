echo "Invoke Lambda"
aws --endpoint-url=http://localhost:4566 lambda invoke \
	--invocation-type RequestResponse \
	--function-name PollAndWrite \
	--payload "" \
	response.json \
	--log-type Tail

echo "Send Message"
aws --endpoint-url=http://localhost:4566 sqs send-message --queue-url http://localhost:4566/000000000000/demo-queue --message-body "test message : sent another message helllloooo"

# echo "Receive Message"
# aws --endpoint-url=http://localhost:4566 sqs receive-message --queue-url http://localhost:4566/000000000000/demo-queue --attribute-names All --message-attribute-names All --max-number-of-messages 10