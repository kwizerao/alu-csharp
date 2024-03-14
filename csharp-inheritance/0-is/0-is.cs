using System;


class Obj{

/// <summary>
/// Check for integer type
/// </summary>
/// <param name="obj"></param>
/// <returns></returns>
    public static bool IsOfTypeInt(Object obj){
        if(obj is int){
            return true;
        }

        return false;
    }
}