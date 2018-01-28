using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class EditModeTest
{

    // test all objects are on start table
    [Test]
    public void Objects_Spawn_On_Table()
    {

        EditorSceneManager.OpenScene("Snowman"); 

        // spawn individual objects
        /*GameObject table = new GameObject("Snowman/table"); // these might need to be "Snowman/table", etc.
        GameObject leftEye = new GameObject("Snowman/leftEye");
        GameObject rightEye = new GameObject("Snowman/rightEye");
        GameObject cone = new GameObject("Snowman/cone");*/

        public GameObject snowmanPrefab;
        private GameObject snowmanObj = Instantiate(snowmanPrefab);

        // Get Size and Position of table's collider
        if (table.GetComponent<Collider>() == null)
        {
            Debug.Log("Table Collider is null");
            Console.Write("Null");
        }
        Vector3 size = table.GetComponent<Collider>().bounds.size;
        Vector3 center = table.GetComponent<Collider>().bounds.center;

        // Get Array of Colliders that are touch or intersect with the table's collider
        Collider[] thingsOnTable = Physics.OverlapBox(center, size);

        // Hardcode the names of things that should be on the table
        Collider[] shouldBeOnTable = { leftEye.GetComponent<Collider>(), rightEye.GetComponent<Collider>(), cone.GetComponent<Collider>() };

        // Sort for Iterative Purposes
        Array.Sort(thingsOnTable);
        Array.Sort(shouldBeOnTable);

        // Iterative through arrays, asserting that things that should be on the table are on the table
        for (uint i = 0; i < shouldBeOnTable.Length; ++i)
        {
            Assert.AreEqual(thingsOnTable[i], shouldBeOnTable[i]);
        }
    }

    // test objects that can be picked up have necessary conditions
    [Test]
    public void GameObject_Can_Be_Picked_Up()
    {

        EditorSceneManager.OpenScene("Snowman");

        // Spawn an object that we know is pickupable
        var thing = new GameObject("top");

        // Test the pickupable attributes
        Assert.AreEqual(thing.tag, "Pickupabale");
        Assert.IsNotNull(thing.GetComponent<Rigidbody>());

    }

}

