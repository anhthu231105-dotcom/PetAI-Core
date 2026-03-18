-- 1. DỌN DẸP HỆ THỐNG (Ngắt kết nối để xóa DB cũ)
USE master;
GO
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'PetAI_Core_DB')
BEGIN
    ALTER DATABASE PetAI_Core_DB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE PetAI_Core_DB;
END
GO

-- 2. TẠO DATABASE MỚI
CREATE DATABASE PetAI_Core_DB;
GO
USE PetAI_Core_DB;
GO
-- 2. TẠO CÁC BẢNG QUẢN LÝ CƠ BẢN
CREATE TABLE Employee (
    EmployeeID VARCHAR(20) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Position NVARCHAR(50),
    PhoneNumber VARCHAR(15)
);

-- BẢNG USERS PHẢI CÓ ĐỦ EMAIL VÀ EMPLOYEEID ĐỂ KHÔNG BỊ LỖI C#
CREATE TABLE Users (
    UserID VARCHAR(50) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL,
    Role NVARCHAR(20),
    Email NVARCHAR(100), -- Thêm Email để hết lỗi 'Invalid column name Email'
    EmployeeID VARCHAR(20) FOREIGN KEY REFERENCES Employee(EmployeeID)
);

CREATE TABLE Customer (
    CustomerID VARCHAR(20) PRIMARY KEY,
    CustomerName NVARCHAR(100) NOT NULL,
    Phone VARCHAR(15),
    Address NVARCHAR(200)
);

CREATE TABLE Pet (
    PetID VARCHAR(20) PRIMARY KEY,
    PetName NVARCHAR(100) NOT NULL,
    Species NVARCHAR(50), 
    Breed NVARCHAR(50),
    CustomerID VARCHAR(20) FOREIGN KEY REFERENCES Customer(CustomerID),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 3. QUẢN LÝ Y TẾ & AI
CREATE TABLE Disease (
    DiseaseID VARCHAR(20) PRIMARY KEY,
    DiseaseName NVARCHAR(100) NOT NULL,
    MainSymptom NVARCHAR(MAX),
    MedicalAdvice NVARCHAR(MAX),
    DangerLevel NVARCHAR(50)
);

CREATE TABLE HealthRecord (
    RecordID INT IDENTITY(1,1) PRIMARY KEY,
    PetID VARCHAR(20) FOREIGN KEY REFERENCES Pet(PetID),
    Temperature FLOAT,
    HeartRate INT,
    DiagnosisID VARCHAR(20) FOREIGN KEY REFERENCES Disease(DiseaseID),
    PredictionDate DATETIME DEFAULT GETDATE()
);

-- 4. QUẢN LÝ KHO THUỐC & DỊCH VỤ
CREATE TABLE Medicine (
    MedicineID VARCHAR(20) PRIMARY KEY,
    MedicineName NVARCHAR(100) NOT NULL,
    Unit NVARCHAR(20),
    Price DECIMAL(18, 2),
    StockQuantity INT
);

CREATE TABLE Service (
    ServiceID VARCHAR(20) PRIMARY KEY,
    ServiceName NVARCHAR(100) NOT NULL,
    Price DECIMAL(18, 2)
);

-- 5. QUẢN LÝ HÓA ĐƠN VÀ LƯU TRÚ
CREATE TABLE Invoice (
    InvoiceID VARCHAR(20) PRIMARY KEY,
    PetID VARCHAR(20) FOREIGN KEY REFERENCES Pet(PetID),
    EmployeeID VARCHAR(20) FOREIGN KEY REFERENCES Employee(EmployeeID),
    TotalAmount DECIMAL(18, 2),
    PaymentDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE InvoiceDetail (
    DetailID INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceID VARCHAR(20) FOREIGN KEY REFERENCES Invoice(InvoiceID),
    ServiceID VARCHAR(20) FOREIGN KEY REFERENCES Service(ServiceID),
    MedicineID VARCHAR(20) FOREIGN KEY REFERENCES Medicine(MedicineID),
    Quantity INT,
    SubTotal DECIMAL(18, 2)
);

CREATE TABLE StayRegistration (
    StayID VARCHAR(20) PRIMARY KEY,
    PetID VARCHAR(20) FOREIGN KEY REFERENCES Pet(PetID),
    EmployeeID VARCHAR(20) FOREIGN KEY REFERENCES Employee(EmployeeID),
    CheckInDate DATETIME DEFAULT GETDATE(),
    ExpectedCheckOutDate DATETIME,
    ActualCheckOutDate DATETIME,
    StayReason NVARCHAR(MAX),
    DailyStatus NVARCHAR(MAX),
    UnitPrice DECIMAL(18, 2) DEFAULT 0,
    TotalStayAmount AS (DATEDIFF(DAY, CheckInDate, ISNULL(ActualCheckOutDate, GETDATE())) * UnitPrice),
    Status NVARCHAR(50)
);
GO
-- 1. CHÈN DỮ LIỆU NHÂN VIÊN (Employee)
INSERT INTO Employee (EmployeeID, FullName, Position, PhoneNumber) VALUES 
('NV001', N'Nguyễn Anh Thư', N'Quản lý hệ thống', '0901234567'),
('NV002', N'Lê Minh bác sĩ', N'Bác sĩ thú y', '0907654321');

-- 2. CHÈN DỮ LIỆU TÀI KHOẢN (Users)
-- Tài khoản này giúp bạn vào được Form Main mà không bị báo lỗi sai Pass/User
INSERT INTO Users (UserID, Username, Password, Role, Email, EmployeeID) VALUES 
('U001', 'admin', '123', 'Admin', 'admin@petai.com', 'NV001'),
('U002', 'doctor1', '123', 'Doctor', 'doctor@petai.com', 'NV002');

-- 3. CHÈN DỮ LIỆU KHÁCH HÀNG & THÚ CƯNG
INSERT INTO Customer (CustomerID, CustomerName, Phone, Address) VALUES 
('KH001', N'Trần Minh Tâm', '0911223344', N'Ninh Kiều, Cần Thơ'),
('KH002', N'Lê Thị Hoa', '0944556677', N'An Giang');

INSERT INTO Pet (PetID, PetName, Species, Breed, CustomerID) VALUES 
('P001', N'Lu Lu', N'Chó', N'Poodle', 'KH001'),
('P002', N'Mimi', N'Mèo', N'Anh lông ngắn', 'KH002'),
('P003', N'Bống', N'Chó', N'Corgi', 'KH001');

-- 4. DANH MỤC BỆNH LÝ (Dành cho tính năng AI quét triệu chứng)
INSERT INTO Disease (DiseaseID, DiseaseName, MainSymptom, MedicalAdvice, DangerLevel) VALUES 
('D001', N'Sốt siêu vi', N'Mệt mỏi, Biếng ăn, Nhiệt độ cao > 39 độ', N'Cho bé uống nhiều nước và nghỉ ngơi.', N'Trung bình'),
('D002', N'Sốc nhiệt', N'Nhiệt độ cơ thể > 40 độ, thở gấp, chảy dãi', N'Làm mát cơ thể bằng khăn ẩm và đưa đi cấp cứu ngay.', N'Rất cao'),
('D003', N'Viêm da (Nấm)', N'Ngứa ngáy, rụng lông theo mảng, da đỏ', N'Vệ sinh da bằng xà phòng chuyên dụng.', N'Thấp');

-- 5. DANH MỤC DỊCH VỤ VÀ THUỐC
INSERT INTO Service (ServiceID, ServiceName, Price) VALUES 
('S001', N'Khám tổng quát (AI)', 50000),
('S002', N'Gói Spa chăm sóc', 200000),
('S003', N'Tiêm chủng định kỳ', 150000);

INSERT INTO Medicine (MedicineID, MedicineName, Unit, Price, StockQuantity) VALUES 
('M001', N'Thuốc hạ sốt Pet', N'Viên', 15000, 100),
('M002', N'Kháng sinh viêm da', N'Gói', 25000, 50);

-- 6. DỮ LIỆU LƯU TRÚ (Test bảng StayRegistration)
INSERT INTO StayRegistration (StayID, PetID, EmployeeID, CheckInDate, ExpectedCheckOutDate, StayReason, DailyStatus, UnitPrice, Status)
VALUES 
('ST001', 'P001', 'NV002', '2026-03-10', '2026-03-20', N'Điều trị sốt siêu vi', N'Ngày 1: Ăn ít. Ngày 2: Đã ổn định hơn.', 120000, N'Đang lưu trú'),
('ST002', 'P002', 'NV001', '2026-03-12', '2026-03-15', N'Gửi chăm sóc khi chủ vắng nhà', N'Sức khỏe rất tốt.', 100000, N'Đang lưu trú');
GO
-- 1. Thêm cột Weight (Kiểu float để khớp với double trong C#)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Pet') AND name = 'Weight')
BEGIN
    ALTER TABLE Pet ADD Weight FLOAT NULL;
END
GO

-- 2. Thêm cột Age (Kiểu int để khớp với int trong C#)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Pet') AND name = 'Age')
BEGIN
    ALTER TABLE Pet ADD Age INT NULL;
END
GO
-- Thay 'Pet' bằng tên bảng mà Thư đang dùng để lưu thông tin thú cưng nhé
ALTER TABLE Pet 
ADD Note nvarchar(MAX);
-- Cập nhật thử một dòng để test hiển thị trên C#
UPDATE Pet SET Note = N'Bé Poodle khỏe mạnh' WHERE PetID = 'P001';