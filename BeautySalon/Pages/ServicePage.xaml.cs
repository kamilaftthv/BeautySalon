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
    /// Interaction logic for ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        private List<Service> _services;
        private List<Service> _filteredServices;
        public ServicePage()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            _services = DataBaseManager.GetServices();
            _filteredServices = _services;
            ServiceDataGrid.ItemsSource = DataBaseManager.GetServices();
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            _filteredServices = _services.Where(d =>
                d.Title.ToLower().Contains(searchText)).ToList();
            ServiceDataGrid.ItemsSource = _filteredServices;
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
                case "Стоимость а/я":
                    _filteredServices = _filteredServices.OrderBy(d => d.Cost).ToList();
                    break;
                case "Стоимость я/а":
                    _filteredServices = _filteredServices.OrderByDescending(d => d.Cost).ToList();
                    break;
            }
            ServiceDataGrid.ItemsSource = _filteredServices;
        }

        private void ServiceDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
