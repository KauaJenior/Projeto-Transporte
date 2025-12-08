using System;

namespace ProjetoTransporte
{
    public class Veiculo
    {
        public int Id { get; private set; }
        public string Placa { get; private set; }
        public int Capacidade { get; private set; }
        public int TotalPassageirosHoje { get; private set; }

        public Veiculo(int id, string placa, int capacidade)
        {
            Id = id;
            Placa = placa;
            Capacidade = capacidade;
            TotalPassageirosHoje = 0;
        }

        public void RegistrarPassageiros(int qtd)
        {
            TotalPassageirosHoje += qtd;
        }

        public void ResetarPassageiros() => TotalPassageirosHoje = 0;

        public override string ToString()
        {
            return $"Veículo {Id} | {Placa} | Cap: {Capacidade} | Total hoje: {TotalPassageirosHoje}";
        }
    }
}
