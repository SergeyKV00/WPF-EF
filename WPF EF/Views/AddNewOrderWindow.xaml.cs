using System.Windows;
using WPF_EF.ViewModels;

namespace WPF_EF.Views
{
    /// <summary>
    /// Логика взаимодействия для AddNewOrderWindow.xaml
    /// </summary>
    public partial class AddNewOrderWindow : Window
    {
        public AddNewOrderWindow()
        {
            InitializeComponent();
            DataContext = new AppViewModel();
        }
    }
}
