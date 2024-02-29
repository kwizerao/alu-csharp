using System.Collections;

class LList{

    public static int Length(LinkedList<int> myList){
        int count = 0;
        foreach(int value in myList){
            count++;
        }
        return count;
    }
}