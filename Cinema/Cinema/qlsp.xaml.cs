using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Cinema
{
    public partial class qlsp : Page
    {
        // Bước 1: Kết nối đúng tên Database bạn vừa gửi
        DBRapPhimEntities db = new DBRapPhimEntities();

        public qlsp()
        {
            InitializeComponent();
            LoadData();
        }

        // Hàm tải dữ liệu lên bảng
        void LoadData()
        {
            try
            {
                // Gọi đúng bảng sanpham trong database
                dgProducts.ItemsSource = db.sanpham.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị danh sách: " + ex.Message);
            }
        }

        // Sự kiện khi bấm nút Thêm Mới
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sanpham moi = new sanpham(); // Phải dùng đúng class sanpham
                moi.ten_san_pham = txtName.Text;
                moi.gia_ban = decimal.Parse(txtPrice.Text);
                moi.loai = cmbCategory.Text;

                db.sanpham.Add(moi);
                db.SaveChanges(); // Lưu vào Database

                LoadData(); // Cập nhật lại bảng
                MessageBox.Show("Thêm thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message);
            }
        }

        // Sự kiện Làm mới ô nhập
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtId.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtName.Focus();
        }

        // Các hàm Sửa/Xóa bạn viết tương tự nhưng nhớ dùng db.sanpham...
        private void btnEdit_Click(object sender, RoutedEventArgs e) { /* Code sửa */ }
        private void btnDelete_Click(object sender, RoutedEventArgs e) { /* Code xóa */ }
        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e) { /* Code chọn dòng */ }
    }
}