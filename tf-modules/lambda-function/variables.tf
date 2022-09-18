variable "publish_dir" {
  description = "The location of published files."
  type = string
}

variable "function_name" {
  description = "The name of lambda function."
  type = string
}

variable "lambda_bucket_id" {
  description = "The id of the bucket lambda function code"
  type = string
}

variable "lambda_handler" {
  description = "The Lambda handler"
  type        = string
}

variable "zip_file" {
  description = "The location of the ZIP file"
  type        = string
}