using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace pr7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Doctor doctor;
        public MainWindow()
        {
            InitializeComponent();
            doctor = new Doctor();
            pegistr.DataContext = doctor;
            vhod.DataContext = doctor;
            print.DataContext = doctor;
        }
        private bool NullStr()
        {
           
            if (doctor.LastName == null)
            {
                MessageBox.Show("Поле 'ID' обязательно для заполнения.");
                return false;
            }
            if (doctor.Name == null)
            {
                MessageBox.Show("Поле 'Name' обязательно для заполнения.");
                return false;
            }
            if (doctor.MiddleName == null)
            {
                MessageBox.Show("Поле 'MiddleName' обязательно для заполнения.");
                return false;
            }
            if (doctor.Specialisation == null)
            {
                MessageBox.Show("Поле 'Specialisation' обязательно для заполнения.");
                return false;
            }
            if (doctor.Password == null)
            {
                MessageBox.Show("Поле 'Password' обязательно для заполнения.");
                return false;
            }
            if (doctor.Password2 == null)
            {
                MessageBox.Show("Поле 'Password2' обязательно для заполнения.");
                return false;
            }
            if (doctor.Password != doctor.Password2)
            {
                MessageBox.Show("Пароли не совпадают!!!");
                return false;
            }
            return true;
        }
        private bool NullStr2()
        {
            if (doctor.Id == null)
            {
                MessageBox.Show("Поле 'ID' обязательно для заполнения.");
                return false;
            }
            if (doctor.Password == null)
            {
                MessageBox.Show("Поле 'Password' обязательно для заполнения.");
                return false;
            }
            return true;
        }


            private void Button_Click( object sender, RoutedEventArgs e )
             {
            Random random = new Random();
            doctor.Id = random.Next(10000, 100000);
            if (!NullStr())
                return;
            FileDoc();
            var jsonString=JsonSerializer.Serialize( doctor );
            var newDoctor= JsonSerializer.Deserialize<Doctor>( jsonString );
            MessageBox.Show($"Вы зарегистировались-{newDoctor.Name}{newDoctor.LastName}{newDoctor.MiddleName}{newDoctor.Specialisation}");
            MessageBox.Show($"Ваш ID-{newDoctor.Id}");
             }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            if (!NullStr2())
                return;
            string fileName = $"{doctor.Id}.json";
            if (!File.Exists(fileName))
            {
                MessageBox.Show($"Нет такого октора с ID= {doctor.Id}");
                return;
            }
            //var jsonString = JsonSerializer.Serialize(doctor);
            string jsonString = File.ReadAllText(fileName);
            var newDoctor = JsonSerializer.Deserialize<Doctor>(jsonString);
            if (newDoctor.Password != doctor.Password)
            {
                MessageBox.Show("Неверный пароль.");
                return;
            }
            MessageBox.Show($"Вы вошли-{doctor.Id}");
        }
        private void FileDoc()
        {
            try
            {
                var jsonString = JsonSerializer.Serialize(doctor);
                string fileName = $"{doctor.Id}.json";
                File.WriteAllText(fileName, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}");
            }
        }
    }
}