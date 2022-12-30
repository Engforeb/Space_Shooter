using UnityEngine;
namespace Data
{
    public static class DataExtensions
    {
        public static T ToDeserialized<T>(this string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public static string ToJson(this object obj)
        {
            return JsonUtility.ToJson(obj);
        }
    }
}
