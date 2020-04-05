using CustomersAPIClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersAPIClient
{
     interface IWindowService
    {
        void OpenProfileWindow(NewEditViewModel vm);
        void CloseProfileWindow(NewEditViewModel vm);
        void OpenProfileWindow2(ContactNewEditModel mv);
    }
}
