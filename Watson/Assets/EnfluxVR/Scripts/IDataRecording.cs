using UnityEngine;
using System.Collections;

public interface IDataRecording {
    void addUpper(float[] upper);
    void addLower(float[] lower);
    void writeData();
    bool shouldRecord();
}
