using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingList
{
	abstract class Employee
	{
        public int Id { get; }
		public string Name { get; }
		public double Rate { get; }

        private protected Employee(int id, string name, double rate)
        {
            this.Id = id;
            this.Name = name;
            this.Rate = rate;
        }
        public virtual double AvarageSalary()
        {
            return this.Rate;
        }
    }

    class CasualEmployee : Employee
    {
        public CasualEmployee(int id, string name, double rate) : base(id, name, rate){}

        public override double AvarageSalary()
        {
            return 20.8 * 8 * this.Rate;
        }
    }

    class FixedEmployee : Employee
    {
        public FixedEmployee(int id, string name, double rate) : base(id, name, rate) { }

    }





}
