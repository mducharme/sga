using System.Collections;
using UnityEngine;

namespace Game
{
    public interface ISaveable
    {
        object PrepareSaveData();
        void RestoreSaveData(object save);
    }
}