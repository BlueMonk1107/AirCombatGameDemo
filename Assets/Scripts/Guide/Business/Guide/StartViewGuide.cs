using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartViewGuide : GuideBase
{
    protected override int GuideId { get { return 1; } }
    public override string GetViewName()
    {
        return Paths.PREFAB_START_VIEW;
    }

    protected override Queue<IGuideGroup> GetGroups()
    {
       Queue<IGuideGroup> groups = new Queue<IGuideGroup>();
       groups.Enqueue(new StartViewGroup(GuideId));
       return groups;
    }
}
