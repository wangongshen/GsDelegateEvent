using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GsDelegateEvent.Event
{
    public class Dog : IObject
    {
        //public Dog(int id)
        //{ }

        public void DoAction()
        {
            this.Wang();
        }
        public void Wang()
        {
            Console.WriteLine("{0} Wang", this.GetType().Name);
        }
    }
}
