using System;
using System.IO;
using System.Linq;

namespace MatrixDistance;
public static class LeitorArquivo
{
    private static string[] LerArquivo (string nomeArquivo) {
        string caminhoDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string caminhoArquivo = Path.Combine(caminhoDesktop, nomeArquivo);
        using var reader = new StreamReader(caminhoArquivo);
        string[] conteudo = File.ReadAllLines(caminhoArquivo);
        if (conteudo.Length == 0) {
            Console.WriteLine($"Aviso: arquivo {nomeArquivo} vazio.");
        }
        return conteudo;
    }

    private static string LerUmaLinha (string nomeArquivo) {
        string caminhoDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string caminhoArquivo = Path.Combine(caminhoDesktop, nomeArquivo);
        using var reader = new StreamReader(caminhoArquivo);
        string linha = "";
        try {
            linha = File.ReadLines(caminhoArquivo).First();
        } catch {
            Console.WriteLine($"Aviso: arquivo {nomeArquivo} vazio.");
        }
        return linha;
    }

    private static int Comprimento (this string linha)
        => linha.Count(x => x == ',') + 1;

    public static (int, int[,]) LerMatriz(string nomeArquivo = "matriz.txt") {
        string[] linhas = LerArquivo(nomeArquivo);
        int n = linhas.Length;
        int[,] matriz = new int[n,n];
        for (int i = 0; i < n; i++) {
            if (linhas[i].Comprimento() != n) {
                throw new Exception($"Número de colunas da linha {i.ParaUsuario()} não coincide com número de linhas da matriz.");
            }
            string[] palavras = linhas[i].Split(",");
            for (int j = 0; j < n; j++) {
                int entrada;
                if (!int.TryParse(palavras[j], out entrada)) {
                    throw new Exception($"Erro na leitura da entrada {i.ParaUsuario()},{j.ParaUsuario()}.");
                }
                matriz[i,j] = entrada;
            }
        }
        return (n, matriz);
    }

    public static int[] LerPercurso(int n = 5, string nomeArquivo = "caminho.txt") {
        string[] textoPercurso = LerUmaLinha(nomeArquivo).Split(",");
        int[] percurso = textoPercurso.Select(x => (int.TryParse(x, out int y) ? y : -1)).ToArray();
        int indiceErro = Array.FindIndex(percurso, x => x <= 0 || x > n);
        if (indiceErro >= 0) {
            throw new Exception($"Erro na leitura da {indiceErro.ParaUsuario()}a cidade.");
        }
        return percurso.ParaSistema();
    }

}
