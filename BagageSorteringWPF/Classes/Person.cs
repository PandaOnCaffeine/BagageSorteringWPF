using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSorteringWPF.Classes
{
    public class Person
    {
        public string Name { get; set; }
        public int BagageAmount { get; private set; }
        public Person(Faker test, int amount)
        {
            Name = test.Name.FullName(new Bogus.DataSets.Name.Gender());
            BagageAmount = amount;
        }
    }
}
