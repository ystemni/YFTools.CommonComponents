using System;
using System.Collections.Generic;
using System.Linq;

namespace YFTools.CommonComponents.Extensions;

public static class DecimalExtension
{
    public static string FinancialAmount(this decimal amount)
    {
        amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        if (amount == 0) return "人民币零元整";

        var numbers = new[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
        var digits = new[] { "", "分", "角", "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿" };
        var beforeReplace = new List<string> { "零拾", "零佰", "零仟", "零万", "零亿", "亿万", "零元", "零零", "零分" };
        var afterReplace = new List<string> { "零", "零", "零", "万", "亿", "亿", "元", "零", "" };
        var beginStr = "人民币";
        if (amount < 0)
        {
            beginStr = "人民币负";
            amount *= -1;
        }
        var endStr = amount % 1 == 0 ? "整" : "";
        var amountStr = "";
        var k = 1;
        var amountByCents = $"{amount}".Replace(".", "");
        for (var i = amountByCents.Length - 1; i >= 0; i--)
        {
            var number = int.Parse(amountByCents[i].ToString());
            amountStr = numbers[number] + digits[k] + amountStr;
            k = k < 11 ? k + 1 : 4;
        }
        while (beforeReplace.Exists(t => amountStr.Contains(t)))
        {
            var index = beforeReplace.IndexOf(beforeReplace.FirstOrDefault(t => amountStr.Contains(t)));
            if (index >= 0)
                amountStr = amountStr.Replace(beforeReplace[index], afterReplace[index]);
        }
        if (amountStr.EndsWith("零角"))
            amountStr = amountStr.Replace("零角", "");
        return string.IsNullOrWhiteSpace(amountStr) ? "人民币零元整" : beginStr + amountStr + endStr;
    }
}