using CRMOnlineStore.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CRMOnlineStore.ViewModels
{
    public class ViewModelProduct : BindableBase
    {
        private OnlineStoreContext Context { get; set; }
        private CreateProductWindow CreateProductWindow { get; set; }
        private Product NewProduct { get; set; }

        private EditCurrentProduct WindowEditProduct { get; set; }

        public DelegateCommand ShowWinCreateProduct { get; set; }
        public DelegateCommand CreateNewProduct { get; set; }
        public DelegateCommand CloseWinCreateProduct { get; set; }

        public DelegateCommand ShowWinEditProduct { get; set; }
        public DelegateCommand SaveChangesEditProduct { get; set; }
        public DelegateCommand CancelChangesEditProduct { get; set; }

        public DelegateCommand DeleteProduct { get; set; }
        

        public ViewModelProduct()
        {
            Context = new OnlineStoreContext();

            ShowWinCreateProduct = new DelegateCommand(() => this.ShowWindowCreateNewProduct());
            CreateNewProduct = new DelegateCommand(() => this.CreateProduct());
            CloseWinCreateProduct = new DelegateCommand(() => this.CloseWindowCreateProduct());

            ShowWinEditProduct = new DelegateCommand(() => this.ShowWinEditSelectedProduct());
            SaveChangesEditProduct = new DelegateCommand(() => this.EditProduct());
            CancelChangesEditProduct = new DelegateCommand(() => this.CloseWindowEditSelectedProduct());

            DeleteProduct = new DelegateCommand(() => this.DeleteSelectedProduct());

            Products = Context.Products.ToList();

            SubscriptionTypes = Context.SubscriptionTypes.ToList();

            SubscriptionTerms = Context.SubscriptionTerms.ToList();
        }

        private List<Product> products;
        public List<Product> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }

        private Product selectedProduct;
        public Product SelectedProduct
        {
            get => selectedProduct;
            set => SetProperty(ref selectedProduct, value);
        }

        private string titleProduct;
        public string TitleProduct
        {
            get => titleProduct;
            set => SetProperty(ref titleProduct, value);
        }

        /// <summary>
        /// Цена парсится в методах.
        /// </summary>
        private string priceProduct;
        public string PriceProduct
        {
            get => priceProduct;
            set => SetProperty(ref priceProduct, value);
        }

        private List<SubscriptionType> subscriptionTypes;
        public List<SubscriptionType> SubscriptionTypes
        {
            get => subscriptionTypes;
            set => SetProperty(ref subscriptionTypes, value);
        }

        private SubscriptionType subscriptionType;
        public SubscriptionType SubscriptionType
        {
            get => subscriptionType;
            set => SetProperty(ref subscriptionType, value);
        }

        private List<SubscriptionTerm> subscriptionTerms;
        public List<SubscriptionTerm> SubscriptionTerms
        {
            get => subscriptionTerms;
            set => SetProperty(ref subscriptionTerms, value);
        }

        private SubscriptionTerm subscriptionTerm;
        public SubscriptionTerm SubscriptionTerm
        {
            get => subscriptionTerm;
            set => SetProperty(ref subscriptionTerm, value);
        }

        /// <summary>
        /// Показывает окно создания продукта.
        /// Привязка командой в MainWindow.
        /// </summary>
        public void ShowWindowCreateNewProduct()
        {
            CreateProductWindow = new CreateProductWindow(this);
            CreateProductWindow.Show();
        }

        /// <summary>
        /// Создаёт новый продукт.
        /// Привязка командой View/CreateProductWindow.
        /// </summary>
        public void CreateProduct()
        {
            if (!CheckFields())
                return;
            

            NewProduct = new Product();
            NewProduct.Name = TitleProduct;

            decimal price = 0;
            if (decimal.TryParse(PriceProduct, out price) == true)
                NewProduct.Price = price;
            else
            {
                MessageBox.Show("In the box price, not a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewProduct.SubscriptionType = SubscriptionType;
            NewProduct.SubscriptionTerm = SubscriptionTerm;

            Context.Products.Add(NewProduct);
            Context.SaveChanges();
            Zeroing();

            Products = Context.Products.ToList();
        }
        /// <summary>
        /// отменяет все изменения и закрывает окно.
        /// </summary>
        public void CloseWindowCreateProduct() 
        {
            Zeroing();
            CreateProductWindow.Close();             
        }

        /// <summary>
        /// Инициализирует поля и показывает окно редактирования продукта.
        /// Привязка через команду к MainWidow.
        /// </summary>
        public void ShowWinEditSelectedProduct()
        {
            if (SelectedProduct is null)
            {
                MessageBox.Show("Product not selected!", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            WindowEditProduct = new EditCurrentProduct(this);

            TitleProduct = SelectedProduct.Name;
            PriceProduct = SelectedProduct.Price.ToString();
            SubscriptionType = SelectedProduct.SubscriptionType;
            SubscriptionTerm = SelectedProduct.SubscriptionTerm;

            WindowEditProduct.Show();
        }

        /// <summary>
        /// Редактирует выбраный продукт.
        /// Привязка через команду к View/editCurrentProduct
        /// </summary>
        public void EditProduct()
        {
            if (!CheckFields())
                return;

            SelectedProduct.Name = TitleProduct;

            decimal price = 0;
            if (decimal.TryParse(PriceProduct, out price) == true)
                SelectedProduct.Price = price;
            else
            {
                MessageBox.Show("In the box price, not a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SelectedProduct.SubscriptionType = SubscriptionType;
            SelectedProduct.SubscriptionTerm = SubscriptionTerm;

            Context.SaveChanges();

            Products = Context.Products.ToList();

            CloseWindowEditSelectedProduct();
        }

        /// <summary>
        /// отменяет все изменения и закрывает окно.
        /// </summary>
        public void CloseWindowEditSelectedProduct()
        {
            Zeroing();
            SelectedProduct = null;
            WindowEditProduct.Close();
        }

        /// <summary>
        /// Удаляет выбраный продукт.
        /// </summary>
        public void DeleteSelectedProduct()
        {
            if (SelectedProduct is null)
            {
                MessageBox.Show("Product not selected!", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Context.Products.Remove(SelectedProduct);
            Context.SaveChanges();

            Products = Context.Products.ToList();

            SelectedProduct = null;
        }

        #region Вспомогательные функции
        /// <summary>
        /// Вспомогательная ф-я. Проверка полей, заполнены или нет.
        /// </summary>
        private bool CheckFields()
        {
            if (string.IsNullOrEmpty(TitleProduct) || string.IsNullOrEmpty(PriceProduct)
                || SubscriptionType is null || SubscriptionTerm is null)
            {
                MessageBox.Show("Not all fields are filled.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// Вспомогательная ф-я. Обнуляет поля.
        /// </summary>
        private void Zeroing()
        {
            TitleProduct = null;
            PriceProduct = null;
            SubscriptionType = null;
            SubscriptionTerm = null;
        }
        #endregion

    }
}
