public class FlowCallbackResponse
{
    public Response response { get; set; }
    public string httpStatusCode { get; set; }
}

public class Response
{
    public string value { get; set; }
    public string method { get; set; }
    public string basePath { get; set; }
}