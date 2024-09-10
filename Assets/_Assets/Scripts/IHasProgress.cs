using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress
{
    public event EventHandler<ProgressBarEventArgs> OnprogressBarChange;
    public class ProgressBarEventArgs : EventArgs {
        public float progress;
        public Color color;
    }
}
