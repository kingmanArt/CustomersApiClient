using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;

namespace CustomersAPIClient
{
     class NewEditViewModel
    {
        
        private static NewEditViewModel personS;
        public static NewEditViewModel getInstance(string name)
        {
            if (personS == null)
                personS = new NewEditViewModel(name);
            return personS;
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
        public NewEditViewModel(string name) { }
        public void GetInfo()
        {

            personS.SelectedPers.CountryCode = countryCode;
            personS.SelectedPers.GreetingId = ifGrid;
            personS.SelectedPers.Lname = Lname;
            personS.SelectedPers.Fname = FName;
            personS.SelectedPers.City = City;
            personS.SelectedPers.Street = Street;
            personS.SelectedPers.Zip = Postcode;
            personS.SelectedPers.Cpny = Company;
            personS.SelectedPers.Title = Title;
            


        }
        public NewEditViewModel()
        {
            var editPerson = new Person();
            var addPerson = new Person() ;
            var persCont = new PersonContact() { Txt = "23423", ContactTypeId = 1, PersonId = 2, PersonContactId = 324361, Active = 1 };
            switch (personS.windowType)
            {
                case 1 :
                    saveCommand = new RelayCommand(obj =>
                    {

                        try
                        {
                            GetGreeting();
                           
                            addPerson = new Person() { Id = personS.id, Fname = FName, Lname = Lname, GreetingId = ifGrid, CountryCode = countryCode, City = City, Street = Street, Zip = Postcode, Cpny = Company, Title = Title };
                            if (ifGrid == 0 || countryCode == null || City == "")
                            {
                                MessageBox.Show("the fields greeting, country, city must be filled");
                            }
                            else
                            {
                                AddPersonAsync(addPerson);
                                MessageBox.Show("Person Added");

                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    });

                    break;
                case 2:

                            WriteEditPersonInfo();

                    saveCommand = new RelayCommand(obj =>
                    {
                        
                        try
                        {
                            GetGreeting();
                            GetInfo();
                            if (ifGrid == 0 || countryCode == "" || City == "")
                            {
                                MessageBox.Show("the fields greeting, country, city must be filled");
                            }
                            else
                            {
                                EditPersonAsync(personS.SelectedPers);
                                MessageBox.Show("Person Edited");

                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    });
                    break;
            }
            NEVER_EAT_POISON_Disable_CertificateValidation();
            GetSomeInfo();
           
            
        }

        #region Properties
        public string _greeting;
        public string _fname;
        public string _lname;
        public string _country;
        public string _city;
        public string _street;
        public string _postcode;
        public string _company;
        public string _title;
        public string _email;
        public string _phone;

        public string _selectedDataSource;
        
        public string SelectedDataSource
        {
            get { return _selectedDataSource; }
            set
            {
                _selectedDataSource = value;

                OnPropertyChanged("SelectedDataSource");
            }
        }

        public string FName
        {
            get => _fname;

            set
            {
                if (!SetValue(ref _fname, value)) return;

                _fname = value;
                OnPropertyChanged(nameof(FName));

            }

        }
        public string Lname
        {
            get => _lname;

            set
            {
                if (!SetValue(ref _lname, value)) return;

                _lname = value;
                OnPropertyChanged(nameof(Lname));
            }

        }
        public string Greeting
        {
            get => _greeting;

            set
            {
                if (!SetValue(ref _greeting, value)) return;

                _greeting = value;
                OnPropertyChanged(nameof(Greeting));
            }

        }
        public string Country
        {
            get => _country;

            set
            {
                if (!SetValue(ref _country, value)) return;

                _country = value;
                OnPropertyChanged(nameof(Country));
            }

        }
        public string City
        {
            get => _city;

            set
            {
                if (!SetValue(ref _city, value)) return;

                _city = value;
                OnPropertyChanged(nameof(City));
            }

        }
        public string Street
        {
            get => _street;

            set
            {
                if (!SetValue(ref _street, value)) return;

                _street = value;
                OnPropertyChanged(nameof(Street));
            }

        }
        public string Postcode
        {
            get => _postcode;

            set
            {
                if (!SetValue(ref _postcode, value)) return;

                _postcode = value;
                OnPropertyChanged(nameof(Postcode));
                
            }

        }
        public string Company
        {
            get => _company;

            set
            {
                if (!SetValue(ref _company, value)) return;

                _company = value;
                OnPropertyChanged(nameof(Company));
            }

        }
        public string Title
        {
            get => _title;

            set
            {
                if (!SetValue(ref _title, value)) return;

                _title = value;
                OnPropertyChanged(nameof(Title));
            }

        }
        public string Email
        {
            get => _email;

            set
            {
                if (!SetValue(ref _email, value)) return;

                _email = value;
                OnPropertyChanged(nameof(Email));
            }

        }
        public string Phone
        {
            get => _phone;

            set
            {
                if (!SetValue(ref _phone, value)) return;

                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }

        }
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
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
        public List<int> PersonsIdies = new List<int>();
        public List<Greeting> Greetings = new List<Greeting>();
        public ObservableCollection<Country> сountries = new ObservableCollection<Country>();
        public ObservableCollection<Country> Countries
        {
            get => сountries;
            set
            {
                сountries = value;
                OnPropertyChanged(nameof(Countries));
            }
        }
        public List<string> GreetingCombobox { get; set; } = new List<string>() { "Mr", "Mrs", "Mr. and Mrs." };

        public Country getcode;
        public Country GetCode
        {
            get => getcode;
            set
            {
                getcode = value;
                OnPropertyChanged(nameof(Countries));
                GetCoutryCode(getcode.Txt4);
            }
        }
        int ifGrid=0;
        #endregion
        public Person selectedPers;
        public Person SelectedPers
        {
            get => selectedPers;
            set
            {
                selectedPers = value;
                OnPropertyChanged(nameof(SelectedPers));
            }
        }
        public int windowType=1;
        public int id;
        public string conName;
        string countryCode;
       
       
        public void WriteEditPersonInfo()
        {
            #region 
            if (personS.SelectedPers.Fname != null)
            {
                FName = personS.SelectedPers.Fname;
            }
            else
            {
                FName = "";
            }

            if (personS.SelectedPers.Lname != null)
            {
            Lname = personS.SelectedPers.Lname;
            }
            else
            {
                Lname = "";
            }

            if (personS.SelectedPers.GrTxtEdit != null)
            {
                SelectedDataSource = personS.SelectedPers.GrTxtEdit;

            }
            else
            {
                Greeting = "";
            }

            if (personS.SelectedPers.CountryCode != "")
            {
                countryCode = personS.SelectedPers.CountryCode;
            }
            else
            {
                Greeting = "";
                countryCode = "";
            }

            if (personS.SelectedPers.City != null)
            {
                City = personS.SelectedPers.City;

            }
            else
            {
                City = "";
            }

            if (personS.SelectedPers.Street != null)
            {
                Street = personS.SelectedPers.City;

            }
            else
            {
                Street = "";
            }

            if (personS.SelectedPers.Cpny != null)
            {
                Company = personS.SelectedPers.Cpny;

            }
            else
            {
                Company = "";
            }

            if (personS.SelectedPers.Title != null)
            {
                Title = personS.SelectedPers.Title;

            }
            else
            {
                Title = "";
            }

            if (personS.SelectedPers.Zip != null)
            {
                Postcode = personS.SelectedPers.Zip;

            }
            else
            {
                Postcode = "";
            }
            foreach (var item in personS.SelectedPers.PersonContacts)
            {
                if (item.ContactTypeId == 2 )
                {
                    Email = item.Txt;

                }
                if(item.ContactTypeId == 0)
                {
                    Email = "";
                }
            }

            foreach (var item in personS.SelectedPers.PersonContacts)
            {
                if (item.ContactTypeId == 1)
                {
                    Phone = item.Txt;
                    break;

                }
                
                if(item.ContactTypeId == 1 && item == null)
                {
                    Phone = "";
                }
            }

            #endregion

        }
        public void GetCoutryCode(string h)
        {
            foreach (var item in Countries)
            {
                if (h == item.Txt4)
                {
                    countryCode = item.Code;
                }

            }
        }
        private void GetGreeting()
        {
            if(SelectedDataSource== "Mr")
            {
                ifGrid = 2;
            }
            if (SelectedDataSource == "Mrs")
            {
                ifGrid = 3;

            }
            if (SelectedDataSource == "Mr. and Mrs.")
            {
                ifGrid = 4;

            }
        }

        public void GetSomeInfo()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001");

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage rPersonsIdies = client.GetAsync("api/person/IdInfo").Result;
            HttpResponseMessage rGreetings = client.GetAsync("api/Greetings").Result;
            HttpResponseMessage rCountries = client.GetAsync("api/Countries").Result;


            if (rPersonsIdies.IsSuccessStatusCode)
            {
                var personsIdies = rPersonsIdies.Content.ReadAsAsync<IEnumerable<Person>>().Result;
                foreach (var item in personsIdies)
                {
                    PersonsIdies.Add(item.Id);
                }
            }
            else
            {
                MessageBox.Show("Error Code" + rPersonsIdies.StatusCode + " : Message - " + rPersonsIdies.ReasonPhrase);
            }

            if (rGreetings.IsSuccessStatusCode)
            {
                var greetings = rGreetings.Content.ReadAsAsync<IEnumerable<Greeting>>().Result;
                foreach (var item in greetings)
                {
                    Greetings.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Error Code" + rGreetings.StatusCode + " : Message - " + rGreetings.ReasonPhrase);
            }

            if (rCountries.IsSuccessStatusCode)
                {
                    var countries = rCountries.Content.ReadAsAsync<IEnumerable<Country>>().Result;
                foreach (var item in countries)
                {
                    Countries.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Error Code" + rCountries.StatusCode + " : Message - " + rCountries.ReasonPhrase);
            }

        }
        public virtual bool SetValue<T>(ref T field, T newValue)
        {
            if (Equals(field, newValue)) return false;

            field = newValue;
            return true;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        static async Task<Uri> AddPersonAsync(Person person)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync(
                 "api/Person", person);

            response.EnsureSuccessStatusCode();

            return response.Headers.Location;

        }
        static async Task<Uri> EditPersonAsync(Person person)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PutAsJsonAsync(
                 "api/Person", person);

            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://localhost:5001/");


            //HttpResponseMessage response = client.PutAsJsonAsync("api/person", p).Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    MessageBox.Show("User Edited");
            //}
            //else
            //    MessageBox.Show("Error");
        }
        static async Task<Uri> AddContactAsync( PersonContact persContact)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001/");

            // Add an Accept header for JSON format.

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

          

            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/Contacts", persContact);

            response.EnsureSuccessStatusCode();


            // return URI of the created resource.
            return response.Headers.Location;

        }
        
    }
}
