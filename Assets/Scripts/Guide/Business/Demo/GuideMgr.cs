using System.Collections.Generic;

namespace TestDemo
{
    public class GuideMgr : GuideMgrBase<GuideMgr>, IUpdate
    {
        public override void InitGuide()
        {
            base.InitGuide();
            UIManager.Single.AddShowListener(ShowUI);
            UIManager.Single.AddHideListener(HideUI);
            LifeCycleMgr.Single.Add(LifeName.UPDATE, this);
        }

        protected override Dictionary<string, IGuideRoot> GetViweGuides()
        {
            Dictionary<string, IGuideRoot> dic = new Dictionary<string, IGuideRoot>();
            IGuideRoot guide = new StartViewGuide();
            dic.Add(guide.GetViewName(), guide);

            return dic;
        }

        public int Times { get; set; }
        public int UpdateTimes { get; }

        public void UpdateFun()
        {
            Update();
        }
    }
}