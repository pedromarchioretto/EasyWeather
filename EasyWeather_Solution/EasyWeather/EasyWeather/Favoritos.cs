using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EasyWeather
{
    public class Favoritos
    {
        public void SalvarDadosLocalmente(string dados)
        {
            string nomeArquivo = "CidadesFavoritas.txt";
            string caminhoCompleto = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), nomeArquivo);

            File.AppendAllText(caminhoCompleto, $"{dados}{Environment.NewLine}");
        }


        public void RemoverCidadeFavorita(string cidade)
        {
            string nomeArquivo = "CidadesFavoritas.txt";
            string caminhoCompleto = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), nomeArquivo);

            if (File.Exists(caminhoCompleto))
            {
                // Lê todas as linhas do arquivo
                List<string> linhas = File.ReadAllLines(caminhoCompleto).ToList();

                // Remove a linha que contém o nome da cidade
                linhas.Remove(cidade);

                // Reescreve o arquivo com as linhas restantes
                File.WriteAllLines(caminhoCompleto, linhas);
            }
        }


        public List<string> ListarCidadesFavoritas()
        {
            string nomeArquivo = "CidadesFavoritas.txt";
            string caminhoCompleto = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), nomeArquivo);

            List<string> cidades = new List<string>();

            if (File.Exists(caminhoCompleto))
            {
                // Lê todas as linhas do arquivo e adiciona cada uma à lista de cidades
                string[] linhas = File.ReadAllLines(caminhoCompleto);
                cidades.AddRange(linhas);
            }

            return cidades;
        }

        public bool CidadeExiste(string cidade)
        {
            string nomeArquivo = "CidadesFavoritas.txt";
            string caminhoCompleto = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), nomeArquivo);

            if (File.Exists(caminhoCompleto))
            {
                // Lê todas as linhas do arquivo
                List<string> linhas = File.ReadAllLines(caminhoCompleto).ToList();

                // Verifica se a lista contém a cidade
                if (linhas.Contains(cidade))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
