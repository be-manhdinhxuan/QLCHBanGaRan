using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QLCHBanGaRan.Utilities
{
    public class LoadingAnimation
    {
        private Bitmap _bitmap;
        private int _dotCount = 5; // Số lượng chấm
        private int _currentPosition = 0; // Vị trí hiện tại của hiệu ứng
        private int _dotSize = 10; // Kích thước chấm
        private int _spacing = 15; // Khoảng cách giữa các chấm
        private Color _dotColor = Color.White; // Màu chấm

        public LoadingAnimation(int width, int height)
        {
            _bitmap = new Bitmap(width, height);
            StartAnimation();
        }

        public void UpdateFrame()
        {
            using (Graphics g = Graphics.FromImage(_bitmap))
            {
                g.Clear(Color.Transparent); // Nền trong suốt
                g.SmoothingMode = SmoothingMode.AntiAlias; // Tăng mượt mà khi vẽ
                for (int i = 0; i < _dotCount; i++)
                {
                    int x = (i * _spacing) + 5; // Điều chỉnh vị trí x để cân đối
                    int opacity = CalculateOpacity(i); // Độ mờ dựa trên vị trí
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(opacity, _dotColor)))
                    {
                        g.FillEllipse(brush, x, _bitmap.Height / 2 - _dotSize / 2, _dotSize, _dotSize);
                    }
                }
                _currentPosition = (_currentPosition + 2) % (_spacing); // Tăng tốc độ di chuyển
            }
        }

        public Bitmap GetBitmap()
        {
            return _bitmap; // Trả về bitmap hiện tại
        }

        private int CalculateOpacity(int index)
        {
            int relativePosition = (_currentPosition + (index * _spacing)) % (_dotCount * _spacing);
            int maxOpacity = 255; // Độ mờ tối đa (0-255)
            int cycleLength = _dotCount * _spacing;
            int opacity = maxOpacity - (Math.Abs(relativePosition - cycleLength / 2) * maxOpacity / (cycleLength / 2));
            return Math.Max(50, Math.Min(maxOpacity, opacity)); // Giới hạn độ mờ
        }

        public void StartAnimation()
        {
            _currentPosition = 0;
        }
    }
}