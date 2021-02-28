using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParserStarter : MonoBehaviour {
    public string filename;

    public GameObject rock;
    public GameObject brick;
    public GameObject questionBox;
    public GameObject stone;
    public GameObject spikes;
    public GameObject flag;
    public GameObject fall;
    public GameObject player;

    public Transform parentTransform;
    GameManager gameManager;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
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
            case 'b': spawnedObject = Instantiate(brick, parentTransform); break;
            case '?': spawnedObject = Instantiate(questionBox, parentTransform); break;
            case 'x': spawnedObject = Instantiate(rock, parentTransform); break;
            case 's': spawnedObject = Instantiate(stone, parentTransform); break;
            case 'k': spawnedObject = Instantiate(spikes, parentTransform); break;
            case 'f': spawnedObject = Instantiate(flag, parentTransform); break;
            case 'w': spawnedObject = Instantiate(fall, parentTransform); break;
            case 'p':
                spawnedObject = Instantiate(player, positionToSpawn, Quaternion.identity);
                gameManager.player = spawnedObject; // Used to kill player at game end
                break;
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
