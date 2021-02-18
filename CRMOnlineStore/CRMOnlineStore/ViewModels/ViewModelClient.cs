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
    public class ViewModelClient : BindableBase
    {
        /// <summary>
        /// Контекст данных для работы с БД.
        /// </summary>
        private OnlineStoreContext Context { get; set; }

        private CreateClientWindow ClientCreationWindow { get; set; }
        private Client CreatedNewClient { get; set; }

        private EditCurrentClient WindowEditSelectedClient { get; set; }

        /// <summary>
        /// Делегаты для привязки команд.
        /// </summary>
        public DelegateCommand ShowWindowForCreateNewClient { get; set; }
        public DelegateCommand CreateNewClient { get; set; }
        public DelegateCommand CloseWinCreationNewClient { get; set; }

        public DelegateCommand ShowWindowForEditSelectedClient { get; set; }
        public DelegateCommand EditSelectedClient { get; set; }
        public DelegateCommand CloseWinEditSelectedClient { get; set; }

        public DelegateCommand DeleteClient { get; set; }

        public ViewModelClient()
        {
            Context = new OnlineStoreContext();

            ShowWindowForCreateNewClient = new DelegateCommand(() => this.ShowClientCreationWindow());
            CreateNewClient = new DelegateCommand(() => this.CreateClient());
            CloseWinCreationNewClient = new DelegateCommand(() => this.CloseWindowCreationNewClient());

            ShowWindowForEditSelectedClient = new DelegateCommand(() => this.ShowWindowEditSelectedClient());
            EditSelectedClient = new DelegateCommand(() => this.EditClient());
            CloseWinEditSelectedClient = new DelegateCommand(() => this.CloseWindowEditSelectedClient());

            DeleteClient = new DelegateCommand(() => this.DeleteSelectedClient());

            Clients = Context.Clients.ToList();
            Managers = Context.Managers.ToList();
            Statuses = Context.Status.ToList();

        }

        /// <summary>
        /// Список клиентов.
        /// </summary>
        private List<Client> clients;
        public List<Client> Clients
        {
            get => clients;
            set => SetProperty(ref clients, value);
        }

        /// <summary>
        /// Выбраный клиент.
        /// Привязка к Mainwindow.
        /// </summary>
        private Client selectedClient;
        public Client SelectedClient
        {
            get => selectedClient;
            set => SetProperty(ref selectedClient, value);
        }

        /// <summary>
        /// Показывает окно создания нового клиента.
        /// Привязка командой к MainWindow.
        /// </summary>
        public void ShowClientCreationWindow()
        {
            ClientCreationWindow = new CreateClientWindow(this);
            ClientCreationWindow.Show();
        }

        /// <summary>
        /// Имя нового клиента.
        /// Привязка к View/CreateClientWindow.
        /// </summary>
        private string nameClient;
        public string NameClient
        {
            get => nameClient;
            set => SetProperty(ref nameClient, value);
        }

        /// <summary>
        /// Список менеджеров, для выбора.
        /// Привязка к View/CreateClientWindow.
        /// </summary>
        private List<Manager> managers;
        public List<Manager> Managers
        {
            get => managers;
            set => SetProperty(ref managers, value);
        }

        /// <summary>
        /// Выбраный менеджер из списка.
        /// Привязка к View/CreateClientWindow.
        /// </summary>
        private Manager selectedManager;
        public Manager SelectedManager
        {
            get => selectedManager;
            set => SetProperty(ref selectedManager, value);
        }

        /// <summary>
        /// Список статусов.
        /// Привязка к View/CreateClientWindow
        /// </summary>
        private List<Status> statuses;
        public List<Status> Statuses
        {
            get => statuses;
            set => SetProperty(ref statuses, value);
        }

        /// <summary>
        /// Выбраный статус из списка.
        /// Привязка к View/CreateClientWindow.
        /// </summary>
        private Status selectedStatus;
        public Status SelectedStatus
        {
            get => selectedStatus;
            set => SetProperty(ref selectedStatus, value);
        }

        /// <summary>
        /// Создает нового клиента.
        /// Привязка View/CreateClientWindow.
        /// </summary>
        public void CreateClient()
        {
            if(string.IsNullOrEmpty(NameClient) || SelectedManager == null || SelectedStatus == null)
            {
                MessageBox.Show("Not all fields are filled.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CreatedNewClient = new Client();
            CreatedNewClient.Name = NameClient;
            CreatedNewClient.Manager = SelectedManager;
            CreatedNewClient.Status = SelectedStatus;

            Context.Clients.Add(CreatedNewClient);
            Context.SaveChanges();

            Clients = Context.Clients.ToList();

            NameClient = null;
            SelectedManager = null;
            SelectedStatus = null;

            MessageBox.Show("New client successfully created.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void CloseWindowCreationNewClient() => ClientCreationWindow.Close();


        /// <summary>
        /// Показываем окно редактирования выбраного клиента и иницилизируем данными.
        /// Привязка к MainWindow.
        /// </summary>
        public void ShowWindowEditSelectedClient()
        {
            if (SelectedClient is null)
            {
                MessageBox.Show("Client not selected!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            WindowEditSelectedClient = new EditCurrentClient(this);

            NameClient = SelectedClient.Name;
            SelectedManager = SelectedClient.Manager;
            SelectedStatus = SelectedClient.Status;

            WindowEditSelectedClient.Show();
        }

        /// <summary>
        /// Редактирует клиента.
        /// Привязка к View/EditCurrentclient.
        /// </summary>
        public void EditClient()
        {

            SelectedClient.Name = NameClient;
            SelectedClient.Manager = SelectedManager;
            SelectedClient.Status = SelectedStatus;

            Context.SaveChanges();

            Clients = Context.Clients.ToList();

            NameClient = null;
            SelectedManager = null;
            SelectedStatus = null;

            WindowEditSelectedClient.Close();
        }

        public void CloseWindowEditSelectedClient() => WindowEditSelectedClient.Close();

        /// <summary>
        /// Удаляет выбраного клиента.
        /// </summary>
        public void DeleteSelectedClient()
        {
            if (SelectedClient is null)
            {
                MessageBox.Show("Client not selected!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Context.Clients.Remove(SelectedClient);
            Context.SaveChanges();

            Clients = Context.Clients.ToList();

            SelectedClient = null;
        }

    }
}
