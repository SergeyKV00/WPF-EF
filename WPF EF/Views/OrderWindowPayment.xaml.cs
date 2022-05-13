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
using WPF_EF.ViewModels;

namespace WPF_EF.Views
{
    /// <summary>
    /// Логика взаимодействия для OrderWindowPayment.xaml
    /// </summary>
    public partial class OrderWindowPayment : Window
    {
        public OrderWindowPayment()
        {
            InitializeComponent();
            DataContext = new AppViewModel();
        }
    }
}
