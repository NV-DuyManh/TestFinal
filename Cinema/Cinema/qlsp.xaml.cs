using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Cinema
{
    public partial class qlsp : Page // Đã chuyển thành Page chuẩn chỉnh
    {
        // Khởi tạo chìa khóa kết nối Database
        DBRapPhimEntities db = new DBRapPhimEntities();

        public qlsp()
        {
            InitializeComponent();
            LoadData(); // Gọi hàm hiển thị dữ liệu ngay khi mở trang
        }

        // --- HÀM TẢI DỮ LIỆU LÊN BẢNG ---
        void LoadData()
        {
            try
            {
                dgProducts.ItemsSource = db.sanpham.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // 1. SỰ KIỆN: NÚT THÊM MỚI (Trị lỗi btnAdd_Click)
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sanpham moi = new sanpham();
                moi.ten_san_pham = txtName.Text;
                moi.gia_ban = decimal.Parse(txtPrice.Text);
                moi.loai = cmbCategory.Text;

                db.sanpham.Add(moi);
                db.SaveChanges();

                LoadData(); // Cập nhật lại bảng
                MessageBox.Show("Thêm thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message, "Lỗi");
            }
        }

        // 2. SỰ KIỆN: NÚT LÀM MỚI (Trị lỗi btnClear_Click)
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtId.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtName.Focus();
        }

        // 3. SỰ KIỆN: NÚT CẬP NHẬT (Trị lỗi btnEdit_Click)
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Tạm thời để trống cho khỏi báo lỗi, sau này bạn viết code Sửa vào đây
            MessageBox.Show("Chức năng Sửa đang được hoàn thiện!");
        }

        // 4. SỰ KIỆN: NÚT XÓA BỎ (Trị lỗi btnDelete_Click)
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Tạm thời để trống cho khỏi báo lỗi, sau này bạn viết code Xóa vào đây
            MessageBox.Show("Chức năng Xóa đang được hoàn thiện!");
        }

        // 5. SỰ KIỆN: CHỌN DÒNG TRÊN BẢNG (Trị lỗi dgProducts_SelectionChanged)
        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Tạm thời để trống để dập lỗi. 
            // Sau này viết code để khi click vào dòng, dữ liệu tự nhảy lên các ô Textbox
        }
    }
}