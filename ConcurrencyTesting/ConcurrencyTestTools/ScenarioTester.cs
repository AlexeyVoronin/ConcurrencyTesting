using System;

namespace ConcurrencyTestTools
{
  public sealed class ScenarioTester : IScenarioTester
  {
    public ScenarioTester(
      Action scenario1, 
      IResourceLockNotifier resourceLockNotifier,
      IThreadUnsafeResource threadUnsafeResource)
    {
      _scenario1 = scenario1;
      _threadUnsafeResource = threadUnsafeResource;
      _resourceLockNotifier = resourceLockNotifier;
    }

    public void Test()
    {
      _resourceLockNotifier.ResourceLocked += _resourceLockNotifier_ResourceLocked;
      _resourceLockNotifier.ResourceUnlocked += _resourceLockNotifier_ResourceUnlocked;

      _threadUnsafeResource.InvocationIntercepted +=
        _threadUnsafeResource_InvocationIntercepted;

      _scenario1();

      if (_isUnsafeInvocationDetected)
        throw new UnsafeThreadAccessException();
    }

    void _resourceLockNotifier_ResourceLocked(object sender, ResourceLockEventArgs e)
    {
    }

    void _resourceLockNotifier_ResourceUnlocked(object sender, ResourceLockEventArgs e)
    {
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
    private readonly IResourceLockNotifier _resourceLockNotifier;
    private bool _isUnsafeInvocationDetected;
  }
}
