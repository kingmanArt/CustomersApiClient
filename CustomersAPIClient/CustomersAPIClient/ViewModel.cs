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

namespace CustomersAPIClient
{
    class ViewModel : INotifyPropertyChanged
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
        public List<string> LanguagesCombobox { get; set; } = new List<string>() { "EN", "FR", "DE", "IT" }; 
        public ObservableCollection<string> SelectedDataSources { get; set; }
        public ObservableCollection<Person> _filtredpersons;
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
                    foreach (var item in Persons)
                    {
                        FiltredPersons.Add(item);
                    }
                }
                else
                FastSearch();


            }

        }
       

        private RelayCommand delCommand;
        private RelayCommand refCommand;
        private RelayCommand editCommand;
        public RelayCommand DelCommand
        {
            get
            {
                return delCommand ??
                  (delCommand = new RelayCommand(obj =>
                  {

                      try
                      {
                          foreach (var item in TestSelected)
                          {
                              Person per = (Person)item;
                              HttpClient client = new HttpClient();
                              client.BaseAddress = new Uri("https://localhost:5001");
                              client.DefaultRequestHeaders.Accept.Add(
                              new MediaTypeWithQualityHeaderValue("application/json"));
                              HttpResponseMessage response = client.DeleteAsync("api/Person/" + per.Id).Result;
                              //HttpResponseMessage response = client.PutAsJsonAsync("api/Person/10000", per).Result;
                              if (response.IsSuccessStatusCode)
                              {
                                  FiltredPersons.Remove(per); 
                                 
                                  OnPropertyChanged("sd");
                                  MessageBox.Show("Person was deleted");
                              }
                              else
                              {
                                  MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                              }
                          }

                      }
                      catch (Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
        

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
                    }
                    DataSourceFilter();
                    OnPropertyChanged("sd");

                }
                else
                {
                    MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
            }
            if(FastData == "")
            {
                //foreach (var item in Persons)
                //{
                //    FiltredPersons.Add(item);
                //}
                OnPropertyChanged("sd");
            }
        }
  
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand(obj =>
                  {

                      try
                      {
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
                          Refresh();
                          OnPropertyChanged("Refresh");
                          GetAll();
                          MessageBox.Show("Refreshed");
                          
                      }
                      catch (Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
       
        private void GetAll()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/person/").Result;
            HttpResponseMessage response2 = client.GetAsync("api/Contacts/").Result;

            if (response.IsSuccessStatusCode && response2.IsSuccessStatusCode)
            {
                var persons = response.Content.ReadAsAsync<IEnumerable<Person>>().Result;
                
                
                foreach (var item in persons)
                {
                    Persons.Add(item);
                    FiltredPersons.Add(item);
                }
               
                //var contacts = response2.Content.ReadAsAsync<IEnumerable<PersonContact>>().Result;
                //{
                //    foreach (var item in contacts)
                //    {
                //        foreach (var item1 in Persons)
                //        {
                //            item1.PersonContact = new ObservableCollection<PersonContact>();
                //            if (item1.Id == item.PersonId)
                //            {
                //                item1.PersonContact.Add(item);
                //            }
                //    }
                //    }
                    
                //}
                //MessageBox.Show(Persons[5].Phone);
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
        NewPersonView view = NewPersonView.getInstance();
        
        public int test = 4;
        public int Test
        {
            get => test;
            set
            {
                test = value;
                OnPropertyChanged(nameof(Test));
            }
        }
        public ViewModel(IWindowService windowService)
        {

            NEVER_EAT_POISON_Disable_CertificateValidation();
            FiltredPersons = new ObservableCollection<Person>();
            Persons = new ObservableCollection<Person>();
            BindingOperations.EnableCollectionSynchronization(Persons, _lock);
            //Persons = GetProducts();

            GetAll();
            if (_selectedDataSource != null)
            {
                ButtonOfforOn = true;
            }
            _windowService = windowService;
            AddProfile = new DelegateCommand(() =>
            {
                view.Test = SelectePerson.Id;
                _windowService.OpenProfileWindow(new NewPersonView());
            });



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
        public bool buttonEvent;
        public bool ButtonOfforOn
        {
            get { return buttonEvent; }
            set
            {
                buttonEvent = value;
                OnPropertyChanged("SelectedDataSource");
                

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
                
            }
             
        }
        
        
       
    }

}
