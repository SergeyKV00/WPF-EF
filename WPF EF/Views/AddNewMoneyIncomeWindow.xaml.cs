using System.Windows;
using WPF_EF.ViewModels;

namespace WPF_EF.Views
{
    /// <summary>
    /// Логика взаимодействия для AddNewMoneyIncomeWindow.xaml
    /// </summary>
    public partial class AddNewMoneyIncomeWindow : Window
    {
        public AddNewMoneyIncomeWindow()
        {
            InitializeComponent();
            DataContext = new AppViewModel();
        }
    }
}
