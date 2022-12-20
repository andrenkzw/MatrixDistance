using System;
using System.Linq;

namespace MatrixDistance;
class Program
{
    static int LerTamanhoUsuario() {
        int n;
        Console.WriteLine($"Insira o número de cidades:");
        while (!int.TryParse(Console.ReadLine(), out n)) {
            Console.WriteLine("Erro na leitura.");
            Console.WriteLine($"Insira o número de cidades:");
        }
        return n;        
    }

    static int LerDistUsuario(int i, int j) {
        int dist;
        Console.WriteLine($"Insira a distância entre a cidade {i} e a cidade {j}:");
        while (!int.TryParse(Console.ReadLine(), out dist)) {
            Console.WriteLine("Erro na leitura.");
            Console.WriteLine($"Insira a distância entre a cidade {i} e a cidade {j}:");
        }
        return dist;
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
        Console.WriteLine($"Insira o percurso com as cidades separadas por espaço:");
        string[] textoPercurso = (Console.ReadLine() ?? "").Split(" ");
        int[] percurso = textoPercurso.Select(x => (int.TryParse(x, out int y) ? y : -1)).ToArray();
        while (percurso.Any(x => x <= 0 || x > n)) {
            Console.WriteLine("Erro na leitura.");
            Console.WriteLine($"Insira o percurso com as cidades separadas por espaço:");
            textoPercurso = (Console.ReadLine() ?? "").Split(" ");
            percurso = textoPercurso.Select(x => (int.TryParse(x, out int y) ? y : -1)).ToArray();
        }
        return percurso.ParaSistema();
    }

    static int CalculaDistancia(int[,] matriz, int[] percurso)
        => Enumerable.Range(0,percurso.Length-1).Select(x => matriz[percurso[x],percurso[x+1]]).Sum();

    static void Main(string[] args)
    {
        int n = LerTamanhoUsuario();
        int[,] matriz = LerMatrizUsuario(n);
        int[] percurso = LerPercursoUsuario(n);
        Console.WriteLine($"A distância percorrida no percurso {string.Join(" ", percurso.Select(x => x+1))} é {CalculaDistancia(matriz, percurso)} km.");
    }
}
