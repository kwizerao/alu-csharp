using System.Collections;
using System;

class List{
    public static List<int> Divide(List<int> list1, List<int> list2, int listLength){
        List<int> result = new List<int>();
        int sonder = 0;
//sonder will temporarily hold the result for the division
        for(int i = 0; i < listLength ; i++){
            try{
                sonder = list1[i] / list2[i];
                result.Add(sonder);
            }catch(ArgumentOutOfRangeException){
                Console.WriteLine("Out of range");
            }catch(DivideByZeroException){
                Console.WriteLine("Cannot divide by zero");
                result.Add(0);
            }
        }

        return result;
    }
}