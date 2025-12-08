using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoTransporte
{
    public class Garagem
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        private Stack<Veiculo> veiculos = new Stack<Veiculo>();

        public Garagem(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public void EstacionarVeiculo(Veiculo v)
        {
            veiculos.Push(v);
        }

        public Veiculo LiberarVeiculo()
        {
            return veiculos.Pop();
        }

        public bool Vazia() => veiculos.Count == 0;

        public int QuantidadeVeiculos() => veiculos.Count;

        public int PotencialTransporte() => veiculos.Sum(v => v.Capacidade);

        public IEnumerable<Veiculo> ListarVeiculos()
        {
            return veiculos.ToArray(); 
        }

        public override string ToString()
        {
            return $"[{Id}] {Nome} | Veículos: {QuantidadeVeiculos()} | Potencial: {PotencialTransporte()}";
        }
    }
}
