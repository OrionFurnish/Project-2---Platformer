using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParserStarter : MonoBehaviour
{
    public string filename;

    public GameObject Rock;
    public GameObject Brick;
    public GameObject QuestionBox;
    public GameObject Stone;
    public GameObject player;

    public Transform parentTransform;

    void Start()
    {
        RefreshParse();
    }


    private void FileParser() {
        string fileToParse = string.Format("{0}{1}{2}.txt", Application.dataPath, "/Resources/", filename);

        using (StreamReader sr = new StreamReader(fileToParse)) {
            string line = "";
            int row = 0;

            while ((line = sr.ReadLine()) != null) {
                int column = 0;
                char[] letters = line.ToCharArray();
                row++;
                foreach (var letter in letters) {
                    column++;
                    SpawnPrefab(letter, new Vector3(column, -row, 0f));
                }

            }

            sr.Close();
        }
    }

    private void SpawnPrefab(char spot, Vector3 positionToSpawn) {
        GameObject spawnedObject = null;

        switch (spot)
        {
            case 'b': spawnedObject = Instantiate(Brick, parentTransform); break;
            case '?': spawnedObject = Instantiate(QuestionBox, parentTransform); break;
            case 'x': spawnedObject = Instantiate(Rock, parentTransform); break;
            case 's': spawnedObject = Instantiate(Stone, parentTransform); break;
            case 'p': spawnedObject = Instantiate(player, positionToSpawn, Quaternion.identity); break;
            default: return;
        }
        if (spawnedObject != null) {
            spawnedObject.transform.localPosition = positionToSpawn;
        }
    }

    public void RefreshParse() {
        GameObject newParent = new GameObject();
        newParent.name = "Environment";
        newParent.transform.position = parentTransform.position;
        newParent.transform.parent = this.transform;
        
        if (parentTransform) Destroy(parentTransform.gameObject);

        parentTransform = newParent.transform;
        FileParser();
    }
}
