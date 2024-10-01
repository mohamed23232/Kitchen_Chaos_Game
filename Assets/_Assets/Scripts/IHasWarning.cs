using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface IHasWarning
{
    public event EventHandler<OnWarningEventArgs> OnWarning;

    public class OnWarningEventArgs : EventArgs {
        public StoveCounter.State state;
        public bool Empty;
    }
}
