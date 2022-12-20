using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace MatrixDistance;
public static class LeitorCSV
{
    private static dynamic[] LerArquivo (string nomeArquivo) {
        string caminhoDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string caminhoArquivo = Path.Combine(caminhoDesktop, nomeArquivo);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
        };
        using (var reader = new StreamReader(caminhoArquivo))
        using (var csv = new CsvReader(reader, config))
        {
            var conteudo = csv.GetRecords<dynamic>().ToArray();
            if (conteudo.Length == 0) {
                Console.WriteLine($"Aviso: arquivo {nomeArquivo} vazio.");
            }
            return conteudo;
        }
    }

    private static dynamic? LerUmaLinha (string nomeArquivo) {
        string caminhoDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string caminhoArquivo = Path.Combine(caminhoDesktop, nomeArquivo);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
        };
        using (var reader = new StreamReader(caminhoArquivo))
        using (var csv = new CsvReader(reader, config))
        {
            if (!csv.Read()) {
                Console.WriteLine($"Aviso: arquivo {nomeArquivo} vazio.");
            }
            return csv.GetRecord<dynamic>();
        }
    }

    public static (int, int[,]) LerMatriz(string nomeArquivo = "matriz.txt") {
        dynamic[] linhas = LerArquivo(nomeArquivo);
        int n = linhas.Length;
        int[,] matriz = new int[n,n];
        for (int i = 0; i < n; i++) {
            var linha = (System.Dynamic.ExpandoObject)linhas[i];
            if (linha.Count() != n) {
                throw new Exception($"Número de colunas da linha {i.ParaUsuario()} não coincide com número de linhas da matriz.");
            }
            string[] palavras = linha.Select(x => x.Value.ToString()).ToArray();
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
        var primeiraLinha = (System.Dynamic.ExpandoObject?)LerUmaLinha(nomeArquivo);
        string[] textoPercurso = primeiraLinha.Select(x => x.Value.ToString()).ToArray();
        int[] percurso = textoPercurso.Select(x => (int.TryParse(x, out int y) ? y : -1)).ToArray();
        int indiceErro = Array.FindIndex(percurso, x => x <= 0 || x > n);
        if (indiceErro >= 0) {
            throw new Exception($"Erro na leitura da {indiceErro.ParaUsuario()}a cidade.");
        }
        return percurso.ParaSistema();
    }

}
