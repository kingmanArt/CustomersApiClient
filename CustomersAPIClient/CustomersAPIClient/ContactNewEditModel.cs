using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CustomersAPIClient.ViewModels
{
    class ContactNewEditModel
    {
        private static ContactNewEditModel personS2;
        public static ContactNewEditModel getInstance(string name)
        {
            if (personS2 == null)
                personS2 = new ContactNewEditModel(name);
            return personS2;
        }
        public ContactNewEditModel(string name) { }
        public List<string> GreetingCombobox { get; set; } = new List<string>() {  "Phone", "Email" };
        public ContactNewEditModel()
        {
            var editContact = new PersonContact();
            var addContact = new PersonContact();
            //MessageBox.Show(personS2.SelectedPers.Id.ToString());
            switch (personS2.NewWind)
            {
                case 1:

                    saveCommand = new RelayCommand(obj =>
                    {
                        GenerateId();
                        GetType();
                        try
                        {
                            //addContact = new PersonContact() { Txt = NewTxt, ContactTypeId = ContactTypeId, PersonId = personS2.SelectedPers.Id , PersonContactId = Id};
                            //AddContactAsync(addContact);
                            MessageBox.Show("Не встиг доробити");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    });

                    break;
                case 2:


                    saveCommand = new RelayCommand(obj =>
                    {

                        try
                        {
                            MessageBox.Show("Не встиг доробити");

                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    });
                    break;

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
        public int id;
        public int ContactTypeId;
        public void GetType ()
        {
        if(SelectedDataSource == "Phone")
            {
                ContactTypeId = 1;
            }
        if(SelectedDataSource == "Email")
            {
                ContactTypeId = 2;
            }
           
        }
        
        public void GenerateId()
        {
            foreach (var item in personS2.SelectedPers.PersonContacts)
            {
                Id = item.PersonContactId + 1;
            }
        }
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
        public string _selectedDataSource;
        public int Id { get; set; }

        public string SelectedDataSource
        {
            get { return _selectedDataSource; }
            set
            {
                _selectedDataSource = value;
                OnPropertyChanged("SelectedDataSource");
            }
        }
        public int _re;
        public int NewWind
        {
            get => _re;

            set
            {
                if (!SetValue(ref _re, value)) return;

                _re = value;

            }

        }
        public string newTxt;
        public string NewTxt
        {
            get => newTxt;

            set
            {
                if (!SetValue(ref newTxt, value)) return;

                newTxt = value;

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
        static async Task<Uri> AddContactAsync(PersonContact persContact)
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
