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
    class ViewModelListClients : BindableBase
    {
        /// <summary>
        /// Контекст данных для работы с БД.
        /// </summary>
        private OnlineStoreContext Context { get; set; }

        /// <summary>
        /// Делегат для привязки команды.
        /// </summary>
        public DelegateCommand Show { get; set; }

        public ViewModelListClients() 
        {
            Context = new OnlineStoreContext();

            Show = new DelegateCommand(() => this.ShowItems());

            //Начальная инициализация списка с фильтрами.
            ListFilter = new List<string>
            {
                "Clients", "by Managers", "by Status"
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

        /// <summary>
        /// Список данных для DataGrid.
        /// </summary>
        private List<Client> dataForDataGrid;
        public List<Client> DataForDataGrid
        {
            get => dataForDataGrid;
            set => SetProperty(ref dataForDataGrid, value);
        }

        /// <summary>
        /// Поле запроса.
        /// Привязка к MainWindow.
        /// </summary>
        private string requestField;
        public string RequestField
        {
            get => requestField;
            set => SetProperty(ref requestField, value);
        }

        /// <summary>
        /// Показывает данные в зависимости от фильтра.
        /// Привязка к MainWindow.
        /// </summary>
        public void ShowItems()
        {
            DataForDataGrid = new List<Client>();
            switch (SelectedFilterItem)
            {
                case "Clients":
                    DataForDataGrid = Context.Clients.ToList();
                    break;
                case "by Managers":
                    LoadClientByManager(); ;
                    break;
                case "by Status":
                    LoadClientByStatus();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Загрузка клиентов по указаному менеджеру.
        /// </summary>
        private void LoadClientByManager()
        {
            if (string.IsNullOrEmpty(RequestField))
            {
                MessageBox.Show("Request field is empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataForDataGrid = Context.Clients.Where(i => i.Manager.NameManager == RequestField).ToList();

            RequestField = null;
        }

        /// <summary>
        /// Загрузка клиентов по указаному статусу.
        /// </summary>
        private void LoadClientByStatus()
        {
            if (string.IsNullOrEmpty(RequestField))
            {
                MessageBox.Show("Request field is empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataForDataGrid = Context.Clients.Where(i => i.Status.NameStatus == RequestField).ToList();

            RequestField = null;
        }
    }
}
