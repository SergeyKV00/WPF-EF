using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
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
        public static ListView AllTransactionView;
        private AppViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new AppViewModel();
            DataContext = _viewModel;
            
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateView_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Start();

            AllOrdersView = viewAllOrders;
            AllMoneyIncomesView = viewAllMoneyIncomes;
            AllTransactionView = viewAllTransactions;
        }

        private void UpdateView_Tick(object sender, EventArgs args)
        {
            var selectedOrder = _viewModel.SelectedOrder;
            var selectedIncome = _viewModel.SelectedMoneyIncome;
            _viewModel.UpdateAllDataView();

            AllOrdersView.SelectedItem = selectedOrder;
            AllMoneyIncomesView.SelectedItem = selectedIncome;
        }
    }
}
