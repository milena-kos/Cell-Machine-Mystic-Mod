using System.Text;
using UnityEngine;

public class Save : MonoBehaviour
{
    public GameObject saveText;
    private const string cellKey = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!$%&+-.=?^{}";

    public void Awake()
    {
        saveText.gameObject.SetActive(false);
    }

    private string EncodeInt(int num)
    {
        if (num < 74)
            return cellKey[num % 74].ToString();

        string output = "";
        while (num > 0)
        {
            output = cellKey[num % 74] + output;
            num /= 74;
        }
        return output;
    }

    public void SaveString()
    {
        string output = "";

        int[] cellData;
        int dataIndex = 0;

        switch (PlayerPrefs.GetInt("ExportFormat"))
        {
            case 0:
                output = "V1;" + CellFunctions.gridWidth + ";" + CellFunctions.gridHeight + ";";

                bool debounce = false;
                for (int y = 0; y < CellFunctions.gridHeight; y++)
                {
                    for (int x = 0; x < CellFunctions.gridWidth; x++)
                    {
                        if (GridManager.instance.tilemap.GetTile(new Vector3Int(x, y, 0)) == GridManager.instance.placebleTile)
                        {
                            if (debounce)
                                output += ",";
                            debounce = true;
                            output += x + "." + y;
                        }
                    }
                }
                output += ";";

                debounce = false;
                foreach (Cell cell in CellFunctions.cellList)
                {
                    if (debounce)
                        output += ",";
                    debounce = true;
                    output += (int)cell.cellType + "." + (int)cell.getDirection() + "." + (int)cell.spawnPosition.x + "." + (int)cell.spawnPosition.y;
                }
                output += ";;";

                break;

            case 1:
                output = "V2;" + EncodeInt(CellFunctions.gridWidth) + ";" + EncodeInt(CellFunctions.gridHeight) + ";";

                cellData = new int[CellFunctions.gridWidth * CellFunctions.gridHeight];

                for (int y = 0; y < CellFunctions.gridHeight; y++)
                {
                    for (int x = 0; x < CellFunctions.gridWidth; x++)
                    {
                        cellData[x + (y * CellFunctions.gridWidth)] = GridManager.instance.tilemap.GetTile(new Vector3Int(x, y, 0)) == GridManager.instance.placebleTile ? 73 : 72;
                    }
                }
                foreach (Cell cell in CellFunctions.cellList)
                {
                    cellData[(int)cell.spawnPosition.x + ((int)cell.spawnPosition.y * CellFunctions.gridWidth)] += (2 * (int)cell.cellType) + (18 * cell.rotation) - 72;
                }

                int runLength = 1;

                while (dataIndex < cellData.Length)
                {

                    if (dataIndex + 1 < cellData.Length && cellData[dataIndex] == cellData[dataIndex + 1])
                        runLength++;
                    else
                    {
                        if (runLength > 3)
                        {
                            if (EncodeInt(runLength - 1).Length > 1)
                                output += cellKey[cellData[dataIndex]] + "(" + EncodeInt(runLength - 1) + ")";
                            else
                                output += cellKey[cellData[dataIndex]] + ")" + EncodeInt(runLength - 1);
                        }
                        else
                            output += new string(cellKey[cellData[dataIndex]], runLength);
                        runLength = 1;
                    }
                    dataIndex++;
                }

                cellData = null;
                output += ";;";
                break;

            case 2:
                output = "V3;" + EncodeInt(CellFunctions.gridWidth) + ";" + EncodeInt(CellFunctions.gridHeight) + ";";

                cellData = new int[CellFunctions.gridWidth * CellFunctions.gridHeight];

                for (int y = 0; y < CellFunctions.gridHeight; y++)
                {
                    for (int x = 0; x < CellFunctions.gridWidth; x++)
                    {
                        cellData[x + (y * CellFunctions.gridWidth)] = GridManager.instance.tilemap.GetTile(new Vector3Int(x, y, 0)) == GridManager.instance.placebleTile ? 73 : 72;
                    }
                }
                foreach (Cell cell in CellFunctions.cellList)
                {
                    cellData[(int)cell.spawnPosition.x + ((int)cell.spawnPosition.y * CellFunctions.gridWidth)] += (2 * (int)cell.cellType) + (18 * cell.rotation) - 72;
                }

                int matchLength;
                int maxMatchLength;
                int maxMatchOffset = 0;

                while (dataIndex < cellData.Length)
                {
                    maxMatchLength = 0;
                    for (int matchOffset = 1; matchOffset <= dataIndex; matchOffset++)
                    {
                        matchLength = 0;
                        while (dataIndex + matchLength < cellData.Length && cellData[dataIndex + matchLength] == cellData[dataIndex + matchLength - matchOffset])
                        {
                            matchLength++;
                            if (matchLength > maxMatchLength)
                            {
                                maxMatchLength = matchLength;
                                maxMatchOffset = matchOffset - 1;
                            }
                        }
                    }
                    if (maxMatchLength > 3)
                    {
                        if (EncodeInt(maxMatchLength).Length == 1)
                        {
                            if (EncodeInt(maxMatchOffset).Length == 1)
                            {
                                if (maxMatchLength > 3)
                                {
                                    output += ")" + EncodeInt(maxMatchOffset) + EncodeInt(maxMatchLength);
                                    dataIndex += maxMatchLength - 1;
                                }
                                else
                                    output += cellKey[cellData[dataIndex]];
                            }
                            else
                            {
                                if (maxMatchLength > 3 + EncodeInt(maxMatchOffset).Length)
                                {
                                    output += "(" + EncodeInt(maxMatchOffset) + ")" + EncodeInt(maxMatchLength);
                                    dataIndex += maxMatchLength - 1;
                                }
                                else
                                    output += cellKey[cellData[dataIndex]];
                            }
                        }
                        else
                        {
                            output += "(" + EncodeInt(maxMatchOffset) + "(" + EncodeInt(maxMatchLength) + ")";
                            dataIndex += maxMatchLength - 1;
                        }
                    }
                    else
                        output += cellKey[cellData[dataIndex]];

                    maxMatchLength = 0;
                    dataIndex += 1;
                }

                cellData = null;
                output += ";;";
                break;
            case 3:
                output = "V4;" + EncodeInt(CellFunctions.gridWidth) + ";" + EncodeInt(CellFunctions.gridHeight) + ";";
                StringBuilder rawOut = new StringBuilder();

                for (int y = 0; y < CellFunctions.gridHeight; y++)
                {
                    for (int x = 0; x < CellFunctions.gridWidth; x++)
                    {
                        bool placable = GridManager.instance.tilemap.GetTile(new Vector3Int(x, y, 0)) == GridManager.instance.placebleTile;
                        Cell currentCell = CellFunctions.cellGrid[x, y];
                        if (currentCell == null)
                        {
                            rawOut.Append(EncodeInt(placable ? 73 : 72)); continue;
                        }
                        rawOut.Append(EncodeInt((2 * (int)currentCell.cellType) + (18 * currentCell.rotation) + (placable ? 1 : 0)));
                    }
                }


                output += Compression.BrotliString(rawOut.ToString()) + ";;";
                rawOut = null;
                break;
        }

        GridManager.hasSaved = true;
        GUIUtility.systemCopyBuffer = output;
        GameObject go = Instantiate(saveText, this.GetComponentInParent<Transform>(), true);
        go.SetActive(true);
        Destroy(go, 3);
    }
}
