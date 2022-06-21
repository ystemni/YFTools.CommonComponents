namespace YFTools.CommonComponents.Models;

public class ResultCode
{
    public int Code { get; set; }

    public string Message { get; set; }

    public ResultCode(int code, string message)
    {
        Code = code;
        Message = message;
    }

    // 成功
    public static ResultCode Success = new(0, "成功");

    // 系统错误 1~499
    public static ResultCode BadRequest = new(400, "无效请求");
    public static ResultCode UnAuthorized = new(401, "认证信息过期，请重新登录");
    public static ResultCode Forbidden = new(403, "拒绝请求");
    public static ResultCode NotFound = new(404, "未找到服务");
    public static ResultCode SystemError = new(500, "系统错误，请稍候再试");

    // 接口错误 1000~9999
    public static ResultCode ParameterInvalid = new(1001, "无效参数");
    public static ResultCode DataExpired = new(1001, "数据已过期，请刷新页面");
    public static ResultCode BusinessVerificationFailed = new(1002, "业务校验失败");
}