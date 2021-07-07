using System;

[System.Serializable]
public class CTetris : Tetris
{
    public Matrix oCScreen;

    private static Matrix[][] setOfCBlockObjects;
    private Matrix currCBlk;
    private Matrix iCScreen;


    public CTetris(int dy, int dx) : base(dy, dx)
    {
        currCBlk = new Matrix();
        iCScreen = new Matrix(CreateArrayScreen(), arrayScreenDy, arrayScreenDx);
        oCScreen = new Matrix(iCScreen);
    }

    public static new void Init(int[][] setOfBlockArrays, int nTypes, int nDegrees)
    {
        Tetris.Init(setOfBlockArrays, nTypes, nDegrees);
        setOfCBlockObjects = new Matrix[nBlockTypes][];
        for (int y = 0; y < nBlockTypes; y++)
            setOfCBlockObjects[y] = new Matrix[nBlockDegrees];

        int[] size = new int[nBlockTypes];
        for (int i = 0; i < nBlockTypes; i++)
            size[i] = 0;

        for (int i = 0; i < nBlockTypes; i++)
        {
            for (int j = 0; setOfBlockArrays[i*nBlockDegrees][j] != -1; j++)
                size[i]++;
            size[i] = (int)Math.Sqrt(size[i]);
        }

        for (int i = 0; i < nBlockTypes; i++)
        {
            for (int j = 0; j < nBlockDegrees; j++)
            {
                Matrix obj = new Matrix(setOfBlockArrays[i * nBlockDegrees + j], size[i], size[i]);
                obj.Mulc(i + 1);
                setOfCBlockObjects[i][j] = obj;
            }
        }
    }

    public new TetrisState Accept(char key)
    {
        if (key >= '0' && key <= ('0' + nBlockTypes - 1))
        {
            if (justStarted == false)
                DeleteFullLines();
            iCScreen = new Matrix(oCScreen);
        }

        state = base.Accept(key);

        currCBlk = setOfCBlockObjects[idxBlockType][idxBlockDegree];
        Matrix tempCBlk = iCScreen.Clip(top, left, top + currCBlk.Get_dy(), left + currCBlk.Get_dx());
        tempCBlk = tempCBlk.Add(currCBlk);

        oCScreen = new Matrix(iCScreen);
        oCScreen.Paste(tempCBlk, top, left);
        return state;
    }

    protected override void DeleteFullLines()
    {
        int[,] array = oScreen.Get_array();
        int[,] cArray = oCScreen.Get_array();

        for (int y = oScreen.Get_dy() - iScreenDw - 1; y >= 0; y--)
        {
            bool isFull = true;

            for (int x = iScreenDw; x < oScreen.Get_dx() - iScreenDw; x++)
            {
                if (array[y, x] == 0)
                {
                    isFull = false;
                    break;
                }
            }
            if (isFull)
            {
                for (int line = y; line > 0; line--)
                {
                    for (int x = iScreenDw; x < oScreen.Get_dx() - iScreenDw; x++)
                    {
                        array[line, x] = array[line - 1, x];
                        cArray[line, x] = cArray[line - 1, x];
                    }
                }
                for (int x = iScreenDw; x < oScreen.Get_dx() - iScreenDw; x++)
                {
                    array[0, x] = 0;
                    cArray[0, x] = 0;
                }

                int[] array2 = new int[oScreen.Get_dy() * oScreen.Get_dx() + 1];
                int[] cArray2 = new int[oScreen.Get_dy() * oScreen.Get_dx() + 1];
                for (int i = 0; i < oScreen.Get_dy(); i++)
                {
                    for (int j = 0; j < oScreen.Get_dx(); j++)
                    {
                        array2[i * oScreen.Get_dx() + j] = array[i, j];
                        cArray2[i * oScreen.Get_dx() + j] = cArray[i, j];
                    }
                }
                oScreen = new Matrix(array2, oScreen.Get_dy(), oScreen.Get_dx());
                oCScreen = new Matrix(cArray2, oScreen.Get_dy(), oScreen.Get_dx());

                DeleteFullLines();
                return;
            }
        }

        return;
    }
}
