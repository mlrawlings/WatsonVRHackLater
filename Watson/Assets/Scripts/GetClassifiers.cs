using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using IBM.Watson.DeveloperCloud.Services.VisualRecognition.v3;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Utilities;

public class GetClassifiers : MonoBehaviour
{

    private VisualRecognition m_VisualRecognition = new VisualRecognition();

    public Action ClassifyResult;

    void Start()
    {
        //Debug.Log("Hello there!");

        //string imagePath = Application.dataPath + "/Watson/Examples/ServiceExamples/TestData/visual-recognition-classifiers/obama.jpg";
        //string[] classifierIDs = { "default" };

        //CheckMatch(imagePath, "person", classifierIDs, (match) => {
        //	if(match) {
        //		Debug.Log("Matched!");
        //	} else {
        //		Debug.Log("Did Not Match...");
        //	}
        //});
    }

    public void CheckMatch(string imagePath, string expectedClass, string[] classifiers, Action<bool> callback)
    {
        string[] owners = { "me" };

        m_VisualRecognition.Classify(imagePath, (classify, data) =>
        {
            if (classify != null)
            {
                foreach (ClassifyTopLevelSingle image in classify.images)
                {
                    if (image.classifiers != null && image.classifiers.Length > 0)
                    {
                        foreach (ClassifyPerClassifier classifier in image.classifiers)
                        {
                            foreach (ClassResult classResult in classifier.classes)
                            {
                                if (classResult.m_class == expectedClass)
                                {
                                    callback(true);
                                    Debug.Log("Classifier: Expected Pose: " + expectedClass + " Success Filename: " + imagePath);
                                    return;
                                }
                                else
                                {
                                    Debug.Log("Classifier: Expected Pose: " + expectedClass + " Failed, Returned " + classResult.m_class + "Filename: " + imagePath);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Classifier: Expected Pose: " + expectedClass + "Failed");
                callback(true);
                return;
            }
            callback(false);
            return;
        }, owners, classifiers, 0.5f);
    }
}