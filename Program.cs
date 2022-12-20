using System;
using System.Linq;

namespace MatrixDistance;
class Program
{
    static int CalculaDistancia(int[,] matriz, int[] percurso)
        => Enumerable.Range(0,percurso.Length-1).Select(x => matriz[percurso[x],percurso[x+1]]).Sum();

    static void Main(string[] args)
    {
        (int n, int[,] matriz) = LeitorArquivo.LerMatriz();
        int[] percurso = LeitorArquivo.LerPercurso(n);
        Console.WriteLine($"A distância percorrida no percurso {string.Join(" ", percurso.ParaUsuario())} é {CalculaDistancia(matriz, percurso)} km.");
    }
}
