using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WorkingList
{
	class Program
	{
        public enum EmployeeType
        {
            None = 0,
            Casual = 1,
            Fixed = 2
        }
		static void Main(string[] args)
		{
            string fileInPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"baltimore-city-employee-salaries-fy2019-1.csv");

			CsvReader reader = new CsvReader(fileInPath);

			List<Employee> employees = reader.ReadAllEmployee();
			
            //1
            foreach (var country in employees.Where(i=>i.Rate!=0).OrderByDescending(i=>i.AvarageSalary()).ThenBy(i=>i.Name))
            {
                Console.WriteLine("{0,-10}{1,-30}{2,-30}", country.Id, country.Name, SalaryFormatter.FormatPopulation(country.AvarageSalary()));
            }

			// 2
			// listing the first 20 countries without commas in their names
			foreach (var name in employees.Where(i => i.Rate != 0).OrderByDescending(i=>i.AvarageSalary()).ThenBy(i=>i.Name).Select(i=>i.Name).Take(5))
			{
                Console.WriteLine("{0,-10}",  name);
			}

			//3
            foreach (var id in employees.Where(i => i.Rate != 0).OrderByDescending(i => i.AvarageSalary()).ThenBy(i => i.Name)
                .Select(i => i.Id).Skip(Math.Max(0, employees.Count(i => i.Rate != 0) - 3)))
            {
                Console.WriteLine("{0,-10}", id);
            }

            
			Console.WriteLine();

            string fileOutPath = @"C:\Users\olegs\baltimore-city-employee-salaries-fy2019-1-out.csv";
            WriteAllEmployee(fileOutPath, employees);


            Console.ReadLine();
        }



        public static List<Employee> WriteAllEmployee(string path, IEnumerable<Employee> lst)
        {
            List<Employee> countries = new List<Employee>();

            using (var file = File.CreateText(path))
            {
                foreach (var arr in lst)
                {
                    file.WriteLine(string.Join(",", arr));
                }
            }
            return countries;
        }

    }
}
