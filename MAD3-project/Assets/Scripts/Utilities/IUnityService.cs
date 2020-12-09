using UnityEngine;

/*
    Setup for testing, not working yet.
*/
public interface IUnityService
{
    float GetDeltaTime();
    float GetAxis(string axisName);
}

class UnityService: IUnityService
{
    public float GetDeltaTime()
    {
        return Time.deltaTime;
    }
    public float GetAxis(string axisName)
    {
        return Input.GetAxis(axisName);
    }
}