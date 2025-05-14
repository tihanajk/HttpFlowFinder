using System;

public class FlowInfo
{
    public string name { get; set; }
    public Guid id { get; set; }
    public string trigger { get; set; }
    public string authType { get; set; }
    public string users { get; set; }
    public string schema { get; set; }
    public int state { get; set; }
    public string state_display { get; set; }
    public string link { get; set; }
}