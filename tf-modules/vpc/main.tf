# VPC
resource "aws_vpc" "vpc" {
  cidr_block = var.vpc_cidr_block
  enable_dns_hostnames = true
  enable_dns_support = true
}

# Subnets
# Internet Gateway for Public Subnet
resource "aws_internet_gateway" "internet_gateway" {
    vpc_id = aws_vpc.vpc.id
}

# Elastic IP for NAT
resource "aws_eip" "nat_eip" {
  vpc = true
  depends_on = [
    aws_internet_gateway.internet_gateway
  ]
}

resource "aws_nat_gateway" "nat_gateway" {
  allocation_id = aws_eip.nat_eip
  subnet_id = element(aws_subnet.public_subnet.*.id, 0)
}

# Public subnet
resource "aws_subnet" "public_subnet" {
  vpc_id = aws_vpc.vpc.id
  cidr_block = element(var.public_subnets_cidr_block, count.index)
  availability_zone = element(var.availability_zones, count.index)
  map_public_ip_on_launch = true  
}

# Private subnet
resource "aws_subnet" "private_subnet" {
  vpc_id = aws_vpc.vpc.id
  cidr_block = element(var.private_subnets_cidr_block, count.index)
  availability_zone = element(var.availability_zones, count.index)
  map_public_ip_on_launch = false
}

# Route table for private subnet
resource "aws_route_table" "private_route" {
  vpc_id = aws_vpc.vpc.id
}

# Route table for public subnet
resource "aws_route_table" "public_route" {
  vpc_id = aws_vpc.vpc.id
}

# Route for Internet Gateway
resource "aws_route" "public_internet_gateway" {
  route_table_id = aws_route_table.public_route.id
  destination_cidr_block = "0.0.0.0/0"
  gateway_id = aws_internet_gateway.internet_gateway.id
}

# Route for NAT
resource "aws_route" "private_nat_gateway" {
  route_table_id = aws_route_table.private_route.id
  destination_cidr_block = "0.0.0.0/0"
  nat_gateway_id = aws_nat_gateway.nat_gateway.id
}

# Route table association for public subnet
resource "aws_route_table_association" "public" {
  subnet_id = element(aws_subnet.public_subnet.*.id, count.index)
  route_table_id = aws_route_table.public_route.id
}

# Route table association for private subnet
resource "aws_route_table_association" "private" {
  subnet_id = element(aws_subnet.private_subnet.*.id, count.index)
  route_table_id = aws_route_table.private_route.id
}

#Security Group
resource "aws_security_group" "security_group" {
  name = "${var.name} Security group"
  description = "Default security group"
  vpc_id = aws_vpc.vpc.id
  depends_on = [
    aws_vpc.vpc
  ]
}