using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestDemo
{
    public class StartViewGuide : GuideBase
    {
        protected override int GuideId { get; }

        public override string GetViewName()
        {
            return Paths.PREFAB_START_VIEW;
        }

        protected override Queue<IGuideGroup> GetGroups()
        {
            Queue<IGuideGroup> groups = new Queue<IGuideGroup>();
            groups.Enqueue(new Demo1GuideGroup(GuideId));
            groups.Enqueue(new DemoGuideGroupA(GuideId));
            return groups;
        }
    }

    public class DemoGuideGroupA : GuideGroupGroupBase
    {
        //这里的逻辑是连续执行两个组的行为
        protected override Queue<IGuideGroup> GetGuideItems()
        {
            Queue<IGuideGroup> groups = new Queue<IGuideGroup>();
            groups.Enqueue(new Demo1GuideGroup());
            groups.Enqueue(new Demo2GuideGroup());
            return groups;
        }

        protected override bool IsTrigger { get; }

        protected override int GroupId { get; }

        public DemoGuideGroupA(int parentId) : base(parentId)
        {
        }
    }

    /// <summary>
    /// Demo1引导行为组
    /// </summary>
    public class Demo1GuideGroup : GuideBehaviourGroupBase
    {
        protected override Queue<IGuideBehaviour> GetGuideItems()
        {
            Queue<IGuideBehaviour> behaviours = new Queue<IGuideBehaviour>();
            behaviours.Enqueue(new ClickButtonA());
            return behaviours;
        }

        protected override bool IsTrigger
        {
            //比如这里的条件是 金币大于20
            get { return true; }
        }
        
        protected override int GroupId { get; }

        public Demo1GuideGroup(int parentId = -1) : base(parentId)
        {
        }
    }

    /// <summary>
    /// Demo2引导行为组
    /// </summary>
    public class Demo2GuideGroup : GuideBehaviourGroupBase
    {
        protected override Queue<IGuideBehaviour> GetGuideItems()
        {
            Queue<IGuideBehaviour> behaviours = new Queue<IGuideBehaviour>();
            behaviours.Enqueue(new ClickButtonA());
            return behaviours;
        }

        protected override bool IsTrigger
        {
            //第一次进入此界面
            get { return true; }
        }

        protected override int GroupId { get; }

        public Demo2GuideGroup(int parentId = -1) : base(parentId)
        {
        }
    }


    public class ClickButtonA : GuideBehaviourBase
    {
        protected override void OnEnterLogic()
        {
            //初始化按钮，绑定按钮事件
            //点击之后，执行退出逻辑 OnExitLogic()；
        }

        protected override void OnExitLogic()
        {
            //显示下一个界面
        }
    }
}