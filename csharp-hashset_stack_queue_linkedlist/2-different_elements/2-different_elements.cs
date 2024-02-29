using System.Collections;

class List{

    public static List<int> DifferentElements(List<int> list1, List<int> list2){
        HashSet<int> HashOne = new HashSet<int>(list1);
        HashSet<int> HashTwo = new HashSet<int>(list2);

        HashOne.SymmetricExceptWith(HashTwo);
        int[] arrInt;
        arrInt = HashOne.ToArray();
        Array.Sort(arrInt);
        HashOne.Clear();
        HashOne.UnionWith(arrInt);

        return new List<int>(HashOne);
    }
}