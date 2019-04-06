using System;
using System.Collections.Generic;

[Serializable]
public class XVScenesDataList
{
    public List<string> list;

    public XVScenesDataList()
    {
        list = new List<string>();
    }
    
    public void Add(string objectData)
    {
        list.Add(objectData);
    }

    public void Remove(string objectData)
    {
        list.Remove(objectData);
    }

    public void AddRange(IEnumerable<string> range)
    {
        list.AddRange(range);
    }
}
