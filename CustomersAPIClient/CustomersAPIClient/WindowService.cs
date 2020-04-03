using System.Windows;


namespace CustomersAPIClient
{

    public class WindowService : IWindowService
    {
        public void OpenProfileWindow(NewPersonView vm)
        {
            UserData win = new UserData();
            win.DataContext = vm;
            win.Show();
        }
    }
}
