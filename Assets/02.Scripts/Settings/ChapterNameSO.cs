using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class ChapterInfo
{
    public string chapterName;
    public Tilemap tilemap;
    public AudioClip bgm;
}


[CreateAssetMenu(fileName = "Chapter", menuName = "ChapterNameList")]
public class ChapterNameSO : ScriptableObject
{
    public string[] chapterAdjective;
    public ChapterInfo[] chapterInfo;

}
