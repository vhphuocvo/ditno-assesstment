provider "aws" {
  region = var.aws_region
}

# Create S3 bucket to store source code.
resource "aws_s3_bucket" "lambda_bucket" {
  bucket = var.assetment_bucket_name
  acl = "private"
  force_destroy = true
}

# Create Assetment Lambda function
module "ditno-assetment-lambda" {
  source = "./tf-modules/lambda-function"
  lambda_bucket_id = aws_s3_bucket.lambda_bucket.id
  publish_dir = "${path.module}/DitnoCalculateBusinessDay/DitnoCalculateBusinessDay/bin/Release/net6.0/linux-x64/publish"
  zip_file = "DitnoCalculateBusinessDay.zip"
  function_name = "DitnoCalculateBusinessDay"
  lambda_handler = "DitnoCalculateBusinessDay::DitnoCalculateBusinessDay.Function::FunctionHandler"
}

# Create VPC configuration with subnets in multiple availability zones
module "vpc" {
  source = "./tf-modules/vpc"
  name = "VPC"
  availability_zones = ["${var.aws_region}-a", "${var.aws_region}-b", "${var.aws_region}-c"]
  vpc_cidr_block = "10.0.0.0/16"
  public_subnets_cidr_block = ["10.0.32.0/24"]
  private_subnets_cidr_block = ["10.0.128.0/19"]
}