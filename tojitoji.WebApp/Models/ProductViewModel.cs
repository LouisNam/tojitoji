using System;

namespace tojitoji.WebApp.Models
{
    public class ProductViewModel
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public decimal RRP { set; get; } // Giá RRP, giá niêm yết

        public decimal SP { set; get; } // Giá SP, giá bán

        public DateTime? SpecialFromTime { set; get; }

        public DateTime? SpecialToTime { set; get; }

        public bool? Status { set; get; } // Active:1 - Inactive:0

        public string NameEn { set; get; }

        public int CategoryID { set; get; }

        public string Brand { set; get; } // Thương hiệu

        public string Model { set; get; } // Mẫu

        public string ProductCode { set; get; } // Mã sản phẩm chung

        public string ColorFamily { set; get; } // Màu sắc

        public string Size { set; get; }

        public int? ProductLifeTime { set; get; } // Thời gian sử dụng (tính theo ngày)

        public int? Warranty { set; get; } // Thời gian bảo hành (tính theo ngày)

        public string WarrantyType { set; get; } // Loại bảo hành

        public string Unit { set; get; } // Đơn vị tính

        public string PackageContent { set; get; } // Bộ sản phẩm bao gồm những gì

        public int? PackageWeight { set; get; } // Cân nặng - kg

        public int? PackageLength { set; get; } // Chiều dài - cm

        public int? PackageWidth { set; get; } // Chiều rộng - cm

        public int? PackageHeight { set; get; } // Chiều cao - cm

        public string ShortDescription { set; get; } // Đặc tính

        public string Description { set; get; } // Mô tả

        public string Origin { set; get; } // Nơi sản xuất

        public string Video { set; get; }

        public string MainImage { set; get; } // Hình chính

        public string MoreImage { set; get; } // Các hình ảnh phụ

        public string Note { set; get; }

        public virtual CategoryViewModel Category { set; get; }
    }
}