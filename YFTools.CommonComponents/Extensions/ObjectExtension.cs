namespace YFTools.CommonComponents.Extensions;

public static class ObjectExtension
{
    public static bool IsNullOrEmpty(this object? obj)
    {
        if (obj == null) return true;
        return obj is not string || string.IsNullOrWhiteSpace(obj.ToString());
    }
}