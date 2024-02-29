using System;
using System.Collections.Generic;

public class List{

    public static List<int> DifferentElements(List<int> list1, List<int> list2)
    {
        HashSet<int> uniqueElements = new HashSet<int>();

        foreach (int item in list1)
        {
            if (!list2.Contains(item))
            {
                uniqueElements.Add(item);
            }
        }

        foreach (int item in list2)
        {
            if (!list1.Contains(item))
            {
                uniqueElements.Add(item);
            }
        }

        List<int> resultList = new List<int>(uniqueElements);
        resultList.Sort();
        return resultList;
    }
}