using System;


public class User : BaseClass {

    public string? name
    {
        get;
        set;
    }
    public User(string name){
        this.name = name;
    }
}