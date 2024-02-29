using System.Collections;

class List{

    public static List<int> CommonElements(List<int> list1, List<int> list2){
        HashSet<int> HashOne = new HashSet<int>(list1);
        HashSet<int> HashTwo = new HashSet<int>(list2);

        HashTwo.IntersectWith(HashOne);
        List<int> nList = new List<int>(HashTwo);

        return nList;
    }
}