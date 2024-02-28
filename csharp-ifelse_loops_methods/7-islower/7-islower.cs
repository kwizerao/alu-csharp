using System;

class Character{

    static List<char> lowerCase = new List<char>();
    public static bool IsLower(char c){

        for(char i = 'a'; i <= 'z'; i++){
            lowerCase.Add(i);
        }

        if(lowerCase.Contains(c)){
            return true;
        }else{
            return false;
        }
    }
}