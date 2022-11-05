using Il2CppInterop.Runtime.InteropTypes;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace SeededRuns.Extensions;

public static class ListExtensions
{
    public static Il2CppReferenceArray<T> ToIl2CppReferenceArray<T>(this System.Collections.Generic.List<T> list) where T: Il2CppObjectBase
    {
        return new Il2CppReferenceArray<T>(list.ToArray());
    }
    
    public static Il2CppSystem.Collections.Generic.List<T> ToIl2CppList<T>(this System.Collections.Generic.List<T> list) where T: Il2CppObjectBase
    {
        var newList = new Il2CppSystem.Collections.Generic.List<T>();
        
        foreach (var obj in list)
        {
            newList.Add(obj);
        }

        return newList;
    }
}