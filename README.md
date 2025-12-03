# Sistema de Transporte – Controle de Frota com Pilha (Console Application)

## 📌 Descrição Geral
Este sistema controla a frota de vans de uma empresa de fretamento que opera viagens entre aeroportos.  
As garagens utilizam **pilha (Stack)** porque os veículos entram de ré e somente o último que entrou pode sair.

O sistema foi desenvolvido seguindo **POO (Programação Orientada a Objetos)**, com encapsulamento, separação de responsabilidades e uso adequado de estruturas de dados.

---

## 📦 Funcionalidades Implementadas

- Cadastro de veículos e garagens  
- Controle de início e encerramento da jornada  
- Distribuição alternada dos veículos entre as garagens no início da jornada  
- Liberação de viagens entre garagens  
- Registro completo das viagens  
- Listagem de veículos por garagem  
- Contagem de viagens por rota  
- Contagem de passageiros transportados por rota  
- Uso de **PILHA** na modelagem das garagens  

---

## 🧱 Estrutura do Projeto

O projeto está dividido nas seguintes classes:

---

# **UML – Diagrama de Classes **  


---

--------------------------------------------  
**Veiculo**  
--------------------------------------------  
- id: int  
- placa: string  
- capacidade: int  
- totalPassageirosHoje: int  
--------------------------------------------  
+ Veiculo(int id, string placa, int capacidade): void  
+ RegistrarPassageiros(int qtd): void  
+ ResetarPassageiros(): void  
+ ToString(): string  
--------------------------------------------  

--------------------------------------------  
**Garagem**  
--------------------------------------------  
- id: int  
- nome: string  
- veiculos: Stack<Veiculo>  
--------------------------------------------  
+ Garagem(int id, string nome): void  
+ EstacionarVeiculo(Veiculo v): void  
+ LiberarVeiculo(): Veiculo  
+ Vazia(): bool  
+ QuantidadeVeiculos(): int  
+ PotencialTransporte(): int  
+ ListarVeiculos(): IEnumerable<Veiculo>  
+ ToString(): string  
--------------------------------------------  

---------------------------------------------------  
**Viagem**  
---------------------------------------------------  
- id: int  
- idOrigem: int  
- idDestino: int  
- nomeOrigem: string  
- nomeDestino: string  
- idVeiculo: int  
- passageiros: int  
- dataHora: DateTime  
- nextId: int (static)  
---------------------------------------------------  
+ Viagem(int idOrigem, int idDestino, string nomeOrigem, string nomeDestino, int idVeiculo, int passageiros): void  
+ ToString(): string  
---------------------------------------------------  

---------------------------------------------------  
**GestorFrota**  
---------------------------------------------------  
- frota: List<Veiculo>  
- garagens: List<Garagem>  
- viagens: List<Viagem>  
- origensBloqueadas: HashSet<int>  
- JornadaIniciada: bool  
---------------------------------------------------  
+ CadastrarVeiculo(Veiculo v): bool  
+ CadastrarGaragem(Garagem g): bool  
+ IniciarJornada(): bool  
+ EncerrarJornada(): List<string>  
+ LiberarViagem(int idOrigem, int idDestino): (bool ok, string msg)  
+ ObterGaragem(int id): Garagem  
+ ObterVeiculo(int id): Veiculo  
+ ListarVeiculosEmGaragem(int idGaragem): IEnumerable<Veiculo>  
+ GetQtdViagens(int origem, int destino): int  
+ GetViagens(int origem, int destino): IEnumerable<Viagem>  
+ GetPassageiros(int origem, int destino): int  
---------------------------------------------------  

---

## 🧠 Estrutura de Dados Utilizada
### **Pilha – Stack<Veiculo>**
As garagens utilizam pilha porque:

- Os veículos estacionam de ré.  
- O último que entra é o primeiro que sai (**LIFO**).  
- Modela perfeitamente a dinâmica real de um pátio com saída única.

---

## 🧭 Menu Principal

- **0.** Finalizar  
- **1.** Cadastrar veículo  
- **2.** Cadastrar garagem  
- **3.** Iniciar jornada  
- **4.** Encerrar jornada  
- **5.** Liberar viagem de uma origem para um destino  
- **6.** Listar veículos de uma garagem  
- **7.** Quantidade de viagens entre origem e destino  
- **8.** Listar viagens entre origem e destino  
- **9.** Quantidade de passageiros transportados  
