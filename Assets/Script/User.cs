using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class User : MonoBehaviour
{
    public List<string> userCard = new List<string>();
    public abstract string Name { get; set; }

    public abstract void Submit(string cardcode);
    public abstract void Pass();

    public abstract void SetName();

}
