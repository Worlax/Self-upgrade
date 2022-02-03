using System;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtension
{
    public static List<T> GetPossibleRange<T>(this List<T> list, int index, int count)
    {
        if (index < 0 || index > list.Count - 1) throw new ArgumentOutOfRangeException();

        List<T> devList = new List<T>();
        int indexAfterLast = Mathf.Clamp(index + count, -1, list.Count);

        // Count forward
        if (count >= 0)
		{
            for (int i = index; i < indexAfterLast; ++i)
            {
                devList.Add(list[i]);
            }
        }
        // Count backward
        else
		{
            for (int i = index; i > indexAfterLast; --i)
            {
                devList.Add(list[i]);
            }
            devList.Reverse();
        }

        return devList;
    }
}