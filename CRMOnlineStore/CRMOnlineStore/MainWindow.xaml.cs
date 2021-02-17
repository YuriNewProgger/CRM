using CRMOnlineStore.ViewModels;
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

namespace CRMOnlineStore
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewLists DataSource { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ManagersField.DataContext = new ViewManager();
            ClientsField.DataContext = new ViewClient();
            ProductsField.DataContext = new ViewProduct();

            DataSource = new ViewLists();
            FilterList.ItemsSource = DataSource.ListFilter;
            FieldRequest.IsEnabled = false;
            ButtonShow.IsEnabled = false;
        }
        /// <summary>
        /// Логика выбора фильтров.
        /// </summary>
        private void Filters_SelectionDataSource(object sender, SelectionChangedEventArgs e)
        {

            switch (FilterList.SelectedItem.ToString())
            {
                case "Managers":
                    FieldRequest.IsEnabled = false;
                    ButtonShow.IsEnabled = false;
                    DataSource.LoadManagers();
                    DataList.ItemsSource = DataSource.Managers;

                    DisplayOfColumns(DataList, "NameManager");
                    break;
                case "Clients":
                    FieldRequest.IsEnabled = false;
                    ButtonShow.IsEnabled = false;
                    DataSource.LoadClients();
                    DataList.ItemsSource = DataSource.Clients;

                    DisplayOfColumns(DataList, "Name", "Status");
                    break;
                case "Products":
                    FieldRequest.IsEnabled = false;
                    ButtonShow.IsEnabled = false;
                    DataSource.LoadProduct();
                    DataList.ItemsSource = DataSource.Products;

                    DisplayOfColumns(DataList, "Name", "Price", "SubscriptionType", "SubscriptionTerm");
                    break;
                default:
                    FieldRequest.IsEnabled = true;
                    ButtonShow.IsEnabled = true;
                    break;
            }
        }
        /// <summary>
        /// Логика выбора более сложных фильтров. Динамически меняется список отображаемых объектов.
        /// </summary>
        private void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FieldRequest.Text))
            {
                MessageBox.Show("Request field is empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            switch (FilterList.SelectedItem.ToString())
            {
                case "Client by managers":
                    DataSource.ManagerName = FieldRequest.Text;
                    DataSource.LoadClientByManagers();
                    DataList.ItemsSource = DataSource.ClientsByManagers;

                    DisplayOfColumns(DataList, "Name", "Status", "Manager");
                    break;
                case "Product by clients":
                    DataSource.ClientName = FieldRequest.Text;
                    DataSource.LoadProductByClient();
                    DataList.ItemsSource = DataSource.Products;

                    DisplayOfColumns(DataList, "Name", "Price", "SubscriptionType", "SubscriptionTerm");
                    break;
                case "Client by status":
                    DataSource.NameStatus = FieldRequest.Text;
                    DataSource.LoadClientByStatus();
                    DataList.ItemsSource = DataSource.Clients;

                    DisplayOfColumns(DataList, "Name", "Status", "Manager");
                    break;
            }
        }

        private void DisplayOfColumns(DataGrid dl, params string[] columns)
        {
            for (int i = 0; i < dl.Columns.Count; i++)
            {
                for (int j = 0; j < columns.Length; j++)
                {
                    if (dl.Columns[i].Header.ToString() == columns[j].ToString())
                    {
                        dl.Columns[i].Visibility = Visibility.Visible;
                        break;
                    }
                    else
                        dl.Columns[i].Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
