using UnityEngine;     // Unity default
using NUnit.Framework; // Testing Framework

/******************************************************************************
 * EditModeTest.cs
 * Written by: Stephen Reaves
 * Date Last Modified: January 31, 2018
 * Summary: This code will run unit test on our objects that are able to be 
 *          picked up.  This includes testing the name of the 'Tag', as well as
 *          making sure the object has a 'RigidBody'.
 **/

public class PickUpTest
{

    // test objects that can be picked up have necessary conditions
    [Test]
    public void GameObjectCanBePickedUp()
    {

        // Spawn an object that we know is pickupable
        GameObject top = GameObject.Find("Snowman/top");
        GameObject middle = GameObject.Find("Snowman/middle");
        GameObject bottom = GameObject.Find("Snowman/bottom");
        GameObject cone = GameObject.Find("Snowman/cone");

        // Test the pickupable tag
        Assert.AreEqual("Pickupable", top.tag);
        Assert.AreEqual("Pickupable", middle.tag);
        Assert.AreEqual("Pickupable", bottom.tag);
        Assert.AreEqual("Pickupable", cone.tag);

        // Test the existance of a rigidbody
        Assert.IsNotNull(top.GetComponent<Rigidbody>());
        Assert.IsNotNull(middle.GetComponent<Rigidbody>());
        Assert.IsNotNull(bottom.GetComponent<Rigidbody>());
        Assert.IsNotNull(cone.GetComponent<Rigidbody>());

    }

}