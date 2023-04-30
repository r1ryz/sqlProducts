using Microsoft.EntityFrameworkCore;
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

namespace sql
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db = new ApplicationContext();
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            db.Database.EnsureCreated();

            db.Users.Load();

            DataContext= db.Users.Local.ToObservableCollection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserWindow UserWindow = new UserWindow(new User());
            if (UserWindow.ShowDialog() == true) 
            {
                User User = UserWindow.User;
                db.Users.Add(User);
                db.SaveChanges();
            
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            User? user = usersList.SelectedItem as User;
            if (user is null) return;

            UserWindow userWindow = new UserWindow(new User
            {
                Id= user.Id,
                Age= user.Age,
                Name= user.Name,
                Email= user.Email

            });

            if (userWindow.ShowDialog() == true)
            {
                user = db.Users.Find(userWindow.User.Id);
                if (user != null)
                {
                    user.Age = userWindow.User.Age;
                    user.Name = userWindow.User.Name;
                    user.Email = userWindow.User.Email;
                    db.SaveChanges();
                    usersList.Items.Refresh();
                }
            }       
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            User? user = usersList.SelectedItem as User;
            // если ни одного объекта не выделено, выходим
            if (user is null) return;
            db.Users.Remove(user);
            db.SaveChanges();

        }
    }
}
