using System;
using System.Data;
using System.IO.Compression;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Text.Json;
using System.Threading.Tasks.Dataflow;

public class Program
{
    private static bool running = true;
    private static JsonStorage? storage;
    private static string[] AllClasses = new[] { "user", "inventory", "item" };

    private static Dictionary<string, string> ClassCombination = new Dictionary<string, string>(){ {"user","User"}, {"inventory", "Inventory"}, {"item", "Item"}};

    public static void Main(string[] args)
    {
        storage = JsonStorage.Instance;
        
        PrintAvailableCommands();
        /*
        Item temp = new Item("Nwalahnjie");
        temp.description = "Testing Descriptions";
        temp.price = 10f;
        storage?.New(temp);
        storage?.New(new Item("Anye"));
        
        
        storage?.New(new User("anTe"));
        User newUser = new User("Nwalahnjie Anye");
        newUser.date_created = DateTime.Now;
        
        storage?.New(newUser);
        
        storage?.New(new Inventory(newUser.id!, temp.id!, 20));
        
        storage?.Save();
        
        storage?.Load();
        */
      
        /*
        Dictionary<string, object> allData = storage?.All()!;

        foreach (var data in allData)
        {
            Console.WriteLine(data.Key);
            Console.WriteLine(data.Value);
        } */

        while (running)
        {
            Console.Write("> ");
            string inputLine = Console.ReadLine()!;
            string[] arguments = inputLine.Split(' ');

            if (arguments.Length > 0)
            {
                string command = arguments[0].ToLower();
                ProcessCommand(command, arguments);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a command");
                // show all commands
                PrintAvailableCommands();
            }
        }
    }

    private static void ProcessCommand(string command, string[] arguments)
    {
        switch (command)
        {
            case "create":
                if (arguments.Length > 2)
                {
                    Console.WriteLine("Too Many Arguments");
                }
                if (arguments.Length == 2)
                {
                    if (arguments[1].ToLower() == AllClasses[0])
                    {
                        User instance = new User("Test");
                        storage?.New(instance);
                        storage?.Save();
                        break;
                    }
                    
                    if (arguments[1].ToLower() == AllClasses[1])
                    {
                        Inventory instance = new Inventory("Test", "Test", 10);
                        storage?.New(instance);
                        storage?.Save();
                        break;
                    }
                    
                    if (arguments[1].ToLower() == AllClasses[2])
                    {
                        Item instance = new Item("Test");
                        storage?.New(instance);
                        storage?.Save();
                        break;
                    }
                    
                    Console.WriteLine("<ClassName> is not a valid object type");
                    
                    /*
                    foreach (var data in ClassCombination)
                    {
                        if (arguments[1].ToLower() == data.Key)
                        {
                            Console.WriteLine(data.Value);
                            Type type = Type.GetType(data.Value);
                            if (type != null)
                            {
                                Console.WriteLine("type found");
                                object instance = Activator.CreateInstance(type);
                                storage?.New(instance);
                                storage?.Save();
                            }
                           
                            break;
                        }
                    } */
                }
                else
                {
                    PrintAvailableCommands();
                }
                break;
            case "classnames":
                ShowClassNames();
                break;
            case "all":
                if (arguments.Length > 2)
                {
                    Console.WriteLine("Too many Arguments");
                }

                if (arguments.Length == 1)
                {
                    AllObjects();
                    break;
                }

                if (arguments.Length == 2)
                {
                    Dictionary<string, object> allData = storage?.All();
                    if (arguments[1].ToLower() == AllClasses[0])
                    {
                        foreach (var data in allData)
                        {
                            string[] ClassName = data.Key.Split(".");
                            if (ClassName[0].ToLower() == AllClasses[0])
                            {
                                Console.WriteLine(data.Value);
                            }
                        }

                        break;
                    }
                    
                    if (arguments[1].ToLower() == AllClasses[1])
                    {
                        foreach (var data in allData)
                        {
                            string[] ClassName = data.Key.Split(".");
                            if (ClassName[0].ToLower() == AllClasses[1])
                            {
                                Console.WriteLine(data.Value);
                            }
                        }

                        break;
                    }
                    
                    if (arguments[1].ToLower() == AllClasses[2])
                    {
                        foreach (var data in allData)
                        {
                            string[] ClassName = data.Key.Split(".");
                            if (ClassName[0].ToLower() == AllClasses[2])
                            {
                                Console.WriteLine(data.Value);
                            }
                        }

                        break;
                    }
                    
                    Console.WriteLine("<ClassName> is not a valid object type");
                }
                else
                {
                    PrintAvailableCommands();
                }
                break;
            case "show":
                if (arguments.Length > 3)
                {
                    Console.WriteLine("Too many Arguments");
                }

                if (arguments.Length <= 2)
                {
                    break;
                }

                if (arguments.Length == 3)
                {
                    Dictionary<string, object> allData = storage?.All()!;
                    if (arguments[1].ToLower() == AllClasses[0])
                    {
                        foreach (var data in allData)
                        {
                            string completeId = arguments[1] + "." + arguments[2];
                            if (completeId == data.Key)
                            {
                                Console.WriteLine(data.Value);
                            }
                        }

                        break;
                    }
                    
                    if (arguments[1].ToLower() == AllClasses[1])
                    {
                        foreach (var data in allData)
                        {
                            string completeId = arguments[1] + "." + arguments[2];
                            if (completeId == data.Key)
                            {
                                Console.WriteLine(data.Value);
                            }
                        }

                        break;
                    }
                    
                    if (arguments[1].ToLower() == AllClasses[2])
                    {
                        foreach (var data in allData)
                        {
                            string completeId = arguments[1] + "." + arguments[2];
                            if (completeId == data.Key)
                            {
                                Console.WriteLine(data.Value);
                            }
                        }

                        break;
                    }
                    
                    Console.WriteLine("Object <id> could not be found");
                }
                else
                {
                    PrintAvailableCommands();
                }
                
                break;
            case "delete":
                if (arguments.Length > 3)
                {
                    Console.WriteLine("Too many Arguments");
                }

                if (arguments.Length <= 2)
                {
                    break;
                }

                if (arguments.Length == 3)
                {
                    Dictionary<string, object> allData = storage?.All();
                    if (arguments[1].ToLower() == AllClasses[0])
                    {
                        foreach (var data in allData)
                        {
                            string completeId = arguments[1] + "." + arguments[2];
                            if (completeId == data.Key)
                            {
                                allData.Remove(data.Key);
                                storage?.Save();
                            }
                        }

                        break;
                    }
                    
                    if (arguments[1].ToLower() == AllClasses[1])
                    {
                        foreach (var data in allData)
                        {
                            string completeId = arguments[1] + "." + arguments[2];
                            if (completeId == data.Key)
                            {
                                allData.Remove(data.Key);
                                storage?.Save();
                            }
                        }

                        break;
                    }
                    
                    if (arguments[1].ToLower() == AllClasses[2])
                    {
                        foreach (var data in allData)
                        {
                            string completeId = arguments[1] + "." + arguments[2];
                            if (completeId == data.Key)
                            {
                                allData.Remove(data.Key);
                                storage?.Save();
                            }
                        }

                        break;
                    }
                    
                    Console.WriteLine("Object <id> could not be found");
                }
                else
                {
                    PrintAvailableCommands();
                }
                break;
            case "update":
                if (arguments.Length > 3)
                {
                    Console.WriteLine("Too many Arguments");
                }

                if (arguments.Length <= 2)
                {
                    Console.WriteLine("Arguments are Less than Required");
                    break;
                }

                if (arguments.Length == 3)
                {
                    Console.WriteLine("The Right Number of Arguments Used here?");
                    Dictionary<string, object> allData = storage?.All();
                    
                    if (arguments[1].ToLower() == AllClasses[0])
                    {
                        Console.WriteLine("The User Class has been called");
                        foreach (var data in allData)
                        {
                            string completeId = "User." + arguments[2];
                           // Console.WriteLine("Foreach is running");
                           // Console.WriteLine("The Complete Key is: " + completeId);
                            if (completeId == data.Key)
                            {
                               // Console.WriteLine("Executing User Update");
                               // Console.WriteLine(data.Value);
                                Object obj = data.Value;
                                //Console.WriteLine(obj.GetType());
                               // Console.WriteLine(obj);
                                JsonElement element = (JsonElement)obj;
                                Console.WriteLine("Json Element is : " + element);

                                string Username = "";
                                DateTime created = DateTime.Now;
                                DateTime updated = DateTime.Now;
                                
                               
                                foreach (var property in element.EnumerateObject())
                                {
                                    string propertyName = property.Name;
                                   // Console.WriteLine("Property Names : " + propertyName);
                                    if (propertyName == "name" && property.Value.ValueKind == JsonValueKind.String)
                                    {
                                        Username = property.Value.GetString();
                                    }
                                    
                                    if (propertyName == "date_created" && property.Value.ValueKind == JsonValueKind.Number)
                                    {
                                        created = property.Value.GetDateTime();
                                    }
                                    
                                    if (propertyName == "date_updated" && property.Value.ValueKind == JsonValueKind.Number)
                                    {
                                        updated = property.Value.GetDateTime();
                                    }
                                }

                                User temp = new User(Username);
                                temp.date_created = created;
                                temp.date_updated = updated;
                                allData[completeId] = temp;
                            }
                        }
                        storage?.Save();

                        break;
                    }
                    
                    if (arguments[1].ToLower() == AllClasses[1])
                    {
                        foreach (var data in allData)
                        {
                            string completeId = "Inventory." + arguments[2];
                            if (completeId == data.Key)
                            {
                                 // Console.WriteLine("Executing User Update");
                               // Console.WriteLine(data.Value);
                                Object obj = data.Value;
                                //Console.WriteLine(obj.GetType());
                               // Console.WriteLine(obj);
                                JsonElement element = (JsonElement)obj;
                                Console.WriteLine("Json Element is : " + element);
                                
                                DateTime created = DateTime.Now;
                                DateTime updated = DateTime.Now;
                                string user_id = "";
                                string item_id = "";
                                int quantity = 0;
                                
                               
                                foreach (var property in element.EnumerateObject())
                                {
                                    string propertyName = property.Name;
                                   // Console.WriteLine("Property Names : " + propertyName);
                                    if (propertyName == "user_id" && property.Value.ValueKind == JsonValueKind.String)
                                    {
                                        user_id = property.Value.GetString();
                                    }
                                    
                                    if (propertyName == "item_id" && property.Value.ValueKind == JsonValueKind.String)
                                    {
                                        item_id = property.Value.GetString();
                                    }
                                    
                                    if (propertyName == "quantity" && property.Value.ValueKind == JsonValueKind.Number)
                                    {
                                        quantity = property.Value.GetInt32();
                                    }
                                    
                                    if (propertyName == "date_created" && property.Value.ValueKind == JsonValueKind.Number)
                                    {
                                        created = property.Value.GetDateTime();
                                    }
                                    
                                    if (propertyName == "date_updated" && property.Value.ValueKind == JsonValueKind.Number)
                                    {
                                        updated = property.Value.GetDateTime();
                                    }
                                }

                                Inventory temp = new Inventory(user_id, item_id, quantity);
                                temp.date_created = created;
                                temp.date_updated = updated;
                                allData[completeId] = temp;
                            }
                            
                            storage?.Save();
                        }

                        break;
                    }
                    
                    if (arguments[1].ToLower() == AllClasses[2])
                    {
                        foreach (var data in allData)
                        {
                            string completeId = "Item." + arguments[2];
                            if (completeId == data.Key)
                            {
                                 // Console.WriteLine("Executing User Update");
                               // Console.WriteLine(data.Value);
                                Object obj = data.Value;
                                //Console.WriteLine(obj.GetType());
                               // Console.WriteLine(obj);
                                JsonElement element = (JsonElement)obj;
                                Console.WriteLine("Json Element is : " + element);
                                
                                DateTime created = DateTime.Now;
                                DateTime updated = DateTime.Now;
                                string Username = "";
                                int price = 0;
                                string description = "";
                                
                               
                                foreach (var property in element.EnumerateObject())
                                {
                                    string propertyName = property.Name;
                                   // Console.WriteLine("Property Names : " + propertyName);
                                    if (propertyName == "name" && property.Value.ValueKind == JsonValueKind.String)
                                    {
                                        Username = property.Value.GetString();
                                    }
                                    
                                    if (propertyName == "price" && property.Value.ValueKind == JsonValueKind.Number)
                                    {
                                        price = property.Value.GetInt32();
                                    }
                                    
                                    if (propertyName == "description" && property.Value.ValueKind == JsonValueKind.String)
                                    {
                                        description = property.Value.GetString();
                                    }
                                    
                                    if (propertyName == "date_created" && property.Value.ValueKind == JsonValueKind.Number)
                                    {
                                        created = property.Value.GetDateTime();
                                    }
                                    
                                    if (propertyName == "date_updated" && property.Value.ValueKind == JsonValueKind.Number)
                                    {
                                        updated = property.Value.GetDateTime();
                                    }
                                }

                                Item temp = new Item(Username);
                                temp.date_created = created;
                                temp.date_updated = updated;
                                temp.price = price;
                                temp.description = description;
                                allData[completeId] = temp;
                            }
                            
                            storage?.Save();
                        }

                        break;
                    }
                    
                    Console.WriteLine("Object <id> could not be found");
                }
                else
                {
                    PrintAvailableCommands();
                }
                break;
            case "exit":
                running = false;
                break;
            default:
                PrintAvailableCommands();
                break;
        }
    }

    /*
    private static void ProcessCommand(string command)
    {
        switch (command.ToLower())
        {
            case "create":
                Console.WriteLine("Creating something...");
                // Add your logic for the create command here
                break;
            case "update":
                Console.WriteLine("Updating something...");
                // Add your logic for the update command here
                break;
            case "exit":
                running = false;
                Console.WriteLine("Exiting...");
                break;
            case "classnames":
                ShowClassNames();
                break;
            case "all":
                AllObjects();
                break;
            default:
                Console.WriteLine("Invalid command. Try 'create', 'update', or 'exit'.");
                break;
        }
    }
    */

    public static void AllObjects()
    {
        Dictionary<string, object> allData = storage?.All()!;
        
        Console.WriteLine("All Objects");
        Console.WriteLine("---------------------------------------------");
        
        foreach (var data in allData)
        {
            Console.WriteLine(data.Value);
        } 
        
        PrintAvailableCommands();
    }
    

    public static void ShowClassNames()
    {
        Console.WriteLine("Class Names");
        Console.WriteLine("-----------");
        Console.WriteLine("User");
        Console.WriteLine("Inventory");
        Console.WriteLine("Item");
    }

    
    public static void PrintAvailableCommands(){
        Console.WriteLine("Inventory Manager");
        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine("<ClassName> show all ClassNames of objects");
        Console.WriteLine("<All> show all objects");
        Console.WriteLine("<All [ClassName]> show all objects of a ClassName");
        Console.WriteLine("<Create [ClassName]> a new object");
        Console.WriteLine("<Show [ClassName object_id]> an object");
        Console.WriteLine("<Update [ClassName object_id]> an object");
        Console.WriteLine("<Delete [ClassName object_id]> an object");
        Console.WriteLine("<Exit>");
    }

}