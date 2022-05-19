using System.Windows;
using System.Windows.Controls;
using WPF_EF.ViewModels;

namespace WPF_EF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView AllOrdersView;
        public static ListView AllMoneyIncomesView;
        public static ListView AllTransactionView; // ?????
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new AppViewModel();

            AllOrdersView = viewAllOrders;
            AllMoneyIncomesView = viewAllMoneyIncomes;
            AllTransactionView = viewAllTransactions;
        }
    }
}
