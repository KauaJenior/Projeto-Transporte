using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoTransporte
{
    public class GestorFrota
    {
        private List<Veiculo> frota = new List<Veiculo>();
        private List<Garagem> garagens = new List<Garagem>();
        private List<Viagem> viagens = new List<Viagem>();

        private HashSet<int> origensBloqueadas = new HashSet<int>();

        public bool JornadaIniciada { get; private set; } = false;

        public IReadOnlyList<Veiculo> Frota => frota;
        public IReadOnlyList<Garagem> Garagens => garagens;
        public IReadOnlyList<Viagem> Viagens => viagens;

        

        public bool CadastrarVeiculo(Veiculo v)
        {
            if (JornadaIniciada) return false;
            if (frota.Any(x => x.Id == v.Id)) return false;
            frota.Add(v);
            return true;
        }

        public bool CadastrarGaragem(Garagem g)
        {
            if (JornadaIniciada) return false;
            if (garagens.Any(x => x.Id == g.Id)) return false;
            garagens.Add(g);
            return true;
        }

        

        public bool IniciarJornada()
        {
            if (JornadaIniciada) return false;
            if (garagens.Count == 0) return false;

            var vList = frota.ToList();

            int i = 0;
            foreach (var v in vList)
            {
                garagens[i % garagens.Count].EstacionarVeiculo(v);
                i++;
            }

            JornadaIniciada = true;
            origensBloqueadas.Clear();
            return true;
        }

        public List<string> EncerrarJornada()
        {
            var rel = new List<string>();
            rel.Add("=== RELATÓRIO DE ENCERRAMENTO DA JORNADA ===");

            foreach (var v in frota)
            {
                rel.Add($"Veículo {v.Id} ({v.Placa}) transportou {v.TotalPassageirosHoje} passageiros.");
                v.ResetarPassageiros();
            }

            viagens.Clear();
            JornadaIniciada = false;
            origensBloqueadas.Clear();

            return rel;
        }

        

        public (bool ok, string msg) LiberarViagem(int idOrigem, int idDestino)
        {
            if (!JornadaIniciada)
                return (false, "Jornada não iniciada.");

            var origem = garagens.FirstOrDefault(g => g.Id == idOrigem);
            var destino = garagens.FirstOrDefault(g => g.Id == idDestino);

            if (origem == null) return (false, "Origem não encontrada.");
            if (destino == null) return (false, "Destino não encontrado.");
            if (origem.Vazia()) return (false, "Origem sem veículos.");
            if (origensBloqueadas.Contains(idOrigem)) return (false, "Origem bloqueada (aguardando retorno).");

            var ve = origem.LiberarVeiculo();

            ve.RegistrarPassageiros(ve.Capacidade);

            destino.EstacionarVeiculo(ve);

            if (origem.Vazia())
                origensBloqueadas.Add(idOrigem);

            if (origensBloqueadas.Contains(idDestino))
                origensBloqueadas.Remove(idDestino);

            viagens.Add(new Viagem(
                idOrigem, idDestino,
                origem.Nome, destino.Nome,
                ve.Id, ve.Capacidade));

            return (true, "Viagem registrada com sucesso.");
        }

        

        public Garagem ObterGaragem(int id) =>
            garagens.FirstOrDefault(g => g.Id == id);

        public Veiculo ObterVeiculo(int id) =>
            frota.FirstOrDefault(v => v.Id == id);

        public IEnumerable<Veiculo> ListarVeiculosEmGaragem(int idGaragem)
        {
            return ObterGaragem(idGaragem)?.ListarVeiculos() ?? Enumerable.Empty<Veiculo>();
        }

        public int GetQtdViagens(int o, int d) =>
            viagens.Count(v => v.IdOrigem == o && v.IdDestino == d);

        public IEnumerable<Viagem> GetViagens(int o, int d) =>
            viagens.Where(v => v.IdOrigem == o && v.IdDestino == d);

        public int GetPassageiros(int o, int d) =>
            viagens.Where(v => v.IdOrigem == o && v.IdDestino == d)
                   .Sum(v => v.Passageiros);
    }
}
