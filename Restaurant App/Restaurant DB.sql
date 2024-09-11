
create database RestaurantDB
use RestaurantDB

Create table TblFood (fid Int Primary key Identity(1,1),Fname varchar(50) unique,Ftype varchar(20)Not Null,
				Fprice Money check (Fprice>0),Favailable varchar(30)Not Null)

select * from TblFood

Create table TblBilling (BillNo INT Primary key Identity(1000,1),BillDate DATETIME DEFAULT getdate(),Fid INT FOREIGN KEY REFERENCES TblFood (Fid),
				Fname varchar(50),Price MONEY CHECK (price>0),Quantity INT DEFAULT 1,Amount MONEY CHECK (Amount>0))

select * from TblBilling