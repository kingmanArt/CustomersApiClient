using System.Windows;


namespace CustomersAPIClient
{

     class WindowService : IWindowService
    {
        public void OpenProfileWindow(NewEditViewModel vm)
        {
            UserData win = new UserData();
            win.DataContext = vm;
            win.Show();
        }
    }
}
