variable "assetment_bucket_name" {
  description = "Name of the S3 bucket store source code of DitnoCalculateBusinessDay."
  type = string
  default = "assetment-bucket"
}

variable "aws_region" {
  default = "us-east-1"
}