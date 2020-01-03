using System.Collections.Generic;
using UnityEngine;

public class TestMgr : NormalSingleton<TestMgr>, IInit
{
    private readonly List<ITest> _editorTests = new List<ITest>
    {
        new MsgEventTest()
    };

    private readonly List<ITest> _realTests = new List<ITest>();

    public void Init()
    {
#if UNITY_EDITOR
        EditorTest();
#endif
        RealTest();

        Clear();
    }

    private void EditorTest()
    {
        ExecuteAll(_editorTests);
    }

    private void RealTest()
    {
        ExecuteAll(_realTests);
    }

    private void ExecuteAll(List<ITest> tests)
    {
        foreach (var test in tests)
        {
            test.Execute();
            Debug.Log("当前" + test + "类测试完成");
        }
    }

    private void Clear()
    {
        _editorTests.Clear();
        _realTests.Clear();
    }
}