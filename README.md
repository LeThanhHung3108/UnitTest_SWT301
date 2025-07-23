# SWP391 School Medical Management System - Hướng dẫn Setup & Chạy Unit Test

## 1. Yêu cầu hệ thống
- [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) hoặc mới hơn
- Git

## 2. Clone source code
```bash
git clone https://github.com/LeThanhHung3108/UnitTest_SWT301.git
cd UnitTest_SWT301
```

## 3. Cài đặt dependencies
Chạy lệnh sau ở thư mục gốc để khôi phục các package cần thiết:
```bash
dotnet restore
```

## 4. Build project
```bash
dotnet build
```

## 5. Chạy Unit Test
Chạy lệnh sau để thực thi toàn bộ unit test:
```bash
dotnet test SWP_SchoolMedicalManagementSystem_UnitTest/SWP_SchoolMedicalManagementSystem_UnitTest.csproj
```

Hoặc để chạy tất cả test trong solution:
```bash
dotnet test
```

## 6. Xem báo cáo coverage (nếu có)
Nếu đã cấu hình code coverage, bạn có thể xem báo cáo trong thư mục `coveragereport/`.

## 7. Một số lệnh hữu ích
- Chạy test với log chi tiết:
  ```bash
  dotnet test --logger "console;verbosity=detailed"
  ```
- Chạy test và xuất báo cáo coverage (cần cài đặt coverlet):
  ```bash
  dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
  ```

## 8. Liên hệ
Nếu gặp vấn đề khi setup hoặc chạy unit test, vui lòng liên hệ chủ repository hoặc tạo issue mới trên GitHub.
