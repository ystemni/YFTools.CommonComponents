using System.Collections.Generic;

namespace YFTools.CommonComponents.Models;

public class ResponseDataListModel<T>
{
    /// <summary>
    /// 数据总数
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    /// 当前数据列表
    /// </summary>
    public List<T>? Data { get; set; }
}