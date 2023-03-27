using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress
{
    public class OnProgressChangedArgs : EventArgs
    {
        public float ProgressNormalized;
    }
    public event EventHandler<OnProgressChangedArgs> OnProgressChanged;
}
