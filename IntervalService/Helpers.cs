using Interfaces.Models;
namespace IntervalService;

public static class HelperFunctions
{
    static bool shouldSwap<T>(List<T> collection, int start, int curr)
    {
        for (int i = start; i < curr; i++)
        {
            if (collection[i].Equals(collection[curr]))
            {
                return false;
            }
        }
        return true;
    }
    
    
    public static List<List<T>> findPermutations<T>(List<T> collection,
                                int index, int n)
    {
        List<List<T>> result = new List<List<T>>();
        if (index >= n){
            result.Add(new List<T>(collection));
        }
        else
            for (int i = index; i < n; i++)
            {
                bool check = shouldSwap(collection, index, i);
                if (check)
                {
                    swap(collection, index, i);
                    result.AddRange(findPermutations(collection, index + 1, n));
                    swap(collection, index, i);
                }
            }
        return result;
    }
    
    static void swap<T>(List<T> str, int i, int j)
    {
        T c = str[i];
        str[i] = str[j];
        str[j] = c;
    }
}