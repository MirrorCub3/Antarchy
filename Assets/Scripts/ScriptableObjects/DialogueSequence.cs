using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueSequence : ScriptableObject //Joyce Mai // allows premade dialogue to be loaded onto a character
{
    public enum Speaker {Player, Narration, Peter, Petra}
    public Speaker speaker; // moreso for ease in editor

    [Tooltip("all the lines said by a character")]
    [TextArea(1, 10)]
    public string[] sentences; // all the lines that will be said

    [Tooltip("sprites the character changes between, correlates with the line index")]
    // the corresponding sprites and locations
    public List<Sprite> spriteList;

    [Tooltip("locations the character changes between, correlates with the sprite index")]
    public List<Position> placements;

    [Tooltip("place for the character to go after talking")]
    public Position postTextPos; // where to move post dialogue
    public Sprite postTextSprite; // where to move post dialogue
}
