using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net;
using System.Net.Http;
using System.Windows;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Windows.Data;
using FastDeepCloner;
using RestSharp;
using Microsoft.VisualStudio.PlatformUI;
using CustomersAPIClient.ViewModels;

namespace CustomersAPIClient
{
    class MainViewModel : INotifyPropertyChanged
    {
        private Person selectedPerson;
        public ObservableCollection<Person> _persons;
        public ObservableCollection<Person> Persons 
          {
                get => _persons;
                set{
                    _persons = value;
                OnPropertyChanged(nameof(Persons));
                }
            }

        private readonly IWindowService _windowService;
        public ICommand AddProfile { get; }
        // створюю ліст з мов, зоб потім отримати селект з них
        public List<string> LanguagesCombobox { get; set; } = new List<string>() { "EN", "FR", "DE", "IT" };
        public List<Person> TestSelektedL  = new List<Person>() ;
        public ObservableCollection<string> SelectedDataSources { get; set; }
        public ObservableCollection<Person> _filtredpersons;
        // створюю проміжну колекцію персонів для пошук, тощо
        public ObservableCollection<Person> FiltredPersons
        {
            get => _filtredpersons;
            set
            {
                _filtredpersons = value;
                OnPropertyChanged(nameof(Persons));
            }
        }

        private string _searchData;
        // Функція для швидкого пошуку (Робить пошук на строні сервера, як було написано у завданні)
        public void FastSearch()
        {
            FiltredPersons.Clear();
            if (FastData != null)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:5001");

                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                string pe = "api/person/" + FastData;
                HttpResponseMessage response = client.GetAsync(pe).Result;
                if (response.IsSuccessStatusCode)
                {
                    var persons = response.Content.ReadAsAsync<IEnumerable<Person>>().Result;
                    foreach (var item in persons)
                    {
                        FiltredPersons.Add(item);
                        foreach (var item1 in item.PersonContacts)
                        {
                            if (item1.PersonContactId == 1 && item1.ContactTypeId == 1)
                            {
                                item.Test = item1.Txt;
                            }
                        }
                    }
                    DataSourceFilter();
                    OnPropertyChanged("sd");

                }
                else
                {
                    MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
            }

        }
        public string FastData
        {
            get => _searchData;

            set
            {
                if (!SetValue(ref _searchData, value)) return;

                _searchData = value;
                OnPropertyChanged(nameof(FastData));
                if(_searchData == "")
                {
                    FiltredPersons.Clear(); 
                    foreach (var item in Persons)
                    {
                        FiltredPersons.Add(item);
                    }
                }
                else
                FastSearch();


            }

        }
        // команди для виконання дій
        private RelayCommand newCommand;
        private RelayCommand delCommand;
        private RelayCommand refCommand;
        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand(obj =>
                  {

                      try
                      {
                          view.windowType = 2;
                          view.SelectedPers = SelectePerson;
                          view1.SelectedPers = SelectePerson;
                          _windowService.OpenProfileWindow(new NewEditViewModel(view.windowType));
                          if (view.NewWind == 1)
                          {
                              _windowService.OpenProfileWindow2(new ContactNewEditModel());
                          }

                      }
                      catch (Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
        public RelayCommand NewCommand
        {
            get
            {
                return newCommand ??
                  (newCommand = new RelayCommand(obj =>
                  {

                      try
                      {
                          view.windowType = 1;
                          view1.SelectedPers = SelectePerson;

                          _windowService.OpenProfileWindow(new NewEditViewModel( view.windowType));
                      }
                      catch (Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }

                  }));
            }
        }
        public RelayCommand DelCommand
        {
            get
            {
                return delCommand ??
                  (delCommand = new RelayCommand(obj =>
                  {

                      try
                      {
                          foreach (var item3 in TestSelected)
                          {
                              Person per3 = (Person)item3;

                              TestSelektedL.Add(per3);
                          }
                          foreach (var item in TestSelected)
                          {
                              Person per = (Person)item;
                              HttpClient client = new HttpClient();
                              client.BaseAddress = new Uri("https://localhost:5001");
                              client.DefaultRequestHeaders.Accept.Add(
                              new MediaTypeWithQualityHeaderValue("application/json"));
                              HttpResponseMessage response = client.DeleteAsync("api/Person/" + per.Id).Result;
                              if (response.IsSuccessStatusCode)
                              {
                                
                                 
                                  OnPropertyChanged("sd");
                                  
                              }
                              else
                              {
                                  MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                              }
                              
                          }
                          foreach (var item1 in TestSelektedL.ToList())
                          {
                              Person per1 = (Person)item1;

                              FiltredPersons.Remove(per1);
                              Persons.Remove(per1);
                          }
                          MessageBox.Show("Person was deleted");
                      }
                      catch (Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
        public RelayCommand RefCommand
        {
            get
            {
                return refCommand ??
                  (refCommand = new RelayCommand(obj =>
                  {

                  try
                  {
                      MessageBox.Show("Please wait, data is refreshing");
                          FiltredPersons.Clear();
                          GetAll();
                          OnPropertyChanged("Refresh");
                          //Refresh();
                          MessageBox.Show("Data was refreshed");
                          
                      }
                      catch (Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
        // отримання основного списку персонів
        private void GetAll()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/person/").Result;

            if (response.IsSuccessStatusCode )
            {
                var persons = response.Content.ReadAsAsync<IEnumerable<Person>>().Result;
                foreach (var item in persons)
                {
                    Persons.Add(item);
                    FiltredPersons.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }
        static void NEVER_EAT_POISON_Disable_CertificateValidation()
        {

            ServicePointManager.ServerCertificateValidationCallback =
                delegate (
                    object s,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors
                ) {
                    return true;
                };
        }
        NewEditViewModel view = NewEditViewModel.getInstance("da");
        ContactNewEditModel view1 = ContactNewEditModel.getInstance("da");

        
        public MainViewModel(IWindowService windowService)
        {
            
            view.windowType = 1;
            NEVER_EAT_POISON_Disable_CertificateValidation();
            FiltredPersons = new ObservableCollection<Person>();
            Persons = new ObservableCollection<Person>();
            BindingOperations.EnableCollectionSynchronization(Persons, _lock);
            //Persons = GetProducts();
            GetAll();
            GenerateId();
            foreach (var item in FiltredPersons)
            {
                    foreach (var item1 in item.PersonContacts)
                    {
                    if( item1.ContactTypeId == 1)
                    {
                        item.Test = item1.Txt;
                        break;
                    }
                    }
            }
            _windowService = windowService;
            NewWind = view.NewWind;
            if (view.NewWind == 1)
            {
                _windowService.OpenProfileWindow2(new ContactNewEditModel());
            }
            
            


        }
        
       public MainViewModel(IWindowService windowService, string name)
        {
                view1.NewWind = view.NewWind;
                _windowService = windowService;
                _windowService.OpenProfileWindow2(new ContactNewEditModel());
            
        }

        private int _re;
        public int NewWind
        {
            get => _re;

            set
            {
                if (!SetValue(ref _re, value)) return;

                _re = value;
                OnPropertyChanged(nameof(NewWind));
                if (view.NewWind == 1)
                {
                    _windowService.OpenProfileWindow2(new ContactNewEditModel());
                }
            }

        }
        public void GenerateId()
        {
            int i = 0;
            foreach (var item in Persons)
            {
                if (i < item.Id)
                {
                    i = item.Id;

                }
            }
            view.id = i + 1;
        }
        public void Refresh()
        {
            foreach (var item in Persons)
            {
                FiltredPersons.Add(item);
            }
        }
        public string _selectedDataSource;
        public string SelectedDataSource
        {
            get { return _selectedDataSource; }
            set
            {
                _selectedDataSource = value;
                
                OnPropertyChanged("SelectedDataSource");
                DataSourceFilter();
                

            }
        }
        public bool deletebuttonEvent;
        public bool DeleteButtonOfforOn
        {
            get { return deletebuttonEvent; }
            set
            {
                deletebuttonEvent = value;
                OnPropertyChanged("DeleteButtonOfforOn");
            }
        }

        public bool editbuttonEvent;
        public bool EditButtonOfforOn
        {
            get { return editbuttonEvent; }
            set
            {
                editbuttonEvent = value;
                OnPropertyChanged("EditButtonOfforOn");
            }
        }
        private void DataSourceFilter()
        {
            switch (SelectedDataSource)
            {
                case "FR":
                    foreach (var item in FiltredPersons)
                    {

                        if (item.ConTxt2 == null)
                        {
                            item.ConTxtEdit = item.ConTxt4;
                            item.GrTxtEdit = item.GrTxt4;

                        }
                        else
                        {
                            item.ConTxtEdit = item.ConTxt2;
                            item.GrTxtEdit = item.GrTxt2;
                        }
                       
                    }
                    OnPropertyChanged("Refresh");
                    break;

                case "DE":
                    foreach (var item in FiltredPersons)
                    {

                        if (item.ConTxt3 == null)
                        {
                            item.ConTxtEdit = item.ConTxt4;
                            item.GrTxtEdit = item.GrTxt4;

                        }
                        else
                        {
                            item.ConTxtEdit = item.ConTxt1;
                            item.GrTxtEdit = item.GrTxt1;
                        }
                        OnPropertyChanged("Refresh");
                    }
                    break;
                    
                case "IT":
                    foreach (var item in FiltredPersons)
                    {

                        if (item.ConTxt4 == null)
                        {
                            item.ConTxtEdit = item.ConTxt4;
                            item.GrTxtEdit = item.GrTxt4;

                        }
                        else
                        {
                            item.ConTxtEdit = item.ConTxt3;
                            item.GrTxtEdit = item.GrTxt3;
                        }
                        OnPropertyChanged("Refresh");
                    }
                    break;

                case "EN":
                    foreach (var item in FiltredPersons)
                    {
                            item.ConTxtEdit = item.ConTxt4;
                            item.GrTxtEdit = item.GrTxt4;
                    }
                    OnPropertyChanged("Refresh");
                    break;


            }

        }

        public Person SelectePerson
        {
            get => selectedPerson;
            set
            {
                if (!SetValue(ref selectedPerson, value)) return;

                selectedPerson = value;
                OnPropertyChanged(nameof(SelectePerson));
               

            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            if (view.NewWind == 1)
            {
                _windowService.OpenProfileWindow2(new ContactNewEditModel());
            }
        }

        public virtual bool SetValue<T>(ref T field, T newValue)
        {
            if (Equals(field, newValue)) return false;

            field = newValue;
            return true;
        }

        private static object _lock = new object();
        
        public IEnumerable<Person> Model { get { return Persons; } }

        private IList _selectedModels = new ArrayList();

        public IList TestSelected
        {
            get { return _selectedModels; }
            set
            {
                _selectedModels = value;
                OnPropertyChanged("TestSelected");

                if (TestSelected.Count == 0)
                {
                    DeleteButtonOfforOn = false;
                    EditButtonOfforOn = false;

                }
                if (TestSelected.Count > 0)
                {
                    DeleteButtonOfforOn = true;
                }
                if (TestSelected.Count > 1)
                {
                    EditButtonOfforOn = false;
                }
                if (TestSelected.Count == 1)
                {
                    EditButtonOfforOn = true;
                }
            }
             
        }



    }

}
