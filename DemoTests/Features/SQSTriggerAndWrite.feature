Feature: SQS Trigger And Write Lambda

    This feature will test the lambda that would be triggered when an item is pushed into an sqs queue

    Scenario: To verify when data is pushed into the queue the lambda is triggered and a new file is created and pushed into an s3 bucket
        Given an order is placed successfully from SALT Funnel
        When the order data is pushed into the queue
        Then the lambda should be triggered
        And a file should be created with order data and placed into the s3 bucket