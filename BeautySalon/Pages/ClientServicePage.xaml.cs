using BeautySalon.Data;
using BeautySalon.DbConnection;
using BeautySalon.EditPages;
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
namespace BeautySalon.Pages
{
    public partial class ClientServicePage : Page
    {
        private List<ClientService> _clientServices;
        private List<ClientService> _filteredClientServices;

        public ClientServicePage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _clientServices = DataBaseManager.GetClientServices();
            _filteredClientServices = _clientServices;
            ClientServiceDataGrid.ItemsSource = _filteredClientServices;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            _filteredClientServices = _clientServices.Where(d =>
                d.Client.LastName.ToLower().Contains(searchText) ||
                d.Service.Title.ToLower().Contains(searchText)).ToList();
            ClientServiceDataGrid.ItemsSource = _filteredClientServices;
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilterComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string sortOption = selectedItem.Content.ToString();
                ApplySorting(sortOption);
            }
        }

        private void ApplySorting(string sortOption)
        {
            switch (sortOption)
            {
                case "Дата а/я":
                    _filteredClientServices = _filteredClientServices.OrderBy(d => d.StartTime).ToList();
                    break;
                case "Дата я/а":
                    _filteredClientServices = _filteredClientServices.OrderByDescending(d => d.StartTime).ToList();
                    break;
            }
            ClientServiceDataGrid.ItemsSource = _filteredClientServices;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditClientServicePage());
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ClientServiceDataGrid.SelectedItem as ClientService;

            if (selectedItem != null)
            {
                NavigationService.Navigate(new EditClientServicePage(selectedItem));
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ClientServiceDataGrid.SelectedItem as ClientService;

            if (selectedItem != null)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить эту запись?",
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    DataBaseManager.DataBaseConnection.ClientService.Remove(selectedItem);
                    if (DataBaseManager.UpdateDatabase())
                    {
                        MessageBox.Show("Запись успешно удалена.");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении записи.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления.");
            }
        }

        private void ClientServiceDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}