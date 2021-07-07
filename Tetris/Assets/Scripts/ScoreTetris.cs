[System.Serializable]
public class ScoreTetris : CTetris
{
    private int[] linesToClear;
    private int   lineCount;
    public int    Score { private set; get; }

    public ScoreTetris(int dy, int dx) : base(dy, dx)
    {
        linesToClear = new int[dy];
        lineCount = 0;
    }

    public new TetrisState Accept(char key)
    {
        if (key >= '0' && key <= ('0' + nBlockTypes - 1))
        {
            if (justStarted == false)
                DeleteFullLines(0);
        }
        return base.Accept(key);
    }

    protected void DeleteFullLines(int recCnt)
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
                // Clear할 line 목록에 추가
                AddLine(y-recCnt);

                // 점수 증가
                for ( int x=iScreenDw; x<oScreen.Get_dx()-iScreenDw; ++x )
                {
                    Score += cArray[y, x]*100;
                }

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

                DeleteFullLines(recCnt+1);
                return;
            }
        }

        return;
    }

    private void AddLine(int y)
    {
        linesToClear[lineCount++] = y;
    }

    public int ClearLine()
    {
        // Clear할 line이 없으면 -1 리턴
        if ( lineCount == 0 ) return -1;

        int ret = linesToClear[0];

        for ( int i=0; i<lineCount-1; ++i )
        {
            linesToClear[i] = linesToClear[i+1];
        }
        lineCount--;

        return ret;
    }
}
