using System;
using System.Reflection.Metadata;
using UnityEngine;

public class Fox : Animal
{
    private void Start()
    {
        print("i have" + health);
    }

    private void Update()
    {
        Death();
    }
}