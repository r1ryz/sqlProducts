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
using System.Windows.Shapes;
using QRCoder;
using SkiaSharp;


namespace sql
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private void GenerateQRCodeButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение ввода текста из текстовых полей
            string text1 = txtName.Text;
            string text2 = txtmoney.Text;
            string text3 = txtopis.Text;

            // Объедините вводимые тексты в одну строку
            string text = $"{text1}, {text2}, {text3}";

            // Сгенерируйте растровое изображение QR-кода с помощью библиотеки QRCoder
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            System.Drawing.Bitmap qrCodeBitmap = qrCode.GetGraphic(20);

            // Преобразование растрового изображения QR-кода в растровое изображение WPF
            BitmapImage qrCodeImage = new BitmapImage();
            qrCodeImage.BeginInit();
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            qrCodeBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
            memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
            qrCodeImage.StreamSource = memoryStream;
            qrCodeImage.EndInit();

            // Отображение изображения QR-кода в элементе управления изображением WPF
            QRCodeImage.Source = qrCodeImage;
        }
        public User User { get; private set; }
        public UserWindow(User user)
        {
            InitializeComponent();
            User = user;
            DataContext = User;
            GenerateQRCodeButton_Click(null, null);
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}