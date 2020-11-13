using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedHeroViewGuide : GuideBase 
{
    protected override int GuideId { get { return 0; } }
    public override string GetViewName()
    {
        return Paths.PREFAB_SELECTED_HERO_VIEW;
    }

    protected override Queue<IGuideGroup> GetGroups()
    {
        Queue<IGuideGroup> groups = new Queue<IGuideGroup>();
        groups.Enqueue(new ShowHerosGroup(GuideId));
        return groups;
    }
}
