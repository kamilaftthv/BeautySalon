using BeautySalon.Data;
using BeautySalon.DbConnection;
using BeautySalon.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace BeautySalon.EditPages
{
    /// <summary>
    /// Interaction logic for EditClientPage.xaml
    /// </summary>
    public partial class EditClientPage : Page
    {
        private Client _currentClient;
        private bool _isNew;

        public EditClientPage(Client client = null)
        {
            InitializeComponent();

            if (client == null)
            {
                _currentClient = new Client
                {
                    RegistrationDate = DateTime.Now
                };
                _isNew = true;
            }
            else
            {
                _currentClient = client;
                _isNew = false;
            }

            FirstNameTextBox.Text = _currentClient.FirstName;
            LastNameTextBox.Text = _currentClient.LastName;
            PatronymicTextBox.Text = _currentClient.Patronymic;
            BirthdayDatePicker.SelectedDate = _currentClient.Birthday;
            RegistrationDatePicker.SelectedDate = _currentClient.RegistrationDate;
            EmailTextBox.Text = _currentClient.Email;
            PhoneTextBox.Text = _currentClient.Phone;
            GenderCodeTextBox.Text = _currentClient.GenderCode;
            PhotoPathTextBox.Text = _currentClient.PhotoPath;
        }

        private void PhotoPathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Обновляем изображение при изменении пути к фото
            ClientPhoto.Source = new PathToImageConverter().Convert(PhotoPathTextBox.Text, typeof(BitmapImage), null, CultureInfo.CurrentCulture) as BitmapImage;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _currentClient.FirstName = FirstNameTextBox.Text;
            _currentClient.LastName = LastNameTextBox.Text;
            _currentClient.Patronymic = PatronymicTextBox.Text;
            _currentClient.Birthday = BirthdayDatePicker.SelectedDate;
            _currentClient.RegistrationDate = RegistrationDatePicker.SelectedDate ?? DateTime.Now;
            _currentClient.Email = EmailTextBox.Text;
            _currentClient.Phone = PhoneTextBox.Text;
            _currentClient.GenderCode = GenderCodeTextBox.Text;
            _currentClient.PhotoPath = PhotoPathTextBox.Text;

            if (_isNew)
            {
                DataBaseManager.DataBaseConnection.Client.Add(_currentClient);
            }

            if (DataBaseManager.UpdateDatabase())
            {
                MessageBox.Show("Данные успешно сохранены.");
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Ошибка при сохранении данных.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

