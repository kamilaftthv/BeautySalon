using BeautySalon.Data;
using BeautySalon.DbConnection;
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

namespace BeautySalon.EditPages
{
    public partial class EditClientServicePage : Page
    {
        private ClientService _currentClientService;
        private bool _isNew;

        public EditClientServicePage(ClientService clientService = null)
        {
            InitializeComponent();

            ClientComboBox.ItemsSource = DataBaseManager.GetClients();
            ServiceComboBox.ItemsSource = DataBaseManager.GetServices();

            if (clientService == null)
            {
                _currentClientService = new ClientService
                {
                    StartTime = DateTime.Now
                };
                _isNew = true;
            }
            else
            {
                _currentClientService = clientService;
                _isNew = false;
            }

            ClientComboBox.SelectedItem = _currentClientService.Client;
            ServiceComboBox.SelectedItem = _currentClientService.Service;
            StartTimeDatePicker.SelectedDate = _currentClientService.StartTime;
            CommentTextBox.Text = _currentClientService.Comment;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientComboBox.SelectedItem == null || ServiceComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента и услугу.");
                return;
            }

            _currentClientService.ClientID = ((Client)ClientComboBox.SelectedItem).ID;
            _currentClientService.ServiceID = ((Service)ServiceComboBox.SelectedItem).ID;
            _currentClientService.StartTime = StartTimeDatePicker.SelectedDate ?? DateTime.Now;
            _currentClientService.Comment = CommentTextBox.Text;

            if (_isNew)
            {
                DataBaseManager.DataBaseConnection.ClientService.Add(_currentClientService);
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