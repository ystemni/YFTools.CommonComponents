namespace YFTools.CommonComponents.Models;

public class ResponseModel<T>
{
    /// <summary>
    /// 请求是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 返回代码
    /// </summary>
    public int? Code { get; set; }

    /// <summary>
    /// 返回信息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 返回结果
    /// </summary>
    public T? Data { get; set; }
}