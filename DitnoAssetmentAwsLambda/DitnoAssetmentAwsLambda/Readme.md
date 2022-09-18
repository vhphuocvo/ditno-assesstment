# AWS Lambda Function Project

Task 1 – Lambda function to calculate weekdays between two dates
Create a Lambda function to calculate business days between two dates
Assume weekdays are Monday to Friday
The returned count should exclude the from date and to date
Example:
- From Wed 4 August 2021 to Fri 6 August 2021 should return 1
- From Mon 2 August 2021 to Thu 12 August 2021 should return 7
Your function should:
• be written in C#
• include unit tests to demonstrate the correctness of the solution
• be easy to understand and maintain
• be considerate of performance
• the core calculation should not use any external libraries, and only use .NET built-in
assemblies. (AWS and unit test related libraries are acceptable)

Task 2 – Terraform code to deploy the Lambda function
Write Terraform code to deploy the Lambda function and associated infrastructure.
This should include the following:
• VPC configuration with subnets in multiple availability zones
• Lambda function configuration
• Security group(s)
• IAM role(s)