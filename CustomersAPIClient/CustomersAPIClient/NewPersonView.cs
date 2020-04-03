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
    public class NewPersonView
    {
        Random rnd = new Random();
        public int fa;
        private static NewPersonView personS;
        public static NewPersonView getInstance()
        {
            if (personS == null)
                personS = new NewPersonView();
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
        public NewPersonView()
        {
            NEVER_EAT_POISON_Disable_CertificateValidation();
            GetSomeInfo();
            var person = new Person() { Id = id, Fname = FName, Lname = Lname, GreetingId = ifGrid, CountryCode = countryCode, City = City, Street = Street, Zip = Postcode, Cpny = Company, Title = Title };
            var persCont = new PersonContact() { Txt = "23423", ContactTypeId = 1, PersonId = 2, PersonContactId = 324361, Active = 1 };

            //AddPersonAsync(person);
            AddContactAsync(persCont);
            AddPerson = new DelegateCommand(() =>
            {
                //GenerateId();
                //DataSourceFilter();
                //GetCoutryCode();

                MessageBox.Show(personS.Test.ToString());



                //var person = new Person() { Id = id, Fname = FName, Lname = Lname, GreetingId = ifGrid, CountryCode = countryCode, City = City, Street = Street, Zip = Postcode, Cpny = Company, Title = Title };
                //var persCont = new PersonContact() { Txt = "23423", ContactTypeId = 1, PersonId = 2,PersonContactId = 324361, Active = 1  };

                //AddPersonAsync(person);
                //AddContactAsync(persCont);
            });
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
                OnPropertyChanged(nameof(Title));
            }

        }
        public string Phone
        {
            get => _phone;

            set
            {
                if (!SetValue(ref _phone, value)) return;

                _phone = value;
                OnPropertyChanged(nameof(Title));
            }

        }
        public ICommand AddPerson { get; }
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
            }
        }
        int ifGrid;
        #endregion
        private Person selectedPerson;
        public int test ;
        public int Test
        {
            get => test;
            set
            {
                test = value;
                OnPropertyChanged(nameof(Test));
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
        int id;
        string countryCode;
        public void GetCoutryCode()
        {
            foreach (var item in Countries)
            {
                if (GetCode.Txt4 == item.Txt4)
                {
                    countryCode = item.Code;
                }

            }
        }
        private void DataSourceFilter()
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
             //HttpResponseMessage rPersonsIdies = client.GetAsync("api/person/IdInfo").Result;
            HttpResponseMessage rGreetings = client.GetAsync("api/Greetings").Result;
            HttpResponseMessage rCountries = client.GetAsync("api/Countries").Result;


            //if (rPersonsIdies.IsSuccessStatusCode )
            //{
            //    var personsIdies = rPersonsIdies.Content.ReadAsAsync<IEnumerable<Person>>().Result;
            //    foreach (var item in personsIdies)
            //    {
            //        PersonsIdies.Add(item.Id);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Error Code" + rPersonsIdies.StatusCode + " : Message - " + rPersonsIdies.ReasonPhrase);
            //}

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

            // Add an Accept header for JSON format.

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync(
                 "api/Person", person);

      

            response.EnsureSuccessStatusCode();


            // return URI of the created resource.
            return response.Headers.Location;

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
        public void GenerateId()
        {
            id = rnd.Next(0, 100000);
            foreach (var item in PersonsIdies)
            {
                if(item == id)
                {
                    id = rnd.Next(0, 100000);
                } 

            }
        }
    }
}
