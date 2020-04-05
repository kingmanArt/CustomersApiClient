using CustomersAPIClient.ViewModels;
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

        public void CloseProfileWindow(NewEditViewModel vm)
        {
            UserData win = new UserData();
            win.DataContext = vm;
            win.Close();
        }

        public void OpenProfileWindow2(ContactNewEditModel mv)
        {
            ContactsNewEdit win2 = new ContactsNewEdit();
            win2.DataContext = mv;
            win2.Show();
        }
    }
}
