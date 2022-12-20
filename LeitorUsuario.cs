using System;
using System.Linq;

namespace MatrixDistance;
public static class LeitorUsuario
{
    public static int LerTamanho() {
        return Utils.LerInt(
            () => {
                Console.WriteLine($"Insira o número de cidades:");
                bool sucesso = int.TryParse(Console.ReadLine(), out int n);
                return (sucesso, n);
            }
        );
    }

    private static int LerDistancia(int i, int j) {
        return Utils.LerInt(
            () => {
                Console.WriteLine($"Insira a distância entre a cidade {i} e a cidade {j}:");
                bool sucesso = int.TryParse(Console.ReadLine(), out int dist);
                return (sucesso, dist);
            }
        );
    }

    public static int[,] LerMatriz(int n = 5) {
        int[,] matriz = new int[n,n];
        for (int i = 0; i < n; i++) {
            for (int j = i+1; j < n; j++) {
                int dist = LerDistancia(i.ParaUsuario(), j.ParaUsuario());
                matriz[i,j] = dist;
                matriz[j,i] = dist;
            }
        }
        return matriz;
    }

    public static int[] LerPercurso(int n = 5) {
        return Utils.LerArray(
            () => {
                Console.WriteLine($"Insira o percurso com as cidades separadas por espaço:");
                string[] textoPercurso = (Console.ReadLine() ?? "").Split(" ");
                return  textoPercurso.Select(x => (int.TryParse(x, out int y) ? y : -1)).ToArray();
            },
            (int[] percurso) => {
                return !percurso.Any(x => x <= 0 || x > n);
            }
        ).ParaSistema();
    }

}
