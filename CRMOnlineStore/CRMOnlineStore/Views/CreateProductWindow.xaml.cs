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

namespace CRMOnlineStore.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateProductWindow.xaml
    /// </summary>
    public partial class CreateProductWindow : Window
    {
        public Product NewProduct { get; set; }

        public CreateProductWindow(OnlineStoreContext context)
        {
            InitializeComponent();

            BoxSubscriptionType.ItemsSource = context.SubscriptionTypes.ToList();
            BoxSubscriptionTerm.ItemsSource = context.SubscriptionTerms.ToList();
        }

        private void Button_Create(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(FieldName.Text) || string.IsNullOrEmpty(FieldPrice.Text) 
                || BoxSubscriptionTerm is null || BoxSubscriptionType is null)
            {
                MessageBox.Show("Not all fields are filled!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewProduct = new Product();
            NewProduct.Name = FieldName.Text;

            decimal price = 0;
            if (decimal.TryParse(FieldPrice.Text, out price) == true)
                NewProduct.Price = price;
            else
            {
                MessageBox.Show("In the box price, not a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewProduct.SubscriptionType = BoxSubscriptionType.SelectedItem as SubscriptionType;
            NewProduct.SubscriptionTerm = BoxSubscriptionTerm.SelectedItem as SubscriptionTerm;

            DialogResult = true;
        }

        private void Button_Cancel(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
