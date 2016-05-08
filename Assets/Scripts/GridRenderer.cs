using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridRenderer : MonoBehaviour {

	public List<TextAsset> LevelFiles;

	private const int kGridWidth = 16;
	private const int kGridHeight = 8;
	private const int kSquarePixelSize = 32;

	class SquareInfo
	{
		public int id;
		public string name;
		public int colour_id;
		public string colour_name;
	}

	Dictionary<char, SquareInfo> m_info;
	Dictionary<int, SquareInfo> m_indexToInfo;
	int m_nextId = 1;

	GameObject[] m_prefabs;

	int[,] m_map;

	public int GetWorldId(int x, int y)
	{
		return m_map[y, x];
	}

	private string GetTileName(int x, int y)
	{
		return string.Format("Sq:{0:D2}-{1:D2}", y, x);
	}

	public void Colour(int x, int y)
	{
		SquareInfo info = m_indexToInfo[m_map[y, x]];
		string name = GetTileName(x, y);
		GameObject tile = GameObject.Find(name);
		GameObject.Destroy(tile);

		CreateSquare(x * kSquarePixelSize, y * kSquarePixelSize, info.colour_id, name);
	}

	void LoadLevel(int _level)
	{
        if(_level < 0 || LevelFiles.Count <= _level)
        {
            Debug.Log("GridRenderer.Loadlevel: _level out of bounds.");
            return;
        }

		var file = LevelFiles[_level].text;
		var lines = file.Split ('\n');
		bool readingMap = true;
		m_info = new Dictionary<char, SquareInfo> ();
		m_indexToInfo = new Dictionary<int, SquareInfo>();
		m_map = new int[kGridHeight, kGridWidth];
		int y = 0;

		foreach (string line in lines) 
		{
			if (readingMap)
			{
				if (line.Length == 0)
				{
					readingMap = false;
					continue;
				}
				else if (line.Length == kGridWidth)
				{
					// Processing a line of the map
					for (int i = 0; i < kGridWidth; ++i)
					{
						if (line[i] == '.')
						{
							// Gap in the grid
							m_map[y, i] = 0;
						}
						else
						{
							int id = 0;

							// Grab the id for the square
							if (m_info.ContainsKey(line[i]))
							{
								id = m_info[line[i]].id;
							}
							else
							{
								SquareInfo info = new SquareInfo();
								info.id = m_nextId++;
								m_info.Add(line[i], info);
								m_indexToInfo.Add(info.id, info);
								id = info.id;
								info.colour_id = 0;
							}
							m_map[y, i] = id;
						}
					}
				}
				else
				{
					// Invalid line
				}
			}
			else
			{
				// We are reading the codes
				string[] codes = line.Trim().Split(' ');

				if ((codes.Length == 2 || codes.Length == 3) && codes[0].Length == 1)
				{
					char ch = codes[0][0];
					if (m_info.ContainsKey(ch))
					{
						var info = m_info[ch];
						info.name = codes[1];
						if (codes.Length == 3)
						{
							info.colour_id = m_nextId++;
							info.colour_name = codes[2];
						}
						m_info[ch] = info;
					}
				}
			} // readingMap?

			++y;
		} // foreach line

		// Create prefabs
		m_prefabs = new GameObject[m_nextId-1];
		foreach (var infoPair in m_info)
		{
			SquareInfo info = infoPair.Value;
			CreatePrefabTileSprite(info.id, info.name);

			if (info.colour_id != 0)
			{
				CreatePrefabTileSprite(info.colour_id, info.colour_name);
			}
		}
	}

    void UnloadLevel()
    {

    }

	void CreatePrefabTileSprite(int id, string name)
	{
		GameObject gob = new GameObject(string.Format("Prefab-Square{0}", id));
		gob.SetActive(false);
		m_prefabs[id - 1] = gob;

		SpriteRenderer renderer = gob.AddComponent<SpriteRenderer>();
		Texture2D tex = Resources.Load<Texture2D>(name);
		var newSprite = Sprite.Create(tex, new Rect(0, 0, kSquarePixelSize, kSquarePixelSize), new Vector2(0.0f, 1.0f), (float)kSquarePixelSize);
		renderer.sprite = newSprite;
	}

	void CreateSquare(int x, int y, int id, string name)
	{
		float xx = -8.0f + ((float)x / (float)kSquarePixelSize);
		float yy = 4.0f - ((float)y / (float)kSquarePixelSize);
		var pos = new Vector3(xx, yy, 0);

		GameObject gob = GameObject.Instantiate(m_prefabs[id - 1]);
		gob.name = name;
		gob.SetActive(true);
		gob.transform.position = pos;
	}

    public void HandleLoadLevel(int _level)
    {
        int w = Screen.width;
        int h = Screen.height;

        LoadLevel(_level);

        for (int row = 0; row < kGridHeight; ++row)
        {
            for (int col = 0; col < kGridWidth; ++col)
            {
                int id = m_map[row, col];

                if (id != 0)
                {
                    CreateSquare(kSquarePixelSize * col, kSquarePixelSize * row, id, GetTileName(col, row));
                }
            }
        }

        Colour(8, 4);
    }

    public void HandleUnloadLevel()
    {
        UnloadLevel();
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
