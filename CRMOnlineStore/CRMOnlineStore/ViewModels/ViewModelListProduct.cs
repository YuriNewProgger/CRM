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
    class ViewModelListProduct : BindableBase
    {
        private OnlineStoreContext Context { get; set; }

        public DelegateCommand ShowItems { get; set; }

        public ViewModelListProduct()
        {
            Context = new OnlineStoreContext();

            ShowItems = new DelegateCommand(() => this.Show());

            ListFilter = new List<string>
            {
                "Products", "by Clients"
            };
        }

        private List<string> listFilter;
        public List<string> ListFilter
        {
            get => listFilter;
            set => SetProperty(ref listFilter, value);
        }

        private string selectedFilterItem;
        public string SelectedFilterItem
        {
            get => selectedFilterItem;
            set => SetProperty(ref selectedFilterItem, value);
        }

        private string requestField;
        public string RequestField
        {
            get => requestField;
            set => SetProperty(ref requestField, value);
        }

        private List<Product> products;
        public List<Product> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }

        public void Show()
        {
            switch (SelectedFilterItem)
            {
                case "Products":
                    Products = Context.Products.ToList();
                    break;
                case "by Clients":
                    LoadProductsByClient();
                    break;
            }
        }

        private void LoadProductsByClient()
        {
            if (string.IsNullOrEmpty(RequestField))
            {
                MessageBox.Show("Request field is empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Client client = Context.Clients.Where(i => i.Name == RequestField).FirstOrDefault();

            Products = Context.ClientProducts.Where(i => i.ClientId == client.Id).Select(q => q.Product).ToList();
        }
    }
}
