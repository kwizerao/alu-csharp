namespace InventoryManagement.Tests;

public class Tests
{
    private static JsonStorage? storage;
    private User newUser;
    private Item temp;
    private Dictionary<string, object> Data;
    
    [SetUp]
    public void Setup()
    {
        storage = JsonStorage.Instance;
        
        temp = new Item("Nwalahnjie");
        temp.description = "Testing Descriptions";
        temp.price = 10f;
        storage?.New(temp);
        storage?.New(new Item("Anye"));
        
        
        storage?.New(new User("anTe"));
        newUser = new User("Nwalahnjie Anye");
        newUser.date_created = DateTime.Now;
        
        storage?.New(newUser);
        
        storage?.New(new Inventory(newUser.id!, temp.id!, 20));
        
        storage?.Save();
        
        storage?.Load();
    }
    
    [TearDown]
    public void CleanUp()
    {
        storage?.EmptyFile();
    }


    [Test]
    public void Test_User()
    {
        User TestUser = new User("Test");
        
        Assert.IsTrue(TestUser.name == "Test");
    }

    [Test]
    public void Test_Item()
    {
        Item TestItem = new Item("Test");
        TestItem.price = 10;
        TestItem.description = "test description";
        
        Assert.IsTrue(TestItem.name == "Test");
        Assert.IsTrue(TestItem.price == 10);
        Assert.IsTrue(TestItem.description == "test description");
    }

    [Test]
    public void Test_Inventory()
    {
        Inventory testInventory = new Inventory(newUser.id!, temp.id!, 20);
        Assert.IsTrue(testInventory.item_id == temp.id);
        Assert.IsTrue(testInventory.user_id == newUser.id);
        Assert.IsTrue(testInventory.quantity == 20);
    }

    [Test]
    public void Test_UserAddition()
    {
       int count =  storage.All().Count;
       Assert.IsTrue(count == 5 );
    }

    [Test]
    public void Test_Adding()
    {
        User AnotherUser = new User("Test");
        storage?.New(AnotherUser);
        storage.Save();

        int count = storage.All().Count;
        Assert.IsTrue(count == 6);
        
    }

    [Test]
    public void Test_Deleting()
    {
        Data = storage?.All();
        Data.Remove(newUser.id!);

        int count = storage!.All().Count;
        Assert.IsTrue(count == 5);
    }

 
    
    
}