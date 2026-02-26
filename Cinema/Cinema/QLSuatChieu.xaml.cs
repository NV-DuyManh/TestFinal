using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cinema
{
    /// <summary>
    /// Interaction logic for QLSuatChieu.xaml
    /// </summary>
    public partial class QLSuatChieu : Page
    {
        DBRapPhimEntities db = new DBRapPhimEntities();
        public QLSuatChieu()
        {
            InitializeComponent();
            LoadDuLieuBieuMau();
            LoadDanhSachSuatChieu();
        }
        // --- HÀM TẢI DỮ LIỆU TỪ CƠ SỞ DỮ LIỆU ---
        private void LoadDuLieuBieuMau()
        {
            try
            {
                // 1. Tải dữ liệu cho Form thêm mới (Giữ nguyên)
                cmb_Phim.ItemsSource = db.phim.ToList();
                cmb_Phim.DisplayMemberPath = "ten_phim";
                cmb_Phim.SelectedValuePath = "ma_phim";

                cmb_Phong.ItemsSource = db.phongchieu.ToList();
                cmb_Phong.DisplayMemberPath = "ten_phong";
                cmb_Phong.SelectedValuePath = "ma_phong";

                // 2. CODE MỚI: TẢI DANH SÁCH RẠP CHO CỘT LỌC BÊN TRÁI
                var danhSachLoc = db.phongchieu.ToList();

                // Chế thêm một mục "Tất cả phòng" ảo dán lên đầu danh sách (cho nó mã = 0)
                phongchieu tatCa = new phongchieu();
                tatCa.ma_phong = 0;
                tatCa.ten_phong = "Tất cả phòng";
                danhSachLoc.Insert(0, tatCa);

                lst_LocPhong.ItemsSource = danhSachLoc;
                lst_LocPhong.DisplayMemberPath = "ten_phong";
                lst_LocPhong.SelectedValuePath = "ma_phong";

                // Mặc định chọn dòng đầu tiên ("Tất cả phòng") khi vừa mở trang
                lst_LocPhong.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadDanhSachSuatChieu()
        {
            try
            {
                var danhSachLich = db.lichchieu.ToList();
                dtg_suat_chieu.ItemsSource = danhSachLich;
                txt_ket_qua.Text = $"Đang hiển thị {danhSachLich.Count} suất chiếu.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu DataGrid: " + ex.Message);
            }
        }

        // --- CHỨC NĂNG THÊM SUẤT CHIẾU (LƯU VÀO SQL) ---
        private void btn_LuuNhanh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmb_Phim.SelectedValue == null || cmb_Phong.SelectedValue == null || dp_NgayChieu.SelectedDate == null)
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ Phim, Phòng và Ngày chiếu!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                lichchieu lcMoi = new lichchieu();
                lcMoi.ma_phim = (int)cmb_Phim.SelectedValue;
                lcMoi.ma_phong = (int)cmb_Phong.SelectedValue;
                lcMoi.ngay_chieu = dp_NgayChieu.SelectedDate.Value;

                lcMoi.gio_bat_dau = TimeSpan.Parse(txt_GioChieu.Text);
                lcMoi.gia_ve_co_ban = decimal.Parse(txt_GiaVe.Text);

                lcMoi.nguoi_lap_lich = 1;

                db.lichchieu.Add(lcMoi);
                db.SaveChanges();

                MessageBox.Show("Tạo suất chiếu mới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadDanhSachSuatChieu();
                pnl_NhapLieu.Visibility = Visibility.Collapsed;
            }
            catch (FormatException)
            {
                MessageBox.Show("Giờ chiếu (VD: 19:30) hoặc Giá vé nhập sai định dạng!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                Exception rootCause = ex;
                while (rootCause.InnerException != null)
                {
                    rootCause = rootCause.InnerException;
                }
                MessageBox.Show("Lỗi chi tiết từ SQL Server:\n\n" + rootCause.Message, "Phát hiện lỗi Database", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        // --- CHỨC NĂNG XÓA SUẤT CHIẾU ---
        private void btn_xoa_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            lichchieu suatChieuCanXoa = btn.DataContext as lichchieu;

            if (suatChieuCanXoa != null)
            {
                MessageBoxResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa suất chiếu phim '{suatChieuCanXoa.phim.ten_phim}' lúc {suatChieuCanXoa.gio_bat_dau} không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.lichchieu.Remove(suatChieuCanXoa);
                        db.SaveChanges();
                        LoadDanhSachSuatChieu();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                    }
                }
            }
        }

        // --- CÁC HÀM XỬ LÝ GIAO DIỆN---
        private void txt_tim_kiem_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_tim_kiem.Text == "🔍 Tìm kiếm suất chiếu...")
            {
                txt_tim_kiem.Text = "";
                txt_tim_kiem.Foreground = Brushes.Black;
            }
        }

        private void txt_tim_kiem_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_tim_kiem.Text))
            {
                txt_tim_kiem.Text = "🔍 Tìm kiếm suất chiếu...";
                txt_tim_kiem.Foreground = Brushes.Gray;
            }
        }

        private void btn_hien_form_them_Click(object sender, RoutedEventArgs e)
        {
            pnl_NhapLieu.Visibility = Visibility.Visible;
        }

        private void btn_HuyNhap_Click(object sender, RoutedEventArgs e)
        {
            pnl_NhapLieu.Visibility = Visibility.Collapsed;
        }

        private void lst_LocPhong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lst_LocPhong.SelectedValue != null)
                {
                    int maPhongChon = (int)lst_LocPhong.SelectedValue;

                    if (maPhongChon == 0) // Nếu người dùng bấm "Tất cả phòng"
                    {
                        LoadDanhSachSuatChieu(); // Gọi lại hàm tải toàn bộ
                    }
                    else // Nếu bấm vào rạp cụ thể (Rạp 1, Rạp 2...)
                    {
                        // Dùng Entity Framework (Where) để lọc đúng mã phòng
                        var danhSachDaLoc = db.lichchieu.Where(x => x.ma_phong == maPhongChon).ToList();
                        dtg_suat_chieu.ItemsSource = danhSachDaLoc;

                        // Cập nhật câu thông báo phía dưới
                        txt_ket_qua.Text = $"Đang hiển thị {danhSachDaLoc.Count} suất chiếu của Rạp {maPhongChon}.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc phòng chiếu: " + ex.Message);
            }
        }

        private void FastEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btn_LuuNhanh_Click(sender, e);
            }
        }

    }
}
