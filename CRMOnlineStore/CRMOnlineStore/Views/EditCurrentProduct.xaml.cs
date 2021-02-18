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
using System.Windows.Shapes;

namespace CRMOnlineStore.Views
{
    /// <summary>
    /// Логика взаимодействия для EditCurrentProduct.xaml
    /// </summary>
    public partial class EditCurrentProduct : Window
    {
        public EditCurrentProduct(ViewModelProduct viewProduct)
        {
            InitializeComponent();

            this.DataContext = viewProduct;
        }

    }
}
