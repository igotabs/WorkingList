using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static WorkingList.Program;

namespace WorkingList
{
	class CsvReader
	{
       
		private string _csvFilePath;

		public CsvReader(string csvFilePath)
		{
			this._csvFilePath = csvFilePath;
		}

		private static Employee ReadEmployeeFromCsvLine(string csvLine)
		{
			string[] parts = csvLine.Split(',');
			string name;
			double rate;
            int id;
            int type;
            try
            {
                switch (parts.Length)
			    {
				    case 4:
					    id= Int32.Parse(parts[0]);
					    name = parts[1];
					    type = Int32.Parse(parts[2]);
                        Double.TryParse(parts[3], NumberStyles.Any, CultureInfo.InvariantCulture, out rate);
                        break;
				    case 5:
                        id = Int32.Parse(parts[0]);
					    name = parts[1] + ", " + parts[2];
					    name = name.Replace("\"", null).Trim();
                        type = Int32.Parse(parts[3]);
                        Double.TryParse(parts[4], NumberStyles.Any, CultureInfo.InvariantCulture, out rate);
					    break;
				    default:
					    throw new Exception($"Can't parse country from csvLine: {csvLine}");
			    }

                if (type == (int) EmployeeType.Casual) return new CasualEmployee(id, name, rate);
                return new FixedEmployee(id, name, rate);
            }
            catch (System.FormatException e)
            {
                Console.WriteLine(e);
                return null;
            }
		}

		public List<Employee> ReadAllEmployee()
		{
			List<Employee> employees = new List<Employee>();

			using (StreamReader sr = new StreamReader(_csvFilePath))
			{
				// read header line
				sr.ReadLine();

				string csvLine;
				while ((csvLine = sr.ReadLine()) != null)
                {
                    var entity = ReadEmployeeFromCsvLine(csvLine);
					if(entity!=null) employees.Add(entity);
				}
			}
			return employees;
		}

    }
}
