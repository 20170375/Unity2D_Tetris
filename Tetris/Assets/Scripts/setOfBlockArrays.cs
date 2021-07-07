[System.Serializable]
public class setOfBlockArrays
{
    public static int maxBlkTypes = 7;
    public static int maxBlkDegrees = 4;

    public static int[] T0D0 = { 1, 1, 1, 1, -1 };
    public static int[] T0D1 = { 1, 1, 1, 1, -1 };
    public static int[] T0D2 = { 1, 1, 1, 1, -1 };
    public static int[] T0D3 = { 1, 1, 1, 1, -1 };

    public static int[] T1D0 = { 0, 1, 0, 1, 1, 1, 0, 0, 0, -1 };
    public static int[] T1D1 = { 0, 1, 0, 0, 1, 1, 0, 1, 0, -1 };
    public static int[] T1D2 = { 0, 0, 0, 1, 1, 1, 0, 1, 0, -1 };
    public static int[] T1D3 = { 0, 1, 0, 1, 1, 0, 0, 1, 0, -1 };

    public static int[] T2D0 = { 1, 0, 0, 1, 1, 1, 0, 0, 0, -1 };
    public static int[] T2D1 = { 0, 1, 1, 0, 1, 0, 0, 1, 0, -1 };
    public static int[] T2D2 = { 0, 0, 0, 1, 1, 1, 0, 0, 1, -1 };
    public static int[] T2D3 = { 0, 1, 0, 0, 1, 0, 1, 1, 0, -1 };

    public static int[] T3D0 = { 0, 0, 1, 1, 1, 1, 0, 0, 0, -1 };
    public static int[] T3D1 = { 0, 1, 0, 0, 1, 0, 0, 1, 1, -1 };
    public static int[] T3D2 = { 0, 0, 0, 1, 1, 1, 1, 0, 0, -1 };
    public static int[] T3D3 = { 1, 1, 0, 0, 1, 0, 0, 1, 0, -1 };

    public static int[] T4D0 = { 0, 1, 0, 1, 1, 0, 1, 0, 0, -1 };
    public static int[] T4D1 = { 1, 1, 0, 0, 1, 1, 0, 0, 0, -1 };
    public static int[] T4D2 = { 0, 1, 0, 1, 1, 0, 1, 0, 0, -1 };
    public static int[] T4D3 = { 1, 1, 0, 0, 1, 1, 0, 0, 0, -1 };

    public static int[] T5D0 = { 0, 1, 0, 0, 1, 1, 0, 0, 1, -1 };
    public static int[] T5D1 = { 0, 0, 0, 0, 1, 1, 1, 1, 0, -1 };
    public static int[] T5D2 = { 0, 1, 0, 0, 1, 1, 0, 0, 1, -1 };
    public static int[] T5D3 = { 0, 0, 0, 0, 1, 1, 1, 1, 0, -1 };

    public static int[] T6D0 = { 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, -1 };
    public static int[] T6D1 = { 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, -1 };
    public static int[] T6D2 = { 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, -1 };
    public static int[] T6D3 = { 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, -1 };

    public static int[][] array = {
          T0D0, T0D1, T0D2, T0D3,
          T1D0, T1D1, T1D2, T1D3,
          T2D0, T2D1, T2D2, T2D3,
          T3D0, T3D1, T3D2, T3D3,
          T4D0, T4D1, T4D2, T4D3,
          T5D0, T5D1, T5D2, T5D3,
          T6D0, T6D1, T6D2, T6D3,
        };
}
