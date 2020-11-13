using System.Collections.Generic;

public class GuideDataConfig
{
    public static readonly List<IGuideRoot> GUIDE_ROOTS = new List<IGuideRoot>
    {
        new StartViewGuide(),
        new SelectedHeroViewGuide()
    };
}