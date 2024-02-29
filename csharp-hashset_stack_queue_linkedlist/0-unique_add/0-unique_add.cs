using System.Collections;

class List{
    public static int Sum(List<int> myList){
        HashSet<int> Elements = new HashSet<int>(myList);
        int sum = 0;
        foreach(int value in Elements){
            sum += value;
        }

        return sum;
    }
}