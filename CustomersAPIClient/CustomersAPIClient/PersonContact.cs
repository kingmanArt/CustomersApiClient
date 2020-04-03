using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersAPIClient
{
    public class PersonContact
    {
        public PersonContact()
        {
            Person = new HashSet<Person>();
        }
        public int PersonId { get; set; }
        public int PersonContactId { get; set; }
        public int ContactTypeId { get; set; }
        public string Txt { get; set; }
        public string Notes { get; set; }
        public sbyte Active { get; set; }

        public virtual ICollection<Person> Person { get; set; }
        //public virtual Person Person { get; set; }

    }
}
