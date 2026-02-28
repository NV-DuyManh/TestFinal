using System;
using System.Windows;
using System.Windows.Controls;

namespace Cinema
{
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        // Quản lý phim
        private void BtnPhim_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng Quản lý phim đang được xây dựng!", "Thông báo");
        }

        // Quản lý sản phẩm (ĐÃ SỬA CHỖ NÀY)
        private void BtnSanPham_Click(object sender, RoutedEventArgs e)
        {
            // Xóa dòng MessageBox báo lỗi và gọi thẳng trang qlsp (Quản lý bắp nước)
            MainFrame.Navigate(new qlsp());
        }

        // Quản lý suất chiếu
        private void BtnSuatChieu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng Quản lý suất chiếu đang được xây dựng!", "Thông báo");
        }

        // Quản lý tài khoản
        private void BtnTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng Quản lý tài khoản đang được xây dựng!", "Thông báo");
        }
    }
}