using CRMOnlineStore.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CRMOnlineStore.ViewModels
{
    class ViewProduct : BindableBase
    {
        private OnlineStoreContext Context { get; set; }

        public DelegateCommand CreateProduct { get; set; }
        public DelegateCommand EditProduct { get; set; }
        public DelegateCommand DeleteProduct { get; set; }

        public ViewProduct()
        {
            Context = new OnlineStoreContext();

            CreateProduct = new DelegateCommand(() => this.CreateNewProduct());
            EditProduct = new DelegateCommand(() => this.EditSelectedProduct());
            DeleteProduct = new DelegateCommand(() => this.DeleteSelectedProduct());

            Products = Context.Products.ToList();
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

        public void CreateNewProduct()
        {
            CreateProductWindow createProductWindow = new CreateProductWindow(Context);

            if (createProductWindow.ShowDialog() == false)
                return;

            Product newProduct = createProductWindow.NewProduct;

            Context.Products.Add(newProduct);
            Context.SaveChanges();

            Products = Context.Products.ToList();
        }

        public void EditSelectedProduct()
        {
            if (SelectedProduct is null)
            {
                MessageBox.Show("Product not selected!", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            EditCurrentProduct editCurrentProduct = new EditCurrentProduct(SelectedProduct, Context);

            if (editCurrentProduct.ShowDialog() == false)
                return;

            SelectedProduct = editCurrentProduct.Product;
            Context.SaveChanges();

            SelectedProduct = null;

            Products = Context.Products.ToList();
        }

        public void DeleteSelectedProduct()
        {
            if(SelectedProduct is null)
            {
                MessageBox.Show("Product not selected!", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Context.Products.Remove(SelectedProduct);
            Context.SaveChanges();

            Products = Context.Products.ToList();

            SelectedProduct = null;
        }
    }
}
