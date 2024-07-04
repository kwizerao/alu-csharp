using System;
using System.Dynamic;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;

public class JsonStorage {

    private static void Main(String[] args)
    {
        
    }

    private static JsonStorage? _instance;
    private static readonly object Lock = new object();

    private JsonStorage() {}

    public static JsonStorage? Instance {
        get {
            if(_instance == null){
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new JsonStorage();
                    }
                }
            }

            return _instance;
        }
    }

    Dictionary<string, object> _objects = new Dictionary<string, object>();
    readonly string _filename =  "inventory_manager.json";
    string _path = String.Empty;
    private string _baseDirectory = string.Empty;
    string _newDirectoryName = "storage";

   // private string temporalpath =  "C:\\Users\\Lenovo\\2023\\SE-SPECIALIZATION\\Programming C#\\alu-csharp\\csharp-text_based_interface\\storage\\inventory_manager.json";
    
    
    string? _data = default(string);

    public Dictionary<string, object> All(){
        return _objects;
    }

    public void New(object value){
        BaseClass? classObject = value as BaseClass;
        string objId = $"{value}.{classObject?.id}";

        _objects.Add(objId, value);
    }

    public void PathCreations()
    {
        // _baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        _baseDirectory = Path.GetFullPath(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!);
    }



    /// <summary>
    /// serialize all objects
    /// </summary>   
    public void Save(){

       PathCreations();
       string newDirectoryFullPath = Path.Combine(_baseDirectory, "..", "..", "..", "..", _newDirectoryName);

       if(!Directory.Exists(newDirectoryFullPath)){
          Directory.CreateDirectory(newDirectoryFullPath);
       }

       _path = Path.Combine(newDirectoryFullPath, _filename);
       // Console.WriteLine(_path);
    
        var options = new JsonSerializerOptions { WriteIndented = true };

      

        _data = JsonSerializer.Serialize(_objects, options);
       

        File.WriteAllText(_path, _data);
        
        /*
        if(File.Exists(path)){  
            
        } 
        */

       
    }

    public void EmptyFile()
    {
        if (File.Exists(_path))
        {
            _objects = new Dictionary<string, object>();
            Save();
        }
       
    }

    /// <summary>
    /// deserialize all objects back
    /// </summary>

    public void Load(){
        
        
       // Console.WriteLine(_path);
        if(File.Exists(_path)){
            if (_path != null)
            {
               // Console.WriteLine("Path exist if this is shown");
                string data = File.ReadAllText(_path);
                //List<SampleData>?  dataList = JsonSerializer.Deserialize<List<SampleData>>(Data);
                //objects = dataList.ToDictionary(data => data.classname);
                var options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };
                _objects = JsonSerializer.Deserialize<Dictionary<string, object>>(data, options) ?? throw new InvalidOperationException();
            }
        }else{
                Console.WriteLine("File Does Not Exist");
        }
    }


}