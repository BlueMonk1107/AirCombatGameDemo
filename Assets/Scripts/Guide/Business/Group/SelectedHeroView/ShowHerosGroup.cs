using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHerosGroup : GuideBehaviourGroupBase {
    public ShowHerosGroup(int parentId) : base(parentId)
    {
    }

    protected override bool IsTrigger
    {
        get { return true; }
    }
    protected override int GroupId { get { return 2; } }
    protected override Queue<IGuideBehaviour> GetGuideItems()
    {
        Queue<IGuideBehaviour> behaviours = new Queue<IGuideBehaviour>();
        behaviours.Enqueue(new ShowHerosBehaviour());
        return behaviours;
    }
}
