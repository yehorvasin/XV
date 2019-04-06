using System;
using System.Collections.Generic;

[Serializable]
public class XVObjectDataList
{
    public List<XVObjectData> list;

    public XVObjectDataList()
    {
        list = new List<XVObjectData>();
    }

    public void Add(XVObjectData objectData)
    {
        list.Add(objectData);
    }

    public void Remove(XVObjectData objectData)
    {
        list.Remove(objectData);
    }

    public void AddRange(IEnumerable<XVObjectData> range)
    {
        list.AddRange(range);
    }
}
