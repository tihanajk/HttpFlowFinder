
using System.Web;

public class FlowClientData
{
    public Properties properties { get; set; }
    public string schemaVersion { get; set; }
}

public class Properties
{
    public Connectionreferences connectionReferences { get; set; }
    public Definition definition { get; set; }
    public string templateName { get; set; }
}

public class Connectionreferences
{
}

public class Definition
{
    public string schema { get; set; }
    public string contentVersion { get; set; }
    public Parameters parameters { get; set; }
    public Triggers triggers { get; set; }
}

public class Parameters
{
    public Authentication authentication { get; set; }
}

public class Authentication
{
    public Defaultvalue defaultValue { get; set; }
    public string type { get; set; }
}

public class Defaultvalue
{
}

public class Triggers
{
    public Manual manual { get; set; }
}

public class Manual
{
    public Metadata metadata { get; set; }
    public string type { get; set; }
    public string kind { get; set; }
    public Inputs inputs { get; set; }
}

public class Metadata
{
    public string operationMetadataId { get; set; }
}

public class Inputs
{
    public string triggerAuthenticationType { get; set; }
    public string triggerAllowedUsers { get; set; }
    public object schema { get; set; }
}

public class Schema
{
    public string type { get; set; }
    public Properties1 properties { get; set; }
}

public class Properties1
{
    public Name name { get; set; }
}

public class Name
{
    public string type { get; set; }
}