using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;




namespace WebApplication1.Controllers
{
    // Define a rota base para o controlador e que ele é um controlador de API
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        // Declara uma variável privada para armazenar a configuração
        private IConfiguration _configuration;

        // Construtor do controlador que recebe a configuração e a armazena na variável privada, onde ela é injetada atraves do construtor
        public AgendaController(IConfiguration configuration)
        {
            //essa variavel é usada para acessar as configuraçoes armazenadas no appsettings.json, por exemplo o AgendaDB

            _configuration = configuration;
        }

        //get para obter os dados do DB
        [HttpGet]
        [Route("GetNotes")]
        public JsonResult GetNotes()
        {
            // Define a consulta SQL para selecionar todos os registros da tabela "compromissos"
            string querry = "SELECT * FROM dbo.compromissos";

            // Cria uma nova tabela de dados para armazenar os resultados
            DataTable table = new DataTable();

            // Obtém a string de conexão do banco de dados a partir da configuração
            string sqlDatasource = _configuration.GetConnectionString("AgendaDB");

            // Declara um leitor de dados SQL
            SqlDataReader myReader;

            // Cria uma conexão com o banco de dados usando a string de conexão
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                // Abre a conexão com o banco de dados
                myCon.Open();

                // Cria um comando SQL para executar a consulta
                using (SqlCommand myCommand = new SqlCommand(querry, myCon))
                {
                    // Executa o comando e obtém um leitor de dados
                    myReader = myCommand.ExecuteReader();
                    // Carrega os dados lidos na tabela de dados
                    table.Load(myReader);
                    // Fecha o leitor de dados
                    myReader.Close();
                    // Fecha a conexão com o banco de dados
                    myCon.Close();

                }
            }
            // Retorna a tabela de dados como um resultado JSON
            return new JsonResult(table);
        }

        //post para adicionar notas no db
        [HttpPost]
        [Route("AddNotes")]

        public JsonResult AddNotes([FromForm] string newNotes, [FromForm] string newDates)
        {

            // Define a consulta SQL para inserir dados na tabela compromissos
            // Especifica as colunas 'descricao' e 'data' e usa parâmetros para os valores
            string querry = "insert into dbo.compromissos  (descricao, data) VALUES (@newNotes, @newDates)";

            // Cria uma nova tabela de dados para armazenar os resultados
            DataTable table = new DataTable();

            // Obtém a string de conexão do banco de dados a partir da configuração
            string sqlDatasource = _configuration.GetConnectionString("AgendaDB");

            // Declara um leitor de dados SQL
            SqlDataReader myReader;

            // Cria uma conexão com o banco de dados usando a string de conexão
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                // Abre a conexão com o banco de dados
                myCon.Open();

                // Cria um comando SQL para executar a consulta
                using (SqlCommand myCommand = new SqlCommand(querry, myCon))
                {
                    //adicionam parâmetros à consulta SQL para evitar ataques de SQL Injection e garantir que os valores corretos sejam usados na consulta
                    //AddWithValue  :é usado para adicionar um novo parâmetro e seu valor à coleção de parâmetros de um SqlCommand. Ele associa o nome do parâmetro ao valor fornecido, que será inserido na consulta SQL.

                    myCommand.Parameters.AddWithValue("@newNotes", newNotes);
                    myCommand.Parameters.AddWithValue("@newDates", newDates);

                    // Executa o comando e obtém um leitor de dados
                    myReader = myCommand.ExecuteReader();
                    // Carrega os dados lidos na tabela de dados
                    table.Load(myReader);
                    // Fecha o leitor de dados
                    myReader.Close();
                    // Fecha a conexão com o banco de dados
                    myCon.Close();


                }
            }
            // Retorna a tabela de dados como um resultado JSON
            return new JsonResult("Adicionado com sucesso!");
        }

        //delete para apagar resultados do banco
        [HttpDelete]
        [Route("DeleteNotes")]

        public JsonResult DeleteNotes(int id)
        {
            // Define a consulta SQL para deletar dados da tabela 'compromissos' onde o id corresponde ao parâmetro
            string querry = "delete from dbo.compromissos where id=@id";

            // Cria uma nova tabela de dados para armazenar os resultados
            DataTable table = new DataTable();

            // Obtém a string de conexão do banco de dados a partir da configuração
            string sqlDatasource = _configuration.GetConnectionString("AgendaDB");

            // Declara um leitor de dados SQL
            SqlDataReader myReader;

            // Cria uma conexão com o banco de dados usando a string de conexão
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                // Abre a conexão com o banco de dados
                myCon.Open();

                // Cria um comando SQL para executar a consulta
                using (SqlCommand myCommand = new SqlCommand(querry, myCon))
                {
                    //diz que o valor @id é igual ao id
                    myCommand.Parameters.AddWithValue("@id", id);
                    // Executa o comando e obtém um leitor de dados
                    myReader = myCommand.ExecuteReader();
                    // Carrega os dados lidos na tabela de dados
                    table.Load(myReader);
                    // Fecha o leitor de dados
                    myReader.Close();
                    // Fecha a conexão com o banco de dados
                    myCon.Close();

                }
            }
            // Retorna a tabela de dados como um resultado JSON
            return new JsonResult("deletado com sucesso!");
        }

    }
}
