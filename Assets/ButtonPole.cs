using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPole : Obstacle {
    public Acid acid;

    public override void SetOverride()
    {
        overriden = true;
        acid.Override();
        // Animation?
    }

}
