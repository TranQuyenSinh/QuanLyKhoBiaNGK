namespace QuanLyKhoBiaNGK.Models
{
    public class PhiieuNhapHang
    {
        public int Id { get; set; }
        public string MaPhieu { get; set; }
        public DateTime NgayNhap { get; set; }
        public int NhaCungCapId { get; set; }
        public List<ChiTietPhieuNhap> ChiTietPhieuNhap { get; set; }
    }

    public class ChiTietPhieuNhap
    {
        public int Id { get; set; }
        public int SanPhamId { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }
    }
}
