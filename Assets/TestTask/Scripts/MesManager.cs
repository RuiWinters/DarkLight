using UnityEngine;
using System.Collections;
using System;

public class MesManager : MonoSingletion<MesManager> {

    public event Action<TaskEventArgs> checkEvent;

    public void Check(TaskEventArgs e)
    {
        checkEvent(e);
    }
}
