using System;

public class Quest
{
    public string questID;
    public string questHeader;
    public string description;
    public Action onStart;
    public Action onComplete;
    public bool isCompleted = false;
}
