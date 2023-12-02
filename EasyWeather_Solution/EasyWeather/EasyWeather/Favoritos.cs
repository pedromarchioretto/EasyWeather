using System;
using System.Collections.Generic;
using System.IO;
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
    }
}
