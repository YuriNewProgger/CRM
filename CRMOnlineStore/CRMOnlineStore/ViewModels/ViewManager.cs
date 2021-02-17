using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CRMOnlineStore
{
    public class ViewManager : BindableBase
    {
        /// <summary>
        /// Контекст данных для обращения к БД.
        /// </summary>
        private OnlineStoreContext Context { get; set; }
        /// <summary>
        /// Делегаты для привязки команд.
        /// </summary>
        public DelegateCommand ShowWinCreateManager { get; set; }
        public DelegateCommand ShowWinForEditManager { get; set; }
        public DelegateCommand DeleteManager { get; set; }
        public DelegateCommand CreateNewManager { get; set; }
        public DelegateCommand CloseWinCreateManager { get; set; }
        public DelegateCommand EditSelectedCurrentManager { get; set; }
        public DelegateCommand CloseEditManager { get; set; }

        private CreateManager WinCreateManager { get; set; }
        private EditCurrentManager EditCurrentManager { get; set; }

        private Manager NewCreatedManager { get; set; }

        public ViewManager()
        {
            Context = new OnlineStoreContext();

            ShowWinCreateManager = new DelegateCommand(() => this.ShowWindowForCreatedNewManager());
            ShowWinForEditManager = new DelegateCommand(() => this.ShowWindowForEditSelectedManager());
            DeleteManager = new DelegateCommand(() => this.DeleteSelectedManager());
            CreateNewManager = new DelegateCommand(() => this.CreateManager());
            CloseWinCreateManager = new DelegateCommand(() => this.CloseCreateManager());
            EditSelectedCurrentManager = new DelegateCommand(() => this.EditManager());
            CloseEditManager = new DelegateCommand(() => this.CloseWinEditManager());

            Managers = Context.Managers.ToList();
        }
        /// <summary>
        /// Список менеджеров для вывода.
        /// </summary>
        private List<Manager> managers;
        public List<Manager> Managers
        {
            get => managers;
            set => SetProperty(ref managers, value);
        }
        /// <summary>
        /// Имя нового менеджера.
        /// Привязан к Views/CreateManager.
        /// </summary>
        private string nameNewManager;
        public string NameNewManager
        {
            get => nameNewManager;
            set => SetProperty(ref nameNewManager, value);
        }
        /// <summary>
        /// Показывает окно создания нового менеджера.
        /// Привязан командой к MainWindow.
        /// </summary>
        public void ShowWindowForCreatedNewManager()
        {
            WinCreateManager = new CreateManager(this);
            WinCreateManager.Show();
        }
        /// <summary>
        /// Пользователь нажал создать.
        /// Привязка командой к Views/CreateManager. 
        /// </summary>
        public void CreateManager()
        {
            NewCreatedManager = new Manager();
            NewCreatedManager.NameManager = NameNewManager;

            Context.Managers.Add(NewCreatedManager);
            Context.SaveChanges();

            NameNewManager = null;

            Managers = Context.Managers.ToList();

            MessageBox.Show("New manager successfully created.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        /// <summary>
        /// Пользователь нажал отмена.
        /// Привязка командой к Views/CreateManager.
        /// </summary>
        public void CloseCreateManager() => WinCreateManager.Close();

        /// <summary>
        /// Выбраный менеджер.
        /// Привязан к MainWindow.
        /// </summary>
        private Manager selectedManager;
        public Manager SelectedManager
        {
            get => selectedManager;
            set => SetProperty(ref selectedManager, value);
        }

        public void DeleteSelectedManager()
        {
            if (SelectedManager is null)
            {
                MessageBox.Show("Manager not selected!", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Context.Managers.Remove(SelectedManager);
            Context.SaveChanges();

            Managers = Context.Managers.ToList();

            SelectedManager = null;
        }
        /// <summary>
        /// Имя для редактирования.
        /// Привязан к  Views/EditCurrentManager.
        /// </summary>
        private string editNameManager;
        public string EditNameManager
        {
            get => editNameManager;
            set => SetProperty(ref editNameManager, value);
        }
        /// <summary>
        /// Показывает окно редактирования выбраного менеджера.
        /// Привязан командой к MainWindow.
        /// </summary>
        public void ShowWindowForEditSelectedManager()
        {
            if (SelectedManager is null)
            {
                MessageBox.Show("Manager not selected!", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            EditCurrentManager = new EditCurrentManager(this);
            EditCurrentManager.Show();
            EditNameManager = SelectedManager.NameManager;
        }
        /// <summary>
        /// Метод редактирования выбраного менеджера. 
        /// Если пользователь сохраняет изменения.
        /// Привязан командой к Views/EditCurrentManager.
        /// </summary>
        public void EditManager()
        {
            SelectedManager.NameManager = EditNameManager;

            Context.SaveChanges();

            SelectedManager = null;
            EditNameManager = null;

            Managers = Context.Managers.ToList();

            EditCurrentManager.Close();
        }
        /// <summary>
        /// Если пользователь не сохраняет изменения.
        /// Привязан командой к Views/EditCurrentManager.
        /// </summary>
        public void CloseWinEditManager() => EditCurrentManager.Close();
    }
}
