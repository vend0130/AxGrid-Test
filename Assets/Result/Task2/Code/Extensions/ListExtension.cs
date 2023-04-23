using System.Collections.Generic;
using UnityEngine;

namespace Result.Task2.Code.Extensions
{
    public static class ListExtension
    {
        public static T GetRandomElement<T>(this List<T> list) =>
            list[Random.Range(0, list.Count)];
    }
}