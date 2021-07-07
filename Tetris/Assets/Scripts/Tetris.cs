using System;

public enum TetrisState { Running, NewBlock, Finished, Paused };

[System.Serializable]
public class Tetris
{
    public static int           iScreenDw;
    public Matrix               oScreen;

    protected static int        nBlockTypes;
    protected static int        nBlockDegrees;
    protected static Matrix[,]  setOfBlockObjects;
    protected bool              justStarted;
    protected int               idxBlockType;
    protected int               idxBlockDegree;
    protected int               arrayScreenDx;
    protected int               arrayScreenDy;
    protected int               top;
    protected int               left;
    protected TetrisState       state;

    private int                 iScreenDy;
    private int                 iScreenDx;
    private int[]               arrayScreen;
    private Matrix              currBlk;
    private Matrix              tempBlk;
    private Matrix              iScreen;

    public Tetris(int dy, int dx)
    {
        iScreenDy = dy;
        iScreenDx = dx;
        currBlk = new Matrix();
        tempBlk = new Matrix();
        iScreen = new Matrix(CreateArrayScreen(), arrayScreenDy, arrayScreenDx);
        oScreen = new Matrix(iScreen);
        justStarted = true;
    }

    public static void Init(int[][] setOfBlockArrays, int nTypes, int nDegrees)
    {
        nBlockTypes = nTypes;
        nBlockDegrees = nDegrees;
        setOfBlockObjects = new Matrix[nBlockTypes, nBlockDegrees];

        int maxSize = 0;
        int[] size = new int[nBlockTypes];
        for (int i = 0; i < nBlockTypes; i++)
            size[i] = 0;

        for (int i = 0; i < nBlockTypes; i++)
        {
            for (int j = 0; setOfBlockArrays[i * nBlockDegrees][j] != -1; j++)
                size[i]++;
            size[i] = (int)Math.Sqrt(size[i]);
            if (maxSize <= size[i])
                maxSize = size[i];
        }

        for (int i = 0; i < nBlockTypes; i++)
        {
            for (int j = 0; j < nBlockDegrees; j++)
                setOfBlockObjects[i, j] = new Matrix(setOfBlockArrays[i * nBlockDegrees + j], size[i], size[i]);
        }
        iScreenDw = maxSize;     // large enough to cover the largest block
    }

    public TetrisState Accept(char key)
    {
        state = TetrisState.Running;

        if (key >= '0' && key <= ('0' + nBlockTypes - 1))
        {
            if (justStarted == false)
                DeleteFullLines();
            iScreen = new Matrix(oScreen);
            idxBlockType = (int)key - '0';
            idxBlockDegree = 0;
            currBlk = setOfBlockObjects[idxBlockType, idxBlockDegree];
            top = 0;
            left = iScreenDw + iScreenDx / 2 - currBlk.Get_dx() / 2;
            tempBlk = iScreen.Clip(top, left, top + currBlk.Get_dy(), left + currBlk.Get_dx());
            tempBlk = tempBlk.Add(currBlk);
            justStarted = false;
            // std::cout << std::endl; // 한줄 공백출력

            if (tempBlk.AnyGreaterThan(1))
                state = TetrisState.Finished;
            oScreen = new Matrix(iScreen);
            oScreen.Paste(tempBlk, top, left);
            return state;
        }
        else if (key == 'q') { }
        else if (key == 'a') { left -= 1; }
        else if (key == 'd') { left += 1; }
        else if (key == 's') { top += 1; }
        else if (key == 'w')
        { // rotate the block clockwise
            idxBlockDegree = (idxBlockDegree + 1) % nBlockDegrees;
            currBlk = setOfBlockObjects[idxBlockType, idxBlockDegree];
        }
        else if (key == ' ')
        { // drop the block
            while ( !tempBlk.AnyGreaterThan(1) )
            {
                top += 1;
                tempBlk = iScreen.Clip(top, left, top + currBlk.Get_dy(), left + currBlk.Get_dx());
                tempBlk = tempBlk.Add(currBlk);
            }
        }
        else {  }

        tempBlk = iScreen.Clip(top, left, top + currBlk.Get_dy(), left + currBlk.Get_dx());
        tempBlk = tempBlk.Add(currBlk);

        if (tempBlk.AnyGreaterThan(1))
        {   // 벽 충돌시 undo 수행
            if (key == 'a') { left += 1; } // undo: move right
            else if (key == 'd') { left -= 1; } // undo: move left
            else if (key == 's')
            { // undo: move up
                top -= 1;
                state = TetrisState.NewBlock;
            }
            else if (key == 'w')
            { // undo: rotate the block counter-clockwise
                if (idxBlockDegree > 1)
                    idxBlockDegree = (idxBlockDegree - 1) % nBlockDegrees;
                else
                    idxBlockDegree = (idxBlockDegree + nBlockDegrees - 1) % nBlockDegrees;
                currBlk = setOfBlockObjects[idxBlockType, idxBlockDegree];
            }
            else if (key == ' ')
            { // undo: move up
                top -= 1;
                state = TetrisState.NewBlock;
            }

            tempBlk = iScreen.Clip(top, left, top + currBlk.Get_dy(), left + currBlk.Get_dx());
            tempBlk = tempBlk.Add(currBlk);
        }

        oScreen = new Matrix(iScreen);
        oScreen.Paste(tempBlk, top, left);

        return state;
    }

    protected int[] CreateArrayScreen()
    {
        arrayScreenDy = iScreenDy + iScreenDw;
        arrayScreenDx = iScreenDx + iScreenDw * 2;

        arrayScreen = new int[arrayScreenDy * arrayScreenDx];
        for (int y = 0; y < iScreenDy; y++)
        {
            for (int x = 0; x < iScreenDw; x++)
                arrayScreen[y * arrayScreenDx + x] = 1;
            for (int x = 0; x < iScreenDx; x++)
                arrayScreen[y * arrayScreenDx + iScreenDw + x] = 0;
            for (int x = 0; x < iScreenDw; x++)
                arrayScreen[y * arrayScreenDx + iScreenDw + iScreenDx + x] = 1;
        }

        for (int y = 0; y < iScreenDw; y++)
            for (int x = 0; x < arrayScreenDx; x++)
                arrayScreen[(iScreenDy + y) * arrayScreenDx + x] = 1;

        return arrayScreen;
    }

    protected virtual void DeleteFullLines()
    {

    }
}