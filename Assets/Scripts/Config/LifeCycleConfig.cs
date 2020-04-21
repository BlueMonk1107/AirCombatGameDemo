using System;
using System.Collections.Generic;

public class LifeCycleConfig
{
    public static Dictionary<LifeName, ILifeCycle> LifeCycles = new Dictionary<LifeName, ILifeCycle>
    {
        {LifeName.INIT, new LifeCycle<IInit>()},
        {LifeName.UPDATE, new LifeCycle<IUpdate>()}
    };

    public static Dictionary<LifeName, Action> LifeCycleFuns = new Dictionary<LifeName, Action>
    {
        {LifeName.INIT, () => LifeCycles[LifeName.INIT].Execute((IInit o) => o.Init())},
        {
            LifeName.UPDATE, () =>
            {
                LifeCycles[LifeName.UPDATE].Execute((IUpdate o) =>
                {
                    if (o.Times < o.UpdateTimes)
                    {
                        o.Times++;
                        return;
                    }

                    o.Times = 0;
                    o.UpdateFun();
                });
            }
        }
    };
}

public enum LifeName
{
    INIT,
    UPDATE
}