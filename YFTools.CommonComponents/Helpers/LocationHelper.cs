using System;

namespace YFTools.CommonComponents.Helpers;

public static class LocationHelper
{
    private static double Rad(double d)
    {
        return d * Math.PI / 180d;
    }

    public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
    {
        const double earthRadius = 6378137d;
        var radLat1 = Rad(lat1);
        var radLng1 = Rad(lng1);
        var radLat2 = Rad(lat2);
        var radLng2 = Rad(lng2);
        var a = radLat1 - radLat2;
        var b = radLng1 - radLng2;
        return 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * earthRadius;
    }
}