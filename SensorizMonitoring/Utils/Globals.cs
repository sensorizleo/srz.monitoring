using System.Text.RegularExpressions;

namespace SensorizMonitoring
{
    public class Globals
    {
        private const string PastaArquivos = "LOGS";

        public void EscreverArquivo(string line)
        {
            // Verifica se a pasta de arquivos existe, senão cria
            string pastaArquivos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PastaArquivos);
            if (!Directory.Exists(pastaArquivos))
            {
                Directory.CreateDirectory(pastaArquivos);
            }

            // Gera o nome do arquivo com base na data atual
            string nomeArquivo = $"arquivo_{DateTime.Now:yyyyMMdd}.txt";
            string caminhoArquivo = Path.Combine(pastaArquivos, nomeArquivo);

            string conteudo = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - " + line + "\n";
            // Escreve o conteúdo no arquivo
            File.AppendAllText(caminhoArquivo, conteudo);

            // Remove arquivos com mais de 5 dias
            RemoverArquivosAntigos(pastaArquivos, TimeSpan.FromDays(5));
        }
        private void RemoverArquivosAntigos(string pastaArquivos, TimeSpan retencao)
        {
            DateTime dataLimite = DateTime.Now - retencao;

            // Percorre os arquivos na pasta e remove os mais antigos
            foreach (string arquivo in Directory.GetFiles(pastaArquivos))
            {
                DateTime dataCriacao = File.GetCreationTime(arquivo);
                if (dataCriacao < dataLimite)
                {
                    File.Delete(arquivo);
                }
            }
        }
        public bool IsValidEmail(string email)
        {
            // Padrão de expressão regular para verificar a estrutura do email
            string pattern = @"^[\w\.-]+@([\w-]+\.)+[\w-]{2,4}$";

            // Verifica se o email corresponde ao padrão
            return Regex.IsMatch(email, pattern);
        }

        public int OffSet(int limit, int page)
        {
            // Padrão de expressão regular para verificar a estrutura do email
            return (page - 1) * limit;
        }
    }

}