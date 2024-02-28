using System;
using System.Net.Mail;

class Program{
    static void Main(String[] args){

         List<int> CurrentNumbers = new List<int>();
         int FirstForm = 0;
         int SecondForm = 0;

         for(int i = 0; i < 100 ; i++){
            string normalNumber = i.ToString("D2");
            
            int current00 = int.Parse(normalNumber.Substring(0,1));
            int current01 = int.Parse(normalNumber.Substring(1,1));
          
           

            FirstForm = int.Parse( current00.ToString() + current01.ToString());
            SecondForm = int.Parse(current01.ToString() + current00.ToString());

            if(FirstForm < SecondForm){
                if(CurrentNumbers.Contains(FirstForm)){
                    continue;
                }else{
                    CurrentNumbers.Add(FirstForm);
                }
                
            }else if(FirstForm > SecondForm){
                if(CurrentNumbers.Contains(SecondForm)){
                    continue;
                }else{
                    CurrentNumbers.Add(SecondForm);
                }
            }else if(FirstForm == SecondForm){
                continue;
            }

            }

            int count = CurrentNumbers.Count;
            for(int i = 0; i < count ; i ++){
                if( i == count - 1){
                      Console.WriteLine($"{CurrentNumbers[i]}");
                }else{                
                      Console.Write($"{CurrentNumbers[i]:D2}, ");
                }             
            }
        }
}