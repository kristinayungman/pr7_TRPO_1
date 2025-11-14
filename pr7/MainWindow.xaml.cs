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
        public Doctor regist;
        public Doctor vhodd;

        public Pacient dobavl;
        public Pacient Poiskk;

        public Itog itog;
        public MainWindow()
        {
            InitializeComponent();
            doctor = new Doctor();
            regist = new Doctor();
            vhodd = new Doctor();
            dobavl = new Pacient();
            Poiskk = new Pacient();
            itog = new Itog();

            pegistr.DataContext = regist;
            vhod.DataContext = vhodd;
            //print.DataContext = doctor;

            dobav.DataContext = dobavl;
            Poisk.DataContext = Poiskk;

            file.DataContext = itog;

        }
        private bool NullStr()
        {
           
            if (regist.LastName == null)
            {
                MessageBox.Show("Поле 'ID' обязательно для заполнения.");
                return false;
            }
            if (regist.Name == null)
            {
                MessageBox.Show("Поле 'Name' обязательно для заполнения.");
                return false;
            }
            if (regist.MiddleName == null)
            {
                MessageBox.Show("Поле 'MiddleName' обязательно для заполнения.");
                return false;
            }
            if (regist.Specialisation == null)
            {
                MessageBox.Show("Поле 'Specialisation' обязательно для заполнения.");
                return false;
            }
            if (regist.Password == 0)
            {
                MessageBox.Show("Поле 'Password' обязательно для заполнения.");
                return false;
            }
            if (regist.Password2 == 0)
            {
                MessageBox.Show("Поле 'Password2' обязательно для заполнения.");
                return false;
            }
            if (regist.Password != regist.Password2)
            {
                MessageBox.Show("Пароли не совпадают!!!");
                return false;
            }
            return true;
        }
        private bool NullStr2()
        {
            if (vhodd.Id == 0)
            {
                MessageBox.Show("Поле 'ID' обязательно для заполнения.");
                return false;
            }
            if (vhodd.Password == 0)
            {
                MessageBox.Show("Поле 'Password' обязательно для заполнения.");
                return false;
            }
            return true;
        }
        private bool NullStr3()
        {
            if (dobavl.Name == null)
            {
                MessageBox.Show("Поле 'Name' обязательно для заполнения.");
                return false;
            }
            if (dobavl.LastName == null)
            {
                MessageBox.Show("Поле 'LastName' обязательно для заполнения.");
                return false;
            }
            if (dobavl.MiddleName == null)
            {
                MessageBox.Show("Поле 'MiddleName' обязательно для заполнения.");
                return false;
            }
            /*if (!(dobavl.Birthday == DateTime.MinValue))
            {
                MessageBox.Show("Поле 'Birthday' обязательно для заполнения.");
                return false;
            }*/
            //if (dobavl.LastDoctor == 0)
            //{
            //    MessageBox.Show("Поле 'Password2' обязательно для заполнения.");
            //    return false;
            //}
            return true;
        }
        private bool NullStr4()
        {
            if (Poiskk.Id == 0)
            {
                MessageBox.Show("Поле 'ID' обязательно для заполнения.");
                return false;
            }
            return true;
        }
        private void Button_Click( object sender, RoutedEventArgs e )
             {
            Random random = new Random();
            regist.Id = random.Next(10000, 100000);
            if (!NullStr())
                return;
            FileDoc(regist);
            var jsonString=JsonSerializer.Serialize( regist );
            var newDoctor= JsonSerializer.Deserialize<Doctor>( jsonString );
            MessageBox.Show($"Вы зарегистировались-{newDoctor.Name} {newDoctor.LastName} {newDoctor.MiddleName} {newDoctor.Specialisation}");
            MessageBox.Show($"Ваш ID-{newDoctor.Id}");

            itog.IdDoc++;
            itog.IdVse++;
            file.DataContext = null;
            file.DataContext = itog;

        }

        bool vhodClick=false;
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            if (!NullStr2())
                return;
            string fileName = $"{vhodd.Id}.json";
            if (!File.Exists(fileName))
            {
                MessageBox.Show($"Нет такого октора с ID= {vhodd.Id}");
                return;
            }
            string jsonString = File.ReadAllText(fileName);
            var newDoctor = JsonSerializer.Deserialize<Doctor>(jsonString);
            if (newDoctor.Password != vhodd.Password)
            {
                MessageBox.Show("Неверный пароль.");
                return;
            }
            else
            {

                print.DataContext = newDoctor;
                doctor = newDoctor;
                MessageBox.Show($"Вы вошли-{newDoctor.Id}");
                vhodClick = true;
            }
           
        }
        private void FileDoc(Doctor savefile)
        {
            try
            {
                var jsonString = JsonSerializer.Serialize(savefile);
                string fileName = $"{savefile.Id}.json";
                File.WriteAllText(fileName, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}");
            }
        }
        private void FilePacient(Pacient savefile2)
        {
            try
            {
                var jsonString = JsonSerializer.Serialize(new Pacient { Id = savefile2.Id, Name=savefile2.Name, LastName=savefile2.LastName,MiddleName=savefile2.MiddleName,Birthday=savefile2.Birthday, LastAppointment=savefile2.LastAppointment,LastDoctor=savefile2.LastDoctor,Diagnosis=savefile2.Diagnosis,Recomendations=savefile2.Recomendations});
                string fileName = $"{savefile2.Id}.json";
                File.WriteAllText(fileName, jsonString, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}");
            }
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            //vhodd.Id = dobavl.LastDoctor;
            if (!vhodClick)
            { MessageBox.Show("Сначала войдите"); }
            else
            {
                Random random = new Random();
                dobavl.Id = random.Next(1000000, 10000000);
                if (!NullStr3())
                    return;
                FilePacient(dobavl);
                var jsonString = JsonSerializer.Serialize(dobavl);
                var newPacient = JsonSerializer.Deserialize<Pacient>(jsonString);
                MessageBox.Show($"Вы добавили пациента-{newPacient.Name} {newPacient.LastName} {newPacient.MiddleName} {newPacient.Birthday}");
                MessageBox.Show($"ID Пациента-{newPacient.Id}");

                itog.IdPac++;
                itog.IdVse++;
                file.DataContext = null;
                file.DataContext = itog;
            }
            
        }

        private void Button_Poisk(object sender, RoutedEventArgs e)
        {
            if (!vhodClick)
            { MessageBox.Show("Сначала войдите"); }
            else
            {
                if (!NullStr4())
                    return;
                string fileName = $"{Poiskk.Id}.json";
                if (!File.Exists(fileName))
                {
                    MessageBox.Show($"Нет такого пациента с ID= {Poiskk.Id}");
                    return;
                }
                string jsonString = File.ReadAllText(fileName);
                var newPacient = JsonSerializer.Deserialize<Pacient>(jsonString);

                //newPacient.LastDoctor = doctor.LastName;
                newPacient.LastDoctor = doctor.Id;
                PoiskPrint.DataContext = newPacient;
                izm.DataContext = newPacient;
                MessageBox.Show($"Прием пациента-{newPacient.Id}");
            }
        
        }

        private void Button_Izm(object sender, RoutedEventArgs e)
        {
            if (!vhodClick)
            {
                MessageBox.Show("Сначала войдите");
                return;
            }

           /* string fileName = $"{Poiskk.Id}.json";
            string jsonString = File.ReadAllText(fileName);
            var newPacient = JsonSerializer.Deserialize<Pacient>(jsonString);
           // newPacient.LastDoctor = doctor.Id;
           // PoiskPrint.DataContext = newPacient;
            izm.DataContext = newPacient;
            FilePacient(newPacient);*/

            Pacient newPacient = (Pacient)izm.DataContext;
            FilePacient(newPacient);

            MessageBox.Show($"Данные пациента успешно обновлены");

        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            if (!vhodClick)
            {
                MessageBox.Show("Сначала войдите");
                return;
            }
            string fileName = $"{Poiskk.Id}.json";
            string jsonString = File.ReadAllText(fileName);
            var newPacient = JsonSerializer.Deserialize<Pacient>(jsonString);
            /*newPacient.LastDoctor = doctor.Id;
            PoiskPrint.DataContext = newPacient;*/
            izm.DataContext = newPacient;

        }
        
    }
}