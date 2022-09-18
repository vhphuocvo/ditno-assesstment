variable "name" {
  type        = string
  description = "The VPC name"
}

variable "vpc_cidr_block" {
  type        = string
  description = "CIDR block of the VPC"
}

variable "public_subnets_cidr_block" {
  type        = list
  description = "CIDR block for Public Subnet"
}

variable "private_subnets_cidr_block" {
  type        = list
  description = "CIDR block for Private Subnet"
}

variable "availability_zones" {
  type        = list
  description = "Availability zones of all the resources will be deployed"
}