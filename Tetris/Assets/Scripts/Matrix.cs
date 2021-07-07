[System.Serializable]
public class Matrix
{
    private int    dy;
    private int    dx;
    private int[,] array;

    private void Alloc(int cy, int cx)
    {
        if ((cy < 0) || (cx < 0)) return;
        dy = cy;
        dx = cx;
        array = new int[dy,dx];
        for (int y = 0; y < dy; y++)
            for (int x = 0; x < dx; x++)
                array[y,x] = 0;
    }

    public int Get_dy() { return dy; }

    public int Get_dx() { return dx; }

    public int[,] Get_array() { return array; }

    public Matrix() { Alloc(0, 0); }

    public Matrix(int cy, int cx)
    {
        Alloc(cy, cx);
        for (int y = 0; y < dy; y++)
            for (int x = 0; x < dx; x++)
                array[y, x] = 0;
    }

    public Matrix(Matrix obj)
    {
      Alloc(obj.dy, obj.dx);
      for (int y = 0; y<dy; y++)
        for (int x = 0; x<dx; x++)
          array[y, x] = obj.array[y, x];
    }

    public Matrix(int[] arr, int col, int row)
    {
        Alloc(col, row);
        for (int y = 0; y < dy; y++)
            for (int x = 0; x < dx; x++)
                array[y,x] = arr[y * dx + x];
    }

    public Matrix Clip(int top, int left, int bottom, int right)
    {
        int cy = bottom - top;
        int cx = right - left;
        Matrix temp = new Matrix(cy, cx);
        for (int y = 0; y < cy; y++)
        {
            for (int x = 0; x < cx; x++)
            {
                if ((top + y >= 0) && (left + x >= 0) &&
                (top + y < dy) && (left + x < dx))
                    temp.array[y,x] = array[top+y, left+x];
            }
        }
        return temp;
    }

    public void Paste(Matrix obj, int top, int left)
    {
        for (int y = 0; y < obj.dy; y++)
            for (int x = 0; x < obj.dx; x++)
            {
                if ((top + y >= 0) && (left + x >= 0) &&
                (top + y < dy) && (left + x < dx))
                    array[y+top, x+left] = obj.array[y,x];
            }
    }

    public Matrix Add(Matrix obj)
    {
        if ((dx != obj.dx) || (dy != obj.dy)) return null;
        Matrix temp = new Matrix(dy, dx);
        for (int y = 0; y < dy; y++)
            for (int x = 0; x < dx; x++)
                temp.array[y,x] = array[y,x] + obj.array[y,x];
        return temp;
    }

    public int Sum()
    {
        int total = 0;
        for (int y = 0; y < dy; y++)
            for (int x = 0; x < dx; x++)
                total += array[y, x];
        return total;
    }

    public void Mulc(int coef)
    {
        for (int y = 0; y < dy; y++)
            for (int x = 0; x < dx; x++)
                array[y, x] = coef * array[y, x];
    }

    public Matrix Binary()
    {
        Matrix temp = new Matrix(dy, dx);
        int[,] t_array = temp.Get_array();
        for (int y = 0; y < dy; y++)
            for (int x = 0; x < dx; x++)
                t_array[y, x] = (array[y, x] != 0 ? 1 : 0);

        return temp;
    }

    public bool AnyGreaterThan(int val)
    {
        for (int y = 0; y < dy; y++)
        {
            for (int x = 0; x < dx; x++)
            {
                if (array[y, x] > val)
                    return true;
            }
        }
        return false;
    }
}
