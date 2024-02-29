using System.Collections;

class LList{
    public static LinkedList<int> CreatePrint(int size){

        LinkedList<int> linkedList = new LinkedList<int>();

        if (size < 0)
        {
            return linkedList;
        }
        else{
            for(int i = 0; i < size; i++){
                Console.WriteLine(i + "");
                linkedList.AddFirst(i);
            }
        }
        return linkedList;
    }
}