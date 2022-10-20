Based on:
https://github.com/aws-samples/amazon-cloudfront-secure-static-site/blob/master/templates/main.yaml

Create s3 for CF:
aws s3 mb s3://static-name-cf-bucket --profile personal

Package:
aws cloudformation package --template-file main.yaml --s3-bucket static-name-cf-bucket --output-template-file packaged.template --profile personal

Create stack:
aws cloudformation deploy --template-file packaged.template --stack-name static-site --parameter-overrides file://parameters/qa.json --profile personal --capabilities CAPABILITY_NAMED_IAM CAPABILITY_IAM CAPABILITY_AUTO_EXPAND

Delete stack:
aws cloudformation delete-stack --stack-name static-site --profile personal





