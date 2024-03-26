# Hướng dẫn cấu hình và chạy ứng dụng Movie Management

## Bước 1: Khởi động SQL Server

- Đảm bảo rằng SQL Server đã được cài đặt trên thiết bị của bạn.
- Khởi động SQL Server để chuẩn bị cho việc tạo và quản lý cơ sở dữ liệu.

## Bước 2: Chạy file Data Migration

- Mở SQL Server Management Studio (SSMS) hoặc công cụ quản lý SQL Server khác.
- Chạy file Data Migration để tạo hoặc cập nhật cơ sở dữ liệu cho ứng dụng Movie Management.

## Bước 3: Thay đổi chuỗi kết nối trong mã nguồn

- Mở solution của ứng dụng Movie Management trong một trình biên tập mã nguồn như Visual Studio.
- Tìm và mở file MovieManagementContext.cs hoặc tệp chứa chuỗi kết nối đến cơ sở dữ liệu.
- Tìm đến phần trong mã nguồn liên quan đến chuỗi kết nối, thường nằm trong phương thức OnConfiguring() của DbContext.
- Thay đổi server name trong chuỗi kết nối thành tên máy chủ của thiết bị của bạn.

    Ví dụ:
    Trước khi chỉnh sửa:
    ```
    "Server=LAPTOP-1P79QFBT; Trusted_Connection=Yes; Initial Catalog=MovieManagement; TrustServerCertificate=True"
    ```
    Sau khi chỉnh sửa:
    ```
    "Server=Your-SERVER; Trusted_Connection=Yes; Initial Catalog=MovieManagement; TrustServerCertificate=True"
    ```

## Bước 4: Chạy ứng dụng

- Chạy ứng dụng Movie Management từ môi trường phát triển như Visual Studio hoặc từ tệp thực thi sau khi xây dựng.
- Đảm bảo rằng ứng dụng kết nối đến cơ sở dữ liệu mà bạn đã cấu hình.

