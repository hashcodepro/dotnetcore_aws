Feature: Poll and Write Lambda

    This feature will test the lambda by triggering it manually or by an event and verifying that all the items in the queue are polled successfully and written to a file and pushed onto an s3 bucket.

    Scenario: To verify that on triggering the lambda all items in the queue are polled and written to a file and pushed into the s3 bucket
        Given multiple order have been placed through SALT funnel
        And the order data has been pushed into the Demo Queue
        When the lambda is triggered externally\manually
        Then the data in the queue should be polled successfully
        And a file containing the order data should be placed in the Demo Bucket