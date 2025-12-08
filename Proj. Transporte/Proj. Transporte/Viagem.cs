using System;

namespace ProjetoTransporte
{
    public class Viagem
    {
        public int Id { get; private set; }
        public int IdOrigem { get; private set; }
        public int IdDestino { get; private set; }

        public string NomeOrigem { get; private set; }
        public string NomeDestino { get; private set; }

        public int IdVeiculo { get; private set; }
        public int Passageiros { get; private set; }
        public DateTime DataHora { get; private set; }

        private static int nextId = 1;

        public Viagem(int idOrigem, int idDestino, string origemNome, string destinoNome, int idVeiculo, int passageiros)
        {
            Id = nextId++;
            IdOrigem = idOrigem;
            IdDestino = idDestino;
            NomeOrigem = origemNome;
            NomeDestino = destinoNome;
            IdVeiculo = idVeiculo;
            Passageiros = passageiros;
            DataHora = DateTime.Now;
        }

        public override string ToString()
        {
            return
                $"Viagem {Id}: {NomeOrigem} > {NomeDestino} | Veículo {IdVeiculo} | Passageiros: {Passageiros} | {DataHora:dd/MM HH:mm}";
        }
    }
}
