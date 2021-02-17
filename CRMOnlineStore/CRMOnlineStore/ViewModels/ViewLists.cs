using CRMOnlineStore.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMOnlineStore.ViewModels
{
    class ViewLists : BindableBase
    {
        private OnlineStoreContext Context { get; set; }

        public ViewLists() 
        {
            Context = new OnlineStoreContext();

            //Начальная инициализация списка с фильтрами.
            ListFilter = new List<string>
            {
                "Managers", "Clients", "Products", "Client by managers",
                "Product by clients", "Client by status"
            };
        }

        private List<string> listFilter;
        public List<string> ListFilter
        {
            get => listFilter;
            set => SetProperty(ref listFilter, value);
        }

        private List<Manager> managers;
        public List<Manager> Managers
        {
            get => managers;
            set => SetProperty(ref managers, value);
        }

        public void LoadManagers() => Managers = Context.Managers.ToList();

        private List<Client> clients;
        public List<Client> Clients
        {
            get => clients;
            set => SetProperty(ref clients, value);
        }

        public void LoadClients() => Clients = Context.Clients.ToList();

        private List<Product> products;
        public List<Product> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }

        public void LoadProduct() => Products = Context.Products.ToList();

        private List<Client> clientsByManagers;
        public List<Client> ClientsByManagers
        {
            get => clientsByManagers;
            set => SetProperty(ref clientsByManagers, value);
        }

        public string ManagerName { get; set; }

        /// <summary>
        /// Загрузка клиентов по выбраному менеджеру.
        /// </summary>
        public void LoadClientByManagers()
        {

            Manager manager = Context.Managers.Where(i => i.NameManager == ManagerName).FirstOrDefault();

            ClientsByManagers = Context.Clients.Where(i => i.Manager.NameManager == ManagerName).ToList();
        }

        public string ClientName { get; set; }

        /// <summary>
        /// Загрузка клиентоа по имени и поиск его товаров по навигационому св-ву.
        /// </summary>
        public void LoadProductByClient()
        {
            Client client = Context.Clients.Where(i => i.Name == ClientName).FirstOrDefault();

            if (client is null)
                return;

            List<ClientProduct> productLisrCurrentClient = Context.ClientProducts.Where(q => q.ClientId == client.Id).ToList();

            List<Product> tmpListProduct = Context.Products.ToList();

            Products = new List<Product>();

            for (int i = 0; i < productLisrCurrentClient.Count; i++)
            {
                for(int j = 0; j< tmpListProduct.Count; j++)
                {
                    if (productLisrCurrentClient[i].ProductId == tmpListProduct[j].Id)
                        Products.Add(tmpListProduct[j]);
                }
            }
        }

        /// <summary>
        /// Загрузка клиентов по статусу.
        /// </summary>
        public string NameStatus { get; set; }
        public void LoadClientByStatus()
        {
            Status status = Context.Status.Where(i => i.NameStatus == NameStatus).FirstOrDefault();

            if (status is null)
                return;

            Clients = Context.Clients.Where(i => i.Status.NameStatus == status.NameStatus).ToList();
        }



    }
}
