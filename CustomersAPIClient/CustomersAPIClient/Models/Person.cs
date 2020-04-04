using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomersAPIClient
{
   public class Person : INotifyPropertyChanged
    {
       
        public int Id { get; set; }
        public string Cpny { get; set; }
        public virtual Greeting Greeting { get; set; }
        public string Title { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Street { get; set; }
        public Country CountryCodeNavigation { get; set; }

        public string ConTxt1 { get; set; }
        public string ConTxt2 { get; set; }
        public string ConTxt3 { get; set; }
        public string ConTxt4 { get; set; }

        public string ConTxtEdit { get; set; }

        //GREETING
        public string GrTxt1 { get; set; }
        public string GrTxt2 { get; set; }
        public string GrTxt3 { get; set; }
        public string GrTxt4 { get; set; }

        public string GrTxtEdit { get; set; }



        public string Zip { get; set; }
        public string City { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime FirstContact { get; set; }
        public string CountryCode { get; set; }
        public sbyte GenderId { get; set; }
        public int GreetingId { get; set; }

        public ObservableCollection<PersonContact> _contacts;

        public ICollection<PersonContact> PersonContacts { get; set; }
        public string test;
        public string Test
        {
            get { return test; }
            set
            {
                test = value;

                OnPropertyChanged("SelectedDataSource");


            }
        }

        public string Notes { get; set; }
        //private string _phones;
        //public string Phone
        //{
        //    get 
        //    {
        //        var kek = PersonContact.Select(x => x.Txt).FirstOrDefault();
        //        return kek;
        //    }
        //    set
        //    {
        //        _phones = value;
        //        OnPropertyChanged(nameof(Phone));
        //    }
        //}




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
