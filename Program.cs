using System;
using System.Linq;

namespace MatrixDistance;
class Program
{
    static int LerTamanhoUsuario() {
        return Utils.LerInt(
            () => {
                Console.WriteLine($"Insira o número de cidades:");
                bool sucesso = int.TryParse(Console.ReadLine(), out int n);
                return (sucesso, n);
            }
        );
    }

    static int LerDistUsuario(int i, int j) {
        return Utils.LerInt(
            () => {
                Console.WriteLine($"Insira a distância entre a cidade {i} e a cidade {j}:");
                bool sucesso = int.TryParse(Console.ReadLine(), out int dist);
                return (sucesso, dist);
            }
        );
    }

    static int[,] LerMatrizUsuario(int n = 5) {
        int[,] matriz = new int[n,n];
        for (int i = 0; i < n; i++) {
            for (int j = i+1; j < n; j++) {
                int dist = LerDistUsuario(i.ParaUsuario(), j.ParaUsuario());
                matriz[i,j] = dist;
                matriz[j,i] = dist;
            }
        }
        return matriz;
    }

    static int[] LerPercursoUsuario(int n = 5) {
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

    static int CalculaDistancia(int[,] matriz, int[] percurso)
        => Enumerable.Range(0,percurso.Length-1).Select(x => matriz[percurso[x],percurso[x+1]]).Sum();

    static void Main(string[] args)
    {
        int n = LerTamanhoUsuario();
        int[,] matriz = LerMatrizUsuario(n);
        int[] percurso = LerPercursoUsuario(n);
        Console.WriteLine($"A distância percorrida no percurso {string.Join(" ", percurso.ParaUsuario())} é {CalculaDistancia(matriz, percurso)} km.");
    }
}
