using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class DataRecording : MonoBehaviour {

    private List<float[]> record_upper = new List<float[]>();
    private List<float[]> record_lower = new List<float[]>();
    bool record = false;

    public void setRecordState(bool state)
    {
        record = state;
    }

    public bool shouldRecord()
    {
        return record;
    }

    public void addUpper(float[] up)
    {
        record_upper.Add(up);
    }

    public void addLower(float[] lo)
    {
        record_lower.Add(lo);
    }

    public void writeData()
    {
        //get from list
        //format to print to csv
        using (var writer = new StreamWriter("Assets/PoseRecordings/raw.csv"))
        {            
            for (int i = 0; i < record_upper.Count; i++)
            {                
                float[] _lower = record_lower[i];
                float[] _upper = record_upper[i];

                string result = string.Format(
                "{0:f4},{1:f4},{2:f4},{3:f4},{4:f4}," +
                "{5:f4},{6:f4},{7:f4},{8:f4},{9:f4}," +
                "{10:f4},{11:f4},{12:f4},{13:f4},{14:f4}," +
                "{15:f4},{16:f4},{17:f4},{18:f4},{19:f4}," +
                "{20:f4},{21:f4},{22:f4},{23:f4},{24:f4}," +
                "{25:f4},{26:f4},{27:f4},{28:f4},{29:f4}," +
                "{30:f4},{31:f4},{32:f4},{33:f4},{34:f4}," +
                "{35:f4},{36:f4},{37:f4},{38:f4},{39:f4}",
                _upper[0], _upper[1], _upper[2], _upper[3], _upper[4],
                _upper[5], _upper[6], _upper[7], _upper[8], _upper[9],
                _upper[10], _upper[11], _upper[12], _upper[13], _upper[14],
                _upper[15], _upper[16], _upper[17], _upper[18], _upper[19],
                _lower[0], _lower[1], _lower[2], _lower[3], _lower[4],
                _lower[5], _lower[6], _lower[7], _lower[8], _lower[9],
                _lower[10], _lower[11], _lower[12], _lower[13], _lower[14],
                _lower[15], _lower[16], _lower[17], _lower[18], _lower[19]);

                writer.WriteLine(result);
            }
        }

        Debug.Log("Finished writing data");
    }
}
