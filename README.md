# 🐔 QLCHBanGaRan - Ứng Dụng Quản Lý Cửa Hàng Bán Gà Rán

## 📌 Giới thiệu
**QLCHBanGaRan** là ứng dụng quản lý cửa hàng bán gà rán **FastFood Chicken Bông**, phát triển bằng **C# (Windows Forms)**.  
Ứng dụng sử dụng **MDI Layout**, kết nối **SQL Server**, hỗ trợ **phân quyền người dùng** (Admin & Nhân viên) và cung cấp các chức năng:

- 📦 Quản lý đơn hàng (gọi món, in hóa đơn)
- 🍔 Quản lý sản phẩm (đồ ăn, đồ uống, nhà cung cấp)
- 👨‍💼 Quản lý nhân sự (hồ sơ, chấm công, lương)
- 📊 Thống kê báo cáo (doanh thu, sản phẩm bán, tồn kho)
- 🗑 Quản lý danh mục đã xóa (khôi phục dữ liệu)
- 🔐 Đăng nhập/đăng xuất an toàn (AES Encryption)

---

## 🛠 Công nghệ sử dụng
| Thành phần | Mô tả |
|------------|-------|
| **Ngôn ngữ** | C# |
| **Giao diện** | Windows Forms (MDI Layout) |
| **Cơ sở dữ liệu** | SQL Server |
| **Bảo mật** | AES Encryption (`cls_Encryption`) |
| **Thư viện chính** | `System.Data.SqlClient`, `System.Text.RegularExpressions` |

---

## 📥 Cài đặt

### 1️⃣ Clone repository
git clone https://github.com/be-manhdinhxuan/QLCHBanGaRan.git
### 2️⃣ Cấu hình cơ sở dữ liệu

- Tải file QLCHBanGaRan.mdf:
  🔗 [QLCHBanGaRan.mdf](https://drive.google.com/file/d/1eSADG-tcllVO7DAY3DtMaml04yNZCoOp/view?usp=sharing)
  🔗 [QLCHBanGaRan_log.ldf](https://drive.google.com/file/d/1pyBm_RfiSXXKTeqh4-l7025ZjyKg2gc1/view?usp=sharing)

- Attach vào SQL Server:

1. Mở SQL Server Management Studio (SSMS).

2. Chuột phải Databases → Attach....

3. Chọn file .mdf và .ldf.

- Cập nhật connection string trong app.config:
<connectionStrings>
  <add name="ConnectionString" 
       connectionString="Server=YOUR_SERVER;Database=QLCHBanGaRan;Trusted_Connection=True;" />
</connectionStrings>

### 3️⃣ Mở dự án trong Visual Studio

- Mở file .sln

- Build để kiểm tra lỗi.

### 4️⃣ Chạy ứng dụng

- F5 để chạy

- Tài khoản mặc định:

- `Username`: admin

- `Password`: admin123

🚀 Cách sử dụng

1. Đăng nhập bằng tài khoản trong bảng NguoiDung (mật khẩu AES).

2. Điều hướng qua menu chính (frm_Main) → mở các form con (MDI Tabs).

3. Quản lý dữ liệu:

- Gọi món → chọn sản phẩm → in hóa đơn.

- Quản lý sản phẩm → thêm/sửa/xóa.

- Quản lý nhân sự → hồ sơ, chấm công, lương.

- Thống kê → doanh thu, tồn kho.

- Danh mục đã xóa → khôi phục dữ liệu.

## 📂 Cấu trúc dự án
```text
QLCHBanGaRan/
│── Forms/            # Form giao diện (Login, Main, Order, Personnel...)
│── lib/              # Lớp xử lý (DB, Encryption, EmployeeManagement...)
│── Resources/        # Hình ảnh, icon, logo...
│── Database Script/  # Script SQL (bảng, stored procedure, trigger...)
│── QLCHBanGaRan.sln  # Solution file
```

⚠ Lưu ý

- Dự án sử dụng xóa mềm (IsDeleted = 1) để giữ dữ liệu.

- Phân quyền:

Phân quyền:
  - `LaQuanTri = 1` → **Admin** (đầy đủ quyền)
  - `LaQuanTri = 0` → **Nhân viên** (giới hạn quyền)


- Kết nối DB phải cấu hình đúng trong cls_DatabaseManager.

- Có thể mở rộng bảo mật (HTTPS, chống SQL Injection).

## 👤 Tác giả

**Mạnh Đinh Xuân**  
🔗 [GitHub](https://github.com/be-manhdinhxuan)

## 📜 Giấy phép

MIT License – Tự do sử dụng, chỉnh sửa, phân phối nhưng giữ nguyên bản quyền.

## 💡 Nếu cần hỗ trợ hoặc muốn đóng góp, hãy tạo issue trên GitHub repository!
