using EmployeeRecordSystem;
using System;

namespace EmployeeRecordSystem
{
        class EmployeeRecordSystem
        {
            static void Main(string[] args)
            {
            IMemoryDB memoryDB = new MemoryDB();

                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("\nEmployee Record System");
                    Console.WriteLine("1. Add Employee");
                    Console.WriteLine("2. Update Employee");
                    Console.WriteLine("3. Delete Employee");
                    Console.WriteLine("4. Find Employee by ID");
                    Console.WriteLine("5. Search Employee by Name");
                    Console.WriteLine("6. Show All Employees");
                    Console.WriteLine("7. Exit");
                    Console.Write("Choose an option: ");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddEmployee(memoryDB);
                            break;
                        case "2":
                            UpdateEmployee(memoryDB);
                            break;
                        case "3":
                            DeleteEmployee(memoryDB);
                            break;
                        case "4":
                            FindEmployee(memoryDB);
                            break;
                        case "5":
                            SearchEmployee(memoryDB);
                            break;
                        case "6":
                            ShowAllEmployees(memoryDB);
                            break;
                        case "7":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice, please try again.");
                            break;
                    }
                }
            }

            static void AddEmployee(IMemoryDB db)
            {
                Console.Write("Enter Employee ID: ");
                int id = int.Parse(Console.ReadLine());
                Console.Write("Enter Employee Name: ");
                string name = Console.ReadLine();

                db.AddRecord(id, name);
                Console.WriteLine("Employee added successfully.");
            }

            static void UpdateEmployee(IMemoryDB db)
            {
                Console.Write("Enter Employee ID to Update: ");
                int id = int.Parse(Console.ReadLine());
                Console.Write("Enter New Employee Name: ");
                string name = Console.ReadLine();

                db.UpdateRecord(id, name);
                Console.WriteLine("Employee updated successfully.");
            }

            static void DeleteEmployee(IMemoryDB db)
            {
                Console.Write("Enter Employee ID to Delete: ");
                int id = int.Parse(Console.ReadLine());

                db.DeleteRecord(id);
                Console.WriteLine("Employee deleted successfully.");
            }

            static void FindEmployee(IMemoryDB db)
            {
                Console.Write("Enter Employee ID to Find: ");
                int id = int.Parse(Console.ReadLine());

                var result = db.FindRecord(id);
                if (result.Item2 != null)
                    Console.WriteLine($"Employee Found: ID = {result.Item1}, Name = {result.Item2}");
                else
                    Console.WriteLine("Employee not found.");
            }

            static void SearchEmployee(IMemoryDB db)
            {
                Console.Write("Enter part of Employee Name to Search: ");
                string stub = Console.ReadLine();

                var results = db.Search(stub);
                if (results.Count > 0)
                {
                    Console.WriteLine("Search Results:");
                    foreach (var (id, name) in results)
                        Console.WriteLine($"ID = {id}, Name = {name}");
                }
                else
                    Console.WriteLine("No employees found with the given search term.");
            }

            static void ShowAllEmployees(IMemoryDB db)
            {
                var records = db.GetAllRecords();
                if (records.Count > 0)
                {
                    Console.WriteLine("All Employees:");
                    foreach (var (id, name) in records)
                        Console.WriteLine($"ID = {id}, Name = {name}");
                }
                else
                    Console.WriteLine("No employees in the system.");
            }
        }
}