# Beautiful Open-Ended INterior Graphics

## Testing

### Summary

We have conducted a unit test in our Snowman Demo.  This test script test to make sure that any object that we say is pickupable, is actually pickupable.  To do this, the object must meet two requirements:
  1. The object must have a 'RigidBody'.  This is a Unity requirement for movable objects. To [read Unity's 'RigidBody' Documentation](https://docs.unity3d.com/ScriptReference/Rigidbody.html), please click the link.
  2. The object must also have a tag that reads 'Pickupable'.  This is a requirement made by our team in order to make the other scripts play nicely.
  
[Our testing code](https://github.com/SCCapstone/Beautiful-Open-Ended-Interior-Graphics/blob/master/Snowman/Snowman%20Demo/Assets/Scripts/Editor/PickUpTest.cs) can be accessed by following this link.

### Running Tests

To run our test, you can follow these steps:
  1. Clone the latest repo.
  1. Open the 'Snowman Demo' project in Unity.
  1. Click on the 'Window' tab, and then the 'Test Runner' button.  This will open Unity's built in unit tester using the 'NUnit' testing framework.
  1. Under the 'Edit Mode' tab, you should see the 'Snowman Demo' field as well as the 'Assembly-CSharp-Editor.dll' field underneath.  Keep twirling down the triangles on the left until you see 'GameObjectCanBePickedUp'.  Double-Click it to run the test.  This make take a little bit of time, depending on your computer's hardware.  After it is finished, you should see green check marks by the tests that passed.
  
You can also run the test by running the following command in your terminal/command prompt

```cmd
Unity.exe -runTests -projectPath <PATH_TO_YOUR_PROJECT> -testResults <PATH_TO_SAVE>\results.xml -testPlatform editmode
```
