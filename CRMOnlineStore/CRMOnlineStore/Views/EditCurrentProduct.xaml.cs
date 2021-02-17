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
    /// Логика взаимодействия для EditCurrentProduct.xaml
    /// </summary>
    public partial class EditCurrentProduct : Window
    {
        public Product Product { get; set; }

        public EditCurrentProduct(Product product, OnlineStoreContext context)
        {
            InitializeComponent();

            Product = product;

            FieldName.Text = Product.Name;
            FieldPrice.Text = Product.Price.ToString();

            
            BoxSubscriptionType.ItemsSource = context.SubscriptionTypes.ToList();
            BoxSubscriptionTerm.ItemsSource = context.SubscriptionTerms.ToList();
            

            BoxSubscriptionType.SelectedItem = product.SubscriptionType;
            BoxSubscriptionTerm.SelectedItem = product.SubscriptionTerm;
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FieldName.Text) || string.IsNullOrEmpty(FieldPrice.Text)
                || BoxSubscriptionTerm is null || BoxSubscriptionType is null)
            {
                MessageBox.Show("Not all fields are filled!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Product.Name = FieldName.Text;

            decimal price = 0;
            if (decimal.TryParse(FieldPrice.Text, out price) == true)
                Product.Price = price;
            else
            {
                MessageBox.Show("In the box price, not a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Product.SubscriptionType = BoxSubscriptionType.SelectedItem as SubscriptionType;

            if (Product.SubscriptionType.Name == "License")
            {
                Product.SubscriptionTerm = null;
                DialogResult = true;
                return;
            }

            Product.SubscriptionTerm = BoxSubscriptionTerm.SelectedItem as SubscriptionTerm;

            DialogResult = true;
        }

        private void Button_Cancel(object sender, RoutedEventArgs e) => DialogResult = false;

        private void BoxSubscriptionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BoxSubscriptionType.SelectedItem.ToString() == "License")
            {
                BoxSubscriptionTerm.SelectedIndex = -1;
                return;
            }
        }
    }
}
