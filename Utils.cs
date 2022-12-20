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
    
    public static int LerInt(Func<(bool,int)> ler) {
        (bool sucesso, int resultado) = ler();
        while (!sucesso) {
            Console.WriteLine("Erro na leitura.");
            (sucesso, resultado) = ler();
        }
        return resultado;
    }

    public static int[] LerArray(Func<int[]> ler, Func<int[], bool> testar) {
        int[] resultado = ler();
        while (!testar(resultado)) {
            Console.WriteLine("Erro na leitura.");
            resultado = ler();
        }
        return resultado;
    }
}
