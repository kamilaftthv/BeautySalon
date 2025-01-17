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
    /// <summary>
    /// Interaction logic for ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        private List<Client> _clients;
        private List<Client> _filteredClients;
        public ClientPage()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            _clients = DataBaseManager.GetClients();
            _filteredClients = _clients;
            ClientDataGrid.ItemsSource = DataBaseManager.GetClients();
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            _filteredClients = _clients.Where(d =>
                d.Gender.Name.ToLower().Contains(searchText)).ToList();
            ClientDataGrid.ItemsSource = _filteredClients;
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
                case "Фамилия а/я":
                    _filteredClients = _filteredClients.OrderBy(d => d.LastName).ToList();
                    break;
                case "Фамилия я/а":
                    _filteredClients = _filteredClients.OrderByDescending(d => d.LastName).ToList();
                    break;
            }
            ClientDataGrid.ItemsSource = _filteredClients;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditClientPage());
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ClientDataGrid.SelectedItem as Client;

            if (selectedItem != null)
            {
                NavigationService.Navigate(new EditClientPage(selectedItem));
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ClientDataGrid.SelectedItem as Client;

            if (selectedItem != null)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить эту запись?",
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    DataBaseManager.DataBaseConnection.Client.Remove(selectedItem);
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

        private void ClientDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
