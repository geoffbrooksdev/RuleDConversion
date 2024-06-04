namespace RuleDConversion.Models;

public class RuleStream
{
    public long StreamId { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
    public DateTime DateStamp { get; set; }
    public string UserUpdated { get; set; }
    public string Guid { get; set; }
    public string Comments { get; set; }
}

public class RuleStreamMember
{
    public long RecordId { get; set; }
    public long StreamId { get; set; }
    public long GroupId { get; set; }
    public DateTime DateStamp { get; set; }
    public string UserUpdated { get; set; }
    public string Guid { get; set; }
    public int Order { get; set; }
}

public class RuleGroup
{
    public long GroupId { get; set; }
    public string GroupName { get; set; }
    public string GroupDescription { get; set; }
    public int ExitOnFirstFail { get; set; }
    public int NextGroupOnFail { get; set; }
    public int NextGroupOnSuccess { get; set; }
    public string UserUpdated { get; set; }
    public DateTime DateStamp { get; set; }
    public string GlobalId { get; set; }
    public int Scope { get; set; }
}

public class GroupMember
{
    public long GroupId { get; set; }
    public long RuleNumber { get; set; }
    public string RuleDescription { get; set; }
    public string RuleType { get; set; }
    public DateTime DateStamp { get; set; }
    public string UserUpdated { get; set; }
    public int Scope { get; set; }
}
public class Rule
{
    public long RuleNumber { get; set; }
    public string RuleName { get; set; }
    public string RuleType { get; set; }
    public DateTime DateApplied { get; set; }
    public string UserUpdated { get; set; }
    public DateTime DateStamp { get; set; }
    public string GlobalId { get; set; }
    public int Scope { get; set; }
    public string Comments { get; set; }
}