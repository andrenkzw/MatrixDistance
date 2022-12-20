using System;
using System.Linq;

namespace MatrixDistance;
public static class Utils
{
    public static int ParaUsuario(this int intSistema)
        => intSistema+1;
    
    public static int ParaSistema(this int intUsuario)
        => intUsuario-1;

    public static int[] ParaUsuario(this int[] arraySistema)
        => arraySistema.Select(x => x+1).ToArray();
    
    public static int[] ParaSistema(this int[] arrayUsuario)
        => arrayUsuario.Select(x => x-1).ToArray();
}
