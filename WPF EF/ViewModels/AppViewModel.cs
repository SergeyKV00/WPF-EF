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

        // Property
        public string OrderSum { get; set; }

        // Add new order
        private RelayCommand addNewOrder;
        public RelayCommand AddNewOrder
        {
            get
            {
                return addNewOrder ?? new RelayCommand(obj =>
                {
                    Window? window = obj as Window;
                    decimal sum;
                    if(!decimal.TryParse(OrderSum, out sum))
                    {
                        SetRedBlockControll(window, "SumBlock");
                    }
                    else
                    {
                        DataWorker.CreateOrder(sum);
                    }                   
                });
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
        #endregion

        // Open new dialog window 
        private void OpenDialogWindow(Window window)
        {
            window.Owner = App.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        private void SetRedBlockControll(Window? window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
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
