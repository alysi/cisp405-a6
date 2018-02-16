// CISP 405 Summer 2017 Alysia Iglesias 
// Assignment 6

// PayrollSystemTest.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CISP405_A6
{
    public abstract class Employee
    {
        private Date birthDate;             // new private instance variable birthDate to hold a Date object

        public string FirstName { get; }
        public string LastName { get; }
        public string SocialSecurityNumber { get; }

        // six-parameter constructor
        public Employee(string firstName, string lastName,
           string socialSecurityNumber, int month, int day, int year)           // added month, day, year variables
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
            birthDate = new Date(month, day, year);                             // added the birthDate assignment
        }

        // public property to access the Date object
        public Date BirthDate
        {
            get
            {
                return birthDate;
            }
        }

        // return string representation of Employee object, using properties
        public override string ToString() => $"{FirstName} {LastName}\n" +
           $"social security number: {SocialSecurityNumber}\n" + $"birth date: {BirthDate}";    // add birth date

        // abstract method overridden by derived classes
        public abstract decimal Earnings(); // no implementation here
    }

    public class CommissionEmployee : Employee
    {
        private decimal grossSales; // gross weekly sales
        private decimal commissionRate; // commission percentage

        // eight-parameter constructor
        public CommissionEmployee(string firstName, string lastName,
           string socialSecurityNumber, int month, int day, int year, decimal grossSales,
           decimal commissionRate)
           : base(firstName, lastName, socialSecurityNumber, month, day, year)      // now adds birth date
        {
            GrossSales = grossSales; // validates gross sales
            CommissionRate = commissionRate; // validates commission rate
        }

        // property that gets and sets commission employee's gross sales
        public decimal GrossSales
        {
            get
            {
                return grossSales;
            }
            set
            {
                if (value < 0) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(GrossSales)} must be >= 0");
                }

                grossSales = value;
            }
        }

        // property that gets and sets commission employee's commission rate
        public decimal CommissionRate
        {
            get
            {
                return commissionRate;
            }
            set
            {
                if (value <= 0 || value >= 1) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(CommissionRate)} must be > 0 and < 1");
                }

                commissionRate = value;
            }
        }

        // calculate earnings; override abstract method Earnings in Employee
        public override decimal Earnings() => CommissionRate * GrossSales;

        // return string representation of CommissionEmployee object
        public override string ToString() =>
           $"commission employee: {base.ToString()}\n" +
           $"gross sales: {GrossSales:C}\n" +
           $"commission rate: {CommissionRate:F2}";
    }

    public class BasePlusCommissionEmployee : CommissionEmployee
    {
        private decimal baseSalary; // base salary per week

        // six-parameter constructor
        public BasePlusCommissionEmployee(string firstName, string lastName,
           string socialSecurityNumber, int month, int day, int year, decimal grossSales,
           decimal commissionRate, decimal baseSalary)
           : base(firstName, lastName, socialSecurityNumber, month, day, year,
                grossSales, commissionRate)                                         // now adds birth date
        {
            BaseSalary = baseSalary; // validates base salary
        }

        // property that gets and sets 
        // BasePlusCommissionEmployee's base salary
        public decimal BaseSalary
        {
            get
            {
                return baseSalary;
            }
            set
            {
                if (value < 0) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(BaseSalary)} must be >= 0");
                }

                baseSalary = value;
            }
        }

        // calculate earnings
        public override decimal Earnings() => BaseSalary + base.Earnings();

        // return string representation of BasePlusCommissionEmployee
        public override string ToString() =>
           $"base-salaried {base.ToString()}\nbase salary: {BaseSalary:C}";
    }

    public class HourlyEmployee : Employee
    {
        private decimal wage; // wage per hour
        private decimal hours; // hours worked for the week

        // eight-parameter constructor
        public HourlyEmployee(string firstName, string lastName,
           string socialSecurityNumber, int month, int day, int year, decimal hourlyWage,
           decimal hoursWorked)
           : base(firstName, lastName, socialSecurityNumber, month, day, year)              // now adds birth date
        {
            Wage = hourlyWage; // validate hourly wage 
            Hours = hoursWorked; // validate hours worked 
        }

        // property that gets and sets hourly employee's wage
        public decimal Wage
        {
            get
            {
                return wage;
            }
            set
            {
                if (value < 0) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(Wage)} must be >= 0");
                }

                wage = value;
            }
        }

        // property that gets and sets hourly employee's hours
        public decimal Hours
        {
            get
            {
                return hours;
            }
            set
            {
                if (value < 0 || value > 168) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(Hours)} must be >= 0 and <= 168");
                }

                hours = value;
            }
        }

        // calculate earnings; override Employee’s abstract method Earnings
        public override decimal Earnings()
        {
            if (Hours <= 40) // no overtime                          
            {
                return Wage * Hours;
            }
            else
            {
                return (40 * Wage) + ((Hours - 40) * Wage * 1.5M);
            }
        }

        // return string representation of HourlyEmployee object
        public override string ToString() =>
           $"hourly employee: {base.ToString()}\n" +
           $"hourly wage: {Wage:C}\nhours worked: {Hours:F2}";
    }

    public class SalariedEmployee : Employee
    {
        private decimal weeklySalary;

        // seven-parameter constructor
        public SalariedEmployee(string firstName, string lastName,
           string socialSecurityNumber, int month, int day, int year, decimal weeklySalary)
           : base(firstName, lastName, socialSecurityNumber, month, day, year)              // now adds birth date
        {
            WeeklySalary = weeklySalary; // validate salary via property
        }

        // property that gets and sets salaried employee's salary
        public decimal WeeklySalary
        {
            get
            {
                return weeklySalary;
            }
            set
            {
                if (value < 0) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(WeeklySalary)} must be >= 0");
                }

                weeklySalary = value;
            }
        }

        // calculate earnings; override abstract method Earnings in Employee
        public override decimal Earnings() => WeeklySalary;

        // return string representation of SalariedEmployee object
        public override string ToString() =>
           $"salaried employee: {base.ToString()}\n" +
           $"weekly salary: {WeeklySalary:C}";
    }

    public class Date
    {
        private int month; // 1-12
        private int day; // 1-31 based on month
        public int Year { get; private set; } // auto-implemented property Year

        // constructor: use property Month to confirm proper value for month; 
        // use property Day to confirm proper value for day
        public Date(int month, int day, int year)
        {
            Month = month; // validates month
            Year = year; // could validate year
            Day = day; // validates day
            Console.WriteLine($"Date object constructor for date {this}");
        }

        // property that gets and sets the month
        public int Month
        {
            get
            {
                return month;
            }
            private set // make writing inaccessible outside the class
            {
                if (value <= 0 || value > 12) // validate month
                {
                    throw new ArgumentOutOfRangeException(
                       nameof(value), value, $"{nameof(Month)} must be 1-12");
                }

                month = value;
            }
        }

        // property that gets and sets the day
        public int Day
        {
            get
            {
                return day;
            }
            private set // make writing inaccessible outside the class
            {
                int[] daysPerMonth =
                   {0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

                // check if day in range for month
                if (value <= 0 || value > daysPerMonth[Month])
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value,
                       $"{nameof(Day)} out of range for current month/year");
                }
                // check for leap year
                if (Month == 2 && value == 29 &&
                   !(Year % 400 == 0 || (Year % 4 == 0 && Year % 100 != 0)))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value,
                       $"{nameof(Day)} out of range for current month/year");
                }

                day = value;
            }
        }

        // return a string of the form month/day/year
        public override string ToString() => $"{Month}/{Day}/{Year}";
    }

    class PayrollSystemTest
    {
        static void Main()
        {
            // create derived class objects
            SalariedEmployee salariedEmployee1 = new SalariedEmployee("John",
               "Smith", "111-11-1111", 6, 15, 1944, 800M);
            HourlyEmployee hourlyEmployee1 = new HourlyEmployee("Karen",
               "Price", "222-22-2222", 12, 29, 1960, 16.75M, 40M);
            CommissionEmployee commissionEmployee1 =
               new CommissionEmployee("Sue", "Jones", "333-33-3333",
               9, 8, 1954, 10000M, .06M);
            BasePlusCommissionEmployee basePlusCommissionEmployee1 =
               new BasePlusCommissionEmployee("Bob", "Lewis", "444-44-4444",
               3, 2, 1965, 5000M, .04M, 300M);

            SalariedEmployee salariedEmployee2 = new SalariedEmployee("Afatarli",
               "Valeri", "511-11-1111", 1, 5, 1915, 600M);
            HourlyEmployee hourlyEmployee2 = new HourlyEmployee("Henry",
               "Buris", "622-22-2222", 2, 9, 1970, 14.75M, 36M);
            CommissionEmployee commissionEmployee2 =
               new CommissionEmployee("Anthony", "Doss", "733-33-3333",
               4, 18, 1944, 10045M, .07M);
            BasePlusCommissionEmployee basePlusCommissionEmployee2 =
               new BasePlusCommissionEmployee("Bob", "Lewis", "844-44-4444",
               5, 1, 1960, 4300M, .05M, 290M);

            SalariedEmployee salariedEmployee3 = new SalariedEmployee("Linda",
               "Ha", "911-11-1111", 7, 25, 1968, 400M);
            HourlyEmployee hourlyEmployee3 = new HourlyEmployee("Brian",
               "Louie", "122-22-2222", 8, 6, 1966, 18.85M, 60M);
            CommissionEmployee commissionEmployee3 =
               new CommissionEmployee("Leon", "Powell", "233-33-3333",
               10, 22, 1959, 10100M, .07M);
            BasePlusCommissionEmployee basePlusCommissionEmployee3 =
               new BasePlusCommissionEmployee("David", "Tong", "344-44-4444",
               11, 8, 1970, 5080M, .08M, 400M);


            Console.WriteLine("Employees processed individually:\n");

            Console.WriteLine("{0}\n{1}: {2:C}\n", salariedEmployee1, "earned", salariedEmployee1.Earnings());
            Console.WriteLine("{0}\n{1}: {2:C}\n", hourlyEmployee1, "earned", hourlyEmployee1.Earnings());
            Console.WriteLine("{0}\n{1}: {2:C}\n", commissionEmployee1, "earned", commissionEmployee1.Earnings());
            Console.WriteLine("{0}\n{1}: {2:C}\n", basePlusCommissionEmployee1, "earned", basePlusCommissionEmployee1.Earnings());

            Console.WriteLine("{0}\n{1}: {2:C}\n", salariedEmployee2, "earned", salariedEmployee2.Earnings());
            Console.WriteLine("{0}\n{1}: {2:C}\n", hourlyEmployee2, "earned", hourlyEmployee2.Earnings());
            Console.WriteLine("{0}\n{1}: {2:C}\n", commissionEmployee2, "earned", commissionEmployee2.Earnings());
            Console.WriteLine("{0}\n{1}: {2:C}\n", basePlusCommissionEmployee2, "earned", basePlusCommissionEmployee2.Earnings());


            Console.WriteLine("{0}\n{1}: {2:C}\n", salariedEmployee3, "earned", salariedEmployee3.Earnings());
            Console.WriteLine("{0}\n{1}: {2:C}\n", hourlyEmployee3, "earned", hourlyEmployee3.Earnings());
            Console.WriteLine("{0}\n{1}: {2:C}\n", commissionEmployee3, "earned", commissionEmployee3.Earnings());
            Console.WriteLine("{0}\n{1}: {2:C}\n", basePlusCommissionEmployee3, "earned", basePlusCommissionEmployee3.Earnings());


            // create four-element Employee array
            Employee[] employees = new Employee[12];

            // initialize array with Employees
            employees[0] = salariedEmployee1;
            employees[1] = hourlyEmployee1;
            employees[2] = commissionEmployee1;
            employees[3] = basePlusCommissionEmployee1;

            employees[4] = salariedEmployee2;
            employees[5] = hourlyEmployee2;
            employees[6] = commissionEmployee2;
            employees[7] = basePlusCommissionEmployee2;

            employees[8] = salariedEmployee3;
            employees[9] = hourlyEmployee3;
            employees[10] = commissionEmployee3;
            employees[11] = basePlusCommissionEmployee3;

            int currentMonth;

            // get and validate current month
            do
            {
                Console.Write("Enter the current month (1 - 12): ");
                currentMonth = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
            } while ((currentMonth < 1) || (currentMonth > 12));

            Console.WriteLine("Employees processed polymorphically:\n");

            // generically process each element in array employees
            foreach (var currentEmployee in employees)
            {
                Console.WriteLine(currentEmployee); // invokes ToString

                // determine whether element is a BasePlusCommissionEmployee
                if (currentEmployee is BasePlusCommissionEmployee)
                {
                    // downcast Employee reference to 
                    // BasePlusCommissionEmployee reference
                    BasePlusCommissionEmployee employee =
                       (BasePlusCommissionEmployee)currentEmployee;

                    employee.BaseSalary *= 1.1M;
                    Console.WriteLine(
                       "new base salary with 10% increase is: {0:C}",
                       employee.BaseSalary);
                } // end if

                // if month of employee's birthday, add $100 to salary
                if (currentMonth == currentEmployee.BirthDate.Month)
                    Console.WriteLine(
                       "earned {0:C} {1}\n", currentEmployee.Earnings(),
                       "plus $100.00 birthday bonus");
                else
                    Console.WriteLine(
                       "earned {0:C}\n", currentEmployee.Earnings());
            } // end for

            // get type name of each object in employees array
            for (int j = 0; j < employees.Length; j++)
                Console.WriteLine("Employee {0} is a {1}", j,
                   employees[j].GetType());


            Console.ReadKey();

        }
    }

}
