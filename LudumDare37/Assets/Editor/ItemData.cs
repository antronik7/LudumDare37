﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[System.Serializable] tells unity to serialize this class if 
//it's used in a public array or as a public variable in a component
[System.Serializable]
public class Item
{
    public enum TestEnum { LevelFixed, LevelDesign };
    public string Name;
    public TestEnum CategorieName;
    public GameObject Prefab;
    public bool outOfRoom;
}

//[CreateAssetMenu] creates an entry in the default Create menu of the ProjectView so you can easily create an instance of this ScriptableObject
[CreateAssetMenu]
public class ItemData : ScriptableObject 
{
    //This ScriptableObject simply stores a list of blocks. It kind of acts like a database in that it stores rows of data
    public List<Item> Blocks = new List<Item>();
}
