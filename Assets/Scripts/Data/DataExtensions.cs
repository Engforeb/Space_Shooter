using UnityEngine;
namespace Data
{
    public static class DataExtensions
    {
        public static T ToDeserialized<T>(this string json) => 
            JsonUtility.FromJson<T>(json);
    }
}
