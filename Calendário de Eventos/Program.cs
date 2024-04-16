using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        ListaEventos listaEventos = new ListaEventos();

        while (true)
        {
            Console.WriteLine("=== Lista de Eventos ===");
            Console.WriteLine("1. Adicionar Evento");
            Console.WriteLine("2. Exibir Eventos");
            Console.WriteLine("3. Pesquisar Eventos por Data Específica");
            Console.WriteLine("4. Pesquisar Eventos por Responsável");
            Console.WriteLine("5. Pesquisar Eventos por Período");
            Console.WriteLine("6. Editar Evento");
            Console.WriteLine("7. Apagar Evento");
            Console.WriteLine("8. Exportar Eventos para arquivo de texto");
            Console.WriteLine("9. Sair");

            Console.Write("Escolha uma opção: ");
            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    listaEventos.AdicionarEvento();
                    break;
                case "2":
                    listaEventos.ExibirEventos();
                    break;
                case "3":
                    listaEventos.PesquisarEventosPorData();
                    break;
                case "4":
                    listaEventos.PesquisarEventosPorResponsavel();
                    break;
                case "5":
                    listaEventos.PesquisarEventosPorPeriodo();
                    break;
                case "6":
                    listaEventos.EditarEvento();
                    break;
                case "7":
                    listaEventos.ApagarEvento();
                    break;
                case "8":
                    listaEventos.ExportarEventosParaArquivo();
                    break;
                case "9":
                    Console.WriteLine("Saindo...");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.WriteLine();
        }
    }
}

class Evento
{
    public string Codigo { get; private set; }
    public string Nome { get; private set; }
    public DateTime DataEvento { get; private set; }
    public string HoraInicio { get; private set; }
    public string HoraTermino { get; private set; }
    public int QuantidadePessoas { get; private set; }
    public string PublicoAlvo { get; private set; }
    public string NomeResponsavel { get; private set; }
    public string EmailResponsavel { get; private set; }
    public string TelefoneResponsavel { get; private set; }
    public string DescricaoEvento { get; private set; }

    public Evento(string codigo, string nome, DateTime dataEvento, string horaInicio, string horaTermino, int quantidadePessoas, string publicoAlvo, string nomeResponsavel, string emailResponsavel, string telefoneResponsavel, string descricaoEvento)
    {
        Codigo = codigo;
        Nome = nome;
        DataEvento = dataEvento;
        HoraInicio = horaInicio;
        HoraTermino = horaTermino;
        QuantidadePessoas = quantidadePessoas;
        PublicoAlvo = publicoAlvo;
        NomeResponsavel = nomeResponsavel;
        EmailResponsavel = emailResponsavel;
        TelefoneResponsavel = telefoneResponsavel;
        DescricaoEvento = descricaoEvento;
    }

    public override string ToString()
    {
        return $"Código: {Codigo} - Nome: {Nome} - Data: {DataEvento.ToShortDateString()} - Início: {HoraInicio} - Término: {HoraTermino} - Pessoas: {QuantidadePessoas} - Público-Alvo: {PublicoAlvo} - Nome Responsável: {NomeResponsavel} - E-mail Responsável: {EmailResponsavel} - Telefone Responsável: {TelefoneResponsavel} - Descrição: {DescricaoEvento}";
    }
}

class ListaEventos
{
    private List<Evento> eventos;

    public ListaEventos()
    {
        eventos = new List<Evento>();
    }

    public void AdicionarEvento()
    {
        string codigoEvento = GerarCodigo();
        Console.Write("Digite o nome do evento a ser adicionado: ");
        string nomeEvento = Console.ReadLine();
        Console.Write("Digite a data do evento (formato DD/MM/AAAA): ");
        DateTime dataEvento = DateTime.Parse(Console.ReadLine());
        Console.Write("Digite a hora de início do evento (formato HH:MM): ");
        string horaInicio = Console.ReadLine();
        Console.Write("Digite a hora de término do evento (formato HH:MM): ");
        string horaTermino = Console.ReadLine();
        Console.Write("Digite a quantidade de pessoas que irão ao evento: ");
        int quantidadePessoas = int.Parse(Console.ReadLine());
        Console.Write("Digite o público-alvo do evento: ");
        string publicoAlvo = Console.ReadLine();
        Console.Write("Digite o nome do responsável pelo evento: ");
        string nomeResponsavel = Console.ReadLine();
        Console.Write("Digite o e-mail do responsável pelo evento: ");
        string emailResponsavel = Console.ReadLine();
        Console.Write("Digite o telefone do responsável pelo evento: ");
        string telefoneResponsavel = Console.ReadLine();
        Console.Write("Digite uma breve descrição do evento: ");
        string descricaoEvento = Console.ReadLine();

        Evento novoEvento = new Evento(codigoEvento, nomeEvento, dataEvento, horaInicio, horaTermino, quantidadePessoas, publicoAlvo, nomeResponsavel, emailResponsavel, telefoneResponsavel, descricaoEvento);
        eventos.Add(novoEvento);
        Console.WriteLine($"Evento adicionado com sucesso! Código: {codigoEvento}");
    }

    public void ExibirEventos()
    {
        if (eventos.Count == 0)
        {
            Console.WriteLine("Nenhum evento cadastrado.");
            return;
        }

        Console.WriteLine("Eventos:");
        foreach (var evento in eventos)
        {
            Console.WriteLine(evento);
        }
    }

    public void PesquisarEventosPorData()
    {
        Console.Write("Digite a data específica para pesquisar eventos (formato DD/MM/AAAA): ");
        DateTime dataPesquisa = DateTime.Parse(Console.ReadLine());

        var eventosNaData = eventos.Where(e => e.DataEvento.Date == dataPesquisa.Date).ToList();

        if (eventosNaData.Count == 0)
        {
            Console.WriteLine($"Nenhum evento encontrado na data {dataPesquisa.ToShortDateString()}.");
        }
        else
        {
            Console.WriteLine($"Eventos na data {dataPesquisa.ToShortDateString()}:");
            foreach (var evento in eventosNaData)
            {
                Console.WriteLine(evento);
            }
        }
    }

    public void PesquisarEventosPorResponsavel()
    {
        Console.Write("Digite o nome do responsável pelo evento: ");
        string nomeResponsavel = Console.ReadLine();

        var eventosDoResponsavel = eventos.Where(e => e.NomeResponsavel == nomeResponsavel).ToList();

        if (eventosDoResponsavel.Count == 0)
        {
            Console.WriteLine($"Nenhum evento encontrado para o responsável {nomeResponsavel}.");
        }
        else
        {
            Console.WriteLine($"Eventos do responsável {nomeResponsavel}:");
            foreach (var evento in eventosDoResponsavel)
            {
                Console.WriteLine(evento);
            }
        }
    }

    public void PesquisarEventosPorPeriodo()
    {
        Console.Write("Digite a data de início do período (formato DD/MM/AAAA): ");
        DateTime dataInicio = DateTime.Parse(Console.ReadLine());
        Console.Write("Digite a data de término do período (formato DD/MM/AAAA): ");
        DateTime dataFim = DateTime.Parse(Console.ReadLine());

        var eventosNoPeriodo = eventos.Where(e => e.DataEvento.Date >= dataInicio.Date && e.DataEvento.Date <= dataFim.Date).ToList();

        if (eventosNoPeriodo.Count == 0)
        {
            Console.WriteLine($"Nenhum evento encontrado no período de {dataInicio.ToShortDateString()} a {dataFim.ToShortDateString()}.");
        }
        else
        {
            Console.WriteLine($"Eventos no período de {dataInicio.ToShortDateString()} a {dataFim.ToShortDateString()}:");
            foreach (var evento in eventosNoPeriodo)
            {
                Console.WriteLine(evento);
            }
        }
    }

    public void EditarEvento()
    {
        Console.Write("Digite o código do evento que deseja editar: ");
        string codigoEvento = Console.ReadLine();

        var eventoParaEditar = eventos.FirstOrDefault(e => e.Codigo == codigoEvento);
        if (eventoParaEditar != null)
        {
            eventos.Remove(eventoParaEditar);
            Console.WriteLine("Informe os novos dados do evento:");
            AdicionarEvento();
        }
        else
        {
            Console.WriteLine($"Evento com código {codigoEvento} não encontrado.");
        }
    }

    public void ApagarEvento()
    {
        Console.Write("Digite o código do evento que deseja apagar: ");
        string codigoEvento = Console.ReadLine();

        var eventoParaApagar = eventos.FirstOrDefault(e => e.Codigo == codigoEvento);
        if (eventoParaApagar != null)
        {
            eventos.Remove(eventoParaApagar);
            Console.WriteLine($"Evento com código {codigoEvento} apagado com sucesso.");
        }
        else
        {
            Console.WriteLine($"Evento com código {codigoEvento} não encontrado.");
        }
    }

    public void ExportarEventosParaArquivo()
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "eventos.txt");

        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var evento in eventos)
                {
                    writer.WriteLine(evento);
                }
            }

            Console.WriteLine($"Eventos exportados para o arquivo: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro ao exportar os eventos: {ex.Message}");
        }
    }

    private string GerarCodigo()
    {
        const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(caracteres, 6).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}