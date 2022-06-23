namespace Portal.Extentions.Common;

public enum ExceptionStatusCodeType
{
    Success = 200,
    BadRequest = 400,
    Forbidden = 403,
    NotFound = 404,
    InternalServer = 500,
    ServiceUnavailable = 503,
    GatewayTimeout = 504,

    AddUser = 0101,
    NotFoundUser = 0102,
    NotFoundRole = 0103,
    DublicateMobile = 0104,
    NotFoundCategory = 0105,
    NotFoundBlog = 0106,
}
