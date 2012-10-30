using System;

namespace ConcurrencyTestTools
{
  public sealed class ScenarioTester : IScenarioTester
  {
    public ScenarioTester(Action scenario1, IThreadUnsafeResource threadUnsafeResource)
    {
      _scenario1 = scenario1;
      _threadUnsafeResource = threadUnsafeResource;
    }

    public void Test()
    {
      _threadUnsafeResource.InvocationIntercepted +=
        _threadUnsafeResource_InvocationIntercepted;

      _scenario1();

      if (_isUnsafeInvocationDetected)
        throw new UnsafeThreadAccessException();
    }

    private void _threadUnsafeResource_InvocationIntercepted(object sender, EventArgs e)
    {
      InterceptResourceInvocation();
    }

    private void InterceptResourceInvocation()
    {
      _isUnsafeInvocationDetected = true;
    }

    private readonly Action _scenario1;
    private readonly IThreadUnsafeResource _threadUnsafeResource;
    private bool _isUnsafeInvocationDetected;
  }
}
