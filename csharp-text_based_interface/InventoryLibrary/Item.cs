using System;

public class Item : BaseClass{

    public string? name
    {
        get;
        set;
    }

    public string? description {
        get ; set;
    }

    
    private float Price;
    public float price {
        get { return (float)Math.Round(Price, 2);}
        set { Price = value; }
    }

    public List<string>? tags = new List<string>();


    public Item(string name){
        this.name = name;
    }
}