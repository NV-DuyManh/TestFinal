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

        // 1. Quản lý phim
        private void BtnPhim_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng Quản lý phim đang xây dựng!");
        }

        // 2. Quản lý sản phẩm (ĐÃ SỬA)
        private void BtnSanPham_Click(object sender, RoutedEventArgs e)
        {
            // Thay vì .Show(), ta điều hướng Frame tới trang qlsp
            // Lưu ý: Nếu qlsp là Window, bạn phải đổi nó thành Page mới dùng cách này được
            MainFrame.Navigate(new qlsp());
        }

        // 3. Quản lý suất chiếu
        private void BtnSuatChieu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng Quản lý suất chiếu đang xây dựng!");
        }

        // 4. Quản lý tài khoản
        private void BtnTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng Quản lý tài khoản đang xây dựng!");
        }
    }
}