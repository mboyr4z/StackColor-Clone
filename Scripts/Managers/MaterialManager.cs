using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoSingleton<MaterialManager>
{
    [SerializeField] private Material blue;

    [SerializeField] private Material red;

    [SerializeField] private Material pink;

    [SerializeField] private Material green;

    public Material Blue { get => blue;  }
    public Material Red { get => red;  }
    public Material Pink { get => pink; }
    public Material Green { get => green;  }
}
