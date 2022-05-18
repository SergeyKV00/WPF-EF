using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using WPF_EF.Models;
using WPF_EF.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF_EF.ViewModels
{
    public class AppViewModel : INotifyPropertyChanged
    {
        // Orders
        private List<Order> orders = DataWorker.GetAllOrders();
        public List<Order> Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                OnPropertyChanged("Orders");
            }
        }

        // Money Incomes
        private List<MoneyIncome> moneyIncomes = DataWorker.GetAllIncomes();
        public List<MoneyIncome> MoneyIncomes
        {
            get { return moneyIncomes; }
            set
            {
                moneyIncomes = value;
                OnPropertyChanged("MoneyIncomes");
            }
        }

        // Add new order
        public string OrderSum { get; set; }
        private RelayCommand addNewOrder;
        public RelayCommand AddNewOrder
        {
            get
            {
                return addNewOrder ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    decimal sum;
                    if (!decimal.TryParse(OrderSum, out sum))
                    {
                        SetRedBlockControll(window, "orderSumBlock");
                    }
                    else
                    {
                        DataWorker.CreateOrder(sum);
                        UpdateAllOrdersView();
                        SetNullValuesProperty();
                        window.Close();
                    }
                });
            }
        }

        // Add new money income
        public string MoneyIncomeSum { get; set; }
        private DateTime dateTime = DateTime.Now;
        public DateTime MoneyIncomeDate { get => dateTime; set => dateTime = value; }
        private RelayCommand addNewMoneyIncome;
        public RelayCommand AddNewMoneyIncome
        {
            get
            {
                return addNewMoneyIncome ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    decimal sum;
                    if (!decimal.TryParse(MoneyIncomeSum, out sum))
                    {
                        SetRedBlockControll(window, "moneyIncomeSumBlock");
                    }
                    else
                    {
                        DataWorker.CreateMoneyIncome(sum, MoneyIncomeDate);
                        UpdateAllMoneyIncomesView();
                        SetNullValuesProperty();
                        window.Close();
                    }

                });
            }
        }

        // Select order
        private Order selectedOrder;
        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;
                OnPropertyChanged();
            }

        }

        // Select MoneyIncome
        private MoneyIncome selectedMoneyIncome;
        public MoneyIncome SelectedMoneyIncome
        {
            get { return selectedMoneyIncome; }
            set
            {
                selectedMoneyIncome = value;
                OnPropertyChanged();
            }
        }

        #region Windows opening commands 

        // Open new order window
        private RelayCommand? openAddNewOrderWindow;
        public RelayCommand OpenAddNewOrderWindow
        {
            get
            {
                return openAddNewOrderWindow ?? new RelayCommand(obj =>
                {
                    AddNewOrderWindow window = new AddNewOrderWindow();
                    OpenDialogWindow(window);
                });
            }
        }

        // Open new money income window
        private RelayCommand? openAddNewMoneyIncomeWindow;
        public RelayCommand OpenAddNeMoneyIncomeWindow
        {
            get
            {
                return openAddNewMoneyIncomeWindow ?? new RelayCommand(obj =>
                {
                    AddNewMoneyIncomeWindow window = new AddNewMoneyIncomeWindow();
                    OpenDialogWindow(window);
                });
            }
        }

        // Open order window payment
        private RelayCommand? openOrderWindowPayment;
        public RelayCommand OpenOrderWindowPayment
        {
            get
            {
                return openOrderWindowPayment ?? new RelayCommand(obj =>
                {
                    if (SelectedOrder != null)
                    {
                        OrderWindowPayment window = new OrderWindowPayment();
                        OpenDialogWindow(window);
                    }
                    else
                    {
                        MessageBox.Show("Выберите заказ!");
                    }

                });
            }
        }

        // Order payment
        public string AmountPayment { get; set; }
        private RelayCommand? orderPayment;
        public RelayCommand OrderPayment
        {
            get
            {
                return orderPayment ?? new RelayCommand(obj =>
                {
                    decimal paymentSum;
                    if (SelectedOrder != null && SelectedMoneyIncome != null)
                    {
                        MessageBox.Show($"Оплата! Номер заказа: {SelectedOrder.Id}, Номер аванса: {SelectedMoneyIncome.Id}, Cумма оплаты: {AmountPayment}");
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                });
            }
        }

        // Open new dialog window 
        private void OpenDialogWindow(Window window)
        {
            window.Owner = App.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion

        private void UpdateAllDataView()
        {
            UpdateAllOrdersView();
            UpdateAllMoneyIncomesView();
        }

        private void UpdateAllOrdersView()
        {
            Orders = DataWorker.GetAllOrders();
            MainWindow.AllOrdersView.ItemsSource = null;
            MainWindow.AllOrdersView.Items.Clear();
            MainWindow.AllOrdersView.ItemsSource = Orders;
            MainWindow.AllOrdersView.Items.Refresh();
        }

        private void UpdateAllMoneyIncomesView()
        {
            MoneyIncomes = DataWorker.GetAllIncomes();
            MainWindow.AllMoneyIncomesView.ItemsSource = null;
            MainWindow.AllMoneyIncomesView.Items.Clear();
            MainWindow.AllMoneyIncomesView.ItemsSource = MoneyIncomes;
            MainWindow.AllMoneyIncomesView.Items.Refresh();
        }

        private void SetRedBlockControll(Window? window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        private void SetNullValuesProperty()
        {
            OrderSum = null;
            MoneyIncomeSum = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
