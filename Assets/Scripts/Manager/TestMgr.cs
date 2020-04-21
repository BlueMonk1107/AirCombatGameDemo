using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMgr : NormalSingleton<TestMgr>
{
    private readonly List<ITest> _editorTests = new List<ITest>
    {
        new MsgEventTest(),
        new BulletConfigTest()
    };

    private readonly List<ITest> _realTests = new List<ITest>();

    public IEnumerator Init()
    {
#if UNITY_EDITOR
        yield return EditorTest();
#endif
        yield return RealTest();

        yield return Clear();
    }

    private IEnumerator EditorTest()
    {
        yield return ExecuteAll(_editorTests);
    }

    private IEnumerator RealTest()
    {
        yield return ExecuteAll(_realTests);
    }

    private IEnumerator ExecuteAll(List<ITest> tests)
    {
        foreach (var test in tests)
        {
            yield return test.Execute();
            Debug.Log("当前" + test + "类测试完成");
        }
    }

    private IEnumerator Clear()
    {
        _editorTests.Clear();
        _realTests.Clear();
        yield return null;
    }
}